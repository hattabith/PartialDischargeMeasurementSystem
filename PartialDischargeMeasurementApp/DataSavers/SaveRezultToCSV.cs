using NPOI.SS.Formula.Functions;
using PartialDischargeMeasurementApp.Analysis;
using PartialDischargeMeasurementApp.DataProcessing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialDischargeMeasurementApp.DataSavers
{
    public class SaveRezultToCSV
    {
        private string _filenameCSV;
        private List<ParsedData> _rawData;
        private float _coefficient;
        private const float tenPowMinus12 = 0.000000000001f;
        public SaveRezultToCSV(string filenameCSV, List<ParsedData> rawData) : this (filenameCSV, rawData, 1f) { }
        public SaveRezultToCSV(string filenameCSV, List<ParsedData> rawData, float coefficient)
        {
            _filenameCSV = filenameCSV;
            _rawData = rawData;
            _coefficient = coefficient;
            var partialDischarge = new PDIdentifier(_rawData);
            var pdCollection = new PDDataCollection(_rawData);

            using (StreamWriter sw = new StreamWriter(_filenameCSV))
            {
                //Іфму іьер
                sw.WriteLine("Points at which partial discharges occurred: ");
                sw.WriteLine("Id,CH1,CH2");
                foreach (var pd in partialDischarge.GetPartialDischargeList())
                {
                    sw.WriteLine(pd.Id.ToString() + "," + pd.CH1.ToString() + "," + pd.CH2.ToString());
                }
                sw.WriteLine();
                sw.Write("Zero-crossing points:");
                foreach (var zero in pdCollection.GetZeros())
                {
                    sw.Write("," + zero.ToString());
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total partial discharges:," + pdCollection.GetAllPDCount());
                sw.WriteLine();
                sw.WriteLine("Partial discharges are divided into half-periods: ");
                
                for (int i = 0; i < pdCollection.GetPDHalfPeriodsDataCollection().Count; i++)
                {
                    if (pdCollection.GetPDHalfPeriodsDataCollection()[i].IsPositiveHalfPeriod) sw.WriteLine("Positive voltage in half-period number " + (i + 1).ToString());
                    if (!pdCollection.GetPDHalfPeriodsDataCollection()[i].IsPositiveHalfPeriod) sw.WriteLine("Negative voltage in half-period number " + (i + 1).ToString());
                    sw.WriteLine("Id,CH1,CH2,Single partial discharge full energy");
                    for (int j = 0; j < pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList.Count; j++)
                    {
                        sw.WriteLine(pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].Id.ToString() + "," + 
                            pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].CH1.ToString() + "," + 
                            pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].CH2.ToString() + "," +
                            Math.Abs(pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].CH1 * 1000f) * Math.Abs(pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].CH2 * _coefficient) * tenPowMinus12);

                    }
                    sw.WriteLine();
                }

                //var current = new List<float>(); // необхідно придумати як розкласти все по періодам і півперіодам
                //var fullEnergy = new List<float>();
                //foreach (var pd in pdCollection.GetPDHalfPeriodsDataCollection())
                //{
                //    foreach (var elements in pd.PDList)
                //    {
                //        current.Add(Math.Abs(elements.CH2));
                //        fullEnergy.Add(Math.Abs(elements.CH1) * Math.Abs(elements.CH2));
                //    }
                //}
                //var averageCurrent = current.Sum() / (0.01 * pdCollection.GetPDHalfPeriodsDataCollection().Count);
                //var power = fullEnergy.Sum() / (0.01 * pdCollection.GetPDHalfPeriodsDataCollection().Count);
                //sw.WriteLine();
                //sw.WriteLine("Average current is: " + averageCurrent.ToString());
                //sw.WriteLine("Power is: " + power.ToString());
                //sw.WriteLine();

                var currentPositive = new List<float>();
                var currentNegative = new List<float>();
                var fullEnergyPositive = new List<float>();
                var fullEnergyNegative = new List<float>();
                var current = new List<float>();
                foreach (var pd in pdCollection.GetPDHalfPeriodsDataCollection())
                {
                    if (pd.IsPositiveHalfPeriod)
                    {
                        foreach(var elements in pd.PDList)
                        {
                            currentPositive.Add(Math.Abs(elements.CH2) * _coefficient * tenPowMinus12); //add coef
                            fullEnergyPositive.Add(Math.Abs(elements.CH1) * 1000f * Math.Abs(elements.CH2) * _coefficient * tenPowMinus12); //Add current poz + coef
                        }
                    }
                    if (!pd.IsPositiveHalfPeriod) 
                    {
                        foreach (var elements in pd.PDList)
                        {
                            currentNegative.Add(Math.Abs(elements.CH2) * _coefficient * tenPowMinus12);
                            fullEnergyNegative.Add(Math.Abs(elements.CH1) * 1000f * Math.Abs(elements.CH2) * _coefficient * tenPowMinus12);
                        }
                    }
                }
                var averageCurrentPositive = currentPositive.Sum() / currentPositive.Count; // * 0.01 * halfperiod count
                var averageCurrentNegative = currentNegative.Sum() / currentNegative.Count;
                var powerPositive = fullEnergyPositive.Sum() / fullEnergyPositive.Count;
                var powerNegative = fullEnergyNegative.Sum() / fullEnergyNegative.Count;
                var averageCurrent = (currentPositive.Sum() + currentNegative.Sum()) / (0.01f * pdCollection.GetPDHalfPeriodsDataCollection().Count);
                var averagePower = (fullEnergyPositive.Sum() + fullEnergyNegative.Sum()) / (0.01f * pdCollection.GetPDHalfPeriodsDataCollection().Count);
                sw.WriteLine();
                sw.WriteLine("Average current per second is:," + averageCurrent.ToString());
                sw.WriteLine("Power per second is:," + averagePower.ToString());
                sw.WriteLine("Average positive current half periods is:," + averageCurrentPositive.ToString());
                sw.WriteLine("Average negative current half periods is:," + averageCurrentNegative.ToString());
                sw.WriteLine("Power positive half periods is:," + powerPositive.ToString());
                sw.WriteLine("Power negative half periods is:," + powerNegative.ToString());
                sw.WriteLine();


                var pdCalc = new PDCalculator(pdCollection.GetPDHalfPeriodsDataCollection());
                sw.WriteLine();
                sw.Write("Average value of partial discharges for half-periods: ");
                foreach(var pd in pdCalc.GetAveragePDCollection())
                {
                    sw.Write("," + pd.ToString());
                }
                sw.WriteLine();

                sw.WriteLine();
                sw.Write("Median value of partial discharges for half-periods:,");
                sw.WriteLine(pdCalc.GetMedian());
                sw.WriteLine();

                sw.WriteLine("Data from the oscilloscope: ");
                sw.WriteLine("Id,CH1,CH2");
                foreach (var item in _rawData)
                {
                    sw.WriteLine(item.Id.ToString() + "," + item.CH1.ToString() + "," + item.CH2.ToString());
                }
                sw.WriteLine();
            }
        }
    }
}

// need check
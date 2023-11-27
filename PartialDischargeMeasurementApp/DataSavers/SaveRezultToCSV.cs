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
        public SaveRezultToCSV(string filenameCSV, List<ParsedData> rawData)
        {
            _filenameCSV = filenameCSV;
            _rawData = rawData;
            var partialDischarge = new PDIdentifier(_rawData);
            var pdCollection = new PDDataCollection(_rawData);

            using (StreamWriter sw = new StreamWriter(_filenameCSV))
            {

                sw.WriteLine("Points at which partial discharges occurred: ");
                sw.WriteLine("Id,CH1,CH2");
                foreach (var pd in partialDischarge.GetPartialDischargeList())
                {
                    sw.WriteLine(pd.Id.ToString() + "," + pd.CH1.ToString() + "," + pd.CH2.ToString());
                }
                sw.WriteLine();
                sw.WriteLine("Zero-crossing points: ");
                foreach (var zero in pdCollection.GetZeros())
                {
                    sw.Write("," + zero.ToString());
                }
                sw.WriteLine();
                sw.WriteLine("Total partial discharges: " + pdCollection.GetAllPDCount());
                sw.WriteLine();
                sw.WriteLine("Partial discharges are divided into half-periods: ");

                var pdCalcAll = new List<float> ();
                
                for (int i = 0; i < pdCollection.GetPDHalfPeriodsDataCollection().Count; i++)
                {
                    if (pdCollection.GetPDHalfPeriodsDataCollection()[i].IsPositiveHalfPeriod) sw.WriteLine("Positive voltage in half-period number " + (i + 1).ToString());
                    if (!pdCollection.GetPDHalfPeriodsDataCollection()[i].IsPositiveHalfPeriod) sw.WriteLine("Negative voltage in half-period number " + (i + 1).ToString());
                    sw.WriteLine("Id,CH1,CH2,PD");
                    for (int j = 0; j < pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList.Count; j++)
                    {
                        sw.WriteLine(pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].Id.ToString() + "," + 
                            pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].CH1.ToString() + "," + 
                            pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].CH2.ToString() + "," +
                            Math.Abs(pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].CH1) * Math.Abs(pdCollection.GetPDHalfPeriodsDataCollection()[i].PDList[j].CH2));

                    }
                    sw.WriteLine();
                }

                var pdCalc = new PDCalculator(pdCollection.GetPDHalfPeriodsDataCollection());
                sw.WriteLine();
                sw.WriteLine("Average value of partial discharges for half-periods: ");
                foreach(var pd in pdCalc.GetAveragePDCollection())
                {
                    sw.Write(pd.ToString() + ",");
                }
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("Median value of partial discharges for half-periods: ");
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

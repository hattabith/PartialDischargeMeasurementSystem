using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using PartialDischargeMeasurementApp.DataProcessing;

namespace PartialDischargeMeasurementApp.Analysis
{
    public class PDDataCollection
    {
        private List<ParsedData> _pdList;
        private List<PDHalfPeriodData> _pdHalfPeriodList;
        public PDDataCollection(List<ParsedData> pdList) 
        {
            _pdList = pdList;

            var zeros = new WaveZeroFinder(_pdList);
            var halfPeriods = new HalfPeriodFinder(zeros.GetZeroData(), _pdList.Count);
            var noise = new PDNoiseChecker(_pdList);
            var pd = new PDIdentifier(_pdList);

            var PDElement = new PDHalfPeriodData();

            for (var i = 1; i < halfPeriods.GetRezultHalfPeriodWavePoints().Count; i++)
            {
                var tempElement = new PDHalfPeriodData();
                for (var j = halfPeriods.GetRezultHalfPeriodWavePoints()[i - 1]; j < halfPeriods.GetRezultHalfPeriodWavePoints()[i]; j++)
                {
                    var tempList = new List<ParsedData>();
                    tempList.Clear();
                    foreach (var p in pd.GetPartialDischargeList())
                    {
                        if (p.Id == j)
                        {
                            tempList.Add(p);
                        }
                    }
                    tempElement.PDList = tempList;
                    if (tempElement.PDList[0].CH1 > 0) tempElement.IsPositiveHalfPeriod = true;
                    if (tempElement.PDList[0].CH1 < 0) tempElement.IsPositiveHalfPeriod = false;
                 }
                PDElement = tempElement;
            }

        }
        public List<PDHalfPeriodData> GetPDHalfPeriodsDataCollection() 
        {
            return _pdHalfPeriodList;
        }
        public List<int> GetPDCountPerHalfPeriod()
        {
            var pdCount = new List<int>(); 
            foreach (var pd in _pdHalfPeriodList)
            {
                pdCount.Add(pd.PDList.Count);
            }
            return pdCount;
        }
        public int GetAllPositivePDCount()  // Need checking!!!
        {
            //int count = (from pd in _pdHalfPeriodList where pd.SignVoltage select pd).Count();
            int count = 0;

            foreach (var pd in _pdHalfPeriodList)
            {
                if (pd.IsPositiveHalfPeriod) count += pd.PDList.Count;
            }

            return count;
        }
        public int GetAllNegativePDCount()
        {
            int count = 0;

            foreach (var pd in _pdHalfPeriodList)
            {
                if (!pd.IsPositiveHalfPeriod) count += pd.PDList.Count;
            }

            return count;
        }
        public int GetAllPDCount()
        {
            int count = 0;

            foreach (var pd in _pdHalfPeriodList)
            {
                count += pd.PDList.Count;
            }

            return count;
        }
    }
}

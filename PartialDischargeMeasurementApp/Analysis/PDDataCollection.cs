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
            var noize = new PDNoizeChecker(_pdList);
            var pd = new PDIdentifier(_pdList);

            var PDElement = new PDHalfPeriodData();

            for (int i = 0; i < halfPeriods.GetRezultHalfPeriodWavePoints().Count - 1; i++)
            {
                //var halfPeriodPD = new List<ParsedData>();

                var pdHalfPeriod = (from element in pd.GetPartialDischargeList() where (element.Id >= halfPeriods.GetRezultHalfPeriodWavePoints()[i] && element.Id < halfPeriods.GetRezultHalfPeriodWavePoints()[i + 1]) select element).ToList();
                var dataHalfPeriod = (from element in pdHalfPeriod where element.)
                PDElement.PDList.Add(pdHalfPeriod);
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
                if (pd.SignVoltage) count += pd.PDList.Count;
            }

            return count;
        }
        public int GetAllNegativePDCount()
        {
            int count = 0;

            foreach (var pd in _pdHalfPeriodList)
            {
                if (!pd.SignVoltage) count += pd.PDList.Count;
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

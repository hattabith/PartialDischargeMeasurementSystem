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

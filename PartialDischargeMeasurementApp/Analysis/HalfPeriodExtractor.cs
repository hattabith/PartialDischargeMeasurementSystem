using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialDischargeMeasurementApp.Analysis
{
    internal class HalfPeriodExtractor
    {
        private List<ParsedData> _data;
        private List<ParsedData> _halfPeriodData;
        public HalfPeriodExtractor(List<ParsedData> data) 
        {
            _data = data;
        }
    }
}

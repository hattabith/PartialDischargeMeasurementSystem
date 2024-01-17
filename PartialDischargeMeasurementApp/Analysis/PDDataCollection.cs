using PartialDischargeMeasurementApp.DataProcessing;

namespace PartialDischargeMeasurementApp.Analysis
{
    public class PDDataCollection
    {
        private List<ParsedData> _pdList;
        private List<PDHalfPeriodData> _pdHalfPeriodList;
        private List<int> _zerosData;
        public PDDataCollection(List<ParsedData> pdList)
        {
            _pdList = pdList;

            var zeros = new WaveZeroFinder(_pdList);
            _zerosData = zeros.GetZeroData();
            var halfPeriods = new HalfPeriodFinder(zeros.GetZeroData(), _pdList.Count);
            var noise = new PDNoiseChecker(_pdList);
            var pd = new PDIdentifier(_pdList);

            var PDElements = new List<PDHalfPeriodData>();

            // service = null
            // line fasctory

            for (int i = 0; i < halfPeriods.GetRezultHalfPeriodWavePoints().Count - 2; i++)
            {
                int startIndex = halfPeriods.GetRezultHalfPeriodWavePoints()[i];
                int endIndex = halfPeriods.GetRezultHalfPeriodWavePoints()[i + 1];

                List<ParsedData> partialDischargesInHalfPeriod = pd.GetPartialDischargeList()
                    .Where(d => d.Id >= startIndex && d.Id < endIndex)
                    .ToList();
                bool isPositiveHalfPeriod = partialDischargesInHalfPeriod.Any(d => d.CH1 > 0);

                if (partialDischargesInHalfPeriod.Count > 0)
                {
                    PDElements.Add(new PDHalfPeriodData
                    {
                        PDList = partialDischargesInHalfPeriod,
                        IsPositiveHalfPeriod = isPositiveHalfPeriod
                    });
                }
            }

            _pdHalfPeriodList = PDElements;
        }


        // TODO Need make excel saver
        // HACK test hack
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
        public List<int> GetZeros()
        {
            return _zerosData;
        }
    }
}

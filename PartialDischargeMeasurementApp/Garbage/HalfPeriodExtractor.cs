namespace PartialDischargeMeasurementApp.Garbage
{
    internal class HalfPeriodExtractor
    {
        private List<ParsedData>? _data;
        private List<ParsedData> _halfPeriodData;
        public HalfPeriodExtractor(List<ParsedData>? data)
        {
            _data = data;
        }
        public List<List<ParsedData>> GetHalfPeriodList()
        {

            // need to do
            var abc = new List<List<ParsedData>>();
            abc.Add(_halfPeriodData);
            return abc;
        }
    }
}

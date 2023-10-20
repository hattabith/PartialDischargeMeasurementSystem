
namespace PartialDischargeMeasurementApp
{
    public class PDIdentifier
    {
        private List<ParsedData> _data;
        private float _signalLevel;
        public PDIdentifier(List<ParsedData> data)
        {
            _data = data;

            float sum = 0;

            foreach (ParsedData elements in _data)
            {
                sum += Math.Abs(elements.CH2);
            }

            var middleNoise = sum / _data.Count;
            _signalLevel = middleNoise * 2;

            Console.WriteLine();
            Console.WriteLine("Midle signal noise is: {0}", middleNoise);
            Console.WriteLine();
        }
        public float GetSignalLevel()
        {
            return _signalLevel;
        }
    }
}

namespace PartialDischargeMeasurementApp.DataProcessing
{
    public class PDIdentifier
    {
        private List<ParsedData> _data;
        private List<ParsedData> _pdList = new List<ParsedData>();
        private float _positiveNoise;
        private float _negativeNoise;
        public PDIdentifier(List<ParsedData> data)
        {
            _data = data;

            var noise = new PDNoiseChecker(_data);
            _positiveNoise = noise.GetPositiveNoiseLevel();
            _negativeNoise = noise.GetNegativeNoiseLevel();


            foreach (var elements in _data)
            {
                //if (elements.CH2 > _positiveNoise || elements.CH2 < _negativeNoise) _pdList.Add(elements);
                if (elements.CH2 > _positiveNoise && elements.CH1 > 0) _pdList.Add(elements);
                if (elements.CH2 < _negativeNoise && elements.CH1 < 0) _pdList.Add(elements);
            }

        }
        public PDIdentifier(List<ParsedData> data, float positiveNoise, float negativeNoise)
        {
            _data = data;
            _positiveNoise = positiveNoise;
            _negativeNoise = negativeNoise;

            foreach (var elements in _data)
            {
                //if (elements.CH2 > _positiveNoise || elements.CH2 < _negativeNoise) _pdList.Add(elements);
                if (elements.CH2 > _positiveNoise && elements.CH1 > 0) _pdList.Add(elements);
                if (elements.CH2 < _negativeNoise && elements.CH1 < 0) _pdList.Add(elements);
            }
        }

        public float GetPositiveNoiseLevel()
        {
            return _positiveNoise;
        }
        public float GetNegativeNoiseLevel()
        {
            return _negativeNoise;
        }
        public List<ParsedData> GetPartialDischargeList()
        {
            return _pdList;
        }
    }
}

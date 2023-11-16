namespace PartialDischargeMeasurementApp.DataProcessing
{
    public class PDIdentifier
    {
        private List<ParsedData> _data;
        private List<ParsedData> _pdList = new List<ParsedData>();
        private float _positiveNoize;
        private float _negativeNoize;
        public PDIdentifier(List<ParsedData> data)
        {
            _data = data;

            var noize = new PDNoizeChecker(_data);
            _positiveNoize = noize.GetPozitiveNoizeLevel();
            _negativeNoize = noize.GetNegativeNoizeLevel();


            foreach (var elements in _data)
            {
                //if (elements.CH2 > _positiveNoize || elements.CH2 < _negativeNoize) _pdList.Add(elements);
                if (elements.CH2 > _positiveNoize && elements.CH1 > 0) _pdList.Add(elements);
                if (elements.CH2 < _negativeNoize && elements.CH1 < 0) _pdList.Add(elements);
            }

        }
        public PDIdentifier(List<ParsedData> data, float positiveNoize, float negativeNoize)
        {
            _data = data;
            _positiveNoize = positiveNoize;
            _negativeNoize = negativeNoize;

            foreach (var elements in _data)
            {
                //if (elements.CH2 > _positiveNoize || elements.CH2 < _negativeNoize) _pdList.Add(elements);
                if (elements.CH2 > _positiveNoize && elements.CH1 > 0) _pdList.Add(elements);
                if (elements.CH2 < _negativeNoize && elements.CH1 < 0) _pdList.Add(elements);
            }
        }

        public float GetPozitiveNoizeLevel()
        {
            return _positiveNoize;
        }
        public float GetNegativeNoizeLevel()
        {
            return _negativeNoize;
        }
        public List<ParsedData> GetPartialDischargeList()
        {
            return _pdList;
        }
    }
}

using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialDischargeMeasurementApp.Analysis
{
    public class PDCalculator
    {
        //public  float AvgPDCurrent {  get; set; }
        //public float AvgPDPower { get; set; }
        //public float AvgPDEnergy {  get; set; }
        //public int FrequencyPD {  get; set; }
        //public float VoltageFirstPD { get; set; }
        private const float halfPerionTime = 0.01F;
        private float _pdEnergy;
        private float _pdCurrent;
        private float _pdPower;
        private float _pdCoefficient;
        private float _medianPD;
        private List<float> _averagePDList;
        private List<float> _averagePositivePDList;
        private List<float> _averageNegativePDList;
        private List<float> _medianPositivePDList;
        private List<float> _medianNegativePDList;
        private List<ParsedData> _firstPDCollections;
        private List<PDHalfPeriodData> _data;
        private List<ParsedData> _halfPeriodPDList;
        private float calcPDEquivalentCapacity(float voltage, float amplitude)
        {
            return 3f;
        }
        public PDCalculator(List<PDHalfPeriodData> data) : this (data, 1f) { }
        public PDCalculator(List<PDHalfPeriodData> data, float coefficient)
        {
            _data = data;
            _pdCoefficient = coefficient;

            var firstPDCollection = new List<ParsedData>();
            var avgPositivePD = 0f;
            var avgNegativePD = 0f;
            var avgHalfPeriodsPD = new List<float>();

            foreach (var pd in _data)
            {
                    firstPDCollection.Add(pd.PDList[0]);
            }

            _firstPDCollections = firstPDCollection;

            List<float> averagePDList = _data.Select(pd => pd.PDList.Average(parsedData => Math.Abs(parsedData.CH2))).ToList();
            _averagePDList = averagePDList;

            List<float> averagePositivePDList = _data
                .Where(pd => pd.IsPositiveHalfPeriod)
                .Select(pd => pd.PDList.Average(parsedData => parsedData.CH2))
                .ToList();
            _averagePositivePDList = averagePositivePDList;

            List<float> averageNegativePDList = _data
                .Where(pd => !pd.IsPositiveHalfPeriod)
                .Select(pd => pd.PDList.Average(parsedData => parsedData.CH2))
                .ToList();
            _averageNegativePDList = averageNegativePDList;

            List<float> medianPositivePDList = _data
                .Where (pd => pd.IsPositiveHalfPeriod)
                .Select (pd => pd.PDList.OrderBy(parsedData => parsedData.CH2).Select(parsedData => parsedData.CH2).Median())
                .ToList();
            _medianPositivePDList = medianPositivePDList;

            List<float> medianNegativePDList = _data
                .Where (pd =>  !pd.IsPositiveHalfPeriod)
                .Select (pd => pd.PDList.OrderBy(parseData => parseData.CH2).Select(parseData => parseData.CH2).Median())
                .ToList();
            _medianNegativePDList = medianNegativePDList;

            _medianPD = ((Math.Abs(_medianPositivePDList.Median())) + (Math.Abs(_medianPositivePDList.Median()))) / 2;

            // need to do calculations

            List<float> equivalentCapacity = _data.SelectMany(pd => pd.PDList)
            .Select(parsedData => Math.Abs(parsedData.CH2) / Math.Abs(parsedData.CH1))
            .ToList();


        }
        public List<ParsedData> GetFirstPDCollection()
        {
            return _firstPDCollections;
        }
        public List<float> GetAveragePDCollection()
        {
            return _averagePDList;
        }
        public List<float> GetAverageNegativePDCollection()
        {
            return _averageNegativePDList;
        }
        public List<float> GetAveragePositivePDCollection()
        {
            return _averagePositivePDList;
        }
        public List<float> GetMedianPositivePDCollection()
        {
            return _medianPositivePDList;
        }
        public List<float> GetMedianNegativePDCollection()
        {
            return _medianNegativePDList;
        }
        public float GetMedian()
        {
            return _medianPD;
        }
        public float GetPDCoefficient()
        {
            return _pdCoefficient;
        }
    }
}

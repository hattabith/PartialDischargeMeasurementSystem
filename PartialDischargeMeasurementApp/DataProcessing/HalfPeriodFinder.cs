using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialDischargeMeasurementApp.DataProcessing
{
    public class HalfPeriodFinder
    {
        private List<int> _zeroPoints;
        private List<int> _rezultWave = new List<int>();
        private float _averageWaveLength;

        public HalfPeriodFinder(List<int> zeroPoints, int rawDataLength)
        {
            _zeroPoints = zeroPoints;

            if (zeroPoints.Count > rawDataLength)
            {
                throw new Exception("List zeroPoints count must be less then rawDataLength. Class HalfPeriodFinder. ");
            }

            var average = getAverageWaveLength(_zeroPoints);

            _averageWaveLength = average;
            _rezultWave = _zeroPoints;
            if (_rezultWave[0] >= average * 0.8) _rezultWave.Insert(0, 0);
            if (_rezultWave[_rezultWave.Count - 1] - _rezultWave[_rezultWave.Count - 2] >= average * 0.8) _rezultWave.Add(rawDataLength - 1);
        }
        public float GetAverageWaveLength()
        {
            return _averageWaveLength;
        }
        public List<int> GetRezultHalfPeriodWavePoints()
        {
            return _rezultWave;
        }
        public List<int> GetSimpleRezultHalfPeriodWavePoints()
        {
            return _zeroPoints;
        }
        private float getAverageWaveLength(List<int> points)
        {

            return points[1] - points[0];
            //List<float> distance = new List<float>();
            //float rezult = 0f;

            //for (int i = 1; i < points.Count; i++)
            //{
            //    distance.Add((points[i] - points[i - 1]));
            //}
            //foreach (var item in distance)
            //{
            //    rezult += item;
            //}
            //rezult += rezult / distance.Count - 1;

            //return rezult;
        }

    }
}

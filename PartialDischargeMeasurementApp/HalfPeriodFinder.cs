using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialDischargeMeasurementApp
{
    public class HalfPeriodFinder
    {
        private List<int> _zeroPoints;
        private List<int> _rezultWave = new List<int>();
        private float _avarageWaveLength;

        public HalfPeriodFinder(List<int> zeroPoints, int rawDataLength)
        {
            _zeroPoints = zeroPoints;

            var avarage = getAvarageWaveLength(_zeroPoints);

            _avarageWaveLength = avarage;
            _rezultWave = _zeroPoints;
            if (_rezultWave[0] >= avarage * 0.8) _rezultWave.Insert(0, 0);
            if (_rezultWave[_rezultWave.Count - 1] >= avarage * 0.8) _rezultWave.Add(rawDataLength - 1);
        }
        public float GetAvarageWaveLength()
        {
            return _avarageWaveLength;
        }
        public List<int> GetRezultHalfPeriodWavePoints()
        {
            return _rezultWave;
        }
        public List<int> GetSimpleRezultHalfPeriodWavePoints()
        {
            return _zeroPoints;
        }
        private float getAvarageWaveLength(List<int> points)
        {
            List<float> distance = new List<float>();
            float rezult = 0f;

            for (int i = 1; i < points.Count; i++)
            {
                distance.Add((points[i - 1] + points[i]) / 2);
            }
            foreach (var item in distance)
            {
                rezult += item;
            }
            rezult += rezult / distance.Count - 1;

            return rezult;
        }

    }
}

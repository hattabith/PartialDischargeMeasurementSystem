using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialDischargeMeasurementApp
{
    public class SaveRezultToCSV
    {
        private string _filename;
        private List<ParsedData> _rawData;
        public SaveRezultToCSV(string filename, List<ParsedData> rawData)
        {
            _filename = filename;
            _rawData = rawData;
            var partialDischarge = new PDIdentifier(_rawData);

            using (StreamWriter sw = new StreamWriter(_filename))
            {
                sw.WriteLine("Data from oscilloscope: ");
                sw.WriteLine("Id,CH1,CH2");
                foreach (var item in _rawData)
                {
                    sw.WriteLine(item.Id.ToString() + "," + item.CH1.ToString() + "," + item.CH2.ToString());
                }
                sw.WriteLine();
                sw.WriteLine("Partial discharge points: ");
                sw.WriteLine("Id,CH1,CH2");
                foreach (var pd in partialDischarge.GetPartialDischargeList())
                {
                    sw.WriteLine(pd.Id.ToString() + "," + pd.CH1.ToString() + "," + pd.CH2.ToString());
                }

            }
        }
    }
}

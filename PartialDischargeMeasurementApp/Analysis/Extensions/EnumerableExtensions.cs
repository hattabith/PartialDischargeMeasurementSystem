namespace PartialDischargeMeasurementApp.Analysis.Extensions
{
    public static class EnumerableExtensions
    {
        public static float Median(this IEnumerable<float> source)
        {
            List<float> sortedList = source.OrderBy(x => x).ToList();
            int count = sortedList.Count;

            if (count == 0)
            {
                throw new InvalidOperationException("The source sequence is empty. Class EnumerableExtension.");
            }

            if (count % 2 == 0)
            {
                int mid = count / 2;
                return (sortedList[mid - 1] + sortedList[mid]) / 2.0f;
            }
            else
            {
                return sortedList[count / 2];
            }
        }
    }
}

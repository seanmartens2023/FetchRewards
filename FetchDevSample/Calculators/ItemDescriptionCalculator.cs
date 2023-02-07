using FetchDevSample.Models;

namespace FetchDevSample.Calculators
{
    public class ItemDescriptionCalculator : IPointsCalculator
    {
        public long CalculatePoints(Receipt receipt)
        {
            long points = 0L;

            //If the trimmed length of the item description is a multiple of 3, multiply the
            //price by 0.2 and round up to the nearest integer.The result is the number of points earned.
            receipt.Items.ForEach(item => {
                int len = item.ShortDescription.Trim().Length;
                if (len % 3 == 0)
                {
                    //Cast to (int) cents to avoid floating point errors.
                    int cents = (int) (decimal.Parse(item.Price) * 100);
                    decimal itemPoints = cents * 0.2M;
                    points += (long) Math.Ceiling(itemPoints / 100.0M);
                }
            });

            return points;
        }
    }
}

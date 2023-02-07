using FetchDevSample.Models;

namespace FetchDevSample.Calculators
{
    public class DateCalculator : IPointsCalculator
    {
        public long CalculatePoints(Receipt receipt)
        {
            long points = 0;

            //6 points if the day in the purchase date is odd.
            if (receipt.PurchaseDate.Day % 2 == 1)
            {
                points += 6;
            }

            //10 points if the time of purchase is after 2:00pm and before 4:00pm.
            int militaryTime = int.Parse(receipt.PurchaseTime.Replace(":", ""));
            if (militaryTime > 1400 && militaryTime < 1600)
            {
                points += 10;
            }

            return points;
        }
    }
}

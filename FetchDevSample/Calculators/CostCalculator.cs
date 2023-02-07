using FetchDevSample.Models;

namespace FetchDevSample.Calculators
{
    public class CostCalculator : IPointsCalculator
    {
        public long CalculatePoints(Receipt receipt)
        {
            long points = 0L;

            int cents = int.Parse(receipt.Total[^2].ToString());
            //50 points if the total is a round dollar amount with no cents.
            if (cents == 0)
            {
                points += 50;
            }

            //25 points if the total is a multiple of 0.25
            if (cents % 25 == 0)
            {
                points += 25;
            }

            return points;
        }
    }
}

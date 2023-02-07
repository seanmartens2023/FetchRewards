using FetchDevSample.Models;

namespace FetchDevSample.Calculators
{
    public class RetailerNameCalculator : IPointsCalculator
    {
        public long CalculatePoints(Receipt receipt)
        {
            //One point for every alphanumeric character in the retailer name.
            return receipt.Retailer.Sum(l => Char.IsLetterOrDigit(l) ? 1 : 0);
        }
    }
}

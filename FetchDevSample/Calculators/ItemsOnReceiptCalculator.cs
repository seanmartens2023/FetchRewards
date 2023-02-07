using FetchDevSample.Models;

namespace FetchDevSample.Calculators
{
    public class ItemsOnReceiptCalculator : IPointsCalculator
    {
        public long CalculatePoints(Receipt receipt)
        {
            //5 points for every two items on the receipt
            return (receipt.Items.Count / 2) * 5;
        }
    }
}

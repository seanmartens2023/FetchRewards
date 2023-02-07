using FetchDevSample.Models;

namespace FetchDevSample.Calculators
{
    public interface IPointsCalculator
    {
        public long CalculatePoints(Receipt receipt);
    }
}

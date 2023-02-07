using FetchDevSample.Models;

namespace FetchDevSample.Storage
{
    public interface IStorage
    {
        public long AddPoints(Guid retailerId, long score);
        public long GetPoints(Guid retailerId);
        public Guid GetOrAddRetailId(string retailerName);
        public void StoreReceipt(Receipt receipt);
        public void StoreErrorReceipt(Receipt receipt);
    }
}

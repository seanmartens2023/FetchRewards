/*
 * All of these concepts can be implemented by Redis Lists, SortedSets or Hashes in another service.
 */
using FetchDevSample.Models;

namespace FetchDevSample.Storage
{
    public class RedisStorage : IStorage
    {
        public long AddPoints(Guid retailerId, long score)
        {
            throw new NotImplementedException();
        }

        public Guid GetOrAddRetailId(string retailerName)
        {
            throw new NotImplementedException();
        }

        public long GetPoints(Guid retailerId)
        {
            throw new NotImplementedException();
        }

        public void StoreErrorReceipt(Receipt receipt)
        {
            throw new NotImplementedException();
        }

        public void StoreReceipt(Receipt receipt)
        {
            throw new NotImplementedException();
        }
    }
}

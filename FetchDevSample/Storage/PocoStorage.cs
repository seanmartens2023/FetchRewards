using FetchDevSample.Models;
using System.Collections.Concurrent;

namespace FetchDevSample.Storage
{
    public class PocoStorage : IStorage
    {
        private ConcurrentDictionary<string, Guid> retailerToId;
        private ConcurrentDictionary<Guid, long> idToScore;
        private ConcurrentQueue<Receipt> retailerReceipts;
        private ConcurrentQueue<Receipt> retailerErrorReceipts;

        public PocoStorage()
        {
            retailerToId = new ConcurrentDictionary<string, Guid>(1, 8);
            idToScore = new ConcurrentDictionary<Guid, long>(1, 8);
            retailerReceipts = new ConcurrentQueue<Receipt>();
            retailerErrorReceipts = new ConcurrentQueue<Receipt>();
        }
        public long AddPoints(Guid retailerId, long score)
        {
            if (idToScore.TryAdd(retailerId, score))
            {
                return score;
            }
            else
            {
                idToScore[retailerId] += score;
                return idToScore[retailerId];
            }
        }

        public long GetPoints(Guid retailerId)
        {
            if(idToScore.TryGetValue(retailerId, out long score))
            {
                return score;
            }
            throw new InvalidOperationException($"Retailer ID {retailerId} does not exist");
        }

        public Guid GetOrAddRetailId(string retailerName)
        {
            if (retailerToId.TryGetValue(retailerName, out Guid retailerId))
            {
                return retailerId;
            }

            //Retailer has not been assigned an ID, add one for them
            retailerId = Guid.NewGuid();
            if(retailerToId.TryAdd(retailerName, retailerId))
            {
                return retailerId;
            }
            else
            {
                //Possible race condition averted. A thread added the ID since we looked up while it was missing
                return retailerToId[retailerName];
            }

        }

        public void StoreReceipt(Receipt receipt)
        {
            retailerReceipts.Enqueue(receipt);
            
        }

        public void StoreErrorReceipt(Receipt receipt)
        {
            try
            {
                retailerErrorReceipts.Enqueue(receipt);
            }
            catch (Exception)
            {
                //Supressed to not be sent back to HTTP client. 
                return;
            }
        }
    }
}

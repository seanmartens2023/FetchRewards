using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FetchDevSample.Models;
using FetchDevSample.Storage;

namespace FetchDevSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private readonly IStorage db = StorageConfig.Instance;

        [HttpPost]
        [Route("/[controller]/process")]
        public ActionResult<string> SendReceipt(Receipt receipt)
        {
            if(receipt is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                Guid retailerId = db.GetOrAddRetailId(receipt.Retailer);
                db.AddPoints(retailerId, receipt.CalculatePoints());
                db.StoreReceipt(receipt);
                return JsonConvert.SerializeObject(new ProcessedResponse { Id = retailerId });
            }catch(Exception)
            {
                db.StoreErrorReceipt(receipt);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/[controller]/{id}/points")]
        public ActionResult<string> GetPoints(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            try
            {
                if (!Guid.TryParse(id, out var retailerId))
                {
                    return BadRequest();
                }
                long points = db.GetPoints(retailerId);
                return JsonConvert.SerializeObject(new GetPointsResponse { Points = points });
            }
            catch (InvalidOperationException)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No receipt found for that id");
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}

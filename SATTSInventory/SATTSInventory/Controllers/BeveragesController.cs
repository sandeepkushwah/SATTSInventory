using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using SATTSInventory.Models;
using SATTSInventory.Repository;

namespace SATTSInventory.Controllers
{
    [EnableCors("*","*","*")]
    public class BeveragesController : ApiController
    {
        private readonly IBeverageRepository _repository;
        public BeveragesController()
        {
            _repository = new BeverageRepository();
        }
        public BeveragesController(IBeverageRepository repository)
        {

            if (repository == null)
            {
                _repository = new BeverageRepository();
            }
            else
            {
                _repository = repository;
            }
        }
        //private readonly 
        // GET: api/Beverages
        public async Task<IHttpActionResult> GetBeverages()
        {
            IEnumerable<Beverage> beverages = await _repository.GetBeverages();
            return Ok(beverages);
        }

        // GET: api/Beverages/5
        [ResponseType(typeof(Beverage))]
        public async Task<IHttpActionResult> GetBeverage(int id)
        {
            IEnumerable<Beverage> beverages = await _repository.GetBeverages();
            Beverage beverage = beverages.FirstOrDefault(bev => bev.Id == id);
            if (beverage == null)
            {
                return NotFound();
            }

            return Ok(beverage);
        }

        // PUT: api/Beverages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBeverage(int id, Beverage beverage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            bool isUpdated = await _repository.UpdateBeverage(beverage);
            if (!isUpdated)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Beverages
        [ResponseType(typeof(Beverage))]
        public async Task<IHttpActionResult> PostBeverage(Beverage beverage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int? newBeverageID = await _repository.AddBeverage(beverage);

            if (newBeverageID.HasValue)
            {
                beverage.Id = newBeverageID.Value;
                return CreatedAtRoute("DefaultApi", new { id = beverage.Id }, beverage);
            }
            else
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Beverages/5
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> DeleteBeverage(int id)
        {
            bool isDeleted = await _repository.DeleteBeverage(id);
            if (isDeleted)
            {
                return Ok(isDeleted);
            }
            else
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
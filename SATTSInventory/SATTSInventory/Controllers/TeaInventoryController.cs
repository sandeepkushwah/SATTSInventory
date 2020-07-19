using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SATTSInventory.Data;
using SATTSInventory.Models;

namespace SATTSInventory.Controllers
{
    public class TeaInventoryController : ApiController
    {
        private TeaInventoryDBContext db = new TeaInventoryDBContext();

        // GET: api/TeaInventory
        public IQueryable<Beverage> GetBeverages()
        {
            return db.Beverages;
        }

        // GET: api/TeaInventory/5
        [ResponseType(typeof(Beverage))]
        public async Task<IHttpActionResult> GetBeverage(int id)
        {
            Beverage beverage = await db.Beverages.FindAsync(id);
            if (beverage == null)
            {
                return NotFound();
            }

            return Ok(beverage);
        }

        // PUT: api/TeaInventory/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBeverage(int id, Beverage beverage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != beverage.Id)
            {
                return BadRequest();
            }

            db.Entry(beverage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeverageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TeaInventory
        [ResponseType(typeof(Beverage))]
        public async Task<IHttpActionResult> PostBeverage(Beverage beverage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Beverages.Add(beverage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = beverage.Id }, beverage);
        }

        // DELETE: api/TeaInventory/5
        [ResponseType(typeof(Beverage))]
        public async Task<IHttpActionResult> DeleteBeverage(int id)
        {
            Beverage beverage = await db.Beverages.FindAsync(id);
            if (beverage == null)
            {
                return NotFound();
            }

            db.Beverages.Remove(beverage);
            await db.SaveChangesAsync();

            return Ok(beverage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BeverageExists(int id)
        {
            return db.Beverages.Count(e => e.Id == id) > 0;
        }
    }
}
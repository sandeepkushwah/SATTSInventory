using SATTSInventory.Data;
using SATTSInventory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SATTSInventory.Repository
{
    public class BeverageRepository : IBeverageRepository
    {
        private TeaInventoryDBContext db = new TeaInventoryDBContext();
        public async Task<int?> AddBeverage(Beverage beverage)
        {
            db.Beverages.Add(beverage);
            int addResult = await db.SaveChangesAsync();
            return addResult > 0 ? beverage.Id : new int?();
        }

        public async Task<bool> DeleteBeverage(int id)
        {
            Beverage beverage = await db.Beverages.FindAsync(id);
            if (beverage != null)
            {
                db.Beverages.Remove(beverage);
                int deleteResult = await db.SaveChangesAsync();
                return deleteResult > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Beverage>> GetBeverages()
        {
            return await Task.Run(() => { return db.Beverages; });
        }
        public async Task<bool> UpdateBeverage(Beverage beverage)
        {

            if (!BeverageExists(beverage.Id))
            {
                return false;
            }
            db.Entry(beverage).State = EntityState.Modified;
            int updateResult = await db.SaveChangesAsync();
            return updateResult > 0;
        }

        private bool BeverageExists(int id)
        {
            return db.Beverages.Count(e => e.Id == id) > 0;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
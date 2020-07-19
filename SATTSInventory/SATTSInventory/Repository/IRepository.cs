using SATTSInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SATTSInventory
{
    public interface IBeverageRepository : IDisposable
    {
        Task<IEnumerable<Beverage>> GetBeverages();
        Task<int?> AddBeverage(Beverage beverage);

        Task<bool> UpdateBeverage(Beverage beverage);
        Task<bool> DeleteBeverage(int id);
    }
}
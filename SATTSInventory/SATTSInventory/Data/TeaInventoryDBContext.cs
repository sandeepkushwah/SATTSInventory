using SATTSInventory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SATTSInventory.Data
{
    public class TeaInventoryDBContext : DbContext
    {
        public TeaInventoryDBContext() :
          base("TeaInvDevConn")
        {
        }
        public static TeaInventoryDBContext Create()
        {
            return new TeaInventoryDBContext();
        }
        public DbSet<Beverage> Beverages { get; set; }
    }
}
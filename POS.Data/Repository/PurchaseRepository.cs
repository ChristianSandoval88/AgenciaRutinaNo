using POS.Data.Repository.IRepository;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Repository
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        private readonly ApplicationDBContext _db;

        public PurchaseRepository(ApplicationDBContext db):base(db)
        {
            _db = db;
        }
    }
}

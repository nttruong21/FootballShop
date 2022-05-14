using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballShopModel.DAO
{
    public class BillDetailDAO
    {
        private FootballEntities db;
        public BillDetailDAO()
        {
            db = new FootballEntities();
            db.Configuration.ProxyCreationEnabled = false;

            db.Configuration.LazyLoadingEnabled = true;

        }


        public bool CreateBillDetail(BillDetail billdetail)
        {
            try
            {
                db.BillDetails.Add(billdetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<BillDetail> getAllbyId(int id)
        {
            return db.BillDetails.Where(p => p.billId == id).ToList();
        }
    }
}

using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballShopModel.DAO
{
    public class BillDAO
    {
        private FootballEntities db;
        public BillDAO()
        {
            db = new FootballEntities();
            db.Configuration.ProxyCreationEnabled = false;

            db.Configuration.LazyLoadingEnabled = true;

        }


        public Bill CreateBill(Bill bill)
        {
           
            bill.createdAt = DateTime.Now;
            bill.status = 0;
            var t = db.Bills.Add(bill);
            db.SaveChanges();
            return t;
            
           
        }
    }
}

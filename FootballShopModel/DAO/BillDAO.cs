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
        private FootballShopEntities db;
        public BillDAO()
        {
            db = new FootballShopEntities();
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = true;
        }

        // GET ALL
        public List<Bill> getAllBills()
        {
            return db.Bills.ToList();
        }

        // GET ALL WAITING APPROVE BILL (HÓA ĐƠN ĐANG CHỜ DUYỆT)
        public List<Bill> getWaitingApproveBills()
        {
            return db.Bills.Where(x => x.status == 0).ToList();
        }

        // GET ALL CANCELED BILL (HÓA ĐƠN ĐÃ HỦY)
        public List<Bill> getCanceledBills()
        {
            return db.Bills.Where(x => x.status == 1).ToList();
        }

        // GET ALL SHIPPING BILL (HÓA ĐƠN ĐANG VẬN CHUYỂN)
        public List<Bill> getShippingBills()
        {
            return db.Bills.Where(x => x.status == 2).ToList();
        }

        // GET ALL DELIVERED BILL (HÓA ĐƠN ĐÃ GIAO)
        public List<Bill> getDeliveredBills()
        {
            return db.Bills.Where(x => x.status == 3).ToList();
        }

        // GET ONE BY ID 
        public Bill getBillById(int id)
        {
            return db.Bills.FirstOrDefault(x => x.id == id);
        }

        // CREATE BILL
        public Bill CreateBill(Bill bill)
        {
           
            bill.createdAt = DateTime.Now;
            bill.status = 0;
            var t = db.Bills.Add(bill);
            db.SaveChanges();
            return t;
        }

        // UPDATE BILL STATUS
        public bool UpdateBillStatus(Bill bill, int status)
        {
            try
            {
                bill.status = status;
                db.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

        public bool updateStatus(int id)
        {
            try
            {
                var t = db.Bills.Single(b => b.id == id);
                t.status = 3;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

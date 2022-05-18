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
            }
            catch
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

        // TỔNG DOANH THU
        public double getTotalRevenue()
        {
            var total = db.Bills.Where(x => x.status == 3).Sum(x => x.totalPrice);
            if(total == null)
            {
                return 0;
            }
            return (double) total;
        }

        // SỐ LƯỢNG SẢN PHẨM ĐÃ BÁN
        //public int getSoldProducts()
        //{
        //    return (from bill in db.Bills
        //            join billDetail in db.BillDetails on bill.id equals billDetail.billId
        //            where bill.status == 3
        //            group quantity by billDetail.quantity into g
        //            select new { total = g.sum(x => x.quantity) }).FirstOrDefault();
        //}

        // SỐ LƯỢNG ĐƠN HÀNG ĐANG CHỜ DUYỆT
        public int getNumberOfWaitingApproveBill()
        {
            return (int) db.Bills.Count(x => x.status == 0);
        }

        // SỐ LƯỢNG ĐƠN HÀNG ĐÃ HỦY
        public int getNumberOfCanceledBill()
        {
            return db.Bills.Count(x => x.status == 1);
        }

        // SỐ LƯỢNG ĐƠN HÀNG ĐANG VẬN CHUYỂN
        public int getNumberOfShippingBill()
        {
            return db.Bills.Count(x => x.status == 2);
        }

        // SỐ LƯỢNG ĐƠN HÀNG ĐÃ GIAO
        public int getNumberOfDeliveredBill()
        {
            return db.Bills.Count(x => x.status == 3);
        }
    }
}

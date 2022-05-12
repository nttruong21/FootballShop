using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballShopModel.DAO
{
    public class CartDAO
    {
        private FootballEntities db;
        public CartDAO()
        {     
            db = new FootballEntities();
            db.Configuration.ProxyCreationEnabled = false;

            db.Configuration.LazyLoadingEnabled = true;

        }

        public List<CartDetail> getAll()
        {

            return db.CartDetails.ToList();
        }

        // GET ONE BY ID
        public CartDetail getById(int id)
        {
            return db.CartDetails.FirstOrDefault(x => x.id == id);
        }

        public bool CreateProduct(CartDetail cart)
        {
            try
            {
                db.CartDetails.Add(cart);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // GET COUNT
        public int GetCount()
        {
            return db.CartDetails.ToList().Count();
        }

        // GET TOTAL PRICE
        public long getTotalPrice()
        {
            return db.CartDetails.Sum(x => x.price) != null ? (long)db.CartDetails.Sum(x => x.price) : 0;
        }

        // REMOVE
        public bool removeItem(CartDetail item)
        {
            try
            {
                db.CartDetails.Remove(item);
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

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
        private FootballShopEntities db;
        public CartDAO()
        {     
            db = new FootballShopEntities();
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
                int i = Convert.ToInt32(cart.productId);
                if (checkProductId(i))
                {
                    UpdateQuantityCart(i);
                }
                else
                {
                    db.CartDetails.Add(cart);
                    db.SaveChanges();
                }
                
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
        public bool removeItem(int id)
        {
            try
            {
                CartDetail item = getById(id);
                db.CartDetails.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool checkProductId(int id)
        {
            var a = db.CartDetails.Where(p => p.productId == id).Count();
            return a >= 1 ? true : false ;
        }
        public bool UpdateQuantityCart(int id)
        {
            try
            {
                CartDetail c = db.CartDetails.Single(ca => ca.productId == id);
                c.quantity = c.quantity + 1;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateQuantityCartDown(int id)
        {
            try
            {
                CartDetail c = db.CartDetails.Single(ca => ca.productId == id);
                c.quantity = c.quantity - 1;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public CartDetail getbyProductId(int productId)
        {
            CartDetail c = db.CartDetails.Single(ca => ca.productId == productId);
            return c;
        }
    }
}

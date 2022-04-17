using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballShopModel.DAO
{
    public class ProductDAO
    {
        private FootballShopEntities db;
        public ProductDAO()
        {
            db = new FootballShopEntities();
        }

        // Get all products
        public List<Product> getAllProducts()
        {
            return db.Products.ToList();
        }

        // Get one account by id
        public Product getProductById(int id)
        {
            return db.Products.FirstOrDefault(pro => pro.id == id);
        }

        // Create 
        public bool CreateProduct(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update
        public bool UpdateProduct(Product product)
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete
        public bool DeleteProduct(Product product)
        {
            try
            {
                db.Products.Remove(product);
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

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

        // Get all discount products
        public List<Product> getAllDiscountProducts()
        {
            return db.Products.OrderByDescending(x => x.discount).Where(x => x.discount > 0).ToList();
        }

        // Lấy 10 sản phẩm được bán nhiều nhất 
        public List<Product> getTenMostSoldProducts()
        {
            return db.Products.OrderByDescending(x => x.soldQuantity).ToList();
        }

        // Get all products by category id
        public List<Product> getAllProductsByCategoryId(int id)
        {
            return db.Products.Where(x => x.categoryId == id).ToList();
        }

        // Get all products by category slug
        public List<Product> getAllProductsByCategorySlug(string slug)
        {
            var category = db.Categories.Single(x => x.slug.Equals(slug));
            return db.Products.Where(x => x.categoryId == category.id).ToList();
        }

        // Get one product by slug
        public Product getProductBySlug(string slug)
        {
            return db.Products.Single(x => x.slug.Equals(slug));
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

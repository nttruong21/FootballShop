using FootballShopModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballShopModel.DAO
{
    public class CategoryDAO
    {
        private FootballShopEntities db;
        public CategoryDAO()
        {
            db = new FootballShopEntities();
        }

        // Get all categories
        public List<Category> getAllCategories()
        {
            return db.Categories.ToList();
        }

        // Get one category by id
        public Category getCategoryById(int id)
        {
            return db.Categories.FirstOrDefault(cat => cat.id == id);
        }

        // Get one category by slug
        public Category getCategoryBySlug(string slug)
        {
            return db.Categories.FirstOrDefault(cat => cat.slug.Equals(slug));
        }

        // Create 
        public bool CreateCategory(Category cat)
        {
            try
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update
        public bool UpdateCategory(Category cat)
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
        public bool DeleteCategory(Category cat)
        {
            try
            {
                db.Categories.Remove(cat);
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

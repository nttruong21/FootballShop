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
        private FootballEntities db;
        public CategoryDAO()
        {
            db = new FootballEntities();
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

        // Get one category by name
        public Category getCategoryByName(string name)
        {
            return db.Categories.FirstOrDefault(cat => cat.name.Equals(name));
        }

        // Get max id
        public int getMaxId()
        {
            var obj = db.Categories.ToList();
            if(obj.Count > 0)
            {
                return obj.Last().id;
            } else
            {
                return 0;
            }

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

        // Remove
        public bool RemoveCategory(Category cat)
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

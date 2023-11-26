using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models;
using ShopOnline.Models.DBObjects;

using System;

namespace ShopOnline.Repository
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<CategoryModel> GetAllCategories()
        {
          
                List<CategoryModel> categoryList = new List<CategoryModel>();
                foreach (Category dbcat in this.dbContext.Categories)
                {
                     categoryList.Add(MapDbObjectToModel(dbcat));
                }
                return categoryList;
  
        }
        public List<Product> GetProductsByCategory(Guid categoryId)
        {
            return dbContext.Products
                .Where(p => p.IdCategory == categoryId)
                .ToList();
        }
        public CategoryModel GetCategoryById(Guid id)
        {
            return MapDbObjectToModel(dbContext.Categories.FirstOrDefault(x => x.IdCategory == id));
        }

        public void InsertCategory(CategoryModel categoryModel)
        {
            categoryModel.IdCategory=Guid.NewGuid();
            dbContext.Categories.Add(MapModelToDbObject(categoryModel));
            dbContext.SaveChanges();
        }

        public void UpdateCategory(CategoryModel categoryModel)
        {
            Category category = dbContext.Categories.FirstOrDefault(x => x.IdCategory == categoryModel.IdCategory);
            if (category != null)
            {
                category.IdCategory=categoryModel.IdCategory;
                category.Name=categoryModel.Name;
               
            } 
            dbContext.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            var existingCategory = dbContext.Categories.FirstOrDefault(c => c.IdCategory== id);
            if (existingCategory != null)
            {
                dbContext.Categories.Remove(existingCategory);
                
            }
            dbContext.SaveChanges();
        }

        private CategoryModel MapDbObjectToModel(Category dbCategory)
        {
            CategoryModel categorymodel = new CategoryModel();
           if(dbCategory !=null) {
                categorymodel.IdCategory = dbCategory.IdCategory;
                categorymodel.Name = dbCategory.Name;
            }
           return categorymodel;
        }

        private Category MapModelToDbObject(CategoryModel categoryModel)
        {
      
            Category dbcategory=new Category();
            if(categoryModel != null)
            {
                dbcategory.IdCategory = categoryModel.IdCategory;
                dbcategory.Name = categoryModel.Name;
            }
            return dbcategory;
        }


    }
}


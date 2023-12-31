﻿using ShopOnline.Data;
using System;
using ShopOnline.Models.DBObjects;
using ShopOnline.Models;

namespace ShopOnline.Repository
{
    public class ProductRepository
    {
        private ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        //private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public ProductRepository(ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            this.dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }
        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            //_hostingEnvironment = hostingEnvironment;
        }
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            foreach (Product dbProduct in dbContext.Products)
            {
                products.Add(MapDbObjectToModel(dbProduct));

            }
            return products;
        }

        public ProductModel GetProductById(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Products.FirstOrDefault(x => x.IdProduct == ID));
        }
        public Product GetProductById1(Guid id)
        {
            return dbContext.Products.FirstOrDefault(y => y.IdProduct == id);
        }
        public ProductModel GetProductByIDCategory(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Products.FirstOrDefault(x => x.IdCategory == ID));

        }
        public  Category GetCategoryById(Guid categoryId)
        {
            var categoryFromDb = dbContext.Categories.FirstOrDefault(c => c.IdCategory == categoryId);

            if (categoryFromDb != null)
            {
                var categoryModel = new Category
                {
                    IdCategory = categoryFromDb.IdCategory,
                    Name = categoryFromDb.Name,
                };

                return categoryModel;
            }

            return null; 
        }



        public void InsertProduct(ProductModel product)
        {

            product.IdProduct = Guid.NewGuid();
              dbContext.Products.Add(MapModelToDBObject(product));
           dbContext.SaveChanges();
        }

        public void UpdateProduct(ProductModel product)
        {
            Product existingproduct = dbContext.Products.FirstOrDefault(x => x.IdProduct == product.IdProduct);
            if (existingproduct != null)
            {
                existingproduct.IdProduct = product.IdProduct;
                existingproduct.Name = product.Name;
                existingproduct.Description = product.Description;
                existingproduct.IdCategory = product.IdCategory;
                existingproduct.Price = product.Price;
                existingproduct.StockQuantity = product.StockQuantity;
                existingproduct.Image= product.Image;
                dbContext.SaveChanges();
            }
        }
        public void UpdateProduct(ProductModel productModel, int quantity)
        {
            Product product = dbContext.Products.FirstOrDefault(x => x.IdProduct == productModel.IdProduct);
            if (product != null)
            {
                var finalQuantity = product.StockQuantity;
                finalQuantity = finalQuantity - quantity;

                product.StockQuantity = finalQuantity;

            }
            dbContext.SaveChanges();

        }

        public void DeleteProduct(Guid id,string Img)
        {
            Product existingproduct = dbContext.Products.FirstOrDefault(x => x.IdProduct == id);
            if (existingproduct != null)
            {
                dbContext.Products.Remove(existingproduct);
                dbContext.SaveChanges();
                DeleteImage(Img);
            }
        }

        private ProductModel MapDbObjectToModel(Product product)
        {
            ProductModel product1 = new ProductModel();

            if (dbContext != null)
            {
                product1.IdProduct = product.IdProduct;
                product1.Name = product.Name;
                product1.Description = product.Description;
                product1.IdCategory = product.IdCategory;
                product1.Price = product.Price;
                product1.StockQuantity = product.StockQuantity;
                product1.Image = product.Image;
            }
            return product1;
        }

        private Product MapModelToDBObject(ProductModel product)
        {
            Product product1 = new Product();

            if (dbContext != null)
            {
                product1.IdProduct = product.IdProduct;
                product1.Name = product.Name;
                product1.Description = product.Description;
                product1.IdCategory = product.IdCategory;
                product1.Price = product.Price;
                product1.StockQuantity = product.StockQuantity;
                product1.Image=product.Image;
            }
            return product1;
        }
        public void DeleteImage(string imageName)
        {
            string imagePath = Path.Combine("wwwroot", imageName.TrimStart('/'));
            try
            {
                    File.Delete(imagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la ștergerea imaginii {imageName}: {ex.Message}");
            }
        }
    }
}


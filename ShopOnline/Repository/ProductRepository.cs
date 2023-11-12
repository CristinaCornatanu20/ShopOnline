using ShopOnline.Data;
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
                    // Aici copiezi proprietățile corespunzătoare din categoria din baza de date în categoria model
                    IdCategory = categoryFromDb.IdCategory,
                    Name = categoryFromDb.Name,
                    // Copiază și celelalte proprietăți aici
                };

                return categoryModel;
            }

            return null; // sau orice altă logică specifică cazului tău
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

        public void DeleteProduct(Guid id)
        {
            Product existingproduct = dbContext.Products.FirstOrDefault(x => x.IdProduct == id);
            if (existingproduct != null)
            {
                dbContext.Products.Remove(existingproduct);
                dbContext.SaveChanges();
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
        public IFormFile GetFormFileFromPath(string filePath)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            var memoryStream = new MemoryStream();
            memoryStream.Write(fileBytes, 0, fileBytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            var formFile = new FormFile(memoryStream, 0, fileBytes.Length, "name", "fileName.ext");
            return formFile;
        }
    }
}


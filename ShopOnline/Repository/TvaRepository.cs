using ShopOnline.Data;
using ShopOnline.Models;
using ShopOnline.Models.DBObjects;

namespace ShopOnline.Repository
{
    public class TvaRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TvaRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<TvaModel> GetAllTVAs()
        {

            List<TvaModel> tvaList = new List<TvaModel>();
            foreach (Tva dbcat in this.dbContext.Tvas)
            {
                tvaList.Add(MapDbObjectToModel(dbcat));
            }
            return tvaList;

        }

        public Category GetCategoryById(Guid categoryId)
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

        public void InsertCategory(TvaModel tvaModel)
        {
            tvaModel.IdTva = Guid.NewGuid();
            dbContext.Tvas.Add(MapModelToDbObject(tvaModel));
            dbContext.SaveChanges();
        }

        public void UpdateCategory(TvaModel tvaModel)
        {
            Tva tva = dbContext.Tvas.FirstOrDefault(x => x.IdTva == tvaModel.IdTva);
            if (tva != null)
            {
                tva.IdTva = tvaModel.IdTva;
                tva.Tva1 = tvaModel.Tval;
                tva.IdCategory = tvaModel.IdCategory;

            }
            dbContext.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            var existingCategory = dbContext.Categories.FirstOrDefault(c => c.IdCategory == id);
            if (existingCategory != null)
            {
                dbContext.Categories.Remove(existingCategory);

            }
            dbContext.SaveChanges();
        }

        private TvaModel MapDbObjectToModel(Tva dbTva)
        {
            TvaModel tvamodel = new TvaModel();
            if (dbTva != null)
            {
                tvamodel.IdTva = dbTva.IdCategory;
                tvamodel.Tval = dbTva.Tva1;
                tvamodel.IdCategory = dbTva.IdCategory;
            }
            return tvamodel;
        }

        private Tva MapModelToDbObject(TvaModel tvamodel)
        {

            Tva dbTva = new Tva();
            if (tvamodel != null)
            {
                dbTva.IdTva = tvamodel.IdTva;
                dbTva.Tva1 = tvamodel.Tval;
                dbTva.IdCategory = tvamodel.IdCategory;
            }
            return dbTva;
        }

    }
}

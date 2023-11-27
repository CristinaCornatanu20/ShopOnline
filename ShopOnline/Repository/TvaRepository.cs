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
        public TvaModel GetTvaById(Guid id)
        {

            return MapDbObjectToModel(dbContext.Tvas.FirstOrDefault(x => x.IdTva == id));


        }
        public TvaModel GetTvaByCategoryId(Guid categoryId)
        {
            
            return MapDbObjectToModel(dbContext.Tvas.FirstOrDefault(x => x.IdCategory == categoryId));
        }
        public Category GetCategoryById(Guid categoryId)
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

        public void InsertTva(TvaModel tvaModel)
        {
            tvaModel.IdTva = Guid.NewGuid();
            dbContext.Tvas.Add(MapModelToDbObject(tvaModel));
            dbContext.SaveChanges();
        }

        public void UpdateTva(TvaModel tvaModel)
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

        public void DeleteTva(Guid id)
        {
            var existingTva = dbContext.Tvas.FirstOrDefault(c => c.IdTva == id);
            if (existingTva != null)
            {
                dbContext.Tvas.Remove(existingTva);

            }
            dbContext.SaveChanges();
        }

        private TvaModel MapDbObjectToModel(Tva dbTva)
        {
            TvaModel tvamodel = new TvaModel();
            if (dbTva != null)
            {
                tvamodel.IdTva = dbTva.IdTva;
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

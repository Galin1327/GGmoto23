namespace MotoExpo23_FP.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using MotoExpo23_FP.Data;
    using MotoExpo23_FP.Data.Models;

    public class MotorcyclesService : IMotorcyclesService
    {
        private readonly ApplicationDbContext db;

        public MotorcyclesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Motorcycle> GetAll()
        {
            return this.db.Motorcycles.ToList();
        }

        public Motorcycle GetDetails(int id)
        {
            return this.db.Motorcycles.Find(id);
        }

        public void Create(Motorcycle model)
        {
            this.db.Motorcycles.Add(model);
            this.db.SaveChanges();
        }

        public void Update(Motorcycle model)
        {
            var motorcycle = this.db.Motorcycles.Find(model.Id);

            motorcycle.Price = model.Price;
            motorcycle.PictureUrl = model.PictureUrl;
            motorcycle.EngineDisplacement = model.EngineDisplacement;
            motorcycle.Model = model.Model;

            this.db.Motorcycles.Update(motorcycle);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var motorcycle = this.db.Motorcycles.Find(id);
            this.db.Motorcycles.Remove(motorcycle);
            this.db.SaveChanges();
        }
    }
}

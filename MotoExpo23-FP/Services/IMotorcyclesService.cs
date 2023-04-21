namespace MotoExpo23_FP.Services
{
    using System.Collections.Generic;

    using MotoExpo23_FP.Data.Models;

    public interface IMotorcyclesService
    {
        List<Motorcycle> GetAll();

        Motorcycle GetDetails(int id);

        void Create(Motorcycle model);

        void Update(Motorcycle model);

        void Delete(int id);
    }
}

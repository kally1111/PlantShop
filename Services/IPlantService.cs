using PlantShop.Data;
using PlantShop.Models;
using PlantShop.Models.PlantVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Services
{
      public  interface IPlantService
    {
        
        GetPlantViewModel Get(GetPlantViewModel getPlantViewModel,
            int page);

        void Create(CreatePlantViewModel plant);
        InfoPlantViewModel InfoPlant(int id);

        void Change(DetailedInfoPlantViewModel plant);
        List<UpdateDataPVM> UpdateData(string searchString);
        DetailedInfoPlantViewModel DetailedInfo(int id);
        InfoPlantViewModel Delete(int id);
        void ConfermedDelete(int id);
        GetByShopPVM GetByShop(int id, int page);
    }
}

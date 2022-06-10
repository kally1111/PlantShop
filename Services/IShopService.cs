using PlantShop.Data;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Services
{
      public  interface IShopService
    {
        List<ShopViewModel> Index();
        void Create(ShopViewModel shop);
        ShopViewModel Details(int id);
        List<ShopViewModel> UpdateData(string search);
        ShopViewModel DetailedInfo(int id);
        void Change(ShopViewModel shop);
        ShopViewModel Delete(int id);
        void ConfermedDelete(int id);
        public ShopViewModel DeleteDenied(int id);


    }
}

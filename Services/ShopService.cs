using Microsoft.AspNetCore.Hosting;
using PlantShop.Data;
using PlantShop.DataAccess;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PlantShop.Services
{
    public class ShopService : IShopService
    {
        private readonly PlantShopDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ShopService(PlantShopDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }
        public List<ShopViewModel> Index()
        {
            return db.Shopes.ToList().Where(s => s.Id != 1).Select(s => MapShop(s)).ToList();

        }

        public void Create(ShopViewModel shop)
        {
            db.Add(MapShop(shop));
            db.SaveChanges();
        }

        public ShopViewModel Details(int id)
        {
            return MapShop(this.db.Shopes.FirstOrDefault(s => s.Id == id));
        }
        public List<ShopViewModel> UpdateData(string searchString)
        {

            var q = this.db.Shopes.ToList()
                .Where(s => s.ShopName.Contains(searchString)).Select(s => MapShop(s))
                .OrderBy(s => s.ShopId).ToList();
            return q;
        }
        public ShopViewModel DetailedInfo(int id)
        {

            return MapShop(this.db.Shopes
                .FirstOrDefault(pl => pl.Id == id)
                );
        }
        public void Change(ShopViewModel shop)
        {
            if (shop.Photo != null && shop.PhotoPath != null)
            {
                DeleteFile(shop.PhotoPath);
            }
            string uniqueFileName = UploadedFile(shop);
            var shopToChange = db.Shopes.FirstOrDefault(x => x.Id == shop.ShopId);
            shopToChange.ShopName = shop.ShopName;
            shopToChange.City = shop.City;
            shopToChange.Address = shop.Address;
            shopToChange.PhotoPath = uniqueFileName;
            shopToChange.PhoneNumber = shop.PhoneNumber;
            shopToChange.Email = shop.Email;

            db.SaveChanges();
        }
        public ShopViewModel Delete(int id)
        {
          
            return MapShop(this.db.Shopes.Include(s => s.Plants).Include(s => s.Employees)
                .FirstOrDefault(s => s.Id == id));
        }
        public void ConfermedDelete(int id)
        { 
                var shopToDelete = this.db.Shopes
                    .FirstOrDefault(p => p.Id == id);
                db.Remove(shopToDelete);
                db.SaveChanges();
        }
        public ShopViewModel DeleteDenied(int id)
        {

            return MapShop(this.db.Shopes
                .FirstOrDefault(s => s.Id == id));
        }
        private ShopViewModel MapShop(Shop shop)
        {
            if (shop == null)
            {
                return new ShopViewModel();
            }
            return new ShopViewModel
            {
                ShopId=shop.Id,
                ShopName = shop.ShopName,
                City = shop.City,
                Address = shop.Address,
                PhoneNumber = shop.PhoneNumber,
                PhotoPath = shop.PhotoPath

            };
        }
        private Shop MapShop(ShopViewModel shop)
        {
            string uniqueFileName = UploadedFile(shop);

            return new Shop
            {
                Id=shop.ShopId,
                ShopName = shop.ShopName,
                City = shop.City,
                Address = shop.Address,
                PhotoPath = uniqueFileName,
                PhoneNumber = shop.PhoneNumber,
                Email = shop.Email,
                
            };
        }   
        private string UploadedFile(ShopViewModel shop)
        {
            string uniqueFileName = null;
            if (shop.Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + shop.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    shop.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private void DeleteFile(string fileName)
        {
            string imgToDelate = Path.Combine(webHostEnvironment.WebRootPath, "img\\", fileName);

            if (System.IO.File.Exists(imgToDelate))
            {
                System.IO.File.Delete(imgToDelate);
            }
        }

    }
}

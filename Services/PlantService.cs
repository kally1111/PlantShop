using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PlantShop.Data;
using PlantShop.DataAccess;
using PlantShop.Models;
using PlantShop.Models.PlantVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Services
{
    public class PlantService : IPlantService
    {
        private readonly PlantShopDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PlantService(PlantShopDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public GetPlantViewModel Get(GetPlantViewModel getPlantViewModel, int page)
        {

            int plantsPerPage = 2;
            int start = (page - 1) * plantsPerPage;
            var query = db.Plants.OrderBy(p => p.Id).Include(s => s.Shop)
                .Skip(start).Take(plantsPerPage);
            var count = db.Plants.Count();
            string order = getPlantViewModel.OrderBy;
            if (getPlantViewModel.OrderBy != null ||
                getPlantViewModel.SortByTypeOfPlant != null || getPlantViewModel.SortByPlaceToPlant != null)
            {
                if(getPlantViewModel.OrderBy != null) { 
                if (getPlantViewModel.SortByPlaceToPlant == null && getPlantViewModel.SortByTypeOfPlant == null)
                {
                    switch (getPlantViewModel.OrderBy)
                    {
                        case "orderByName":
                            query = db.Plants.OrderBy(p => p.PlantName).Include(s => s.Shop)
                     .Skip(start).Take(plantsPerPage); break;
                        case "orderByPrice":
                            query = db.Plants.OrderBy(p => p.Price).Include(s => s.Shop)
                     .Skip(start).Take(plantsPerPage); break;

                    }
                }
                else if(getPlantViewModel.SortByPlaceToPlant != null || getPlantViewModel.SortByTypeOfPlant != null)
                {
                    switch (getPlantViewModel.OrderBy)
                    {
                        case "orderByName":
                            query = db.Plants.OrderBy(p => p.PlantName).Include(s => s.Shop);
                            break;
                        case "orderByPrice":
                            query = db.Plants.OrderBy(p => p.Price).Include(s => s.Shop);
                            break;

                    }
                    if (getPlantViewModel.SortByPlaceToPlant != null && getPlantViewModel.SortByTypeOfPlant != null)
                    {
                        query = query
                            .Where(p => p.PlaceToPlant == getPlantViewModel.SortByPlaceToPlant)
                            .Where(p => p.TypeOfPlant == getPlantViewModel.SortByTypeOfPlant)
                           .Include(s => s.Shop).Skip(start).Take(plantsPerPage);
                        count = db.Plants.Where(p => p.PlaceToPlant == getPlantViewModel.SortByPlaceToPlant)
                            .Where(p => p.TypeOfPlant == getPlantViewModel.SortByTypeOfPlant).Count();
                    }
                    else if (getPlantViewModel.SortByPlaceToPlant != null)
                    {
                        query = query
                                .Where(p => p.PlaceToPlant == getPlantViewModel.SortByPlaceToPlant)
                               .Include(s => s.Shop).Skip(start).Take(plantsPerPage);
                        count = count = db.Plants.Where(p => p.PlaceToPlant == getPlantViewModel.SortByPlaceToPlant)
                            .Count();

                    }
                    else if (getPlantViewModel.SortByTypeOfPlant != null)
                    {
                        query = query
                                .Where(p => p.TypeOfPlant == getPlantViewModel.SortByTypeOfPlant)
                               .Include(s => s.Shop).Skip(start).Take(plantsPerPage);
                        count = db.Plants
                                .Where(p => p.TypeOfPlant == getPlantViewModel.SortByTypeOfPlant).Count();

                    }
                }
            }
                if(getPlantViewModel.OrderBy == null) 
                { 
                if (
                    getPlantViewModel.SortByTypeOfPlant != null && getPlantViewModel.SortByPlaceToPlant != null)
                {
                    query = db.Plants.Where(p => p.TypeOfPlant == getPlantViewModel.SortByTypeOfPlant)
                            .Where(p => p.PlaceToPlant == getPlantViewModel.SortByPlaceToPlant)
                           .OrderBy(p => p.Id).Include(s => s.Shop).Skip(start).Take(plantsPerPage);
                    count = db.Plants.Where(p => p.TypeOfPlant == getPlantViewModel.SortByTypeOfPlant)
                            .Where(p => p.PlaceToPlant == getPlantViewModel.SortByPlaceToPlant).Count();
                }
                else if (getPlantViewModel.SortByTypeOfPlant != null)
                {
                    query = db.Plants.Where(p => p.TypeOfPlant == getPlantViewModel.SortByTypeOfPlant)
                        .OrderBy(p => p.Id).Include(s => s.Shop).Skip(start).Take(plantsPerPage);
                    count = db.Plants.Where(p => p.TypeOfPlant == getPlantViewModel.SortByTypeOfPlant).Count();
                }
                else if (getPlantViewModel.SortByPlaceToPlant != null)
                {
                    query = db.Plants.Where(p => p.PlaceToPlant == getPlantViewModel.SortByPlaceToPlant)
                                    .OrderBy(p => p.Id).Include(s => s.Shop).Skip(start).Take(plantsPerPage);
                    count = db.Plants.Where(p => p.PlaceToPlant == getPlantViewModel.SortByPlaceToPlant).Count();
                }
            }
            }
            int pageCount;
            if (count != 0)
            {
                pageCount = Convert.ToInt32(Math.Ceiling(count / (double)plantsPerPage));
            }
            else
            {
                pageCount = 1;
            }
            getPlantViewModel.PageCount = pageCount;
            getPlantViewModel.CurrentPage = page;
            getPlantViewModel.Query = query;
            return getPlantViewModel;
        }

        public void Create(CreatePlantViewModel plant)
        {

            db.Add(MapCreatePlant(plant));
            
            db.SaveChanges();
            
        }

        public InfoPlantViewModel InfoPlant(int id)
        {
         return  MapInfoPlant(this.db.Plants.Include(x => x.Shop)
                .FirstOrDefault(p => p.Id == id));
        }
       
        public void Change(DetailedInfoPlantViewModel plant)
        {
            if (plant.Photo != null&&plant.PhotoPath!=null)
            {
                DeleteFile(plant.PhotoPath);
            }
            string uniqueFileName = ChangeUploadedFile(plant);
            var plantToChange = db.Plants.FirstOrDefault(x => x.Id == plant.PlantId);
            plantToChange.PlantName = plant.PlantName;
            plantToChange.Price = plant.Price;
            plantToChange.Description = plant.Description;
            plantToChange.PhotoPath =uniqueFileName;
            plantToChange.TypeOfPlant = plant.TypeOfPlant;
            plantToChange.PlaceToPlant = plant.PlaceToPlant;
            plantToChange.ShopId = plant.ShopId;
            db.SaveChanges();


        }

        public List<UpdateDataPVM> UpdateData(string searchString)
        {

            var q = this.db.Plants.Include(x=>x.Shop).ToList()
                .Where(p => p.PlantName.Contains(searchString)).Select(p => MapUpdatePlant(p))
                .OrderBy(p => p.PlantId).ToList();
            return q;
        }

        public DetailedInfoPlantViewModel DetailedInfo(int id)
        {
           
            return MapDetailedInfoPlant(this.db.Plants.Include(x=>x.Shop)
                .FirstOrDefault(pl => pl.Id == id)
                );
        }
        public InfoPlantViewModel Delete(int id)
        {
            return MapInfoPlant(this.db.Plants.Include(x => x.Shop)
                .FirstOrDefault(p => p.Id == id));
        }
        public void ConfermedDelete(int id)
        {
            var plantToDelete=this.db.Plants.Include(x => x.Shop)
                .FirstOrDefault(p => p.Id ==id);
            if (plantToDelete.PhotoPath != null)
            {
                DeleteFile(plantToDelete.PhotoPath);
            }
            db.Remove(plantToDelete);
            db.SaveChanges();
        }

        public GetByShopPVM GetByShop(int id, int page)
        {
            GetByShopPVM getByShopPVM = new GetByShopPVM();
            int plantsPerPage = 2;
            int pageCount;
            int start = (page - 1) * plantsPerPage;
            var query = db.Plants
                .Where(m => m.ShopId == id).OrderBy(p => p.PlantName)
                .Skip(start).Take(plantsPerPage);
            var count = db.Plants
                .Where(m => m.ShopId == id).OrderBy(p => p.PlantName).Count();
            if (count != 0)
            {
                pageCount = Convert.ToInt32(Math.Ceiling(count / (double)plantsPerPage));

            }
            else
            {
                pageCount = 1;
            }
            getByShopPVM.ShopId = id;
            getByShopPVM.PageCount = pageCount;
            getByShopPVM.CurrentPage = page;
            getByShopPVM.Query = query;
            return getByShopPVM;
        }


        private string UploadedFile(CreatePlantViewModel plant)
        {
            string uniqueFileName = null;
            if (plant.Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + plant.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    plant.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string ChangeUploadedFile(DetailedInfoPlantViewModel plant)
        {
            string uniqueFileName = null;
            if (plant.Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + plant.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    plant.Photo.CopyTo(fileStream);
                }
            }
            else
            {
                uniqueFileName = plant.PhotoPath;
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
        private UpdateDataPVM MapUpdatePlant(Plant plant)
        {
            if (plant == null)
            {
                return new UpdateDataPVM();
            }
            return new UpdateDataPVM
            {
                PlantId = plant.Id,
                PlantName = plant.PlantName,
                Price = plant.Price,
                PhotoPath = plant.PhotoPath,
                TypeOfPlant = plant.TypeOfPlant,
                ShopId=plant.ShopId,
                ShopName=plant.Shop.ShopName
            };

        }
        private InfoPlantViewModel MapInfoPlant(Plant plant)
        {

            if (plant == null)
            {
                return new InfoPlantViewModel();
            }
            return new InfoPlantViewModel
            {
                PlantId = plant.Id,
                PlantName = plant.PlantName,
                Price = plant.Price,
                Description = plant.Description,
                PhotoPath = plant.PhotoPath,
                TypeOfPlant = plant.TypeOfPlant,
                PlaceToPlant = plant.PlaceToPlant,
                ShopName = plant.Shop.ShopName,
                ShopId=plant.Shop.Id
            };
        }
        private Plant MapCreatePlant(CreatePlantViewModel plant)
        {
            string uniqueFileName = UploadedFile(plant);

            return new Plant
            {
                PlantName = plant.PlantName,
                Price = plant.Price,
                Description = plant.Description,
                PhotoPath = uniqueFileName,
                TypeOfPlant = plant.TypeOfPlant,
                PlaceToPlant = plant.PlaceToPlant,
                ShopId=plant.ShopId
            };
        }
        private DetailedInfoPlantViewModel MapDetailedInfoPlant(Plant plant)
        {
            if (plant == null)
            {
                return new DetailedInfoPlantViewModel();
            }
            return new DetailedInfoPlantViewModel
            {
                PlantId = plant.Id,
                PlantName = plant.PlantName,
                Price = plant.Price,
                Description = plant.Description,
                PhotoPath = plant.PhotoPath,
                TypeOfPlant = plant.TypeOfPlant,
                PlaceToPlant = plant.PlaceToPlant,
                ShopName=plant.Shop.ShopName

            };
        }

    }
}


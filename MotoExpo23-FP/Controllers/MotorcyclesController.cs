namespace MotoExpo23_FP.Controllers
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MotoExpo23_FP.Data.Models;
    using MotoExpo23_FP.Models.Motorcycles;
    using MotoExpo23_FP.Services;

    public class MotorcyclesController : Controller
    {
        private readonly IMotorcyclesService motorcyclesService;
        private readonly ILogger<MotorcyclesController> _logger;

        public MotorcyclesController(
            IMotorcyclesService motorcyclesService,
            ILogger<MotorcyclesController> logger)
        {
            this.motorcyclesService = motorcyclesService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var motorcycles = this.motorcyclesService.GetAll();
            var viewModels = new List<MotorcycleViewModel>();
            foreach (var motorcycle in motorcycles)
            {
                viewModels.Add(new MotorcycleViewModel
                {
                    Id= motorcycle.Id,
                    Price= motorcycle.Price,
                    Model= motorcycle.Model,
                    PictureUrl = motorcycle.PictureUrl,
                });
            }

            return View(viewModels);
        }

        public IActionResult Details(int id)
        {
            var motorcycle = this.motorcyclesService.GetDetails(id);
            var viewModel = new MotorcycleDetailsModel
            {
                Id = motorcycle.Id,
                Price = motorcycle.Price,
                Model = motorcycle.Model,
                PictureUrl = motorcycle.PictureUrl,
                EngineDisplacement = motorcycle.EngineDisplacement,
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MotorcycleCreateModel model)
        {
            var motorcycle = new Motorcycle
            {
                Price = Convert.ToDecimal(this.Request.Form["Price"]),
                Model = this.Request.Form["Model"],
                PictureUrl = this.Request.Form["PictureUrl"],
                EngineDisplacement = this.Request.Form["EngineDisplacement"],
            };

            this.motorcyclesService.Create(motorcycle);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var motorcycle = this.motorcyclesService.GetDetails(id);
            var viewModel = new MotorcycleUpdateModel
            {
                Id = motorcycle.Id,
                Price = motorcycle.Price,
                Model = motorcycle.Model,
                PictureUrl = motorcycle.PictureUrl,
                EngineDisplacement = motorcycle.EngineDisplacement,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] MotorcycleUpdateModel model)
        {
            var id = Convert.ToInt32(this.Request.Form["Id"]);
            var motorcycle = new Motorcycle
            {
                Id = id,
                Price = decimal.Parse(this.Request.Form["Price"]),
                Model = this.Request.Form["Model"],
                PictureUrl = this.Request.Form["PictureUrl"],
                EngineDisplacement = this.Request.Form["EngineDisplacement"],
            };

            this.motorcyclesService.Update(motorcycle);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            this.motorcyclesService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

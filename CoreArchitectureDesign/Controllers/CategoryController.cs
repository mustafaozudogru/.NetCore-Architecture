using CoreArchitectureDesign.Business.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreArchitectureDesign.Entities;
using System.Collections.Generic;
using CoreArchitectureDesign.Core.Common;

namespace CoreArchitectureDesign.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var result = this.categoryService.GetList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(Categories categories)
        {
            this.categoryService.Add(categories);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = this.categoryService.GetById(id);

            return this.View(result.ResultObject);
        }

        [HttpPost]
        public IActionResult Edit(Categories categories)
        {
            this.categoryService.Update(categories);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = this.categoryService.GetById(id);
            this.categoryService.Delete(result.ResultObject);

            return RedirectToAction("Index");
        }
    }
}
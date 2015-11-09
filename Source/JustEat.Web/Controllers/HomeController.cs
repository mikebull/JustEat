using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JustEat.Constants;
using JustEat.Model;
using JustEat.Service.Interfaces;
using JustEat.Web.ViewModels;
using PagedList;

namespace JustEat.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJustEatApiService _service;

        /// <summary>
        /// Inject API service into constructor
        /// </summary>
        /// <param name="service"></param>
        public HomeController(IJustEatApiService service)
        {
            _service = service;
        }

        /// <summary>
        /// Render initial search page
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(new RestaurantViewModel());
        }

        /// <summary>
        /// Render results of restaurant search
        /// </summary>
        /// <param name="outcode"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Results(string outcode, int page = 1)
        {
            var restaurants = _service.GetRestaurantsByOutcode(outcode, page, Paging.PageSize);

            var viewModel = new RestaurantViewModel
            {
                Restaurants = restaurants,
                Outcode = outcode
            };

            return View(viewModel);
        }

        /// <summary>
        /// Handle form submission to render restaurant results
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(RestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Results", new { outcode = model.Outcode });
        }

        /// <summary>
        /// Render restaurants, or show message if none returned
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult RenderResults(RestaurantViewModel model)
        {
            if (model.Restaurants.Any())
            {
                return PartialView("~/Views/Partials/RenderResults.cshtml", model);
            }

            return PartialView("~/Views/Partials/NoResults.cshtml", model.Outcode);
        }
    }
}
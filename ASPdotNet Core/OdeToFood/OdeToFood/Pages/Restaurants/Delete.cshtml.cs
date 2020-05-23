using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurant _restaurantData;

        public Restaurant Restaurant { get; set; }

        public DeleteModel(IRestaurant restaurantService)
        {
            _restaurantData = restaurantService;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = _restaurantData.GetRestaurantById(restaurantId);

            if (Restaurant == null)
            {
                RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost(int restaurantId)
        {
            Restaurant = _restaurantData.Delete(restaurantId);
            _restaurantData.Commit();

            if (Restaurant == null)
            {
                RedirectToPage("./NotFound");
            }

            TempData["Message"] = $"{Restaurant.Name}" + "successfully deleted";
            return RedirectToPage("./List");
        }
    }
}
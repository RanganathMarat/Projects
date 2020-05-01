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
    public class DetailModel : PageModel
    {
        private IRestaurant restaurantFetch;
        public Restaurant Restaurant { get; set; }
        [TempData]
        public string Message { get; set; }

        public DetailModel(IRestaurant restaurantFetch)
        {
            this.restaurantFetch = restaurantFetch;
        }

        public IActionResult OnGet(int restaurantID)
        {
            Restaurant = restaurantFetch.GetRestaurantById(restaurantID);
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}
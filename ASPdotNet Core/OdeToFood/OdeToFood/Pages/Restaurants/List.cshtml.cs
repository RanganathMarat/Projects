using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private IConfiguration config;
        private IRestaurant restaurantData;

        public ListModel(IConfiguration config, IRestaurant restaurantData)
        {
            this.config = config;
            this.restaurantData = restaurantData;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public void OnGet()
        {
            Message = config["Message"];
            Restaurants = restaurantData.GetRestuarantsByName(SearchTerm);
        }
    }
}
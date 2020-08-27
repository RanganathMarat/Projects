using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private IRestaurant restaurantFetch;
        private IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }
        public EditModel(IRestaurant restaurantFetch, IHtmlHelper htmlHelper)
        {
            this.restaurantFetch = restaurantFetch;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if(restaurantId.HasValue)
            {
                var validId = (int)restaurantId;
                Restaurant = restaurantFetch.GetRestaurantById(validId);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();





        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                Restaurant = restaurantFetch.Update(Restaurant);
                TempData["Message"] = "Restaurant successfully updated!!";
            }
            else
            {
                Restaurant = restaurantFetch.AddNew(Restaurant);
                TempData["Message"] = "Restaurant successfully added!!";
            }

            restaurantFetch.Commit();
            return RedirectToPage("./Detail", new { restaurantID = Restaurant.Id });

        }
    }
}
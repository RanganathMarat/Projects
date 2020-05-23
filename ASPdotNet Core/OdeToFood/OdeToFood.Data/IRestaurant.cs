using OdeToFood.Core;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace OdeToFood.Data
{
    public interface IRestaurant
    {
        IEnumerable<Restaurant> GetRestuarantsByName(string name=null);
        Restaurant GetRestaurantById(int restaurantId);
        Restaurant Update(Restaurant restaurant);
        Restaurant AddNew(Restaurant newRestaurant);
        Restaurant Delete(int id);
        int GetCountOfRestaurants();
        int Commit();
    }
}

using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurant
    {
        private OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }
        public Restaurant AddNew(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurantById(id);
            db.Restaurants?.Remove(restaurant);
            return restaurant;
        }

        public Restaurant GetRestaurantById(int restaurantId)
        {
            return db.Restaurants.Find(restaurantId);
        }

        public IEnumerable<Restaurant> GetRestuarantsByName(string name = null)
        {
            var query = from r in db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var entity = db.Restaurants.Attach(restaurant);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return restaurant;
        }
    }
}

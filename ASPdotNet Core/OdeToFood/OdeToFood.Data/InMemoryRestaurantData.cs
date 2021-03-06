﻿using System;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;
using System.Globalization;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurant
    {
        private readonly List<Restaurant> restaurants;
        private int nextAvailableId = 4;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Dominos", Location = "Bangalore", Cuisine = CuisineType.Italian},
                new Restaurant {Id = 2, Name = "RRR", Location = "Mysore", Cuisine = CuisineType.Indian},
                new Restaurant {Id = 3, Name = "Chillies", Location = "Mumbai", Cuisine = CuisineType.Mexican}
            };
        }

        public Restaurant AddNew(Restaurant newRestaurant)
        {
            var temp = new Restaurant()

            {
                Id = nextAvailableId++,
                Name = newRestaurant.Name,
                Cuisine = newRestaurant.Cuisine,
                Location = newRestaurant.Location
            };
            restaurants.Add(temp);
            return temp;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault<Restaurant>(r => r.Id == id);
            restaurants?.Remove(restaurant);
            return restaurant;
        }

        public Restaurant GetRestaurantById(int restaurantId)
        {
            return restaurants.SingleOrDefault(r => r.Id == restaurantId);
        }

        public IEnumerable<Restaurant> GetRestuarantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name, true, CultureInfo.CurrentCulture)
                   orderby r.Name
                   select r;

        }

        public Restaurant Update(Restaurant restaurant)
        {
            var foundRestaurant = restaurants.SingleOrDefault(r => r.Id == restaurant.Id);
            if (foundRestaurant != null)
            {
                foundRestaurant.Name = restaurant.Name;
                foundRestaurant.Cuisine = restaurant.Cuisine;
                foundRestaurant.Location = restaurant.Location;
            }

            return foundRestaurant;
        }
    }
}

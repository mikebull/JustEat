using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustEat.Model;

namespace JustEat.Test.Unit
{
    public static class DummyData
    {
        /// <summary>
        /// Dummy data for empty request
        /// </summary>
        /// <returns></returns>
        public static RequestRoot EmptyRequest()
        {
            return new RequestRoot
            {
                Restaurants = new List<Restaurant>()
            };
        }

        /// <summary>
        /// Dummy data for request
        /// </summary>
        /// <returns></returns>
        public static RequestRoot PopulatedRequest()
        {
            return new RequestRoot
            {
                Restaurants = new List<Restaurant>
                {
                    new Restaurant
                    {
                        Id = 4741,
                        Name = "New Dewaniam",
                        Address = "225A Camberwell New Road",
                        Postcode = "SE5 0TH",
                        City = "London",
                        CuisineTypes = new List<CuisineType>
                        {
                            new CuisineType {
                                Id = 31,
                                Name = "Indian",
                                SeoName = null,
                            },
                            new CuisineType {
                                Id = 86,
                                Name = "Drinks",
                                SeoName = null,
                            },
                        },
                        Url = "http://new-dewaniam.just-eat.co.uk",
                        RatingStars = 4.64,
                        NumberOfRatings = 1746
                    }
                }
            };
        }
    }
}

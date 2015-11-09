using System;
using System.Linq;
using JustEat.Model;
using JustEat.Modules;
using JustEat.Service.Interfaces;
using Ninject;

namespace JustEat.CommandLine
{
    public class Program
    {
        /// <summary>
        /// Main method for command line output
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var outcode = String.Empty;

            Console.WriteLine();
            Console.WriteLine("Please enter your outcode and press return, or press 0 to exit");
            outcode = Console.ReadLine();
            Console.WriteLine();
            if (outcode == "0") return;

            int page = 1;

            var restaurantsToShow = true;

            while (restaurantsToShow)
            {
                var input = String.Empty;

                restaurantsToShow = GetRestaurantByOutcode(outcode, page);

                if (restaurantsToShow)
                {
                    page++;

                    Console.WriteLine();
                    Console.WriteLine("Press any key to view more, or 0 to exit");

                    input = Console.ReadLine();
                    Console.WriteLine();
                    if (input == "0")
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("No more to show! Press any key to exit");
                    input = Console.ReadLine();
                    return;
                }
            }
        }

        /// <summary>
        /// Obtain restaurants by outcode using service
        /// </summary>
        /// <param name="outcode"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private static bool GetRestaurantByOutcode(string outcode, int page = 1)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load<BusinessLogicModule>();

            var service = kernel.Get<IJustEatApiService>();

            var restaurants = service
                .GetRestaurantsByOutcode(outcode, page)
                .ToList();

            if (!restaurants.Any())
            {
                return false;
            }

            foreach (var restaurant in restaurants)
            {
                DisplayRestaurant(restaurant);
            }

            return true;
        }

        /// <summary>
        /// Render restaurant information onto console
        /// </summary>
        /// <param name="restaurant"></param>
        public static void DisplayRestaurant(Restaurant restaurant)
        {
            Console.WriteLine(restaurant.Name);
            Console.WriteLine("Rated: {0} stars from {1} reviews", restaurant.RatingStars, restaurant.NumberOfRatings);
            Console.WriteLine("Cuisine Served: " + String.Join(", ", restaurant.CuisineTypes.Select(type => type.Name)));
            Console.WriteLine();
        }
    }
}

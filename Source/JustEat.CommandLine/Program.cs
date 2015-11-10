using System;
using System.Linq;
using JustEat.Constants;
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
            Console.WriteLine("Please enter your outcode and press return, or type \"{0}\" to exit", Commands.Exit);
            outcode = Console.ReadLine();
            Console.WriteLine();
            if (String.Equals(outcode, Commands.Exit, StringComparison.InvariantCultureIgnoreCase)) return;

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
                    Console.WriteLine("Press any key to view more, or type \"{0}\" to exit", Commands.Exit);

                    input = Console.ReadLine();
                    Console.WriteLine();
                    if (String.Equals(input, Commands.Exit, StringComparison.InvariantCultureIgnoreCase))
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
                Console.WriteLine(restaurant.Name);
                Console.WriteLine("Rated: {0} stars from {1} reviews", restaurant.RatingStars, restaurant.NumberOfRatings);
                Console.WriteLine("Cuisine Served: " + String.Join(", ", restaurant.CuisineTypes.Select(type => type.Name)));
                Console.WriteLine();
            }

            return true;
        }
    }
}

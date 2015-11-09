using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JustEat.Model;

namespace JustEat.Web.ViewModels
{
    public class RestaurantViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }

        [Required(ErrorMessage = "Please enter a valid outcode")]
        public string Outcode { get; set; }
    }
}
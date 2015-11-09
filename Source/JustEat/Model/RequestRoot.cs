using System.Collections.Generic;

namespace JustEat.Model
{
    public class RequestRoot
    {
        public string ShortResultText { get; set; }

        public List<Restaurant> Restaurants { get; set; }
    }
}

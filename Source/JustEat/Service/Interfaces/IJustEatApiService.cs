using System.Collections.Generic;
using JustEat.Model;

namespace JustEat.Service.Interfaces
{
    public interface IJustEatApiService
    {
        IEnumerable<Restaurant> GetRestaurantsByOutcode(string outcode, int page = 1, int pageSize = 5);
    }
}
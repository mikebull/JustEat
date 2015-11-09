using System.Collections.Generic;
using System.Linq;
using System.Net;
using JustEat.Factory.Interfaces;
using JustEat.Model;
using JustEat.Service.Interfaces;
using PagedList;

namespace JustEat.Service
{
    public class JustEatApiService : IJustEatApiService
    {
        private readonly IClientFactory _clientFactory;
        private readonly IRequestFactory _requestFactory;

        /// <summary>
        /// Inject client and request factory into constructor
        /// </summary>
        /// <param name="clientFactory"></param>
        /// <param name="requestFactory"></param>
        public JustEatApiService(IClientFactory clientFactory, IRequestFactory requestFactory)
        {
            _clientFactory = clientFactory;
            _requestFactory = requestFactory;
        }

        /// <summary>
        /// Get paged list of restaurants by given outcode
        /// </summary>
        /// <param name="outcode"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Restaurant> GetRestaurantsByOutcode(string outcode, int page = 1, int pageSize = 5)
        {
            var client = _clientFactory.Create();
            var request = _requestFactory.GetRestaurantsByOutcode(outcode);

            var response = client.Execute<RequestRoot>(request);

            if (response.StatusCode != HttpStatusCode.OK || response.Data == null || response.Data.Restaurants == null)
            {
                return new List<Restaurant>();
            }

            var restaurants = response
                    .Data
                    .Restaurants;

            var sortedRestaurants = restaurants.OrderByDescending(restaurant => restaurant.RatingStars);

            return sortedRestaurants.ToPagedList(page, pageSize);
        }
    }
}

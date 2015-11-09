using System.Configuration;
using JustEat.Factory.Interfaces;
using RestSharp;

namespace JustEat.Factory
{
    public class RestaurantApiClientFactory : IClientFactory
    {
        /// <summary>
        /// Instantiate new REST client
        /// </summary>
        /// <returns></returns>
        public IRestClient Create()
        {
            return new RestClient(ConfigurationManager.AppSettings["JustEatApi"]);
        }
    }
}

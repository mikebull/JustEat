using JustEat.Factory.Interfaces;
using RestSharp;

namespace JustEat.Factory
{
    public class RestaurantApiRequestFactory : IRequestFactory
    {
        /// <summary>
        /// Send request to obtain restaurants by outcode
        /// </summary>
        /// <param name="outcode"></param>
        /// <returns></returns>
        public IRestRequest GetRestaurantsByOutcode(string outcode)
        {
            var request = new RestRequest("restaurants", Method.GET);
            var parameter = CreateParameterByOutcode(outcode);
            
            request.AddParameter(parameter);
            
            request.AddHeader("Accept-Tenant", "uk");
            request.AddHeader("Accept-Language", "en-GB");
            request.AddHeader("Authorization", "Basic VGVjaFRlc3RBUEk6dXNlcjI=");

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            return request;
        }

        /// <summary>
        /// Create RestClient parameter querystring object for outcode
        /// </summary>
        /// <param name="outcode"></param>
        /// <returns></returns>
        private Parameter CreateParameterByOutcode(string outcode)
        {
            return new Parameter
            {
                Name = "q",
                Value = outcode,
                Type = ParameterType.QueryString
            };
        }
    }
}

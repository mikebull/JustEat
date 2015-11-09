using RestSharp;

namespace JustEat.Factory.Interfaces
{
    public interface IRequestFactory
    {
        IRestRequest GetRestaurantsByOutcode(string outcode);
    }
}
using RestSharp;

namespace JustEat.Factory.Interfaces
{
    public interface IClientFactory
    {
        IRestClient Create();
    }
}
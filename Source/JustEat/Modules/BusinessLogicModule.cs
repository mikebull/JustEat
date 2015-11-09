using JustEat.Factory;
using JustEat.Factory.Interfaces;
using JustEat.Service;
using JustEat.Service.Interfaces;
using Ninject.Modules;

namespace JustEat.Modules
{
    public class BusinessLogicModule : NinjectModule
    {
        /// <summary>
        /// Bindings for Ninject
        /// </summary>
        public override void Load()
        {
            Bind<IJustEatApiService>().To<JustEatApiService>();
            Bind<IClientFactory>().To<RestaurantApiClientFactory>();
            Bind<IRequestFactory>().To<RestaurantApiRequestFactory>();
        }
    }
}
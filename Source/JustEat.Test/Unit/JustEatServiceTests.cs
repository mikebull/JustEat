using System.Collections.Generic;
using System.Linq;
using System.Net;
using JustEat.Factory.Interfaces;
using JustEat.Model;
using JustEat.Service;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace JustEat.Test.Unit
{
    [TestFixture]
    public class JustEatApiServiceTests
    {
        /// <summary>
        /// Returns single restaurant in list from data
        /// </summary>
        [Test]
        public void ReturnRestaurantsByOutcodeSuccess()
        {
            var mockData = DummyData.PopulatedRequest();

            var mockClientFactory = MockedClientFactory(mockData, HttpStatusCode.OK);
            var mockRequestFactory = MockedRequestFactory();

            // Add mocked data and factories to service
            var restaurantService = new JustEatApiService(mockClientFactory, mockRequestFactory);

            var actual = restaurantService
                .GetRestaurantsByOutcode("se11")
                .ToList();

            // Then the restaurant list is as expected
            var expected = mockData.Restaurants;

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        /// <summary>
        /// Returns an empty list, as no restaurants in data
        /// </summary>
        [Test]
        public void ReturnEmptyIfNoRestaurantsFound()
        {
            var mockData = DummyData.EmptyRequest();

            var mockClientFactory = MockedClientFactory(mockData, HttpStatusCode.OK);
            var mockRequestFactory = MockedRequestFactory();

            // Add mocked data and factories to service
            var restaurantService = new JustEatApiService(mockClientFactory, mockRequestFactory);

            var actual = restaurantService
                .GetRestaurantsByOutcode("")
                .ToList();

            // Then the restaurant list is as expected
            var expected = mockData.Restaurants;

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        /// <summary>
        /// Returns an empty list, as data is empty
        /// </summary>
        [Test]
        public void ReturnDefaultWhenFailure()
        {
            var mockData = new RequestRoot();

            var mockClientFactory = MockedClientFactory(mockData, HttpStatusCode.NotFound);
            var mockRequestFactory = MockedRequestFactory();

            // Add mocked data and factories to service
            var restaurantService = new JustEatApiService(mockClientFactory, mockRequestFactory);

            var actual = restaurantService
                .GetRestaurantsByOutcode("")
                .ToList();

            // Then the restaurant list is as expected
            var expected = new List<Restaurant>();

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        /// <summary>
        /// Setup of client factory
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private IClientFactory MockedClientFactory(RequestRoot data, HttpStatusCode statusCode)
        {
            var mockedRestClient = new Mock<IRestClient>();
            var clientFactory = new Mock<IClientFactory>();

            mockedRestClient
                .Setup(client => client.Execute<RequestRoot>(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse<RequestRoot>
                {
                    StatusCode = statusCode,
                    Data = data
                });

            clientFactory
                .Setup(factory => factory.Create())
                .Returns(mockedRestClient.Object);

            return clientFactory.Object;
        }

        /// <summary>
        /// Setup of request factory
        /// </summary>
        /// <returns></returns>
        private IRequestFactory MockedRequestFactory()
        {
            var requestFactory = new Mock<IRequestFactory>();

            return requestFactory.Object;
        }
    }
}

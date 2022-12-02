using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Tischreservierung.Controllers;
using Tischreservierung.Data;
using Tischreservierung.Models;

namespace Tischreservierung.Tests
{
    public class RestaurantControllerTest
    {
        [Fact]
        public async Task GetAllRestaurants()
        {
            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(r => r.GetRestaurants()).ReturnsAsync(GetRestaurantTestData);
            var restaurantController = new RestaurantsController(restaurantRepository.Object);

            var actionResult = await restaurantController.GetRestaurants();
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(4, (result.Value as List<Restaurant>).Count());

            restaurantRepository.Verify(r => r.GetRestaurants(), Times.Once);
            restaurantRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetRestauranById()
        {
            int restaurantId = 10;
            Restaurant restaurant = new Restaurant { Id = restaurantId, Name = "R10" };

            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(r => r.GetRestaurantById(restaurantId)).ReturnsAsync(restaurant);
            var restaurantController = new RestaurantsController(restaurantRepository.Object);

            var actionResult = await restaurantController.GetRestaurant(restaurantId);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(restaurant, result.Value as Restaurant);

            restaurantRepository.Verify(r => r.GetRestaurantById(It.IsAny<int>()));
            restaurantRepository.VerifyNoOtherCalls();
        }

        private static List<Restaurant> GetRestaurantTestData()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant() { Id = 1, Name = "R1" });
            restaurants.Add(new Restaurant() { Id = 2, Name = "R2" });
            restaurants.Add(new Restaurant() { Id = 3, Name = "R3" });
            restaurants.Add(new Restaurant() { Id = 4, Name = "R4" });
            return restaurants;
        }
    }
}
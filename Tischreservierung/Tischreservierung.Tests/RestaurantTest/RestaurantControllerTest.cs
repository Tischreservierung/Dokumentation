using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Tischreservierung.Controllers;
using Tischreservierung.Data.RestaurantRepo;
using Tischreservierung.Models;

namespace Tischreservierung.Tests.RestaurantTest
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

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Equal(4, ((List<Restaurant>)result.Value!).Count());

            restaurantRepository.Verify(r => r.GetRestaurants());
            restaurantRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetRestaurantReturnsNotFound()
        {
            var restaurantRepository = new Mock<IRestaurantRepository>();
            var restaurantController = new RestaurantsController(restaurantRepository.Object);

            var actionResult = await restaurantController.GetRestaurant(10);

            Assert.IsType<NotFoundResult>(actionResult.Result);

            restaurantRepository.Verify(r => r.GetRestaurantById(10));
            restaurantRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetRestauranById()
        {
            int restaurantId = 10;
            Restaurant restaurant = new() { Id = restaurantId, Name = "R10" };

            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(r => r.GetRestaurantById(restaurantId)).ReturnsAsync(restaurant);
            var restaurantController = new RestaurantsController(restaurantRepository.Object);

            var actionResult = await restaurantController.GetRestaurant(restaurantId);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Equal(restaurant, result.Value as Restaurant);

            restaurantRepository.Verify(r => r.GetRestaurantById(It.IsAny<int>()));
            restaurantRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task PostRestaurant()
        {
            int restaurantId = 10;
            Restaurant restaurant = new() { Id = restaurantId, Name = "R10" };

            var restaurantRepository = new Mock<IRestaurantRepository>();
            restaurantRepository.Setup(r => r.InsertRestaurant(It.IsAny<Restaurant>()));
            var restaurantController = new RestaurantsController(restaurantRepository.Object);

            var actionResult = await restaurantController.PostRestaurant(restaurant);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = actionResult.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result!.StatusCode);
            Assert.Equal(restaurant, result.Value as Restaurant);

            restaurantRepository.Verify(r => r.InsertRestaurant(restaurant));
            restaurantRepository.Verify(r => r.Save());
            restaurantRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteRestaurant()
        {
            var restaurantRepository = new Mock<IRestaurantRepository>();

            restaurantRepository.Setup(r => r.GetRestaurantById(10)).ReturnsAsync(new Restaurant());
            restaurantRepository.Setup(r => r.DeleteRestaurant(It.IsAny<Restaurant>()));
            var restaurantController = new RestaurantsController(restaurantRepository.Object);

            var actionResult = await restaurantController.DeleteRestaurant(10);

            Assert.IsType<NoContentResult>(actionResult);

            restaurantRepository.Verify(r => r.GetRestaurantById(10));
            restaurantRepository.Verify(r => r.DeleteRestaurant(It.IsAny<Restaurant>()));
            restaurantRepository.Verify(r => r.Save());
            restaurantRepository.VerifyNoOtherCalls();
        }

        private static List<Restaurant> GetRestaurantTestData()
        {
            List<Restaurant> restaurants = new();
            restaurants.Add(new Restaurant() { Id = 1, Name = "R1" });
            restaurants.Add(new Restaurant() { Id = 2, Name = "R2" });
            restaurants.Add(new Restaurant() { Id = 3, Name = "R3" });
            restaurants.Add(new Restaurant() { Id = 4, Name = "R4" });
            return restaurants;
        }
    }
}
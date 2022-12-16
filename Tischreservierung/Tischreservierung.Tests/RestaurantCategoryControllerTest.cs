using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tischreservierung.Controllers;
using Tischreservierung.Data;
using Tischreservierung.Models;

namespace Tischreservierung.Tests
{
    public class RestaurantCategoryControllerTest
    {
        [Fact]
        public async Task GetRestaurantCategories()
        {
            var restaurantCategoryRepository = new Mock<IRestaurantCategoryRepository>();
            restaurantCategoryRepository.Setup(r => r.GetRestaurantCategories()).ReturnsAsync(GetRestaurantCategoryTestData());
            var restaurantCategoryController = new RestaurantCategoriesController(restaurantCategoryRepository.Object);

            var actionResult = await restaurantCategoryController.GetRestaurantCategories();
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Equal(3, (result.Value! as List<RestaurantCategory>).Count());

            restaurantCategoryRepository.Verify(r => r.GetRestaurantCategories(), Times.Once);
            restaurantCategoryRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetRestaurantCategory()
        {
            var repository = new Mock<IRestaurantCategoryRepository>();
            repository.Setup(r => r.GetRestaurantCategory("Pizza")).ReturnsAsync(GetRestaurantCategoryTestData()[0]);

            var controller = new RestaurantCategoriesController(repository.Object);

            var actionResult = await controller.GetRestaurantCategory("Pizza");

            Assert.NotNull(actionResult);
            Assert.Equal("Pizza", actionResult.Value!.Category);

            repository.Verify(r => r.GetRestaurantCategory(It.IsAny<string>()));
            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task InsertRestaurantCategory()
        {
            RestaurantCategory restaurantCategory = GetRestaurantCategoryTestData()[0];

            var repository = new Mock<IRestaurantCategoryRepository>();
            repository.Setup(r => r.InsertRestaurantCategory(It.IsAny<RestaurantCategory>()));
            var controller = new RestaurantCategoriesController(repository.Object);

            var actionResult = await controller.PostRestaurantCategory(restaurantCategory);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = actionResult.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result!.StatusCode);
            Assert.Equal(restaurantCategory, result.Value as RestaurantCategory);

            repository.Verify(r => r.InsertRestaurantCategory(restaurantCategory));
            repository.Verify(r => r.Save());
            repository.VerifyNoOtherCalls();

        }

        [Fact]
        public async Task DeleteRestauratCategory()
        {
            var repository = new Mock<IRestaurantCategoryRepository>();

            repository.Setup(r => r.GetRestaurantCategory("Pizza")).ReturnsAsync(new RestaurantCategory());
            repository.Setup(r => r.DeleteRestaurantCategory(It.IsAny<RestaurantCategory>()));
            var controller = new RestaurantCategoriesController(repository.Object);

            var actionResult = await controller.DeleteRestaurantCategory("Pizza");

            Assert.IsType<NoContentResult>(actionResult);

            repository.Verify(r => r.GetRestaurantCategory("Pizza"));
            repository.Verify(r => r.DeleteRestaurantCategory(It.IsAny<RestaurantCategory>()));
            repository.Verify(r => r.Save());
            repository.VerifyNoOtherCalls();
        }

        private static List<RestaurantCategory> GetRestaurantCategoryTestData()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant() { Id = 1, Name = "R1" });
            restaurants.Add(new Restaurant() { Id = 2, Name = "R2" });
            restaurants.Add(new Restaurant() { Id = 3, Name = "R3" });


            List<RestaurantCategory> restaurantCategories = new List<RestaurantCategory>();
            restaurantCategories.Add(new RestaurantCategory() { Category = "Pizza", Restaurants = restaurants });
            restaurantCategories.Add(new RestaurantCategory() { Category = "Pommes", Restaurants = restaurants });
            restaurants.Add(new Restaurant() { Id = 4, Name = "R4" });
            restaurantCategories.Add(new RestaurantCategory() { Category = "Ita", Restaurants = restaurants });
            return restaurantCategories;
        }
    }
}

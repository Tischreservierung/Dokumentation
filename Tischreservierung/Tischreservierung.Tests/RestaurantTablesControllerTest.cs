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
    public class RestaurantTablesControllerTest
    {
        [Fact]
        public async Task GetAllRestaurants()
        {
            var tableRepository = new Mock<IRestaurantTableRepository>();
            tableRepository.Setup(r => r.GetRestaurantTables()).ReturnsAsync(GetRestaurantTableTestData);
            var tableController = new RestaurantTablesController(tableRepository.Object);

            var actionResult = await tableController.GetRestaurantTables();

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Equal(6, ((List<RestaurantTable>)result.Value!).Count());

            tableRepository.Verify(r => r.GetRestaurantTables());
            tableRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetRestaurantReturnsNotFound()
        {
            var tableRepository = new Mock<IRestaurantTableRepository>();
            var tableController = new RestaurantTablesController(tableRepository.Object);

            var actionResult = await tableController.GetRestaurantTable(10);

            Assert.IsType<NotFoundResult>(actionResult.Result);

            tableRepository.Verify(r => r.GetRestaurantTableById(10));
            tableRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetRestauranById()
        {
            int restaurantTableId = 10;
            RestaurantTable table = new() { Id = restaurantTableId, SeatPlaces = 5, RestaurantId = 20 };

            var tableRepository = new Mock<IRestaurantTableRepository>();
            tableRepository.Setup(r => r.GetRestaurantTableById(restaurantTableId)).ReturnsAsync(table);
            var tableController = new RestaurantTablesController(tableRepository.Object);

            var actionResult = await tableController.GetRestaurantTable(restaurantTableId);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Equal(table, result.Value as RestaurantTable);

            tableRepository.Verify(r => r.GetRestaurantTableById(It.IsAny<int>()));
            tableRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task PostRestaurant()
        {
            int restaurantTableId = 10;
            RestaurantTable table = new() { Id = restaurantTableId, SeatPlaces = 5, RestaurantId = 20 };

            var tableRepository = new Mock<IRestaurantTableRepository>();
            tableRepository.Setup(r => r.InsertRestaurantTable(It.IsAny<RestaurantTable>()));
            var tableController = new RestaurantTablesController(tableRepository.Object);

            var actionResult = await tableController.PostRestaurantTable(table);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = actionResult.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result!.StatusCode);
            Assert.Equal(table, result.Value as RestaurantTable);

            tableRepository.Verify(r => r.InsertRestaurantTable(table));
            tableRepository.Verify(r => r.Save());
            tableRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteRestaurant()
        {
            var tableRepository = new Mock<IRestaurantTableRepository>();

            tableRepository.Setup(r => r.GetRestaurantTableById(10)).ReturnsAsync(new RestaurantTable());
            tableRepository.Setup(r => r.DeleteRestaurantTable(It.IsAny<RestaurantTable>()));
            var tableController = new RestaurantTablesController(tableRepository.Object);

            var actionResult = await tableController.DeleteRestaurantTable(10);

            Assert.IsType<NoContentResult>(actionResult);

            tableRepository.Verify(r => r.GetRestaurantTableById(10));
            tableRepository.Verify(r => r.DeleteRestaurantTable(It.IsAny<RestaurantTable>()));
            tableRepository.Verify(r => r.Save());
            tableRepository.VerifyNoOtherCalls();
        }

        private static List<RestaurantTable> GetRestaurantTableTestData()
        {
            List<RestaurantTable> restaurants = new();
            restaurants.Add(new RestaurantTable() { Id = 1, SeatPlaces = 2, RestaurantId = 1 });
            restaurants.Add(new RestaurantTable() { Id = 2, SeatPlaces = 4, RestaurantId = 1 });
            restaurants.Add(new RestaurantTable() { Id = 3, SeatPlaces = 3, RestaurantId = 1 });
            restaurants.Add(new RestaurantTable() { Id = 4, SeatPlaces = 4, RestaurantId = 2 });
            restaurants.Add(new RestaurantTable() { Id = 5, SeatPlaces = 3, RestaurantId = 2 });
            restaurants.Add(new RestaurantTable() { Id = 6, SeatPlaces = 3, RestaurantId = 3 });
            return restaurants;
        }
    }
}

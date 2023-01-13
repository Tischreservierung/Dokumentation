using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tischreservierung.Controllers;
using Tischreservierung.Data;
using Tischreservierung.Data.RestaurantRepo;
using Tischreservierung.Models;

namespace Tischreservierung.Tests.RestaurantTest
{
    public class OpeningTimeControllerTest
    {
        [Fact]
        public async Task GetOpeningTimesForRestaurant()
        {
            List<RestaurantOpeningTime> times = OpeningTimeTestData();

            var openingTimeRepository = new Mock<IOpeningTimeRepository>();
            openingTimeRepository.Setup(r => r.GetOpeningTimesByRestaurant(1)).ReturnsAsync(new List<RestaurantOpeningTime>() { times[0], times[1], times[4], times[5] });
            var openingTimeController = new RestaurantOpeningTimesController(openingTimeRepository.Object);

            var actionResult = await openingTimeController.GetRestaurantOpeningTimeForRestaurant(1);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Equal(4, ((List<RestaurantOpeningTime>)result.Value!).Count);


            openingTimeRepository.Verify(r => r.GetOpeningTimesByRestaurant(It.IsAny<int>()));
            openingTimeRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetOpeningTimesForDay()
        {
            List<RestaurantOpeningTime> times = OpeningTimeTestData();

            var openingTimeRepository = new Mock<IOpeningTimeRepository>();
            openingTimeRepository.Setup(r => r.GetOpeningTimesByDay(4)).ReturnsAsync(new List<RestaurantOpeningTime>() { times[4], times[5] });
            var openingTimeController = new RestaurantOpeningTimesController(openingTimeRepository.Object);

            var actionResult = await openingTimeController.GetRestaurantOpeningTimeForDay(4);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Equal(2, ((List<RestaurantOpeningTime>)result.Value!).Count);


            openingTimeRepository.Verify(r => r.GetOpeningTimesByDay(It.IsAny<int>()));
            openingTimeRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetOpeningTimesForRestaurantAndDay()
        {
            List<RestaurantOpeningTime> times = OpeningTimeTestData();

            var openingTimeRepository = new Mock<IOpeningTimeRepository>();
            openingTimeRepository.Setup(r => r.GetOpeningTimesByDayAndRestaurant(1,4)).ReturnsAsync(new List<RestaurantOpeningTime>() { times[4], times[5] });
            var openingTimeController = new RestaurantOpeningTimesController(openingTimeRepository.Object);

            var actionResult = await openingTimeController.GetRestaurantOpeningTimeForRestaurantAndDay(1,4);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Equal(2, ((List<RestaurantOpeningTime>)result.Value!).Count);


            openingTimeRepository.Verify(r => r.GetOpeningTimesByDayAndRestaurant(It.IsAny<int>(), It.IsAny<int>()));
            openingTimeRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task PostOpeningTime()
        {
            RestaurantOpeningTime openingTime = new RestaurantOpeningTime() { Day = 1, 
                ClosingTime = new DateTime(1900, 1, 1, 18, 0, 0), OpeningTime = new DateTime(1900, 1, 1, 10, 0, 0), RestaurantId = 1 };

            var openingTimeRepository = new Mock<IOpeningTimeRepository>();
            openingTimeRepository.Setup(oT => oT.InsertRestaurantOpeningTime(openingTime));
            var openingTimeController = new RestaurantOpeningTimesController(openingTimeRepository.Object);

            var actionResult = await openingTimeController.PostRestaurantOpeningTime(openingTime);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var result = actionResult.Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result!.StatusCode);
            Assert.Equal(openingTime, result.Value as RestaurantOpeningTime);

            openingTimeRepository.Verify(r => r.InsertRestaurantOpeningTime(openingTime));
            openingTimeRepository.Verify(r => r.Save());
            openingTimeRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateOpeningTime()
        {
        }

        [Fact]
        public async Task DeleteOpeningTime()
        {
        }


        private static List<RestaurantOpeningTime> OpeningTimeTestData()
        {
            List<RestaurantOpeningTime> openingTimes = new();
            openingTimes.Add(new RestaurantOpeningTime() { Day = 1, ClosingTime = new DateTime(1900, 1, 1, 18, 0, 0), OpeningTime = new DateTime(1900, 1, 1, 10, 0, 0), RestaurantId = 1 });
            openingTimes.Add(new RestaurantOpeningTime() { Day = 0, ClosingTime = new DateTime(1900, 1, 1, 18, 0, 0), OpeningTime = new DateTime(1900, 1, 1, 10, 0, 0), RestaurantId = 1 });
            openingTimes.Add(new RestaurantOpeningTime() { Day = 2, ClosingTime = new DateTime(1900, 1, 1, 18, 0, 0), OpeningTime = new DateTime(1900, 1, 1, 10, 0, 0), RestaurantId = 2 });
            openingTimes.Add(new RestaurantOpeningTime() { Day = 3, ClosingTime = new DateTime(1900, 1, 1, 18, 0, 0), OpeningTime = new DateTime(1900, 1, 1, 10, 0, 0), RestaurantId = 2 });
            openingTimes.Add(new RestaurantOpeningTime() { Day = 4, ClosingTime = new DateTime(1900, 1, 1, 12, 0, 0), OpeningTime = new DateTime(1900, 1, 1, 10, 0, 0), RestaurantId = 1 });
            openingTimes.Add(new RestaurantOpeningTime() { Day = 4, ClosingTime = new DateTime(1900, 1, 1, 18, 0, 0), OpeningTime = new DateTime(1900, 1, 1, 14, 0, 0), RestaurantId = 1 });
            
            return openingTimes;
        }
    }
}

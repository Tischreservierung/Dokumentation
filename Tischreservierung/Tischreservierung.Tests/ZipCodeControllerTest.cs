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
    public class ZipCodeControllerTest
    {

        [Fact]
        public async Task GetAllZipCodes()
        {
            var zipCodeRepository = new Mock<IZipCodeRepository>();
            zipCodeRepository.Setup(r => r.GetZipCodes()).ReturnsAsync(GetZipCodeTestData);
            var restaurantController = new ZipCodesController(zipCodeRepository.Object);

            var actionResult = await restaurantController.GetZipcodes();

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Equal(4, ((List<ZipCode>)result.Value!).Count());

            zipCodeRepository.Verify(r => r.GetZipCodes());
            zipCodeRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetZipCodesByZipCodeNr()
        {
            List<ZipCode> zipCodes = GetZipCodeTestData();

            var zipCodeRepository = new Mock<IZipCodeRepository>();
            zipCodeRepository.Setup(r => r.GetByZipCode("4470")).ReturnsAsync(new List<ZipCode>() { zipCodes[0] });
            var zipCodeController = new ZipCodesController(zipCodeRepository.Object);

            var actionResult = await zipCodeController.GetZipCodesByZipCode("4470");

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.Single(((List<ZipCode>)result.Value!));

            zipCodeRepository.Verify(r => r.GetByZipCode(It.IsAny<string>()));
            zipCodeRepository.VerifyNoOtherCalls();
        }


        [Fact]
        public async Task GetZipCodesByDistrict()
        {
            List<ZipCode> zipCodes = GetZipCodeTestData();

            var zipCodeRepository = new Mock<IZipCodeRepository>();
            zipCodeRepository.Setup(r => r.GetByDistrict("Linz-Land")).ReturnsAsync(new List<ZipCode>() { zipCodes[0], zipCodes[3] });
            var zipCodeController = new ZipCodesController(zipCodeRepository.Object);

            var actionResult = await zipCodeController.GetZipcodesByDistrict("Linz-Land");

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(2, ((List<ZipCode>)result.Value!).Count);

            zipCodeRepository.Verify(r => r.GetByDistrict(It.IsAny<string>()));
            zipCodeRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetZipCodesByLocation()
        {
            List<ZipCode> zipCodes = GetZipCodeTestData();

            var zipCodeRepository = new Mock<IZipCodeRepository>();
            zipCodeRepository.Setup(r => r.GetByLocation("Linz")).ReturnsAsync(new List<ZipCode>() { zipCodes[1], zipCodes[2] });
            var zipCodeController = new ZipCodesController(zipCodeRepository.Object);

            var actionResult = await zipCodeController.GetZipcodesByLocation("Linz");

            Assert.IsType<OkObjectResult>(actionResult.Result);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(2, ((List<ZipCode>)result.Value!).Count);

            zipCodeRepository.Verify(r => r.GetByLocation(It.IsAny<string>()));
            zipCodeRepository.VerifyNoOtherCalls();
        }

        private static List<ZipCode> GetZipCodeTestData()
        {
            List<ZipCode> zipCodes = new();
            zipCodes.Add(new ZipCode() { ZipCodeNr = "4470", District = "Linz-Land", Location = "Enns" });
            zipCodes.Add(new ZipCode() { ZipCodeNr = "4020", District = "Linz", Location = "Linz" });
            zipCodes.Add(new ZipCode() { ZipCodeNr = "4030", District = "Linz", Location = "Linz" });
            zipCodes.Add(new ZipCode() { ZipCodeNr = "4460", District = "Linz-Land", Location = "Leonding" });
            return zipCodes;
        }
    }
}

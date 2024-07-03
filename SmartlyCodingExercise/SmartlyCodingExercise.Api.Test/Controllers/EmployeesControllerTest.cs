using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmartlyCodingExercise.Api.Configurations;
using SmartlyCodingExercise.Api.Controllers;
using SmartlyCodingExercise.Api.Models;
using SmartlyCodingExercise.Api.Models.Dtos;
using SmartlyCodingExercise.Api.Repository.Interfaces;
using SmartlyCodingExercise.Api.Test.TestData;

namespace SmartlyCodingExercise.Api.Test.Controllers
{
    [TestFixture]
    public class EmployeesControllerTest
    {
        private ILogger<EmployeesController> _logger;
        private IMapper _mockMapper;
        private Mock<IEmployeeRepository> _mockEmpRepo;
        private EmployeesController? _sut;

        [OneTimeSetUp]
        public void SetUp()
        {
            _logger = Mock.Of<ILogger<EmployeesController>>();
            _mockMapper = MappingConfig.RegisterMaps().CreateMapper();
            _mockEmpRepo = new Mock<IEmployeeRepository>();
        }

        [Test]
        public async Task GetAll_ShouldReturnAllEmployees()
        {
            // Arrange
            _mockEmpRepo.Setup(repo => repo.GetAllAsyn()).ReturnsAsync(CommonCollections.GetEmployees());
            _sut = new EmployeesController(_logger, _mockMapper, _mockEmpRepo.Object);

            // Act
            var result = await _sut.Get();

            // Assert
            var okResult = result as OkObjectResult;
            var response = okResult?.Value as List<EmployeeDto>;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(response?.Count, Is.GreaterThan(0));
            });
        }

        [Test]
        public async Task GetAll_WhenEmployeesNotFound_ShouldReturnNotFound()
        {
            // Arrange
            _mockEmpRepo.Setup(repo => repo.GetAllAsyn()).ReturnsAsync(new List<Employee>());
            _sut = new EmployeesController(_logger, _mockMapper, _mockEmpRepo.Object);

            // Act
            var result = await _sut.Get();

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(notFoundResult, Is.Not.Null);
            });
        }

        [Test]
        public async Task GetAll_WhenErrorOccured_ShouldReturnBadRequest()
        {
            // Arrange            
            _sut = new EmployeesController(_logger, _mockMapper, null);

            // Act
            var result = await _sut.Get();

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(badRequestResult, Is.Not.Null);
            });
        }
    }
}

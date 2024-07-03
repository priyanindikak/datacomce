using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmartlyCodingExercise.Api.Controllers;
using SmartlyCodingExercise.Api.Repository.Interfaces;
using SmartlyCodingExercise.Api.Test.TestData;

namespace SmartlyCodingExercise.Api.Test.Controllers
{
    [TestFixture]
    public class SalaryCalculationsControllerTest
    {
        private ILogger<SalaryCalculationsController> _logger;
        private Mock<IEmployeeRepository> _mockEmpRepo;
        private Mock<IEmployeeSalaryRepository> _mockEmpSalRepo;
        private Mock<ISalaryHelperRepository> _mockSalCalcHelperRepo;
        private SalaryCalculationsController? _sut;

        [OneTimeSetUp]
        public void SetUp()
        {
            _logger = Mock.Of<ILogger<SalaryCalculationsController>>();
            _mockEmpRepo = new Mock<IEmployeeRepository>();
            _mockEmpSalRepo = new Mock<IEmployeeSalaryRepository>();
            _mockSalCalcHelperRepo = new Mock<ISalaryHelperRepository>();
        }

        [Test]
        public async Task Get_ShouldReturn_EmployeeSalaryCalculation()
        {
            // Arrange
            _mockEmpRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(CommonCollections.GetSingleEmployee());
            _mockEmpSalRepo.Setup(repo => repo.GetByEmployeeAsync(1)).ReturnsAsync(CommonCollections.GetSingleEmployeeSalary());
            _mockSalCalcHelperRepo.Setup(repo => repo.GetTaxRatesAsync()).ReturnsAsync(CommonCollections.GetTaxRates());

            _sut = new SalaryCalculationsController(_logger, _mockEmpRepo.Object, _mockEmpSalRepo.Object, _mockSalCalcHelperRepo.Object);

            // Act
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var result = _sut.Get(1, year, month);

            // Assert
            var okResult = await result as OkObjectResult;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(okResult, Is.Not.Null);
            });
        }

        [Test]
        public async Task Get_ShouldReturn_BadRequest_WhenNoEmployeeFound()
        {
            // Arrange
            _mockEmpRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(CommonCollections.GetSingleEmployee());
            _mockEmpSalRepo.Setup(repo => repo.GetByEmployeeAsync(0)).ReturnsAsync(CommonCollections.GetSingleEmployeeSalary());

            _sut = new SalaryCalculationsController(_logger, _mockEmpRepo.Object, _mockEmpSalRepo.Object, _mockSalCalcHelperRepo.Object);

            // Act
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var result = _sut.Get(0, year, month);

            // Assert
            var badRequestResult = await result as BadRequestObjectResult;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(badRequestResult, Is.Not.Null);
            });
        }

        [Test]
        public async Task Get_ShouldReturn_NotFound_WhenNonExistingEmployeeFound()
        {
            // Arrange
            _mockEmpRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(CommonCollections.GetSingleEmployee());
            _mockEmpSalRepo.Setup(repo => repo.GetByEmployeeAsync(10)).ReturnsAsync(CommonCollections.GetSingleEmployeeSalary());

            _sut = new SalaryCalculationsController(_logger, _mockEmpRepo.Object, _mockEmpSalRepo.Object, _mockSalCalcHelperRepo.Object);

            // Act
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var result = _sut.Get(10, year, month);

            // Assert
            var notFoundResult = await result as NotFoundResult;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(notFoundResult, Is.Not.Null);
            });
        }

        [Test]
        public async Task Get_ShouldReturn_BadRequest_WhenErrorOccured()
        {
            // Arrange
            _sut = new SalaryCalculationsController(null, null, null, null);

            // Act
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var result = await _sut.Get(1, year, month);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(badRequestResult, Is.Not.Null);
            });
        }

        [Test]
        public async Task Calculate_ShouldReturn_SalaryCalculation()
        {
            // Arrange
            _mockEmpRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(CommonCollections.GetSingleEmployee());
            _mockEmpSalRepo.Setup(repo => repo.GetByEmployeeAsync(1)).ReturnsAsync(CommonCollections.GetSingleEmployeeSalary());
            _mockSalCalcHelperRepo.Setup(repo => repo.GetTaxRatesAsync()).ReturnsAsync(CommonCollections.GetTaxRates());

            _sut = new SalaryCalculationsController(_logger, _mockEmpRepo.Object, _mockEmpSalRepo.Object, _mockSalCalcHelperRepo.Object);

            // Act
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var result = _sut.Get("John", "Smith", 60050, 9, year, month);

            // Assert
            var okResult = await result as OkObjectResult;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(okResult, Is.Not.Null);
            });
        }

        [Test]
        public async Task Calculate_ShouldReturn_BadRequest_WhenErrorOccured()
        {
            // Arrange
            _sut = new SalaryCalculationsController(null, null, null, null);

            // Act
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var result = await _sut.Get("John", "Smith", 60050, 9, year, month);

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

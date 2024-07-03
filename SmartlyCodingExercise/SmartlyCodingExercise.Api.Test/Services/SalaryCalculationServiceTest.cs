using Microsoft.Extensions.Logging;
using Moq;
using SmartlyCodingExercise.Api.Common.Enums;
using SmartlyCodingExercise.Api.Repository.Interfaces;
using SmartlyCodingExercise.Api.Services;
using SmartlyCodingExercise.Api.Test.TestData;

namespace SmartlyCodingExercise.Api.Test.Services
{
    public class SalaryCalculationServiceTest
    {
        private ILogger _logger;
        private Mock<IEmployeeRepository> _mockEmpRepo;
        private Mock<IEmployeeSalaryRepository> _mockEmpSalRepo;
        private Mock<ISalaryHelperRepository> _mockSalCalcHelperRepo;
        private SalaryCalculationService? _sut;

        [OneTimeSetUp]
        public void Setup()
        {
            _logger = Mock.Of<ILogger>();
            _mockEmpRepo = new Mock<IEmployeeRepository>();
            _mockEmpSalRepo = new Mock<IEmployeeSalaryRepository>();
            _mockSalCalcHelperRepo = new Mock<ISalaryHelperRepository>();

            _mockEmpRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(CommonCollections.GetSingleEmployee());            
            _mockEmpSalRepo.Setup(repo => repo.GetByEmployeeAsync(1)).ReturnsAsync(CommonCollections.GetSingleEmployeeSalary());
            _mockSalCalcHelperRepo.Setup(repo => repo.GetTaxRatesAsync()).ReturnsAsync(CommonCollections.GetTaxRates());
        }

        [Test]
        public async Task GetEmpSalDetails_ShouldReturnObjectWithCorrectData()
        {
            // Arrange
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            var payPeriod = $"{startDate:dd MMMM} - {endDate:dd MMMM}";

            _sut = new SalaryCalculationService(_logger, _mockEmpRepo.Object, _mockEmpSalRepo.Object, _mockSalCalcHelperRepo.Object);

            // Act
            var result = await _sut.GetEmployeeSalaryDetailsByMonth(1, year, (Month)month);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result?.Id, Is.EqualTo(1));
                Assert.That(result?.FullName, Is.EqualTo("John Smith"));
                Assert.That(result?.PayPeriod, Is.EqualTo(payPeriod));
                Assert.That(result?.GrossIncome, Is.EqualTo(5004.17));
                Assert.That(result?.IncomeTax, Is.EqualTo(919.58));
                Assert.That(result?.NetIncome, Is.EqualTo(4084.59));
                Assert.That(result?.SuperAmount, Is.EqualTo(450.38));
            });
        }

        [Test]
        public async Task GetEmpSalDetails_ShouldReturnEmptyObject_WhenInvalidEmployee()
        {
            // Arrange
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            _sut = new SalaryCalculationService(_logger, _mockEmpRepo.Object, _mockEmpSalRepo.Object, _mockSalCalcHelperRepo.Object);

            // Act
            var result = await _sut.GetEmployeeSalaryDetailsByMonth(-1, year, (Month)month);

            // Assert
            Assert.That(result?.Id, Is.EqualTo(0));
        }

        [Test]
        public void GetEmpSalDetails_ShouldReturnException_WhenErrorOccured()
        {
            // Arrange
            _sut = new SalaryCalculationService(null, null, null, null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NullReferenceException>(async () => await _sut.GetEmployeeSalaryDetailsByMonth(1, 2000, Month.February));
            Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

        [Test]
        public async Task GetEmployeeSalaryCalculation_ShouldReturnObjectWithCorrectData()
        {
            // Arrange
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            var payPeriod = $"{startDate:dd MMMM} - {endDate:dd MMMM}";

            _sut = new SalaryCalculationService(_logger, _mockEmpRepo.Object, _mockEmpSalRepo.Object, _mockSalCalcHelperRepo.Object);

            // Act
            var result = await _sut.GetEmployeeSalaryCalculation("John", "Smith", 60050, 9, year, (Month)month);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result?.FullName, Is.EqualTo("John Smith"));
                Assert.That(result?.PayPeriod, Is.EqualTo(payPeriod));
                Assert.That(result?.GrossIncome, Is.EqualTo(5004.17));
                Assert.That(result?.IncomeTax, Is.EqualTo(919.58));
                Assert.That(result?.NetIncome, Is.EqualTo(4084.59));
                Assert.That(result?.SuperAmount, Is.EqualTo(450.38));
            });
        }

        [Test]
        public void GetEmployeeSalaryCalculation_ShouldReturnException_WhenErrorOccured()
        {
            // Arrange
            _sut = new SalaryCalculationService(null, null, null, null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NullReferenceException>(async () => 
                await _sut.GetEmployeeSalaryCalculation("John", "Smith", 60050, 9, 2000, Month.February));
            Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

    }
}

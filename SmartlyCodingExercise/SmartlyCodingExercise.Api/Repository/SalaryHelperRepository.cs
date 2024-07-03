using Microsoft.EntityFrameworkCore;
using SmartlyCodingExercise.Api.Data;
using SmartlyCodingExercise.Api.Models;
using SmartlyCodingExercise.Api.Repository.Interfaces;

namespace SmartlyCodingExercise.Api.Repository
{
    public class SalaryHelperRepository(
        ILogger<SalaryHelperRepository> logger, 
        SCEDbContext context) : ISalaryHelperRepository
    {
        private readonly ILogger<SalaryHelperRepository> _logger = logger;
        private readonly SCEDbContext _context = context;

        public async Task<IList<TaxRate>> GetTaxRatesAsync()
        {
            try
            {
                return CommonConstantsHelpers.InMemoryDataMode
                    ? IMData.GetTaxRates()
                    : await _context.TaxRates.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}

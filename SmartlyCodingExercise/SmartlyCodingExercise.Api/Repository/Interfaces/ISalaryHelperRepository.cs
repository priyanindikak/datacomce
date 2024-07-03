using SmartlyCodingExercise.Api.Models;

namespace SmartlyCodingExercise.Api.Repository.Interfaces
{
    public interface ISalaryHelperRepository
    {
        Task<IList<TaxRate>> GetTaxRatesAsync();
    }
}

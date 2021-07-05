using ICM.Taxes.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICM.Taxes.Infrastructure.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxCalculator _taxCalculator;

        public TaxService(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        public async Task<dynamic> GetRates(string zipCode, string optQS)
        {
            return await _taxCalculator.GetRates(zipCode, optQS);
        }

        public async Task<dynamic> TaxCalculation(dynamic order)
        {
            return await _taxCalculator.CalcTaxes(order);
        }
    }
}

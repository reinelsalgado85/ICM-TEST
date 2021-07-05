using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICM.Taxes.Core.Interfaces
{
    public interface ITaxCalculator
    {
        public Task<dynamic> GetRates(string zipCode, string optQS);
        public Task<dynamic> CalcTaxes(dynamic payLoad);
    }
}

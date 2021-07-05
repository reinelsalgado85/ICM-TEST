using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICM.Taxes.Services
{
    public interface ITaxCalculator
    {
        public Task<dynamic> GetRates();


    }
}

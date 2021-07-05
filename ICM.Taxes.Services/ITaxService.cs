using System;
using System.Threading.Tasks;

namespace ICM.Taxes.Services
{
    public interface ITaxService
    {

        public Task<dynamic> GetRates();



    }
}

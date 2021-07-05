//using ICM.Taxes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICM.Taxes.Core.Interfaces;
using my_icm.Helpers;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using my_icm.ViewModel;

namespace my_icm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenController : ControllerBase
    {
        private readonly ITaxService _taxService;
        public OpenController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpPost("taxes")]
        public async Task<IActionResult> TaxCalculation([FromBody] OrderVM obj)
        {
            var validationMessages = obj.ValidationErrors();
            if (validationMessages.Any())
            {
                throw new ArgumentException("There is something wrong with the data", nameof(obj));
            }
            var result = await _taxService.TaxCalculation(obj);
            return Ok(result);
        }
        [HttpGet("rates/{zipCode}")]
        public async Task<IActionResult> First(string zipCode, [FromQuery] RatesOptParamsVM obj)
        {
            var qs = OneHelper.ObjToQueryStirng(obj);
            var result = await _taxService.GetRates(zipCode, qs);
            return Ok(result);
        }


    }
}

using System.Collections.Generic;
using CalculationEngine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CalculationEngine.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CalculationController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Please enter your input string after the URL";
        }

        // GET api/<CalculationController>/5
        [HttpGet("{input}")]
        public double Get(string input)
        {
            char[] operators = { 'd', 'm', 'a', 's' };

            Calculation calc = new Calculation();

            calc.NumbersAndOperatorsList = new List<string>(Regex.Split(input, @"([^\d\.]+)"));

            return CalculationService.Evaluate(calc, operators);

        }

    }
}

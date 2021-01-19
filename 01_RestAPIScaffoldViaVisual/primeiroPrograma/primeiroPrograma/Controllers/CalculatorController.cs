﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace primeiroPrograma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        //Parametros da rota
        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                //Soma
                var sum = ConvertInt(firstNumber) + ConvertInt(secondNumber);
                //Subtração
                //var sub = ConvertInt(firstNumber) - ConvertInt(secondNumber);
                //Multiplicação
                //var mult = ConvertInt(firstNumber) * ConvertInt(secondNumber);
                //Div
                //var div = ConvertInt(firstNumber) / ConvertInt(secondNumber);
                //Media
                //var med = (ConvertInt(firstNumber))/ 2;
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }
        private bool IsNumeric(string number)
        {
            double n;
            //Convertendo e parseando em numerico
            bool isNumber =double.TryParse(number, 
                System.Globalization.NumberStyles.Any, 
                System.Globalization.NumberFormatInfo.InvariantInfo, 
                out n);
            return isNumber;
         //   return number = false;
        }
        private decimal ConvertInt(string Valor)
        {
            //var convert = Convert.ToInt32(Valor);
            decimal decimalValue;
            if(decimal.TryParse(Valor, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }
    }
}

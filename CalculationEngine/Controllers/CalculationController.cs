using System.Collections.Generic;
using CalculationEngine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Data;
using System;
using System.Linq;

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
            char[] operators = { '/', '*', '+', '-' };

            Calculation calc = new Calculation();

            calc.NumbersAndOperatorsList = new List<string>(Regex.Split(input, @"([^\d\.]+)"));

            foreach (char op in operators)
            {
                for (int i = 0; i < calc.NumbersAndOperatorsList.Count - 2; i += 2)
                {
                    if (!string.IsNullOrEmpty(calc.NumbersAndOperatorsList[i]) && !string.IsNullOrEmpty(calc.NumbersAndOperatorsList[i + 1]) && !string.IsNullOrEmpty(calc.NumbersAndOperatorsList[i + 2]))
                    {
                        calc.Operator = Convert.ToChar(calc.NumbersAndOperatorsList[i + 1]);
                        if (!operators.Contains(calc.Operator))
                            throw new ArithmeticException("Input expression is not valid - The operator is not in the required format of 'd', 'm', 'a', or 's'");
                        if (op == calc.Operator)
                        {
                            calc.LeftOperand = Convert.ToDouble(calc.NumbersAndOperatorsList[i]);
                            calc.RightOperand = Convert.ToDouble(calc.NumbersAndOperatorsList[i + 2]);
                            calc.NumbersAndOperatorsList.RemoveAt(i + 1);
                            calc.NumbersAndOperatorsList.RemoveAt(i + 1);
                            DataTable dt = new DataTable();
                            switch (calc.Operator)
                            {

                                case '/':
                                    calc.Result = Convert.ToDouble(dt.Compute(calc.LeftOperand.ToString() + '/' + calc.RightOperand.ToString(), ""));
                                    break;
                                case '*':
                                    calc.Result = Convert.ToDouble(dt.Compute(calc.LeftOperand.ToString() + '*' + calc.RightOperand.ToString(), ""));
                                    break;
                                case '+':
                                    calc.Result = Convert.ToDouble(dt.Compute(calc.LeftOperand.ToString() + '+' + calc.RightOperand.ToString(), ""));
                                    break;
                                case '-':
                                    calc.Result = Convert.ToDouble(dt.Compute(calc.LeftOperand.ToString() + '-' + calc.RightOperand.ToString(), ""));
                                    break;
                            }
                            calc.NumbersAndOperatorsList.RemoveAt(i);
                            calc.NumbersAndOperatorsList.Insert(i, calc.Result.ToString());
                        }
                    }
                    else
                        throw new ArithmeticException("Input expression is not valid - The operand is not valid");
                }
            }
            return Convert.ToDouble(calc.NumbersAndOperatorsList[0]);

        }

    }
}

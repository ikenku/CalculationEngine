using System;
using System.Data;
using System.Linq;


namespace CalculationEngine.Models
{
    public class CalculationService
    {
        public static double Evaluate(Calculation calc, char[] operators)
        {
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

                                case 'd':
                                    calc.Result = Convert.ToDouble(dt.Compute(calc.LeftOperand.ToString() + '/' + calc.RightOperand.ToString(), ""));
                                    break;
                                case 'm':
                                    calc.Result = Convert.ToDouble(dt.Compute(calc.LeftOperand.ToString() + '*' + calc.RightOperand.ToString(), ""));
                                    break;
                                case 'a':
                                    calc.Result = Convert.ToDouble(dt.Compute(calc.LeftOperand.ToString() + '+' + calc.RightOperand.ToString(), ""));
                                    break;
                                case 's':
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
            if (calc.NumbersAndOperatorsList.Count > 1)
            {
                return Evaluate(calc, operators);
            }
            else
                return Convert.ToDouble(calc.NumbersAndOperatorsList[0]);
        }
    }
}

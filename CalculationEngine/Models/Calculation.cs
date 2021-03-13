using System.Collections.Generic;

namespace CalculationEngine.Models
{
    public class Calculation
    {
        public double Result { get; set; }
        public char Operator { get; set; }
        public double LeftOperand { get; set; }
        public double RightOperand { get; set; }
        public List<string> NumbersAndOperatorsList { get; set; }
    }
}

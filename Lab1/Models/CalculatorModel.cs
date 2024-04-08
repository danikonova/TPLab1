using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class CalculatorModel
    {
        [Required(ErrorMessage = "Поле Operand1 обязательно для заполнения.")]
        public sbyte Operand1 { get; set; }

        [Range(-100, 100, ErrorMessage = "Operand2 должен быть в диапазоне от -100 до 100.")]
        public sbyte Operand2 { get; set; }
        public string Operation { get; set; }
		public decimal Result { get; set; }
        public string Action { get; set; }
    }
}

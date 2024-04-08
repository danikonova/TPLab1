using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Lab1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new CalculatorModel());
        }

        [HttpPost]
        public ActionResult Calculate(CalculatorModel model)
        {
            ViewBag.TargetValue = 20;

            // Проверяем модель на валидность
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            if (model.Action == "Clear") // Проверка значения атрибута кнопки value
            {
                model.Operand1 = 0;
                model.Operand2 = 0;
                model.Operation = "+";
                model.Result = 0;
                ModelState.Clear();
                return View("Index", model);
            }

            switch (model.Operation)
            {
                case "+":
                    model.Result = model.Operand1 + model.Operand2;
                    break;
                case "-":
                    model.Result = model.Operand1 - model.Operand2;
                    break;
                case "*":
                    model.Result = model.Operand1 * model.Operand2;
                    break;
                case "/":
                    if (model.Operand2 != 0)
                    {
                        model.Result = model.Operand1 / model.Operand2;
                    }
                    else
                    {
                        ModelState.AddModelError("Operand2", "Division by zero is not allowed");
                    }
                    break;
                default:
                    ModelState.AddModelError("Operation", "Invalid operation");
                    break;
            }

            return View("Index", model);
        }

        public string GetOperationWord(string operation)
        {
            {
                int indexOfOperation = operation.LastIndexOfAny(new char[] { '+', '-', '*', '/' });
                if (indexOfOperation != -1)
                {
                    string operationSign = operation.Substring(indexOfOperation, 1);
                    string operationWord;

                    switch (operationSign)
                    {
                        case "+":
                            operationWord = "плюс";
                            break;
                        case "-":
                            operationWord = "минус";
                            break;
                        case "*":
                            operationWord = "умножить на";
                            break;
                        case "/":
                            operationWord = "разделить на";
                            break;
                        default:
                            operationWord = "некорректная операция";
                            break;
                    }

                    return operationWord;
                }
                else
                {
                    return "некорректная операция";
                }
            }
        }
		public ActionResult Resuult(sbyte operand1, sbyte operand2, string operation, decimal result)
        {
            var model = new CalculatorModel
            {
                Operand1 = operand1,
                Operand2 = operand2,
                Operation = operation,
                Result = result
            };
            // Подготовка параметров запроса для дублирования информации
            string queryString = $"operand1={operand1}&operand2={operand2}&operation={operation}&result={result}";

            
            return Redirect("/Home/Result?" + queryString);
        }
        public ActionResult Result(string operand1, string operand2, string operation, string result)
        {
            var model = new CalculatorModel
            {
                Operand1 = Convert.ToSByte(operand1),
                Operand2 = Convert.ToSByte(operand2),
                Operation = operation,
                Result = Convert.ToDecimal(result)
            };

            return View(model);
        }
    }
}
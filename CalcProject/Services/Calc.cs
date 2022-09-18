using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject.Services
{
    /*
     * Головний клас - запуск програми
     */
    public class Calc
    {
        private readonly Resources Resources;   // Dependency

        public Calc()
        {
        }

        public Calc(Resources resources)
        {
            Resources = resources;      
        }

        public RomanNumber EvalExpression(string expression)
        {
            RomanNumber res = null!;
            string[] parts_input = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);        // Разбиение строки на части 

            if (parts_input.Length != 3)                                                                // Проверка на кол-ч введеных операций + чисел  
            {
                throw new ArgumentException(Resources.GetInvalidInputExression());                      // При не выполнение условия выброс исключения
            }

            if (!(RomanNumber.Operations.ContainsKey(parts_input[1])))                                  // Проверка на допустимость введеный операции
            {                                                                                           // с доступными в RomanNumber
                throw new ArgumentException(Resources.GetInvalidInputExression());
            }

            RomanNumber rn1 = new(RomanNumber.Parse(parts_input[0]));                                   // Создание объектов с частей массива строки
            RomanNumber rn2 = new(RomanNumber.Parse(parts_input[2]));
            //parts_input[1] == RomanNumber.Operations[0]
            //? rn1.Add(rn2)
            //: rn1.Sub(rn2);

            res = (RomanNumber)RomanNumber.Operations[parts_input[1]].DynamicInvoke(rn1, rn2);          // Получения результата с помощью использования Dictionary.
                                                                                                        // В нем хранится операция и делегат на соответствующий метод класса. 
            return res;                                                                                 // С помощью DynamicInvoke вызываем методы по операции и возвращаем результат.
        }
        /// <summary>
        /// Старая версия запуска калькулятора 
        /// </summary>
        public void Run_Old()
        {

            string? str;
            RomanNumber? rn1 = null;
            RomanNumber? rn2 = null;
            do
            {
                Console.WriteLine(Resources.GetEnterNumberMessage());
                str = Console.ReadLine();
                try
                {
                    rn1 = new RomanNumber(RomanNumber.Parse(str!));
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("System error. Program terminated");
                    return;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            while (rn1 == null);

            do
            {
                Console.WriteLine(Resources.GetEnterNumberMessage());
                str = Console.ReadLine();
                try
                {
                    rn2 = new RomanNumber(RomanNumber.Parse(str!));
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("System error. Program terminated");
                    return;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            while (rn2 == null);
            Console.WriteLine($"{rn1} + {rn2} = {rn1.Add(rn2)}");
        }
        /// <summary>
        /// Обновленная версия запуска калькулятора (с применением рефакторинга) 
        /// </summary>
        public void Run()
        {
            string? userinput;
            RomanNumber res = null!;
            do
            {
                Console.Write(Resources.GetInputOperation());   // Вызов метода ввода из Ресурсов
                userinput = Console.ReadLine() ?? "";
                try                                             // Потенциально место выброса исключения
                {
                    res = EvalExpression(userinput);            // Метод основной логики калькулятора
                }
                catch (ArgumentException ex)                    // Отлов и обработка возможных исключений
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (res is null);                                
            Console.WriteLine(Resources.GetResultMessage(userinput, res.ToString()));   // Вывод результат с помощью Ресурсов
        }
    }
}
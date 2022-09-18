using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcProject.Services
{
    public class Resources
    {
        private readonly static Dictionary<int, string> ListOfCulture = new Dictionary<int, string>() { [1] = "uk-UA", [2] = "en-US" };   // Добавление словаря с допустимыми в программе языками интерфейса   
        private string Culture { get; set; } = ListOfCulture[2];    // по умолчанию 
        public void SetCulture()        // Метод для ввода пользователем языка системы
        {
            int input;
            do
            {
                Console.Write("Enter language in system (1-Ukrainian 2-English): ");
                input = int.Parse(Console.ReadLine().Replace(" ", ""));     // Уборка пробелов и преобразование 
                try
                {
                    Culture = GetInputCulture(input);       // Проверка на исключения, если такого языка нет в словаре
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } //while (!(input=="1"||input=="2"));
            while (!ListOfCulture.ContainsKey(input));      // Если выбрано допустимое значение
            Console.Clear();
        }
        public string GetInputCulture(int key)      // Проверка на содержание языка в словаре 
        {
            /*return input switch
            //{
            //    "1" => "uk-UA",
            //    "2" => "en-US",
            //    _ => throw new Exception("Unupported culture")
            };
            */
            return ListOfCulture.ContainsKey(key) ? ListOfCulture[key] : throw new Exception(GetUnsupportedCultureMessage());   // Возврат значения языка, или выброс исключения
        }
        public string GetUnsupportedCultureMessage(string? culture = null)      // Не допустимый язык в системе 
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => $"Непідтримувана культура",
                "en-US" => $"Unsupported culture",
                _ => throw new Exception("Wrong system options!")
            };
        }
        public string GetEmptyStringMessage(string? culture = null)             // Сообщение об пустой строке  
        {
            if (culture == null) culture = Culture;
            switch (culture)
            {
                case "uk-UA": return "Порожній рядок неприпустимий";
                case "en-US": return "Empty string not allowed";
            }
            throw new Exception(GetUnsupportedCultureMessage());
        }

        public string GetInvalidCharMessage(char c, string? culture = null)     // Сообщение об не допустимом символе 
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => $"Недозволений символ '{c}'",
                "en-US" => $"Invalid char '{c}'",
                _ => throw new Exception(GetUnsupportedCultureMessage())
            };
        }
        public string GetInvalidTypeMessage(string type, string? culture = null)     // Сообщение об не поддерживаем типе данных
        {
            culture = culture ?? Culture;
            return culture switch
            {
                "uk-UA" => $"obj: тип '{type}' не підтримується",
                "en-US" => $"obj: type '{type}' unsupported",
                _ => throw new Exception(GetUnsupportedCultureMessage())
            };
        }
        public string GetOnlyOne_N_Exception(string? culture = null)     // Сообщение об возможности только одного 'N'
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Недозволенний символ, тільки одна 'N'",
                "en-US" => "Invalid number, only one 'N'",
                _ => throw new Exception(GetUnsupportedCultureMessage())
            };
        }
        public string GetEnterNumberMessage(string? culture = null)     // Сообщение пользователю об вводе числа 
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Введiть число: ",
                "en-US" => "Enter number: ",
                _ => throw new Exception(GetUnsupportedCultureMessage()),
            };
        }
        // Enter operation
        public string GetEnterOperationMessage(string? culture = null)     // Сообщение об том что такой операции нет в калькуляторе 
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Такої операцiї не існує: ",
                "en-US" => "Enter operation: ",
                _ => throw new Exception(GetUnsupportedCultureMessage()),
            };
        }
        // Result
        public string GetResultMessage(string userInput, string res, string? culture = null)     // Сообщение пользователю об результате
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => $"Результат ({userInput}) : {res}",
                "en-US" => $"Result ({userInput}) : {res}",
                _ => throw new Exception(GetUnsupportedCultureMessage()),
            };
        }
        public string GetInputOperation(string? culture = null)     // Сообщение пользователю про ввод операции
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Введіть вираз (Х + Х):",
                "en-US" => "Enter expression (X + X): ",
                _ => throw new Exception(GetUnsupportedCultureMessage()),
            };
        }
        public string GetInvalidInputExression(string? culture = null)     // Сообщение об не допустимом выражении
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Не допустимий вираз",
                "en-US" => "Invalid expression",
                _ => throw new Exception(GetUnsupportedCultureMessage()),
            };
        }

    }
}
/* Створити ресурси для UI (консолі):
 * Введіть число / Enter number
 * Введіть операцію / Enter operation
 * Результат: / Result:
 */

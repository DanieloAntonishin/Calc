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
        public string Culture { get; set; } = "en-US";  // Язык по-умолчание в системе   

        public string GetEmptyStringMessage(string? culture = null)  // Исключения на пустоту строки 
        {
            if (culture == null) culture = Culture;
            switch (culture)                                          //  switch для проверки выбраного языка 
            {
                case "uk-UA": return "Порожній рядок неприпустимий"; 
                case "en-US": return "Empty string not allowed";
            }
            throw new Exception("Unupported culture");
        }

        public string GetInvalidCharMessage(char c, string? culture = null)  // Исключения на не допустимого символа 
        {
            culture ??= Culture; 
            return culture switch                                            // Новый вид switch 
            {
                "uk-UA" => $"Недозволений символ '{c}'",
                "en-US" => $"Invalid char '{c}'",
                _ => throw new Exception("Unupported culture")
            };
        }
        public string GetInvalidTypeMessage(int objNumber, string type, string? culture = null)  // Исключения на не поддерживаемый тип данных
        {
            culture = culture ?? Culture;
            return culture switch
            {
                "uk-UA" => $"obj{objNumber}: тип '{type}' не підтримується",
                "en-US" => $"obj{objNumber}: type '{type}' unsupported",
                _ => throw new Exception("Unupported culture")
            };
        }
        public string GetOnlyOne_N_Exception(string? culture = null)    // Исключения на попадание больше одного "N"
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Недозволенний символ, тільки одна 'N'",
                "en-US" => "Invalid number, only one 'N'",
                _ => throw new Exception("Unsupported culture")
            };
        }
        public string GetEnterNumberMessage(string? culture = null)     // Сообщения при работе с консолью пользователю UI
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Введiть число: ",
                "en-US" => "Enter number: ",
                _ => throw new Exception("Unsupported culture"),
            };
        }
        // Enter operation
        public string GetEnterOperationMessage(string? culture = null)      // Сообщения на ввода операцию в консоль
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Введiть операцiю: ",
                "en-US" => "Enter operation: ",
                _ => throw new Exception("Unsupported culture"),
            };
        }
        // Result
        public string GetResultMessage(int res, string? culture = null)      // Сообщения на получения результата в консоль
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => $"Результат: {res}",
                "en-US" => $"Result: {res}",
                _ => throw new Exception("Unsupported culture"),
            };
        }

    }
}
/* Створити ресурси для UI (консолі):
 * Введіть число / Enter number
 * Введіть операцію / Enter operation
 * Результат: / Result:
 */

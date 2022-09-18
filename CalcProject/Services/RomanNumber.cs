using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace CalcProject.Services
{
    public record RomanNumber
    {
        public int romanNumber { set; get; }
        public static Resources Resources { get; set; } = null!;

        public readonly static Dictionary<string, Delegate> Operations = new Dictionary<string, Delegate>()     // Словарь из ключа операции и вызова метода, с помощью делегата
        {       
            ["+"] = new Func<object, object, RomanNumber>(Add),     // Добавление в одном месте операции и значения равной методу 
            ["-"] = new Func<object, object, RomanNumber>(Sub),     // Рефакторинг класса по добавлению операции 
            ["*"] = new Func<object, object, RomanNumber>(Mul),     
            ["/"] = new Func<object, object, RomanNumber>(Div)
        };

        public RomanNumber(int num = 0) => romanNumber = num;       // Коснструктор с арабским числом
        private RomanNumber(object obj)
        {
            if (obj is null) throw new ArgumentNullException($"obj is null");  // проверка и исключения для отлова в тестах 

            if (obj is int val) romanNumber = val;                                 // int
            else if (obj is String str && str.Length > 0) romanNumber = Parse(str);  // string
            else if (obj is RomanNumber rn) romanNumber = rn.romanNumber;                                       // RomanNumber
            else throw new ArgumentException(Resources.GetInvalidTypeMessage(obj.GetType().Name));                     // else вызов исключения 
        }
        /// <summary>
        /// перевод из арабских чисел в римские
        /// </summary>
        /// <returns>Строку</returns>
        public override string ToString()
        {
            if (this.romanNumber == 0) { return "N"; };                                 // При 0 возврат сразу N
            int n = this.romanNumber < 0 ? -this.romanNumber : this.romanNumber;        // Проверка на меньше 0
            string res = this.romanNumber < 0 ? "-" : "";
            string[] parts = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] val = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            for (int i = 0; i < parts.Length; i++)      // Формируем результат 
            {
                while (n >= val[i])
                {
                    n -= val[i];
                    res += parts[i];
                }
            }
            return res;
        }
        /// <summary>
        /// Получения числа с строки с римскими числами
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Челочисленное число или исключения</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int Parse(string str)
        {
            // До рефакторинга
            //if (str is null) { throw new ArgumentNullException();}

            //if (str == "N") { return 0; }

            // После

            switch (str)  // Пред проверки на исключения
            {
                case null:
                    throw new ArgumentNullException();
                case "N":
                    return 0;
            }

            bool isNegative = false;  // переменная флаг, для проверки на '-'
            if (str.StartsWith('-'))
            {
                isNegative = true;
                str = str[1..];  // Переход на 1 позицию и приравнивание
            }

            if (str.Length == 0) { throw new ArgumentException(Resources.GetEmptyStringMessage()); }//когда пустая строка генерируем исключения

            char[] digits = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            int[] digitValues = { 1, 5, 10, 50, 100, 500, 1000 };
            // Якщо наступна цифра числа більша за поточну, то
            // вона віднімається від результату, інакше додається
            // IX : -1 + 10;  XC : -10 + 100;  XX : +10+10; CX : +100+10
            int res = 0;
            int lastnumb = 0;
            int ind = 0;
            int val = 0;
            //До рефакторинга

            //for (int i = str.Length - 1; i >= 0; i--)
            //{
            //    char digit = str[i];

            //    ind = Array.IndexOf(digits, digit);

            //    if (str.Contains('n') && lastnumb == 0)  // проверка на наличие более одного 'n'
            //    {
            //        throw new ArgumentException(Resources.GetOnlyOne_N_Exception());
            //    }
            //    if (ind == -1)
            //    {
            //        throw new ArgumentException(Resources.GetInvalidCharMessage(str[i]));
            //    }

            //    val = digitValues[ind];
            //    if (val < lastnumb) { res -= val; }
            //    else { res += val; }

            //    lastnumb = val;
            //}


            // После рефакторинга
            // Переменные счетчики

            for (int i = str.Length - 1; i >= 0; i--)
            {
                ind = Array.IndexOf(digits, str[i]);

                if (str.Contains('N') && lastnumb == 0)  // Проверка на наличие более одного 'N'
                {
                    throw new ArgumentException(Resources.GetOnlyOne_N_Exception());
                }

                if (ind == -1)  // Генерация исключения при не известном символе
                {
                    throw new ArgumentException(Resources.GetInvalidCharMessage(str[i]));
                }

                val = digitValues[ind];                          // получения в арабского числа
                res = val < lastnumb ? res -= val : res += val;  // проверка и получения результат
                lastnumb = val;                                  // Запоминаем предидущие число
            }

            return isNegative ? -res : res;  // добавляем "-" в начало, если флаг истина
        }
        /// <summary>
        /// Операция сложения чисел
        /// </summary>
        /// <param name="rn"></param>
        /// <returns>Римское число</returns>
        /// <exception cref="ArgumentNullException"></exception>

        // Динамические

        // До рефакторинга

        /*public RomanNumber Add(RomanNumber rn)
        //{
        //    if (rn is null) { throw new ArgumentNullException(nameof(rn));}
        //    return new(this.romanNumber + rn.romanNumber);
        //    //return this with {romanNumber=this.romanNumber + rn.romanNumber };
        //}
        //public RomanNumber Add(int val)
        //{
        //    return new(this.romanNumber + val);
        //}
        //public RomanNumber Add(string str)
        //{
        //    return new(this.romanNumber +RomanNumber.Parse(str));
        */


        // После рефакторинга 

        public RomanNumber Add(RomanNumber rn2)
        {
            if (rn2 is null) { throw new ArgumentNullException(nameof(rn2)); }
            return new(this.romanNumber + rn2.romanNumber);
            //return this with {romanNumber=this.romanNumber + rn.romanNumber };
        }
        public RomanNumber Sub(RomanNumber rn2)
        {
            if (rn2 == null) throw new ArgumentNullException(nameof(rn2));
            return new(this.romanNumber - rn2.romanNumber);
            //return this.Add(rn2 with { romanNumber=-rn2.romanNumber });
        }
        public RomanNumber Mul(RomanNumber rn2)
        {
            if (rn2 == null) throw new ArgumentNullException(nameof(rn2));
            return new(this.romanNumber * rn2.romanNumber);
            //return this.Add(rn2 with { romanNumber=-rn2.romanNumber });
        }
        public RomanNumber Div(RomanNumber rn2)
        {
            if (rn2 == null) throw new ArgumentNullException(nameof(rn2));
            if (rn2.romanNumber == 0) throw new ArgumentException(nameof(rn2));
            if (this.romanNumber % rn2.romanNumber != 0) throw new ArgumentException(nameof(rn2));

            return new(this.romanNumber / rn2.romanNumber);
            //return this.Add(rn2 with { romanNumber=-rn2.romanNumber });
        }

        public RomanNumber Add(int val)
        {
            // Вместо дуюлирования алгоритма мы создаем объект из 
            // "сырых" данных и делигирует другому методу 
            return this.Add(new RomanNumber(val));
        }
        public RomanNumber Add(string str)
        {
            return this.Add(new RomanNumber(Parse(str)));
        }

        // Тесты для статиков 

        // До рефакторинга
        /*public static RomanNumber Add(RomanNumber obj1, RomanNumber obj2)
        //{
        //    if (obj1 is null || obj2 is null) { throw new ArgumentNullException(nameof(RomanNumber)); }
        //    return new(obj1.romanNumber + obj2.romanNumber);
        //}
        //public static RomanNumber Add(int obj1, int obj2)
        //{
        //    return new(obj1 + obj2);
        //}
        //public static RomanNumber Add(string obj1, string obj2)
        //{
        //    if (obj1 is null || obj2 is null) { throw new ArgumentNullException(nameof(RomanNumber)); }

        //    if (obj1.Length == 0 || obj2.Length == 0)
        //    {
        //        throw new ArgumentException(Resources.GetEmptyStringException());
        //    }
        //    return new(Parse(obj1) + Parse(obj2));
        //}
        //public static RomanNumber Add(RomanNumber obj1, string obj2)
        //{
        //    if (obj1 is null) { throw new ArgumentNullException(nameof(obj1)); }

        //    if (obj2.Length == 0)
        //    {
        //        throw new ArgumentException(Resources.GetEmptyStringException());
        //    }
        //    return new(obj1.romanNumber + Parse(obj2));
        //}
        //public static RomanNumber Add(RomanNumber obj1, int obj2)
        //{
        //    if (obj1 is null) { throw new ArgumentNullException(nameof(obj1)); }
        //    return new(obj1.romanNumber + obj2);
        //}
        */

        // После рефакторинга 
        //public static RomanNumber Add(RomanNumber obj1, RomanNumber obj2)
        //{
        //    if (obj1 is null || obj2 is null)
        //    { throw new ArgumentNullException(nameof(RomanNumber)); }
        //    return new RomanNumber(obj1.romanNumber).Add(obj2.romanNumber);
        //}
        //public static RomanNumber Add(int obj1, int obj2)
        //{
        //    return new RomanNumber(obj1).Add(obj2);
        //}
        //public static RomanNumber Add(string obj1, string obj2)
        //{
        //    // До рефакторинга
        //    /*if (obj1 is null || obj2 is null)
        //    //{ 
        //    //    throw new ArgumentNullException( 
        //            obj1 is null ?nameof(obj1):nameof(obj2)); 
        //    }*/

        //    // После 
        //    if (obj1 is null) { throw new ArgumentNullException(nameof(obj1)); }
        //    if (obj2 is null) { throw new ArgumentNullException(nameof(obj2)); }


        //    if (obj1.Length == 0 || obj2.Length == 0)
        //    {
        //        throw new ArgumentException(Resources.GetEmptyStringException());
        //    }
        //    return new RomanNumber(Parse(obj1)).Add(Parse(obj2));
        //}
        //public static RomanNumber Add(RomanNumber obj1, string obj2)
        //{
        //    if (obj1 is null) { throw new ArgumentNullException(nameof(obj1)); }

        //    if (obj2.Length == 0)
        //    {
        //        throw new ArgumentException(Resources.GetEmptyStringException());
        //    }
        //    return new RomanNumber(obj1.romanNumber).Add(Parse(obj2));
        //}
        //public static RomanNumber Add(RomanNumber obj1, int obj2)
        //{
        //    if (obj1 is null) { throw new ArgumentNullException(nameof(obj1)); }
        //    return new RomanNumber(obj1.romanNumber).Add(obj2);
        //}

        /// <summary>
        /// Calculate sum of obj1 and obj2
        /// Where rn's  types are: int, String, RomanNumber
        /// </summary>
        /// <param name="obj1">int, String, RomanNumber</param>
        /// <param name="obj2">int, String, RomanNumber</param>
        /// <returns>RomanNumber</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static RomanNumber Add_Old(object obj1, object obj2)
        {
            /* Рефакторинг - разделение условий (условия внутри условия)
            if (obj1 is null || obj2 is null)
            {
                throw new ArgumentNullException(
                    obj1 is null ? nameof(obj1) : nameof(obj2)) ;
            }*/


            // Рефакторинг - соединение(перераспределение) условий
            // if (obj1 is int && obj2 is int) return new RomanNumber((int)obj1).Add((int)obj2);
            //else if (obj1 is String && obj2 is String) return new RomanNumber(RomanNumber.Parse((String)obj1)).Add((String)obj2);
            //else if (obj1 is int && obj2 is String) return new RomanNumber((int)obj1).Add((String)obj2);
            //else if (obj1 is String && obj2 is int) return new RomanNumber((int)obj2).Add((String)obj1);

            // (obj1 is int && obj2 is int) + (obj1 is int && obj2 is String)-- >
            //(obj1 is int)(obj2 is int + obj2 is String)


            // До полного рефакторинга 

            /*if (obj1 is int v1)
            //{
            //    // Рефакторинг - если код присутствует во всех блоках, его нужно вынести
            //    //if(obj2 is int v2) return new RomanNumber(v1).Add(v2);
            //    //if(obj2 is String s2) return new RomanNumber(v1).Add(s2);
            //    
            //    var rn = new RomanNumber(v1);
            //    if (obj2 is int v2) return rn.Add(v2);
            //    if (obj2 is String s2) return rn.Add(s2);
            //}
            //if (obj1 is String s1)
            //{
            //    var rn = new RomanNumber(Parse(s1));
            //    if (obj2 is int v2) return rn.Add(v2);
            //    if (obj2 is String s2) return rn.Add(s2);
            //}
            //if (obj1 is RomanNumber r1 && obj2 is RomanNumber r2)
            //{
            //    return r1.Add(r2);
            //}
            return new RomanNumber(); */


            // Макисмальный рефакторинг 

            var rns = new RomanNumber[] { null!, null! };
            var pars = new object[] { obj1, obj2 };

            for (int i = 0; i < 2; i++)
            {
                if (pars[i] is null) throw new ArgumentNullException($"obj{i + 1}");  // проверка и исключения для отлова в тестах 

                if (pars[i] is int val) rns[i] = new RomanNumber(val);                                 // int
                else if (pars[i] is String str && str.Length > 0) rns[i] = new RomanNumber(Parse(str));  // string
                else if (pars[i] is RomanNumber rn) rns[i] = rn;                                       // RomanNumber
                else throw new ArgumentException(Resources.GetInvalidTypeMessage(pars[i].GetType().Name));                     // else вызов исключения 
            }

            return rns[0].Add(rns[1]);  // результат 
        }
        // Методы статик с рефакторингом
        public static RomanNumber Add(object obj1, object obj2)
        {
            var rn1 = (obj1 is RomanNumber r) ? r : new RomanNumber(obj1);      // Проверки на соответсвию на тип или присваивания object
            var rn2 = (obj2 is RomanNumber r2) ? r2 : new RomanNumber(obj2);
            return rn1.Add(rn2);                                                // Результат операции
        }
        public static RomanNumber Sub(object obj1, object obj2)
        {
            var rn1 = (obj1 is RomanNumber r) ? r : new RomanNumber(obj1);      // Проверки на соответсвию на тип или присваивания object
            var rn2 = (obj2 is RomanNumber r2) ? r2 : new RomanNumber(obj2);
            return rn1.Sub(rn2);                                                // Результат операции
        }
        public static RomanNumber Mul(object obj1, object obj2)
        {
            var rn1 = (obj1 is RomanNumber r) ? r : new RomanNumber(obj1);      // Проверки на соответсвию на тип или присваивания object
            var rn2 = (obj2 is RomanNumber r2) ? r2 : new RomanNumber(obj2);
            return rn1.Mul(rn2);                                                // Результат операции
        }
        public static RomanNumber Div(object obj1, object obj2)
        {
            var rn1 = (obj1 is RomanNumber r) ? r : new RomanNumber(obj1);      // Проверки на соответсвию на тип или присваивания object
            var rn2 = (obj2 is RomanNumber r2) ? r2 : new RomanNumber(obj2);
            return rn1.Div(rn2);                                               // Результат операции
        }
    }
}



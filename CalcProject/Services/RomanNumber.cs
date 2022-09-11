using System;
namespace CalcProject.Services
{
    public record RomanNumber
    {

        public int romanNumber { set; get; }
      
        public RomanNumber(int num=0) => romanNumber = num;
        /// <summary>
        /// перевод из арабских чисел в римские
        /// </summary>
        /// <returns>Строку</returns>
        public override string ToString()
        {
            if (this.romanNumber == 0) { return "N"; };
            int n=this.romanNumber< 0 ?-this.romanNumber:this.romanNumber;
            string res = this.romanNumber<0?"-":"";
            string[] parts = {"M","CM","D","CD","C","XC","L","XL","X","IX","V","IV","I"};
            int[] val = {1000,900,500,400,100,90,50,40,10,9,5,4,1 };
           
            for (int i = 0; i < parts.Length; i++)
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
            if (str == null)
            {
                throw new ArgumentNullException();
            }
           
            if (str == "N") { return 0; }

            bool isNegative = false;// переменная флаг, для проверки на '-'
            if (str.StartsWith('-'))
            {
                isNegative= true;
                str=str[1..];//Переход на 1 позицию и приравнивание
            }

            if (str.Length == 0)
            {
                throw new ArgumentException("Empty string not allowed");
            }
            char[] digits = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            int[] digitValues = { 1, 5, 10, 50, 100, 500, 1000 };
            // Якщо наступна цифра числа більша за поточну, то
            // вона віднімається від результату, інакше додається
            // IX : -1 + 10;  XC : -10 + 100;  XX : +10+10; CX : +100+10
            int res = 0;
            int lastnumb = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                char digit = str[i];

                int ind = Array.IndexOf(digits, digit);
                
                if (str.Contains('N') && lastnumb==0)//Проверка на наличие более одного 'N'
                {
                    throw new ArgumentException("Invalid number, only one 'N'");
                }
                if (ind == -1)
                {
                    throw new ArgumentException($"Invalid char {digit}");
                }

                int val = digitValues[ind];
                if (val < lastnumb) { res -= val; }
                else { res += val; }

                lastnumb = val;
            }
            return isNegative ? -res:res;// добавляем "-" в начало, если флаг истина
        }
        /// <summary>
        /// Операция сложения чисел
        /// </summary>
        /// <param name="rn"></param>
        /// <returns>Римское число</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public RomanNumber Add(RomanNumber rn)
        {
            if (rn is null) { throw new ArgumentNullException(nameof(rn));}
            return new(this.romanNumber + rn.romanNumber);
            //return this with {romanNumber=this.romanNumber + rn.romanNumber };
        }
        public RomanNumber Add(int val)
        {
            return new(this.romanNumber + val);
        }
        public RomanNumber Add(string str)
        {
            return new(this.romanNumber +RomanNumber.Parse(str));
        }

        //Тесты для статиков
        public static RomanNumber Add (RomanNumber rn1, RomanNumber rn2)
        {
            if (rn1 is null||rn2 is null) { throw new ArgumentNullException(nameof(RomanNumber)); }
            return new (rn1.romanNumber + rn2.romanNumber);
        }
        public static RomanNumber Add(int rn1, int rn2)
        {
            return new(rn1 + rn2);
        }
        public static RomanNumber Add(string rn1, string rn2)
        {
            if (rn1 is null || rn2 is null) { throw new ArgumentNullException(nameof(RomanNumber)); }

            if (rn1.Length == 0||rn2.Length==0)
            {
                throw new ArgumentException("Empty string not allowed");
            }
            return new(Parse(rn1) + Parse(rn2));
        }
        public static RomanNumber Add(RomanNumber rn1, string rn2)
        {
            if (rn1 is null) { throw new ArgumentNullException(nameof(rn1)); }

            if (rn2.Length == 0)
            {
                throw new ArgumentException("Empty string not allowed");
            }
            return new(rn1.romanNumber + Parse(rn2));
        }
        public static RomanNumber Add(RomanNumber rn1, int rn2)
        {
            if (rn1 is null) { throw new ArgumentNullException(nameof(rn1)); }
            return new(rn1.romanNumber + rn2);
        }
    }
}

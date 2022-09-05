using System;
namespace CalcProject.Services
{
    public class RomanNumber
    {
       
        public static int Parse(string str)
        {
            char [] digits = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            int[] digitValues = { 1, 5, 10, 50, 100, 500, 1000 };
            // Якщо наступна цифра числа більша за поточну, то
            // вона віднімається від результату, інакше додається
            // IX : -1 + 10;  XC : -10 + 100;  XX : +10+10; CX : +100+10
            int res = 0;
            int lastnumb = 0;
            for (int i= str.Length-1; i>=0;i--)
            {
          
                char digit = str[i];
                
                int ind = Array.IndexOf(digits, digit);
                if (ind == -1)
                {
                    throw new ArgumentException($"Invalid char {digit}");
                }
                int val = digitValues[ind];

                if (val < lastnumb) { res -= val; }
                else { res += val; }
              
                lastnumb = val;
            }
           

            return res;
        }
        
    }
}

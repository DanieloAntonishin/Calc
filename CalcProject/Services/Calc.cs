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

        public Calc(){}
        public Calc(Resources resources)
        {
            Resources = resources;
        }

        public void Run()
        {
            
            string? str;
            RomanNumber? rn1=null;
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
    }
}
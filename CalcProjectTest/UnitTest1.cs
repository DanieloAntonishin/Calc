using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcProject.Services;
namespace CalcProjectTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalcTest()
        {
            CalcProject.Services.Calc calc = new();
            Assert.IsNotNull(calc);
        }
        [TestMethod]
        public void RomanNumberParseTest()

        {
            Assert.AreEqual(RomanNumber.Parse("I"), 1, "I == 1");
            Assert.AreEqual(RomanNumber.Parse("IV"), 4, "IV == 4");
            Assert.AreEqual(RomanNumber.Parse("XV"), 15);
            Assert.AreEqual(RomanNumber.Parse("XXX"), 30);
            Assert.AreEqual(RomanNumber.Parse("CM"), 900);
            Assert.AreEqual(RomanNumber.Parse("MCMXCIX"), 1999);
            Assert.AreEqual(RomanNumber.Parse("CD"), 400);
            Assert.AreEqual(RomanNumber.Parse("CDI"), 401);
            Assert.AreEqual(RomanNumber.Parse("LV"), 55);
            Assert.AreEqual(RomanNumber.Parse("XL"), 40);
        }


    }
}


//TDD - Test Drive Development - разработка управления тестами
// Суть - сначала пишутся тесты, потом создаем ПО, которое удовлетворяет
// этими тестами. XP - минимальный путь( без "запасов")
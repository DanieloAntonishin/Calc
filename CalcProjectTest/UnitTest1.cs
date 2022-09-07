using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcProject.Services;
using System;

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
        public void RomanNumberParseTest1Digit()

        {
            Assert.AreEqual(1, RomanNumber.Parse("I"));
            Assert.AreEqual(5, RomanNumber.Parse("V"));
            Assert.AreEqual(10, RomanNumber.Parse("X"));
            Assert.AreEqual(50, RomanNumber.Parse("L"));
            Assert.AreEqual(100, RomanNumber.Parse("C"));
            Assert.AreEqual(500, RomanNumber.Parse("D"));
            Assert.AreEqual(1000, RomanNumber.Parse("M"));

            Assert.AreEqual(0, RomanNumber.Parse("N"));

        }
        [TestMethod]
        public void RomanNumberParseTest2Digit()
        {
            Assert.AreEqual(4, RomanNumber.Parse("IV"));
            Assert.AreEqual(15, RomanNumber.Parse("XV"));
            Assert.AreEqual(900, RomanNumber.Parse("CM"));
            Assert.AreEqual(400, RomanNumber.Parse("CD"));
            Assert.AreEqual(55, RomanNumber.Parse("LV"));
            Assert.AreEqual(40, RomanNumber.Parse("XL"));
        }
        [TestMethod]
        public void RomanNumberParseTest3Digit()
        {
            Assert.AreEqual(30, RomanNumber.Parse("XXX"));
            Assert.AreEqual(401, RomanNumber.Parse("CDI"));
            Assert.AreEqual(1999, RomanNumber.Parse("MCMXCIX"));
        }
        [TestMethod]
        public void RomanNumberParseTestInavalidDigit()
        {
            //Assert.AreEqual(0, RomanNumber.Parse("XXA")); not working, exception like fail test
            //Assert.ThrowsException<Exception>(() => { RomanNumber.Parse("XX"); }); not working, waiting Exception but comes ArgumentException
            var exc = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("XXA"); });//save 
            var exp = new ArgumentException($"Invalid char A"); //expected result
            Assert.AreEqual(exp.Message, exc.Message);
        }
        [TestMethod]
        public void RomanNumberParseTestInavalidDigit2()
        {
            //var exc = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("X2X"); });
            //var expn = new ArgumentException($"Invalid char 2");
            //Assert.AreEqual(expn.Message, exc.Message);

            //var exc1 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("1IX"); });
            //var expn1 = new ArgumentException($"Invalid char 1");
            //Assert.AreEqual(expn1.Message, exc1.Message);

            //var exc2 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("AX"); });
            //var expn2 = new ArgumentException($"Invalid char A");
            //Assert.AreEqual(expn2.Message, exc2.Message);
            string mess = "Invalid char";
            Assert.AreEqual(true,Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse(" XX"); }).Message.StartsWith(mess));
        }
        [TestMethod]
        public void RomanNumberParseTestEmpty()
        {
            var empt = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse(""); });
            var exp = new ArgumentException("Empty string not allowed");
            Assert.AreEqual(exp.Message, empt.Message);

            Assert.IsNotNull(Assert.ThrowsException<ArgumentNullException>(() => { RomanNumber.Parse(null); }));
        }

        [TestMethod]
        public void RomanNumberParseOnlyN()
        {
            var empt = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("XNX"); });
            var exp = new ArgumentException("Invalid number, only one 'N'");
            Assert.AreEqual(exp.Message, empt.Message);

            var empt2 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("XNN"); });
            var exp2 = new ArgumentException("Invalid number, only one 'N'");
            Assert.AreEqual(exp2.Message, empt2.Message);

            var empt3 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("NN"); });
            var exp3 = new ArgumentException("Invalid number, only one 'N'");
            Assert.AreEqual(exp3.Message, empt3.Message);

            var empt4 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("NX"); });
            var exp4 = new ArgumentException("Invalid number, only one 'N'");
            Assert.AreEqual(exp4.Message, empt4.Message);
        }

    }
}

//TDD - Test Drive Development - разработка управления тестами
// Суть - сначала пишутся тесты, потом создаем ПО, которое удовлетворяет
// этими тестами. XP - минимальный путь( без "запасов")
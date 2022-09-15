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
            Assert.AreEqual(0, RomanNumber.Parse("N"));
            Assert.AreEqual(1, RomanNumber.Parse("I"));
            Assert.AreEqual(5, RomanNumber.Parse("V"));
            Assert.AreEqual(10, RomanNumber.Parse("X"));
            Assert.AreEqual(50, RomanNumber.Parse("L"));
            Assert.AreEqual(100, RomanNumber.Parse("C"));
            Assert.AreEqual(500, RomanNumber.Parse("D"));
            Assert.AreEqual(1000, RomanNumber.Parse("M"));

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
            var exp = new ArgumentException(Resources.GetInvalidCharMessage('A')); //expected result
            Assert.AreEqual(exp.Message, exc.Message);
        }
        [TestMethod]
        public void RomanNumberParseTestInavalidDigit2()
        {
            var exc = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("X2X"); });
            var expn = new ArgumentException(Resources.GetInvalidCharMessage('2'));
            Assert.AreEqual(expn.Message, exc.Message);

            var exc1 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("1IX"); });
            var expn1 = new ArgumentException(Resources.GetInvalidCharMessage('1'));
            Assert.AreEqual(expn1.Message, exc1.Message);

            var exc2 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("AX"); });
            var expn2 = new ArgumentException(Resources.GetInvalidCharMessage('A'));
            Assert.AreEqual(expn2.Message, exc2.Message);

            Assert.AreEqual(true, Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("2X X1"); })
                .Message.StartsWith(Resources.GetInvalidCharMessage('\0')[..5]));
        }
        [TestMethod]
        public void RomanNumberParseTestEmpty()
        {
            var empt = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse(""); });
            var exp = new ArgumentException(Resources.GetEmptyStringException());
            Assert.AreEqual(exp.Message, empt.Message);

            Assert.IsNotNull(Assert.ThrowsException<ArgumentNullException>(() => { RomanNumber.Parse(null); }));
        }
        [TestMethod]
        public void RomanNumberCtor()
        {
            RomanNumber romanNumber = new();
            Assert.IsNotNull(romanNumber);
        }
        [TestMethod]
        public void RomanNumberParseOnlyN()
        {
            RomanNumber romanNumber = new();
            var empt = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("XNX"); });
            var exp = new ArgumentException(Resources.GetOnlyOne_N_Exception());
            Assert.AreEqual(exp.Message, empt.Message);

            var empt2 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("XNN"); });
            var exp2 = new ArgumentException(Resources.GetOnlyOne_N_Exception());
            Assert.AreEqual(exp2.Message, empt2.Message);

            var empt3 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("NN"); });
            var exp3 = new ArgumentException(Resources.GetOnlyOne_N_Exception());
            Assert.AreEqual(exp3.Message, empt3.Message);

            var empt4 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("NX"); });
            var exp4 = new ArgumentException(Resources.GetOnlyOne_N_Exception());
            Assert.AreEqual(exp4.Message, empt4.Message);
           

            romanNumber = new(10);
            Assert.IsNotNull(romanNumber);

            romanNumber = new(0);
            Assert.IsNotNull(romanNumber);
        }
        [TestMethod]
        public void RomanNumberToString()
        {
            RomanNumber romanNumber = new();
            Assert.AreEqual("N", romanNumber.ToString());
            romanNumber = new(10);
            Assert.AreEqual("X", romanNumber.ToString());
            romanNumber = new(90);
            Assert.AreEqual("XC", romanNumber.ToString());
            romanNumber = new(20);
            Assert.AreEqual("XX", romanNumber.ToString());
            romanNumber = new(1999);
            Assert.AreEqual("MCMXCIX", romanNumber.ToString());
        }
        [TestMethod]
        public void RomanNumberToStringParseCrossTest()
        {
            RomanNumber roman = new();
            for (int n = 0; n <= 2022; n++)
            {
                roman.romanNumber = n;
                Assert.AreEqual(n, RomanNumber.Parse(roman.ToString()));
            }
        }
        [TestMethod]
        public void RomanNumberTypeTest()
        {
            RomanNumber rn1 = new(10);
            RomanNumber rn2 = rn1;
            Assert.AreSame(rn1, rn2);

            RomanNumber rn3 = rn1 with { };
            Assert.AreNotSame(rn3, rn1);
            Assert.AreEqual(rn3, rn1);
            Assert.IsTrue(rn1 == rn3);

            RomanNumber rn4 = rn1 with { romanNumber = 20 };
            Assert.AreNotSame(rn4, rn1);
            Assert.AreNotEqual(rn4, rn1);
            Assert.IsFalse(rn1 == rn4);
            Assert.IsFalse(rn1.Equals(rn4));
        }
        [TestMethod]
        public void RomanNumberNegative()
        {
            //Тесты для Parse
            Assert.AreEqual(-10, RomanNumber.Parse("-X"));
            Assert.AreEqual(-400, RomanNumber.Parse("-CD"));
            Assert.AreEqual(-1900, RomanNumber.Parse("-MCM"));
            // ToString()
            RomanNumber rn = new() { romanNumber = -10 };
            Assert.AreEqual("-X", rn.ToString());
            rn.romanNumber = -90;
            Assert.AreEqual("-XC", rn.ToString());
            // Исключения
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("M-CM"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("M-"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("-"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("-N"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("--X"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("--"));
        }
    }

    [TestClass]
    public class OperationsTest
    {
        [TestMethod]
        public void AddRnTest()
        {
            RomanNumber rn = new(5);
            RomanNumber rn2 = new(2);
            RomanNumber _rn2 = new(-2);
            RomanNumber _rn7 = new(-7);
            RomanNumber _rn5 = new(-5);

            Assert.AreEqual(10, rn.Add(new RomanNumber(5)).romanNumber);
            Assert.AreEqual(25, rn.Add(new RomanNumber(20)).romanNumber);
            Assert.AreEqual(6, rn.Add(new RomanNumber(1)).romanNumber);

            Assert.AreEqual(5, rn.Add(new RomanNumber(0)).romanNumber);
            Assert.AreEqual(2, rn.Add(new RomanNumber(-3)).romanNumber);
            Assert.AreEqual(0, rn.Add(new RomanNumber(-5)).romanNumber);

            Assert.AreEqual(10, rn.Add(rn).romanNumber);
            Assert.AreEqual(new RomanNumber(7).romanNumber, rn2.Add(rn).romanNumber);
            Assert.AreEqual(_rn7, _rn2.Add(_rn5));

            Assert.AreEqual("XVII", rn.Add(new RomanNumber(12)).ToString());
            Assert.AreEqual("-V", rn.Add(new RomanNumber(-10)).ToString());

            Assert.ThrowsException<ArgumentNullException>(() => rn.Add((RomanNumber)null!));
        }
        [TestMethod]
        public void AddValueTest()
        {
            var rn = new RomanNumber(10);
            Assert.AreEqual(20, rn.Add(10).romanNumber);
            Assert.AreEqual("V", rn.Add(-5).ToString());
            Assert.AreEqual(rn, rn.Add(0));
        }
        [TestMethod]
        public void AddStringTest()
        {
            var rn = new RomanNumber(10);
            Assert.AreEqual(30, rn.Add("XX").romanNumber);
            Assert.AreEqual("-XL", rn.Add("-L").ToString());
            Assert.AreEqual(rn, rn.Add("N"));


            Assert.ThrowsException<ArgumentException>(() => rn.Add(""));
            Assert.ThrowsException<ArgumentException>(() => rn.Add("-"));
            Assert.ThrowsException<ArgumentException>(() => rn.Add("20"));
            Assert.ThrowsException<ArgumentNullException>(() => rn.Add((string)null!));
        }
        [TestMethod]
        public void AddStaticTest()
        {
            RomanNumber rn5 = RomanNumber.Add(2, 3);
            RomanNumber rn8 = RomanNumber.Add(rn5, 3);
            RomanNumber rn10 = RomanNumber.Add("I","IX");
            RomanNumber rn9 = RomanNumber.Add(rn5,"IV");
            RomanNumber rn15 = RomanNumber.Add(rn5,rn10);
            // Прохождения тестов + рефакторинга
            Assert.AreEqual(5, rn5.romanNumber);
            Assert.AreEqual(rn8.romanNumber, (rn5.romanNumber+3));
            Assert.AreEqual(rn10.ToString(), "X");
            Assert.AreEqual(rn9.romanNumber, rn5.romanNumber+RomanNumber.Parse("IV"));
            Assert.AreEqual(rn15.romanNumber, rn5.romanNumber + rn10.romanNumber);
            // Исключения для статика + для рефакторинга
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Add("",""));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Add("-","-"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Add("",null!));
            Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Add((RomanNumber)null!,""));
            Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Add((RomanNumber)null!,0));
            Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Add((RomanNumber)null!,(RomanNumber)null!));
        }
    }
}

//TDD - Test Drive Development - разработка управления тестами
// Суть - сначала пишутся тесты, потом создаем ПО, которое удовлетворяет
// этими тестами. XP - минимальный путь( без "запасов")
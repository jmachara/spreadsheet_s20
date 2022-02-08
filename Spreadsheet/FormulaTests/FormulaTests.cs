using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
/// <summary> 
/// Author:    Jack Machara
/// Partner:   None
/// Date:      1/29/20 
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jack Machara - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jack Machara, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source except for the code provided to me by the course instructor.  All references used in the
/// completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
/// Tests for the Formula Class.
/// </summary>
namespace FormulaTests
{
    [TestClass]
    public class FormulaTests
    {
        private static readonly List<string> validTokens = new List<string> { "+", "-", "/", "*", "(", ")" };
        /// <summary>
        /// Testing Addition.
        /// </summary>
        [TestMethod]
        public void TestAddition()
        {
            Formula f = new Formula("1+4");
            Assert.AreEqual(5.0, f.Evaluate(lookup));
            
        }
        /// <summary>
        /// Testing Subtraction
        /// </summary>
        [TestMethod]
        public void TestSubtraction()
        {
            Formula f = new Formula("8-1");
            Assert.AreEqual(7.0, f.Evaluate(lookup));

        }
        /// <summary>
        /// Testing Multiplication
        /// </summary>
        [TestMethod]
        public void TestMultiplication()
        {
            Formula f = new Formula("2*6");
            Assert.AreEqual(12.0, f.Evaluate(lookup));

        }
        /// <summary>
        /// Testing Division
        /// </summary>
        [TestMethod]
        public void TestDivision()
        {
            Formula f = new Formula("8/2");
            Assert.AreEqual(4.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// Testing scientific notation
        /// </summary>
        [TestMethod]
        public void TestScientificNotation()
        {
            Formula f = new Formula("5e-2");
            Assert.AreEqual(.05, f.Evaluate(lookup));

        }
        /// <summary>
        /// Testing scientific notation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]

        public void TestNegativeNumber()
        {
            Formula f = new Formula("-2",normalize,isValid);
        }
        /// <summary>
        /// Testing the single argument constructor with a sentence.
        /// </summary>
        [TestMethod]
        public void TestOneArgConstrutor()
        {
            Formula f = new Formula("1");
            Assert.IsTrue(f is Formula);
            Assert.AreEqual(1.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// Testing a long formula with every operation and scientific method and variables
        /// </summary>
        [TestMethod]
        public void TestLongFormula1()
        {
            Formula f = new Formula("((10*2)+(9-1)/(4+4))*(5e2)/a1",normalize,isValid);
            Assert.AreEqual(1050.0,f.Evaluate(lookup));
        }
        /// <summary>
        /// Testing toString 
        /// </summary>
        [TestMethod]
        public void TesttoString()
        {
            Formula f = new Formula("a1+5e6", normalize, isValid);
            Assert.AreEqual("A1+5e6", f.ToString());
        }
        /// <summary>
        /// Testing white space equality
        /// </summary>
        [TestMethod]
        public void TestWhiteSpace()
        {
            Formula f = new Formula("5   +    10", normalize, isValid);
            Formula f2 = new Formula("5+10", normalize, isValid);
            Assert.IsTrue(f2.ToString().Equals(f.ToString()));
        }
        /// <summary>
        /// Testing GetHashCode()
        /// </summary>
        [TestMethod]
        public void TestHashCode()
        {
            Formula f = new Formula("11-4");
            Formula f2 = f;
            Assert.IsTrue(f2.GetHashCode().Equals(f.GetHashCode()));
        }
        /// <summary>
        /// Testing empty formula
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestingEmptyFormula()
        {
            Formula f = new Formula("");
        }
        /// <summary>
        /// Testing Dividing by zero
        /// </summary>
        [TestMethod]
        public void TestingDivideByZero()
        {
            Formula f = new Formula("99/(A1-10)",normalize,isValid);
            Assert.AreEqual(new FormulaError("Can't Divide By Zero"), f.Evaluate(lookup));
        }
        /// <summary>
        /// Testing odd number of parentheses
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestingOddParen()
        {
            Formula f = new Formula("(1+2))");
        }
        /// <summary>
        /// Testing bad variable name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestingInvalidVariable()
        {
            Formula f = new Formula("(10+5)*93aC+(3+8)");
        }
        /// <summary>
        /// Testing == & != operators
        /// </summary>
        [TestMethod]
        public void TestOperators()
        {
            Formula f = new Formula("4*3", normalize, isValid);
            Formula f2 = new Formula("4*3", normalize, isValid);
            Formula f3 = new Formula("1+1");
            Formula f4 = null;
            Formula f5 = null;
            Assert.IsTrue(f == f2);
            Assert.IsTrue(f != f3);
            Assert.IsFalse(f != f2);
            Assert.IsTrue(f4 == f5);
            Assert.IsTrue(f4 != f3);
            Assert.IsTrue(f2 != f5);
        }
        /// <summary>
        /// Testing the Equals overridden method
        /// </summary>
        [TestMethod]
        public void TestEquals()
        {
            Formula f = new Formula("4*3", normalize, isValid);
            Formula f2 = f;
            Formula f3 = new Formula("1+1");
            Formula f4 = null;
            Assert.IsTrue(f2.Equals(f));
            Assert.IsFalse(f.Equals(f3));
            Assert.IsFalse(f.Equals(f4));
        }
        /// <summary>
        /// Testing GetVariables method with and without variables
        /// </summary>
        [TestMethod]
        public void TestGetVariables()
        {
            Formula f = new Formula("4*3", normalize, isValid);
            Formula f2 = new Formula("x2 + x5");
            HashSet<String> hs = new HashSet<string>();
            HashSet<string> rightAnswer = new HashSet<string>();
            rightAnswer.Add("X2");
            rightAnswer.Add("X6");
            Assert.AreEqual(hs.ToString(), f.GetVariables().ToString());
            Assert.AreEqual(rightAnswer.ToString(), f2.GetVariables().ToString());
        }
        /// <summary>
        /// Testing a openParentheses after a number. Should throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestingParenAfterNum()
        {
            Formula f = new Formula("(10+5)*6(3+8)");
        }
        /// <summary>
        /// Testing multiplying variables
        /// </summary>
        [TestMethod]
        public void TestingMultiplyingVariables()
        {
            Formula f = new Formula("(10*2) + (a1*B1)",normalize, isValid);
            Assert.AreEqual(100.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// Testing the check Scientific notation method I made but wll return false;
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestingScientificNotationFalse()
        {
            Formula f = new Formula("100E-29!", normalize, isValid);
        }
        /// <summary>
        /// This test should throw an error because you can't have a variable right after a close parentheses
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestingVariableAfterCloseParen()
        {
            Formula f = new Formula("(1+1)a1");
        }
        /// <summary>
        /// Testing null formula
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestingNullFormula()
        {
            Formula f = new Formula("");
        }
        /// <summary>
        /// formula of parenthesis
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestingOnlyParenthesis()
        {
            Formula f = new Formula("((((()))))");
        }
        /// <summary>
        /// formula of parenthesis in the wrong order
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestingOutOfOrderParenthesis()
        {
            Formula f = new Formula(")3+2(");
        }
        /// <summary>
        /// testing isValid function written in the test class
        /// </summary>
        [TestMethod]
        public void TestisValid()
        {
            Assert.IsTrue(isValid("_______________"));
        }
        /// <summary>
        /// testing the normalize function written in the test class
        /// </summary>
        [TestMethod]
        public void TestNormalize()
        {
            Assert.AreEqual( "HI1234__TA", normalize("hI1234__ta"));
        }
        /// <summary>
        /// testing the lookup function written in the test class
        /// </summary>
        [TestMethod]
        public void TestLookup()
        {
            Formula f = new Formula("A1");
            Assert.AreEqual(10.0, f.Evaluate(lookup));
        }
        /// <summary>
        /// Converts the lowercase letters in the token to uppercase.
        /// </summary>
        /// <param name="token">token being changed</param>
        /// <returns>Uppercase Token</returns>
        private static string normalize(string token)
        {
            string outputString = "";
            foreach (char character in token)
            {
                outputString += char.ToUpper(character);
            }
            return outputString;
        }
        /// <summary>
        /// Determines if tokens are valid variable names.
        /// </summary>
        /// <param name="token"> token being looked at</param>
        /// <returns>True if the token is a valid variable name.</returns>
        private static bool isValid(string token)
        {
            if (validTokens.Contains(token) || double.TryParse(token, out double output))
                return false;
            else
            {
                char[] characters = token.ToCharArray();
                for(int i = 0; i < characters.Length; i++)
                {
                    if (i == 0 && !(char.IsLetter(characters[i]) || characters[i] == '_'))
                        return false;
                    else if (i > 0 && !(char.IsLetterOrDigit(characters[i]) || characters[i] == '_'))
                        return false;
                }
                return true;
            }
        }
        /// <summary>
        /// Simple variable lookup for testing purposes
        /// </summary>
        /// <param name="token">Token being lookedup</param>
        /// <returns>value of the token according to this method</returns>
        private static double lookup(string token)
        {
            if (token.Equals("A1"))
                return 10;
            else if (token.Equals("B1"))
                return 8;
            else if (token.Equals("a1"))
                return 1000000000;
            else
                return 1;
        }
    }
}

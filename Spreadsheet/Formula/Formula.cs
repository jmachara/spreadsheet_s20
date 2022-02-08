// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// (Daniel Kopta) 
// Version 1.2 (9/10/17) 

// Change log:
//  (Version 1.2) Changed the definition of equality with regards
//                to numeric tokens


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
/// Creates a Formula object with normalized variables from a string if the syntax is correct. 
/// </summary>
namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax (without unary preceeding '-' or '+'); 
    /// variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {
        private string[] tokenArray;
        private static readonly List<string> validTokens = new List<string> { "+", "-", "/", "*", "(", ")" };
        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {

        }

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {
            if (formula != null)
            {

                IEnumerable<string> tokens = GetTokens(formula);
                if (tokens.Count() == 0)
                    throw new FormulaFormatException("Formula Cannot Be Empty");
                tokenArray = new string[tokens.Count()];
                int count = 0;
                int parenCount = 0;
                //Verifies the first and last tokens
                string firstToken = tokens.First<string>();
                string lastToken = tokens.Last<string>();
                if ((validTokens.Contains(firstToken) && !firstToken.Equals("(")) || (validTokens.Contains(lastToken) && !lastToken.Equals(")")))
                    throw new FormulaFormatException("Cannot begin or end on an operator");
                if ((validTokens.Contains(firstToken) && firstToken.Equals("("))||(ScientificNotationChecker(firstToken) || double.TryParse(firstToken, out double parseResultFirst) || isValid(normalize(firstToken))) && (validTokens.Contains(lastToken) && lastToken.Equals(")"))||(ScientificNotationChecker(lastToken) || double.TryParse(lastToken, out double parseResultLast) || isValid(normalize(lastToken))))
                {
                    bool openParenLast = false;
                    bool numVarOrCloseParenLast = false;
                    bool doubleOperatorCheck = false;
                    foreach (string token in tokens)
                    {
                        if (parenCount < 0)
                            throw new FormulaFormatException("Formula Not Valid: Parenthesis");
                        //if the number is in scientific method, it converts it to a float
                        if (ScientificNotationChecker(token))
                        {
                            double.Parse(token, System.Globalization.NumberStyles.Float);
                            tokenArray[count] = token;
                            count++;
                            numVarOrCloseParenLast = true;
                            doubleOperatorCheck = false;
                            openParenLast = false;
                        }
                        else if (double.TryParse(token, out double result))
                        {
                            if (numVarOrCloseParenLast)
                                throw new FormulaFormatException("Can't have two numbers in a row");
                            tokenArray[count] = result.ToString();
                            count++;
                            numVarOrCloseParenLast = true;
                            doubleOperatorCheck = false;
                            openParenLast = false;
                        }
                        else if (validTokens.Contains(token))
                        {
                            //if the last token was '(' then the next one has to be a number, variable or '(', if not throws error
                            if (openParenLast)
                            {
                                if (validTokens.Contains(token) && !token.Equals("("))
                                    throw new FormulaFormatException("Formula Not Valid: '(' has to be followed by a number, variable or '('");
                                openParenLast = false;
                            }
                            //if the last token was a number, variable or ')' then the next token has to be an operator or ')', if not throws error
                            if (numVarOrCloseParenLast)
                            {
                                if (!validTokens.Contains(token) || token.Equals("("))
                                    throw new FormulaFormatException("Formula Not Valid: #, variables and ')' need to be followed by an operator or ')'");
                                numVarOrCloseParenLast = false;
                            }
                            if (doubleOperatorCheck)
                            {
                                if (validTokens.Contains(token) && !(token.Equals("(") || token.Equals(")")))
                                    throw new FormulaFormatException("Cannot have two operators in a row.");
                                doubleOperatorCheck = false;

                            }
                            if (token.Equals("("))
                            {
                                parenCount++;
                                openParenLast = true;
                            }
                            else if (token.Equals(")"))
                            {
                                parenCount--;
                                numVarOrCloseParenLast = true;
                            }
                            else if (validTokens.Contains(token))
                                doubleOperatorCheck = true;

                            tokenArray[count] = token;
                            count++;
                        

                        }
                        //if it's a variable
                        else if (isValid(normalize(token)))
                        {
                            if (numVarOrCloseParenLast)
                                throw new FormulaFormatException("Formula Not Valid: #, variable or ')' followed by invalid character");
                            numVarOrCloseParenLast = true;
                            tokenArray[count] = normalize(token);
                            count++;
                            openParenLast = false;
                            doubleOperatorCheck = false;
                        }
                        else
                            throw new FormulaFormatException("Variable Not Valid");
                    }
                }
                else
                    throw new FormulaFormatException("Formula Not Valid: Uneven Parenthesis");
                if (parenCount != 0)
                    throw new FormulaFormatException("Formula Not Valid: Uneven Parenthesis");
            }
        }

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            Stack<String> operators = new Stack<String>();
            Stack<double> values = new Stack<double>();
            foreach (String value in tokenArray)
            {
                double result;
                //if its a number
                if (double.TryParse(value, out result))
                {
                    values.Push(result);
                    if (operators.isOntopOfStack("*"))
                    {
                        allOp("*", values, operators);
                    }
                    else if (operators.isOntopOfStack("/"))
                    {
                        if (DivideByZeroCheck(values, operators))
                            return new FormulaError("Can't Divide By Zero");
                        allOp("/", values, operators);
                    }
                }
                else if (value.Equals("+") || value.Equals("-"))
                {
                    if (operators.isOntopOfStack("+") || operators.isOntopOfStack("-"))
                        allOp(operators.Peek(), values, operators);
                    operators.Push(value);
                }
                else if (value.Equals("(") || value.Equals("*") || value.Equals("/"))
                {
                    operators.Push(value);
                }
                else if (value.Equals(")"))
                {
                    if (operators.Peek().Equals("+") || (operators.Peek().Equals("-")))
                        allOp(operators.Peek(), values, operators);
                    operators.Pop();
                    if (operators.isOntopOfStack("*") || operators.isOntopOfStack("/"))
                    {
                        if (DivideByZeroCheck(values, operators))
                            return new FormulaError("Can't Divide By Zero");
                        allOp(operators.Peek(), values, operators);
                    }
                }
                //if it's a variable
                else
                {
                    double number;
                    try
                    {
                        number = lookup(value);
                    }
                    catch (Exception)
                    {
                        return new FormulaError();
                    }

                    values.Push(number);
                    if (operators.isOntopOfStack("*"))
                    {
                        allOp("*", values, operators);
                    }
                    else if (operators.isOntopOfStack("/"))
                    {
                        if (DivideByZeroCheck(values, operators))
                            return new FormulaError("Can't Divide By Zero");
                        allOp("/", values, operators);
                    }
                }
            }
            if (operators.Count != 0)
                allOp(operators.Peek(), values, operators);
            if (operators.Count != 0 || values.Count != 1)
                return new FormulaError("Invalid Formula");
            return values.Pop();
        }

        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            HashSet<string> set = new HashSet<string>();
            foreach (string token in tokenArray)
            {
                double tryParseNum;
                if (!validTokens.Contains(token) && !double.TryParse(token, out tryParseNum))
                {
                    set.Add(token);
                }
            }
            return set;
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            String outputString = "";
            foreach (string token in tokenArray)
            {
                outputString += token;
            }
            return outputString;
        }

        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Formula)
            {
                if (this.GetHashCode() == obj.GetHashCode())
                {
                    return true;
                }
                IEnumerable<string> objTokens = GetTokens(obj.ToString());
                int count = 0;
                foreach (string token in objTokens)
                {
                    if (double.TryParse(token, out double tokenResult) && double.TryParse(tokenArray[count], out double arrayResult))
                    {
                        if (tokenResult.ToString() != arrayResult.ToString())
                        {
                            return false;
                        }
                    }
                    else if (!token.Equals(tokenArray[count]))
                    {
                        return false;
                    }
                    count++;
                }
                return true;

            }
            return false;
        }

        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            if (f1 is null && f2 is null)
                return true;
            if (!(f1 is null))
                return f1.Equals(f2);
            return false;
        }

        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            return !(f1 == f2);
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }

        /// <summary>
        /// pops the operator stack once and the values stack twice and applys the <paramref name="op"/>
        /// to the 2 popped values and pushes it onto the stack. 
        /// </summary>
        /// <param name="op">string of the operation to be performed</param>
        private static void allOp(String op, Stack<double> values, Stack<String> operators)
        {
            operators.Pop();
            double val1 = values.Pop();
            double val2 = values.Pop();
            if (op.Equals("+"))
                values.Push(val1 + val2);
            else if (op.Equals("-"))
                values.Push(val2 - val1);
            else if (op.Equals("/"))
            {
                values.Push(val2 / val1);
            }
            else if (op.Equals("*"))
                values.Push(val2 * val1);
        }
        /// <summary>
        /// Checks if the Formula will divide by zero
        /// </summary>
        /// <param name="values">values Stack</param>
        /// <param name="operators">operators Stack</param>
        /// <returns>True if it will divide by zero</returns>
        private static bool DivideByZeroCheck(Stack<double> values, Stack<String> operators)
        {
            double val1 = values.Peek();
            if (operators.isOntopOfStack<string>("/") && val1 == 0.0)
                return true;
            return false;

        }
        /// <summary>
        /// Checks if a String is in scientific notation
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool ScientificNotationChecker(String token)
        {
            
            char[] tokenChars = token.ToCharArray();
            int count = 0;
            if (!char.IsDigit(tokenChars[0]))
                return false;
            while (char.IsDigit(tokenChars[count]))
            {
                count++;
                if (count == tokenChars.Length)
                    return false;
            }
            if (tokenChars[count].Equals('e') || tokenChars[count].Equals('E'))
            {
                count++;
                if (tokenChars[count].Equals('-'))
                    count++;
                while (count < tokenChars.Length && char.IsDigit(tokenChars[count]))
                    count++;
                if (count == tokenChars.Length)
                    return true;
            }
            return false;
        }
    }

}

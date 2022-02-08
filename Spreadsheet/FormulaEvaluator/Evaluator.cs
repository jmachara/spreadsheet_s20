using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/// <summary> 
/// Author:    Jack Machara
/// Partner:   None
/// Date:      1/9/20 
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jack Machara - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jack Machara, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
///    Takes a math problem as a string and solves it. 
/// </summary>
namespace FormulaEvaluator
{
    public class Evaluator
    {

        public delegate int Lookup(String variable_name);
        /// <summary>
        ///   The function does ....  You should be aware of the following edge cases ....
        /// 
        /// </summary>
        /// <param name="expression"> The formula as a String </param>
        /// <param name="variableEvaluator"> Returns the value of a variable </param>
        /// <returns> answer to <paramref name="expression"/></returns>
        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            Stack<String> operators = new Stack<String>();
            Stack<int> values = new Stack<int>();
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            foreach (String value in substrings)
            {
                int result;
                if (!value.Equals(""))
                {
                    if (Int32.TryParse(value, out result))
                    {
                        int number = Int32.Parse(value);
                        values.Push(number);
                        if (operators.isOntopOfStack("*"))
                        {
                            allOp("*", values, operators);
                        }
                        else if (operators.isOntopOfStack("/"))
                        {
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
                            allOp(operators.Peek(),values, operators);
                        if (!operators.isOntopOfStack("("))
                            throw new ArgumentException("Missing partenthesis");
                        operators.Pop();
                        if (operators.isOntopOfStack("*") || operators.isOntopOfStack("/"))
                            allOp(operators.Peek(), values, operators);
                    }
                    else if (validVariable(value))
                    {
                        int number = variableEvaluator(value);
                        values.Push(number);
                        if (operators.isOntopOfStack("*"))
                        {
                            allOp("*", values, operators);
                        }
                        else if (operators.isOntopOfStack("/"))
                        {
                            allOp("/", values, operators);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("invalid formula");
                    }
                }
            }

            if (operators.Count != 0)
                allOp(operators.Peek(), values, operators);
            if (operators.Count != 0 || values.Count != 1)
                throw new ArgumentException("Operators still on stack");
            return values.Pop();
        }
        /// <summary>
        /// pops the operator stack once and the values stack twice and applys the <paramref name="op"/>
        /// to the 2 popped values and pushes it onto the stack. 
        /// </summary>
        /// <param name="op">string of the operation to be performed</param>
        private static void allOp(String op, Stack<int> values, Stack<String> operators)
        {
            if (values.Count < 2)
                throw new ArgumentException("Equation isn't valid");
            operators.Pop();
            int val1 = values.Pop();
            int val2 = values.Pop();
            if (op.Equals("+"))
                values.Push(val1 + val2);
            else if (op.Equals("-"))
                values.Push(val2 - val1);
            else if (op.Equals("/"))
            {
                if (val1 == 0)
                    throw new ArgumentException("Tried to Divide by Zero");
                values.Push(val2 / val1);
            }
            else if (op.Equals("*"))
                values.Push(val2 * val1);
            else
                throw new ArgumentException("Not Valid Operation");

        }
        /// <summary>
        /// checks if the string is a valid variable name
        /// </summary>
        /// <param name="name">name of the variable</param>
        /// <returns>true if <paramref name="name"/> is a variable, if not it returns false</returns>
        private static Boolean validVariable(string name)
        {
            if (name.Length < 2)
            {
                return false;
            }
            char[] word = name.ToCharArray();
            int i = 0;
            int numLetters = 0;
            int numDigits = 0;
            while (i < word.Length && char.IsLetter(word[i]))
            {
                numLetters++;
                i++;
            }
            while(i < word.Length && char.IsDigit(word[i]))
            {
                numDigits++;
                i++;
            }
            return ((i == word.Length) && (numDigits > 0) && (numLetters > 0));
            
        }
    }
}

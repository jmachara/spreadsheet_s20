using System;
using System.Collections.Generic;
using System.Text;

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
///  Contains the extension for a stack 
/// </summary>
namespace FormulaEvaluator
{
    static class Extensions
    {
        /// <summary>
        /// Returns if the <paramref name="value"/> is on top of the stack
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack">The stack that is being looked at</param>
        /// <param name="value">The value that is being looked for on top of the stack</param>
        /// <returns></returns>
        public static Boolean isOntopOfStack<T>(this Stack<T> stack, string value)
        {
            if (stack.Count > 0)
            {
                T topVal = stack.Peek();
                if (topVal.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

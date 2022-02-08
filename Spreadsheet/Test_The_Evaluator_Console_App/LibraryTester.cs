using FormulaEvaluator;
using System;
/// <summary> 
/// Author:    Jack Machara
/// Partner:   None
/// Date:      1/22/20 
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jack Machara - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jack Machara, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source except for the code provided to me by the course instructor.  All references used in the
/// completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
/// Tests the Evaluator.cs class
/// </summary>

namespace Test_The_Evaluator_Console_App
{
    class LibraryTester
    {
        static void Main(string[] args)
        {
            //test a number
            if (Evaluator.Evaluate("1", null) != 1) { Console.WriteLine("Number test failed"); }
            //test a variable
            if (Evaluator.Evaluate("A5", (name) => 4) !=4) { Console.WriteLine("variable test failed"); }
            //test addition
            if (Evaluator.Evaluate("1+1", null) != 2) { Console.WriteLine("addition test failed"); }
            //test subtraction
            if (Evaluator.Evaluate("10-5", null) != 5) { Console.WriteLine("subtraction test failed"); }
            //test division
            if (Evaluator.Evaluate("121/11", null) != 11) { Console.WriteLine("division test failed"); }
            //test multiplication
            if (Evaluator.Evaluate("10*4", null) != 40) { Console.WriteLine("multiplication test failed"); }
            //test parenthesis
            if (Evaluator.Evaluate("(1+5)*5", null) != 30) { Console.WriteLine("parenthesis test failed"); }
            //test long equation without variables
            if (Evaluator.Evaluate("(8+4)*(9-2)/3", null) != 28) { Console.WriteLine("long equation without variables test failed"); }
            //test long equation with variables
            if (Evaluator.Evaluate("(A5-2)*((13+1)/(16-9))", (name) => 6) != 8) { Console.WriteLine("long equation with variables test failed"); }
            //order of operations test
            if (Evaluator.Evaluate("8+6/3*4", null) != 16) { Console.WriteLine("order of operations test failed"); }
            //bad code test(provided)
            try
            {
                Evaluator.Evaluate("-A-", null);
                Console.WriteLine("bad code test failed");
            }
            catch (ArgumentException)
            {
                
            }










        }

    }
}

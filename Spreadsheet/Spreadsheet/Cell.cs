using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary> 
/// Author:    Jack Machara
/// Partner:   None
/// Date:      2/10/20 
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jack Machara - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jack Machara, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source except for the code provided to me by the course instructor.  All references used in the
/// completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
/// A cell is the building block of the spreadsheet. This cell contains a read only value, as well as contents which can be changed by the spreadsheet.
/// </summary>
namespace SS
{
   /// <summary>
   /// A cell is a object that contains its contents and value.
   /// Contents are what are in the cell
   /// Value is the evaluated cell, this will be the same as contents unless contents
   ///      is a formula  
   /// </summary>
    public class Cell
    {
        //value is contents evaluated, contents are what is in the cell
        //contents can either be a formula, string or double.
        public object contents = new object();
        public bool isFormula;
        /// <summary>
        /// Cell class constructor builds a cell with an object in it.
        /// </summary>
        /// <param name="contents"></param>
        public Cell(object contents)
        {
            this.contents = contents;
            isFormula = false;
        }
        /// <summary>
        /// Cell class constructor builds a cell with a formula in it.
        /// </summary>
        /// <param name="contents"></param>
        public Cell(Formula contents)
        {
            this.contents = contents;
            isFormula = true;
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SpreadsheetUtilities;
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
/// Contains the spreadsheet object that uses the interfact AbstractSpreadsheet and implements its required methods
/// </summary>
namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        //Keeps track of Cells
        private Dictionary<string, Cell> cellDictionary;
        //Keeps track of Cell Dependencies
        private DependencyGraph dependencies;
        //Keeps track of Cell Values
        private Dictionary<string, double> valueLookup;
        public override bool Changed { get; protected set; }
        /// <summary>
        /// Zero argument constructor
        /// </summary>
        public Spreadsheet() :
            base(s => true, s => s, "default")
        {
            initializeSS();
        }
        /// <summary>
        /// Three argument constructor
        /// </summary>
        /// <param name="isValid"  > Checks if a variable is valid</param>
        /// <param name="normalize"> Normalizes variables         </param>
        /// <param name="version"  > Current Version Number       </param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version)
        : base(isValid, normalize, version)
        {
            initializeSS();
        }
        /// <summary>
        /// Four arument constructor, Loads a spreadSheet from a filename
        /// </summary>
        /// <param name="fileName" > Location of Saved Spreadsheet </param>
        /// <param name="isValid"  > Checks if a variable is valid </param>
        /// <param name="normalize"> Normalizes variables          </param>
        /// <param name="version"  > Current Version Number        </param>
        public Spreadsheet(string fileName, Func<string, bool> isValid, Func<string, string> normalize, string version)
        : base(isValid, normalize, version)
        {
            initializeSS();
            string guiVersion = this.GetSavedVersion(fileName);
            //Checks for null or different Spreadsheet
            if (guiVersion is null)
                throw new SpreadsheetReadWriteException("Version Cannot be Null");
            if (!version.Equals(guiVersion))
                throw new SpreadsheetReadWriteException("Version Numbers Are Different between saved and current program");
            else
                XmlReadFile(fileName);
        }
        /// <summary>
        /// Initializes the cellDictionary, DependencyGraph, and sets Changed to False;
        /// </summary>
        private void initializeSS()
        {
            if (Version is null)
                throw new SpreadsheetReadWriteException("Version Cannot be Null");
            if (IsValid is null)
                throw new SpreadsheetReadWriteException("isValid Cannot be Null");
            if (Normalize is null)
                throw new SpreadsheetReadWriteException("normalize Cannot be Null");
            this.cellDictionary = new Dictionary<string, Cell>();
            this.dependencies = new DependencyGraph();
            this.valueLookup = new Dictionary<string, double>();
            Changed = false;
        }
        /// <summary>
        ///   Returns the names of all non-empty cells.
        /// </summary>
        /// <returns>
        ///     Returns an Enumerable that can be used to enumerate
        ///     the names of all the non-empty cells in the spreadsheet.  
        /// </returns>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            List<string> output = new List<string>();
            foreach (string key in cellDictionary.Keys)
            {
                output.Add(key);
            }
            return output;
        }
        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// 
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a SpreadsheetUtilities.FormulaError.
        /// </summary>
        public override object GetCellValue(String name)
        {
            if (!cellDictionary.ContainsKey(name))
                return "";
            name = ValidateAndNormalize(name);
            if (cellDictionary[name].isFormula)
            {
                if (!valueLookup.ContainsKey(name))
                    return new FormulaError();
                return valueLookup[name];
            }
            else
                return GetCellContents(name);

        }
        /// <summary>
        ///   Returns the contents (as opposed to the value) of the named cell.
        /// </summary>
        /// 
        /// <exception cref="InvalidNameException"> 
        ///   Thrown if the name is null or invalid
        /// </exception>
        /// 
        /// <param name="name">The name of the spreadsheet cell to query</param>
        /// 
        /// <returns>
        ///   The return value should be either a string, a double, or a Formula.
        ///   See the class header summary 
        /// </returns>
        public override object GetCellContents(String name)
        {
            name = ValidateAndNormalize(name);

            if (cellDictionary.ContainsKey(name))
            {
                if (cellDictionary[name].isFormula)
                    return "=" + cellDictionary[name].contents.ToString();
                else
                    return cellDictionary[name].contents;

            }
            else
                return "";
        }
        /// <summary>
        /// Returns the Content of the cell 
        /// If the cell is a Formula it returns it without the '=' infront;
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private object GetContentsWithoutEquals(String name)
        {
            name = ValidateAndNormalize(name);

            if (cellDictionary.ContainsKey(name))
            {
                return cellDictionary[name].contents;
            }
            else
                return "";
        }
        /// <summary>
        ///   <para>Sets the contents of the named cell to the appropriate value. </para>
        ///   <para>
        ///       First, if the content parses as a double, the contents of the named
        ///       cell becomes that double.
        ///   </para>
        ///
        ///   <para>
        ///       Otherwise, if content begins with the character '=', an attempt is made
        ///       to parse the remainder of content into a Formula.  
        ///       There are then three possible outcomes:
        ///   </para>
        ///
        ///   <list type="number">
        ///       <item>
        ///           If the remainder of content cannot be parsed into a Formula, a 
        ///           SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       </item>
        /// 
        ///       <item>
        ///           If changing the contents of the named cell to be f
        ///           would cause a circular dependency, a CircularException is thrown,
        ///           and no change is made to the spreadsheet.
        ///       </item>
        ///
        ///       <item>
        ///           Otherwise, the contents of the named cell becomes f.
        ///       </item>
        ///   </list>
        ///
        ///   <para>
        ///       Finally, if the content is a string that is not a double and does not
        ///       begin with an "=" (equal sign), save the content as a string.
        ///   </para>
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException"> 
        ///   If the content parameter is null, throw an ArgumentNullException.
        /// </exception>
        /// 
        /// <exception cref="InvalidNameException"> 
        ///   If the name parameter is null or invalid, throw an InvalidNameException
        /// </exception>
        ///
        /// <exception cref="SpreadsheetUtilities.FormulaFormatException"> 
        ///   If the content is "=XYZ" where XYZ is an invalid formula, throw a FormulaFormatException.
        /// </exception>
        /// 
        /// <exception cref="CircularException"> 
        ///   If changing the contents of the named cell to be the formula would 
        ///   cause a circular dependency, throw a CircularException.  
        ///   (NOTE: No change is made to the spreadsheet.)
        /// </exception>
        /// 
        /// <param name="name"> The cell name that is being changed</param>
        /// <param name="content"> The new content of the cell</param>
        /// 
        /// <returns>
        ///       <para>
        ///           This method returns a list consisting of the passed in cell name,
        ///           followed by the names of all other cells whose value depends, directly
        ///           or indirectly, on the named cell. The order of the list MUST BE any
        ///           order such that if cells are re-evaluated in that order, their dependencies 
        ///           are satisfied by the time they are evaluated.
        ///       </para>
        ///
        ///       <para>
        ///           For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        ///           list {A1, B1, C1} is returned.  If the cells are then evaluate din the order:
        ///           A1, then B1, then C1, the integrity of the Spreadsheet is maintained.
        ///       </para>
        /// </returns>
        public override IList<String> SetContentsOfCell(String name, String content)
        {
            if (content is null)
                throw new ArgumentNullException();
            name = ValidateAndNormalize(name);
            //Determines contents type
            if (!SameContents(name, content))
            {
                Changed = true;
                //equation
                if (content.Length > 0 && content.Trim()[0].Equals('='))
                {
                    Formula contentFormula = new Formula(content.Substring(1, content.Length - 1), Normalize, IsValid);
                    return SetCellContents(name, contentFormula);
                }
                else
                {
                    return SetCellContents(name, content);
                }
            }
            //Value didn't change
            else
            {
                return new List<string>();
            }

        }
        /// <summary>
        ///  Set the contents of the named cell to the given number.  
        /// </summary>
        /// 
        /// <requires> 
        ///   The name parameter must be non null and valid
        /// </requires>
        /// 
        /// <param name="name"> The name of the cell </param>
        /// <param name="number"> The new contents/value </param>
        /// 
        /// <returns>
        ///   <para>
        ///       This method returns a LIST consisting of the passed in name followed by the names of all 
        ///       other cells whose value depends, directly or indirectly, on the named cell.
        ///   </para>
        ///
        ///   <para>
        ///       The order must correspond to a valid dependency ordering for recomputing
        ///       all of the cells, i.e., if you re-evaluate each cell in the order of the list,
        ///       the overall spreadsheet will be consistently updated.
        ///   </para>
        ///
        ///   <para>
        ///     For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        ///     set {A1, B1, C1} is returned, i.e., A1 was changed, so then A1 must be 
        ///     evaluated, followed by B1 re-evaluated, followed by C1 re-evaluated.
        ///   </para>
        /// </returns>
        protected override IList<String> SetCellContents(String name, double number)
        {
            //Will not get called
            return SetCellContents(name, number.ToString());
        }
        /// <summary>
        /// The contents of the named cell becomes the text.  
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException"> 
        ///   If text is null, throw an ArgumentNullException.
        /// </exception>
        /// 
        /// <requires> 
        ///   The name parameter must be non null and valid
        /// </requires>
        /// 
        /// <param name="name"> The name of the cell </param>
        /// <param name="text"> The new content/value of the cell</param>
        /// 
        /// <returns>
        ///   <para>
        ///       This method returns a LIST consisting of the passed in name followed by the names of all 
        ///       other cells whose value depends, directly or indirectly, on the named cell.
        ///   </para>
        ///
        ///   <para>
        ///       The order must correspond to a valid dependency ordering for recomputing
        ///       all of the cells, i.e., if you re-evaluate each cell in the order of the list,
        ///       the overall spreadsheet will be consistently updated.
        ///   </para>
        ///
        ///   <para>
        ///     For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        ///     set {A1, B1, C1} is returned, i.e., A1 was changed, so then A1 must be 
        ///     evaluated, followed by B1 re-evaluated, followed by C1 re-evaluated.
        ///   </para>
        /// </returns>
        protected override IList<String> SetCellContents(String name, String text)
        {
            //Adds cell
            if (!cellDictionary.ContainsKey(name))
            {
                cellDictionary.Add(name, new Cell(text));
            }
            else
            {
                if (valueLookup.ContainsKey(name))
                    valueLookup.Remove(name);
                cellDictionary[name].isFormula = false;
                dependencies.ReplaceDependees(name, new List<string>());
            }
            //Deletes cell if empty
            if (text.Equals(""))
            {
                cellDictionary.Remove(name);
            }
            //if text is a double
            else if (double.TryParse(text, out double parseDouble))
            {
                cellDictionary[name].contents = parseDouble;
                if (!valueLookup.ContainsKey(name))
                    valueLookup.Add(name, parseDouble);
                else
                    valueLookup[name] = parseDouble;
                cellDictionary[name].isFormula = false;
            }
            //text is a string
            else
            {
                cellDictionary[name].contents = text;
            }
            //Recalculates all dependents
            List<string> output = new List<string>();
            foreach (string cell in GetCellsToRecalculate(name))
            {
                output.Add(cell);
                calculateCell(cell);
            }
            return output;

        }
        /// <summary>
        /// Calculates the Value of the cell
        /// </summary>
        /// <param name="">Adds the value of the cell to the cellValues Dictionary</param>
        private void calculateCell(string name)
        {
            name = ValidateAndNormalize(name);
            if (cellDictionary.ContainsKey(name) && cellDictionary[name].isFormula)
            {
                Formula contentFormula = new Formula(GetContentsWithoutEquals(name).ToString(), Normalize, IsValid);
                object Value = contentFormula.Evaluate(ValueLookupFunc);
                if (Value is FormulaError)
                {
                    if (valueLookup.ContainsKey(name))
                        valueLookup.Remove(name);
                }
                else
                {
                    if (!valueLookup.ContainsKey(name))
                        valueLookup.Add(name, (double)Value);
                    else
                        valueLookup[name] = double.Parse(Value.ToString());
                }
            }
        }
        /// <summary>
        /// Set the contents of the named cell to the formula.  
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException"> 
        ///   If formula parameter is null, throw an ArgumentNullException.
        /// </exception>
        /// 
        /// <requires> 
        ///   The name parameter must be non null and valid
        /// </requires>
        /// 
        /// <exception cref="CircularException"> 
        ///   If changing the contents of the named cell to be the formula would 
        ///   cause a circular dependency, throw a CircularException.  
        ///   (NOTE: No change is made to the spreadsheet.)
        /// </exception>
        /// 
        /// <param name="name"> The cell name</param>
        /// <param name="formula"> The content of the cell</param>
        /// 
        /// <returns>
        ///   <para>
        ///       This method returns a LIST consisting of the passed in name followed by the names of all 
        ///       other cells whose value depends, directly or indirectly, on the named cell.
        ///   </para>
        ///
        ///   <para>
        ///       The order must correspond to a valid dependency ordering for recomputing
        ///       all of the cells, i.e., if you re-evaluate each cell in the order of the list,
        ///       the overall spreadsheet will be consistently updated.
        ///   </para>
        ///
        ///   <para>
        ///     For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        ///     set {A1, B1, C1} is returned, i.e., A1 was changed, so then A1 must be 
        ///     evaluated, followed by B1 re-evaluated, followed by C1 re-evaluated.
        ///   </para>
        /// </returns>
        protected override IList<String> SetCellContents(String name, Formula formula)
        {
            //Checks for circular loop
            foreach (string dependee in formula.GetVariables())
            {
                if (IsCircle(name, dependee))
                {
                    Changed = false;
                    throw new CircularException();
                }
            }
            //Adds a new cell if one doesn't exist already 
            if (!cellDictionary.ContainsKey(name))
                cellDictionary.Add(name, new Cell(formula));
            //Set cell 
            cellDictionary[name].isFormula = true;
            cellDictionary[name].contents = formula;
            //Replaces dependees
            dependencies.ReplaceDependees(name, formula.GetVariables());
            //Recalculates all dependents
            List<String> output = new List<String>();
            output.Add(name);
            foreach (string cell in GetCellsToRecalculate(name))
            {
                calculateCell(cell);
                output.Add(cell);
            }
            return output;

        }
        /// <summary>
        /// Determines whether there is a infinite loop created by adding a formula to a cell
        /// </summary>
        /// <param name="name">name of the cell</param>
        /// <param name="key">name of the cell being added (if name = key it is an infinite loop)</param>
        /// <returns></returns>
        private Boolean IsCircle(string name, string key)
        {
            if (name.Equals(key))
                return true;
            if (dependencies.HasDependents(name))
                foreach (string dependent in dependencies.GetDependents(name))
                {
                    if (dependent.Equals(key))
                        return true;
                    return IsCircle(dependent, key);
                }
            return false;
        }
        /// <summary>
        /// Returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell. 
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException"> 
        ///   If the name is null, throw an ArgumentNullException.
        /// </exception>
        /// 
        /// <exception cref="InvalidNameException"> 
        ///   If the name is null or invalid, throw an InvalidNameException
        /// </exception>
        /// 
        /// <param name="name"></param>
        /// <returns>
        ///   Returns an enumeration, without duplicates, of the names of all cells that contain
        ///   formulas containing name.
        /// 
        ///   <para>For example, suppose that: </para>
        ///   <list type="bullet">
        ///      <item>A1 contains 3</item>
        ///      <item>B1 contains the formula A1 * A1</item>
        ///      <item>C1 contains the formula B1 + A1</item>
        ///      <item>D1 contains the formula B1 - C1</item>
        ///   </list>
        /// 
        ///   <para>The direct dependents of A1 are B1 and C1</para>
        /// 
        /// </returns>
        protected override IEnumerable<String> GetDirectDependents(String name)
        {
            List<string> output = new List<string>();
            foreach (string cell in dependencies.GetDependents(name))
            {
                output.Add(cell);
            }
            return output;
        }
        /// <summary>
        /// Returns the version information of the spreadsheet saved in the named file.
        /// If there are any problems opening, reading, or closing the file, the method
        /// should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override string GetSavedVersion(String filename)
        {
            string errorMessage = "Error Reading the File";
            string loadVersion = "";
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:

                                if (reader.Name.Equals("spreadsheet"))
                                {
                                    loadVersion = reader.GetAttribute("version");
                                    reader.Close();
                                    return loadVersion;
                                }
                                break;
                        }

                    }

                }
            }
            catch (Exception)
            {
                throw new SpreadsheetReadWriteException(errorMessage);
            }
            return loadVersion;
        }
        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>cell name goes here</name>
        /// <contents>cell contents goes here</contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.  
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// 
        /// If there are any problems opening, writing, or closing the file, the method should throw a
        /// SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override void Save(String filename)
        {
            try
            {
                //XmlWriterSettings settings = new XmlWriterSettings();
                //settings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(filename))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    writer.WriteAttributeString("version", Version);
                    foreach (string key in GetNamesOfAllNonemptyCells())
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("name", key);
                        writer.WriteElementString("contents", GetCellContents(key).ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (Exception)
            {
                throw new SpreadsheetReadWriteException("Error in Saving the File");
            }
            Changed = false;

        }
        /// <summary>
        /// Delegate for the valueLookup dictionary
        /// </summary>
        /// <param name="name">Value being looked for</param>
        /// <returns>Value associated with the name</returns>
        private double ValueLookupFunc(string name)
        {
            if (valueLookup.ContainsKey(name))
                return valueLookup[name];
            else
            {
                return (double)GetCellValue(name);
            }
        }
        /// <summary>
        /// Checks the String if the name is valid, if so it calls the IsValid Delegate
        /// </summary>
        /// <param name="name">Name to be validated</param>
        /// <param name="IsValid">Delegate passed in by the constuctor</param>
        /// <returns></returns>
        private static Boolean ValidVariable(string name, Func<string, bool> IsValid)
        {
            if (name.Length < 2)
                return false;
            char[] characters = name.ToCharArray();
            int counter = 0;
            if (!char.IsLetter(characters[0]))
                return false;
            //loops through all letters
            while (char.IsLetter(characters[counter]))
                counter++;
            //loops throught all numbers
            while (counter < characters.Length && char.IsDigit(characters[counter]))
                counter++;
            if (counter == characters.Length)
                return IsValid(name);
            return false;

        }
        /// <summary>
        /// Validates the name isn't null and is a valid variable and returns the name normalized by the delegate
        /// </summary>
        /// <param name="name">Name being validated and Normalized</param>
        /// <returns>Normalized name</returns>
        private string ValidateAndNormalize(string name)
        {
            if (name == null || !ValidVariable(name, IsValid))
                throw new InvalidNameException();
            else
            {
                return Normalize(name);
            }
        }
        /// <summary>
        /// Reads the filename and converts the Xml to a spreadsheet
        /// </summary>
        /// <param name="filename">name of the file</param>
        private void XmlReadFile(string filename)
        {
            string errorMessage = "Error Reading the File";
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (reader.Name.Equals("cell"))
                                {
                                    while (reader.Read())
                                    {
                                        bool setCell = false;
                                        switch (reader.NodeType)
                                        {
                                            case XmlNodeType.Element:
                                                string cellName = reader.ReadElementContentAsString();
                                                //reader.Read();
                                                string cellContents = reader.ReadElementContentAsString();
                                                setCell = true;
                                                try
                                                {
                                                    SetContentsOfCell(cellName, cellContents);
                                                }
                                                catch (Exception)
                                                {
                                                    errorMessage = "Cell Contents or Name was Invalid";
                                                    throw new Exception();
                                                }
                                                break;
                                        }
                                        if (setCell)
                                        {
                                            break;
                                        }

                                    }
                                }
                                break;

                        }

                    }

                }
            }
            catch (Exception)
            {
                throw new SpreadsheetReadWriteException(errorMessage);
            }
        }
        /// <summary>
        /// checks if the cell contents are the same as the new contents
        /// </summary>
        /// <param name="name">cell name</param>
        /// <param name="content">new contents</param>
        /// <returns>true if they are the same</returns>
        private bool SameContents(string name, string content)
        {
            if (cellDictionary.ContainsKey(name))
            {
                if (GetCellContents(name).ToString().Equals(content))
                    return true;
            }
            return false;
        }
    }
}


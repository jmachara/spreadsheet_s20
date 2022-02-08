using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Xml;
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
/// Contains Tests to test the spreadsheet.cs file
/// </summary>
namespace SS
{
    [TestClass]
    public class SpreadsheetTests
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext tc)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create("SaveForTests", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "default");

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "hello");
                writer.WriteEndElement();
                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A2");
                writer.WriteElementString("contents", "44");
                writer.WriteEndElement();
                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "C3");
                writer.WriteElementString("contents", "=A2");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create("SaveForTestsNullVersion", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", null);

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "hello");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            using (XmlWriter writer = XmlWriter.Create("InvalidCell", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "default");

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "231");
                writer.WriteElementString("contents", "hello");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Test Constructor Section
        /// </summary>

        [TestMethod]
        public void NoArgConstructorTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            Assert.IsTrue(SS is Spreadsheet);
        }
        [TestMethod]
        public void ThreeArgConstructorTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet(s => true, s => s, "default");
            Assert.IsTrue(SS is Spreadsheet);
        }
        [TestMethod]
        public void FourArgConstrutorTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet("SaveForTests", s => true, s => s, "default");
            Assert.IsTrue(SS is Spreadsheet);
            List<string> cells = new List<string>();
            cells.Add("A1"); cells.Add("A2"); cells.Add("C3");
            Assert.AreEqual(cells.ToString(), SS.GetNamesOfAllNonemptyCells().ToString());
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void ThreeArgConstructorErrorNullVersionTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet(s => true, s => s, null);
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void FourArgConstructorErrorNullVersionTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet("SaveForTests", s => true, s => s, null);
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void FourArgConstructorErrorNullFilenameTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet(null, s => true, s => s, "default");
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void FourArgConstructorInvalidCellTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet("InvalidCell", s => true, s => s, "default");
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void FourArgConstructorErrorNullisValidTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet("SaveForTests", null, s => s, "default");
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void FourArgConstructorErrorNullNormalizeTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet("SaveForTests", s => true, null, "default");
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void ThreeArgConstructorErrorNullisValidTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet(null, s => s, "default");
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void ThreeArgConstructorErrorNullNormalizeTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet(s => true, null, "default");
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void FourArgConstructorLoadVersionDifferentTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet("SaveForTests", null, s => s, "default.2");
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void VoidVersionSaved()
        {
            AbstractSpreadsheet SS = new Spreadsheet("SaveForTestsNullVersion", s => true, s => s, "default");
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void MissMatchVersionSaved()
        {
            AbstractSpreadsheet SS = new Spreadsheet("SaveForTestsNullVersion", s => true, s => s, "default3");
        }

        //GetAllOfNonEmptyCellsTests
        [TestMethod]
        public void GetAllNonEmptyCellsEmptyTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            Assert.IsTrue(SS.GetNamesOfAllNonemptyCells().ToString().Equals(new List<string>().ToString()));
        }
        [TestMethod]
        public void GetAllNonEmptyCellsFullTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.SetContentsOfCell("A1", "100");
            SS.SetContentsOfCell("D1", "Hi");
            SS.SetContentsOfCell("AAA1", "DAD");
            List<string> answerList = new List<string>();
            answerList.Add("A1");
            answerList.Add("D1");
            answerList.Add("AAA1");
            Assert.IsTrue(SS.GetNamesOfAllNonemptyCells().ToString().Equals(answerList.ToString()));
        }
        //GetCellValueTests
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellValueEmptyTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.GetCellValue("A3");
        }
        [TestMethod]
        public void GetCellValueStringTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.SetContentsOfCell("A3", "Hi Mom I hope you are doing well");
            Assert.IsTrue(SS.GetCellContents("A3").ToString().Equals("Hi Mom I hope you are doing well"));
            Assert.IsTrue(SS.GetCellValue("A3").ToString().Equals("Hi Mom I hope you are doing well"));
        }
        [TestMethod]
        public void GetCellValuedependeesTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.SetContentsOfCell("A1", "100");
            Assert.IsTrue(SS.GetCellValue("A1").ToString().Equals("100"));
            SS.SetContentsOfCell("B3", "=A1*4");
            Assert.IsTrue(SS.GetCellValue("B3").ToString().Equals("400"));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCellValueNullTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.GetCellValue(null);
        }
        //GetCellContentTests
        [TestMethod]
        public void GetCellContentTestEquation()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.SetContentsOfCell("A2", "78");
            Assert.IsTrue(SS.GetCellContents("A2").ToString().Equals("78"));
            SS.SetContentsOfCell("A4", "22");
            Assert.IsTrue(SS.GetCellContents("A4").ToString().Equals("22"));
            SS.SetContentsOfCell("A7", "=A2*A4");
            Assert.IsTrue(SS.GetCellContents("A7").ToString().Equals("=A2*A4"));
        }
        [TestMethod]
        public void GetCellContentInvalidEquation()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.SetContentsOfCell("A2", "78");
            Assert.IsTrue(SS.GetCellContents("A4").ToString().Equals(""));

        }
        //SetContentsOfCellsTests
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetContentsOfCellInvalidNameTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.SetContentsOfCell("__1A1", "100");

        }
        [TestMethod]
        public void SetContentsOfCellSameValTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.SetContentsOfCell("A1", "100");
            Assert.IsTrue(SS.Changed);
            SS.Save("RandomSave");
            Assert.IsTrue(SS.Version.Equals(SS.GetSavedVersion("RandomSave")));
            Assert.IsFalse(SS.Changed);
            SS.SetContentsOfCell("A1", "100");
            Assert.IsFalse(SS.Changed);
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void SetContentInCircleTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.SetContentsOfCell("A1", "100");
            Assert.IsTrue(SS.Changed);
            SS.Save("RandomSave");
            Assert.IsTrue(SS.Version.Equals(SS.GetSavedVersion("RandomSave")));
            Assert.IsFalse(SS.Changed);
            SS.SetContentsOfCell("A2", "=A2");
            Assert.IsFalse(SS.Changed);
        }
        //StressTests
        [TestMethod]
        [Timeout(3000)]
        public void SetGetSaveLoadStressTest()
        {
            AbstractSpreadsheet SS = new Spreadsheet(s => true, s => s, "default");
            List<string> correctList = new List<String>();
            for (int i = 0; i < 500; i++)
            {
                correctList.Add("A1" + i);
                SS.SetContentsOfCell("A1" + i, "i");
            }
            Assert.IsTrue(correctList.ToString().Equals(SS.GetNamesOfAllNonemptyCells().ToString()));
            for (int i = 0; i < 500; i++)
            {
                Assert.IsTrue(SS.Changed);
                SS.Save("SaveFileStress");
                Assert.IsFalse(SS.Changed);
                //clear list
                for (int j = 0; j < 500; j++)
                {
                    SS.SetContentsOfCell("A1" + j, "");
                }
                Assert.AreEqual(SS.GetNamesOfAllNonemptyCells().ToString(), new List<string>().ToString());
                SS = new Spreadsheet("SaveFileStress", s => true, s => s, "default");
                Assert.AreEqual(SS.GetNamesOfAllNonemptyCells().ToString(), correctList.ToString());
            }
            correctList.Clear();
            for (int i = 0; i < 500; i++)
            {
                correctList.Add(SS.GetCellValue("A1" + i).ToString());
                SS.SetContentsOfCell("A1" + i, "");

            }
            SS = new Spreadsheet("SaveFileStress", s => true, s => s, "default");
            for (int i = 0; i < 500; i++)
            {
                Assert.AreEqual(SS.GetCellValue("A1" + i).ToString(), correctList[i]);
            }
        }





        //OLD TESTS REFACTORED
        //GetNamesOfAllNonemptyCellsTests
        [TestMethod]
        public void TestGetNameOfAllCellsEmpty()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            List<string> emptyList = new List<string>();
            Assert.IsTrue(emptyList.ToString().Equals(SS.GetNamesOfAllNonemptyCells().ToString()));
        }
        [TestMethod]
        public void TestGetNameOfAllCellsWithCells()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            List<string> cellList = new List<string>();
            cellList.Add("A1");
            cellList.Add("B2");
            SS.SetContentsOfCell("A1", "38.2");
            SS.SetContentsOfCell("B2", "Hello Mom");
            Assert.IsTrue(cellList.ToString().Equals(SS.GetNamesOfAllNonemptyCells().ToString()));
        }
        //GetCellContentsTests
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsNull()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            String input = null;
            SS.GetCellContents(input);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidCellName()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.GetCellContents("A34+");
        }

        [TestMethod]
        public void TestGetCellContentsDouble()
        {
            AbstractSpreadsheet SS = new Spreadsheet();
            SS.SetContentsOfCell("A1", "38.2");
            SS.SetContentsOfCell("B2", "Hello Mom");
            Assert.AreEqual(38.2, SS.GetCellContents("A1"));
            Assert.AreEqual("", SS.GetCellContents("C2").ToString());
        }
        //SetContentsOfCellTestsDouble
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNameNullDouble()
        {
            AbstractSpreadsheet spreadSheet = new Spreadsheet();
            spreadSheet.SetContentsOfCell(null, "8.0");
        }
        //SetContentsOfCellTestsString
        [TestMethod]
        public void TestSetCellEmptyContents()
        {
            AbstractSpreadsheet spreadSheet = new Spreadsheet();
            spreadSheet.SetContentsOfCell("A1", "");
            spreadSheet.SetContentsOfCell("B1", "Hi");
            List<string> rightList = new List<string>();
            rightList.Add("B1");
            Assert.AreEqual(rightList.ToString(), spreadSheet.GetNamesOfAllNonemptyCells().ToString());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetNullString()
        {
            AbstractSpreadsheet spreadSheet = new Spreadsheet();
            String input = null;
            spreadSheet.SetContentsOfCell("A1", input);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNameEmptyString()
        {
            AbstractSpreadsheet spreadSheet = new Spreadsheet();
            spreadSheet.SetContentsOfCell("", "Hi Mom");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNameNullString()
        {
            AbstractSpreadsheet spreadSheet = new Spreadsheet();
            spreadSheet.SetContentsOfCell(null, "Hi Mom");
        }
        //SetContentsOfCellTestsFormula
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetContentsOfCellFormulaNull()
        {
            AbstractSpreadsheet spreadSheet = new Spreadsheet();
            spreadSheet.SetContentsOfCell("A1", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNameNullFormula()
        {
            AbstractSpreadsheet spreadSheet = new Spreadsheet();
            spreadSheet.SetContentsOfCell(null, "=1+1");
        }
        [TestMethod]
        public void TestCircularLoopPassFourCells()
        {
            AbstractSpreadsheet spreadSheet = new Spreadsheet();
            List<string> expected = new List<string>();
            spreadSheet.SetContentsOfCell("A1", "=B1*2");
            spreadSheet.SetContentsOfCell("B1", "=C1*2");
            spreadSheet.SetContentsOfCell("C1", "=D5+2");
            spreadSheet.SetContentsOfCell("D5", "18");
            expected.Add("A1");
            expected.Add("B1");
            expected.Add("C1");
            expected.Add("D5");
            Assert.IsTrue(expected.ToString().Equals(spreadSheet.GetNamesOfAllNonemptyCells().ToString()));
        }

        [TestMethod]
        public void TestSetCellDouble()
        {
            AbstractSpreadsheet spreadSheet = new Spreadsheet();
            spreadSheet.SetContentsOfCell("A1", "25.6");
            spreadSheet.SetContentsOfCell("A1", "14.9");
            Assert.AreEqual(14.9, spreadSheet.GetCellContents("A1"));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary> 
/// Author:    Brian Dong and Jack Machara
/// Partner:   None
/// Date:      2/27/2020
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Brian Dong - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Brian Dong and Jach Machara, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source, besides the provided starter gui code.
/// All references used in the completion of the assignment are cited in my README file. 
/// 
/// Provides a gui interface for a spreadsheet, and ensuring its general functionality/support
/// </summary>
namespace SpreadsheetGUI
{
    class Spreadsheet_Window : ApplicationContext
    {
        /// <summary>
        ///  Number of open forms
        /// </summary>
        private int formCount = 0;

        /// <summary>
        ///  Singleton ApplicationContext
        /// </summary>
        private static Spreadsheet_Window appContext;

        /// <summary>
        /// Returns the one DemoApplicationContext.
        /// </summary>
        public static Spreadsheet_Window getAppContext()
        {
            if (appContext == null)
            {
                appContext = new Spreadsheet_Window();
            }
            return appContext;
        }

        /// <summary>
        /// Private constructor for singleton pattern
        /// </summary>
        private Spreadsheet_Window()
        {
        }

        /// <summary>
        /// Build another GUI Window
        /// </summary>
        public void RunForm(Form form)
        {
            // One more form is running
            formCount++;

            // Assign an EVENT handler to take an action when the GUI is closed 
            form.FormClosed += (o, e) => { if (--formCount <= 0) ExitThread(); };

            // Run the form
            form.Show();
        }

    }
    class SpreadsheetGUI
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start an application context and run one form inside it
            Spreadsheet_Window appContext = Spreadsheet_Window.getAppContext();
            appContext.RunForm(new SpreadsheetView());
            Application.Run(appContext);

        }
    }
}

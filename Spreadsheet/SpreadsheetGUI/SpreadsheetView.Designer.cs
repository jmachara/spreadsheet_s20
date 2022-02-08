
/// <summary>
///   Original Author: Joe Zachary
///   Further Authors: H. James de St. Germain
///   
///   Dates          : 2012-ish - Original 
///                    2020     - Updated for use with ASP Core
///                    
///   This code represents a Windows Form element for a Spreadsheet
///   
///   This code is the "auto-generated" portion of the SimpleSpreadsheetGUI.
///   See the SimpleSpreadsheetGUI.cs for "hand-written" code.
///  
/// </summary>

using SpreadsheetGrid_Framework;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


/// <summary> 
/// Author:    Brian Dong and Jack Machara
/// Partner:   None
/// Date:      2/27/2020
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Brian Dong - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Brian Dong and Jach Machara, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source, besides the provided starter gui code that may be credited as such.
/// Original Author: Joe Zachary
///   Further Authors: H. James de St. Germain
///   
///   Dates          : 2012-ish - Original 
///                    2020     - Updated for use with ASP Core
/// All references used in the completion of the assignment are cited in my README file. 
/// 
/// Provides a gui interface for a spreadsheet, and ensuring its general functionality/support
/// </summary>
namespace SpreadsheetGUI
{
    partial class SpreadsheetView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BGWorker = new BackgroundWorker();
            this.MainControlArea = new System.Windows.Forms.TableLayoutPanel();
            this.input_textbox = new System.Windows.Forms.TextBox();
            this.CellName = new System.Windows.Forms.TextBox();
            this.CellValue = new System.Windows.Forms.TextBox();
            this.ContentLabel = new System.Windows.Forms.Label();
            this.CurrentCellName = new System.Windows.Forms.Label();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.grid_widget = new SpreadsheetGrid_Framework.SpreadsheetGridWidget();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AddingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.MainControlArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(779, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddingToolStripMenuItem,
            this.savingToolStripMenuItem,
            this.loadingToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            

            // 
            // MainControlArea
            // 
            this.MainControlArea.AutoSize = true;
            this.MainControlArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainControlArea.BackColor = System.Drawing.Color.DarkRed;
            this.MainControlArea.Controls.Add(this.input_textbox);
            this.MainControlArea.Controls.Add(this.CellValue);
            this.MainControlArea.Controls.Add(this.CellName);
            this.MainControlArea.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainControlArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainControlArea.Location = new System.Drawing.Point(4, 4);
            this.MainControlArea.Margin = new System.Windows.Forms.Padding(4);
            this.MainControlArea.MinimumSize = new System.Drawing.Size(133, 123);
            this.MainControlArea.Name = "MainControlArea";
            this.MainControlArea.Size = new System.Drawing.Size(771, 123);
            this.MainControlArea.TabIndex = 4;
            this.MainControlArea.Controls.Add(this.CurrentCellName,0, 0);
            this.MainControlArea.Controls.Add(this.ValueLabel, 0, 1);
            this.MainControlArea.Controls.Add(this.ContentLabel, 0, 2);
            this.MainControlArea.Controls.Add(this.CellName, 1, 0);
            this.MainControlArea.Controls.Add(this.CellValue, 1, 1);
            this.MainControlArea.Controls.Add(this.input_textbox, 1, 2);
            
            //
            //CurrentCellName
            //
            this.CurrentCellName.Text = "Cell Name:";
            this.CurrentCellName.Size = new System.Drawing.Size(100, 30);
            this.CurrentCellName.BackColor = Color.Gray;

            //
            //ContentLabel
            //
            this.ContentLabel.Text = "Contents";
            this.ContentLabel.Size = new System.Drawing.Size(100, 30);
            this.ContentLabel.BackColor = Color.Gray;

            //
            //ValueLabel
            //
            this.ValueLabel.Text = "Value";
            this.ValueLabel.Size = new System.Drawing.Size(100, 30);
            this.ValueLabel.BackColor = Color.Gray;

            // 
            // input_textbox
            // 
            this.input_textbox.Location = new System.Drawing.Point(4, 4);
            this.input_textbox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.input_textbox.Name = "input_textbox";
            this.input_textbox.Size = new System.Drawing.Size(600, 22);
            this.input_textbox.TabIndex = 2;
            this.input_textbox.TextChanged += new System.EventHandler(this.input_textbox_TextChanged);
            //
            //CellName
            //
            this.CellName.Location = new System.Drawing.Point(4, 4);
            this.CellName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.CellName.Name = "CellName";
            this.CellName.Size = new System.Drawing.Size(40, 22);
            this.CellName.TabIndex = 2;
            this.CellName.ReadOnly = true;
            //
            //CellValue
            //
            this.CellValue.Location = new System.Drawing.Point(4, 4);
            this.CellValue.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.CellValue.Name = "CellValue";
            this.CellValue.Size = new System.Drawing.Size(600, 22);
            this.CellValue.TabIndex = 2;
            this.CellValue.ReadOnly = true;

            // 
            // grid_widget
            // 
            this.grid_widget.AutoSize = true;
            this.grid_widget.BackColor = System.Drawing.Color.Gray;
            this.grid_widget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_widget.Location = new System.Drawing.Point(4, 127);
            this.grid_widget.Margin = new System.Windows.Forms.Padding(4);
            this.grid_widget.MaximumSize = new System.Drawing.Size(2800, 2462);
            this.grid_widget.Name = "grid_widget";
            this.grid_widget.Size = new System.Drawing.Size(771, 285);
            this.grid_widget.TabIndex = 0;
            this.grid_widget.SelectionChanged += new SpreadsheetGrid_Framework.SelectionChangedHandler(this.grid_widget_SelectionChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.MainControlArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grid_widget, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(779, 416);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // AddingHelpToolStripMenuItem
            // 
            this.AddingToolStripMenuItem.Name = "AddingToolStripMenuItem";
            this.AddingToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.AddingToolStripMenuItem.Text = "Adding";
            this.AddingToolStripMenuItem.Click += new System.EventHandler(this.addingToolStripMenuItem_Click);

            // 
            // savingHelpToolStripMenuItem
            // 
            this.savingToolStripMenuItem.Name = "savingToolStripMenuItem";
            this.savingToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.savingToolStripMenuItem.Text = "Saving";
            this.savingToolStripMenuItem.Click += new System.EventHandler(this.savingToolStripMenuItem_Click);
            // 
            // loadingHelpToolStripMenuItem
            // 
            this.loadingToolStripMenuItem.Name = "loadingToolStripMenuItem";
            this.loadingToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.loadingToolStripMenuItem.Text = "Loading";
            this.loadingToolStripMenuItem.Click += new System.EventHandler(this.loadingToolStripMenuItem_Click);
            // 
            // SimpleSpreadsheetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 444);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SimpleSpreadsheetGUI";
            this.Text = "Basic Spreadsheet";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.MainControlArea.ResumeLayout(false);
            this.MainControlArea.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            //
            //BGWorker
            //
            BGWorker.DoWork += CalculateCells;
            BGWorker.RunWorkerCompleted += compute_done;

        }



        #endregion


        private SpreadsheetGridWidget grid_widget;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;

        private TableLayoutPanel MainControlArea;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox input_textbox;
        private TextBox CellName;
        private TextBox CellValue;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem AddingToolStripMenuItem;
        private ToolStripMenuItem savingToolStripMenuItem;
        private ToolStripMenuItem loadingToolStripMenuItem;
        private Label ContentLabel;
        private Label ValueLabel;
        private Label CurrentCellName;
        private BackgroundWorker BGWorker;
    }
}

﻿namespace midtermProgLab
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lexical = new FontAwesome.Sharp.IconButton();
            this.zoomOutCode = new FontAwesome.Sharp.IconButton();
            this.zoomInCode = new FontAwesome.Sharp.IconButton();
            this.saveAs = new FontAwesome.Sharp.IconButton();
            this.savefile = new FontAwesome.Sharp.IconButton();
            this.undo = new FontAwesome.Sharp.IconButton();
            this.redo = new FontAwesome.Sharp.IconButton();
            this.stop = new FontAwesome.Sharp.IconButton();
            this.start = new FontAwesome.Sharp.IconButton();
            this.searchButton = new FontAwesome.Sharp.IconButton();
            this.searchText = new Bunifu.UI.WinForms.BunifuTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MyPictureBox = new System.Windows.Forms.PictureBox();
            this.MyRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MyPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lexical);
            this.panel1.Controls.Add(this.zoomOutCode);
            this.panel1.Controls.Add(this.zoomInCode);
            this.panel1.Controls.Add(this.saveAs);
            this.panel1.Controls.Add(this.savefile);
            this.panel1.Controls.Add(this.undo);
            this.panel1.Controls.Add(this.redo);
            this.panel1.Controls.Add(this.stop);
            this.panel1.Controls.Add(this.start);
            this.panel1.Controls.Add(this.searchButton);
            this.panel1.Controls.Add(this.searchText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1163, 77);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::midtermProgLab.Properties.Resources.Cobra1;
            this.pictureBox1.Location = new System.Drawing.Point(10, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(108, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // lexical
            // 
            this.lexical.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lexical.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lexical.FlatAppearance.BorderSize = 0;
            this.lexical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lexical.IconChar = FontAwesome.Sharp.IconChar.BookJournalWhills;
            this.lexical.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lexical.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.lexical.IconSize = 30;
            this.lexical.Location = new System.Drawing.Point(507, 25);
            this.lexical.Name = "lexical";
            this.lexical.Size = new System.Drawing.Size(45, 40);
            this.lexical.TabIndex = 12;
            this.lexical.UseVisualStyleBackColor = true;
            this.lexical.Click += new System.EventHandler(this.lexical_Click);
            // 
            // zoomOutCode
            // 
            this.zoomOutCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.zoomOutCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomOutCode.FlatAppearance.BorderSize = 0;
            this.zoomOutCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomOutCode.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlassMinus;
            this.zoomOutCode.IconColor = System.Drawing.Color.Snow;
            this.zoomOutCode.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.zoomOutCode.IconSize = 30;
            this.zoomOutCode.Location = new System.Drawing.Point(757, 25);
            this.zoomOutCode.Name = "zoomOutCode";
            this.zoomOutCode.Size = new System.Drawing.Size(45, 40);
            this.zoomOutCode.TabIndex = 11;
            this.zoomOutCode.UseVisualStyleBackColor = true;
            this.zoomOutCode.Click += new System.EventHandler(this.zoomOutCode_Click);
            // 
            // zoomInCode
            // 
            this.zoomInCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.zoomInCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomInCode.FlatAppearance.BorderSize = 0;
            this.zoomInCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomInCode.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlassPlus;
            this.zoomInCode.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.zoomInCode.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.zoomInCode.IconSize = 30;
            this.zoomInCode.Location = new System.Drawing.Point(706, 25);
            this.zoomInCode.Name = "zoomInCode";
            this.zoomInCode.Size = new System.Drawing.Size(45, 40);
            this.zoomInCode.TabIndex = 10;
            this.zoomInCode.UseVisualStyleBackColor = true;
            this.zoomInCode.Click += new System.EventHandler(this.zoomInCode_Click);
            // 
            // saveAs
            // 
            this.saveAs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveAs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveAs.FlatAppearance.BorderSize = 0;
            this.saveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAs.IconChar = FontAwesome.Sharp.IconChar.Copy;
            this.saveAs.IconColor = System.Drawing.Color.DeepSkyBlue;
            this.saveAs.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.saveAs.IconSize = 30;
            this.saveAs.Location = new System.Drawing.Point(160, 23);
            this.saveAs.Name = "saveAs";
            this.saveAs.Size = new System.Drawing.Size(45, 40);
            this.saveAs.TabIndex = 9;
            this.saveAs.UseVisualStyleBackColor = true;
            this.saveAs.Click += new System.EventHandler(this.saveAs_Click);
            // 
            // savefile
            // 
            this.savefile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.savefile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.savefile.FlatAppearance.BorderSize = 0;
            this.savefile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savefile.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.savefile.IconColor = System.Drawing.Color.DeepSkyBlue;
            this.savefile.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.savefile.IconSize = 30;
            this.savefile.Location = new System.Drawing.Point(201, 23);
            this.savefile.Name = "savefile";
            this.savefile.Size = new System.Drawing.Size(45, 40);
            this.savefile.TabIndex = 8;
            this.savefile.UseVisualStyleBackColor = true;
            this.savefile.Click += new System.EventHandler(this.savefile_Click);
            // 
            // undo
            // 
            this.undo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.undo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.undo.FlatAppearance.BorderSize = 0;
            this.undo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.undo.IconChar = FontAwesome.Sharp.IconChar.RotateBackward;
            this.undo.IconColor = System.Drawing.Color.White;
            this.undo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.undo.IconSize = 30;
            this.undo.Location = new System.Drawing.Point(288, 23);
            this.undo.Name = "undo";
            this.undo.Size = new System.Drawing.Size(45, 40);
            this.undo.TabIndex = 7;
            this.undo.UseVisualStyleBackColor = true;
            this.undo.Click += new System.EventHandler(this.undo_Click);
            // 
            // redo
            // 
            this.redo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.redo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.redo.FlatAppearance.BorderSize = 0;
            this.redo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.redo.IconChar = FontAwesome.Sharp.IconChar.RotateForward;
            this.redo.IconColor = System.Drawing.Color.White;
            this.redo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.redo.IconSize = 30;
            this.redo.Location = new System.Drawing.Point(324, 23);
            this.redo.Name = "redo";
            this.redo.Size = new System.Drawing.Size(45, 40);
            this.redo.TabIndex = 6;
            this.redo.UseVisualStyleBackColor = true;
            this.redo.Click += new System.EventHandler(this.redo_Click);
            // 
            // stop
            // 
            this.stop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.stop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stop.FlatAppearance.BorderSize = 0;
            this.stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stop.IconChar = FontAwesome.Sharp.IconChar.Stop;
            this.stop.IconColor = System.Drawing.Color.Red;
            this.stop.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.stop.IconSize = 30;
            this.stop.Location = new System.Drawing.Point(456, 26);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(45, 40);
            this.stop.TabIndex = 3;
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // start
            // 
            this.start.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.start.FlatAppearance.BorderSize = 0;
            this.start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start.IconChar = FontAwesome.Sharp.IconChar.Play;
            this.start.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.start.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.start.IconSize = 30;
            this.start.Location = new System.Drawing.Point(405, 26);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(45, 40);
            this.start.TabIndex = 2;
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.searchButton.IconColor = System.Drawing.Color.White;
            this.searchButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.searchButton.Location = new System.Drawing.Point(827, 29);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(45, 40);
            this.searchButton.TabIndex = 1;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchText
            // 
            this.searchText.AcceptsReturn = false;
            this.searchText.AcceptsTab = false;
            this.searchText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchText.AnimationSpeed = 200;
            this.searchText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.searchText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.searchText.BackColor = System.Drawing.Color.Transparent;
            this.searchText.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("searchText.BackgroundImage")));
            this.searchText.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.searchText.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.searchText.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.searchText.BorderColorIdle = System.Drawing.Color.Silver;
            this.searchText.BorderRadius = 20;
            this.searchText.BorderThickness = 1;
            this.searchText.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.searchText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.searchText.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.searchText.DefaultText = "";
            this.searchText.FillColor = System.Drawing.Color.White;
            this.searchText.HideSelection = true;
            this.searchText.IconLeft = null;
            this.searchText.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.searchText.IconPadding = 10;
            this.searchText.IconRight = null;
            this.searchText.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.searchText.Lines = new string[0];
            this.searchText.Location = new System.Drawing.Point(875, 28);
            this.searchText.MaxLength = 32767;
            this.searchText.MinimumSize = new System.Drawing.Size(1, 1);
            this.searchText.Modified = false;
            this.searchText.Multiline = false;
            this.searchText.Name = "searchText";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.searchText.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.searchText.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.searchText.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.searchText.OnIdleState = stateProperties4;
            this.searchText.Padding = new System.Windows.Forms.Padding(3);
            this.searchText.PasswordChar = '\0';
            this.searchText.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.searchText.PlaceholderText = "Search Text";
            this.searchText.ReadOnly = false;
            this.searchText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.searchText.SelectedText = "";
            this.searchText.SelectionLength = 0;
            this.searchText.SelectionStart = 0;
            this.searchText.ShortcutsEnabled = true;
            this.searchText.Size = new System.Drawing.Size(260, 37);
            this.searchText.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.searchText.TabIndex = 0;
            this.searchText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.searchText.TextMarginBottom = 0;
            this.searchText.TextMarginLeft = 3;
            this.searchText.TextMarginTop = 0;
            this.searchText.TextPlaceholder = "Search Text";
            this.searchText.UseSystemPasswordChar = false;
            this.searchText.WordWrap = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1163, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveFileToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.openFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newFileToolStripMenuItem.Text = "New File";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(121, 6);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.redoToolStripMenuItem.Text = "Re-do";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 101);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1163, 24);
            this.panel3.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.MyPictureBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(57, 406);
            this.panel2.TabIndex = 5;
            // 
            // MyPictureBox
            // 
            this.MyPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyPictureBox.Location = new System.Drawing.Point(0, 0);
            this.MyPictureBox.Name = "MyPictureBox";
            this.MyPictureBox.Size = new System.Drawing.Size(57, 406);
            this.MyPictureBox.TabIndex = 0;
            this.MyPictureBox.TabStop = false;
            this.MyPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // MyRichTextBox
            // 
            this.MyRichTextBox.AcceptsTab = true;
            this.MyRichTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(85)))));
            this.MyRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyRichTextBox.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MyRichTextBox.ForeColor = System.Drawing.SystemColors.Info;
            this.MyRichTextBox.Location = new System.Drawing.Point(57, 125);
            this.MyRichTextBox.Name = "MyRichTextBox";
            this.MyRichTextBox.Size = new System.Drawing.Size(1106, 406);
            this.MyRichTextBox.TabIndex = 7;
            this.MyRichTextBox.Text = "";
            this.MyRichTextBox.WordWrap = false;
            this.MyRichTextBox.VScroll += new System.EventHandler(this.MyRichTextBox_VScroll);
            this.MyRichTextBox.TextChanged += new System.EventHandler(this.MyRichTextBox_TextChanged);
            this.MyRichTextBox.Resize += new System.EventHandler(this.MyRichTextBox_Resize);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(15)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1163, 531);
            this.Controls.Add(this.MyRichTextBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1179, 570);
            this.Name = "Form1";
            this.Text = "Cobra Programming Language";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MyPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private FontAwesome.Sharp.IconButton searchButton;
        private Bunifu.UI.WinForms.BunifuTextBox searchText;
        private FontAwesome.Sharp.IconButton stop;
        private FontAwesome.Sharp.IconButton start;
        private FontAwesome.Sharp.IconButton undo;
        private FontAwesome.Sharp.IconButton redo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private FontAwesome.Sharp.IconButton saveAs;
        private FontAwesome.Sharp.IconButton savefile;
        private FontAwesome.Sharp.IconButton zoomOutCode;
        private FontAwesome.Sharp.IconButton zoomInCode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox MyRichTextBox;
        private System.Windows.Forms.PictureBox MyPictureBox;
        private FontAwesome.Sharp.IconButton lexical;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}


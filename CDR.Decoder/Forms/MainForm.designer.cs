namespace Decoder
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.inProgressGroupBox = new System.Windows.Forms.GroupBox();
            this.filesInText = new System.Windows.Forms.Label();
            this.statusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.filesInLabel = new System.Windows.Forms.Label();
            this.srcCdrText = new System.Windows.Forms.Label();
            this.srcCdrLabel = new System.Windows.Forms.Label();
            this.recOutText = new System.Windows.Forms.Label();
            this.recOutLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.exitButton = new System.Windows.Forms.Button();
            this.sinceStartupGroupBox = new System.Windows.Forms.GroupBox();
            this.filesOutText = new System.Windows.Forms.Label();
            this.filesOutLabel = new System.Windows.Forms.Label();
            this.timerText = new System.Windows.Forms.Label();
            this.startTimeText = new System.Windows.Forms.Label();
            this.recOutTotalText = new System.Windows.Forms.Label();
            this.recOutTotalLabel = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.startTimeLabel = new System.Windows.Forms.Label();
            this.decodeButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.jobTabPage = new System.Windows.Forms.TabPage();
            this.jobPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.jobToolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolButton = new System.Windows.Forms.ToolStripButton();
            this.logTabPage = new System.Windows.Forms.TabPage();
            this.logListView = new System.Windows.Forms.ListView();
            this.timeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.messageColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.helpButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.inProgressGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBindingSource)).BeginInit();
            this.sinceStartupGroupBox.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.jobTabPage.SuspendLayout();
            this.jobToolStrip.SuspendLayout();
            this.logTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // inProgressGroupBox
            // 
            this.inProgressGroupBox.Controls.Add(this.filesInText);
            this.inProgressGroupBox.Controls.Add(this.filesInLabel);
            this.inProgressGroupBox.Controls.Add(this.srcCdrText);
            this.inProgressGroupBox.Controls.Add(this.srcCdrLabel);
            this.inProgressGroupBox.Controls.Add(this.recOutText);
            this.inProgressGroupBox.Controls.Add(this.recOutLabel);
            this.inProgressGroupBox.Controls.Add(this.progressBar);
            this.inProgressGroupBox.Location = new System.Drawing.Point(6, 9);
            this.inProgressGroupBox.Name = "inProgressGroupBox";
            this.inProgressGroupBox.Size = new System.Drawing.Size(424, 146);
            this.inProgressGroupBox.TabIndex = 7;
            this.inProgressGroupBox.TabStop = false;
            this.inProgressGroupBox.Text = "In Progress";
            // 
            // filesInText
            // 
            this.filesInText.AutoSize = true;
            this.filesInText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.statusBindingSource, "CdrFilesIn", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.filesInText.Location = new System.Drawing.Point(83, 40);
            this.filesInText.Name = "filesInText";
            this.filesInText.Size = new System.Drawing.Size(66, 16);
            this.filesInText.TabIndex = 11;
            this.filesInText.Text = "filesInText";
            // 
            // statusBindingSource
            // 
            this.statusBindingSource.DataSource = typeof(CDR.Decoder.JobStatus);
            // 
            // filesInLabel
            // 
            this.filesInLabel.AutoSize = true;
            this.filesInLabel.Location = new System.Drawing.Point(8, 40);
            this.filesInLabel.Name = "filesInLabel";
            this.filesInLabel.Size = new System.Drawing.Size(81, 16);
            this.filesInLabel.TabIndex = 10;
            this.filesInLabel.Text = "CDR Files In:";
            // 
            // srcCdrText
            // 
            this.srcCdrText.AutoEllipsis = true;
            this.srcCdrText.AutoSize = true;
            this.srcCdrText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.statusBindingSource, "CurrentCdrFile", true));
            this.srcCdrText.Location = new System.Drawing.Point(54, 22);
            this.srcCdrText.MaximumSize = new System.Drawing.Size(184, 13);
            this.srcCdrText.Name = "srcCdrText";
            this.srcCdrText.Size = new System.Drawing.Size(69, 13);
            this.srcCdrText.TabIndex = 8;
            this.srcCdrText.Text = "srcCdrText";
            // 
            // srcCdrLabel
            // 
            this.srcCdrLabel.AutoSize = true;
            this.srcCdrLabel.Location = new System.Drawing.Point(8, 22);
            this.srcCdrLabel.Name = "srcCdrLabel";
            this.srcCdrLabel.Size = new System.Drawing.Size(47, 16);
            this.srcCdrLabel.TabIndex = 7;
            this.srcCdrLabel.Text = "File In:";
            // 
            // recOutText
            // 
            this.recOutText.AutoSize = true;
            this.recOutText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.statusBindingSource, "RecordsOut", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.recOutText.Location = new System.Drawing.Point(85, 58);
            this.recOutText.Name = "recOutText";
            this.recOutText.Size = new System.Drawing.Size(50, 16);
            this.recOutText.TabIndex = 6;
            this.recOutText.Text = "recText";
            // 
            // recOutLabel
            // 
            this.recOutLabel.AutoSize = true;
            this.recOutLabel.Location = new System.Drawing.Point(8, 58);
            this.recOutLabel.Name = "recOutLabel";
            this.recOutLabel.Size = new System.Drawing.Size(82, 16);
            this.recOutLabel.TabIndex = 3;
            this.recOutLabel.Text = "Records Out:";
            // 
            // progressBar
            // 
            this.progressBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.statusBindingSource, "Percent", true));
            this.progressBar.Location = new System.Drawing.Point(6, 79);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(231, 14);
            this.progressBar.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitButton.Location = new System.Drawing.Point(1068, 533);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 9;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // sinceStartupGroupBox
            // 
            this.sinceStartupGroupBox.Controls.Add(this.filesOutText);
            this.sinceStartupGroupBox.Controls.Add(this.filesOutLabel);
            this.sinceStartupGroupBox.Controls.Add(this.timerText);
            this.sinceStartupGroupBox.Controls.Add(this.startTimeText);
            this.sinceStartupGroupBox.Controls.Add(this.recOutTotalText);
            this.sinceStartupGroupBox.Controls.Add(this.recOutTotalLabel);
            this.sinceStartupGroupBox.Controls.Add(this.timerLabel);
            this.sinceStartupGroupBox.Controls.Add(this.startTimeLabel);
            this.sinceStartupGroupBox.Location = new System.Drawing.Point(436, 9);
            this.sinceStartupGroupBox.Name = "sinceStartupGroupBox";
            this.sinceStartupGroupBox.Size = new System.Drawing.Size(688, 146);
            this.sinceStartupGroupBox.TabIndex = 12;
            this.sinceStartupGroupBox.TabStop = false;
            this.sinceStartupGroupBox.Text = "Since Startup";
            // 
            // filesOutText
            // 
            this.filesOutText.AutoSize = true;
            this.filesOutText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.statusBindingSource, "CdrFilesOut", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.filesOutText.Location = new System.Drawing.Point(89, 76);
            this.filesOutText.Name = "filesOutText";
            this.filesOutText.Size = new System.Drawing.Size(75, 16);
            this.filesOutText.TabIndex = 10;
            this.filesOutText.Text = "filesOutText";
            // 
            // filesOutLabel
            // 
            this.filesOutLabel.AutoSize = true;
            this.filesOutLabel.Location = new System.Drawing.Point(6, 76);
            this.filesOutLabel.Name = "filesOutLabel";
            this.filesOutLabel.Size = new System.Drawing.Size(90, 16);
            this.filesOutLabel.TabIndex = 9;
            this.filesOutLabel.Text = "CDR Files Out:";
            // 
            // timerText
            // 
            this.timerText.AutoSize = true;
            this.timerText.Location = new System.Drawing.Point(89, 40);
            this.timerText.Name = "timerText";
            this.timerText.Size = new System.Drawing.Size(59, 16);
            this.timerText.TabIndex = 5;
            this.timerText.Text = "00:00:00";
            // 
            // startTimeText
            // 
            this.startTimeText.AutoSize = true;
            this.startTimeText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.statusBindingSource, "StartTime", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "G"));
            this.startTimeText.Location = new System.Drawing.Point(89, 22);
            this.startTimeText.Name = "startTimeText";
            this.startTimeText.Size = new System.Drawing.Size(131, 16);
            this.startTimeText.TabIndex = 4;
            this.startTimeText.Text = "dd.mm.yyyy HH:mi:ss";
            // 
            // recOutTotalText
            // 
            this.recOutTotalText.AutoSize = true;
            this.recOutTotalText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.statusBindingSource, "RecordsOutTotal", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.recOutTotalText.Location = new System.Drawing.Point(89, 58);
            this.recOutTotalText.Name = "recOutTotalText";
            this.recOutTotalText.Size = new System.Drawing.Size(50, 16);
            this.recOutTotalText.TabIndex = 3;
            this.recOutTotalText.Text = "recText";
            // 
            // recOutTotalLabel
            // 
            this.recOutTotalLabel.AutoSize = true;
            this.recOutTotalLabel.Location = new System.Drawing.Point(13, 58);
            this.recOutTotalLabel.Name = "recOutTotalLabel";
            this.recOutTotalLabel.Size = new System.Drawing.Size(82, 16);
            this.recOutTotalLabel.TabIndex = 2;
            this.recOutTotalLabel.Text = "Records Out:";
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Location = new System.Drawing.Point(47, 40);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(46, 16);
            this.timerLabel.TabIndex = 1;
            this.timerLabel.Text = "Timer:";
            // 
            // startTimeLabel
            // 
            this.startTimeLabel.AutoSize = true;
            this.startTimeLabel.Location = new System.Drawing.Point(25, 22);
            this.startTimeLabel.Name = "startTimeLabel";
            this.startTimeLabel.Size = new System.Drawing.Size(73, 16);
            this.startTimeLabel.TabIndex = 0;
            this.startTimeLabel.Text = "Start Time:";
            // 
            // decodeButton
            // 
            this.decodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.decodeButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.decodeButton.Location = new System.Drawing.Point(986, 533);
            this.decodeButton.Name = "decodeButton";
            this.decodeButton.Size = new System.Drawing.Size(75, 23);
            this.decodeButton.TabIndex = 13;
            this.decodeButton.Text = "Decode";
            this.decodeButton.UseVisualStyleBackColor = true;
            this.decodeButton.Click += new System.EventHandler(this.decodeButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.jobTabPage);
            this.tabControl.Controls.Add(this.logTabPage);
            this.tabControl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl.ImageList = this.imageList;
            this.tabControl.Location = new System.Drawing.Point(2, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1141, 505);
            this.tabControl.TabIndex = 14;
            // 
            // jobTabPage
            // 
            this.jobTabPage.Controls.Add(this.jobPropertyGrid);
            this.jobTabPage.Controls.Add(this.jobToolStrip);
            this.jobTabPage.ImageIndex = 0;
            this.jobTabPage.Location = new System.Drawing.Point(4, 25);
            this.jobTabPage.Name = "jobTabPage";
            this.jobTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.jobTabPage.Size = new System.Drawing.Size(1133, 476);
            this.jobTabPage.TabIndex = 1;
            this.jobTabPage.Text = "Job Properties";
            this.jobTabPage.UseVisualStyleBackColor = true;
            // 
            // jobPropertyGrid
            // 
            this.jobPropertyGrid.BackColor = System.Drawing.SystemColors.ControlDark;
            this.jobPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.jobPropertyGrid.Location = new System.Drawing.Point(3, 28);
            this.jobPropertyGrid.Name = "jobPropertyGrid";
            this.jobPropertyGrid.Size = new System.Drawing.Size(1127, 445);
            this.jobPropertyGrid.TabIndex = 0;
            this.jobPropertyGrid.ToolbarVisible = false;
            this.jobPropertyGrid.ViewBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.jobPropertyGrid.Click += new System.EventHandler(this.jobPropertyGrid_Click);
            // 
            // jobToolStrip
            // 
            this.jobToolStrip.BackColor = System.Drawing.Color.Gray;
            this.jobToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.jobToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolButton,
            this.toolStripSeparator1,
            this.openToolButton,
            this.saveToolButton});
            this.jobToolStrip.Location = new System.Drawing.Point(3, 3);
            this.jobToolStrip.Name = "jobToolStrip";
            this.jobToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.jobToolStrip.Size = new System.Drawing.Size(1127, 25);
            this.jobToolStrip.TabIndex = 1;
            this.jobToolStrip.Text = "toolStrip1";
            // 
            // newToolButton
            // 
            this.newToolButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolButton.Image")));
            this.newToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolButton.Name = "newToolButton";
            this.newToolButton.Size = new System.Drawing.Size(51, 22);
            this.newToolButton.Text = "New";
            this.newToolButton.ToolTipText = "Create new job.";
            this.newToolButton.Visible = false;
            this.newToolButton.Click += new System.EventHandler(this.newToolButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // openToolButton
            // 
            this.openToolButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolButton.Image")));
            this.openToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolButton.Name = "openToolButton";
            this.openToolButton.Size = new System.Drawing.Size(56, 22);
            this.openToolButton.Text = "Open";
            this.openToolButton.ToolTipText = "Open existing job-file.";
            this.openToolButton.Visible = false;
            this.openToolButton.Click += new System.EventHandler(this.openToolButton_Click);
            // 
            // saveToolButton
            // 
            this.saveToolButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolButton.Image")));
            this.saveToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolButton.Name = "saveToolButton";
            this.saveToolButton.Size = new System.Drawing.Size(79, 22);
            this.saveToolButton.Text = "Save As ...";
            this.saveToolButton.ToolTipText = "Save this settings to file.";
            this.saveToolButton.Visible = false;
            this.saveToolButton.Click += new System.EventHandler(this.saveToolButton_Click);
            // 
            // logTabPage
            // 
            this.logTabPage.Controls.Add(this.logListView);
            this.logTabPage.Controls.Add(this.inProgressGroupBox);
            this.logTabPage.Controls.Add(this.sinceStartupGroupBox);
            this.logTabPage.ImageIndex = 1;
            this.logTabPage.Location = new System.Drawing.Point(4, 25);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.Padding = new System.Windows.Forms.Padding(6);
            this.logTabPage.Size = new System.Drawing.Size(1133, 476);
            this.logTabPage.TabIndex = 2;
            this.logTabPage.Text = "Decoder Log";
            this.logTabPage.UseVisualStyleBackColor = true;
            // 
            // logListView
            // 
            this.logListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timeColumnHeader,
            this.messageColumnHeader});
            this.logListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logListView.FullRowSelect = true;
            this.logListView.GridLines = true;
            this.logListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.logListView.HideSelection = false;
            this.logListView.Location = new System.Drawing.Point(6, 161);
            this.logListView.Name = "logListView";
            this.logListView.Size = new System.Drawing.Size(1121, 309);
            this.logListView.TabIndex = 13;
            this.logListView.UseCompatibleStateImageBehavior = false;
            this.logListView.View = System.Windows.Forms.View.Details;
            this.logListView.VirtualMode = true;
            this.logListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.logListView_RetrieveVirtualItem);
            // 
            // timeColumnHeader
            // 
            this.timeColumnHeader.Text = "Time";
            this.timeColumnHeader.Width = 90;
            // 
            // messageColumnHeader
            // 
            this.messageColumnHeader.Text = "Message";
            this.messageColumnHeader.Width = 331;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "1289586052_cog_edit.png");
            this.imageList.Images.SetKeyName(1, "1289586063_accept.png");
            // 
            // helpButton
            // 
            this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.helpButton.Enabled = false;
            this.helpButton.Location = new System.Drawing.Point(687, 533);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(75, 23);
            this.helpButton.TabIndex = 15;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Visible = false;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Job Files (*.job)|*.job|All Files (*.*)|*.*";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Job Files (*.job)|*.job|All Files (*.*)|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1155, 568);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.decodeButton);
            this.Controls.Add(this.exitButton);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "IrexSoft Corp. for Huawei ASN.1 Decoder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.inProgressGroupBox.ResumeLayout(false);
            this.inProgressGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBindingSource)).EndInit();
            this.sinceStartupGroupBox.ResumeLayout(false);
            this.sinceStartupGroupBox.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.jobTabPage.ResumeLayout(false);
            this.jobTabPage.PerformLayout();
            this.jobToolStrip.ResumeLayout(false);
            this.jobToolStrip.PerformLayout();
            this.logTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox inProgressGroupBox;
        private System.Windows.Forms.Label recOutText;
        private System.Windows.Forms.Label recOutLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.GroupBox sinceStartupGroupBox;
        private System.Windows.Forms.Label startTimeLabel;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.Label recOutTotalLabel;
        private System.Windows.Forms.Button decodeButton;
        private System.Windows.Forms.BindingSource statusBindingSource;
        private System.Windows.Forms.Label recOutTotalText;
        private System.Windows.Forms.Label timerText;
        private System.Windows.Forms.Label startTimeText;
        private System.Windows.Forms.Label filesOutText;
        private System.Windows.Forms.Label filesOutLabel;
        private System.Windows.Forms.Label srcCdrLabel;
        private System.Windows.Forms.Label srcCdrText;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage jobTabPage;
        private System.Windows.Forms.TabPage logTabPage;
        private System.Windows.Forms.PropertyGrid jobPropertyGrid;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.ToolStrip jobToolStrip;
        private System.Windows.Forms.ToolStripButton openToolButton;
        private System.Windows.Forms.ToolStripButton newToolButton;
        private System.Windows.Forms.ToolStripButton saveToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ListView logListView;
        private System.Windows.Forms.ColumnHeader messageColumnHeader;
        private System.Windows.Forms.ColumnHeader timeColumnHeader;
        private System.Windows.Forms.Label filesInLabel;
        private System.Windows.Forms.Label filesInText;
        private System.Windows.Forms.ImageList imageList;
    }
}


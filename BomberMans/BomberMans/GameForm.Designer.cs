namespace BomberMans
{
    partial class GameForm
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            playerContextMenu = new ContextMenuStrip(components);
            kickPlayerButton = new ToolStripMenuItem();
            banPlayerButton = new ToolStripMenuItem();
            GameTimer = new System.Windows.Forms.Timer(components);
            menuStrip = new MenuStrip();
            играToolStripMenuItem = new ToolStripMenuItem();
            CreateLevel = new ToolStripMenuItem();
            StartButton = new ToolStripMenuItem();
            StopButton = new ToolStripMenuItem();
            параметрыToolStripMenuItem = new ToolStripMenuItem();
            DeveloperButtom = new ToolStripMenuItem();
            splitContainer = new SplitContainer();
            playersDataGridView = new DataGridView();
            PositionColumn = new DataGridViewTextBoxColumn();
            NameColumn = new DataGridViewTextBoxColumn();
            ScoreColumn = new DataGridViewTextBoxColumn();
            pictureBox = new PictureBox();
            playerContextMenu.SuspendLayout();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)playersDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // playerContextMenu
            // 
            playerContextMenu.Items.AddRange(new ToolStripItem[] { kickPlayerButton, banPlayerButton });
            playerContextMenu.Name = "playerContextMenu";
            playerContextMenu.Size = new Size(159, 48);
            // 
            // kickPlayerButton
            // 
            kickPlayerButton.Name = "kickPlayerButton";
            kickPlayerButton.Size = new Size(158, 22);
            kickPlayerButton.Text = "Выгнать";
            kickPlayerButton.Click += kickPlayerButton_Click;
            // 
            // banPlayerButton
            // 
            banPlayerButton.Name = "banPlayerButton";
            banPlayerButton.Size = new Size(158, 22);
            banPlayerButton.Text = "Заблокировать";
            banPlayerButton.Click += banPlayerButton_Click;
            // 
            // GameTimer
            // 
            GameTimer.Interval = 2000;
            GameTimer.Tick += GameTimer_Tick;
            // 
            // menuStrip
            // 
            menuStrip.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point);
            menuStrip.Items.AddRange(new ToolStripItem[] { играToolStripMenuItem, параметрыToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1577, 28);
            menuStrip.TabIndex = 2;
            menuStrip.Text = "menuStrip1";
            // 
            // играToolStripMenuItem
            // 
            играToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { CreateLevel, StartButton, StopButton });
            играToolStripMenuItem.Name = "играToolStripMenuItem";
            играToolStripMenuItem.Size = new Size(60, 24);
            играToolStripMenuItem.Text = "Игра";
            // 
            // CreateLevel
            // 
            CreateLevel.Name = "CreateLevel";
            CreateLevel.Size = new Size(241, 24);
            CreateLevel.Text = "Создать уровень";
            CreateLevel.Click += CreateLevel_Click;
            // 
            // StartButton
            // 
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(241, 24);
            StartButton.Text = "Начать/продолжить";
            StartButton.Click += StartButton_Click;
            // 
            // StopButton
            // 
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(241, 24);
            StopButton.Text = "Остановить";
            StopButton.Click += StopButton_Click;
            // 
            // параметрыToolStripMenuItem
            // 
            параметрыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { DeveloperButtom });
            параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            параметрыToolStripMenuItem.Size = new Size(114, 24);
            параметрыToolStripMenuItem.Text = "Параметры";
            // 
            // DeveloperButtom
            // 
            DeveloperButtom.Name = "DeveloperButtom";
            DeveloperButtom.Size = new Size(249, 24);
            DeveloperButtom.Text = "Режим разработчика";
            DeveloperButtom.Click += DeveloperButtom_Click;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 28);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.AutoScroll = true;
            splitContainer.Panel1.Controls.Add(pictureBox);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(playersDataGridView);
            splitContainer.Size = new Size(1577, 877);
            splitContainer.SplitterDistance = 1033;
            splitContainer.SplitterWidth = 12;
            splitContainer.TabIndex = 4;
            splitContainer.PreviewKeyDown += splitContainer_PreviewKeyDown;
            // 
            // playersDataGridView
            // 
            playersDataGridView.AllowUserToAddRows = false;
            playersDataGridView.AllowUserToDeleteRows = false;
            playersDataGridView.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Bookman Old Style", 24F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            playersDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            playersDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            playersDataGridView.Columns.AddRange(new DataGridViewColumn[] { PositionColumn, NameColumn, ScoreColumn });
            playersDataGridView.ContextMenuStrip = playerContextMenu;
            playersDataGridView.Dock = DockStyle.Fill;
            playersDataGridView.Location = new Point(0, 0);
            playersDataGridView.Name = "playersDataGridView";
            playersDataGridView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Bookman Old Style", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            playersDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Font = new Font("Bookman Old Style", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            playersDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            playersDataGridView.RowTemplate.Height = 40;
            playersDataGridView.Size = new Size(532, 877);
            playersDataGridView.TabIndex = 1;
            // 
            // PositionColumn
            // 
            PositionColumn.HeaderText = "#";
            PositionColumn.Name = "PositionColumn";
            PositionColumn.ReadOnly = true;
            PositionColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // NameColumn
            // 
            NameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            NameColumn.HeaderText = "Игрок";
            NameColumn.Name = "NameColumn";
            NameColumn.ReadOnly = true;
            NameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // ScoreColumn
            // 
            ScoreColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ScoreColumn.HeaderText = "Очки";
            ScoreColumn.Name = "ScoreColumn";
            ScoreColumn.ReadOnly = true;
            ScoreColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            ScoreColumn.Width = 113;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1033, 877);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1577, 905);
            Controls.Add(splitContainer);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GameForm";
            Text = "Чемпионат \"Бомбермен\"";
            playerContextMenu.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)playersDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private MenuStrip menuStrip;
        private ToolStripMenuItem играToolStripMenuItem;
        private ToolStripMenuItem StartButton;
        private ToolStripMenuItem StopButton;
        private ToolStripMenuItem CreateLevel;
        private SplitContainer splitContainer;
        private ContextMenuStrip playerContextMenu;
        private ToolStripMenuItem kickPlayerButton;
        private ToolStripMenuItem banPlayerButton;
        private ToolStripMenuItem параметрыToolStripMenuItem;
        private ToolStripMenuItem DeveloperButtom;
        private DataGridView playersDataGridView;
        private DataGridViewTextBoxColumn PositionColumn;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn ScoreColumn;
        private PictureBox pictureBox;
    }
}

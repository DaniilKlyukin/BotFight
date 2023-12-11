namespace BomberManClient
{
    partial class ClientForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            splitContainer = new SplitContainer();
            pictureBox = new PictureBox();
            playersDataGridView = new DataGridView();
            PositionColumn = new DataGridViewTextBoxColumn();
            NameColumn = new DataGridViewTextBoxColumn();
            ScoreColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playersDataGridView).BeginInit();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
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
            splitContainer.Size = new Size(1196, 731);
            splitContainer.SplitterDistance = 799;
            splitContainer.SplitterWidth = 12;
            splitContainer.TabIndex = 5;
            splitContainer.PreviewKeyDown += splitContainer_PreviewKeyDown;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(400, 400);
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
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
            playersDataGridView.Size = new Size(385, 731);
            playersDataGridView.TabIndex = 2;
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
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1196, 731);
            Controls.Add(splitContainer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ClientForm";
            Text = "Чемпионат \"Бомбермен\"";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)playersDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer;
        private DataGridView playersDataGridView;
        private DataGridViewTextBoxColumn PositionColumn;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn ScoreColumn;
        private PictureBox pictureBox;
    }
}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            playersListBox = new ListBox();
            playerContextMenu = new ContextMenuStrip(components);
            kickPlayerButton = new ToolStripMenuItem();
            banPlayerButton = new ToolStripMenuItem();
            GameTimer = new System.Windows.Forms.Timer(components);
            menuStrip = new MenuStrip();
            играToolStripMenuItem = new ToolStripMenuItem();
            CreateLevel = new ToolStripMenuItem();
            StartButton = new ToolStripMenuItem();
            StopButton = new ToolStripMenuItem();
            pictureBox = new PictureBox();
            splitContainer = new SplitContainer();
            playerContextMenu.SuspendLayout();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // playersListBox
            // 
            playersListBox.ContextMenuStrip = playerContextMenu;
            playersListBox.Dock = DockStyle.Fill;
            playersListBox.Font = new Font("Bookman Old Style", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            playersListBox.FormattingEnabled = true;
            playersListBox.ItemHeight = 32;
            playersListBox.Location = new Point(0, 0);
            playersListBox.Name = "playersListBox";
            playersListBox.Size = new Size(423, 877);
            playersListBox.TabIndex = 0;
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
            menuStrip.Items.AddRange(new ToolStripItem[] { играToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1314, 28);
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
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(879, 877);
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
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
            splitContainer.Panel2.Controls.Add(playersListBox);
            splitContainer.Size = new Size(1314, 877);
            splitContainer.SplitterDistance = 879;
            splitContainer.SplitterWidth = 12;
            splitContainer.TabIndex = 4;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1314, 905);
            Controls.Add(splitContainer);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GameForm";
            Text = "Чемпионат \"Бомбермен\"";
            playerContextMenu.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel1.PerformLayout();
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox playersListBox;
        private System.Windows.Forms.Timer GameTimer;
        private MenuStrip menuStrip;
        private ToolStripMenuItem играToolStripMenuItem;
        private ToolStripMenuItem StartButton;
        private ToolStripMenuItem StopButton;
        private PictureBox pictureBox;
        private ToolStripMenuItem CreateLevel;
        private SplitContainer splitContainer;
        private ContextMenuStrip playerContextMenu;
        private ToolStripMenuItem kickPlayerButton;
        private ToolStripMenuItem banPlayerButton;
    }
}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            splitContainer = new SplitContainer();
            pictureBox = new PictureBox();
            playersListBox = new ListBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
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
            splitContainer.Panel2.Controls.Add(playersListBox);
            splitContainer.Size = new Size(1196, 731);
            splitContainer.SplitterDistance = 799;
            splitContainer.SplitterWidth = 12;
            splitContainer.TabIndex = 5;
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(799, 731);
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // playersListBox
            // 
            playersListBox.Dock = DockStyle.Fill;
            playersListBox.Font = new Font("Bookman Old Style", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            playersListBox.FormattingEnabled = true;
            playersListBox.ItemHeight = 32;
            playersListBox.Location = new Point(0, 0);
            playersListBox.Name = "playersListBox";
            playersListBox.Size = new Size(385, 731);
            playersListBox.TabIndex = 0;
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
            splitContainer.Panel1.PerformLayout();
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer;
        private PictureBox pictureBox;
        private ListBox playersListBox;
    }
}
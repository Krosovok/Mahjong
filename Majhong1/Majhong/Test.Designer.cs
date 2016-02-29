namespace Mahjong
{
    partial class Test
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
            this.handView1 = new Mahjong.View.Controls.HandView();
            this.openHandView1 = new Mahjong.View.Controls.OpenHandView();
            this.win1 = new Mahjong.View.Controls.Win();
            this.SuspendLayout();
            // 
            // handView1
            // 
            this.handView1.BackColor = System.Drawing.Color.Transparent;
            this.handView1.Hand = null;
            this.handView1.Location = new System.Drawing.Point(123, 292);
            this.handView1.Mode = Mahjong.View.Controls.Mode.Back;
            this.handView1.Name = "handView1";
            this.handView1.Size = new System.Drawing.Size(598, 64);
            this.handView1.TabIndex = 2;
            // 
            // openHandView1
            // 
            this.openHandView1.BackColor = System.Drawing.Color.Transparent;
            this.openHandView1.Hand = null;
            this.openHandView1.Location = new System.Drawing.Point(123, 344);
            this.openHandView1.Mode = Mahjong.View.Controls.Mode.Back;
            this.openHandView1.Name = "openHandView1";
            this.openHandView1.Size = new System.Drawing.Size(598, 53);
            this.openHandView1.TabIndex = 1;
            this.openHandView1.Visible = false;
            // 
            // win1
            // 
            this.win1.BackColor = System.Drawing.Color.DarkGreen;
            this.win1.Location = new System.Drawing.Point(138, 12);
            this.win1.Name = "win1";
            this.win1.Size = new System.Drawing.Size(651, 456);
            this.win1.TabIndex = 3;
            this.win1.Visible = false;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(1018, 634);
            this.Controls.Add(this.win1);
            this.Controls.Add(this.handView1);
            this.Controls.Add(this.openHandView1);
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion

        private View.Controls.OpenHandView openHandView1;
        private View.Controls.HandView handView1;
        private View.Controls.Win win1;






    }
}
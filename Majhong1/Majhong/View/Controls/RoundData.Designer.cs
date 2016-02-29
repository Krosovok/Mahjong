namespace Mahjong.View.Controls
{
    partial class RoundData
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.roundN = new System.Windows.Forms.Label();
            this.playerRiichi = new System.Windows.Forms.PictureBox();
            this.oppositeRiichi = new System.Windows.Forms.PictureBox();
            this.leftRiichi = new System.Windows.Forms.PictureBox();
            this.rightRiichi = new System.Windows.Forms.PictureBox();
            this.playerData4 = new Mahjong.View.Controls.PlayerData();
            this.playerData3 = new Mahjong.View.Controls.PlayerData();
            this.playerData2 = new Mahjong.View.Controls.PlayerData();
            this.playerData1 = new Mahjong.View.Controls.PlayerData();
            ((System.ComponentModel.ISupportInitialize)(this.playerRiichi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oppositeRiichi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftRiichi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightRiichi)).BeginInit();
            this.SuspendLayout();
            // 
            // roundN
            // 
            this.roundN.AutoSize = true;
            this.roundN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.roundN.Location = new System.Drawing.Point(87, 94);
            this.roundN.Name = "roundN";
            this.roundN.Size = new System.Drawing.Size(34, 20);
            this.roundN.TabIndex = 0;
            this.roundN.Text = "1東";
            this.roundN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerRiichi
            // 
            this.playerRiichi.Image = global::Mahjong.Properties.Resources.m1000;
            this.playerRiichi.Location = new System.Drawing.Point(3, 189);
            this.playerRiichi.Name = "playerRiichi";
            this.playerRiichi.Size = new System.Drawing.Size(180, 17);
            this.playerRiichi.TabIndex = 5;
            this.playerRiichi.TabStop = false;
            this.playerRiichi.Visible = false;
            // 
            // oppositeRiichi
            // 
            this.oppositeRiichi.Image = global::Mahjong.Properties.Resources.m1000;
            this.oppositeRiichi.Location = new System.Drawing.Point(26, 3);
            this.oppositeRiichi.Name = "oppositeRiichi";
            this.oppositeRiichi.Size = new System.Drawing.Size(180, 17);
            this.oppositeRiichi.TabIndex = 6;
            this.oppositeRiichi.TabStop = false;
            this.oppositeRiichi.Visible = false;
            // 
            // leftRiichi
            // 
            this.leftRiichi.Image = global::Mahjong.Properties.Resources.m1000;
            this.leftRiichi.Location = new System.Drawing.Point(3, 3);
            this.leftRiichi.Name = "leftRiichi";
            this.leftRiichi.Size = new System.Drawing.Size(17, 180);
            this.leftRiichi.TabIndex = 7;
            this.leftRiichi.TabStop = false;
            this.leftRiichi.Visible = false;
            // 
            // rightRiichi
            // 
            this.rightRiichi.Image = global::Mahjong.Properties.Resources.m1000;
            this.rightRiichi.Location = new System.Drawing.Point(189, 26);
            this.rightRiichi.Name = "rightRiichi";
            this.rightRiichi.Size = new System.Drawing.Size(17, 180);
            this.rightRiichi.TabIndex = 8;
            this.rightRiichi.TabStop = false;
            this.rightRiichi.Visible = false;
            // 
            // playerData4
            // 
            this.playerData4.BackColor = System.Drawing.Color.Transparent;
            this.playerData4.Location = new System.Drawing.Point(82, 40);
            this.playerData4.Name = "playerData4";
            this.playerData4.Points = 25000;
            this.playerData4.Position = Mahjong.Model.Game.PlayerPosition.West;
            this.playerData4.Size = new System.Drawing.Size(49, 51);
            this.playerData4.TabIndex = 4;
            // 
            // playerData3
            // 
            this.playerData3.BackColor = System.Drawing.Color.Transparent;
            this.playerData3.Location = new System.Drawing.Point(136, 79);
            this.playerData3.Name = "playerData3";
            this.playerData3.Points = 25000;
            this.playerData3.Position = Mahjong.Model.Game.PlayerPosition.South;
            this.playerData3.Size = new System.Drawing.Size(49, 51);
            this.playerData3.TabIndex = 3;
            // 
            // playerData2
            // 
            this.playerData2.BackColor = System.Drawing.Color.Transparent;
            this.playerData2.Location = new System.Drawing.Point(82, 117);
            this.playerData2.Name = "playerData2";
            this.playerData2.Points = 25000;
            this.playerData2.Position = Mahjong.Model.Game.PlayerPosition.East;
            this.playerData2.Size = new System.Drawing.Size(49, 51);
            this.playerData2.TabIndex = 2;
            // 
            // playerData1
            // 
            this.playerData1.BackColor = System.Drawing.Color.Transparent;
            this.playerData1.Location = new System.Drawing.Point(26, 79);
            this.playerData1.Name = "playerData1";
            this.playerData1.Points = 25000;
            this.playerData1.Position = Mahjong.Model.Game.PlayerPosition.North;
            this.playerData1.Size = new System.Drawing.Size(49, 51);
            this.playerData1.TabIndex = 1;
            // 
            // RoundData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.Controls.Add(this.rightRiichi);
            this.Controls.Add(this.leftRiichi);
            this.Controls.Add(this.oppositeRiichi);
            this.Controls.Add(this.playerRiichi);
            this.Controls.Add(this.playerData4);
            this.Controls.Add(this.playerData3);
            this.Controls.Add(this.playerData2);
            this.Controls.Add(this.playerData1);
            this.Controls.Add(this.roundN);
            this.Name = "RoundData";
            this.Size = new System.Drawing.Size(209, 209);
            ((System.ComponentModel.ISupportInitialize)(this.playerRiichi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oppositeRiichi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftRiichi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightRiichi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label roundN;
        private PlayerData playerData1;
        private PlayerData playerData2;
        private PlayerData playerData3;
        private PlayerData playerData4;
        private System.Windows.Forms.PictureBox playerRiichi;
        private System.Windows.Forms.PictureBox oppositeRiichi;
        private System.Windows.Forms.PictureBox leftRiichi;
        private System.Windows.Forms.PictureBox rightRiichi;
    }
}

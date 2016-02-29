namespace Mahjong
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.kanButton = new System.Windows.Forms.Button();
            this.ponButton = new System.Windows.Forms.Button();
            this.chiButton = new System.Windows.Forms.Button();
            this.pasButton = new System.Windows.Forms.Button();
            this.ronButton = new System.Windows.Forms.Button();
            this.riichiButton = new System.Windows.Forms.Button();
            this.tsumoButton = new System.Windows.Forms.Button();
            this.autoButton = new System.Windows.Forms.Button();
            this.oppositeRiichi = new System.Windows.Forms.PictureBox();
            this.playerRiichi = new System.Windows.Forms.PictureBox();
            this.rightRiichi = new System.Windows.Forms.PictureBox();
            this.leftRiichi = new System.Windows.Forms.PictureBox();
            this.discrardLeft = new Mahjong.View.Controls.OpenTilesView();
            this.discardRight = new Mahjong.View.Controls.OpenTilesView();
            this.discardOpp = new Mahjong.View.Controls.OpenTilesView();
            this.handViewOpp = new Mahjong.View.Controls.HandView();
            this.handViewRight = new Mahjong.View.Controls.HandView();
            this.handViewLeft = new Mahjong.View.Controls.HandView();
            this.discardPlayer = new Mahjong.View.Controls.OpenTilesView();
            this.handViewPlayer = new Mahjong.View.Controls.HandView();
            this.playerOpenTiles = new Mahjong.View.Controls.OpenTilesView();
            ((System.ComponentModel.ISupportInitialize)(this.oppositeRiichi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerRiichi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightRiichi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftRiichi)).BeginInit();
            this.SuspendLayout();
            // 
            // kanButton
            // 
            this.kanButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.kanButton.BackColor = System.Drawing.Color.Transparent;
            this.kanButton.Enabled = false;
            this.kanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kanButton.Location = new System.Drawing.Point(50, 714);
            this.kanButton.Margin = new System.Windows.Forms.Padding(14, 3, 14, 3);
            this.kanButton.Name = "kanButton";
            this.kanButton.Size = new System.Drawing.Size(75, 23);
            this.kanButton.TabIndex = 8;
            this.kanButton.Text = "Кан";
            this.kanButton.UseVisualStyleBackColor = false;
            this.kanButton.EnabledChanged += new System.EventHandler(this.button_EnabledChanged);
            this.kanButton.Click += new System.EventHandler(this.Kan);
            // 
            // ponButton
            // 
            this.ponButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ponButton.BackColor = System.Drawing.Color.Transparent;
            this.ponButton.Enabled = false;
            this.ponButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ponButton.Location = new System.Drawing.Point(153, 714);
            this.ponButton.Margin = new System.Windows.Forms.Padding(14, 3, 14, 3);
            this.ponButton.Name = "ponButton";
            this.ponButton.Size = new System.Drawing.Size(75, 23);
            this.ponButton.TabIndex = 9;
            this.ponButton.Text = "Пон";
            this.ponButton.UseVisualStyleBackColor = false;
            this.ponButton.EnabledChanged += new System.EventHandler(this.button_EnabledChanged);
            this.ponButton.Click += new System.EventHandler(this.Pon);
            // 
            // chiButton
            // 
            this.chiButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chiButton.BackColor = System.Drawing.Color.Transparent;
            this.chiButton.Enabled = false;
            this.chiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chiButton.Location = new System.Drawing.Point(256, 714);
            this.chiButton.Margin = new System.Windows.Forms.Padding(14, 3, 14, 3);
            this.chiButton.Name = "chiButton";
            this.chiButton.Size = new System.Drawing.Size(75, 23);
            this.chiButton.TabIndex = 10;
            this.chiButton.Text = "Чи";
            this.chiButton.UseVisualStyleBackColor = false;
            this.chiButton.EnabledChanged += new System.EventHandler(this.button_EnabledChanged);
            this.chiButton.Click += new System.EventHandler(this.Chi);
            // 
            // pasButton
            // 
            this.pasButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pasButton.BackColor = System.Drawing.Color.Transparent;
            this.pasButton.Enabled = false;
            this.pasButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pasButton.Location = new System.Drawing.Point(462, 714);
            this.pasButton.Margin = new System.Windows.Forms.Padding(14, 3, 14, 3);
            this.pasButton.Name = "pasButton";
            this.pasButton.Size = new System.Drawing.Size(75, 23);
            this.pasButton.TabIndex = 11;
            this.pasButton.Text = "Пас";
            this.pasButton.UseVisualStyleBackColor = false;
            this.pasButton.EnabledChanged += new System.EventHandler(this.button_EnabledChanged);
            this.pasButton.Click += new System.EventHandler(this.Pas);
            // 
            // ronButton
            // 
            this.ronButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ronButton.BackColor = System.Drawing.Color.Transparent;
            this.ronButton.Enabled = false;
            this.ronButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ronButton.Location = new System.Drawing.Point(565, 714);
            this.ronButton.Margin = new System.Windows.Forms.Padding(14, 3, 14, 3);
            this.ronButton.Name = "ronButton";
            this.ronButton.Size = new System.Drawing.Size(75, 23);
            this.ronButton.TabIndex = 13;
            this.ronButton.Text = "Рон";
            this.ronButton.UseVisualStyleBackColor = false;
            this.ronButton.EnabledChanged += new System.EventHandler(this.button_EnabledChanged);
            this.ronButton.Click += new System.EventHandler(this.Ron);
            // 
            // riichiButton
            // 
            this.riichiButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.riichiButton.BackColor = System.Drawing.Color.Transparent;
            this.riichiButton.Enabled = false;
            this.riichiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.riichiButton.Location = new System.Drawing.Point(359, 714);
            this.riichiButton.Margin = new System.Windows.Forms.Padding(14, 3, 14, 3);
            this.riichiButton.Name = "riichiButton";
            this.riichiButton.Size = new System.Drawing.Size(75, 23);
            this.riichiButton.TabIndex = 14;
            this.riichiButton.Text = "Риичи";
            this.riichiButton.UseVisualStyleBackColor = false;
            this.riichiButton.EnabledChanged += new System.EventHandler(this.button_EnabledChanged);
            this.riichiButton.Click += new System.EventHandler(this.Riichi);
            // 
            // tsumoButton
            // 
            this.tsumoButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tsumoButton.BackColor = System.Drawing.Color.Transparent;
            this.tsumoButton.Enabled = false;
            this.tsumoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tsumoButton.Location = new System.Drawing.Point(668, 714);
            this.tsumoButton.Margin = new System.Windows.Forms.Padding(14, 3, 14, 3);
            this.tsumoButton.Name = "tsumoButton";
            this.tsumoButton.Size = new System.Drawing.Size(75, 23);
            this.tsumoButton.TabIndex = 15;
            this.tsumoButton.Text = "Цумо";
            this.tsumoButton.UseVisualStyleBackColor = false;
            this.tsumoButton.EnabledChanged += new System.EventHandler(this.button_EnabledChanged);
            this.tsumoButton.Click += new System.EventHandler(this.Tsumo);
            // 
            // autoButton
            // 
            this.autoButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.autoButton.BackColor = System.Drawing.Color.Transparent;
            this.autoButton.Enabled = false;
            this.autoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autoButton.Location = new System.Drawing.Point(771, 714);
            this.autoButton.Margin = new System.Windows.Forms.Padding(14, 3, 14, 3);
            this.autoButton.Name = "autoButton";
            this.autoButton.Size = new System.Drawing.Size(75, 23);
            this.autoButton.TabIndex = 16;
            this.autoButton.Text = "Авто";
            this.autoButton.UseVisualStyleBackColor = false;
            this.autoButton.Click += new System.EventHandler(this.Auto);
            // 
            // oppositeRiichi
            // 
            this.oppositeRiichi.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.oppositeRiichi.Image = global::Mahjong.Properties.Resources.m1000;
            this.oppositeRiichi.Location = new System.Drawing.Point(355, 307);
            this.oppositeRiichi.Name = "oppositeRiichi";
            this.oppositeRiichi.Size = new System.Drawing.Size(180, 17);
            this.oppositeRiichi.TabIndex = 17;
            this.oppositeRiichi.TabStop = false;
            this.oppositeRiichi.Visible = false;
            // 
            // playerRiichi
            // 
            this.playerRiichi.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerRiichi.Image = global::Mahjong.Properties.Resources.m1000;
            this.playerRiichi.Location = new System.Drawing.Point(355, 384);
            this.playerRiichi.Name = "playerRiichi";
            this.playerRiichi.Size = new System.Drawing.Size(180, 17);
            this.playerRiichi.TabIndex = 18;
            this.playerRiichi.TabStop = false;
            this.playerRiichi.Visible = false;
            // 
            // rightRiichi
            // 
            this.rightRiichi.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.rightRiichi.Image = global::Mahjong.Properties.Resources.m1000;
            this.rightRiichi.Location = new System.Drawing.Point(602, 284);
            this.rightRiichi.Name = "rightRiichi";
            this.rightRiichi.Size = new System.Drawing.Size(17, 180);
            this.rightRiichi.TabIndex = 19;
            this.rightRiichi.TabStop = false;
            this.rightRiichi.Visible = false;
            // 
            // leftRiichi
            // 
            this.leftRiichi.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.leftRiichi.Image = global::Mahjong.Properties.Resources.m1000;
            this.leftRiichi.Location = new System.Drawing.Point(273, 284);
            this.leftRiichi.Name = "leftRiichi";
            this.leftRiichi.Size = new System.Drawing.Size(17, 180);
            this.leftRiichi.TabIndex = 20;
            this.leftRiichi.TabStop = false;
            this.leftRiichi.Visible = false;
            // 
            // discrardLeft
            // 
            this.discrardLeft.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.discrardLeft.BackColor = System.Drawing.Color.Transparent;
            this.discrardLeft.Direction = Mahjong.View.Controls.Direction.ToLeftPlayer;
            this.discrardLeft.IsDiscard = true;
            this.discrardLeft.Location = new System.Drawing.Point(52, 248);
            this.discrardLeft.Name = "discrardLeft";
            this.discrardLeft.Size = new System.Drawing.Size(215, 260);
            this.discrardLeft.TabIndex = 7;
            // 
            // discardRight
            // 
            this.discardRight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.discardRight.BackColor = System.Drawing.Color.Transparent;
            this.discardRight.Direction = Mahjong.View.Controls.Direction.ToRightPlayer;
            this.discardRight.IsDiscard = true;
            this.discardRight.Location = new System.Drawing.Point(625, 248);
            this.discardRight.Name = "discardRight";
            this.discardRight.Size = new System.Drawing.Size(215, 260);
            this.discardRight.TabIndex = 6;
            // 
            // discardOpp
            // 
            this.discardOpp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.discardOpp.BackColor = System.Drawing.Color.Transparent;
            this.discardOpp.Direction = Mahjong.View.Controls.Direction.ToOppositePlayer;
            this.discardOpp.IsDiscard = true;
            this.discardOpp.Location = new System.Drawing.Point(317, 86);
            this.discardOpp.Name = "discardOpp";
            this.discardOpp.Size = new System.Drawing.Size(260, 215);
            this.discardOpp.TabIndex = 5;
            // 
            // handViewOpp
            // 
            this.handViewOpp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.handViewOpp.BackColor = System.Drawing.Color.Transparent;
            this.handViewOpp.Hand = null;
            this.handViewOpp.Location = new System.Drawing.Point(147, 12);
            this.handViewOpp.Mode = Mahjong.View.Controls.Mode.Back;
            this.handViewOpp.Name = "handViewOpp";
            this.handViewOpp.Size = new System.Drawing.Size(598, 64);
            this.handViewOpp.TabIndex = 4;
            // 
            // handViewRight
            // 
            this.handViewRight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.handViewRight.BackColor = System.Drawing.Color.Transparent;
            this.handViewRight.Hand = null;
            this.handViewRight.Location = new System.Drawing.Point(846, 173);
            this.handViewRight.Mode = Mahjong.View.Controls.Mode.ForRightSide;
            this.handViewRight.Name = "handViewRight";
            this.handViewRight.Size = new System.Drawing.Size(20, 387);
            this.handViewRight.TabIndex = 3;
            // 
            // handViewLeft
            // 
            this.handViewLeft.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.handViewLeft.BackColor = System.Drawing.Color.Transparent;
            this.handViewLeft.Hand = null;
            this.handViewLeft.Location = new System.Drawing.Point(26, 173);
            this.handViewLeft.Mode = Mahjong.View.Controls.Mode.ForLeftSide;
            this.handViewLeft.Name = "handViewLeft";
            this.handViewLeft.Size = new System.Drawing.Size(20, 387);
            this.handViewLeft.TabIndex = 2;
            // 
            // discardPlayer
            // 
            this.discardPlayer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.discardPlayer.BackColor = System.Drawing.Color.Transparent;
            this.discardPlayer.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.discardPlayer.IsDiscard = true;
            this.discardPlayer.Location = new System.Drawing.Point(317, 407);
            this.discardPlayer.Name = "discardPlayer";
            this.discardPlayer.Size = new System.Drawing.Size(260, 215);
            this.discardPlayer.TabIndex = 1;
            // 
            // handViewPlayer
            // 
            this.handViewPlayer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.handViewPlayer.BackColor = System.Drawing.Color.Transparent;
            this.handViewPlayer.Hand = null;
            this.handViewPlayer.Location = new System.Drawing.Point(147, 624);
            this.handViewPlayer.Mode = Mahjong.View.Controls.Mode.Front;
            this.handViewPlayer.Name = "handViewPlayer";
            this.handViewPlayer.Size = new System.Drawing.Size(598, 73);
            this.handViewPlayer.TabIndex = 0;
            this.handViewPlayer.Discard += new Mahjong.View.Controls.HandView.DiscardEventHandler(this.DiscardOrRiichi);
            // 
            // playerOpenTiles
            // 
            this.playerOpenTiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.playerOpenTiles.BackColor = System.Drawing.Color.Transparent;
            this.playerOpenTiles.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.playerOpenTiles.IsDiscard = false;
            this.playerOpenTiles.Location = new System.Drawing.Point(178, 633);
            this.playerOpenTiles.Name = "playerOpenTiles";
            this.playerOpenTiles.Size = new System.Drawing.Size(688, 64);
            this.playerOpenTiles.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(891, 749);
            this.Controls.Add(this.leftRiichi);
            this.Controls.Add(this.rightRiichi);
            this.Controls.Add(this.playerRiichi);
            this.Controls.Add(this.oppositeRiichi);
            this.Controls.Add(this.autoButton);
            this.Controls.Add(this.tsumoButton);
            this.Controls.Add(this.riichiButton);
            this.Controls.Add(this.ronButton);
            this.Controls.Add(this.pasButton);
            this.Controls.Add(this.chiButton);
            this.Controls.Add(this.ponButton);
            this.Controls.Add(this.kanButton);
            this.Controls.Add(this.discrardLeft);
            this.Controls.Add(this.discardRight);
            this.Controls.Add(this.discardOpp);
            this.Controls.Add(this.handViewOpp);
            this.Controls.Add(this.handViewRight);
            this.Controls.Add(this.handViewLeft);
            this.Controls.Add(this.discardPlayer);
            this.Controls.Add(this.handViewPlayer);
            this.Controls.Add(this.playerOpenTiles);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.oppositeRiichi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerRiichi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightRiichi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftRiichi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private View.Controls.HandView handViewPlayer;
        private View.Controls.OpenTilesView discardPlayer;
        private View.Controls.HandView handViewLeft;
        private View.Controls.HandView handViewRight;
        private View.Controls.HandView handViewOpp;
        private View.Controls.OpenTilesView discardOpp;
        private View.Controls.OpenTilesView discardRight;
        private View.Controls.OpenTilesView discrardLeft;
        private System.Windows.Forms.Button kanButton;
        private System.Windows.Forms.Button ponButton;
        private System.Windows.Forms.Button chiButton;
        private System.Windows.Forms.Button pasButton;
        private View.Controls.OpenTilesView playerOpenTiles;
        private System.Windows.Forms.Button ronButton;
        private System.Windows.Forms.Button riichiButton;
        private System.Windows.Forms.Button tsumoButton;
        private System.Windows.Forms.Button autoButton;
        private System.Windows.Forms.PictureBox oppositeRiichi;
        private System.Windows.Forms.PictureBox playerRiichi;
        private System.Windows.Forms.PictureBox rightRiichi;
        private System.Windows.Forms.PictureBox leftRiichi;
    }
}


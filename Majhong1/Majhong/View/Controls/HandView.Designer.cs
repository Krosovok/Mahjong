namespace Mahjong.View.Controls
{
    partial class HandView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandView));
            this.sideForHand = new System.Windows.Forms.PictureBox();
            this.sideForNewTile = new System.Windows.Forms.PictureBox();
            this.newTile = new Mahjong.View.Controls.TileBox();
            this.tile13 = new Mahjong.View.Controls.TileBox();
            this.tile12 = new Mahjong.View.Controls.TileBox();
            this.tile11 = new Mahjong.View.Controls.TileBox();
            this.tile10 = new Mahjong.View.Controls.TileBox();
            this.tile9 = new Mahjong.View.Controls.TileBox();
            this.tile8 = new Mahjong.View.Controls.TileBox();
            this.tile7 = new Mahjong.View.Controls.TileBox();
            this.tile6 = new Mahjong.View.Controls.TileBox();
            this.tile5 = new Mahjong.View.Controls.TileBox();
            this.tile4 = new Mahjong.View.Controls.TileBox();
            this.tile3 = new Mahjong.View.Controls.TileBox();
            this.tile2 = new Mahjong.View.Controls.TileBox();
            this.tile1 = new Mahjong.View.Controls.TileBox();
            ((System.ComponentModel.ISupportInitialize)(this.sideForHand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sideForNewTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile1)).BeginInit();
            this.SuspendLayout();
            // 
            // sideForHand
            // 
            this.sideForHand.BackColor = System.Drawing.Color.Transparent;
            this.sideForHand.Image = global::Mahjong.Properties.Resources.left;
            this.sideForHand.Location = new System.Drawing.Point(140, 163);
            this.sideForHand.Margin = new System.Windows.Forms.Padding(0);
            this.sideForHand.Name = "sideForHand";
            this.sideForHand.Size = new System.Drawing.Size(20, 41);
            this.sideForHand.TabIndex = 14;
            this.sideForHand.TabStop = false;
            this.sideForHand.Visible = false;
            // 
            // sideForNewTile
            // 
            this.sideForNewTile.Image = global::Mahjong.Properties.Resources.left;
            this.sideForNewTile.Location = new System.Drawing.Point(140, 219);
            this.sideForNewTile.Margin = new System.Windows.Forms.Padding(0);
            this.sideForNewTile.Name = "sideForNewTile";
            this.sideForNewTile.Size = new System.Drawing.Size(20, 41);
            this.sideForNewTile.TabIndex = 15;
            this.sideForNewTile.TabStop = false;
            this.sideForNewTile.Visible = false;
            // 
            // newTile
            // 
            this.newTile.BackColor = System.Drawing.Color.Transparent;
            this.newTile.CenterLocation = new System.Drawing.Point(577, 42);
            this.newTile.Closed = false;
            this.newTile.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.newTile.Highlighted = false;
            this.newTile.Image = ((System.Drawing.Image)(resources.GetObject("newTile.Image")));
            this.newTile.Jumping = false;
            this.newTile.Location = new System.Drawing.Point(556, 10);
            this.newTile.Margin = new System.Windows.Forms.Padding(10);
            this.newTile.Mode = Mahjong.View.Controls.Mode.Front;
            this.newTile.Name = "newTile";
            this.newTile.Size = new System.Drawing.Size(42, 64);
            this.newTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.newTile.TabIndex = 13;
            this.newTile.TabStop = false;
            this.newTile.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.newTile.TileValue = '5';
            this.newTile.TurnedSideways = false;
            this.newTile.Unavailible = false;
            this.newTile.Click += new System.EventHandler(this.tile_Click);
            this.newTile.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.newTile.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile13
            // 
            this.tile13.BackColor = System.Drawing.Color.Transparent;
            this.tile13.CenterLocation = new System.Drawing.Point(525, 42);
            this.tile13.Closed = false;
            this.tile13.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile13.Highlighted = false;
            this.tile13.Image = ((System.Drawing.Image)(resources.GetObject("tile13.Image")));
            this.tile13.Jumping = false;
            this.tile13.Location = new System.Drawing.Point(504, 10);
            this.tile13.Margin = new System.Windows.Forms.Padding(0);
            this.tile13.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile13.Name = "tile13";
            this.tile13.Size = new System.Drawing.Size(42, 64);
            this.tile13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile13.TabIndex = 12;
            this.tile13.TabStop = false;
            this.tile13.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile13.TileValue = '9';
            this.tile13.TurnedSideways = false;
            this.tile13.Unavailible = false;
            this.tile13.Click += new System.EventHandler(this.tile_Click);
            this.tile13.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile13.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile12
            // 
            this.tile12.BackColor = System.Drawing.Color.Transparent;
            this.tile12.CenterLocation = new System.Drawing.Point(483, 42);
            this.tile12.Closed = false;
            this.tile12.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile12.Highlighted = false;
            this.tile12.Image = ((System.Drawing.Image)(resources.GetObject("tile12.Image")));
            this.tile12.Jumping = false;
            this.tile12.Location = new System.Drawing.Point(462, 10);
            this.tile12.Margin = new System.Windows.Forms.Padding(0);
            this.tile12.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile12.Name = "tile12";
            this.tile12.Size = new System.Drawing.Size(42, 64);
            this.tile12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile12.TabIndex = 11;
            this.tile12.TabStop = false;
            this.tile12.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile12.TileValue = '9';
            this.tile12.TurnedSideways = false;
            this.tile12.Unavailible = false;
            this.tile12.Click += new System.EventHandler(this.tile_Click);
            this.tile12.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile12.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile11
            // 
            this.tile11.BackColor = System.Drawing.Color.Transparent;
            this.tile11.CenterLocation = new System.Drawing.Point(441, 42);
            this.tile11.Closed = false;
            this.tile11.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile11.Highlighted = false;
            this.tile11.Image = ((System.Drawing.Image)(resources.GetObject("tile11.Image")));
            this.tile11.Jumping = false;
            this.tile11.Location = new System.Drawing.Point(420, 10);
            this.tile11.Margin = new System.Windows.Forms.Padding(0);
            this.tile11.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile11.Name = "tile11";
            this.tile11.Size = new System.Drawing.Size(42, 64);
            this.tile11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile11.TabIndex = 10;
            this.tile11.TabStop = false;
            this.tile11.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile11.TileValue = '9';
            this.tile11.TurnedSideways = false;
            this.tile11.Unavailible = false;
            this.tile11.Click += new System.EventHandler(this.tile_Click);
            this.tile11.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile11.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile10
            // 
            this.tile10.BackColor = System.Drawing.Color.Transparent;
            this.tile10.CenterLocation = new System.Drawing.Point(399, 42);
            this.tile10.Closed = false;
            this.tile10.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile10.Highlighted = false;
            this.tile10.Image = ((System.Drawing.Image)(resources.GetObject("tile10.Image")));
            this.tile10.Jumping = false;
            this.tile10.Location = new System.Drawing.Point(378, 10);
            this.tile10.Margin = new System.Windows.Forms.Padding(0);
            this.tile10.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile10.Name = "tile10";
            this.tile10.Size = new System.Drawing.Size(42, 64);
            this.tile10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile10.TabIndex = 9;
            this.tile10.TabStop = false;
            this.tile10.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile10.TileValue = '8';
            this.tile10.TurnedSideways = false;
            this.tile10.Unavailible = false;
            this.tile10.Click += new System.EventHandler(this.tile_Click);
            this.tile10.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile10.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile9
            // 
            this.tile9.BackColor = System.Drawing.Color.Transparent;
            this.tile9.CenterLocation = new System.Drawing.Point(357, 42);
            this.tile9.Closed = false;
            this.tile9.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile9.Highlighted = false;
            this.tile9.Image = ((System.Drawing.Image)(resources.GetObject("tile9.Image")));
            this.tile9.Jumping = false;
            this.tile9.Location = new System.Drawing.Point(336, 10);
            this.tile9.Margin = new System.Windows.Forms.Padding(0);
            this.tile9.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile9.Name = "tile9";
            this.tile9.Size = new System.Drawing.Size(42, 64);
            this.tile9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile9.TabIndex = 8;
            this.tile9.TabStop = false;
            this.tile9.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile9.TileValue = '7';
            this.tile9.TurnedSideways = false;
            this.tile9.Unavailible = false;
            this.tile9.Click += new System.EventHandler(this.tile_Click);
            this.tile9.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile9.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile8
            // 
            this.tile8.BackColor = System.Drawing.Color.Transparent;
            this.tile8.CenterLocation = new System.Drawing.Point(315, 42);
            this.tile8.Closed = false;
            this.tile8.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile8.Highlighted = false;
            this.tile8.Image = ((System.Drawing.Image)(resources.GetObject("tile8.Image")));
            this.tile8.Jumping = false;
            this.tile8.Location = new System.Drawing.Point(294, 10);
            this.tile8.Margin = new System.Windows.Forms.Padding(0);
            this.tile8.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile8.Name = "tile8";
            this.tile8.Size = new System.Drawing.Size(42, 64);
            this.tile8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile8.TabIndex = 7;
            this.tile8.TabStop = false;
            this.tile8.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile8.TileValue = '6';
            this.tile8.TurnedSideways = false;
            this.tile8.Unavailible = false;
            this.tile8.Click += new System.EventHandler(this.tile_Click);
            this.tile8.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile8.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile7
            // 
            this.tile7.BackColor = System.Drawing.Color.Transparent;
            this.tile7.CenterLocation = new System.Drawing.Point(273, 42);
            this.tile7.Closed = false;
            this.tile7.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile7.Highlighted = false;
            this.tile7.Image = ((System.Drawing.Image)(resources.GetObject("tile7.Image")));
            this.tile7.Jumping = false;
            this.tile7.Location = new System.Drawing.Point(252, 10);
            this.tile7.Margin = new System.Windows.Forms.Padding(0);
            this.tile7.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile7.Name = "tile7";
            this.tile7.Size = new System.Drawing.Size(42, 64);
            this.tile7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile7.TabIndex = 6;
            this.tile7.TabStop = false;
            this.tile7.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile7.TileValue = '5';
            this.tile7.TurnedSideways = false;
            this.tile7.Unavailible = false;
            this.tile7.Click += new System.EventHandler(this.tile_Click);
            this.tile7.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile7.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile6
            // 
            this.tile6.BackColor = System.Drawing.Color.Transparent;
            this.tile6.CenterLocation = new System.Drawing.Point(231, 42);
            this.tile6.Closed = false;
            this.tile6.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile6.Highlighted = false;
            this.tile6.Image = ((System.Drawing.Image)(resources.GetObject("tile6.Image")));
            this.tile6.Jumping = false;
            this.tile6.Location = new System.Drawing.Point(210, 10);
            this.tile6.Margin = new System.Windows.Forms.Padding(0);
            this.tile6.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile6.Name = "tile6";
            this.tile6.Size = new System.Drawing.Size(42, 64);
            this.tile6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile6.TabIndex = 5;
            this.tile6.TabStop = false;
            this.tile6.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile6.TileValue = '4';
            this.tile6.TurnedSideways = false;
            this.tile6.Unavailible = false;
            this.tile6.Click += new System.EventHandler(this.tile_Click);
            this.tile6.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile6.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile5
            // 
            this.tile5.BackColor = System.Drawing.Color.Transparent;
            this.tile5.CenterLocation = new System.Drawing.Point(189, 42);
            this.tile5.Closed = false;
            this.tile5.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile5.Highlighted = false;
            this.tile5.Image = ((System.Drawing.Image)(resources.GetObject("tile5.Image")));
            this.tile5.Jumping = false;
            this.tile5.Location = new System.Drawing.Point(168, 10);
            this.tile5.Margin = new System.Windows.Forms.Padding(0);
            this.tile5.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile5.Name = "tile5";
            this.tile5.Size = new System.Drawing.Size(42, 64);
            this.tile5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile5.TabIndex = 4;
            this.tile5.TabStop = false;
            this.tile5.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile5.TileValue = '3';
            this.tile5.TurnedSideways = false;
            this.tile5.Unavailible = false;
            this.tile5.Click += new System.EventHandler(this.tile_Click);
            this.tile5.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile5.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile4
            // 
            this.tile4.BackColor = System.Drawing.Color.Transparent;
            this.tile4.CenterLocation = new System.Drawing.Point(147, 42);
            this.tile4.Closed = false;
            this.tile4.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile4.Highlighted = false;
            this.tile4.Image = ((System.Drawing.Image)(resources.GetObject("tile4.Image")));
            this.tile4.Jumping = false;
            this.tile4.Location = new System.Drawing.Point(126, 10);
            this.tile4.Margin = new System.Windows.Forms.Padding(0);
            this.tile4.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile4.Name = "tile4";
            this.tile4.Size = new System.Drawing.Size(42, 64);
            this.tile4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile4.TabIndex = 3;
            this.tile4.TabStop = false;
            this.tile4.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile4.TileValue = '2';
            this.tile4.TurnedSideways = false;
            this.tile4.Unavailible = false;
            this.tile4.Click += new System.EventHandler(this.tile_Click);
            this.tile4.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile4.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile3
            // 
            this.tile3.BackColor = System.Drawing.Color.Transparent;
            this.tile3.CenterLocation = new System.Drawing.Point(105, 42);
            this.tile3.Closed = false;
            this.tile3.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile3.Highlighted = false;
            this.tile3.Image = ((System.Drawing.Image)(resources.GetObject("tile3.Image")));
            this.tile3.Jumping = false;
            this.tile3.Location = new System.Drawing.Point(84, 10);
            this.tile3.Margin = new System.Windows.Forms.Padding(0);
            this.tile3.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile3.Name = "tile3";
            this.tile3.Size = new System.Drawing.Size(42, 64);
            this.tile3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile3.TabIndex = 2;
            this.tile3.TabStop = false;
            this.tile3.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile3.TileValue = '1';
            this.tile3.TurnedSideways = false;
            this.tile3.Unavailible = false;
            this.tile3.Click += new System.EventHandler(this.tile_Click);
            this.tile3.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile3.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile2
            // 
            this.tile2.BackColor = System.Drawing.Color.Transparent;
            this.tile2.CenterLocation = new System.Drawing.Point(63, 42);
            this.tile2.Closed = false;
            this.tile2.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile2.Highlighted = false;
            this.tile2.Image = ((System.Drawing.Image)(resources.GetObject("tile2.Image")));
            this.tile2.Jumping = false;
            this.tile2.Location = new System.Drawing.Point(42, 10);
            this.tile2.Margin = new System.Windows.Forms.Padding(0);
            this.tile2.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile2.Name = "tile2";
            this.tile2.Size = new System.Drawing.Size(42, 64);
            this.tile2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile2.TabIndex = 1;
            this.tile2.TabStop = false;
            this.tile2.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile2.TileValue = '1';
            this.tile2.TurnedSideways = false;
            this.tile2.Unavailible = false;
            this.tile2.Click += new System.EventHandler(this.tile_Click);
            this.tile2.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile2.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // tile1
            // 
            this.tile1.BackColor = System.Drawing.Color.Transparent;
            this.tile1.CenterLocation = new System.Drawing.Point(21, 42);
            this.tile1.Closed = false;
            this.tile1.Direction = Mahjong.View.Controls.Direction.ToPlayer;
            this.tile1.Highlighted = false;
            this.tile1.Image = ((System.Drawing.Image)(resources.GetObject("tile1.Image")));
            this.tile1.Jumping = false;
            this.tile1.Location = new System.Drawing.Point(0, 10);
            this.tile1.Margin = new System.Windows.Forms.Padding(0);
            this.tile1.Mode = Mahjong.View.Controls.Mode.Front;
            this.tile1.Name = "tile1";
            this.tile1.Size = new System.Drawing.Size(42, 64);
            this.tile1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tile1.TabIndex = 0;
            this.tile1.TabStop = false;
            this.tile1.TileCategory = Mahjong.Model.Tiles.TileCategory.Man;
            this.tile1.TileValue = '1';
            this.tile1.TurnedSideways = false;
            this.tile1.Unavailible = false;
            this.tile1.Click += new System.EventHandler(this.tile_Click);
            this.tile1.MouseEnter += new System.EventHandler(this.newTile_MouseEnter);
            this.tile1.MouseLeave += new System.EventHandler(this.newTile_MouseLeave);
            // 
            // HandView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.newTile);
            this.Controls.Add(this.tile13);
            this.Controls.Add(this.tile12);
            this.Controls.Add(this.tile11);
            this.Controls.Add(this.tile10);
            this.Controls.Add(this.tile9);
            this.Controls.Add(this.tile8);
            this.Controls.Add(this.tile7);
            this.Controls.Add(this.tile6);
            this.Controls.Add(this.tile5);
            this.Controls.Add(this.tile4);
            this.Controls.Add(this.tile3);
            this.Controls.Add(this.tile2);
            this.Controls.Add(this.tile1);
            this.Controls.Add(this.sideForNewTile);
            this.Controls.Add(this.sideForHand);
            this.Name = "HandView";
            this.Size = new System.Drawing.Size(606, 402);
            this.EnabledChanged += new System.EventHandler(this.HandView_EnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.sideForHand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sideForNewTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tile1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected TileBox tile1;
        protected TileBox tile2;
        protected TileBox tile3;
        protected TileBox tile4;
        protected TileBox tile5;
        protected TileBox tile6;
        protected TileBox tile7;
        protected TileBox tile8;
        protected TileBox tile9;
        protected TileBox tile10;
        protected TileBox tile11;
        protected TileBox tile12;
        protected TileBox tile13;
        protected TileBox newTile;
        protected System.Windows.Forms.PictureBox sideForHand;
        protected System.Windows.Forms.PictureBox sideForNewTile;



    }
}

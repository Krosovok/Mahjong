using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mahjong.Model.Tiles;
using Mahjong.Properties;
using System.Drawing.Imaging;

namespace Mahjong.View.Controls
{
    /// <summary>
    /// Режим отображения тайла.
    /// </summary>
    public enum Mode
    {
        Front,
        Back,
        ForLeftSide,
        ForRightSide,
        Face
    } 

    /// <summary>
    /// Направление, со стороны которого тайл выглядит прямо.
    /// То есть то, насколько градусов он отвёрнут от игрока.
    /// </summary>
    public enum Direction
    {
        ToPlayer = 0,
        ToRightPlayer = 90,
        ToOppositePlayer = 180,
        ToLeftPlayer = 270
    }

    ///// <summary>
    ///// Особенности отображения тайла.
    ///// </summary>
    //public enum Display
    //{
    //    None,
    //    Unavailible,

    //}

    /// <summary>
    /// Элемент управления для отображения тайла.
    /// </summary>
    public partial class TileBox : PictureBox
    {
        private Tile tile;
        private Mode mode;
        private bool turnedSideways;
        private Direction direction;
        private bool jumping;
        private bool unavailable;
        private bool highlighted;
        private bool closed;

        public TileBox()
        {
            InitializeComponent();
            mode = Mode.Face;
            direction = Direction.ToPlayer;
            SizeMode = PictureBoxSizeMode.CenterImage;
        }

        public TileBox(Tile tile) : this()
        {
            this.Tile = tile;
        }

        /// <summary>
        /// Направление, со стороны которого тайл выглядит прямо,
        /// если свойство TurnedSideways не установленно в true.
        /// Работает только с режимом Mode.Face.
        /// </summary>
        public Direction Direction
        {
            get { return direction; }
            set
            {
                if (this.Mode != Mode.Face)
                    return;

                int dif = (int)this.direction - (int)value;

                if (dif < 0)
                    dif = Constants.ROUND + dif;

                switch (dif)
                {
                    case (int)Direction.ToPlayer: // 0
                        RotateFlip(RotateFlipType.RotateNoneFlipNone); // Оставить как есть.
                        break;
                    case (int)Direction.ToRightPlayer: // 90
                        RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case (int)Direction.ToOppositePlayer: // 180
                        RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case (int)Direction.ToLeftPlayer: // 270
                        RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                this.direction = value;
            }
        }
        /// <summary>
        /// Задаёт или возвращает, является ли тайл повёрнутым
        /// на 90 градусов относительно своего положения.
        /// </summary>
        public bool TurnedSideways
        {
            get { return turnedSideways; }
            set
            {
                if (turnedSideways != value && this.Mode == Mode.Face && this.Image != null) // Есть необходимость что-то менять.
                {
                    if (value)
                        RotateFlip(RotateFlipType.Rotate90FlipNone);
                    else
                        RotateFlip(RotateFlipType.Rotate270FlipNone);
                    turnedSideways = value;
                    //this.Size = Image.Size;
                }
            }
        }
        /// <summary>
        /// Задаёт или получает тайл, который отображает этот элемент управления.
        /// </summary>
        public Tile Tile
        {
            get { return tile; }
            set
            {
                //this.direction = Direction.ToPlayer;
                Direction dir = this.Direction;
                this.direction = View.Controls.Direction.ToPlayer;

                this.tile = value;

                if (tile == null)
                {
                    this.Visible = false;
                    this.Direction = dir;
                    return;
                }
                else
                    this.Visible = true;
 
                switch (Mode)
                {
                    case Mode.Face:
                        this.Image = tile.FaceImage.Clone() as Image;
                        break;
                    case Mode.Front:
                        this.Image = tile.HandImage.Clone() as Image;
                        break;
                    case Mode.Back:
                        this.Image = Resources.back;
                        break;
                    case Mode.ForLeftSide:
                        this.Image = Resources.left_top;
                        break;
                    case Mode.ForRightSide:
                        this.Image = Resources.right_top;
                        break;
                }

                if (Mode != Mode.Front && Mode != Mode.Back)
                    this.Size = this.Image.Size;
                else
                    this.Size = new Size(Constants.TILE_WIDTH, this.Image.Height);

                this.Direction = dir;
            }
        }
        /// <summary>
        /// Задаёт или получает то, как тайл будет отбражён.
        /// </summary>
        public Mode Mode 
        {
            get { return mode; }
            set {
                unavailable = false;
                highlighted = false;
                Jumping = false;
                turnedSideways = false;
                closed = false;

                mode = value;
                Tile = Tile;
            } 
        }
        /// <summary>
        /// Позволяет задавать тайл в конструкторе. Использовать перед заданием значения тайла свойством TileValue.
        /// Не использовать программно, для этого есть Tile.
        /// </summary>
        public TileCategory TileCategory 
        {
            get; 
            set; 
        }
        /// <summary>
        /// Позволяет задавать тайл в конструкторе. Использовать после задания категории свойством TileCategory.
        /// Не использовать программно, для этого есть Tile.
        /// </summary>
        public char TileValue {
            get { return (tile ?? new ManTile('1')).Type ; }
            set {
                switch (this.TileCategory)
                {
                    case TileCategory.Man:
                        this.Tile = new ManTile(value);
                        break;
                    case TileCategory.Pin:
                        this.Tile = new PinTile(value);
                        break;
                    case TileCategory.Sou:
                        this.Tile = new SouTile(value);
                        break;
                    case TileCategory.Wind:
                        this.Tile = new WindTile(value);
                        break;
                    case TileCategory.Dragon:
                        this.Tile = new DragonTile(value);
                        break;
                }
            } 
        }
        /// <summary>
        /// Получает или задаёт местоположение элемента управления по его центру 
        /// относительно левого верхнего угла контейнера.
        /// </summary>
        public Point CenterLocation
        {
            get { return new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2); }
            set
            {
                this.Location = new Point(value.X - this.Width / 2, value.Y - this.Height / 2);
            }
        }
        /// <summary>
        /// Получает или задаёт приподнят ли тайл.
        /// </summary>
        public bool Jumping
        {
            get { return jumping; }
            set
            {
                if (jumping != value && Mode == Mode.Front)
                {
                    jumping = value;
                    this.Location = new Point(Location.X, Location.Y
                        + (jumping ? -Constants.TILE_JUMPING : Constants.TILE_JUMPING));
                    //if (jumping)
                    //    this.Location = new Point(Location.X, Location.Y + Constants.TILE_JUMPING);
                    //else
                    //    this.Location = new Point(Location.X, Location.Y - Constants.TILE_JUMPING);
                }
            }
        }
        ///// <summary>
        ///// Получает или задаёт то, является ли тайл доступным для выбора.
        ///// Когда тайл недоступен, он окрашивается в красноватый оттенок 
        ///// и не реагирует на действия пользователя. Доступен только в Mode.Front.
        ///// </summary>
        public bool Unavailible
        {
            get { return unavailable; }
            set
            {
                if (unavailable != value && this.Mode == Mode.Front && Tile != null) // || this.Mode == Mode.Face)
                {
                    unavailable = value;

                    Bitmap bmp = new Bitmap(this.Image.Width, this.Image.Height);

                    ImageAttributes imageAttributes = new ImageAttributes();
                    float[][] colorMatrixElements =
                    {
                        new float[] {1, 0, 0, 0, 0},
                        new float[] {0, 1, 0, 0, 0},
                        new float[] {0, 0, 1, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                    };
                    ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                    if (unavailable)
                    {
                        colorMatrix.Matrix41 = -0.3f;
                        colorMatrix.Matrix42 = -0.4f;
                    }
                    else
                    {
                        //if (this.Mode == Mode.Front) 
                            this.Image = Tile == null ? null : Tile.HandImage;
                        //else if (this.Mode == Mode.Face)
                        //    this.Image = Tile == null ? null : Tile.FaceImage;
                    }

                    imageAttributes.SetColorMatrix(
                        colorMatrix,
                        ColorMatrixFlag.Default,
                        ColorAdjustType.Bitmap
                        );

                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawImage(
                           this.Image,
                           new Rectangle(0, 0, Image.Width, Image.Height),
                           0, 0,
                           Image.Width, Image.Height,
                           GraphicsUnit.Pixel,
                           imageAttributes);
                    }

                    this.Image = bmp;

                    this.Refresh();
                    this.Enabled = !unavailable;
                }

            }
        }
        /// <summary>
        /// Получает или задаёт, подсвечен ли тайл зелёным цветом.
        /// Доступен только в Mode.Face.
        /// </summary>
        public bool Highlighted
        {
            get { return highlighted; }
            set
            {
                if (highlighted != value && this.Mode == Mode.Face && Image != null) // || this.Mode == Mode.Front)
                {
                    highlighted = value;

                    Bitmap bmp = new Bitmap(this.Image.Width, this.Image.Height);

                    ImageAttributes imageAttributes = new ImageAttributes();
                    float[][] colorMatrixElements =
                    {
                        new float[] {1, 0, 0, 0, 0},
                        new float[] {0, 1, 0, 0, 0},
                        new float[] {0, 0, 1, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                    };
                    ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                    if (highlighted)
                    {
                        colorMatrix.Matrix40 = -0.4f;
                        //colorMatrix.Matrix41 = -0.1f;
                        colorMatrix.Matrix42 = -0.4f;
                    }
                    else
                    {
                        // Необходимо будет заново повернуть изображение.
                        // Запоминаем повёрнутость.
                        Direction dir = this.Direction;
                        bool turned = this.TurnedSideways;
                        // Обнуляем.
                        direction = Direction.ToPlayer;
                        turnedSideways = false;

                        // Задаём изображение.
                        this.Image = Tile == null ? null : Tile.FaceImage;

                        // Заново поворачиваем.
                        this.Direction = dir;
                        this.TurnedSideways = turned;
                    }

                    imageAttributes.SetColorMatrix(
                        colorMatrix,
                        ColorMatrixFlag.Default,
                        ColorAdjustType.Bitmap
                        );

                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawImage(
                           this.Image,
                           new Rectangle(0, 0, Image.Width, Image.Height),
                           0, 0,
                           Image.Width, Image.Height,
                           GraphicsUnit.Pixel,
                           imageAttributes);
                    }

                    //
                    this.Image = bmp;
                    //

                    this.Refresh();
                    //this.Enabled = !highlighted;
                }
            }
        }
        /// <summary>
        /// Делает отображение лицевой стороны рубашкой.
        /// Для закрытого кана.
        /// </summary>
        public bool Closed
        {
            get { return closed; }
            set 
            {
                if (this.Closed != value && Mode == Mode.Face)
                {
                    closed = value;
                    if (closed)
                        this.Image = Resources.face_down_40x43px;
                    else
                        Tile = Tile; // Update tile image.
                }
            }
        }

        private void RotateFlip(RotateFlipType type)
        {
            if (this.Image == null)
                return;
            Image image = this.Image;
            image.RotateFlip(type);
            this.Image = image;
            this.Size = Image.Size;
        }

        private void TileBox_EnabledChanged(object sender, EventArgs e)
        {

        }

        //private void ChangeImage()
        //{
            
        //}

        //private void TileBox_Paint(object sender, PaintEventArgs e)
        //{
        //    //this
        //}
    }
}

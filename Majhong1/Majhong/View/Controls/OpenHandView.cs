using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mahjong.Properties;

namespace Mahjong.View.Controls
{
    public partial class OpenHandView : HandView
    {
        public OpenHandView()
        {
            InitializeComponent();
        }

        public override Mode Mode
        {
            get
            {
                return base.Mode;
            }
            set
            {
                //Bitmap btm;
                this.mode = value;
                List<TileBox> tileBoxes = this.Controls.OfType<TileBox>().ToList(); // Тайлы идут задом напрерёд?
                //tileBoxes.ForEach(tileBox => tileBox.Mode = value);
                tileBoxes.ForEach(tileBox => tileBox.Mode = Mode.Face);

                Action<List<TileBox>, int> changeLocation;
                switch (mode)
                {
                    case Mode.Back: // Расставляем тайлы с начала.
                        changeLocation =
                            (tiles, idx) =>
                            {
                                tiles[idx].Direction = Direction.ToOppositePlayer;
                                tiles[idx].Location =
                                    new Point(
                                        idx * (tiles[0].Width + tiles[idx].Margin.Right * 2) + newTile.Margin.Right - tiles[idx].Margin.Right,
                                        0); 
                            };
                        break;

                    case Mode.Front: // Расставляем тайлы с конца. 
                        changeLocation =
                            (tiles, idx) =>
                            {
                                tiles[idx].Direction = Direction.ToPlayer;
                                tiles[idx].Location =
                                    new Point(
                                        (tiles.Count - idx - 1) * (tiles[0].Width + tiles[idx].Margin.Left + tiles[idx].Margin.Right),//
                                    //0);
                                        0);
                            };
                        break;

                    case Mode.ForRightSide:
                        changeLocation =
                            (tiles, idx) =>
                            {
                                tiles[idx].Direction = Direction.ToRightPlayer;
                                tiles[idx].Location =
                                    new Point(0,
                                    idx * (tiles[0].Height + (newTile.Margin.All - tiles[idx].Margin.All))//
                                    );
                            };
                        //btm = (Bitmap)Resources.ResourceManager.GetObject(Constants.RIGHT);
                        //this.sideForNewTile.Image = btm;
                        //this.sideForHand.Image = btm;
                        break;

                    case Mode.ForLeftSide:
                        changeLocation =
                            (tiles, idx) =>
                            {
                                tiles[idx].Direction = Direction.ToLeftPlayer;
                                tiles[idx].Location =
                                    new Point(0,
                                    (tiles.Count - idx - 1) * (tiles[0].Height + tiles[idx].Margin.All)//
                                    );
                            };
                        //btm = (Bitmap)Resources.ResourceManager.GetObject(Constants.LEFT);
                        //this.sideForNewTile.Image = btm;
                        //this.sideForHand.Image = btm;
                        break;

                    default://
                        changeLocation = (tiles, idx) => tiles[idx].Location = tiles[idx].Location;//
                        break;//
                }

                { // Задание расположения.
                    int i = 0;
                    for (; i < tileBoxes.Count; i++)
                    {
                        changeLocation(tileBoxes, i);
                    }
                    LocateNewTile(i); //
                    //LocateSideParts();//
                }

                if (this.Controls.OfType<PictureBox>().Where(box => box.Visible).Count() != 0)
                    this.Size = new Size(
                        //tileBoxes.Where(box => box.Tile != null).Max(box => box.Location.X + box.Size.Width),
                        //tileBoxes.Where(box => box.Tile!= null).Max(box => box.Location.Y + box.Size.Height)
                    this.Controls.OfType<PictureBox>().Where(box => box.Visible).Max(box => box.Location.X + box.Size.Width),
                    this.Controls.OfType<PictureBox>().Where(box => box.Visible).Max(box => box.Location.Y + box.Size.Height)// + Constants.TILE_JUMPING
                    );

            }
        }

        protected override void LocateNewTile(int i)
        {
            switch (mode)
            {
                case Mode.Back:
                    newTile.Location = new Point(
                        (Constants.MAX_HAND_SIZE - i - 1) * newTile.Width,
                        0);
                    break;
                case Mode.Front:
                    newTile.Location = new Point(
                        (i) * (newTile.Width + tile1.Margin.Left * 2) + newTile.Margin.Left,//
                        0);
                        //Constants.TILE_JUMPING);
                    //this.Controls.OfType<TileBox>().Min(box => box.Location.Y));
                    break;

                case Mode.ForRightSide:
                    newTile.Location = new Point(0,
                        (Constants.MAX_HAND_SIZE - i - 1) * newTile.Height
                        );
                    break;
                case Mode.ForLeftSide:
                    newTile.Location = new Point(0,
                        (i) * newTile.Height + newTile.Margin.Top
                        );
                    break;
            }
            LocateSideParts();
        }

        protected override void LocateSideParts()
        {
            this.sideForHand.Visible = false;
            this.sideForNewTile.Visible = false;
        }

        private void tile8_LocationChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mahjong.Model.Game;

namespace Mahjong.View.Controls
{
    public partial class RoundData : UserControl
    {
        private Cardinal cardinal;
        private Number number;

        public RoundData()
        {
            InitializeComponent();

            leftRiichi.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            rightRiichi.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

            number = Model.Game.Number.First;
        }

        public RoundData(Cardinal card, Number num) 
            : this()
        {
            Cardinal = card;
            Number = num;
        }

        public Cardinal Cardinal
        {
            get { return cardinal; }
            set 
            {
                cardinal = value;
                SetRound();
            }
        }
        public Number Number
        {
            get { return number; }
            set 
            {
                number = value;
                SetRound();
            }
        }
        public bool this[Direction dir]
        {
            get
            {
                switch (dir)
                {
                    case Direction.ToPlayer:
                        return playerRiichi.Visible;
                    case Direction.ToLeftPlayer:
                        return leftRiichi.Visible;
                    case Direction.ToOppositePlayer:
                        return oppositeRiichi.Visible;
                    case Direction.ToRightPlayer:
                        return rightRiichi.Visible;
                    default:
                        throw new Exceptions.MahjongException();
                }
            }
            set
            {
                switch (dir)
                {
                    case Direction.ToPlayer:
                        playerRiichi.Visible = value;
                        return;
                    case Direction.ToLeftPlayer:
                        leftRiichi.Visible = value;
                        return;
                    case Direction.ToOppositePlayer:
                        oppositeRiichi.Visible = value;
                        return;
                    case Direction.ToRightPlayer:
                        rightRiichi.Visible = value;
                        return;
                    default:
                        throw new Exceptions.MahjongException();
                }
            }
        }

        private void SetRound()
        {
            string strCardinal = String.Empty;
            switch (cardinal)
            {
                case Cardinal.East:
                    strCardinal = Constants.EAST;
                    break;
                case Cardinal.South:
                    strCardinal = Constants.SOUTH;
                    break;
                case Cardinal.West:
                    strCardinal = Constants.WEST;
                    break;
            }
            roundN.Text = ((int)number).ToString() + strCardinal;
        }

        //private void RoundData_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics gdiObj = e.Graphics;
        //    StringFormat strFormat = new StringFormat();

        //    strFormat.FormatFlags = StringFormatFlags.DirectionVertical;
        //    //strFormat.FormatFlags = StringFormatFlags.

        //    System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);


        //    gdiObj.DrawString("kdncl", new Font("Times New Roman", 16F), drawBrush, new PointF(25, 25), strFormat);

        //}

    }
}

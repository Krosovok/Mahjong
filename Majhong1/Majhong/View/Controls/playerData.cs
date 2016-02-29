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
    public partial class PlayerData : UserControl
    {
        private PlayerPosition position;
        private int points = 25000;

        public PlayerData()
        {
            InitializeComponent();
        }

        public PlayerPosition Position
        {
            get { return position; }
            set
            {
                position = value;
                switch (value)
                {
                    case PlayerPosition.East:
                        this.positionLabel.Text = Constants.EAST;
                        break;
                    case PlayerPosition.South:
                        this.positionLabel.Text = Constants.SOUTH;
                        break;
                    case PlayerPosition.West:
                        this.positionLabel.Text = Constants.WEST;
                        break;
                    case PlayerPosition.North:
                        this.positionLabel.Text = Constants.NORTH;
                        break;
                }
            }
        }
        public int Points
        {
            get { return points; }
            set
            {
                points = value;
                this.pointsLabel.Text = points.ToString();
            }
        }
    }
}

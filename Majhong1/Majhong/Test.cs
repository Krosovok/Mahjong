using Mahjong.Model.HandSpace;
using Mahjong.Model.Tiles;
using Mahjong.View.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mahjong
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();


            Hand hand = new Model.HandSpace.Hand();
            //openHandView1.Mode = View.Controls.Mode.ForLeftSide;

            //hand.Draw(new ManTile('1'));
            //hand.Draw(new ManTile('1'));
            //hand.Draw(new ManTile('1'));
            //hand.Draw(new PinTile('1'));
            //hand.Draw(new PinTile('1'));
            //hand.Draw(new PinTile('1'));
            //hand.Draw(new SouTile('1'));
            //hand.Draw(new SouTile('1'));
            //hand.Draw(new SouTile('1'));
            //hand.Draw(new WindTile('N'));
            //hand.Draw(new WindTile('N'));
            //hand.Draw(new WindTile('N'));
            //hand.Draw(new WindTile('W'));

            openHandView1.Hand = hand;
            openHandView1.UpdateHand();
            handView1.Hand = hand;
            handView1.UpdateHand();

            Tile newTile = new WindTile('W');
            openHandView1.DrawTile(newTile);
            handView1.DrawTile(newTile);
            //hand.Draw(newTile);

            handView1.Discard += handView1_Discard;

            //openHandView1.Mode = View.Controls.Mode.Front;
            //openHandView1.UpdateHand();

            //roundData1[Direction.ToRightPlayer] = true;
        }

        void handView1_Discard(Tile tile, bool riichi)
        {
            //openHandView1.Hand = handView1.Hand;
            //openHandView1.UpdateHand();
            //openHandView1.DrawTile(handView1.NewTile);

            handView1.Visible = false;
            openHandView1.Visible = true;
            Thread.Sleep(1000);
            win1.Visible = true;
            win1.WinHand = handView1.Hand;
            //openHandView1.Open();
        }
    }
}

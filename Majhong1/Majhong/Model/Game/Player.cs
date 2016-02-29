using Mahjong.Model.Tiles;
using Mahjong.View.Controls;
using System.Linq;
using System;
using System.Collections.Generic;
using Mahjong.Model.HandSpace;

namespace Mahjong.Model.Game
{
    /// <summary>
    /// Базовый класс для игрока.
    /// </summary>
    abstract class Player
    {
        protected HandView handView;
        protected Hand hand;
        protected OpenTilesView discard;
        //private char roundWind;
        //private char playerWind; 


        public Player()
        {
            Hand = new Hand();
        }

        public bool DeclaredRiichi { get { return Hand.Riichi; } }
        public char roundWind { get; set; }
        public char playerWind { get; set; }

        void hand_SetDeclared(bool isKan)
        {
            OnDeclareSet(isKan);
        }

        public Hand Hand
        {
            get { return hand; }
            set 
            { 
                hand = value;
                hand.SetDeclared += hand_SetDeclared;
            }
        }

        public bool Furiten
        {
            get
            {
                return Discard.Tiles.Any(tile => Hand.IsTempai.Wait.Contains(tile));
            }
        }

        public HandView HandView
        {
            get { return handView; }
            set 
            {
                handView = value;
                handView.Hand = this.hand;
                //handView.UpdateHand();
            }
        }

        public OpenTilesView Discard
        {
            set 
            { 
                discard = value;
            }
            get { return discard; }
        }

        public abstract DeclarationType AnswerMove(AnswerVariants decl, Tile tile, DeclaredFrom takenFrom);

        public abstract void MakeTurn();

        /// <summary>
        /// Игрок берёт тайл из стены.
        /// </summary>
        public virtual void TakeFromWall(Stack<Tile> wall)
        {
            Tile tile = wall.Pop();
            handView.Invoke(
                new Action<Tile>(t => handView.DrawTile(t)), tile);
            Hand.Draw(tile);
            //handView.Invoke(
            //    new Action(() => handView.Enabled = true));
        }

        protected void OnWait()
        {
            if (WaitForPlayer != null)
                WaitForPlayer();
        }

        protected void OnDiscardPhaseEnd()
        {
            if (DiscardPhaseEnd != null)
                DiscardPhaseEnd();
        }

        protected void OnDeclareSet(bool isKan)
        {
            if (DeclareSet != null)
                DeclareSet(this, isKan);
        }

        public delegate void WaitEventHandler();

        public delegate void DeclareSetEventHandler(Player player, bool isKan);

        public event WaitEventHandler WaitForPlayer;

        public event Action DiscardPhaseEnd;

        public event DeclareSetEventHandler DeclareSet;
    }

    struct Move
    {
        public Tile discard;
        public MoveType type;

        public Move(Tile tile, MoveType type)
        {
            discard = tile;
            this.type = type;
        }
    }

    enum MoveType
    {
        Discard,
        Riichi,
        Kan,
        Tsumo
    }
}

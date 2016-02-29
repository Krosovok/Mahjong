using Mahjong.Model.HandSpace;
using Mahjong.Model.Tiles;
using Mahjong.View.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Mahjong.Model.Game
{
    enum Yaku { Yakuhai, Tanyao, Riichi }
    /// <summary>
    /// Отвечает за игру ИИ-игрока.
    /// </summary>
    class AIPlayer : Player
    {
        ////
        //private char roundWind = 'E';
        //private char playerWind = 'E'; 
        ////

        private Yaku Building { get; set; }
        private bool CanOpen { get; set; }

        public AIPlayer()
            : base()
        {
            AnalyzeStartHand();
        }

        public override void MakeTurn()
        {

            // Сделать ход.
            Decide();

            // Ждать ответа.
            OnWait();
        }

        public void Decide()
        {
            Thread.Sleep(1000);
            Move m = new Move(SelectDiscard(), MoveType.Discard);
            if (m.type == MoveType.Discard)
            {
                this.Hand.Discard(m.discard);
                this.HandView.Invoke(new Action(() => this.HandView.UpdateHand()));
                this.Discard.Invoke(new Action<Tile>(t => this.Discard.Put(t)), m.discard);
            }
            OnDiscardPhaseEnd();
        }

        private void AnalyzeStartHand()
        {
            int countSimple = 0;
            if (hand.Categories[4].Contains(new HandForm(GroupValue.Pair, new DragonTile('C')))
                || hand.Categories[4].Contains(new HandForm(GroupValue.Pair, new DragonTile('B')))
                || hand.Categories[4].Contains(new HandForm(GroupValue.Pair, new DragonTile('F'))))
            {
                Building = Yaku.Yakuhai;
                return;
            }
            for (int i = 0; i < hand.Categories.Count(); i++)
            {
                foreach (Group group in hand.Categories[i])
                {
                    foreach (Tile tile in group)
                    {
                        if (tile is SuitedTile && tile.Type != '1' && tile.Type != '9')
                        {
                            countSimple++;
                        }
                    }
                }
            }
            Building = countSimple > 6 ? Yaku.Tanyao : Yaku.Riichi;
        }

        private Tile SelectDiscard()
        {
            for (int i = 0; i < this.Hand.Categories.Count(); i++)
            {
                for (int j = 0; j < this.Hand.Categories[i].Count; j++)
                {
                    if (this.Hand.Categories[i][j].Count == 1 && this.Hand.Categories[i][j].Min.Type == '1')
                    {
                        return this.Hand.Categories[i][j].Min;
                    }
                    if (this.Hand.Categories[i][j].Count == 1 && this.Hand.Categories[i][j].Max.Type == '9')
                    {
                        return this.Hand.Categories[i][j].Max;
                    }
                    if (this.Hand.Categories[i][j].Count == 1 &&
                        i == 3 &&
                        this.Hand.Categories[i][j].Min.Type != playerWind &&
                        this.Hand.Categories[i][j].Min.Type != roundWind)
                    {
                        return this.Hand.Categories[i][j].Min;
                    }
                    if (this.Hand.Categories[i][j].Count == 1)
                    {
                        return this.Hand.Categories[i][j].Min;
                    }
                }
            }
            if (Building == Yaku.Tanyao)
            {
                char[] chars = new char[] { '2', '3', '4', '5', '6', '7', '8' };
                for (int i = 0; i < this.Hand.Categories.Count(); i++)
                {
                    foreach (Tile t in this.Hand.Categories[i])
                    {
                        if (!chars.Contains(t.Type))
                        {
                            return t;
                        }
                    }
                }
            }
            return hand.First();
        }

        public override DeclarationType AnswerMove(AnswerVariants decl, Tile tile, DeclaredFrom takenFrom)
        {
            return DeclarationType.None;
            //if (Building == Yaku.Riichi) { return DeclarationType.None; }
            //if (CanOpen) {}
            //if (Building == Yaku.Yakuhai && (decl.CanPon || decl.CanKan)
            //    && (hand.Categories[4].Contains(new HandForm(GroupValue.Pair, tile))
            //    || hand.Categories[4].Contains(new HandForm(GroupValue.Pon, tile))))
            //{
            //    CanOpen = true;
            //}
        }
    }
}

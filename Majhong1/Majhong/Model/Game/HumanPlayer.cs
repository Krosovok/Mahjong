using Mahjong.Model.Tiles;
using Mahjong.View.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.Game
{
    /// <summary>
    /// Отвечает за взаимодействие человеческого игрока с игрой.
    /// </summary>
    class HumanPlayer : AIPlayer
    {
        //bool done;
        //Move move;

        public HumanPlayer()
        {

        }

        public override void MakeTurn()
        {
            this.HandView.Invoke(new Action(() => HandView.Enabled = true/*.ChoiceNeeded = Choice.Discard*/));
            // Ждём хода.
            OnWait();
            OnDiscardPhaseEnd();
            // Ждём действий остальных.
            OnWait();

            
        }

        public override DeclarationType AnswerMove(AnswerVariants decl, Tile tile, DeclaredFrom takenFrom)
        {
            //return DeclarationType.None;
            return base.AnswerMove(decl, tile, takenFrom);
        }
    }
}

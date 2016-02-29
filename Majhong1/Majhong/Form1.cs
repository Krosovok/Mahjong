using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mahjong.Model.HandSpace;
using Mahjong.Model.Game;
using Mahjong.Model.Tiles;
using Mahjong.Model;
using System.Threading;
using Mahjong.View.Controls;

namespace Mahjong
{
    public partial class Form1 : Form
    {
        Round round;
        AnswerOptions answerOptionsInfo;
        MoveVariants moveVariantsInfo;

        public Form1()
        {
            InitializeComponent();


            round = new Round(
                Cardinal.East,
                Number.First,
                0,
                0,
                new HumanPlayer() { HandView = handViewPlayer, Discard = discardPlayer, playerWind = 'E', roundWind = 'E' },
                new AIPlayer() { HandView = handViewRight, Discard = discardRight, playerWind = 'S', roundWind = 'E' },
                new AIPlayer() { HandView = handViewOpp, Discard = discardOpp, playerWind = 'W', roundWind = 'E' },
                new AIPlayer() { HandView = handViewLeft, Discard = discrardLeft, playerWind = 'N', roundWind = 'E' }
                );

            Ev = new AutoResetEvent(false);
            round.Ev = Ev;
            round.Contiunue += this.ContinueTheGame;

            round.NeedAnswer += ProposeAnswerMove;
            round.GameEnd += round_GameEnd;
            round.GetHumanPlayer().Hand.NewTileDrawn += InvokeProposeMove;
            handViewPlayer.Chi += ChosenChi;
            handViewPlayer.ClosedKan += ClosedKan;

            leftRiichi.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            rightRiichi.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

            //round.Players

            //
            //playerOpenTiles.Declare(DeclarationType.Pon, DeclaredFrom.OppositePlayer, new WindTile('N'), new WindTile('N'), new WindTile('N')); //, new WindTile('N')
            //playerOpenTiles.Declare(DeclarationType.ClosedKan, DeclaredFrom.OppositePlayer, new DragonTile('C'), new DragonTile('C'), new DragonTile('C'), new DragonTile('C'));
            //playerOpenTiles.Declare(DeclarationType.Kan, DeclaredFrom.OppositePlayer, new WindTile('N'));
            //
        }

        void round_GameEnd()
        {
            MessageBox.Show("Game ended.");

            //throw new NotImplementedException();
            //Show some results.
        }


        /// <summary>
        /// Уведомляет поток игры о том, что игру можно продолжить. 
        /// Вызвыать Set всякий раз, как все действия со стороны игроков закончены.
        /// </summary>
        public AutoResetEvent Ev{ get; set; }
        /// <summary>
        /// Поток, в котором идёт игровой круг.
        /// </summary>
        private Thread t { get; set; }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            t = new Thread(round.Start);
            t.Start();
        }

        /// <summary>
        /// Делает активными элементы управления, которые отвечают за возможные объявления.
        /// Отвечает за ответные объявления: Чи, Пон, Кан, Рон.
        /// </summary>
        /// <param name="variants">Доступные объявления, записанные в стркутуре DeclarationVariants.</param>
        /// <param name="tile">Тайл, с которого можно сделать объявление.</param>
        /// <param name="dir">Направления дискарада игрока, с</param>
        /// <returns>true, если игроку предлагаются какие-либо действия. Иначе false.</returns>
        private bool ProposeAnswerMove(AnswerVariants variants, Tile tile, DeclaredFrom from)
        {
            answerOptionsInfo = new AnswerOptions(variants, tile, from);

            if (!variants.CanPas)
            {
                Pas(pasButton, new EventArgs());
                return false;
            }

            kanButton.Enabled = variants.CanKan;
            ponButton.Enabled = variants.CanPon;
            chiButton.Enabled = variants.CanChi != ChiType.None;
            pasButton.Enabled = variants.CanPas;
            ronButton.Enabled = variants.CanRon;

            autoButton.Enabled = true;

            return true;
        }

        private void InvokeProposeMove(MoveVariants moveVar)
        {
            this.Invoke(new Action<MoveVariants>(ProposeMove), moveVar);
        }

        private void ProposeMove(MoveVariants moveVar)
        {
            moveVariantsInfo = moveVar;

            HumanPlayer human = round.GetHumanPlayer();
            bool riichi = human.DeclaredRiichi;

            riichiButton.Enabled = moveVar.CanRiichi.Count != 0 && !riichi;
            kanButton.Enabled = moveVar.CanClosedKan.Count != 0;
            tsumoButton.Enabled = moveVar.CanTsumo;

            autoButton.Enabled = true;

            if (riichi)
            {
                handViewPlayer.ProposeRiichiDiscardVariants();
            }

            //if (riichi && !(kanButton.Enabled || tsumoButton.Enabled)) // Автодискард после риичи.
            //    handViewPlayer.AutoRiichiDiscard();
                //DiscardOrRiichi(human.Hand.LastTile, false);
        }

        private void Chi(object sender, EventArgs e)
        {
            ChiType type = answerOptionsInfo.Variants.CanChi;
            int intType = (int)type;
            bool onlyOne = intType != 0 && (intType & (intType - 1)) == 0;

            if (onlyOne) // Сделать этот самый один.
            {
                ChosenChi(type);
            }
            else // Дальше ждать решения.
            {
                handViewPlayer.ProposeChiVariants(answerOptionsInfo);
            }
        }

        private void ChosenChi(ChiType type)
        {
            int intType = (int)type;
            bool onlyOne = intType != 0 && (intType & (intType - 1)) == 0;

            if (!onlyOne)
                throw new Exceptions.InvalidDeclarationException("Чи не выбрано.");

            Player human = round.GetHumanPlayer();
            List<Tile> declared =
                human.Hand.DeclareChi(
                    answerOptionsInfo.Tile as SuitedTile,
                    type);

            playerOpenTiles.Declare(DeclarationType.Chi, answerOptionsInfo.From, declared.ToArray());
            human.HandView.UpdateHandAfterDeclaration();
            GetSourceDiscard().StealDiscarded();

            ContinueTheGame();
        }

        private void Pon(object sender, EventArgs e)
        {
            Player human = round.GetHumanPlayer();
            List<Tile> declared =
            human.Hand.DeclarePon(answerOptionsInfo.Tile);

            playerOpenTiles.Declare(DeclarationType.Pon, answerOptionsInfo.From, declared.ToArray());
            human.HandView.UpdateHandAfterDeclaration();
            GetSourceDiscard().StealDiscarded();

            ContinueTheGame();
        }

        private void Kan(object sender, EventArgs e)
        {
            Player human = round.GetHumanPlayer();
            if (pasButton.Enabled) // Если можно пропасовать - значит это объявление открытого Кана. Иначе - закрытого.
            {
                List<Tile> declared =
                human.Hand.DeclareKan(answerOptionsInfo.Tile);
                //
                playerOpenTiles.Declare(DeclarationType.Kan, answerOptionsInfo.From, declared.ToArray());
                human.HandView.UpdateHandAfterDeclaration();
                GetSourceDiscard().StealDiscarded();
                //
                ContinueTheGame();
            }
            else
            {
                switch (moveVariantsInfo.CanClosedKan.Count)
                {
                    case 0:
                        throw new Exceptions.InvalidDeclarationException("Попытка объявить закрытый Кан без возможности объявить таковой.");
                    case 1:
                        ClosedKan(moveVariantsInfo.CanClosedKan[0]);
                        return;
                    default:
                        human.HandView.ProposeClosedKanVariants(moveVariantsInfo.CanClosedKan);
                        break;
                }
            }
        }

        private void Riichi(object sender, EventArgs e)
        {
            handViewPlayer.ProposeRiichiVariants(moveVariantsInfo.CanRiichi);
        }

        private void Pas(object sender, EventArgs e)
        {
            round.AIAnswer(answerOptionsInfo.Tile, answerOptionsInfo.Source);
            GetSourceDiscard().PasPlayerDeclaration();
        }

        private void Ron(object sender, EventArgs e)
        {
            //OpenHand();
            round.EndGame();

            //throw new NotImplementedException();
        }

        private void Tsumo(object sender, EventArgs e)
        {
            round.EndGame();
        }

        private void Auto(object sender, EventArgs e)
        {
            HumanPlayer human = round.GetHumanPlayer();
            if (pasButton.Enabled) // Если можно пропасовать - значит это объявление. Иначе - свой ход.
            {
                DeclarationType decl = human.AnswerMove(answerOptionsInfo.Variants, answerOptionsInfo.Tile, answerOptionsInfo.From);
                if (decl == DeclarationType.None)
                {
                    GetSourceDiscard().PasPlayerDeclaration();
                }
                ContinueTheGame();
            }
            else
            {
                human.Decide();
                ContinueTheGame();
            }
        }

        /// <summary>
        /// Объявляет закрытый Кан из данного тайла.
        /// </summary>
        /// <param name="tile"></param>
        private void ClosedKan(Tile tile)
        {
            Player human = round.GetHumanPlayer();

            List<Tile> declared = human.Hand.DeclareClosedKan(tile);
            playerOpenTiles.Declare(DeclarationType.ClosedKan, DeclaredFrom.OppositePlayer/*none*/, declared.ToArray());
            // Объявление Кана вызовет взятие с мёртвой стены. Так что ишра продолжиться только тогда.

            /*
                List<Tile> declared =
                human.Hand.DeclareKan(answerOptionsInfo.Tile);
                //
                playerOpenTiles.Declare(DeclarationType.Kan, answerOptionsInfo.From, declared.ToArray());
                human.HandView.UpdateHandAfterDeclaration();
                GetSourceDiscard().StealDiscarded();
                //
                ContinueTheGame();
             */
        }


        /// <summary>
        /// Обработчик сброса игрока. Продолжает игру после сброса.
        /// </summary>
        /// <param name="tile">Сброшенный тайл.</param>
        private void DiscardOrRiichi(Tile tile, bool riichi)
        {
            Player human = round.GetHumanPlayer();
            human.Hand.Discard(tile);
            human.HandView.UpdateHand();
            human.Discard.Put(tile, riichi);

            if (riichi)
            {
                playerRiichi.Visible = true; // <-- Палочка.
                human.Hand.Riichi = true;
                // Ещё???

                //throw new NotImplementedException();
            }

            ContinueTheGame();
        }

        private void OpenHand()
        {
            throw new NotImplementedException();
        }

        private void ContinueTheGame()
        {
            handViewPlayer.Enabled = false;
            kanButton.Enabled = false;
            ponButton.Enabled = false;
            chiButton.Enabled = false;
            pasButton.Enabled = false;
            autoButton.Enabled = false;
            riichiButton.Enabled = false;
            tsumoButton.Enabled = false;

            Ev.Set();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Abort();
        }

        private void button_EnabledChanged(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b.Enabled)
                b.BackColor = Color.Khaki;
            else
                b.BackColor = Color.Transparent;
        }

        private OpenTilesView GetSourceDiscard()
        {
            return this.Controls.OfType<OpenTilesView>().
                Where(ctrl => ctrl.IsDiscard && ctrl.Direction == answerOptionsInfo.Source).First();
        }

    }

    public struct AnswerOptions
    {
        AnswerVariants variants;
        Tile tile;
        DeclaredFrom from;

        public AnswerOptions(AnswerVariants var, Tile t, DeclaredFrom fr)
        {
            variants = var;
            tile = t;
            from = fr;
        }

        public AnswerVariants Variants { get { return variants; } }
        public Tile Tile { get { return tile; } }
        public DeclaredFrom From { get { return from; } }
        public Direction Source { get { return (Direction)(((int)this.From + 1) * 90); } }
    }
}

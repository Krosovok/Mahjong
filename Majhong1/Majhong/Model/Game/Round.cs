using Mahjong.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahjong.Model.HandSpace;
using System.Threading;
using Mahjong.View.Controls;
using Mahjong.Exceptions;

namespace Mahjong.Model.Game
{
    public enum Cardinal { East, South, West };
    public enum Number { First = 1, Second = 2, Third = 3, Fourth = 4 };
    public enum PlayerPosition { East, South, West, North };

    /// <summary>
    /// Представляет раунд.
    /// </summary>
    class Round
    {
        //сторона света, номер раздачи, количество ренчанов и палочек риичи
        Cardinal cardinal;
        Number number;
        int renchan;
        int riichStick;

        //игроки
        Player[] players;
        PlayerPosition current;

        //дискарды
        List<Tile>[] discrads;

        //стены
        Stack<Tile> wall;
        Stack<Tile> deadWall;

        bool currentChangedByDeclaration;
        bool endGame;

        /// <summary>
        /// Конструктор для раздачи.
        /// </summary>
        /// <param name="card">сторона света</param>
        /// <param name="numb">номер раздачи</param>
        /// <param name="rench">номер ренчана</param>
        /// <param name="rStick">количество палочек риичи</param>
        /// <param name="east">восточный игрок</param>
        /// <param name="south">южный игрок</param>
        /// <param name="west">западный игрок</param>
        /// <param name="north">северный игрок</param>
        public Round(Cardinal card, Number numb, int rench, int rStick, /*Human*/Player east,
            Player south, Player west, Player north)
        {
            cardinal = card;
            number = numb;
            renchan = rench;
            riichStick = rStick;
            players = new Player[4] { east, south, west, north };
            //players[(int)PlayerPosition.East] = east;
            //players[(int)PlayerPosition.South] = south;
            //players[(int)PlayerPosition.West] = west;
            //players[(int)PlayerPosition.North] = north;

            discrads = new List<Tile>[4]. // Need???
                Select(list => /*list = */new List<Tile>()).ToArray();
            //discrads[(int)PlayerPosition.East] = new List<Tile>();
            //discrads[(int)PlayerPosition.South] = new List<Tile>();
            //discrads[(int)PlayerPosition.West] = new List<Tile>();
            //discrads[(int)PlayerPosition.North] = new List<Tile>();

            wall = new Stack<Tile>();
            deadWall = new Stack<Tile>();
            GenerateWall();
            GenerateDeadWall();

            foreach (Player player in players)
            {
                GenerateHand(player);
                player.HandView.UpdateHand();
                player.WaitForPlayer += Wait;
                player.DiscardPhaseEnd += TurnMade;
                player.Discard.PutEvent += PutDiscard;
                player.DeclareSet += DeclareSet;
            }
        }

        private void DeclareSet(Player sender, bool isKan)
        {
            Current = (PlayerPosition)Array.IndexOf(players, sender);
            if (isKan)
            {
                ///???
                sender.TakeFromWall(deadWall);
            }
        }

        public PlayerPosition Current 
        { 
            set 
            { 
                current = value;
                currentChangedByDeclaration = true;
            }
            get { return current; }
        }

        public Player[] Players
        {
            get { return players; }
            set { players = value; }
        }
        /// <summary>
        /// Уведомляет поток игры о том, что игру можно продолжить. 
        /// </summary>
        public AutoResetEvent Ev { get; set; }

        /// <summary>
        /// Запускает игру.
        /// </summary>
        public void Start()
        {
            current = PlayerPosition.East;
            while (wall.Count != 0)
            {
                if (!currentChangedByDeclaration)
                    players[(int)current].TakeFromWall(wall);

                players[(int)current].MakeTurn();

                if (endGame)
                    break;

                if (!currentChangedByDeclaration)
                    ChangeCurrent();

            }

            OnGameEnd();
        }

        public void EndGame()
        {
            endGame = true;
        }

        public HumanPlayer GetHumanPlayer()
        {
            foreach (Player player in players)
            {
                if (player is HumanPlayer)
                    return player as HumanPlayer;
            }
            throw new MachineRebellionException();
        }

        /// <summary>
        /// Обработчик события сброса. Даёт игроку возможность объявления, почле чего даёт её компьютерным игрокам.
        /// </summary>
        /// <param name="tile">Сброшенный тайл.</param>
        /// <param name="dir">Направление, с которого был сброшен тайл.</param>
        /// <returns>true, если игроку предлагаются какие-либо действия. Иначе false.</returns>
        private bool PutDiscard(Tile tile, Direction dir)
        {
            HumanPlayer humanPlayer = GetHumanPlayer();
            AnswerVariants variants = new AnswerVariants();
            if (dir != Direction.ToPlayer)
            {
                variants.CanKan = humanPlayer.Hand.CheckKan(tile);
                variants.CanPon = humanPlayer.Hand.CheckPon(tile);
                variants.CanRon = humanPlayer.Hand.CheckRon(tile);
                if (dir == Direction.ToLeftPlayer)
                {
                    variants.CanChi = humanPlayer.Hand.CheckChi(tile as SuitedTile);
                }
                DeclaredFrom from = ConvertTaken(0, (int)dir / 90);
                return OnNeedAnswer(variants, tile, from);
            }
            else
            {
                AIAnswer(tile, dir);
                return false; 
            }
        }


        /// <summary>
        /// Даёт возможность компьютерным игрокам сделать объявление. 
        /// </summary>
        /// <param name="tile">Сброшенный тайл.</param>
        /// <param name="dir">Направление, с которого был сброшен тайл.</param>
        public void AIAnswer(Tile tile, Direction dir)
        {
            
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] is HumanPlayer) { continue; }

                AnswerVariants decl = new AnswerVariants();
                int j;

                if (i != (int)dir / 90)
                {
                    decl.CanKan = players[i].Hand.CheckKan(tile);
                    decl.CanPon = players[i].Hand.CheckPon(tile);
                    decl.CanRon = players[i].Hand.CheckRon(tile);
                    if ((i == (int)dir / 90 + 1 || (int)dir / 90 == 3 && i == 0) && Int32.TryParse(tile.Type.ToString(), out j))
                    {
                        decl.CanChi = players[i].Hand.CheckChi(tile as SuitedTile);
                    }
                    DeclaredFrom takenFrom = ConvertTaken(i, (int)dir / 90);
                    DeclarationType type = players[i].AnswerMove(decl, tile, takenFrom);
                    if (type != DeclarationType.None)
                    {
                        OnContinue();
                        break;
                    }
                }
            }
            OnContinue();
        }

        private void TurnMade()
        {
            currentChangedByDeclaration = false;
        }

        private DeclaredFrom ConvertTaken(int rel, int discarded)
        {
            
            switch(rel)
            {
                case 0:
                    return (DeclaredFrom)discarded - rel - 1;
                case 1:
                    return (DeclaredFrom)Math.Abs(discarded - rel - 1);
                case 2:
                    return discarded - rel - 1 != -3 ? (DeclaredFrom)Math.Abs(discarded - rel - 1) : (DeclaredFrom)1;
                case 3:
                    int j = discarded - rel - 1;
                    return j != -3 && j != -4 ? (DeclaredFrom)Math.Abs(discarded - rel - 1) : j == -3 ? (DeclaredFrom)1 : (DeclaredFrom)0;
            }
            return (DeclaredFrom)0;
        }

        private void GenerateHand(Player /*eastP*/player)
        {
            //Hand h = new Hand();
            for (int i = 0; i < 13; i++)
            {
                player.Hand/*h*/.Draw(wall.Pop());
            }
            ///*eastP**/player.Hand = h;
        }

        /// <summary>
        /// Делает активным следующего игрока.
        /// </summary>
        private void ChangeCurrent()
        {
            switch (current)
            {
                case PlayerPosition.East:
                    current = PlayerPosition.South;
                    break;
                case PlayerPosition.South:
                    current = PlayerPosition.West;
                    break;
                case PlayerPosition.West:
                    current = PlayerPosition.North;
                    break;
                case PlayerPosition.North:
                    current = PlayerPosition.East;
                    break;
            }
        }

        private void GenerateDeadWall()
        {
            for (int i = 0; i < 14; i++)
            {
                deadWall.Push(wall.Pop());
            }
        }

        private void GenerateWall()
        {
            Random r = new Random();
            List<Tile> tiles = TileFactory.Tiles();
            for (int i = tiles.Count - 1; i >= 0; i--)
            {
                int idx = r.Next(i);
                wall.Push(tiles[idx]);
                tiles.RemoveAt(idx);
            }
        }

        /// <summary>
        /// Ждёт отклика от игрока-человка.
        /// </summary>
        private void Wait()
        {
            Ev.WaitOne();
        }

        //publicprivate void OnChi(Tile tile)
        //{
        //    if (Chi != null)
        //    {
        //        Chi(tile);
        //    }
        //}

        //private void OnPon(Tile tile)
        //{
        //    if (Pon != null)
        //    {
        //        Pon(tile);
        //    }
        //}

        //private void OnKan(Tile tile)
        //{
        //    if (Kan != null)
        //    {
        //        Kan(tile);
        //    }
        //}

        private bool OnNeedAnswer(AnswerVariants variants, Tile tile, DeclaredFrom from)
        {
            if (NeedAnswer != null)
            {
                return NeedAnswer(variants, tile, from);
            }
            return false;
        }

        private void OnContinue()
        {
            if (this.Contiunue != null)
                Contiunue();
        }

        private void OnGameEnd()
        {
            if (GameEnd != null)
                GameEnd();
        }


        //public delegate void DeclarationEventHandler(Tile tile);

        //public event DeclarationEventHandler Chi;
        //public event DeclarationEventHandler Pon;
        //public event DeclarationEventHandler Kan;

        /// <summary>
        /// Возникает, когда появляется необходимость получить ответ игрока-человека.
        /// </summary>
        public event NeedAnswerEventHandler NeedAnswer;

        /// <summary>
        /// Возникает, когда появляется необходимость продолжить игру.
        /// </summary>
        public event Action Contiunue;

        public event Action GameEnd; 

        public delegate bool NeedAnswerEventHandler(
            AnswerVariants variants, Tile tile, DeclaredFrom from);
    }
    
    /// <summary>
    /// Содержит информацию о возможности игрока сделать ответные объявления: Кан, Пон, Чи, Рон.
    /// </summary>
    public struct AnswerVariants
    {
        private bool canKan;
        private bool canPon;
        private ChiType canChi;
        private bool canRon;

        public AnswerVariants(bool canKan, bool canPon, ChiType canChi, bool canRon){
            this.canKan = canKan;
            this.canPon = canPon;
            this.canChi = canChi;
            this.canRon = canRon;
        }

        public bool CanKan 
        {
            set { canKan = value; }
            get { return canKan; }
        }
        public bool CanPon 
        {
            set { canPon = value; }
            get { return canPon; }
        }
        public ChiType CanChi 
        {
            set { canChi = value; }
            get { return canChi; }
        }
        public bool CanRon
        {
            set { canRon = value; }
            get { return canRon; }
        }
        public bool CanPas { get { return CanPon || CanKan || CanChi != ChiType.None || CanRon; } }
    }
}

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
using Mahjong.Model.HandSpace;

namespace Mahjong.View.Controls
{
    enum Choice { None, Discard, Chi, Riichi, ClosedKan/*???*/ }

    /// <summary>
    /// Элемент управления, способный показать руку игрока или закрытую руку оппонента.
    /// </summary>
    public partial class HandView : UserControl
    {
        protected Mode mode = Mode.Front;
        AnswerOptions chiInfo;
        Choice choiceNeeded;

        public HandView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Рука для отображения.
        /// </summary>
        public Hand Hand { set; get; }
        /// <summary>
        /// Новый тайл в руке.
        /// </summary>
        public Tile NewTile { get { return newTile.Tile; } }
        /// <summary>
        /// Получает или задаёт то, как будут отображаться все тайлы в руке.
        /// </summary>
        public virtual Mode Mode
        {
            get { return mode; }
            set 
            {
                Bitmap btm;
                this.mode = value;
                List<TileBox> tileBoxes = this.Controls.OfType<TileBox>().ToList(); // Тайлы идут задом напрерёд?
                tileBoxes.ForEach(tileBox => tileBox.Mode = value);

                Action<List<TileBox>, int> changeLocation;
                switch (mode)
                {
                    case Mode.Back: // Расставляем тайлы с начала.
                        changeLocation =
                            (tiles, idx) => tiles[idx].Location =
                                new Point(
                                    idx * Constants.TILE_WIDTH + (newTile.Margin.All - tiles[idx].Margin.All),
                                    0);
                        break;

                    case Mode.Front: // Расставляем тайлы с конца. 
                        changeLocation =
                            (tiles, idx) => tiles[idx].Location =
                                new Point(
                                    (tiles.Count - idx - 1) * Constants.TILE_WIDTH + tiles[idx].Margin.All,
                                    //0);
                                    Constants.TILE_JUMPING);
                        break;

                    case Mode.ForRightSide:
                        changeLocation =
                            (tiles, idx) => tiles[idx].Location =
                                new Point(0,
                                    idx * Constants.TILE_TOP_HEIGHT + (newTile.Margin.All - tiles[idx].Margin.All)
                                    );
                        btm = (Bitmap)Resources.ResourceManager.GetObject(Constants.RIGHT);
                        this.sideForNewTile.Image = btm;
                        this.sideForHand.Image = btm;
                        break;

                    case Mode.ForLeftSide:
                        changeLocation =
                            (tiles, idx) => tiles[idx].Location =
                                new Point(0,
                                    (tiles.Count - idx - 1) * Constants.TILE_TOP_HEIGHT + tiles[idx].Margin.All
                                    );
                        btm = (Bitmap)Resources.ResourceManager.GetObject(Constants.LEFT);
                        this.sideForNewTile.Image = btm;
                        this.sideForHand.Image = btm;
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
                    LocateSideParts();//
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
        /// <summary>
        /// Получает или задаёт Выбор, который необходимо сделать игроку.
        /// Вместе с тем делает элемент управления доступным или недоступным.
        /// </summary>
        private Choice ChoiceNeeded
        {
            get { return choiceNeeded; }
            set
            {
                choiceNeeded = value;

                this.Enabled = !(choiceNeeded == Choice.None);
            }
        }

        /// <summary>
        /// Отображает тайлы на руке.
        /// </summary>
        public void UpdateHand() 
        {
            List<TileBox> tileBoxes = Controls.OfType<TileBox>().Reverse().ToList();

            tileBoxes.ForEach(tileBox => tileBox.Tile = null);

            int i = 0;
            foreach (Tile tile in Hand/*.GetEnumerator()*/)
            {
                if (i != Constants.MAX_HAND_SIZE - 1)
                    tileBoxes[i].Tile = tile;
                i++;
            }

            LocateNewTile(i);
        }

        /// <summary>
        /// Показывает руку после объявления с последним тайлом немного в стороне, как новый.
        /// Намекает пользователю, что нужно сделать снос.
        /// </summary>
        public void UpdateHandAfterDeclaration()
        {
            List<TileBox> tileBoxes = Controls.OfType<TileBox>().Reverse().ToList();

            tileBoxes.ForEach(tileBox => tileBox.Tile = null);

            int i = 0;
            foreach (Tile tile in Hand/*.GetEnumerator()*/)
            {
                if (i != Constants.MAX_HAND_SIZE - 1 &&
                    i != Hand.Count() - 1)//
                    tileBoxes[i].Tile = tile;
                i++;
            }

            DrawTile(Hand.Last());
            LocateNewTile(i - 1);
        }

        public void AutoRiichiDiscard()
        {
            tile_Click(newTile, new EventArgs());
        }

        //public void Open()
        //{
        //    List<TileBox> tileBoxes = this.Controls.OfType<TileBox>().ToList();
        //    Action<TileBox> setDirection = box => { };

        //    switch (this.Mode)
        //    {
        //        case Mode.Front:
        //            setDirection = box => box.Direction = Direction.ToPlayer;
        //            break;
        //        case Mode.Back:
        //            setDirection = box => box.Direction = Direction.ToOppositePlayer;
        //            break;
        //        case Mode.ForLeftSide:
        //            setDirection = box => box.Direction = Direction.ToLeftPlayer;
        //            break;
        //        case Mode.ForRightSide:
        //            setDirection = box => box.Direction = Direction.ToRightPlayer;
        //            break;
        //    }

        //    tileBoxes.ForEach(box => box.Mode = Mode.Face);
        //    tileBoxes.ForEach(setDirection);
        //}

        /// <summary>
        /// Визуализирует взятый со стены тайл. 
        /// Применять до добавления тайла в руку.
        /// </summary>
        public void DrawTile(Tile tile)
        {
            this.newTile.Tile = tile;

            LocateSideParts();
            LocateNewTile(Hand.Count());//
            if (this.Controls.OfType<PictureBox>().Where(box => box.Visible).Count() != 0)
                this.Size = new Size(
                    //tileBoxes.Where(box => box.Tile != null).Max(box => box.Location.X + box.Size.Width),
                    //tileBoxes.Where(box => box.Tile!= null).Max(box => box.Location.Y + box.Size.Height)
                this.Controls.OfType<PictureBox>().Where(box => box.Visible).Max(box => box.Location.X + box.Size.Width),
                this.Controls.OfType<PictureBox>().Where(box => box.Visible).Max(box => box.Location.Y + box.Size.Height) //+ Constants.TILE_JUMPING
                );

            this.newTile.Visible = true;

            //if (!this.Hand.Riichi)
                this.ChoiceNeeded = Choice.Discard;
        }

        /// <summary>
        /// Предлагает выбрать пользователю на руке тайлы из данного списка.
        /// Его выбор будет рассмотрен как выбор тайлов для объявления Чи.
        /// </summary>
        /// <param name="declaration">Информация о объявлении, с которого объявляется Чи.</param>
        public void ProposeChiVariants(AnswerOptions declaration)
        {
            ChiType chi = declaration.Variants.CanChi;
            Tile tile = declaration.Tile;

            if (this.Mode != Mode.Front)
                throw new Exceptions.MachineRebellionException("Попытка подсветить выбор в руке компьютерного игрока.");

            if (chi == ChiType.None)
                throw new Exceptions.InvalidDeclarationException("Попытка показать варианты чи, когда чи нет.");
            List<TileBox> tilesOfSuit = this.Controls.OfType<TileBox>().Where(t => t.Tile != null && t.Tile.Category() == tile.Category()).ToList();

            this.Controls.OfType<TileBox>().ToList().ForEach(box => box.Unavailible = true);
            
            if ((chi & ChiType.Left) == ChiType.Left)
            {
                tilesOfSuit.ForEach(box => box.Unavailible =
                    (tile.CompareTo(box.Tile) == 1 || tile.CompareTo(box.Tile) == 2) ?
                    false : box.Unavailible);
            }
            if ((chi & ChiType.Middle) == ChiType.Middle)
            {
                tilesOfSuit.ForEach(box => box.Unavailible =
                    (tile.CompareTo(box.Tile) == 1 || tile.CompareTo(box.Tile) == -1) ?
                    false : box.Unavailible);
            }
            if ((chi & ChiType.Right) == ChiType.Right)
            {
                tilesOfSuit.ForEach(box => box.Unavailible =
                    (tile.CompareTo(box.Tile) == -1 || tile.CompareTo(box.Tile) == -2) ?
                    false : box.Unavailible);
            }

            this.ChoiceNeeded = Choice.Chi;
            this.chiInfo = declaration;
        }

        /// <summary>
        /// Предлагает выбрать пользователю на руке тайлы из данного списка.
        /// Его выбор будет рассмотрен как выбор сброса для объявления Риичи.
        /// </summary>
        /// <param name="availibleChoices">Доступные для выбора тайлы.</param>
        public void ProposeRiichiVariants(List<Tile> availibleChoices)
        {
            ProposeVariants(availibleChoices);
            ChoiceNeeded = Choice.Riichi;
        }

        /// <summary>
        /// Предлагает выбрать пользователю на руке тайлы из данного списка.
        /// Его выбор будет рассмотрен как выбор тайла для объявления закрытого Кана.
        /// </summary>
        /// <param name="availibleChoices">Доступные для выбора тайлы.</param>
        public void ProposeClosedKanVariants(List<Tile> availibleChoices)
        {
            ProposeVariants(availibleChoices);
            ChoiceNeeded = Choice.ClosedKan;
        }

        public void ProposeRiichiDiscardVariants()
        {
            Controls.OfType<TileBox>().ToList().ForEach(box => box.Unavailible = true);
            newTile.Unavailible = false;
        }

        /// <summary>
        /// Предлагает выбрать пользователю на руке тайлы из данного списка.
        /// </summary>
        /// <param name="availibleChoices">Доступные для выбора тайлы.</param>
        private void ProposeVariants(List<Tile> availibleChoices)
        {
            if (this.Mode != Mode.Front)
                throw new Exceptions.MachineRebellionException("Попытка подсветить выбор в руке компьютерного игрока.");

            if (availibleChoices.Count == 0)
                throw new Exceptions.InvalidDeclarationException("Нельзя предложить 0 вариантов.");

            this.Controls.OfType<TileBox>().ToList().ForEach(
                box => box.Unavailible = box.Visible &&
                    !availibleChoices.Any(choice => 
                        choice.Category() == box.Tile.Category() &&
                        choice.CompareTo(box.Tile) == 0));
        }

        protected virtual void LocateNewTile(int i)
        {
            switch (mode)
            {
                case Mode.Back:
                    newTile.Location = new Point(
                        (Constants.MAX_HAND_SIZE - i - 1) * Constants.TILE_WIDTH,
                        0);
                    break;
                case Mode.Front:
                    newTile.Location = new Point(
                        (i) * Constants.TILE_WIDTH + newTile.Margin.Left,
                        //0);
                        Constants.TILE_JUMPING);
                        //this.Controls.OfType<TileBox>().Min(box => box.Location.Y));
                    break;

                case Mode.ForRightSide:
                    newTile.Location = new Point(0,
                        (Constants.MAX_HAND_SIZE - i - 1) * Constants.TILE_TOP_HEIGHT
                        );
                    break;
                case Mode.ForLeftSide:
                    newTile.Location = new Point(0,
                        (i) * Constants.TILE_TOP_HEIGHT + newTile.Margin.Top
                        );
                    break;
            }
            LocateSideParts();
        }

        protected virtual void LocateSideParts()
        {
            bool show = (Mode == Mode.ForLeftSide || Mode == Mode.ForRightSide);
            if (show)
            {
                sideForHand.Location = new Point(0,
                    this.Controls.OfType<TileBox>().Where(c => c.Tile != null && !Object.ReferenceEquals(c, newTile))
                    .Max(box => box.Location.Y + box.Size.Height));
                sideForNewTile.Location = new Point(0,
                    newTile.Location.Y + newTile.Size.Height);
            }
            sideForHand.Visible = show;
            sideForNewTile.Visible = show && newTile.Visible;
        }

        

        private void tile_Click(object sender, EventArgs e)
        {
            Tile selected = (sender as TileBox).Tile;

            switch (ChoiceNeeded)
            {
                case Choice.Discard:
                    this.ChoiceNeeded = Choice.None;
                    OnDiscard(selected, false); 
                    break;
                case Choice.Chi:
                    this.ChoiceNeeded = Choice.None; 
                    OnChi( ChiSelect(selected) );
                    break;
                case Choice.Riichi:
                    this.ChoiceNeeded = Choice.None;
                    OnDiscard(selected, true);
                    break;
                case Choice.ClosedKan:
                    this.ChoiceNeeded = Choice.None;
                    OnClosedKan(selected);
                    break;
            }

            this.Controls.OfType<TileBox>().ToList().ForEach(box => box.Unavailible = false);
            //this.ChoiceNeeded = Choice.None; //this.Enabled = false;
        }

        private ChiType ChiSelect(Tile selected)
        {
            switch (chiInfo.Tile.CompareTo(selected))
            {
                case 2:
                    return ChiType.Left;
                case -2:
                    return ChiType.Right;
                case 1:
                    return (chiInfo.Variants.CanChi & ChiType.Middle) == ChiType.Middle ?
                        ChiType.Middle : ChiType.Right;
                case -1:
                    return (chiInfo.Variants.CanChi & ChiType.Middle) == ChiType.Middle ?
                        ChiType.Middle : ChiType.Left;
                default:
                    throw new Exceptions.InvalidDeclarationException();
            }
        }

        private void newTile_MouseEnter(object sender, EventArgs e)
        {
            (sender as TileBox).Jumping = true;
        }

        private void newTile_MouseLeave(object sender, EventArgs e)
        {
            (sender as TileBox).Jumping = false;
        }

        private void HandView_EnabledChanged(object sender, EventArgs e)
        {
            //if (this.Enabled == true)
            //{
                foreach (TileBox box in this.Controls.OfType<TileBox>())
                {
                    box.Enabled = !box.Unavailible && this.Enabled;
                }
            //}
        }

        private void OnDiscard(Tile tile, bool riichi)
        {
            if (Discard != null)
                Discard(tile, riichi);
        }

        private void OnChi(ChiType type)
        {
            if (Chi != null)
                Chi(type);
        }

        private void OnClosedKan(Tile tile)
        {
            if (ClosedKan != null)
                ClosedKan(tile);
        }

        public delegate void DiscardEventHandler(Tile tile, bool riichi);

        public delegate void ChiEventHandler(ChiType type);

        public delegate void ClosedKanEventHandler(Tile tile);

        /// <summary>
        /// Происходит при сбросе тайла.
        /// В обработчике стоит удалить тайл из руки, обновить её и добавить тайл в дискард.
        /// Сигнал для продолжения игры.
        /// </summary>
        public event DiscardEventHandler Discard;

        /// <summary>
        /// Происходит при выборе Чи.
        /// В обработчике стоит объявить выбранное Чи.
        /// Сигнал для продолжения игры.
        /// </summary>
        public event ChiEventHandler Chi;

        /// <summary>
        /// Происходит при выборе тайла для закрытого Кана.
        /// В обработчике следует объявить выбранный Кан.
        /// Сигнал для взятия тайла замены.
        /// </summary>
        public event ClosedKanEventHandler ClosedKan;
    }

    /// <summary>
    /// Содержит информацию о возможности игрока сделать объявления на своём ходу: Цумо, Риичи, закрытый Кан.
    /// </summary>
    public struct MoveVariants
    {
        private bool canTsumo;
        private List<Tile> canRiichi;
        private List<Tile> canClosedKan;

        public MoveVariants(bool tsumo, List<Tile> riichi, List<Tile> closedKan)
        {
            canTsumo = tsumo;
            canRiichi = riichi;
            canClosedKan = closedKan;
        }

        public bool CanTsumo
        {
            get { return canTsumo; }
            set { canTsumo = value; }
        }
        public List<Tile> CanRiichi
        {
            get { return canRiichi; }
            set { canRiichi = value; }
        }
        public List<Tile> CanClosedKan
        {
            get { return canClosedKan; }
            set { canClosedKan = value; }
        }
    }
}

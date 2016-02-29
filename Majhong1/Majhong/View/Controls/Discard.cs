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

namespace Mahjong.View.Controls
{
    public enum DeclarationType
    {
        None,
        Chi,
        Pon,
        Kan,
        ClosedKan
    }

    public enum DeclaredFrom
    {
        RightPlayer = 0,
        OppositePlayer = 1,
        LeftPlayer = 2
    }

    /// <summary>
    /// Элемент отобрадения для отображения сброшенных игроком тайлов.
    /// Так же может использоваться для отображения открытых сетов.
    /// </summary>
    public partial class OpenTilesView : UserControl
    {
        // Player?
        private Direction direction;
        private bool discard;
        TileBox last;

        public OpenTilesView()
        {
            InitializeComponent();
            discard = true;
        }

        /// <summary>
        /// Задаёт или получает направление в котором лежат тайлы. 
        /// Нельзя задать направление, если в элементе управления уже имеются тайлы.
        /// </summary>
        public Direction Direction
        {
            get
            {
                return direction;
            }
            set
            {
                if (this.Controls.Count == 0)
                {
                    if (Math.Abs(direction - value) != 180) // Нужно повернуть элемент управления.
                        this.Size = new Size(this.Size.Height, this.Size.Width);
                    direction = value;
                }
            }
        }

        /// <summary>
        /// Задаёт или возвращает, является ли набор тайлов сбросом.
        /// Иначе он является открытыми сетами.
        /// Нельзя задать, если уже есть тайлы.
        /// </summary>
        public bool IsDiscard
        {
            get { return discard; }
            set
            {
                if (this.Controls.Count == 0)
                {
                    discard = value;
                }
            }
        }

        public List<Tile> Tiles
        {
            get { return this.Controls.OfType<TileBox>().Select(box => box.Tile).ToList(); }
        }

        /// <summary>
        /// Кладёт тайл и отображает его в элементе управления.
        /// </summary>
        /// <param name="tile">Сброшенный тайл.</param>
        /// <param name="turned">Нужно ли поворачивать тайл.</param>
        public void Put(Tile tile, bool turned = false, bool closed = false)
        {
            // Создание тайла.
            TileBox newTile = new TileBox(tile)
            {
                Direction = this.Direction
            };

            #region Создание тайла (универсальное).

            int x = newTile.CenterLocation.X;
            int y = 0;
            bool notSwitchXY = newTile.Height > newTile.Width;
            // y
            int row = this.Controls.Count / (discard ? Constants.DISCARD_ROW : Int32.MaxValue);
            y = (int)(((float)row + 0.5) * (notSwitchXY ? newTile.Height : newTile.Width));
            // x
            IEnumerable<TileBox> thisRow = this.Controls.OfType<TileBox>().Skip(row * (discard ? Constants.DISCARD_ROW : Int32.MaxValue));
            if (thisRow.Count() != 0)
            {
                Func<TileBox, int> searchForX;
                switch (this.discard ? this.Direction : Reverse(Direction)) //
                {
                    case Direction.ToPlayer:
                        searchForX = box => box.CenterLocation.X + box.Width / 2 + newTile.Width / 2;
                        break;
                    case Direction.ToLeftPlayer:
                        searchForX = box => box.CenterLocation.Y + box.Height / 2 + newTile.Height / 2;
                        break;
                    case Direction.ToOppositePlayer:
                        searchForX = box => this.Width - box.CenterLocation.X + box.Width / 2 + newTile.Width / 2
                            + (turned ? newTile.Height - newTile.Width : 0);
                        break;
                    case Direction.ToRightPlayer:
                        searchForX = box => this.Height - box.CenterLocation.Y + box.Height / 2 + newTile.Height / 2
                            - (turned ? newTile.Height - newTile.Width : 0);
                        break;
                    default:
                        searchForX = box => 0;
                        break;
                }
                x = thisRow.Max(searchForX);
            }
            // CenterLocation
            SetCenterLocation(newTile, x, y);
            newTile.TurnedSideways = turned;
            //
            if (turned && thisRow.Count() == 0)
            {
                if (!notSwitchXY)
                    SetCenterLocation(newTile, newTile.CenterLocation.X, newTile.Height - newTile.Width);
                else
                    SetCenterLocation(newTile, newTile.Width - newTile.Height, newTile.CenterLocation.Y);
            }
            //
            newTile.Closed = closed;
            #endregion

            // Добавление тайла.
            this.Controls.Add(newTile);
            this.last = newTile;

            // Вызов события.
            bool displayedToPlayer = OnPut(tile);
            if (displayedToPlayer)
                newTile.Highlighted = true;

            //if (!IsDiscard)
            //    AdaptSize(newTile);
        }

        /// <summary>
        /// Заставляет исчезнуть последний сброшенный тайл.
        /// </summary>
        public void StealDiscarded()
        {
            this.Controls.Remove(last);
            last = null;
            // riichi?
        }

        /// <summary>
        /// Возвращает подсвеченный для игрока тайл в нормальное состояние.
        /// </summary>
        public void PasPlayerDeclaration()
        {
            if (last != null)
            {
                last.Highlighted = false;
                last = null;
            }
        }

        /// <summary>
        /// Визуализирует открытый сет из данных тайлов. 
        /// Если количество тайлов не совпадвет с необходимым для типа объявления 
        /// или если свойство IsDiscard установленно в true, выбрасывает исключение.
        /// </summary>
        /// <param name="type">Тип объявления.</param>
        /// <param name="from">От какого игрока был получен завершающий тайл.</param>
        /// <param name="tiles">Тайлы, из которых сотсоит объявленный сет.</param>
        /// <exception cref="InvalidDelclarationException">
        /// Возникает, если элемент управления установлен в режим дискарда или дано неверное количество тайлов для объявления.
        /// </exception>
        public void Declare(DeclarationType type, DeclaredFrom from, params Tile[] tiles)
        {
            if (IsDiscard)
                throw new Exceptions.InvalidDeclarationException
                    ("Элемент управления установлен в режим дискарда, в дискар нельзя делать объявления!");

            if (tiles.Count() == 1 && type == DeclarationType.Kan) // Апгрейд кана.
            {
                UpgrateKan(tiles[0]);
            }
            else
            {
                if (type == DeclarationType.Chi || type == DeclarationType.Pon)
                {
                    PutN(from, 3, tiles);
                }
                else if (type == DeclarationType.Kan)
                {
                    PutN(from, 4, tiles);
                }
                else if (type == DeclarationType.ClosedKan)
                {
                    ClosedKan(tiles);
                }
            }
        }

        private void PutN(DeclaredFrom from, int num, params Tile[] tiles)
        {
            if (tiles.Count() != num)
                throw new Exceptions.InvalidDeclarationException("Неверное количество тайлов.");

            for (int i = 0; i < tiles.Count(); i++)
            {
                this.Put(tiles[i], i == (int)from);
            }
        }

        private void UpgrateKan(Tile newTile)
        {
            var tiles = this.Controls.OfType<TileBox>().Select(box => new { tile = box.Tile, turnedSideways = box.TurnedSideways, closed = box.Closed }).ToList();
            this.Controls.Clear();

            int ponCount = 0;

            foreach (var tileData in tiles)
            {
                if (tileData.tile.Category() == newTile.Category() && tileData.tile.CompareTo(newTile) == 0)
                {
                    ponCount++;
                }

                this.Put(tileData.tile, tileData.turnedSideways, tileData.closed);

                if (ponCount == 3)
                {
                    this.Put(newTile);
                    ponCount++;
                }
            }

            //TileBox down = this.Controls.OfType<TileBox>().Single(
            //    box =>
            //        box.Tile.Category() == tile.Category() &&
            //        box.Tile.CompareTo(tile) == 0 &&
            //        box.TurnedSideways
            //    );

            //TileBox up = new TileBox(tile)
            //{
            //    TurnedSideways = true
            //};

            //SetCenterLocation(up, down.CenterLocation.X, down.CenterLocation.Y/* + Constants.TILE_WIDTH*/);

            //this.Controls.Add(up);

            //AdaptSize();
        }

        private void ClosedKan(params Tile[] tiles)
        {
            if (tiles.Count() != 4)
                throw new Exceptions.InvalidDeclarationException("Не 4 тайла для закрытого кана.");

            for (int i = 0; i < tiles.Count(); i++)
            {
                this.Put(tiles[i], false, i == 0 || i == tiles.Count() - 1);
            }
        }

        //private List<TileBox> Clear()
        //{
        //    List<TileBox> boxes = this.Controls.OfType<TileBox>().ToList();
        //    this.Controls.Clear();
        //    return boxes;
        //}

        private void AdaptSize(TileBox newTile)
        {
            if (this.Direction == Direction.ToPlayer || this.Direction == Direction.ToOppositePlayer)
            {
                this.Width += newTile.Width;
            }
            else
            {
                this.Height += newTile.Height;
            }
            this.Invalidate();
        }

        private static Direction Reverse(Direction dir)
        {
            int dif = (int)dir - Constants.ROUND / 2;
            if (dif < 0)
                dif = Constants.ROUND + dif;
            return (Direction)dif;
        }

        private void SetCenterLocation(TileBox newTile, int x, int y)
        {
            switch (this.discard ? this.Direction : Reverse(Direction))
            {
                case Direction.ToPlayer:
                    newTile.CenterLocation = new Point(x, y);
                    break;
                case Direction.ToLeftPlayer:
                    newTile.CenterLocation = new Point(this.Width - y, x);
                    break;
                case Direction.ToOppositePlayer:
                    newTile.CenterLocation = new Point(this.Width - x, this.Height - y);
                    break;
                case Direction.ToRightPlayer:
                    newTile.CenterLocation = new Point(y, this.Height - x);
                    break;
            }
        }

        private bool OnPut(Tile tile)
        {
            if (PutEvent != null)
                return PutEvent(tile, this.Direction);

            return false;
        }

        public delegate bool PutEventHandler(Tile tile, Direction dir);

        /// <summary>
        /// Происходит при добавлении тайла.
        /// </summary>
        public event PutEventHandler PutEvent;
    }
}
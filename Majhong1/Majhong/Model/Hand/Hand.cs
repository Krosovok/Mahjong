using Mahjong.Model.HandSpace;
using Mahjong.Model.Tiles;
using Mahjong.View.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.HandSpace
{
    [Flags] public enum ChiType { None = 0x0, Left = 0x1, Middle = 0x2, Right = 0x4 }

    /// <summary>
    /// Представляет руку.
    /// </summary>
    public class Hand : IEnumerable<Tile>
    {
        CategoryList[] categories;
        public bool IsOpen { set; get; }
        public Tile LastTile { set; get; }
        public bool Riichi { set; get; }
        private Tempai isTempai;

        /// <summary>
        /// Создаёт пустую руку.
        /// </summary>
        public Hand() 
        {
            categories = new CategoryList[6];
            for(int i = 0; i < categories.Length; i++)
            {
                categories[i] = new CategoryList();
            }
            IsOpen = false;
        }

        public Hand(Hand source, Tile toDelete)
            : this()
        {
            foreach (Tile tile in source)
            {
                categories[(int)tile.Category()].Add(tile);
                LastTile = tile;

                GetAllConfigurations();
            }

            categories[(int)toDelete.Category()].Delete(toDelete);
            GetAllConfigurations();
        }

        public CategoryList[] Categories
        {
            get { return categories; }
        }

        public List<HandConfiguration> Configurations { get; private set; }


        /// <summary>
        /// Проверяет, находиться ли рука в темпай.
        /// Возвращает список тайлов, которые можно сбросить, чтобы остаться темпай.
        /// Если таких тайлов нет, возвращает пустой список.
        /// </summary>
        public Tempai IsTempai 
        {
            get
            {
                return isTempai;
            }
        }

        /// <summary>
        /// Добавляет тайл в руку в соответствующую категорию.
        /// </summary>
        public void Draw(Tile toAdd)
        {
            MoveVariants moveVar = new MoveVariants();
            moveVar.CanTsumo = CheckTsumo(toAdd); // Ok???

            categories[(int)toAdd.Category()].Add(toAdd);
            LastTile = toAdd;

            GetAllConfigurations();
            isTempai = CheckTempai();


            moveVar.CanClosedKan = CheckClosedKan();
            moveVar.CanRiichi = CheckRiichi();
            OnNewTileDrawn(moveVar);
        }

        /// <summary>
        /// Удаляет тайл из соответствующей категории.
        /// </summary>
        public void Discard(Tile toDelete)
        {
            categories[(int)toDelete.Category()].Delete(toDelete);

            GetAllConfigurations();
            isTempai.Wait = isTempai.TempaiVariants.Keys.Contains(toDelete) ?
                isTempai.TempaiVariants[toDelete] : new List<Tile>();
        }

        /// <summary>
        /// Проверяет, можно ли на руке объявить Чи с данным тайлом.
        /// </summary>
        /// <param name="tile">Тайл для проверки.</param>
        /// <returns>Какие Чи можно объявить с этого тайла.</returns>
        public ChiType CheckChi(SuitedTile tile)
        {
            if (Riichi)
                return ChiType.None;

            ChiType type = ChiType.None;
            if (tile == null) { return type; }

            type |= this.Any(t => tile.Category() == t.Category() && tile.CompareTo(t) == 2) &&
                this.Any(t => tile.Category() == t.Category() && tile.CompareTo(t) == 1) ?
                ChiType.Left : ChiType.None;
            type |= this.Any(t => tile.Category() == t.Category() && tile.CompareTo(t) == 1) &&
                this.Any(t => tile.Category() == t.Category() && tile.CompareTo(t) == -1) ?
                ChiType.Middle : ChiType.None;
            type |= this.Any(t => tile.Category() == t.Category() && tile.CompareTo(t) == -2) &&
                this.Any(t => tile.Category() == t.Category() && tile.CompareTo(t) == -1) ?
                ChiType.Right : ChiType.None;

            return type;
        }

        /// <summary>
        /// Проверяет, можно ли объявить Пон с данным тайлом.
        /// </summary>
        /// <param name="tile">Тайл для проверки.</param>
        /// <returns>Можно ли объявить Пон.</returns>
        public bool CheckPon(Tile tile)
        {
            if (Riichi)
                return false;

            IEnumerator enumer = categories[(int)tile.Category()].GetEnumerator();
            int i = 0;
            for (; i == 2 || enumer.MoveNext(); )
            {
                if (tile.CompareTo(enumer.Current as Tile) == 0)
                {
                    i++;
                }
            }
            return i >= 2;
        }

        /// <summary>
        /// Проверяет, не даёт ли тайл объявить улучшение Пона до Кана.
        /// </summary>
        /// <param name="tile">Тайл для проверки.</param>
        /// <returns>Можно ли объявить улучшение.</returns>
        public bool CheckUpgradePon(Tile tile)
        {
            IEnumerator enumer = categories[5].GetEnumerator();
            int i = 0;
            for (; i == 3 || enumer.MoveNext(); )
            {
                if (tile.CompareTo(enumer.Current as Tile) == 0)
                {
                    i++;
                }
            }
            return i >= 3;
        }

        /// <summary>
        /// Проверяет, можно ли объявить Кан с данным тайлом.
        /// </summary>
        /// <param name="tile">Тайл для проверки.</param>
        /// <returns>Можно ли объявить Кан.</returns>
        public bool CheckKan(Tile tile)
        {
            if (Riichi)
                return false;

            IEnumerator enumer = categories[(int)tile.Category()].GetEnumerator();
            int i = 0;
            for (; i == 3 || enumer.MoveNext(); )
            {
                if (tile.CompareTo(enumer.Current as Tile) == 0)
                {
                    i++;
                }
            }
            return i >= 3;
        }

        /// <summary>
        /// Проверяет, можно ли объявить Рон с данного тайла.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public bool CheckRon(Tile tile)
        {
            return IsTempai.True && IsTempai.Wait.Contains(tile); 
        }

        /// <summary>
        /// Проверяет, возможно ли объявить закрытый Кан.
        /// </summary>
        /// <returns>Тайлы, с которых моно объявить закрытый кан. Если тайкого тайла нет, возвращает пустой список.</returns>
        public List<Tile> CheckClosedKan()
        {
            if (Riichi)
                return new List<Tile>();

            List<Tile> kanTiles = new List<Tile>();

            for (int i = 0; i < 5; i++ )
            {
                IEnumerator enumer = categories[i].GetEnumerator();
                int j = 0;
                enumer.MoveNext();
                Tile prev = (Tile)enumer.Current;
                for (; j == 3 || enumer.MoveNext(); )
                {
                    if (prev.CompareTo(enumer.Current as Tile) == 0)
                    {
                        j++;
                    }
                    if (j == 3)
                    {
                        Tile kanTile = prev;
                        //return kanTile;
                        kanTiles.Add(kanTile);
                    }
                    else
                    {
                        j = 0;
                        prev = enumer.Current as Tile;
                    }
                }
            }
            return kanTiles;
        }

        /// <summary>
        /// Вызывать с данными ДО взятия нового тайла, но после самого момента взятия.
        /// </summary>
        /// <returns></returns>
        public bool CheckTsumo(Tile tile)
        {
            return IsTempai.True && IsTempai.Wait.Contains(tile);
        }

        public List<Tile> CheckRiichi()
        {
            return this.IsOpen ? new List<Tile>() : this.IsTempai.TempaiVariants.Keys.ToList();
        }

        public List<Tile> DeclareChi(SuitedTile discardedTile, ChiType type)
        {
            IsOpen = true;
            List<Tile> res = new List<Tile>();

            res.Add(discardedTile);

            if ((type & ChiType.Left) == ChiType.Left)
            {
                for (int i = 2; i > 0; i--)
                    res.Add(this.First(t => discardedTile.Category() == t.Category() && discardedTile.CompareTo(t) == i));
            }
            else if ((type & ChiType.Middle) == ChiType.Middle)
            {
                res.Add(this.First(t => discardedTile.Category() == t.Category() && discardedTile.CompareTo(t) == 1));
                res.Add(this.First(t => discardedTile.Category() == t.Category() && discardedTile.CompareTo(t) == -1));
            }
            else if ((type & ChiType.Right) == ChiType.Right)
            {
                for (int i = -2; i < 0; i++)
                    res.Add(this.First(t => discardedTile.Category() == t.Category() && discardedTile.CompareTo(t) == i));
            }

            for (int i = 1; i < res.Count; i++ )
            {
                categories[(int)discardedTile.Category()].Delete(res[i]);
            }

            Group g = new Group(res, null);
            categories[5].Add(g);

            OnSetDeclared(false);

            return res;
        }

        public List<Tile> DeclarePon(Tile discardedTile)
        {
            IsOpen = true;
            List<Tile> res = new List<Tile>();

            res.Add(discardedTile);

            int i = 0;
            foreach (Tile tile in categories[(int)discardedTile.Category()])
            {
                if (tile.CompareTo(discardedTile) == 0)
                {
                    res.Add(tile);
                    //categories[(int)discardedTile.Category()].Delete(tile);
                    i++;
                }
                if (i == 2)
                {
                    break;
                }
            }

            for (int j = 1; j < res.Count; j++)
            {
                categories[(int)discardedTile.Category()].Delete(res[j]);
            }

            Group g = new Group(res, null);
            categories[5].Add(g);

            OnSetDeclared(false);

            return res;

        }

        public Tile DeclareUpgradePon(Tile tile)
        {
            for (int i = 0; i < categories[5].Count; i++ )
            {
                Group g = categories[5][i];

                if (g.Min.CompareTo(tile) == 0 && 
                    g.Max.CompareTo(g.Min) == 0 &&
                    g.Count == 3)
                {
                    g.Add(tile);
                }
            }

            categories[(int)tile.Category()].Delete(tile);

            OnSetDeclared(true);

            return tile;
        }

        public List<Tile> DeclareKan(Tile discardedTile)
        {
            IsOpen = true;
            List<Tile> res = new List<Tile>();
            res.Add(discardedTile);

            int i = 0;
            foreach (Tile tile in categories[(int)discardedTile.Category()])
            {
                if (tile.CompareTo(discardedTile) == 0)
                {
                    res.Add(tile);
                    //categories[(int)discardedTile.Category()].Delete(tile);
                    i++;
                }
                if (i == 3)
                {
                    break;
                }
            }

            for (int j = 1; j < res.Count; j++)
            {
                categories[(int)discardedTile.Category()].Delete(res[j]);
            }

            Group g = new Group(res, null);
            categories[5].Add(g);

            OnSetDeclared(true);

            return res;

        }

        public List<Tile> DeclareClosedKan(Tile tile) // Тоже гененрируется событие SetDeclared! // В Обработчике тайл берётся ДО удаления 4-ого. Может вызвать ошибки.
        {
            //List<Tile> res = DeclareKan(tile);

            //res.Add(tile);
            //categories[(int)tile.Category()].Delete(tile);

            //return res;
            List<Tile> res = new List<Tile>();
            res.Add(tile);

            int i = 0;
            foreach (Tile t in categories[(int)tile.Category()])
            {
                if (t.CompareTo(tile) == 0)
                {
                    res.Add(t);
                    //categories[(int)discardedTile.Category()].Delete(tile);
                    i++;
                }
                if (i == 3)
                {
                    break;
                }
            }

            for (int j = 1; j < res.Count; j++)
            {
                categories[(int)tile.Category()].Delete(res[j]);
            }

            Group g = new Group(res, null);
            categories[5].Add(g);

            OnSetDeclared(true);

            return res;
        }

        /// <summary>
        /// Определяет, есть ли на руке якухаи и, если есть, прибавляет к текущей стоимости руки их стоимость.
        /// </summary>
        /// <param name="roundWind"> Ветер раунда </param>
        /// <param name="playerwind"> Ветер игрока </param>
        /// <param name="hanValue"> Текущая стоимость руки. </param>
        void checkYakuhai(char roundWind, char playerwind, ref int hanValue)
        {
            CategoryList dragons = categories[(int)TileCategory.Dragon];
            CategoryList winds = categories[(int)TileCategory.Wind];

            for (int i = 0; i < 2; i++)
            {
                HandForm dragonsPon = new HandForm(GroupValue.Pon, new DragonTile(DragonTile.values[i]));
                if (dragons.Contains(dragonsPon))
                {
                    hanValue++;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                HandForm windPon = new HandForm(GroupValue.Pon, new WindTile(WindTile.values[i]));

                if (winds.Contains(windPon))
                {
                    if (windPon.First.Type == roundWind)
                    {
                        hanValue++;
                    }

                    if (windPon.First.Type == playerwind)
                    {
                        hanValue++;
                    }
                }
            }
        }

        /// <summary>
        /// Определяет, есть ли на руке таньяо и, если есть, прибавляет к текущей стоимости руки его стоимость.
        /// </summary>
        /// <param name="hanValue"> Текущая стоимость руки. </param>
        void checkTtanyao(ref int hanValue)
        {
            bool isTanyao = true;

            foreach (Tile tile in this)
            {
                if (tile.IsTerminal() || tile is HonourTile)
                {
                    isTanyao = false;
                }
            }

            if (isTanyao) { hanValue++; }
        }

        Tempai CheckTempai()
        {
            Tempai tempai = new Tempai() { Wait = new List<Tile>() };
            tempai.TempaiVariants = new Dictionary<Tile, List<Tile>>();

            foreach (Tile tile in this)
            {
                Hand possibleTempai = new Hand(this, tile);

                List<GroupValue> setValues = new List<GroupValue>() { GroupValue.Chi, GroupValue.Pon, GroupValue.Kan };

                List<GroupValue> twosValues = new List<GroupValue>() { GroupValue.Penchan, GroupValue.Kanchan, GroupValue.Ryanmen };


                foreach (HandConfiguration config in possibleTempai.Configurations)
                {
                    bool isTempai = CheckConfig(config, tempai, setValues, twosValues, tile);
                    if(isTempai)
                    {
                        tempai.True = true;
                    }
                }
            }
            return tempai;
        }

        private bool CheckConfig(HandConfiguration config, Tempai tempai,
            List<GroupValue> setValues, List<GroupValue> twosValues, Tile tile)
        {
            bool isTempai = false;
            //tempai.True = false;
            int setQuant = 0;
            int pairQuant = 0;
            int twosQuant = 0;


            foreach (HandForm handForm in config.Forms)
            {
                if (setValues.Contains(handForm.Value))
                {
                    setQuant++;
                }

                else if (handForm.Value == GroupValue.Pair)
                {
                    pairQuant++;
                }

                else if (twosValues.Contains(handForm.Value))
                {
                    twosQuant++;
                }
            }

            int idx = categories[5].Cast<object>().Count();

            if (setQuant + idx == 4 || pairQuant == 6)
            {

                HandForm form = config.Forms
                    .First(f => f.Value == GroupValue.Single);

                isTempai = true;

                if (tempai.TempaiVariants.ContainsKey(tile))
                {
                    tempai.TempaiVariants[tile]/*.ElementAt(tempai.TempaiVariants.Count - 1)
                        .Value*/.Add(form.First);
                }
                else
                {
                    tempai.TempaiVariants.Add(tile, new List<Tile>() { form.First });
                }

            }

            else if (setQuant + idx == 3 && pairQuant == 2)
            {
                List<HandForm> pairs = config.Forms
                    .Where(f => f.Value == GroupValue.Pair)
                    .ToList();

                isTempai = true;

                if (tempai.TempaiVariants.ContainsKey(tile))
                {
                    tempai.TempaiVariants[tile]//.ElementAt(tempai.TempaiVariants.Count - 1)
                        /*.Value*/.Add(pairs[0].First);
                }
                else
                {
                    tempai.TempaiVariants.Add
                        (tile, new List<Tile>() { pairs[0].First });
                }

                if (!pairs[0].Equals(pairs[1]))
                {
                    tempai.TempaiVariants.ElementAt(tempai.TempaiVariants.Count - 1)
                        .Value.Add(pairs[1].First);
                }
            }
            else if (setQuant + idx == 3 && pairQuant == 1 && twosQuant == 1)
            {
                isTempai = true;
                HandForm form = config.Forms
                    .First(f => twosValues.Contains(f.Value));

                List<Tile> wait = AnalyzeForm(form);

                if (!tempai.TempaiVariants.ContainsKey(tile))
                {
                    tempai.TempaiVariants.Add(tile, wait);
                }

                else
                {
                    if (!tempai.TempaiVariants.ContainsKey(tile))
                    {
                        if (!tempai.TempaiVariants.Contains
                            (new KeyValuePair<Tile, List<Tile>>(tile, wait)))
                        {
                            wait.ForEach
                               (w => tempai.TempaiVariants
                               .ElementAt(tempai.TempaiVariants.Count - 1)
                               .Value.Add(w));
                        }
                    }
                }
            }
            return isTempai;
        }

        List<Tile> AnalyzeForm(HandForm form)
        {
            List<Tile> res = new List<Tile>();


            switch (form.Value)
            {
                case GroupValue.Penchan:
                    if (form.First.Type == '1')
                    {
                        res.Add(Tile.CustomTile(form.First.Category(), '3'));
                    }

                    else if (form.First.Type == '8')
                    {
                        res.Add(Tile.CustomTile(form.First.Category(), '9'));
                    }
                    break;

                case GroupValue.Kanchan:
                    int oldValue = Convert.ToInt32(form.First.Type);
                    char newValue = Convert.ToChar(++oldValue);
                    res.Add(Tile.CustomTile(form.First.Category(), newValue));
                    break;


                case GroupValue.Ryanmen:
                    int first = Convert.ToInt32(form.First.Type);
                    char prev = Convert.ToChar(--first);
                    char next = Convert.ToChar(++first);

                    res.Add(Tile.CustomTile(form.First.Category(), prev));
                    res.Add(Tile.CustomTile(form.First.Category(), next));
                    break;
            }
            return res;
        }





        List<Group> GetAllForms()
        {
            List<Group> res = new List<Group>();

            List<CategoryList> x = categories.Where(c => c.Count != 0).ToList();

            for (int i = 0; i < x.Count(); i++)
            {
                for (int j = 0; j < x[i].Count; j++)
                {
                    res.Add(x[i][j]);
                }
            }
            return res;
        }

        public void GetAllConfigurations()
        {
            List<HandConfiguration> res = new List<HandConfiguration>();

            List<Group> lst = GetAllForms();

            foreach (Group g in lst)
            {
                res = Combine(res, g);
            }

            Configurations = res.Distinct(new HandConfigComparer<HandConfiguration>()).ToList();
        }

        static List<HandConfiguration> Combine
            (List<HandConfiguration> lst, Group toCombine)
        {
            if (lst.Count == 0)
            {
                lst = new List<HandConfiguration>(toCombine.Forms);
            }

            else if (toCombine == null)
            {
                return lst;
            }
            else
            {
                List<HandConfiguration> lstSecond = toCombine.Forms;

                lst = lst
                    .SelectMany(c => lstSecond
                        .Select(conf => new HandConfiguration
                            (c.Forms.Concat(conf.Forms).ToList())))
                        .ToList();
            }
            return lst;

        }

        IEnumerator<Tile> IEnumerable<Tile>.GetEnumerator()
        {
            int i = 0;
            foreach (CategoryList category in categories)
            {
                if (i == 5)
                    continue;

                foreach (Tile tile in category)
                {
                    yield return tile;
                }
                i++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            int i = 0;
            foreach (CategoryList category in categories)
            {
                if (i == 5)
                    continue;

                foreach (Tile tile in category)
                {
                    yield return tile;
                }
                i++;
            }
        }

        private void OnSetDeclared(bool isKan)
        {
            if (SetDeclared != null)
                SetDeclared(isKan);
        }

        private void OnNewTileDrawn(MoveVariants moveVar)
        {
            if (NewTileDrawn != null)
                NewTileDrawn(moveVar);
        }

        public delegate void SetDeclaredEventHandler(bool isKan);

        public event SetDeclaredEventHandler SetDeclared;

        public delegate void NewTileDrawnHandler(MoveVariants moveVar);

        /// <summary>
        /// Происходит при взятии тайла. 
        /// В обработчике стоит включить возможные альтернативы сбросу: риичи, закрытый Кан, Цумо.
        /// </summary>
        public event NewTileDrawnHandler NewTileDrawn;

    }

    //public struct Tempai
    //{
    //    public bool True { get; set; }
    //    public Dictionary<Tile, List<Tile>> TempaiVariants { get; set; }
    //    public List<Tile> Wait { get; set; }
    //}
}

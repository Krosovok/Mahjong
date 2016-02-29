using Mahjong.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahjong.Exceptions;
using System.Collections;

namespace Mahjong.Model.HandSpace // Доступ?
{
    /// <summary>
    /// Группа тайлов или "форма". 
    /// Упорядоченная группа тайлов, которые соеденены в одну форму (напр. 🀚🀛🀝 2-3-5)
    /// </summary>
    public class Group : IEnumerable
    {
        //Методы и поля для работы с группой как со списком тайлов.
        #region Data
        List<Tile> tiles;

        public Tile Min
        {
            get { return tiles.First(); }
        }
        public Tile Max
        {
            get { return tiles.Last(); }
        }
        public int Count
        {
            get { return tiles.Count; }
        }
        public List<HandConfiguration> Forms { get; private set; }

        /// <summary>
        /// Определяет, чем является простая форма. В случае, если она сложная 
        /// </summary>
        void Calculate()
        {
            if (Forms[0] == null)
            {
                Forms[0] = new HandConfiguration();
            }
            
            Forms = new List<HandConfiguration>();
            Forms.Add(new HandConfiguration());
            Forms[0].Forms.Add(new HandForm(GroupValue.Single, tiles.First()));
            

            if (tiles.Count == 1) // 1 тайл в группе
            {
                return;
            }

            else if (tiles[1].CompareTo(tiles[0]) == 0) // 2й = 1й
            {
                Forms[0].Forms[0] = new HandForm(GroupValue.Pair, tiles.First()); // Ожидание на третий.

                if (tiles.Count > 2)
                {
                    if (tiles[2].CompareTo(tiles[0]) == 0) // 3й = 2й
                    {
                        Forms[0].Forms[0] = new HandForm(GroupValue.Pon, tiles.First()); // Три одинаковых

                        if (tiles.Count > 3)
                        {
                            Forms.RemoveAt(0);
                            CalculateComplex(); // Cложная форма
                        }

                        if (tiles.Count > 3 && tiles[3].CompareTo(tiles[0]) == 0) // 4й = 3й
                        {
                            Forms[0].Forms[0] = new HandForm(GroupValue.Kan, tiles.First()); // Четыре одинаковых

                            if (tiles.Count > 4)
                            {
                                Forms.RemoveAt(0);
                                CalculateComplex(); // Cложная форма

                            }
                        }

                    }
                    else
                    {
                        Forms.RemoveAt(0);
                        CalculateComplex(); // Cложная форма
                    }
                }
            }
            else if (tiles[1].CompareTo(tiles[0]) == 1) //2й < 1й на 1
            {
                if (!(tiles[0].Type == '1' || tiles[1].Type == '8'))
                {
                     Forms[0].Forms[0] = new HandForm(GroupValue.Ryanmen, tiles.First()); // Ожидание в две стороны.
                }
                else
                {
                     Forms[0].Forms[0] = new HandForm(GroupValue.Penchan, tiles.First()); // Ожидание в одну сторону.
                }

                if (tiles.Count > 2)
                {
                    if (tiles[2].CompareTo(tiles[1]) == 1) // 3й > 2й на 1
                    {
                        Forms[0].Forms[0] = new HandForm(GroupValue.Chi, tiles.First()); // Последовательность из ирёх.
                        if(tiles.Count > 3)
                        {
                            Forms.RemoveAt(0);
                            CalculateComplex(); // Cложная форма
                        }
                    }
                    else
                    {
                        Forms.RemoveAt(0);
                        CalculateComplex(); // Cложная форма

                    }
                }
            }

            else if (tiles[1].CompareTo(tiles[0]) == 2) // 2й > 1й на 2
            {
                 Forms[0].Forms[0] = new HandForm(GroupValue.Kanchan, tiles.First()); // Ожидание в дырку.

                if (tiles.Count > 2)
                {
                    Forms.RemoveAt(0);
                    CalculateComplex(); // Cложная форма

                }
            }

            //RemoveDuplicates();
            //Filter();

        }
        /// <summary>
        /// Находит все конфигурации сложной формы в виде множеств простых форм и записывает их в св-во группы.
        /// </summary>
        void CalculateComplex()
        {
            Forms = new List<HandConfiguration>();
            List<bool> visited = new List<bool>();
            var res = new List<HandConfiguration>();
            List<HandConfiguration> tempFirst = new List<HandConfiguration>();
            List<HandConfiguration> tempSecond = new List<HandConfiguration>();
            HandConfiguration temp = new HandConfiguration();
            List<HandConfiguration> tempFinal = new List<HandConfiguration>();


            for (int k = 0; k < tiles.Count; k++)
            {
                visited.Add(false);
            }

            List<bool> initial = new List<bool>(visited);


            // visited = new List<bool>(initial);

            for (int i = 0; i < this.Count; i++)
            {
                tempFirst = new List<HandConfiguration>();
                tempSecond = new List<HandConfiguration>();
                temp = new HandConfiguration();
                tempFinal = new List<HandConfiguration>();

                for (int j = 0; j <= i; j++)
                {
                    visited[j] = true;
                }
                List<bool> copy = new List<bool>(visited);

                tempFirst = tempFirst.Union(Search(visited)).ToList();

                for (int l = 0; l < copy.Count; l++)
                {
                    copy[l] = !copy[i];
                }

                tempSecond = tempSecond.Union(Search(copy)).ToList();

                /*
                if (tempFirst.Count == 0)
                {
                    tempFinal = new List<HandConfiguration>(tempSecond);
                }

                if (tempSecond.Count == 0)
                {
                    tempFinal = new List<HandConfiguration>(tempFirst);
                }
                for (int g = 0; g < tempFirst.Count; g++)
                {
                    for (int h = 0; h < tempSecond.Count; h++)
                    {
                        temp = new HandConfiguration();
                        temp.Forms.AddRange(tempFirst[g].Forms);
                        temp.Forms.AddRange(tempSecond[h].Forms);

                        tempFinal.Add(temp);
                    }
                }

                */
                tempFinal = tempFirst
                    .SelectMany(conf => tempSecond
                        .Select(c => new HandConfiguration(conf.Forms.Concat(c.Forms).ToList())))
                    .ToList();

                res = res.Union(tempFinal).ToList();

                visited = new List<bool>(initial);
            }

            Forms = res
                .Distinct()
                .OrderByDescending(c => c.Forms.Sum(f => f.Prioruty))
                .ToList();
        }

        List<HandConfiguration> Search
            (List<bool> visited)
        {
            List<HandConfiguration> firstPart = new List<HandConfiguration>();
            List<HandConfiguration> secondPart = new List<HandConfiguration>();

            for (int i = 0; i < visited.Count; i++)
            {
                if (!visited[i]) { break; }
                if (i == visited.Count - 1) { return new List<HandConfiguration>(); }
            }

            int lastIdx = visited.FindLastIndex(x => !x);
            int firstIdx = visited.FindIndex(x => !x);

            Tile firstTile = null;
            Tile secondTile = null;
            List<bool> copy = new List<bool>(visited);

            if (firstIdx != -1)
            {
                firstTile = tiles[firstIdx];
                copy.RemoveAt(firstIdx);
            }

            int secondIdx = copy.FindIndex(x => !x) + 1;

            if (secondIdx != 0)
            {
                secondTile = tiles[secondIdx];
            }

            int diff = (secondTile != null && firstTile != null)
                ? secondTile.CompareTo(firstTile)
                : -1;


            if (diff == 0 && lastIdx != secondIdx)
            {

                List<bool> visitedCopy = new List<bool>(visited);

                HandForm f = AnalyzeSelective(visited);

                var x = Search(visited);

                if (x.Count == 0)
                {
                    firstPart.Add(new HandConfiguration(new List<HandForm>()));
                    firstPart.First().Forms.Add(f);
                }

                for (int i = 0; i < x.Count; i++)
                {
                    x[i].Forms.Insert(0, f);
                    firstPart.Add(x[i]);
                }

                visited = new List<bool>(visitedCopy);
            }

            HandForm form = AnalyzeLinear(visited);

            var y = Search(visited);

            if (y.Count == 0)
            {
                secondPart.Add(new HandConfiguration(new List<HandForm>()));
                secondPart.First().Forms.Add(form);
            }
            else
            {
                for (int i = 0; i < y.Count; i++)
                {
                    y[i].Forms.Insert(0, form);
                    secondPart.Add(y[i]);
                }
            }
            return firstPart.Union(secondPart).ToList();
        }


        HandForm AnalyzeSelective
            (List<bool> visited)
        {
            int startIdx = visited.FindIndex(x => !x);
            int endIdx = visited.FindLastIndex(x => !x);
            int length = visited.Count(x => !x);

            HandForm res = new HandForm(GroupValue.Single, tiles[startIdx]);
            visited[startIdx] = true;


            if (length == 1)
            {
                return res;
            }

            for (int i = startIdx + 1; i <= endIdx; i++) // Поиск тайла на 1 больше
            {
                if (visited[i]) { continue; }

                if (tiles[i].CompareTo(tiles[startIdx]) == 1 &&
                    res.Value != GroupValue.Ryanmen && res.Value != GroupValue.Penchan) // 2й > 1й на 1. Нашли второй тайл чи.
                {
                    visited[i] = true;

                    for (int j = i; j <= endIdx; j++)
                    {
                        if (visited[j]) { continue; }

                        if (tiles[j].CompareTo(tiles[startIdx]) == 2) // 3й > 1й на 2. Есть чи.
                        {
                            res = new HandForm(GroupValue.Chi, tiles[startIdx]);
                            visited[j] = true;
                            return res;
                        }
                        else // Нет чи. Проверка на рянмен/пинчан.
                        {
                            if (tiles[startIdx].Type == '1' || tiles[startIdx + 1].Type == '9')
                            {
                                res = new HandForm(GroupValue.Penchan, tiles[startIdx]); // Ожидание в одну сторону.
                            }
                            else
                            {
                 
                                res = new HandForm(GroupValue.Ryanmen, tiles[startIdx]);  // Ожидание в две стороны.
                            }
                            //visited[startIdx] = true;
                            //visited[i] = true;
                            //return res;
                        }
                    }

                    if (tiles[startIdx].Type == '1' || tiles[i].Type == '9')
                    {
                        res = new HandForm(GroupValue.Penchan, tiles[startIdx]); // Ожидание в одну сторону.
                    }
                    else
                    {

                        res = new HandForm(GroupValue.Ryanmen, tiles[startIdx]);  // Ожидание в две стороны.
                    }
                }



                else // Не нашли рянмен/ренчан. Ищем канчан.
                {
                    if (tiles[i].CompareTo(tiles[startIdx]) == 2)
                    {
                        res = new HandForm(GroupValue.Kanchan, tiles[startIdx]);
                        visited[i] = true;
                        return res;
                    }
                }


            }
            return res;
        }


        HandForm AnalyzeLinear
           (List<bool> visited)
        {
            int startIdx = visited.FindIndex(x => !x);
            int endIdx = visited.FindLastIndex(x => !x);
            int length = visited.Count(x => !x);


            HandForm res = new HandForm(GroupValue.Single, tiles[startIdx]); // Ожидание в пару.
            visited[startIdx] = true;

            if (length == 1)
            {
                return res;
            }
            for (int i = startIdx + 1; i <= endIdx; i++)
            {
                if (visited[i]) { continue; }

                if (tiles[i].CompareTo(tiles[startIdx]) == 1) //2й < 1й на 1
                {
                    visited[i] = true;

                    if (tiles[startIdx].Type == '1' || tiles[i].Type == '9')
                    {
                        res = new HandForm(GroupValue.Penchan, tiles[startIdx]); // Ожидание в одну сторону.
                    }
                    else
                    {

                        res = new HandForm(GroupValue.Ryanmen, tiles[startIdx]);  // Ожидание в две стороны.
                    }

                    if (length == 2) { return res; }

                    for (int j = i + 1; j <= endIdx; j++)
                    {
                        if (visited[j]) { continue; }

                        if (tiles[j].CompareTo(tiles[startIdx]) == 2) // 3й > 1й на 2
                        {
                            res = new HandForm(GroupValue.Chi, tiles[startIdx]); // Последовательность из ирёх.
                            visited[j] = true;
                            return res;
                        }

                        else { return res; }
                    }
                }

                else if (tiles[i].CompareTo(tiles[startIdx]) == 2) // 2й > 1й на 2
                {
                    res = new HandForm(GroupValue.Kanchan, tiles[startIdx]); // Ожидание в дырку.
                    visited[i] = true;
                    return res;
                }


                else if (tiles[i].CompareTo(tiles[startIdx]) == 0) // 2й = 1й
                {
                    res = new HandForm(GroupValue.Pair, tiles[startIdx]); // Ожидание на третий.
                    visited[i] = true;

                    if (length == 2)
                    {
                        return res;
                    }

                    for (int j = i + 1; j <= endIdx; j++)
                    {

                        if (visited[j]) { continue; }

                        if (tiles[j].CompareTo(tiles[startIdx]) == 0) // 3й = 2й
                        {
                            res = new HandForm(GroupValue.Pon, tiles[startIdx]); // Три одинаковых
                            visited[j] = true;

                            if (length == 3) { return res; }

                            for (int k = j + 1; k < length; k++)
                            {
                                if (visited[k]) { continue; }

                                if (tiles[k].CompareTo(tiles[startIdx]) == 0) // 4й = 3й
                                {
                                    res = new HandForm(GroupValue.Kan, tiles[startIdx]); ; // Четыре одинаковых
                                    visited[k] = true;

                                    return res;
                                }
                                else { break; }
                            }
                        }
                        else { return res; }
                    }
                }
                else
                {
                    break;
                }
            }
            return res;
        }


        /// <summary>
        /// Удаляет тайл из группы
        /// </summary>
        public void Delete(Tile toDelete)
        {

            if (!this.tiles.Contains(toDelete))
            {
                throw new HandStructureException();
            }
            if (CheckIfDisconnect(toDelete))
            {
                int breakPoint = this.tiles.IndexOf(toDelete);

                // Создаём дополнительную группу и наполняем её отделившимися тайлами,
                // удаляя их из начальной группы.
                Group g = new Group(tiles[breakPoint + 1], null);

                if (Next == null) { Next = g; }
               
                else
                {
                    g.Next = this.Next;
                    this.Next = g;
                }

                for (int i = breakPoint + 1; i < this.tiles.Count; i++)
                {
                    if (!g.Contains(this.tiles[i]))
                    {
                        g.Add(this.tiles[i]);
                    }
                }

                foreach (Tile t in this.Next)
                {
                    this.tiles.Remove(t);
                }
            }
            this.tiles.Remove(toDelete);

            Calculate();
        }

        /// <summary>
        /// Добавляет тайл в группу и, если нужно, производит слияние
        /// </summary>
        public void Add(Tile toAdd)
        {
            //Values.Clear();
            for (int i = 0; i < Forms.Count; i++)
            {
                Forms[i] = new HandConfiguration();
            }
            SortedAdd(toAdd);

            if (CheckIfConnectTo(toAdd, Next))
            {
                MergeWith(Next);
            }
            Calculate();
        }

       

        public bool Contains(HandForm form)
        {
            foreach (HandConfiguration c in Forms)
            {
                if (c.Forms.Contains(form))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains(char tileValue)
        {
            return tiles
                .Select(t => t.Type).ToList()
                .Contains(tileValue);
        }

        public bool Contains(Tile tile)
        {
            return tiles.Contains(tile);
        }

        /// <summary>
        /// Соединяет 2 группы в одну.
        /// </summary>
        void MergeWith(Group group)
        {
            foreach (Tile t in group.tiles)
            {
                SortedAdd(t);
            }
            this.Next = this.Next.Next;
        }

        /// <summary>
        /// Добавляет тайл в список тайлов группы с сохранением порядка.
        /// </summary>
        void SortedAdd(Tile toAdd)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (toAdd.CompareTo(tiles[i]) < 0)
                {
                    tiles.Insert(i, toAdd);
                    return;
                }
            }
            tiles.Add(toAdd);
        }

        /// <summary>
        /// Проверяет, может ли тайл войти в данную группу.
        /// </summary>
        bool CanBeAdded(Tile tile)
        {
            if (tile is HonourTile)
            {
                return tile.CompareTo(this.Min) == 0;
            }
            else
            {
                return tile.CompareTo(this.Min) >= -2 && tile.CompareTo(this.Max) <= 2;
            }


        }

        /// <summary>
        /// Проверка распадается ли группа на две после удаления тайла
        /// </summary>
        bool CheckIfDisconnect(Tile toBeDeleted)
        {
            if (!this.tiles.Contains(toBeDeleted))
            {
                throw new HandStructureException();
            }

            // Группа может распасться только если она из тайлов-мастей
            if (toBeDeleted is SuitedTile)
            {
                int idx = this.tiles.IndexOf(toBeDeleted);
                //Если удаляемый тайл - крайний в группе, то она не распадается
                if (idx == 0 || idx == tiles.Count - 1)
                {
                    return false;
                }

                // Если тайлы, соседние с удаляемым не составляют ожидания - группа распадается.
                if (tiles[idx + 1].CompareTo(tiles[idx - 1]) > 2)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверяет, нужно ли соединить данную группу со другой после добавления в нее нового тайла.
        /// </summary>
        /// <param name="newTile"> Добавленный тайл </param>
        /// <param name="group"> Группа, с которой, возможно, будет слияние</param>
        bool CheckIfConnectTo(Tile newTile, Group group)
        {
            if (newTile is HonourTile) { return false; }

            int idx = tiles.IndexOf(newTile);
            if (idx != tiles.Count - 1) { return false; }

            int difference = (newTile is HonourTile)
                    ? 0 : -2;
            if (Next == null) { return false; }
            if (newTile.CompareTo(Next.Min) < difference)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Создает группу из одного тайла с указанием на следущую группу.
        /// </summary>
        public Group(Tile tile, Group next)
        {
            this.Forms = new List<HandConfiguration>();
            Forms.Add(new HandConfiguration());
            this.tiles = new List<Tile>() { tile };
            Calculate();
            this.Next = next;
        }

        public Group(Group group, Group next)
        {
            this.Forms = new List<HandConfiguration>();
            Forms.Add(new HandConfiguration());
            this.tiles = new List<Tile>(group.tiles);
            Calculate();
            this.Next = next;
        }

        public Group(List<Tile> tiles, Group next)
        {
            this.Forms = new List<HandConfiguration>();
            Forms.Add(new HandConfiguration());
            this.tiles = new List<Tile>(tiles);
            Calculate();
            this.Next = next;
        }

        public IEnumerator GetEnumerator()
        {
            return tiles.GetEnumerator();
        }

        //Методы и поля для работы с группой как с элементом связного списка.
        #region Node
        public Group Next { get; set; }

        /// <summary>
        /// Добавляет тайл в соответствующую группу.
        /// </summary>
        public void AddRecursive(Tile toBeAdded)
        {
            int idx = (toBeAdded is HonourTile)
                    ? 0
                    : 2;

            if (CanBeAdded(toBeAdded))
            {
                this.Add(toBeAdded);
            }
            else
            {
                if (this.Next == null)
                {
                    this.Next = new Group(toBeAdded, null);
                }

                else if (Next.Min.CompareTo(toBeAdded) > idx)
                {
                    Group g = new Group(toBeAdded, this.Next);
                    this.Next = g;
                }
                else
                {
                    this.Next.AddRecursive(toBeAdded);
                }

            }
        }

        /// <summary>
        /// Удаляет тайл из соответствующей группы.
        /// </summary>
        public void DeleteRecursive(Tile toBeDeleted)
        {
            // Удаляемого тайла нет на руке.
            if (Next == null || toBeDeleted.CompareTo(Next.Min) < 0)
            {
                throw new HandStructureException();
            }

            // Удаляемый тайл возможно содержится в следующей группе.
            if (toBeDeleted.CompareTo(Next.Max) <= 0)
            {
                // Если в группе 1 тайл,
                if (Next.Count == 1)
                {
                    // нужно удалить группу целиком.
                    if (Next.Min.CompareTo(toBeDeleted) == 0)
                    {
                        Next = Next.Next;
                    }
                    else
                    {
                        throw new Exceptions.HandStructureException();
                    }
                }
                // Если много,
                else
                {
                    // Вызываем Delete группы
                    Next.Delete(toBeDeleted);
                }

            }
            else
            {
                // Если тайла нет в группе - вызываем данный метод на следующей
                Next.DeleteRecursive(toBeDeleted);
            }
        }

        #endregion
    }

    public enum GroupValue
    {
        None,
        Single, 
        Penchan, 
        Kanchan, 
        Ryanmen, 
        Pair, // Two 
        Chi, // Chi
        Pon, // Pon
        Kan  // Kan
    }


    //class ListComparer<T> : IEqualityComparer<List<T>>
    //{
    //    public bool Equals(List<T> x, List<T> y)
    //    {
    //        if (x.Count != y.Count) { return false; }

    //        for (int i = 0; i < x.Count; i++)
    //        {
    //            if (!x[i].Equals(y[i]))
    //            {
    //                return false;
    //            }
    //        }
    //        return true; ;
    //    }

    //    public int GetHashCode(List<T> obj)
    //    {
    //        int hashcode = 0;
    //        foreach (T t in obj)
    //        {
    //            hashcode ^= t.GetHashCode();
    //        }
    //        return hashcode;
    //    }
    //}

    class HandConfigComparer<T> : IEqualityComparer<HandConfiguration>
    {
        bool IEqualityComparer<HandConfiguration>.Equals(HandConfiguration x, HandConfiguration y)
        {
            return x.Equals(y);
        }

        int IEqualityComparer<HandConfiguration>.GetHashCode(HandConfiguration obj)
        {
            int hashcode = 0;
            foreach (HandForm f in obj.Forms)
            {
                hashcode ^= f.GetHashCode();
            }
            return hashcode;
        }
    }
}

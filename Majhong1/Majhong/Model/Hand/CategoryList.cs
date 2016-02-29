using Mahjong.Exceptions;
using Mahjong.Model.HandSpace;
using Mahjong.Model.Tiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.HandSpace
{
    /// <summary>
    /// Cписок групп (или форм), связанных между собой.
    /// В категории находятся группы одной масти или достоинства.
    /// Содержит указатель на первый элемент.
    /// </summary>
    public class CategoryList : IEnumerable, IEnumerable<Group>
    {
        Group first;

        public int Count
        {
            get
            {
                int i = 0;
                for (Group curr = first; curr != null; curr = curr.Next)
                {
                    i++;
                }
                return i;
            }
        }

        public CategoryList() { }

        /// <summary>
        /// Удаляет тайл.
        /// </summary>
        public void Delete(Tile toBeDeleted)
        {
            // Удаляемого тайла нет на руке.
            int n = toBeDeleted.CompareTo(first.Min);
            if (first == null || n < 0)
            {
                throw new HandStructureException();
            }

            // Удаляемый тайл возможно содержится в первой группе.
            if (toBeDeleted.CompareTo(first.Max) <= 0)
            {
                // Если в группе один тайл - удаляем её.
                if (first.Count == 1)
                {
                    if (first.Min.CompareTo(toBeDeleted) == 0)
                    {
                        first = first.Next;
                    }
                    else
                    {
                        throw new Exceptions.HandStructureException();
                    }
                }
                else
                {
                    // Если тайлов много - вызываем метод Delete группы. 
                    first.Delete(toBeDeleted);
                }
            }
            else
            {
                // Если удаляемого тайла нет в первой группе, запускаем рекурсивный проход по группам.
                first.DeleteRecursive(toBeDeleted);
            }
        }

        /// <summary>
        /// Добавляет тайл.
        /// </summary>
        public void Add(Tile toBeAdded)
        {

            // Если таких тайлов не было - создаем первую группу.
            if (first == null)
            {
                first = new Group(toBeAdded, null);
                return;
            }

            int difference = (toBeAdded is HonourTile)
                    ? 0 : -2;

            // Если тайл меньше первого тайла группы и не составляет 
            // с ним ожидания - создаем новую группу.  

            if (toBeAdded.CompareTo(first.Min) < difference)
            {
                first = new Group(toBeAdded, first);
            }
            // Иначе определяем, в какую группу его добавить
            else
            {
                first.AddRecursive(toBeAdded);
            }

        }

        /// <summary>
        /// Добавляет группу.
        /// </summary>
        /// <param name="group"></param>
        public void Add(Group group)
        {
            first = new Group(group, first);
        }

      

        public bool Contains(HandForm form)
        {
            for (Group curr = first; curr != null; curr = curr.Next)
            {
                if (curr.Contains(form))
                {
                    return true;
                }
            }

            return false;
        }

        public Group this[int idx]
        {
            get
            {
                int i = 0;
                for (Group curr = first; curr != null; curr = curr.Next)
                {
                    if (i == idx) { return curr; }
                    i++;
                }
                return null;
            }
        }

        //public List<HandConfiguration> GetAllConfigurations()
        //{
        //    List<HandConfiguration> res = new List<HandConfiguration>();

        //}

        public IEnumerator GetEnumerator()
        {
            for (Group currentGroup = first; currentGroup != null;
                                    currentGroup = currentGroup.Next)
            {
                foreach (Tile t in currentGroup)
                {
                    yield return t;
                }
            }
        }



        IEnumerator<Group> IEnumerable<Group>.GetEnumerator()
        {
            for (Group curr = first; curr != null; curr = curr.Next)
            {
                yield return curr;
            }
        }
    }
}

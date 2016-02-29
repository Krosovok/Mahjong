using Mahjong.Model.HandSpace;
using Mahjong.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.HandSpace
{
    class CompletedHand
    {
        //HandForm только пон, кан, чи, танки. добавить в GroupValue Single для кокуши?
        List<HandForm>[] categories;
        Tile lastTile;
        bool isOpen;
        char playerWind;
        char roundWind;

        public Dictionary<string, int> Yaku { set; get; }

        public CompletedHand(List<HandForm> forms, Tile last, bool open, char pWind, char rWind)
        {
            categories = new List<HandForm>[6];

            for (int i = 0; i < categories.Length; i++)
            {
                categories[i] = new List<HandForm>();
            }
            foreach (HandForm hf in forms)
            {
                categories[(int)hf.First.Category()].Add(hf);
            }
            lastTile = last;
            isOpen = open;
            playerWind = pWind;
            roundWind = rWind;
            Yaku = new Dictionary<string, int>();
            CheckAll();
        }

        void CheckAll()
        {
            checkYakuhai();
            checkTanyao();
            checkJunchan();
            checkChanta();
            checkPinfu();
            checkIppeiko();
            checkItsu();
            checkSanshoku();
            checkChinitsu();
            checkHonitsu();
            checkToitoi();
            checkChitoitsu();
            checkSananko();
            checkSanshokuDokou();
            checkHonroto();
            checkSankantsu();
            checkSyosangen();
        }

        void checkYakuhai()
        {
            char[] chars = new char[] {roundWind, playerWind, 'C', 'B', 'F'};
            for (int i = 3; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if ((hf.Value == GroupValue.Pon || hf.Value == GroupValue.Kan) && chars.Contains(hf.First.Type))
                    {
                        if(Yaku.ContainsKey("Якухай"))
                        {
                            Yaku["Якухай"] += roundWind == playerWind && hf.First.Type == playerWind ? 2 : 1;
                        }
                        else
                        {
                            Yaku.Add("Якухай", roundWind == playerWind && hf.First.Type == playerWind ? 2 : 1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Определяет, есть ли на руке таньяо и, если есть, прибавляет к текущей стоимости руки его стоимость.
        /// </summary>
        /// <param name="hanValue"> Текущая стоимость руки. </param>
        void checkTanyao()
        {
            if (categories[3].Count != 0 || categories[4].Count != 0)
            {
                return;
            }

            for (int i = 0; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if (hf.Contains('1') || hf.Contains('9'))
                    {
                        return;
                    }
                }
            }

            Yaku.Add("Таняо", 1);
        }

        void checkChanta()
        {
            //bool isOpenHand = categories[5].Where<HandForm>(hf => hf.Value != GroupValue.ClosedKan).Count() != 0;
            char[] chars = new char[] { '1', '9', 'C', 'B', 'F', 'E', 'S', 'W', 'N' };
            for (int i = 0; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if ((hf.Value == GroupValue.Pon || hf.Value == GroupValue.Kan || hf.Value == GroupValue.Pair) && !chars.Contains(hf.First.Type) 
                        || hf.Value == GroupValue.Chi && (hf.First.Type != '1' || hf.First.Type != '7'))
                    {
                        return;
                    }
                }
            }
            if (!Yaku.ContainsKey("Джунчан"))
            {
                Yaku.Add("Чанта", isOpen ? 1 : 2);
            }
        }

        void checkJunchan()
        {
            //bool isOpenHand = categories[5].Where<HandForm>(hf => hf.Value != GroupValue.ClosedKan).Count() != 0;
            char[] chars = new char[] { '1', '9'};
            for (int i = 0; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if ((hf.Value == GroupValue.Pon || hf.Value == GroupValue.Kan || hf.Value == GroupValue.Pair) && !chars.Contains(hf.First.Type)
                        || hf.Value == GroupValue.Chi && (hf.First.Type != '1' || hf.First.Type != '7'))
                    {
                        return;
                    }
                }
            }
            Yaku.Add("Джунчан", isOpen ? 2 : 3);
        }

        void checkPinfu()
        {
            if (isOpen || !(lastTile is SuitedTile))
            {
                return;
            }
            for (int i = 0; i < 4; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if (hf.Value != GroupValue.Chi && hf.Value != GroupValue.Pair 
                        || hf.Value == GroupValue.Pair && (hf.First.Type == roundWind || hf.First.Type == playerWind) 
                        || hf.Value == GroupValue.Chi && Convert.ToInt32(lastTile.Type) - Convert.ToInt32(hf.First.Type) == 1)
                    {
                        return;
                    }
                }
            }
            Yaku.Add("Пинфу", 1);
        }

        //возвращать перечислитель иппейко-рянпейко?
        void checkIppeiko()
        {
            if (isOpen)
            {
                return;
            }
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; i < categories[i].Count - 1; j++)
                {
                    if (categories[i][j].Value == GroupValue.Chi && categories[i][j].Equals(categories[i][j + 1]))
                    {
                        count++;
                    }
                }
            }

            if (count == 1)
            {
                Yaku.Add("Иппейко", 1);
            }
            if (count == 2 && !Yaku.ContainsKey("Иппейко"))
            {
                Yaku.Add("Рянпейко", 3);
            }
        }

        //void checkRyanpeiko(ref int hanValue)
        //{
        //    if (categories[5].Count != 0)
        //    {
        //        return;
        //    }
        //    for (int i = 0; i < 3; i++)
        //    {
        //        for (int j = 0; i < categories[i].Count - 1; j++)
        //        {
        //            if (categories[i][j].Value == GroupValue.Chi && categories[i][j].Equals(categories[i][j + 1]))
        //            {
        //                hanValue++;
        //                return;
        //            }
        //        }
        //    }
        //}

        void checkItsu()
        {
            //bool isOpenHand = categories[5].Where<HandForm>(hf => hf.Value != GroupValue.ClosedKan).Count() != 0;
            for (int i = 0; i < 3; i++)
            {
                if (categories[i].Where<HandForm>(hf => hf.Value == GroupValue.Chi)
                    .Union(categories[5].Where<HandForm>(hf => hf.Value == GroupValue.Chi && (int)hf.First.Category() == i))
                    .Select<HandForm, char>(hf => hf.First.Type).Distinct() == new char[] { '1', '4', '7' })
                {
                    Yaku.Add("Иццу", isOpen ? 1 : 2);
                }
            }
        }

        void checkSanshoku()
        {
            //bool isOpenHand = categories[5].Where<HandForm>(hf => hf.Value != GroupValue.ClosedKan).Count() != 0;
            List<HandForm> chi = new List<HandForm>();
            for (int i = 0; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if (hf.Value == GroupValue.Chi && hf.First is SuitedTile)
                    {
                        chi.Add(hf);
                    }
                }
            }
            if (chi.Count < 3
                || chi.Select<HandForm, char>(hf => hf.First.Type).Distinct().Count() > 2
                || chi.Select<HandForm, int>(hf => (int)hf.First.Category()).Distinct().Count() < 3)
            {
                return;
            }
            Yaku.Add("Саншоку", isOpen ? 1 : 2);
        }

        void checkHonitsu()
        {
            //bool isOpenHand = categories[5].Where<HandForm>(hf => hf.Value != GroupValue.ClosedKan).Count() != 0;
            List<HandForm> forms = new List<HandForm>();
            for (int i = 0; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if (forms.Count == 0 
                        || (int)forms.First().First.Category() < 3 && hf.First.Category() == forms.First().First.Category()
                        || (int)forms.First().First.Category() >= 3)
                    {
                        forms.Add(hf);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            if (!Yaku.ContainsKey("Чиницу"))
            {
                Yaku.Add("Хоницу", isOpen ? 2 : 3);
            }
        }

        void checkChinitsu()
        {
            //bool isOpenHand = categories[5].Where<HandForm>(hf => hf.Value != GroupValue.ClosedKan).Count() != 0;
            List<HandForm> forms = new List<HandForm>();
            for (int i = 0; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if (forms.Count == 0 && (int)hf.First.Category() < 3
                        || hf.First.Category() == forms.First().First.Category())
                    {
                        forms.Add(hf);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            Yaku.Add("Чиницу", isOpen ? 5 : 6);
        }

        void checkToitoi()
        {
            for (int i = 0; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if (hf.Value != GroupValue.Pon || hf.Value != GroupValue.Kan || hf.Value != GroupValue.Pair)
                    {
                        return;
                    }
                }
            }
            Yaku.Add("Тойтой", 2);
        }

        void checkChitoitsu()
        {
            if (categories[5].Count != 0)
            {
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if (hf.Value != GroupValue.Pair)
                    {
                        return;
                    }
                }
            }
            if (!Yaku.ContainsKey("Рянпейко"))
            {
                Yaku.Add("Читойцу", 2);
            }
        }

        //суанко тоже
        void checkSananko()
        {
            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if ((i < 5 && hf.Value == GroupValue.Pon || hf.Value == GroupValue.Kan && !isOpen) 
                        && !(lastTile.GetType() == hf.First.GetType() && lastTile.CompareTo(hf.First) == 0))
                    {
                        count++;
                    }
                }
            }
            if (count == 4)
            {
                Yaku.Add("Суанко", 13);
            }
            if (count == 3)
            {
                Yaku.Add("Сананко", 2);
            }
        }

        void checkSanshokuDokou()
        {
            List<HandForm> pon = new List<HandForm>();
            for (int i = 0; i < 6; i ++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if ((hf.Value == GroupValue.Pon || hf.Value == GroupValue.Kan)
                        && hf.First is SuitedTile)
                    {
                        pon.Add(hf);
                    }
                }
            }
            if (pon.Count < 3 
                || pon.Select<HandForm, char>(hf => hf.First.Type).Distinct().Count() > 2
                || pon.Select<HandForm, int>(hf => (int)hf.First.Category()).Distinct().Count() < 3)
            {
                return;
            }
            Yaku.Add("Саншоку-доко", 2);
        }

        void checkHonroto()
        {
            char[] chars = new char[] { '1', '9', 'C', 'B', 'F', 'E', 'S', 'W', 'N' };
            for (int i = 0; i < 6; i++)
            {
                foreach (HandForm hf in categories[i])
                {
                    if ((hf.Value != GroupValue.Pon || hf.Value != GroupValue.Kan 
                        || hf.Value != GroupValue.Pair) 
                        || !chars.Contains(hf.First.Type))
                    {
                        return;
                    }
                }
            }
            Yaku.Add("Хонрото", 2);
        }

        //сунканцу тоже
        void checkSankantsu()
        {
            int count = 0;
            foreach (HandForm hf in categories[5])
            {
                if (hf.Value == GroupValue.Kan)
                {
                    count++;
                }
            }
            if (count == 4)
            {
                Yaku.Add("Сунканцу", 13);
            }
            if (count == 3)
            {
                Yaku.Add("Санканцу", 2);
            }
        }

        //Дайсанген тоже
        void checkSyosangen()
        {
            int countPon = 0;
            bool tanki = false;
            foreach (HandForm hf in categories[5])
            {
                if ((hf.Value == GroupValue.Kan 
                    || hf.Value == GroupValue.Pon) && hf.First.Category() == TileCategory.Dragon)
                {
                    countPon++;
                }
                if ((hf.Value == GroupValue.Pair) && hf.First.Category() == TileCategory.Dragon)
                {
                    tanki = true;
                }
            }
            if (countPon == 3)
            {
                Yaku.Add("Дайсанген", 13);
            }
            if (countPon == 2 && tanki)
            {
                Yaku.Add("Сесанген", 2);
            }
        }

        //void checkKokushi(ref int hanValue)
        //{
        //    char[] chars = new char[] { '1', '9', 'C', 'B', 'F', 'E', 'S', 'W', 'N' };
        //    List<HandForm> forms = new List<HandForm>();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        foreach (HandForm hf in categories[i])
        //        {
        //            if (hf.Value != GroupValue.Single || hf.Value!= GroupValue.Tanki || !chars.Contains(hf.First.Type))
        //            {
        //                return;
        //            }
        //            else
        //            {
        //                forms.Add(hf);
        //            }
        //        }
        //    }
        //    hf.
        //}
    }
}

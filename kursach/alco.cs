using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kursach
{
    public class alco
    {
        //1Название
        
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        //2Крепость

        private double spirt;
         public double Spirt
        {
            get { return spirt; }
            set { spirt = value; }
        }
        //3Наиминование
         private string sort;
         public string Sort
         {
             get { return sort; }
             set { sort = value; }
         }
        //4Цена
        private int price;
        public int  Price
        {
            get { return price; }
            set { price = value; }
        }
        //5Количество
        private int kol;
        public int Kol
        {
            get { return kol; }
            set { kol = value; }
        }
        //6Чек
        private bool scholarship;
        public bool Scholarship
        {
            get { return scholarship; }
            set { scholarship = value; }
        }

    }
}

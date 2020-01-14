using System;
using System.Collections.Generic;
using System.Text;

namespace Imperium_Marketplace_Lib
{
    class Player_Model
    {
        public string ID { get; set; }
        public string Name { get; set; }
        string Cash { get; set; }

        public void AddCash(int price)
        {
            Cash += price;
        }
    }
}

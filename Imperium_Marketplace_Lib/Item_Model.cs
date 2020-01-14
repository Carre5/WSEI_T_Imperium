using System;

namespace Imperium_Marketplace_Lib
{
    public class Item_Model
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        public string PlayerID { get; set; }
    }
}

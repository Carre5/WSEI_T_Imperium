using System;
using System.Collections.Generic;
using System.Text;

namespace Imperium_Marketplace_Lib
{
    class Marketplace_Controller
    {
        List<Player_Model> Players { get; }
        List<Item_Model> Items { get; }

        public Marketplace_Controller()
        {
            Items = UpdateMarketplaceItems();
            Players = UpdateMarketplacePlayers();
        }

        private List<Player_Model> UpdateMarketplacePlayers()
        {
            List<Player_Model> _list = new List<Player_Model>()
            {
                new Player_Model()
                {
                    ID = "1",
                    Name = "Marcin"
                }
            };

            return _list;
        }

        private List<Item_Model> UpdateMarketplaceItems()
        {
            List<Item_Model> _list = new List<Item_Model>()
            {
                new Item_Model()
                {
                    PlayerID = "1",
                    Name = "Kryształ",
                    Quantity = 50,
                    Price = 100
                }
            };

            return _list;
        }

        public void Buy(Player_Model player, Item_Model item, int Amount)
        {
            int priceToPay = item.Price * Amount;
            var seller = Players.Find(x => x.ID == item.PlayerID);

            if (seller == null)
                throw new Exception();

            if (Amount > item.Quantity)
            {
                throw new IndexOutOfRangeException();
            }
            else if (Amount == item.Quantity)
            {
                player.AddCash(-priceToPay);
                seller.AddCash(priceToPay);
                Items.Remove(item);
            }
            else
            {
                player.AddCash(-priceToPay);
                seller.AddCash(priceToPay);

                Items.Remove(item);
                item.Quantity -= Amount;
                Items.Add(item);
            }
        }

        public void MakeOffer(Item_Model item, bool isComercial = false)
        {
            if (isComercial)
            {
                Items.Insert(0, item);
            }
            else
            {
                Items.Add(item);
            }
        }

        public List<Item_Model> FindOfferByMaxPrice(int MaxPrice)
        {
            return Items.FindAll(x => x.Price < MaxPrice);
        }
    }
}

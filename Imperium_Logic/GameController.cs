using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Imperium_Logic
{
    /// <summary>
    /// Main controller of all library logic
    /// </summary>
    public class GameController
    {
        BuildingsController buildings;
        RaidController raid;
        MarketplaceController marketplace;

        #region Getters
        /// <summary>
        /// Getter for BuildingController variable
        /// </summary>
        public BuildingsController Buildings
        {
            get => buildings;
        }

        /// <summary>
        /// Getter for RaidController variable
        /// </summary>
        public RaidController Raid
        {
            get => raid;
          
        }
        #endregion

        public GameController()
        {
            buildings = new BuildingsController();
            raid = new RaidController(buildings);
            marketplace= new MarketplaceController(this);

        }

        #region Functions

        /// <summary>
        /// function which is used to get material etc. manually or automatically with timer
        /// </summary>
        public void Prosper()
        {
            buildings.Prosper();
        }

        /// <summary>
        /// function to get actual amount of storage
        /// </summary>
        /// <returns>int tab with all storage minerals, credits</returns>
        public double[] Storage()
        {
            return buildings.Storage();
        }

        /// <summary>
        /// Building levelUp Controller
        /// </summary>
        /// <param name="building">building you want to level up</param>
        /// <returns>Bool which tells if level up is done or not</returns>
        public bool levelUp(string building)
        {
            switch (building)
            {
                case "MINERAL_MINE":
                    if (buildings.Mine.levelUp())
                        return true;
                    break;
                case "POWER_STATION":
                    if (buildings.Powerstation.levelUp())
                        return true;
                    break;
                case "MARKETPLACE":
                    if (buildings.Marketplace.levelUp())
                        return true;
                    break;
                case "ANDROIDFACTORY":
                    if (buildings.AndroidFactory.levelUp())
                        return true;
                    break;
                case "SHIPYARD":
                    if (buildings.Shipyard.levelUp())
                        return true;
                    break;
                default:
                    break;
            }

            return false;
        }

        /// <summary>
        /// function to perform attack on selected target with selected amount of units
        /// </summary>
        /// <param name="index">int which sets target from list by index</param>
        /// <param name="number">int which sets the amount of units which you want to send to fight</param>
        /// <returns>string with info about awards and loses of units</returns>
        public string performAttack(int index,string number)
        {
            return raid.performRaid(index, number);
        }

        /// <summary>
        /// function which is used to sell something to get credits
        /// </summary>
        /// <param name="material">string which tells which material you want to sell</param>
        /// <param name="amount">string which tells how many you want to sell</param>
        /// <returns>info about offer acceptation</returns>
        public bool Sell(string material, int amount)
        {
            return marketplace.Sell(material, amount);
        }

        /// <summary>
        /// function which converts credits to something more useful
        /// </summary>
        /// <param name="material">which material you want to buy</param>
        /// <param name="amount">how many you want to buy</param>
        /// <returns>info abour offer acceptation</returns>
        public bool Buy(string material, int amount)
        {
            return marketplace.Buy(material, amount);
        }
        #endregion
    }
}

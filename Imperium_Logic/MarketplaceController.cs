using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imperium_Logic
{
    public class MarketplaceController
    {
        GameController gc;

        public MarketplaceController(GameController _gc)
        {
            gc = _gc;
        }

        #region Functions
        /// <summary>
        /// functions used to control the sell of resources
        /// </summary>
        /// <param name="material">type of resource to sell</param>
        /// <param name="amount">amount of resource to sell</param>
        /// <returns>info about success of the sell</returns>
        public bool Sell(string material, int amount)
        {
            switch (material)
            {
                case "SELL_ENERGY":
                    if (gc.Buildings.Energy >= amount)
                    {
                        gc.Buildings.Energy -= amount;
                        gc.Buildings.Credits += amount * 0.9;
                        return true;
                    }
                    break;

                case "SELL_MINERALS":
                    if (gc.Buildings.Minerals >= amount)
                    {
                        gc.Buildings.Minerals -= amount;
                        gc.Buildings.Credits += amount * 0.9;
                        return true;
                    }
                    break;

                
             
                  
                 default:
                    break;


            }
            return false;
        }

        /// <summary>
        /// functions used to buy something useful
        /// </summary>
        /// <param name="material">type of resource which you want to buy</param>
        /// <param name="amount">amount of resource you want to buy</param>
        /// <returns>info about success of operation</returns>
        public bool Buy(string material, int amount)
        {
            switch (material)
            {
                case "BUY_MINERAL":
                    if (gc.Buildings.Credits >= amount*2)
                    {
                        gc.Buildings.Minerals += amount;
                        gc.Buildings.Credits -= amount*2;
                        return true;
                    }
                    break;

                case "BUY_ENERGY":
                    if (gc.Buildings.Credits >= amount*4)
                    {
                        gc.Buildings.Energy += amount;
                        gc.Buildings.Credits -= amount*4;
                        return true;
                    }
                    break;

                case "BUY_SHIPS":
                    if (gc.Buildings.Credits >= amount*10)
                    {
                        gc.Buildings.Ships += amount;
                        gc.Buildings.Credits -= amount*10;
                        return true;
                    }
                    break;

                default:
                    break;


            }

            return false;
           

        }
        #endregion

    }
}

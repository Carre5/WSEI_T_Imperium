using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imperium_Logic
{
    public class BuildingsController
    {
        //na zewnątrz tylko gettery
        double energyAmount;
        double mineralsAmount;
        double creditsAmount;

        double shipsAmount;

        #region Getters and Setters
        public double Minerals {
            get
            {
                return mineralsAmount;
            }
            set
            {
                mineralsAmount = value;
            }
        }

        public double Energy
        {
            get
            {
                return energyAmount;
            }
            set
            {
                energyAmount = value;
            }
        }

        public double Credits
        {
            get
            {
                return creditsAmount;
            }
            set
            {
                creditsAmount = value;
            }
        }

        public double Ships
        {
            get
            {
                return shipsAmount;
            }
            set
            {
                shipsAmount = value;
            }
        }

        public MineralMine Mine
        {
            get
            {
                return mine;
            }
        }
        public PowerStation Powerstation
        {
            get
            {
                return powerstation;
            }
        }
        public AndroidFactory AndroidFactory
        {
            get
            {
                return factory;
            }
        }
        public MarketPlace Marketplace
        {
            get
            {
                return marketplace;
            }
        }

        public Shipyard Shipyard
        {
            get
            {
                return shipyard;
            }
        }

        #endregion

        MineralMine mine;
        PowerStation powerstation;
        AndroidFactory factory;
        MarketPlace marketplace;
        Shipyard shipyard;

        public BuildingsController()
        {
            mineralsAmount = 20;
            energyAmount = 10;
            creditsAmount = 0;
            shipsAmount = 0;

            mine = new MineralMine(this,10, 0.5, 1);
            powerstation = new PowerStation(this,10,0,3);

            factory = new AndroidFactory(this,100,0,0);
            marketplace = new MarketPlace(this,500,2,0.1);
            shipyard = new Shipyard(this, 1000, 10, 0.07);


            mine.levelUp();
            powerstation.levelUp();
            factory.levelUp();
        }

        /// <summary>
        /// harvesting the resources
        /// </summary>
        public void Prosper()
        {
            mine.Product();
            powerstation.Product();
            marketplace.Product();
            shipyard.Product();
        }

        /// <summary>
        /// returning actual amounts of resources to controller
        /// </summary>
        /// <returns>int tab with resources status</returns>
        public double[] Storage()
        {
            return new double[] {energyAmount, mineralsAmount, creditsAmount, shipsAmount};
        }
    }

    /// <summary>
    /// class which is used to inherit fields to childrens
    /// </summary>
    public abstract class Buildings
    {
        protected BuildingsController bc;
        protected uint lvl;
        protected double priceModifier;
        protected double energyModifier;
        protected double productionModifier;

        public uint Level
        {
            get { return lvl; }
        }

        public Buildings(BuildingsController _controller, double priceM, double energM, double productionM)
        {
            bc = _controller;
            lvl = 0;

            priceModifier = priceM;
            energyModifier = energM;
            productionModifier = productionM;
        }

        /// <summary>
        /// function to describe the level up price and its success
        /// </summary>
        /// <returns></returns>
        public virtual bool levelUp()
        {
            double price = priceModifier*lvl;

            if (bc.Minerals >= price)
            {
                bc.Minerals -= price;
                lvl++;
                return true;
            }

            return false;
        }

        /// <summary>
        /// function to return the amount and price of production of resource
        /// </summary>
        /// <returns>info about success of production</returns>
        public virtual bool Product()
        {
            return false;
        }
    }

    /// <summary>
    /// mineral mine controller used to generate minerals
    /// </summary>
    public class MineralMine : Buildings
    {
        public MineralMine(BuildingsController _bc, double priceM, double energM, double productionM)
            : base(_bc, priceM, energM, productionM) { }
        public override bool Product()
        {
            double energyConsumption = lvl * energyModifier;
            if (lvl > 0 && bc.Energy > energyConsumption)
            {
                bc.Energy -= energyConsumption;
                bc.Minerals += productionModifier * lvl;
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// power station controller used to generate energy
    /// </summary>
    public class PowerStation : Buildings
    {
        public PowerStation(BuildingsController _bc, double priceM, double energM, double productionM)
            : base(_bc, priceM, energM, productionM) { }

        public override bool Product()
        {
            bc.Energy += productionModifier * lvl;
            return true;
        }
    }

    /// <summary>
    /// Android factory controller used to speed up the prosperity of imperium
    /// </summary>
    public class AndroidFactory : Buildings
    {
        public AndroidFactory(BuildingsController _bc, double priceM, double energM, double productionM)
            : base(_bc, priceM, energM, productionM) { }
    }

    /// <summary>
    /// marketplace controller used to generate credits which helps prosper and speed up production
    /// </summary>
    public class MarketPlace : Buildings
    {
        public MarketPlace(BuildingsController _bc, double priceM, double energM, double productionM)
            : base(_bc, priceM, energM, productionM) { }

        public override bool Product()
        {
            double energyConsumption = lvl * energyModifier;
            if (lvl > 0 && bc.Energy > energyConsumption)
            {
                bc.Energy -= energyConsumption;
                bc.Credits += productionModifier * lvl;
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// shipyard controller used to build units to fight with another planets and imperiums
    /// </summary>
    public class Shipyard : Buildings
    {
        public Shipyard(BuildingsController _bc, double priceM, double energM, double productionM)
            : base(_bc, priceM, energM, productionM) { }

        public override bool Product()
        {
            double energyConsumption = lvl * energyModifier;
            if (lvl > 0 && bc.Energy > energyConsumption)
            {
                bc.Energy -= energyConsumption;
                bc.Ships += productionModifier * lvl;
                return true;
            }

            return false;
        }
    }
}
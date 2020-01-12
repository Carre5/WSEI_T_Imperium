using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imperium_Logic
{
    public class RaidController
    {
        RaidTarget[] raidTargets;
        BuildingsController bc;

        int mod;

        public RaidTarget[] RaidTargets
        {
            get => raidTargets;
        }

        public RaidController(BuildingsController _bc)
        {
            bc = _bc;

            raidTargets = new RaidTarget[] {
                new RaidTarget("Asteroida",10,0.01),
                new RaidTarget("Planeta karłowata",100,0.05),
                new RaidTarget("Mała planeta",1000,0.1),
                new RaidTarget("Średnia planeta",10000,5),
                new RaidTarget("Gigantyczna planeta",30000,10),
                new RaidTarget("Imperium",100000,100),
            };

            generateRaidModifier();
        }

        #region Functions
        /// <summary>
        /// shower of percentage to win the next attack
        /// </summary>
        /// <param name="index">index which targets the target :D</param>
        /// <param name="number">amount of units which you want to send to attack</param>
        /// <returns>returns percentage to whow in UI</returns>
        public RaidValue prepareAttack(int index, string number)
        {
            int[] values = Parser(index, number);

            if(values is null)
            {
                return null;
            }
            else
            {

                int[] vals = generateChanceToWin(values);

                return new RaidValue(vals[0],vals[1]);
            }
        }

        /// <summary>
        /// function which performs the attack
        /// </summary>
        /// <param name="index">index of target</param>
        /// <param name="number">amount of units which player sends to fight</param>
        /// <returns>Info about awards and success and loses</returns>
        public string performRaid(int index, string number)
        {
            string endMessage = "";

            int[] values = Parser(index, number);

            if (values is null)
            {
                endMessage = "ERROR";
            }
            else
            {
                RaidTarget rt = RaidTargets[values[0]];
                int[] vals = generateChanceToWin(values);

                Random rand = new Random();

                if (rand.NextDouble() < vals[0])
                {
                    bc.Minerals += (10 * rt.reward);
                    bc.Credits += (1 * rt.reward);

                    endMessage = "Udało się! Zdobyłeś: "+ (int)(10 * rt.reward)+" minerałów, "+ (1 * rt.reward) +" kredytów. Straciłeś "+(int)vals[1]+" jednostek.";

                    if (values[0] == 5)
                    {
                        endMessage = "ENDGAME";
                    }
                }
                else
                {
                    endMessage = "Spróbuj ponownie. Straciłeś " + (int)vals[1] + " jednostek.";
                }

                bc.Ships -= vals[1] ;

                generateRaidModifier();
            }

            return endMessage;
        }

        private void generateRaidModifier()
        {
            //its important to generate it in independent function and store in variable, because the modifier is random 
            //showing in UI and performing attack will end up in different modifiers which will be the lie to player
            //if player will see the mod and it actually end up in another loses and awards

            Random rand = new Random();
            mod = rand.Next(0, 5);
        }

        private int[] generateChanceToWin(int[] values)
        {

            RaidTarget rt = RaidTargets[values[0]];
            double percentage = Math.Max(0,(50 * (values[1]/rt.defenceValue)) - mod);
            double revPercentage = Math.Max(0,100 - percentage);
            int loses = (int) Math.Min(values[1], Math.Ceiling(values[1] * (revPercentage/100)));

            return new int[] { (int) percentage, loses };
        }

        protected int[] Parser(int index, string number)
        {
            int targetIndex;
            int unitsAmount;

            int[] parsedValues = new int[2];

            try
            {
                targetIndex = index;
                unitsAmount = Int32.Parse(number);

                if (unitsAmount > bc.Ships)
                {
                    return null;
                }

                parsedValues[0] = targetIndex;
                parsedValues[1] = unitsAmount;
            }
            catch
            {
                return null;
            }

            return parsedValues;
        }
        #endregion

        /// <summary>
        /// class that stores the information about target of the raid
        /// </summary>
        public class RaidTarget
        {
            public string name;
            public double defenceValue;
            public double reward;

            public RaidTarget(string _name, double _defense, double rewardModifier)
            {
                name = _name;
                defenceValue = _defense;
                reward = rewardModifier;
            }

            public override string ToString()
            {
                return name;
            }
        }

        /// <summary>
        /// single instance of raid values : percentage of wind and with how many loses you will end up
        /// </summary>
        public class RaidValue
        {
            public int chanceToWinMod;
            public int unitsLoseMod;

            public RaidValue(int ctwm, int ulm)
            {
                chanceToWinMod = ctwm;
                unitsLoseMod = ulm;
            }
        }
    }
}

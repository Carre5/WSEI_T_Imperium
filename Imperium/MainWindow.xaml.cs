using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Imperium_Logic;

namespace Imperium
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameController controller;
        private int interval;

        /// <summary>
        /// MainWindow Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void init(object sender, RoutedEventArgs e)
        {
            controller = new GameController();

            foreach (var item in controller.Raid.RaidTargets)
            {
                raidTargets.Items.Add(item.ToString());
            }
            raidTargets.SelectedIndex = 0;
            Refresh();

            interval = 5000;

            InitTimer();

            for (int i = 0; i < 5; i++)
            {
                sellMinerals.Items.Add(Math.Pow(10, i + 1));
                sellEnergy.Items.Add(Math.Pow(10, i + 1));
                buyShips.Items.Add(Math.Pow(10, i + 1));
                buyEnergy.Items.Add(Math.Pow(10, i + 1));
                buyMinerals.Items.Add(Math.Pow(10, i + 1));
            }
            sellMinerals.SelectedIndex = 0;
            sellEnergy.SelectedIndex = 0;
            buyShips.SelectedIndex = 0;
            buyEnergy.SelectedIndex = 0;
            buyMinerals.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controller.Prosper();
            Refresh();
        }
        private void Refresh()
        {
            Invoker();
        }

        private void RefreshRaid()
        {
            var raidValue = controller.Raid.prepareAttack(raidTargets.SelectedIndex, unitAmount.Text);

            raidPreps.Items.Clear();

            if (raidValue is null)
            {
                raidPreps.Items.Add("ERR: NULL");
                raidPreps.Items.Add("ERR: NULL");
            }
            else
            {
                raidPreps.Items.Add("Szansa: " + raidValue.chanceToWinMod + "%");
                raidPreps.Items.Add("Straty: " + raidValue.unitsLoseMod + "");
            }
        }

        private void btn_mineLvlUp_Click(object sender, RoutedEventArgs e)
        {
            controller.levelUp("MINERAL_MINE");
            Refresh();

            //mine level up and show it in label
        }

        private void btn_powerstationLvlUp_Click(object sender, RoutedEventArgs e)
        {
            controller.levelUp("POWER_STATION");
            Refresh();
            //powerstation levelup and show it in label
        }

        private void btn_laboratoryLvlUp_Click(object sender, RoutedEventArgs e)
        {
            if (controller.levelUp("ANDROIDFACTORY"))
            {
                interval = Math.Max(200, 800 - (100 * (int)controller.Buildings.AndroidFactory.Level));
            }
            Refresh();
            //androidFactory levelUp and show it in label
        }

        private void btn_marketplaceLvlUp_Click(object sender, RoutedEventArgs e)
        {
            controller.levelUp("MARKETPLACE");
            Refresh();
            //marketplace levelUp and show it in label
        }

        private void btn_attackLvlUp_Click(object sender, RoutedEventArgs e)
        {
            controller.levelUp("SHIPYARD");
            Refresh();
            //shipyard level up and show it in label
        }

        private void btn_PerformAttack(object sender, RoutedEventArgs e)
        {
            string msg = controller.performAttack(raidTargets.SelectedIndex, unitAmount.Text);
            if (msg == "ENDGAME")
            {
                MessageBox.Show("Wygrałeś! Twoje Imperium jest najpotężniejsze w galaktyce!");
            }
            else
            {
                MessageBox.Show(msg);
                Refresh();
            }
        }

        private void unitAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshRaid();
        }

        private void raidTargets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshRaid();
        }
        /*
         <summary>
        Invoking refreshing the list of storage
         <summary>
         */
        private Timer timer1;
        private void InitTimer()
        {
            timer1 = new Timer();
            timer1.Elapsed += timer1_Tick;
            timer1.Interval = interval; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            controller.Prosper();
            Refresh();
        }

        /// <summary>
        /// Delegate used to invoke the UI
        /// </summary>
        public delegate void InvokeDelegate();

        private void Invoker()
        {
            listOfStorage.Dispatcher.BeginInvoke(new InvokeDelegate(InvokeMethod));
        }

        /// <summary>
        /// Method to Invoke while refreshing the UI items
        /// </summary>
        public void InvokeMethod()
        {
            double[] strg = controller.Storage();

            listOfStorage.Items.Clear();
            listOfStorage.Items.Add("Energia: " + (int)strg[0]);
            listOfStorage.Items.Add("Minerały: " + (int)strg[1]);
            listOfStorage.Items.Add("Kredyty: " + (int)strg[2]);
            listOfStorage.Items.Add("Flota: " + (int)strg[3]);

            lbl_mineLvl.Content = "poziom " + controller.Buildings.Mine.Level;
            lbl_powerstationLvl.Content = "poziom " + controller.Buildings.Powerstation.Level;
            lbl_laboratoryLvl.Content = "poziom " + controller.Buildings.AndroidFactory.Level;
            lbl_marketplaceLvl.Content = "poziom " + controller.Buildings.Marketplace.Level;
            lbl_attackLvl.Content = "poziom " + controller.Buildings.Shipyard.Level;
        }
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            controller.Buy("BUY_MINERAL", (int)Math.Pow(10, buyMinerals.SelectedIndex + 1));
            Refresh();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            controller.Buy("BUY_ENERGY", (int)Math.Pow(10, buyEnergy.SelectedIndex + 1));
            Refresh();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            controller.Buy("BUY_SHIPS", (int)Math.Pow(10, buyShips.SelectedIndex + 1));
            Refresh();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            controller.Sell("SELL_ENERGY", (int)Math.Pow(10, sellEnergy.SelectedIndex + 1));
            Refresh();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            controller.Sell("SELL_MINERALS", (int)Math.Pow(10, sellMinerals.SelectedIndex + 1));
            Refresh();
        }

        private void buyMinerals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MineralsPrice.Content = "Cena:"+ (int)Math.Pow(10, buyMinerals.SelectedIndex + 1);
               
            Refresh();
        }

        private void buyEnergy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnergyPrice.Content = "Cena:" + (int)Math.Pow(10, buyEnergy.SelectedIndex + 1);

            Refresh();
        }

        private void buyShips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShipsPrice.Content = "Cena:" + (int)Math.Pow(10, buyShips.SelectedIndex + 1);

            Refresh();
        }

        private void sellMinerals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SellMineralsPrice.Content = "Cena:" + (int)Math.Pow(10, sellMinerals.SelectedIndex + 1) * 1.5;

            Refresh();
        }

        private void sellEnergy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SellEnergyPrice.Content = "Cena:" + (int)Math.Pow(10, sellEnergy.SelectedIndex + 1)*1.5;

            Refresh();
        }
    }
}

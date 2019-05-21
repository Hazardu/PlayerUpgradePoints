using System;
using UnityEngine;
using TheForest.Utils;
using TheForest.Save;
using System.IO;
using Bolt;
using PathologicalGames;
using TheForest.Tools;
using TheForest.Items.Inventory;
using ModAPI;
using ModAPI.Attributes;
using TheForest.Audio;
using TheForest.World;
using TheForest.Buildings.World;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.CompilerServices;
using TheForest.Utils.Settings;
using TheForest.Items;
using UniLinq;


namespace PlayerUpgradePoints
{
    class BigBlackClock : Clock
    {
        protected override void ChangeDay()
        {
           
            base.ChangeDay();
            if (UpgradePointsMod.instance.specialUpgrades[15].bought)
            {
                LocalPlayer.Inventory.AddItem(231, 10);
                LocalPlayer.Inventory.AddItem(83, 5);

            }
            if (UpgradePointsMod.instance.specialUpgrades[16].bought)
            {
                LocalPlayer.Inventory.AddItem(33, 25);
            }
            if (UpgradePointsMod.instance.specialUpgrades[21].bought)
            {
                LocalPlayer.Inventory.AddItem(114, 3);
                LocalPlayer.Inventory.AddItem(112, 3);
                LocalPlayer.Inventory.AddItem(113, 3);

            }
        }
    }
}

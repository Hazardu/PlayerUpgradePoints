using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Player.Actions;
using UnityEngine;
namespace PlayerUpgradePoints
{
    class AntiSledDrop:PlayerPushSledAction
    {
        public override void forceExitSled()
        {
            if (UpgradePointsMod.instance.specialUpgrades[40].bought)
            {
                return;
            }
            base.forceExitSled();
        }
        protected override void exitPushSled()
        {
            if (UpgradePointsMod.instance.specialUpgrades[40].bought)
            {
                if (!Input.GetKey(KeyCode.E))
                {
                    return;
                }
            }
            base.exitPushSled();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
namespace PlayerUpgradePoints
{
    class ExplosionRadius : Explosion
    {
        public override void Awake()
        {
            base.Awake();
            if (UpgradePointsMod.instance.specialUpgrades[14].bought)
            {
                
                radius.Value += 1;
            }
        }
    }
}

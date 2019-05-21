using TheForest.Utils;
using PlayerUpgradePoints;
using System.Collections;
using TheForest.Buildings.Creation;
using TheForest.Buildings.World;
using TheForest.Commons.Enums;
using TheForest.Graphics;
using TheForest.Items;
using TheForest.Items.Craft;
using TheForest.Items.Inventory;
using TheForest.Items.World;
using TheForest.Items.World.Interfaces;
using TheForest.Modding.Bridge;
using TheForest.Modding.Bridge.Interfaces;
using TheForest.Player;
using TheForest.Player.Actions;
using TheForest.Player.Clothing;
using TheForest.Player.Data;
using TheForest.World;
using TheForest.World.Areas;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using TheForest.Tools;

namespace PlayerUpgradePoints
{
    public class DoubleTrees : TreeHealth
    {
        public override GameObject DoFallTree()
        {
            if ((bool)TrunkUpperSpawn)
            {
                TrunkUpperSpawn = Object.Instantiate(TrunkUpperSpawn, base.transform.position, base.transform.rotation);
                if (!dontScaleTrunk)
                {
                    TrunkUpperSpawn.transform.localScale = base.transform.localScale;
                }
                if (UpgradePointsMod.instance.specialUpgrades[104].bought)
                {
                    Object.Instantiate(TrunkUpperSpawn, base.transform.position, base.transform.rotation);
                  
                }
            }
            GameObject trunkUpperSpawn = TrunkUpperSpawn;
            LodTree.CurrentView = null;
            DestroyTrunk();
            OnTreeCutDown.Invoke(base.transform.position);
            EventRegistry.Player.Publish(TfEvent.CutTree, this);
            return trunkUpperSpawn;
        }
    }
}


    


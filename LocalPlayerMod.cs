using TheForest.Utils;
using UnityEngine;
using TheForest.Items.World;
using TheForest.Items;

namespace PlayerUpgradePoints
{
    class LocalPlayerMod : LocalPlayer
    {

        private void OnEnable()
        {

            UpgradePointsMod.instance = gameObject.AddComponent<UpgradePointsMod>();
            UpgradePointsMod.instance.Respawn = false;
        }
        
    }

 

}


    


using UnityEngine;
using TheForest.Utils;
using System.Collections;
using TheForest.Items.World;
namespace PlayerUpgradePoints
{
    class AutoPickupItems : MonoBehaviour
    {

        private static float radius = 4;
        private bool enabledLoop;
        public static AutoPickupItems instance;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            if (UpgradePointsMod.instance.specialUpgrades[20].bought)
            {
                TurnOn();
            }
        }
        public void TurnOn()
        {
            if (enabledLoop == false)
            {
                enabledLoop = true;
            }
        }
        public void TurnOff()
        {
            enabledLoop = false;
        }



       void Update()
        {
            if (enabledLoop)
            {
                if (UpgradePointsMod.instance.specialUpgrades[87].bought)
                {
                    radius = 7;
                }
                else
                {
                    radius = 4;
                }
                if (!LocalPlayer.FpCharacter.PushingSled)
                {
                    if (!UpgradePointsMod.instance.specialUpgrades[40].bought)
                    {
                        return;
                    }
                }
                    RaycastHit[] hit = Physics.SphereCastAll(LocalPlayer.Transform.position, radius, Vector3.one, radius + 1);
                    for (int i = 0; i < hit.Length; i++)
                    {
                        PickUp pu = hit[i].transform.GetComponent<PickUp>();
                        if (pu != null)
                        {
                            if (pu._itemId == 57 || pu._itemId == 54 || pu._itemId == 53 || pu._itemId == 42 || pu._itemId == 37 || pu._itemId == 36 || pu._itemId == 33 || pu._itemId == 31 || pu._itemId == 83 || pu._itemId == 91 || pu._itemId == 99 || pu._itemId == 67 || pu._itemId == 89 || pu._itemId == 280 || pu._itemId == 41 || pu._itemId == 56 || pu._itemId == 49 || pu._itemId == 43 || pu._itemId == 262 || pu._itemId == 83 || pu._itemId == 94 || pu._itemId == 99 || pu._itemId == 98 || pu._itemId == 97 || pu._itemId == 178 || pu._itemId == 177 || pu._itemId ==109)
                                if (LocalPlayer.Inventory.AddItem(pu._itemId, pu._amount, true, false, pu._properties))
                                {
                                    Destroy(pu._destroyTarget);
                                }

                        }
                    }
                
            }
        }
    }
}

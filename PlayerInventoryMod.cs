using System.Collections;
using System.Collections.Generic;
using TheForest.Items;
using TheForest.Items.Inventory;
using TheForest.Items.Utils;
using TheForest.Utils;
using UnityEngine;

namespace PlayerUpgradePoints
{
    class PlayerInventoryMod : PlayerInventory
    {
        public override void Start()
        {
            base.Start();
            Invoke("ReduceAmounts", 4);
          
        }

        void ReduceAmounts()
        { DatabaseModifier.instance.Setup();
            DatabaseModifier.instance.Modify();
        }
        public override void AddMaxAmountBonus(int itemId, int amount)
        {
            if (DatabaseModifier.ChangeMaxAmount)
            {
                InventoryItem inventoryItem2;
                if (!_possessedItemCache.ContainsKey(itemId))
                {
                    Item item = ItemDatabase.ItemById(itemId);
                    InventoryItem inventoryItem = new InventoryItem();
                    inventoryItem._itemId = itemId;
                    inventoryItem._amount = 0;
                    inventoryItem._maxAmount = item._maxAmount;
                    inventoryItem2 = inventoryItem;
                    _possessedItems.Add(inventoryItem2);
                    _possessedItemCache[itemId] = inventoryItem2;
                }
                else
                {
                    inventoryItem2 = _possessedItemCache[itemId];

                }
                inventoryItem2._maxAmount += amount;

            }
            else
            {
                base.AddMaxAmountBonus(itemId, amount);
            }
        }

        protected override void FireRangedWeapon()
        {
            InventoryItemView inventoryItemView = _equipmentSlots[0];
            Item itemCache = inventoryItemView.ItemCache;
            bool flag = itemCache._maxAmount < 0;
            bool flag2 = false;
            if (flag || RemoveItem(itemCache._ammoItemId, 1, false, true))
            {
                if (UpgradePointsMod.instance.specialUpgrades[43].bought)
                {
                    float r = Random.value;
                    if (r <= 0.30f)
                    {
                        AddItem(itemCache._ammoItemId, 1);
                    }
                }
                InventoryItemView inventoryItemView2 = _inventoryItemViewsCache[itemCache._ammoItemId][0];
                Item itemCache2 = inventoryItemView2.ItemCache;
                FakeParent component = inventoryItemView2._held.GetComponent<FakeParent>();
                if (UseAltWorldPrefab)
                {
                    Debug.Log("Firing " + itemCache._name + " with '" + inventoryItemView.ActiveBonus + "' ammo (alt=" + UseAltWorldPrefab + ")");
                }
                GameObject gameObject = (!(bool)component || component.gameObject.activeSelf) ? UnityEngine.Object.Instantiate(itemCache2._ammoPrefabs.GetPrefabForBonus(inventoryItemView.ActiveBonus, true).gameObject, inventoryItemView2._held.transform.position, inventoryItemView2._held.transform.rotation) : UnityEngine.Object.Instantiate(itemCache2._ammoPrefabs.GetPrefabForBonus(inventoryItemView.ActiveBonus, true).gameObject, component.RealPosition, component.RealRotation);
                if ((bool)gameObject.GetComponent<Rigidbody>())
                {
                    if (itemCache.MatchRangedStyle(Item.RangedStyle.Shoot))
                    {
                        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.TransformDirection(Vector3.forward * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange), ForceMode.VelocityChange);
                    }
                    else
                    {
                        float num = Time.time - _weaponChargeStartTime;
                        if (ForestVR.Enabled)
                        {
                            gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * (float)(int)itemCache._projectileThrowForceRange);
                        }
                        else
                        {
                            if (UpgradePointsMod.instance != null)
                            {
                                if (UpgradePointsMod.instance.specialUpgrades[31].bought)
                                {
                                    if (UpgradePointsMod.instance.specialUpgrades[75].bought)
                                    {
                                        if (UpgradePointsMod.instance.specialUpgrades[76].bought)
                                        {
                                            if (UpgradePointsMod.instance.specialUpgrades[77].bought)
                                            {
                                                gameObject.GetComponent<Rigidbody>().AddForce(3.5f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                            }
                                            else
                                            {
                                                gameObject.GetComponent<Rigidbody>().AddForce(3f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                            }
                                        }
                                        else
                                        {
                                            gameObject.GetComponent<Rigidbody>().AddForce(2.5f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                        }
                                    }
                                    else
                                    {
                                        gameObject.GetComponent<Rigidbody>().AddForce(2f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                    }
                                }
                                else
                                {
                                    gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                }
                            }
                            else
                            {
                                gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                            }
                        }
                        if (LocalPlayer.Inventory.HasInSlot(Item.EquipmentSlot.RightHand, LocalPlayer.AnimControl._bowId))
                        {
                            gameObject.SendMessage("setCraftedBowDamage", SendMessageOptions.DontRequireReceiver);
                        }
                    }
                    inventoryItemView._held.SendMessage("OnAmmoFired", gameObject, SendMessageOptions.DontRequireReceiver);
                }
                if (itemCache._attackReleaseSFX != 0)
                {
                    LocalPlayer.Sfx.SendMessage(itemCache._attackReleaseSFX.ToString(), SendMessageOptions.DontRequireReceiver);
                }
                Mood.HitRumble();
            }
            else
            {
                flag2 = true;
                if (itemCache._dryFireSFX != 0)
                {
                    LocalPlayer.Sfx.SendMessage(itemCache._dryFireSFX.ToString(), SendMessageOptions.DontRequireReceiver);
                }
            }
            if (flag)
            {
                UnequipItemAtSlot(itemCache._equipmentSlot, false, false, flag);
            }
            else
            {
                ToggleAmmo(inventoryItemView, true);
            }
            if (UpgradePointsMod.instance.specialUpgrades[34].bought)
            {
                if (flag || RemoveItem(itemCache._ammoItemId, 1, false, true))
                {
                    if (UpgradePointsMod.instance.specialUpgrades[43].bought)
                    {
                        float r = Random.value;
                        if (r <= 0.30f)
                        {
                            AddItem(itemCache._ammoItemId, 1);
                        }
                    }
                    InventoryItemView inventoryItemView2 = _inventoryItemViewsCache[itemCache._ammoItemId][0];
                    Item itemCache2 = inventoryItemView2.ItemCache;
                    FakeParent component = inventoryItemView2._held.GetComponent<FakeParent>();
                    if (UseAltWorldPrefab)
                    {
                        Debug.Log("Firing " + itemCache._name + " with '" + inventoryItemView.ActiveBonus + "' ammo (alt=" + UseAltWorldPrefab + ")");
                    }
                    GameObject gameObject = (!(bool)component || component.gameObject.activeSelf) ? UnityEngine.Object.Instantiate(itemCache2._ammoPrefabs.GetPrefabForBonus(inventoryItemView.ActiveBonus, true).gameObject, inventoryItemView2._held.transform.position + Vector3.up/3, inventoryItemView2._held.transform.rotation) : UnityEngine.Object.Instantiate(itemCache2._ammoPrefabs.GetPrefabForBonus(inventoryItemView.ActiveBonus, true).gameObject, component.RealPosition, component.RealRotation);
                    if ((bool)gameObject.GetComponent<Rigidbody>())
                    {
                        if (itemCache.MatchRangedStyle(Item.RangedStyle.Shoot))
                        {
                            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.TransformDirection(Vector3.forward * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange), ForceMode.VelocityChange);
                        }
                        else
                        {
                            float num = Time.time - _weaponChargeStartTime;
                            if (ForestVR.Enabled)
                            {
                                gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * (float)(int)itemCache._projectileThrowForceRange);
                            }
                            else
                            {
                                if (UpgradePointsMod.instance != null)
                                {
                                    if (UpgradePointsMod.instance.specialUpgrades[31].bought)
                                    {
                                        if (UpgradePointsMod.instance.specialUpgrades[75].bought)
                                        {
                                            if (UpgradePointsMod.instance.specialUpgrades[76].bought)
                                            {
                                                if (UpgradePointsMod.instance.specialUpgrades[77].bought)
                                                {
                                                    gameObject.GetComponent<Rigidbody>().AddForce(3.5f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                                }
                                                else
                                                {
                                                    gameObject.GetComponent<Rigidbody>().AddForce(3f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                                }
                                            }
                                            else
                                            {
                                                gameObject.GetComponent<Rigidbody>().AddForce(2.5f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                            }
                                        }
                                        else
                                        {
                                            gameObject.GetComponent<Rigidbody>().AddForce(2f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                        }
                                    }
                                    else
                                    {
                                        gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                    }
                                }
                                else
                                {
                                    gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                }
                            }
                            if (LocalPlayer.Inventory.HasInSlot(Item.EquipmentSlot.RightHand, LocalPlayer.AnimControl._bowId))
                            {
                                gameObject.SendMessage("setCraftedBowDamage", SendMessageOptions.DontRequireReceiver);
                            }
                        }
                        inventoryItemView._held.SendMessage("OnAmmoFired", gameObject, SendMessageOptions.DontRequireReceiver);
                    }
                    if (itemCache._attackReleaseSFX != 0)
                    {
                        LocalPlayer.Sfx.SendMessage(itemCache._attackReleaseSFX.ToString(), SendMessageOptions.DontRequireReceiver);
                    }
                    Mood.HitRumble();

                }
                else
                {
                    flag2 = true;
                    if (itemCache._dryFireSFX != 0)
                    {
                        LocalPlayer.Sfx.SendMessage(itemCache._dryFireSFX.ToString(), SendMessageOptions.DontRequireReceiver);
                    }
                }
                if (flag)
                {
                    UnequipItemAtSlot(itemCache._equipmentSlot, false, false, flag);
                }
                else
                {
                    ToggleAmmo(inventoryItemView, true);
                }
            }
            if (UpgradePointsMod.instance.specialUpgrades[39].bought)
            {
                if (flag || RemoveItem(itemCache._ammoItemId, 1, false, true))
                {
                    if (UpgradePointsMod.instance.specialUpgrades[43].bought)
                    {
                        float r = Random.value;
                        if(r <= 0.30f)
                        {
                            AddItem(itemCache._ammoItemId, 1);
                        }
                    }
                    InventoryItemView inventoryItemView2 = _inventoryItemViewsCache[itemCache._ammoItemId][0];
                    Item itemCache2 = inventoryItemView2.ItemCache;
                    FakeParent component = inventoryItemView2._held.GetComponent<FakeParent>();
                    if (UseAltWorldPrefab)
                    {
                        Debug.Log("Firing " + itemCache._name + " with '" + inventoryItemView.ActiveBonus + "' ammo (alt=" + UseAltWorldPrefab + ")");
                    }
                    GameObject gameObject = (!(bool)component || component.gameObject.activeSelf) ? UnityEngine.Object.Instantiate(itemCache2._ammoPrefabs.GetPrefabForBonus(inventoryItemView.ActiveBonus, true).gameObject, inventoryItemView2._held.transform.position - inventoryItemView2._held.transform.right / 3, inventoryItemView2._held.transform.rotation) : UnityEngine.Object.Instantiate(itemCache2._ammoPrefabs.GetPrefabForBonus(inventoryItemView.ActiveBonus, true).gameObject, component.RealPosition, component.RealRotation);
                    if ((bool)gameObject.GetComponent<Rigidbody>())
                    {
                        if (itemCache.MatchRangedStyle(Item.RangedStyle.Shoot))
                        {
                            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.TransformDirection(Vector3.forward * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange), ForceMode.VelocityChange);
                        }
                        else
                        {
                            float num = Time.time - _weaponChargeStartTime;
                            if (ForestVR.Enabled)
                            {
                                gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * (float)(int)itemCache._projectileThrowForceRange);
                            }
                            else
                            {
                                if (UpgradePointsMod.instance != null)
                                {
                                    if (UpgradePointsMod.instance.specialUpgrades[31].bought)
                                    {
                                        if (UpgradePointsMod.instance.specialUpgrades[75].bought)
                                        {
                                            if (UpgradePointsMod.instance.specialUpgrades[76].bought)
                                            {
                                                if (UpgradePointsMod.instance.specialUpgrades[77].bought)
                                                {
                                                    gameObject.GetComponent<Rigidbody>().AddForce(3.5f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                                }
                                                else
                                                {
                                                    gameObject.GetComponent<Rigidbody>().AddForce(3f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                                }
                                            }
                                            else
                                            {
                                                gameObject.GetComponent<Rigidbody>().AddForce(2.5f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                            }
                                        }
                                        else
                                        {
                                            gameObject.GetComponent<Rigidbody>().AddForce(2f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                        }
                                    }
                                    else
                                    {
                                        gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                    }
                                }
                                else
                                {
                                    gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                }

                            }
                            if (LocalPlayer.Inventory.HasInSlot(Item.EquipmentSlot.RightHand, LocalPlayer.AnimControl._bowId))
                            {
                                gameObject.SendMessage("setCraftedBowDamage", SendMessageOptions.DontRequireReceiver);
                            }
                        }
                        inventoryItemView._held.SendMessage("OnAmmoFired", gameObject, SendMessageOptions.DontRequireReceiver);
                    }
                    if (itemCache._attackReleaseSFX != 0)
                    {
                        LocalPlayer.Sfx.SendMessage(itemCache._attackReleaseSFX.ToString(), SendMessageOptions.DontRequireReceiver);
                    }
                    Mood.HitRumble();

                }
                else
                {
                    flag2 = true;
                    if (itemCache._dryFireSFX != 0)
                    {
                        LocalPlayer.Sfx.SendMessage(itemCache._dryFireSFX.ToString(), SendMessageOptions.DontRequireReceiver);
                    }
                }
                if (flag)
                {
                    UnequipItemAtSlot(itemCache._equipmentSlot, false, false, flag);
                }
                else
                {
                    ToggleAmmo(inventoryItemView, true);
                }
                if (flag || RemoveItem(itemCache._ammoItemId, 1, false, true))
                {
                    if (UpgradePointsMod.instance.specialUpgrades[43].bought)
                    {
                        float r = Random.value;
                        if (r <= 0.30f)
                        {
                            AddItem(itemCache._ammoItemId, 1);
                        }
                    }
                    InventoryItemView inventoryItemView2 = _inventoryItemViewsCache[itemCache._ammoItemId][0];
                    Item itemCache2 = inventoryItemView2.ItemCache;
                    FakeParent component = inventoryItemView2._held.GetComponent<FakeParent>();
                    if (UseAltWorldPrefab)
                    {
                        Debug.Log("Firing " + itemCache._name + " with '" + inventoryItemView.ActiveBonus + "' ammo (alt=" + UseAltWorldPrefab + ")");
                    }
                    GameObject gameObject = (!(bool)component || component.gameObject.activeSelf) ? UnityEngine.Object.Instantiate(itemCache2._ammoPrefabs.GetPrefabForBonus(inventoryItemView.ActiveBonus, true).gameObject, inventoryItemView2._held.transform.position + inventoryItemView2._held.transform.right/3, inventoryItemView2._held.transform.rotation) : UnityEngine.Object.Instantiate(itemCache2._ammoPrefabs.GetPrefabForBonus(inventoryItemView.ActiveBonus, true).gameObject, component.RealPosition, component.RealRotation);
                    if ((bool)gameObject.GetComponent<Rigidbody>())
                    {
                        if (itemCache.MatchRangedStyle(Item.RangedStyle.Shoot))
                        {
                            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.TransformDirection(Vector3.forward * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange), ForceMode.VelocityChange);
                        }
                        else
                        {
                            float num = Time.time - _weaponChargeStartTime;
                            if (ForestVR.Enabled)
                            {
                                gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * (float)(int)itemCache._projectileThrowForceRange);
                            }
                            else
                            {

                                if (UpgradePointsMod.instance != null)
                                {
                                    if (UpgradePointsMod.instance.specialUpgrades[31].bought)
                                    {
                                        if (UpgradePointsMod.instance.specialUpgrades[75].bought)
                                        {
                                            if (UpgradePointsMod.instance.specialUpgrades[76].bought)
                                            {
                                                if (UpgradePointsMod.instance.specialUpgrades[77].bought)
                                                {
                                                    gameObject.GetComponent<Rigidbody>().AddForce(3.5f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                                }
                                                else
                                                {
                                                    gameObject.GetComponent<Rigidbody>().AddForce(3f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                                }
                                            }
                                            else
                                            {
                                                gameObject.GetComponent<Rigidbody>().AddForce(2.5f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                            }
                                        }
                                        else
                                        {
                                            gameObject.GetComponent<Rigidbody>().AddForce(2f * inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);
                                        }
                                    }
                                    else
                                    {
                                        gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                    }
                                }
                                else
                                {
                                    gameObject.GetComponent<Rigidbody>().AddForce(inventoryItemView2._held.transform.up * Mathf.Clamp01(num / itemCache._projectileMaxChargeDuration) * (0.016666f / Time.fixedDeltaTime) * (float)(int)itemCache._projectileThrowForceRange);

                                }

                            }
                            if (LocalPlayer.Inventory.HasInSlot(Item.EquipmentSlot.RightHand, LocalPlayer.AnimControl._bowId))
                            {
                                gameObject.SendMessage("setCraftedBowDamage", SendMessageOptions.DontRequireReceiver);
                            }
                        }
                        inventoryItemView._held.SendMessage("OnAmmoFired", gameObject, SendMessageOptions.DontRequireReceiver);
                    }
                    if (itemCache._attackReleaseSFX != 0)
                    {
                        LocalPlayer.Sfx.SendMessage(itemCache._attackReleaseSFX.ToString(), SendMessageOptions.DontRequireReceiver);
                    }
                    Mood.HitRumble();

                }
                else
                {
                    flag2 = true;
                    if (itemCache._dryFireSFX != 0)
                    {
                        LocalPlayer.Sfx.SendMessage(itemCache._dryFireSFX.ToString(), SendMessageOptions.DontRequireReceiver);
                    }
                }
                if (flag)
                {
                    UnequipItemAtSlot(itemCache._equipmentSlot, false, false, flag);
                }
                else
                {
                    ToggleAmmo(inventoryItemView, true);
                }

            }
            _weaponChargeStartTime = 0f;
            SetReloadDelay((!flag2) ? itemCache._reloadDuration : itemCache._dryFireReloadDuration);
            _isThrowing = false;

        }
      
        
        
    }
}
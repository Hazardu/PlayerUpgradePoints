using TheForest.Items;
using TheForest.Items.Utils;
using TheForest.Utils;
using UnityEngine;

namespace PlayerUpgradePoints
{
    public class PlayerStatsMod : PlayerStats
    {

        public static float startfuel;
        public static float startsprayfuel;
        public static float startMaxLungCap;
        
        protected override void Start()
        {
            base.Start();
            startfuel = Fuel.MaxFuelCapacity;
            startsprayfuel = hairSprayFuel.MaxFuelCapacity;
            startMaxLungCap = AirBreathing.MaxLungAirCapacity;
            

        }
        public override void hitFromEnemy(int getDamage)
        {
            if (UpgradePointsMod.instance.specialUpgrades[55].bought)
            {
                UpgradePointsMod.instance.RevengeDamage += getDamage / 10;
            }

            if (UpgradePointsMod.instance.specialUpgrades[12].bought)
            {
                int r = Random.Range(0, 100);
                if (r < 30)
                {
                    return;
                }
            }
            float a = getDamage;
            a = a * UpgradePointsMod.instance.DamageReduction;
           
            a *= BrawlerUpgrade.DamageReduction + 1;
            if (CharacterControllerMod.FloatVelocity < 0.1f)
            {
                if (UpgradePointsMod.instance.specialUpgrades[96].bought)
                {
                    if (UpgradePointsMod.instance.specialUpgrades[97].bought)
                    {
                        if (UpgradePointsMod.instance.specialUpgrades[98].bought)
                        {
                            a *= 0.4f;
                        }
                        else
                        {
                            a *= 0.5f;
                        }
                    }
                    else
                    {
                        a *= 0.7f;
                    }
                    
                }
            }
            if (UpgradePointsMod.instance.specialUpgrades[100].bought)
            {
                float HpMissing = 100 - Health;
                a *= 0.0095f * HpMissing;
            }
            int I = Mathf.RoundToInt(a);
             base.hitFromEnemy(I);
        }

        public override void AddArmorVisible(ArmorTypes type)
        {
            int armorSetIndex = GetArmorSetIndex(type);
            ArmorSet armorSet = (armorSetIndex == -1) ? null : ArmorSets[armorSetIndex];
            if (type == ArmorTypes.Warmsuit)
            {
                for (int i = 0; i < 10; i++)
                {
                    int num = i;
                    if (CurrentArmorTypes[num] != 0)
                    {
                        ArmorTypes type2 = CurrentArmorTypes[num];
                        int armorSetIndex2 = GetArmorSetIndex(type2);
                        ArmorSet armorSet2 = ArmorSets[armorSetIndex2];
                        ToggleArmorPiece(armorSet2.ModelType, armorSet2.Mat, num, false);
                        ToggleArmorPiece(armorSet2.ModelType2, armorSet2.Mat2, num, false);
                        if (UpgradePointsMod.instance.specialUpgrades[5].bought)
                        {
                            for (int x = 0; x < armorSet.Effects.Length; x++)
                            {
                                if (armorSet.Effects[x]._type == StatEffect.Types.Armor)
                                {
                                    armorSet.Effects[x]._amount *= 2;
                                }
                            }
                        }
                        ItemUtils.ApplyEffectsToStats(armorSet2.Effects, false, 1);
                        if (armorSet2.HP - CurrentArmorHP[num] < 4 && !Player.AddItem(armorSet2.ItemId, 1, false, false, null))
                        {
                            LocalPlayer.Inventory.FakeDrop(armorSet2.ItemId, null);
                        }
                        CurrentArmorTypes[num] = ArmorTypes.None;
                    }
                }
                ArmorVis = 0;
                CurrentArmorTypes[0] = ArmorTypes.Warmsuit;
                GetArmorPiece(ArmorTypes.Warmsuit, 0).SetActive(true);
                if (UpgradePointsMod.instance.specialUpgrades[5].bought)
                {
                    for (int x = 0; x < armorSet.Effects.Length; x++)
                    {
                        if (armorSet.Effects[x]._type == StatEffect.Types.Armor)
                        {
                            armorSet.Effects[x]._amount *= 2;
                        }
                    }
                }
                ItemUtils.ApplyEffectsToStats(armorSet.Effects, true, 1);
                UpdateArmorNibbles();
                LocalPlayer.Clothing.RefreshVisibleClothing();
            }
            else
            {
                if (!Warmsuit)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        int num2 = (int)Mathf.Repeat((float)(ArmorVis + j), 10f);
                        if (CurrentArmorTypes[num2] == ArmorTypes.None)
                        {
                            CurrentArmorTypes[num2] = type;
                            if (armorSet != null)
                            {
                                ToggleArmorPiece(armorSet.ModelType, armorSet.Mat, num2, true);
                                ToggleArmorPiece(armorSet.ModelType2, armorSet.Mat2, num2, true);
                                CurrentArmorHP[num2] = armorSet.HP;
                                ArmorVis = num2 + 1;
                                if (UpgradePointsMod.instance.specialUpgrades[5].bought)
                                {
                                    for (int x = 0; x < armorSet.Effects.Length; x++)
                                    {
                                        if (armorSet.Effects[x]._type == StatEffect.Types.Armor)
                                        {
                                            armorSet.Effects[x]._amount *= 2;
                                        }
                                    }
                                }
                                ItemUtils.ApplyEffectsToStats(armorSet.Effects, true, 1);
                            }
                            UpdateArmorNibbles();
                            return;
                        }
                    }
                }
                if (ArmorVis == 10)
                {
                    ArmorVis = 0;
                }
                if (CurrentArmorTypes[ArmorVis] != 0)
                {
                    ArmorTypes type3 = CurrentArmorTypes[ArmorVis];
                    int armorSetIndex3 = GetArmorSetIndex(type3);
                    ArmorSet armorSet3 = ArmorSets[armorSetIndex3];
                    ToggleArmorPiece(armorSet3.ModelType, armorSet3.Mat, ArmorVis, false);
                    ToggleArmorPiece(armorSet3.ModelType2, armorSet3.Mat2, ArmorVis, false);
                    if (UpgradePointsMod.instance.specialUpgrades[5].bought)
                    {
                        for (int x = 0; x < armorSet.Effects.Length; x++)
                        {
                            if (armorSet.Effects[x]._type == StatEffect.Types.Armor)
                            {
                                armorSet.Effects[x]._amount *= 2;
                            }
                        }
                    }
                    ItemUtils.ApplyEffectsToStats(armorSet3.Effects, false, 1);
                    if (armorSet3.HP - CurrentArmorHP[ArmorVis] < 4 && !Warmsuit && !Player.AddItem(armorSet3.ItemId, 1, false, false, null))
                    {
                        LocalPlayer.Inventory.FakeDrop(armorSet3.ItemId, null);
                    }
                    if (Warmsuit)
                    {
                        CurrentArmorTypes[ArmorVis] = ArmorTypes.None;
                        LocalPlayer.Inventory.UnequipItemAtSlot(Item.EquipmentSlot.FullBody, false, true, false);
                        LocalPlayer.Clothing.RefreshVisibleClothing();
                    }
                }
                CurrentArmorHP[ArmorVis] = (armorSet?.HP ?? 0);
                CurrentArmorTypes[ArmorVis] = type;
                if (armorSet != null)
                {
                    ToggleArmorPiece(armorSet.ModelType, armorSet.Mat, ArmorVis, true);
                    ToggleArmorPiece(armorSet.ModelType2, armorSet.Mat2, ArmorVis, true);
                    if (UpgradePointsMod.instance.specialUpgrades[5].bought)
                    {
                        for (int x = 0; x < armorSet.Effects.Length; x++)
                        {
                            if (armorSet.Effects[x]._type == StatEffect.Types.Armor)
                            {
                                armorSet.Effects[x]._amount *= 2;
                            }
                        }
                    }
                    ItemUtils.ApplyEffectsToStats(armorSet.Effects, true, 1);
                    ArmorVis++;
                }
                UpdateArmorNibbles();
            }
        }

        public override int HitArmor(int damage)
        {
            int a = damage;
                if (UpgradePointsMod.instance.specialUpgrades[5].bought)
                {
                    a =Mathf.RoundToInt(a *0.5f);
                }
           
            return base.HitArmor(a);
        }
        protected override void Update()
        {
            base.Update();
            if (UpgradePointsMod.instance != null)
            {
                if (UpgradePointsMod.instance.hpregen > 0)
                {
                    base.Health = Mathf.Clamp(Health + Time.deltaTime * UpgradePointsMod.instance.hpregen, 0, 100);
                    base.HealthTarget = Mathf.Clamp(HealthTarget + Time.deltaTime * UpgradePointsMod.instance.hpregen/2, 0, 100);
                }
                if (UpgradePointsMod.instance.staminaregen > 0)
                {
                    //base.Energy = Mathf.Clamp(Energy + Time.deltaTime * UpgradePointsMod.instance.staminaregen, 0, 100);
                    base.Stamina = Mathf.Clamp(Stamina + Time.deltaTime * UpgradePointsMod.instance.staminaregen, 0, 100);
                }
                if (UpgradePointsMod.instance.chainsawfuel > 1)
                {
                    base.Fuel.MaxFuelCapacity = startfuel * UpgradePointsMod.instance.chainsawfuel;
                }if (UpgradePointsMod.instance.flamefuel > 1)
                {
                    base.hairSprayFuel.MaxFuelCapacity = startsprayfuel * UpgradePointsMod.instance.flamefuel;
                }
                base.AirBreathing.MaxLungAirCapacity = startMaxLungCap * UpgradePointsMod.instance.Diving;
            }
        }

        public static float DrainedStamina;

        public override void setStamina(float val)
        {

            if (val < 0)
            {
                val *= UpgradePointsMod.instance.staminaOnAttack;
                DrainedStamina = -val;
            }
            base.setStamina(val);
        }
        
        public override void HitShark(int damage)
        {
            if (damage < 0)
            {
                if (GameSetup.IsMpClient)
                {
                    if(damage <= -1000000000)
                    {
                        int d = damage + 1000000000;
                        UpgradePointsMod.instance.AddXP(-d, false);

                    }
                    else
                    {
                     UpgradePointsMod.instance.AddXP(-damage, true);
                    ModAPI.Console.Write("Recieving " + -damage + " exp");

                    }
                }
            }
            else
            {
                base.HitShark(damage);
            }
        }
        protected override void CheckDeath()
        {
            if (!Cheats.GodMode && Health <= 0f && !Dead)
            {
                if(UpgradePointsMod.instance.specialUpgrades[46].bought)
                {
                    if (UpgradePointsMod.instance.SecondChanceCooldown <= 0)
                    {
                        Health = 100;
                        Stamina = 100;
                        Energy = 100;
                        HealthTarget = 100;
                        UpgradePointsMod.instance.SecondChanceCooldown = 3600;
                        return;
                    }
                }

                if (LocalPlayer.AnimControl.swimming)
                {
                    DeathInWater(0);
                }
                else
                {
                    Dead = true;
                    Player.enabled = false;
                    FallDownDead();
                }
            }
        }
    }
}


    


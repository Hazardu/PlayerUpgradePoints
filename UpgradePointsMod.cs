using System;
using UnityEngine;
using TheForest.Utils;
using TheForest.Save;
using System.IO;
using ModAPI;
using TheForest.Utils.Settings;
using System.Collections.Generic;
using System.Collections;
using TheForest.Items;
using TheForest.UI.Multiplayer;
using Bolt;
using UnityEngine.SceneManagement;
using ModAPI.Attributes;
namespace PlayerUpgradePoints
{
    [Serializable]
    public class UpgradePointsMod : MonoBehaviour
    {
        public int Exp;
        public int TargetExp;
        public int Level;
        public int PointsToSpend;
        public int SpecialPoints;
        public int RefundPoints;

        public float staminaOnAttack = 1;
        public float sprintspeed = 1;
        public float sprintstaminause = 1;
        public float jumpheight = 1;
        public float falldamage = 1;
        public float hpregen = 0;
        public float flamefuel = 1;
        public float chainsawfuel = 1;
        public float attackspeed = 1;
        public float weaponDmg = 1;
        public float staminaregen = 0;
        public float ArrowDmg = 1;
        public float SecondChanceCooldown = 1;
        public float Diving = 1;
        public float DamageReduction = 1;
        public float Block = 1;
        public float AreaDamage = 0;


        public int FocusFireStacks;
        public GameObject FocusFireTarget;

        public static UpgradePointsMod instance;
        public bool Respawn = false;

        private bool canSpend;

        private bool showtut;
        private int tutstatus;
        private Texture2D BarFillTex;
        public float AreaDamageRadius;
        private float RR;    //rect ratio
        private int DescID = -1;
        [NonSerialized]
        private Vector2 specialoffset;
        [Serializable]
        public struct SpecialUpgrade
        {
            public bool bought;
            public bool enabled;
            [NonSerialized]
            public string name;
            [NonSerialized]
            public string desc;
            [NonSerialized]
            public Vector2 pos;
            [NonSerialized]
            public int[] children;
        }

        [SerializeThis]
        public SpecialUpgrade[] specialUpgrades;
        private int SpecialSize = 105;



        Rect MassacreRect;
        Rect KillCountRect;
        GUIStyle MassacreStyle;



        public void ResetSpecialData()
        {
            specialUpgrades = new SpecialUpgrade[SpecialSize];
            for (int x = 0; x < specialUpgrades.Length; x++)
            {
                specialUpgrades[x].bought = false;
                specialUpgrades[x].enabled = false;
            }
        }

       
        


        void Start()
        {

            DescID = -1;
            if (instance == (UnityEngine.Object)null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
            new GameObject("Quest Listener").AddComponent<QuestListener>();
            RR = (float) Screen.height / 1080;
            Quests.quests_active = new List<Quest>();
            Quests.quests_completed = new List<Quest>();
            Quest item = new Quest
            {
                objectives = new List<QuestObjective>
            {
                new QuestObjective()
            }
            };
            Quests.quests_active.Add(item);
            Quest item2 = new Quest
            {
                objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    _target = 25f,
                    _current = 10f,
                    _name = "Some objective name",
                    _description = "Hey, do this and that :)"
                },
                new QuestObjective
                {
                    _target = 50f,
                    _current = 49f,
                    _name = "Objective #2 ",
                    _description = "This one is almost finished"
                }
            },
                Name = "Another Quest",
                Description = "This is a different description"
            };
            Quests.quests_active.Add(item2);
            Quest item3 = new Quest
            {
                objectives = new List<QuestObjective>
            {
                new QuestObjective()
            },
                Name = "Some finished quest",
                Finished = true
            };
            Quests.quests_completed.Add(item3);
            canSpend = true;
            ModGui.GetImages();
            BarFillTex = new Texture2D(1, 1);
            BarFillTex.SetPixel(0, 0, Color.yellow);
            BarFillTex.Apply();
            specialoffset = Vector2.zero;
            specialUpgrades = AssignSpecialUpgrades();
            ModGui.SetDefaults();
            ModGui.MenuOpened = false;
            ModGui.NewStatsX = Screen.width + 5 * RR;
            ModGui.ShowSpecial = false;
            if (!Respawn)
            {
                UpgradePointsMod upgradePointsMod = LoadedData();
                SpecialPoints = upgradePointsMod.SpecialPoints;
                RefundPoints = upgradePointsMod.RefundPoints;
                PointsToSpend = upgradePointsMod.PointsToSpend;
                Level = upgradePointsMod.Level;
                Exp = upgradePointsMod.Exp;
                TargetExp = upgradePointsMod.TargetExp;
                staminaOnAttack = upgradePointsMod.staminaOnAttack;
                sprintspeed = upgradePointsMod.sprintspeed;
                sprintstaminause = upgradePointsMod.sprintstaminause;
                jumpheight = upgradePointsMod.jumpheight;
                hpregen = upgradePointsMod.hpregen;
                flamefuel = upgradePointsMod.flamefuel;
                falldamage = upgradePointsMod.falldamage;
                chainsawfuel = upgradePointsMod.chainsawfuel;
                attackspeed = upgradePointsMod.attackspeed;
                weaponDmg = upgradePointsMod.weaponDmg;
                staminaregen = upgradePointsMod.staminaregen;
                ArrowDmg = upgradePointsMod.ArrowDmg;
                SecondChanceCooldown = upgradePointsMod.SecondChanceCooldown;
                Diving = upgradePointsMod.Diving;
                DamageReduction = upgradePointsMod.DamageReduction;
                AreaDamage = upgradePointsMod.AreaDamage;
                Block = upgradePointsMod.Block;
                for (int i = 0; i < upgradePointsMod.specialUpgrades.Length; i++)
                {
                    specialUpgrades[i].bought = upgradePointsMod.specialUpgrades[i].bought;
                }
                if (Level > 9)
                {
                    ModGui.MenuOpened = false;
                    ModGui.ShowSpecial = false;
                    ModGui.SpecialUnlocked = true;
                }
            }
            else
            {
                ResetToVarDefaults();
                ResetSpecialData();
                Level = 1;
                PointsToSpend = 2;
                TargetExp = 500;
                Exp = 0;
                Save();
                ModGui.MenuOpened = false;
                ModGui.ShowSpecial = false;
                ModGui.SpecialUnlocked = false;
            }
            if (Level == 0)
            {
                ResetToVarDefaults();
                ResetSpecialData();
                Level = 1;
                PointsToSpend = 2;
                TargetExp = 500;
                Exp = 0;
            }
            BarSecondFillTex = new Texture2D(1, 1);
            BarSecondFillTex.SetPixel(0, 0, new Color(0.8f, 0.8f, 0));
            BarSecondFillTex.Apply();

            BarFillTex = new Texture2D(1, 1);
            BarFillTex.SetPixel(0, 0, Color.yellow);
            BarFillTex.Apply();

            GamesettingsRefresh();
            UpdateSpecialStatus();

            AutoPickupItems.instance = LocalPlayer.GameObject.AddComponent<AutoPickupItems>();
            if (instance.specialUpgrades[20].bought)
            {
                AutoPickupItems.instance.TurnOn();
            }
            DatabaseModifier.instance.Modify();

            if (GameSetup.IsNewGame)
            {
                showtut = true;
                tutstatus = 0;
            }
            ComboStrikeTargetMelee = new Dictionary<Transform, float>();
            ComboStrikeTargetRanged = new Dictionary<Transform, float>();
            StartCoroutine(CoolDownComboStrike());
            new GameObject("BrawlerChecker").AddComponent<BrawlerUpgrade>();

            if (specialUpgrades[78].bought)
            {
                CharacterControllerMod.DashForce = 3000;
            }
            else
            {
                CharacterControllerMod.DashForce = 1500;

            }
            if (specialUpgrades[79].bought)
            {
                if (specialUpgrades[84].bought)
                {
                    CharacterControllerMod.DashMaxCooldown = 5;
                }
                else
                {
                    CharacterControllerMod.DashMaxCooldown = 10;
                }
            }
            else
            {
                CharacterControllerMod.DashForce = 20;

            }
            AreaDamageRadius = 5;
            if (specialUpgrades[101].bought)
            {
                if (specialUpgrades[102].bought)
                {
                    if (specialUpgrades[103].bought)
                    {
                        AreaDamageRadius = 10;
                    }
                    else
                    {
                        AreaDamageRadius = 8;
                    }
                }
                else
                {
                    AreaDamageRadius = 7;
                }
            }
            lineTex = new Texture2D(1, 1);
            matrixBackup = GUI.matrix;

            FillAmountCurrent = 0;
            BarFillSpeed = 0;
       
          new GameObject("KillListener").AddComponent<CoopExpShare>();
     
        }

    

        public float FillAmountCurrent;
        public float BarFillSpeed;
        private Texture2D BarSecondFillTex;


        private void OnGUI()
        {
            try
            {

                if (ModGui.islvlreminder)
                {
                    if (ModGui.slvlreminder)
                    {
                        ModGui.reminderx = Screen.width - 405;
                    }
                    else
                    {
                        ModGui.reminderx = Mathf.SmoothStep(ModGui.reminderx, Screen.width + 5, 50f);
                        if (ModGui.reminderx == Screen.width + 5)
                        {
                            ModGui.islvlreminder = false;
                        }
                    }
                }
                else
                {
                    if (ModGui.slvlreminder)
                    {
                        ModGui.reminderx = Mathf.SmoothStep(ModGui.reminderx, Screen.width - 405, 30f);
                        if (ModGui.reminderx == Screen.width - 405)
                        {
                            ModGui.islvlreminder = true;
                        }
                    }
                    else
                    {
                        ModGui.reminderx = Screen.width + 5;
                    }
                }


                if (ModGui.slvlreminder || ModGui.islvlreminder)
                {
                    Rect reminder = new Rect(ModGui.reminderx * RR, 600 * RR, 400 * RR, 40 * RR);
                    GUI.Box(reminder, "You have to level up first, then upgrade... dummy.");
                }



                if (!ModGui.MenuOpened)
                {
                    specialoffset = Vector2.zero;
                    if(FillAmountCurrent < (float)Exp / TargetExp)
                    {
                        FillAmountCurrent += BarFillSpeed * Time.deltaTime;
                        BarFillSpeed = BarFillSpeed+Time.deltaTime * 0.05f;
                    }
                    else
                    {
                        FillAmountCurrent = (float)Exp / TargetExp;
                        BarFillSpeed =0;
                    }

                    DrawLevel(new Rect(Screen.width - 100 * RR, 0, 60 * RR, 60 * RR), Mathf.RoundToInt(25 * RR));
                    DrawExpBar();
                    //Gained exp
                    if (ModGui.GainedExp > 0)
                    {
                        Rect ExpGainedRect = new Rect((Screen.width - 500 * RR) / 2, 0, 500 * RR, 22 * RR);
                        ExpGainedRect.y += ExpGainedRect.height;
                        ExpGainedRect.height = 100 * RR;
                        GUIStyle GainedExpStyle = new GUIStyle(GUI.skin.label);
                        GainedExpStyle.alignment = TextAnchor.UpperCenter;
                        GainedExpStyle.fontSize = Mathf.RoundToInt(20 * RR);
                        GUI.Label(ExpGainedRect, "+" + ModGui.GainedExp, GainedExpStyle);
                    }
                }


                else if (ModGui.MenuOpened)
                {
                    //SHOW QUESTS

                    if (GameSetup.Slot.ToString() == "0")
                    {
                        if (!GameSetup.IsMpClient)
                        {
                            GUIStyle notSavedStyle = new GUIStyle(GUI.skin.label)
                            {
                                fontSize = Mathf.RoundToInt(25 * RR),
                                alignment = TextAnchor.UpperLeft,
                                wordWrap = true,
                            };
                            GUI.Label(new Rect(3 * RR, 500 * RR, 300 * RR, 1000 * RR), "GAME WILL NOT BE SAVED, CHOOSE A SLOT FIRST!", notSavedStyle);
                        }
                    }
                    if (UnityEngine.Input.GetMouseButtonUp(0))
                    {
                        canSpend = true;
                    }
                    if (GameSetup.IsSinglePlayer)
                    {
                        if (ModGui.Quest_Show)
                        {
                            DrawQuests();
                        }
                    }
                    if (ModGui.SpecialUnlocked)
                    {
                        if (ModGui.ShowSpecial)
                        {
                            Texture2D bgTex = ModGui.SpecialBG;
                            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bgTex);
                        }
                        //button for specials
                        if (CustomButton(new Rect(70 * RR, 5 * RR, 60 * RR, 60 * RR), ModGui.SpecialOFF, ModGui.SpecialHover, ModGui.SpecialON, ModGui.ShowSpecial))
                        {
                            ModGui.ShowSpecial = !ModGui.ShowSpecial;
                            ModGui.Quest_Show = false;
                        }
                        //

                        if (GUI.Button(new Rect(10 * RR, 65 * RR, 150 * RR, 50 * RR), "Refund upgrades"))
                        {
                            Refund();
                        }
                    }
                    if (GameSetup.IsSinglePlayer)
                    {
                        if (CustomButton(new Rect(5 * RR, 5 * RR, 60 * RR, 60 * RR), ModGui.QuestButton_Off, ModGui.QuestButton_Hover, ModGui.QuestButton_On, ModGui.Quest_Show))
                        {
                            ModGui.ShowSpecial = false;
                            ModGui.Quest_Show = !ModGui.Quest_Show;
                        }
                    }
                    if (!ModGui.ShowSpecial && !ModGui.Quest_Show)
                    {
                        ModGui.NewStatsX = Screen.width - 305 * RR;
                        ModGui.OldStatsY = -100 * RR;
                        ModGui.MenuYpos = 0;
                        Rect closebutton = new Rect(Screen.width - 100 * RR, 300 * RR, 100 * RR, 100 * RR);


                        ModGui.value_target -= UnityEngine.Input.GetAxis("Mouse ScrollWheel"); // sensitivity
                        ModGui.value += (ModGui.value_target - ModGui.value) * Time.deltaTime;
                        if (ModGui.value >= 360)
                        {
                            ModGui.value += -360;
                            ModGui.value_target += -360;
                        }
                        else if (ModGui.value < 0)
                        {
                            ModGui.value += 360;
                            ModGui.value_target += 360;
                        }

                        #region Upgrades
                        if (DisplayButton(0, "SPRINT SPEED", "Current sprint speed: " + sprintspeed * 100 + "% of normal", "Next level: " + (sprintspeed + 0.02f) * 100 + "% of normal", "Upgrade your movement speed when running with shift down, good for evading attacks.", 1))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    sprintspeed += 0.02f;
                                    PointsToSpend--;
                                    Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(1, "SPRINT STAMINA USAGE", "Current sprint stamina usage: " + sprintstaminause * 100 + "% of normal", "Next level: " + sprintstaminause * 0.98 * 100 + "% of normal", "You can run longer, and it will help increase athletism.", 2))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    sprintstaminause *= 0.98f;
                                    PointsToSpend--; Save();

                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(2, "SWING STAMINA USAGE", "Current swing stamina usage: " + staminaOnAttack * 100 + "% of normal", "Next level: " + staminaOnAttack * 0.97 * 100 + "% of normal", "Amount of stamina you use with every attack - basically nonstop knight.", 0))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    staminaOnAttack *= 0.97f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(3, "JUMP HEIGHT", "Current jump height: " + jumpheight * 100 + "% of normal", "Next level: " + (jumpheight + 0.04f) * 100 + "% of normal", "Helpful to reach spots that you might use tools to get to.", 3))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    jumpheight += 0.04f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(4, "FALL DAMAGE", "Current fall damage: " + falldamage * 100 + "% of normal", "Next level: " + falldamage * 0.965 * 100 + "% of normal", "Who knows, it might save your life someday...", 4))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    falldamage *= 0.965f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(5, "HEALTH REGEN", "Current HP per second: +" + hpregen + " health points", "Next level: +" + (hpregen + 0.015f) + " health points", "Tis' free medicine!", 5))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    hpregen += 0.015f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(6, "STAMINA REGEN", "Current stamina per second: +" + staminaregen + "", "Next level: +" + (staminaregen + 0.025f) + "", "Works miracles with other stamina upgrades, standalone rather decent.", 10))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    staminaregen += 0.025f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(7, "HAIRSPRAY FUEL", "Current hairspray lasts for: " + flamefuel * 30 + " seconds", "Next level: " + (flamefuel + 0.1f) * 30 + " seconds (+10%)", "Watch them all burn, but longer.", 6))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    flamefuel += 0.1f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(8, "CHAINSAW FUEL", "Current chainsaw lasts for: " + flamefuel * 120 + " seconds", "Next level: " + (flamefuel + 0.1f) * 120 + " seconds (+10%)", "WRRrRrRrrrrRrRrRRrrRrRrRRrRRRrrr!", 7))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    chainsawfuel += 0.1f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(9, "ATTACK SPEED", "Current attack speed: " + attackspeed * 100 + "% of normal", "Next level: " + (attackspeed + 0.02f) * 100 + "% of normal", "Swing whatever you're holding faster", 8))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    attackspeed += 0.02f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(10, "MELEE WEAPON DAMAGE", "Current melee weapon damage: " + weaponDmg * 100 + "% of normal", "Next level: " + (weaponDmg + 0.035) * 100 + "% of normal", "Swing whatever you're holding with more force", 9))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    weaponDmg += 0.035f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(11, "PROJECTILE DAMAGE", "Current projectile damage: " + ArrowDmg * 100 + "% of normal", "Next level: " + (ArrowDmg + 0.0425f) * 100 + "% of normal", "Affects thrown spears, arrows , bullets", 11))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    ArrowDmg += 0.0425f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }


                        if (DisplayButton(12, "DIVING", "Current: " + Diving * 100 + "% of normal", "Next level: " + (Diving + 0.1f) * 100 + "% of normal", "Stay underwater longer ", 12))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    Diving += 0.1f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(13, "DAMAGE REDUCTION", "Current damage reduction: " + (1-DamageReduction) * 100 + "%", "Next level: " + (1- DamageReduction * 0.99f) * 100 + "%", "Reduce the damage taken from cannibals", 13))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    DamageReduction *= 0.99f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(14, "BLOCK", "Current block: " + Block * 100 + "%", "Next level: " + (Block + 0.075f) * 100 + "%", "Increases blocked damage and stamina used on block", 14))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    Block += 0.06f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        if (DisplayButton(15, "AREA DAMAGE", "Current area damage: " + AreaDamage * 100 + "%", "Next level: " + (AreaDamage + 0.005f) * 100 + "%", "Has 20% chance to proc, deals damage in a radius("+AreaDamageRadius+"m). Does not deal damage to the enemy that triggered the AoE damage", 15))
                        {
                            if (PointsToSpend > 0)
                            {
                                if (canSpend)
                                {
                                    canSpend = false;
                                    AreaDamage += 0.005f;
                                    PointsToSpend--; Save();
                                }
                            }
                            else
                            {
                                CancelInvoke("TurnOffReminder");
                                Invoke("TurnOffReminder", 1);
                                ModGui.slvlreminder = true;
                            }
                        }
                        #endregion
                    }
                    else if (!ModGui.Quest_Show && ModGui.ShowSpecial)
                    {

                        UpdateSpecialStatus();
                        if (UnityEngine.Input.GetMouseButton(0))
                        {
                            Vector2 translation = new Vector2(UnityEngine.Input.GetAxis("Mouse X") * 4, -UnityEngine.Input.GetAxis("Mouse Y") * 4);
                            specialoffset += translation * RR;

                        }
                        DescID = -1;
                        mposition = new Vector2(UnityEngine.Input.mousePosition.x, Screen.height - UnityEngine.Input.mousePosition.y);

                        for (int i = 0; i < specialUpgrades.Length; i++)
                        {
                           
                           
                                Vector2[] targets = new Vector2[specialUpgrades[i].children.Length];
                                for (int a = 0; a < targets.Length; a++)
                                {
                                    targets[a] = specialUpgrades[specialUpgrades[i].children[a]].pos * RR + specialoffset;
                                }
                                DrawSpecialButton(ref specialUpgrades[i].bought, ref specialUpgrades[i].enabled, specialUpgrades[i].name, specialUpgrades[i].desc, specialUpgrades[i].pos * RR + specialoffset, targets, i);
                            
                        }
                        DrawSpecialDescription();
                    }
                  
                    Rect StatBGRect = new Rect(Screen.width - 204 * RR, 0, 204 * RR, 257 * RR);
                    GUI.DrawTexture(StatBGRect, ModGui.StatDisplayBG);
                    //rect for every line
                    Rect StatDisplay = new Rect(Screen.width - 190 * RR, 140 * RR, 180 * RR, 100 * RR);
                    //rect for level
                    DrawLevel(new Rect(Screen.width - 152 * RR, 40 * RR, 100 * RR, 100 * RR), Mathf.RoundToInt(36 * RR));

                    //style
                    GUIStyle statDisplayStyle = new GUIStyle(GUI.skin.label);
                    statDisplayStyle.alignment = TextAnchor.UpperCenter;
                    statDisplayStyle.fontSize = Mathf.RoundToInt(13 * RR);
                    Color c = GUI.color;
                    GUI.color = Color.black;
                    //displaying lines
                    GUI.Label(StatDisplay, "EXP [ " + Exp + " / " + TargetExp + " ]", statDisplayStyle);
                    StatDisplay.y += 15 * RR;
                    if (PointsToSpend > 0)
                    {
                        if (PointsToSpend == 1)
                        {
                            GUI.Label(StatDisplay, "1 Upgrade available!", statDisplayStyle);
                            StatDisplay.y += 15 * RR;
                        }
                        else
                        {
                            GUI.Label(StatDisplay, PointsToSpend + " Upgrades available!", statDisplayStyle);
                            StatDisplay.y += 15 * RR;
                        }
                    }
                    if (SpecialPoints > 0)
                    {
                        if (SpecialPoints == 1)
                        {
                            GUI.Label(StatDisplay, "1 Special available!", statDisplayStyle);
                            StatDisplay.y += 15 * RR;
                        }
                        else
                        {
                            GUI.Label(StatDisplay, SpecialPoints + " Specials available!", statDisplayStyle);
                            StatDisplay.y += 15 * RR;
                        }
                    }
                    GUI.color = c;





                }

                //MASSACRE DISPLAY
                if (!String.IsNullOrEmpty(ModGui.MasacreText))
                {
                    MassacreRect = new Rect((Screen.width / 2) + 350f * RR, 20f * RR, 250f * RR, 100f * RR);
                    KillCountRect = new Rect((Screen.width / 2) + 350f * RR, 70f * RR, 250f * RR, 100f * RR);
                    MassacreStyle = new GUIStyle(GUI.skin.label);
                    MassacreStyle.fontSize = Mathf.RoundToInt(30f * RR);
                    MassacreStyle.wordWrap = false;
                    MassacreStyle.fontStyle = FontStyle.BoldAndItalic;
                    MassacreStyle.alignment = TextAnchor.UpperCenter;

                    GUI.DrawTexture(MassacreRect, ModGui.MassacreBG);

                    Color c = GUI.color;
                    GUI.color = Color.red;
                    GUI.Label(MassacreRect, ModGui.MasacreText, MassacreStyle);
                    GUI.Label(KillCountRect, ModGui.MasacreKills + " KILLS!", MassacreStyle);
                    GUI.color = c;
                }
                DrawBuffs();
                //LEVEL UP MESSAGE
                if (ModGui.DisplayLvlUpMsg)
                {
                    GUI.DrawTexture(new Rect(Screen.width - 530 * RR, 500 * RR, 530 * RR, 300 * RR), ModGui.LevelUp);


                }
                //TUTORIAL
                if (showtut)
                {
                    DisplayTutorial();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString());
            }
        }
        void DrawLevel(Rect rect, int fontSize)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = fontSize;
            style.fontStyle = FontStyle.Bold;
            style.wordWrap = false;

            if (Level >= 1 && Level < 10)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[0]);
            }
            else if (Level >= 10 && Level < 20)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[1]);
            }
            else if (Level >= 20 && Level < 30)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[2]);
            }
            else if (Level >= 30 && Level < 40)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[3]);
            }
            else if (Level >= 40 && Level < 50)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[4]);
            }
            else if (Level >= 40 && Level < 50)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[5]);
            }
            else if (Level >= 50 && Level < 60)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[6]);
            }
            else if (Level >= 60 && Level < 70)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[7]);
            }
            else if (Level >= 70 && Level < 80)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[8]);
            }
            else if (Level >= 80 && Level < 90)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[9]);
            }
            else if (Level >= 90 && Level < 100)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[10]);
            }
            else if (Level >= 100 && Level < 120)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[11]);
            }
            else if (Level >= 120)
            {
                GUI.DrawTexture(rect, ModGui.LevelFramesArray[12]);
            }


            GUI.Label(rect, Level.ToString(), style);
        }



        void DrawSpecialDescription()
        {
            if (DescID > -1)
            {
                Rect infoRect = new Rect(0, 0, 80 * RR, 80 * RR);
                infoRect.position = specialUpgrades[DescID].pos*RR + specialoffset;
                infoRect.x += 80 * RR;
                if (infoRect.x > Screen.width - 400 * RR)
                    infoRect.x -= 400 * RR + 80 * RR;
                infoRect.y -= 110 * RR;
                infoRect.height = 300 * RR;
                infoRect.width = 400 * RR;
                GUIStyle namestyle = new GUIStyle(GUI.skin.box)
                {
                    alignment = TextAnchor.UpperCenter,
                    fontSize = Mathf.RoundToInt(22 * RR),
                    fontStyle = FontStyle.Bold
                };
                GUI.Box(infoRect, specialUpgrades[DescID].name, namestyle);
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.wordWrap = true;
               
                style.fontSize = Mathf.RoundToInt(20 * RR);
                Rect labelRect = new Rect(infoRect.x, infoRect.y + 40 * RR, 300 * RR, 360 * RR);
                GUI.Label(labelRect, specialUpgrades[DescID].desc, style);

            }
        }

        void DrawExpBar()
        {
            //exp bar
            Rect Bar = new Rect((Screen.width - 500 * RR) / 2, 0, 500 * RR, 22 * RR);
            Rect BarFill = new Rect((Screen.width - 496 * RR) / 2, 0, 496 * RR, 15 * RR);
            Rect BarSecondFill = new Rect((Screen.width - 496 * RR) / 2, 0, 496 * RR, 15 * RR);
            BarSecondFill.width *= (float)Exp / TargetExp;
            BarFill.width *= FillAmountCurrent;
            GUI.DrawTexture(Bar, ModGui.ExpBarBG);

            GUI.DrawTexture(BarSecondFill, BarSecondFillTex, ScaleMode.StretchToFill, false);

            GUI.DrawTexture(BarFill, BarFillTex, ScaleMode.StretchToFill, false);
            GUI.DrawTexture(Bar, ModGui.ExpBarFront);

        }


        private Vector2 mposition;

        void DrawSpecialButton(ref bool pucharsed, ref bool enabled, string Name, string Description, Vector2 position, Vector2[] lineTargets, int id)
        {


            Rect button = new Rect(0, 0, 80 * RR, 80 * RR);
            Texture2D displayedTexture = ModGui.FrameD;
            if (!pucharsed)
            {

                if (enabled)
                {
                    displayedTexture = ModGui.FrameE;
                    button = new Rect(0, 0, 100 * RR, 100 * RR);
                }
            }
            else
            {
                displayedTexture = ModGui.FrameB;
                button = new Rect(0, 0, 90 * RR, 90 * RR);
            }

            button.center = position;
            if(button.y < -300*RR || button.y > Screen.height+100 || button.x < -300 * RR || button.x > Screen.width+100)
            {
                return;
           }

            //Drawing lines in bg;
              for (int a = 0; a < lineTargets.Length; a++)
            {
                Color c = Color.black;
                if (!specialUpgrades[specialUpgrades[id].children[a]].bought)
                {
                    if (specialUpgrades[specialUpgrades[id].children[a]].enabled)
                    {
                        if (pucharsed)
                        {
                            c = Color.red;
                        }
                    }
                }
                else
                {
                    if (pucharsed)
                    {
                        c = Color.green;
                    }
                }
               DrawLine(lineTargets[a], position, Vector2.Distance(position, lineTargets[a]), c);

            }
            

            GUI.DrawTexture(button, displayedTexture);


            if (button.Contains(mposition))
            {
                DescID = id;

                if (!pucharsed)
                {
                    if (UnityEngine.Input.GetMouseButtonDown(0))
                    {
                        if (SpecialPoints > 0)
                        {
                            if (enabled)
                            {
                                if (canSpend)
                                {
                                    DatabaseModifier.instance.RemoveBonuses();
                                    canSpend = false;
                                    GamesettingsRefresh();
                                    pucharsed = true;
                                    SpecialPoints--;
                                    DatabaseModifier.instance.Modify();
                                    if (specialUpgrades[78].bought)
                                    {
                                        CharacterControllerMod.DashForce = 3000;
                                    }
                                    else
                                    {
                                        CharacterControllerMod.DashForce = 1500;

                                    }
                                    if (specialUpgrades[79].bought)
                                    {
                                        if (specialUpgrades[84].bought)
                                        {
                                            CharacterControllerMod.DashMaxCooldown = 5;
                                            CharacterControllerMod.DashCooldown = 5;

                                        }
                                        else
                                        {
                                            CharacterControllerMod.DashMaxCooldown = 10;
                                            CharacterControllerMod.DashCooldown = 10;

                                        }
                                    }
                                    else
                                    {
                                        CharacterControllerMod.DashMaxCooldown = 20;
                                        CharacterControllerMod.DashCooldown = 20;

                                    }
                                    if (specialUpgrades[101].bought)
                                    {
                                        if (specialUpgrades[102].bought)
                                        {
                                            if (specialUpgrades[103].bought)
                                            {
                                                AreaDamageRadius = 10;
                                            }
                                            else
                                            {
                                                AreaDamageRadius = 8;
                                            }
                                        }
                                        else
                                        {
                                            AreaDamageRadius = 7;
                                        }
                                    }
                                    else
                                    {
                                        AreaDamageRadius = 5;
                                    }
                                    Save();
                                    if (specialUpgrades[20].bought)
                                    {
                                        AutoPickupItems.instance.TurnOn();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void StartClearingRevenge()
        {
            CancelInvoke("ClearRevenge");

            Invoke("ClearRevenge", 0.4f);
        }
        private void ClearRevenge()
        {
            RevengeDamage = 0;
        }
        public void ClearLastStaminaUsed()
        {
            CancelInvoke("LastStaminaClear");

            Invoke("LastStaminaClear", 0.4f);

        }
        private void LastStaminaClear()
        {
            PlayerStatsMod.DrainedStamina = 0;

        }
        private Texture2D lineTex;
        Matrix4x4 matrixBackup;
        void DrawLine(Vector2 pointA, Vector2 pointB, float length, Color color)
        {

            Color c = GUI.color;
            
            float width = 10.0f;
            GUI.color = color;
            float angle = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * 180f / Mathf.PI;

            GUIUtility.RotateAroundPivot(angle, pointA);
            GUI.DrawTexture(new Rect(pointA.x, pointA.y, length, width), lineTex);
            GUI.matrix = matrixBackup;
            GUI.color = c;

        }

        void Update()
        {




            if (SecondChanceCooldown > 0)
            {
                SecondChanceCooldown -= Time.deltaTime;
            }
            if (ModAPI.Input.GetButtonDown("ToggleMenu"))
            {
                ModGui.MenuOpened = !ModGui.MenuOpened;
                if (ModGui.MenuOpened)
                {
                    LocalPlayer.FpCharacter.LockView(true);
                }
                else
                {
                    LocalPlayer.FpCharacter.UnLockView();
                }
            }
        }
        void DisableGainedMsg()
        {
            ModGui.ShowGainedExp = false;
            ModGui.GainedExp = 0;
            ModGui.MasacreKills = 0;
            ModGui.MasacreText = "";
        }



        void Messageoff()
        {
            ModGui.DisplayLvlUpMsg = false;
        }



        void TurnOffReminder()
        {
            ModGui.slvlreminder = false;
        }


        bool DisplayButton(float offset, string title, string currentvalue, string nextlvlvalue, string description, int ImageIndex)
        {
            Color backupcolor = GUI.color;
            float value = offset;
            value *= Mathf.PI / (8f);
            value += ModGui.value;


            float x = (float)Mathf.Cos(value);
            x *= Screen.width / 3;
            x += Screen.width / 2;
            float y = (float)Mathf.Sin(value);
            y *= Screen.height / 4;
            float scale = (float)Mathf.Sin(value);
            if (scale < 0)
            {
                scale = 0;
            }
            Rect rect = new Rect(0, 0, 200 * RR * scale, 200 * RR * scale);

            float colorvalue = scale / 2 + 0.5f;
            GUI.color = new Color(colorvalue, colorvalue, colorvalue, colorvalue);

            rect.center = new Vector2(x, y);
            Rect detailrect = new Rect(rect);
            Rect HalfRect = new Rect(rect.x + rect.width / 4, rect.y + 0.25f * rect.height, rect.width / 2, rect.height / 2);
            GUI.DrawTexture(rect, ModGui.border);

            GUI.DrawTexture(HalfRect, ModGui.NormalTextures[ImageIndex]);

            Vector2 pos = new Vector2(UnityEngine.Input.mousePosition.x, Screen.height - UnityEngine.Input.mousePosition.y);
            if (detailrect.Contains(pos))
            {
                rect.x -= 2 * RR;
                rect.y -= 2 * RR;
                rect.width += 4 * RR;
                rect.height += 4 * RR;

                GUIStyle datastyle = new GUIStyle(GUI.skin.label);
                datastyle.fontSize = Mathf.RoundToInt(14*RR);
                datastyle.wordWrap = true;
                datastyle.richText = true;


                rect.y += 200 * RR;
                rect.x -= 50 * RR;
                rect.width = 300 * RR;
                rect.height = 200 * RR;
                GUIStyle boxstyle = new GUIStyle(GUI.skin.box);
                boxstyle.fontSize = Mathf.RoundToInt(20 * RR);
                boxstyle.fontStyle = FontStyle.Bold;
                Rect guilabel = new Rect(rect.x + 3 * RR, rect.y + 30 * RR, 296 * RR, 60 * RR);
                GUI.Box(rect, "' "+title+" '", boxstyle);
                GUI.Label(guilabel, currentvalue, datastyle);
                guilabel = new Rect(rect.x + 3, rect.y + 60 * RR, 296 * RR, 60 * RR);
                GUI.Label(guilabel, nextlvlvalue, datastyle);
                guilabel = new Rect(rect.x + 3 * RR, rect.y + 120 * RR, 296 * RR, 80 * RR);
                GUI.Label(guilabel, description, datastyle);
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    GUI.color = backupcolor;

                    return true;
                }
            }


            GUI.color = backupcolor;


            return false;
        }




        public void Save()
        {
            if (GameSetup.Slot.ToString() == "0")
            {

                if (!GameSetup.IsMpClient)
                {
                    return;
                }

            }
            string filepath = GetDataPath();
            if (!Directory.Exists(filepath))
            { Directory.CreateDirectory(filepath); }
            filepath += "/PlayerUpgradePointsData.txt";
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.WriteLine(SpecialPoints.ToString());
                writer.WriteLine(RefundPoints.ToString());
                writer.WriteLine(PointsToSpend.ToString());
                writer.WriteLine(Level.ToString());
                writer.WriteLine(Exp.ToString());
                writer.WriteLine(TargetExp.ToString());
                writer.WriteLine(staminaOnAttack.ToString());
                writer.WriteLine(sprintspeed.ToString());
                writer.WriteLine(sprintstaminause.ToString());
                writer.WriteLine(jumpheight.ToString());
                writer.WriteLine(falldamage.ToString());
                writer.WriteLine(hpregen.ToString());
                writer.WriteLine(flamefuel.ToString());
                writer.WriteLine(chainsawfuel.ToString());
                writer.WriteLine(attackspeed.ToString());
                writer.WriteLine(weaponDmg.ToString());
                writer.WriteLine(staminaregen.ToString());
                writer.WriteLine(ArrowDmg.ToString());
                writer.WriteLine(SecondChanceCooldown.ToString());
                writer.WriteLine(Diving.ToString());
                writer.WriteLine(DamageReduction.ToString());
                writer.WriteLine(AreaDamage.ToString());
                writer.WriteLine(Block.ToString());
                for (int i = 0; i < specialUpgrades.Length; i++)
                {
                    if (specialUpgrades[i].bought)
                    {
                        writer.WriteLine("1");

                    }
                    else
                    {
                        writer.WriteLine("0");

                    }
                }
                writer.Close();
                File.SetAttributes(filepath, FileAttributes.Normal);

            }

        }

        public static UpgradePointsMod LoadedData()
        {
            string filepath = GetDataPath();
            UpgradePointsMod upgrademod = new UpgradePointsMod();
            upgrademod.ResetToVarDefaults();
            upgrademod.ResetSpecialData();
            upgrademod.Level = 1;
            upgrademod.PointsToSpend = 2;
            upgrademod.TargetExp = 500;
            upgrademod.Exp = 0;
            if (Directory.Exists(filepath))
            {
                filepath += "/PlayerUpgradePointsData.txt";

                if (File.Exists(filepath))
                {
                    if (!GameSetup.IsNewGame)
                    {
                        string line = "";
                        StreamReader reader = new StreamReader(filepath);
                        line = reader.ReadLine();

                        Int32.TryParse(line, out upgrademod.SpecialPoints);
                        line = reader.ReadLine();

                        Int32.TryParse(line, out upgrademod.RefundPoints);
                        line = reader.ReadLine();

                        Int32.TryParse(line, out upgrademod.PointsToSpend);
                        line = reader.ReadLine();

                        Int32.TryParse(line, out upgrademod.Level);
                        line = reader.ReadLine();

                        Int32.TryParse(line, out upgrademod.Exp);
                        line = reader.ReadLine();

                        Int32.TryParse(line, out upgrademod.TargetExp);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.staminaOnAttack);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.sprintspeed);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.sprintstaminause);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.jumpheight);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.falldamage);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.hpregen);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.flamefuel);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.chainsawfuel);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.attackspeed);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.weaponDmg);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.staminaregen);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.ArrowDmg);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.SecondChanceCooldown);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.Diving);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.DamageReduction);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.AreaDamage);
                        line = reader.ReadLine();

                        float.TryParse(line, out upgrademod.Block);
                        for (int i = 0; i < upgrademod.specialUpgrades.Length; i++)
                        {
                            line = reader.ReadLine();
                            if (line == "0")
                            {
                                upgrademod.specialUpgrades[i].bought = false;
                            }
                            else if (line == "1")
                            {
                                upgrademod.specialUpgrades[i].bought = true;

                            }
                        }
                    }
                }
            }


            return upgrademod;
        }





        static string GetDataPath()
        {
            if (GameSetup.IsMultiplayer)
            {
                if (GameSetup.IsMpClient)
                {
                    return "Mods/Multiplayer/" + PlayerSpawn.GetClientSaveFileName();
                }
                else if (GameSetup.IsMpServer)
                {
                    return "Mods/Multiplayer/" + GameSetup.Slot.ToString();
                }
            }
            return "Mods/Singleplayer/" + GameSetup.Slot.ToString();
        }



        public void ResetToVarDefaults()
        {
            staminaOnAttack = 1;
            sprintspeed = 1;
            staminaregen = 0;
            sprintstaminause = 1;
            jumpheight = 1;
            falldamage = 1;
            hpregen = 0;
            flamefuel = 1;
            chainsawfuel = 1;
            attackspeed = 1;
            weaponDmg = 1;
            ArrowDmg = 1;
            Diving = 1;
            SecondChanceCooldown = 1;
            DamageReduction = 1;
            AreaDamage = 0;
            Block = 1;

        }
        public void AddXP(int amount, bool addKill)
        {
            int am = amount;
            if (addKill)
            {
                CancelInvoke("DisableGainedMsg");
                Invoke("DisableGainedMsg", 25);
                ModGui.MasacreKills++;
                ModGui.MasacreText = "";
                if (ModGui.MasacreKills < 3)
                {
                    ModGui.MasacreText = "";

                }
                else
                {

                    if (ModGui.MasacreKills >= 3 && ModGui.MasacreKills < 5)
                    {
                        ModGui.MasacreText = "Massacre";
                        am += amount;
                    }


                    else if (ModGui.MasacreKills >= 5 && ModGui.MasacreKills < 10)
                    {
                        am += amount * 3;

                        ModGui.MasacreText = "Bloody Massacre";
                    }
                    else if (ModGui.MasacreKills >= 10 && ModGui.MasacreKills < 15)
                    {
                        am += amount * 4;

                        ModGui.MasacreText = "Slaughter!";
                    }
                    if (ModGui.MasacreKills >= 15)
                    {
                        am += amount * 5;

                        ModGui.MasacreText = "R A M P A G E";
                    }
                }
            }

            Exp += am;

            ModGui.GainedExp += am;
            Invoke("DisableGainedMsg", 15);

            while (Exp >= TargetExp)
            {
                Exp = Exp - TargetExp;
                LvlUp();
            }
            Save();
        }
        void LvlUp()
        {
            CancelInvoke("Messageoff");
            Invoke("Messageoff", 3);
            ModGui.DisplayLvlUpMsg = true;
            PointsToSpend++;
            Level++;
            TargetExp = 100 + Level * 400;
            if (Level >= 10)
            {
                if (Level % 5 == 0)
                {
                    SpecialPoints++;
                    ModGui.SpecialUnlocked = true;
                }
            }
            LocalPlayer.Stats.Health += 20;
            LocalPlayer.Stats.HealthTarget += 20;
            LocalPlayer.Stats.Energy += 20;
            LocalPlayer.Stats.Stamina += 20;
            LocalPlayer.Stats.Thirst -= 20;
            LocalPlayer.Stats.Fullness += 20;
            Quests.UpdateActiveQuests(QuestObjective.Type.Level, 1);
           // ChangeName();

        }
        void UpdateSpecialStatus()
        {
            for (int i = 0; i < specialUpgrades.Length; i++)
            {
                specialUpgrades[i].enabled = false;
            }
            for (int i = 0; i < specialUpgrades.Length; i++)
            {
                if (SpecialPoints > 0)
                {
                    if (specialUpgrades[i].bought)
                    {
                        for (int a = 0; a < specialUpgrades[i].children.Length; a++)
                        {
                            int index = specialUpgrades[i].children[a];
                            if (!specialUpgrades[index].bought)
                            {
                                specialUpgrades[index].enabled = true;
                            }
                        }
                    }
                    else
                    {
                        if (i == 1)
                        {
                            specialUpgrades[i].enabled = true;
                        }
                        else if (i == 7)
                        {
                            specialUpgrades[i].enabled = true;

                        }
                        else if (i == 13)
                        {
                            specialUpgrades[i].enabled = true;

                        }
                        else if (i == 21)
                        {
                            specialUpgrades[i].enabled = true;

                        }
                        else if (i == 28)
                        {
                            specialUpgrades[i].enabled = true;

                        }

                    }


                }
                else
                {
                    specialUpgrades[i].enabled = false;
                }
            }
        }
        void GamesettingsRefresh()
        {
            GameSettings.Survival.Refresh();
            if (specialUpgrades[9].bought)
            {
                if (specialUpgrades[11].bought)
                {
                    GameSettings.Survival.StealthRatio *= 2f;
                }
                else
                {
                    GameSettings.Survival.StealthRatio *= 4f;

                }


            }
            if (specialUpgrades[22].bought)
            {
                GameSettings.Survival.InfectionChance *= 0.5f;
            }
            if (specialUpgrades[23].bought)
            {
                GameSettings.Survival.FrostSpeedRatio *= 0.2f;
                GameSettings.Survival.DefrostSpeedRatio *= 1.8f;

            }
            if (specialUpgrades[24].bought)
            {
                GameSettings.Survival.PoisonDamageRatio *= 0.25f;
                GameSettings.Survival.PolutedWaterDamageRatio *= 0.25f;

            }
            if (specialUpgrades[25].bought)
            {
                GameSettings.Survival.ThirstRatio *= 0.5f;

            }
            if (specialUpgrades[26].bought)
            {
                if (specialUpgrades[27].bought)
                {
                    GameSettings.Survival.FireDamageRatio *= 0.25f;
                }
                else
                {
                    GameSettings.Survival.FireDamageRatio *= 0.75f;

                }
            }
        }

        void Refund()
        {
            DatabaseModifier.instance.RemoveBonuses();
            ResetToVarDefaults();
            PointsToSpend = Level + 1;
            for (int i = 0; i < specialUpgrades.Length; i++)
            {
                specialUpgrades[i].bought = false;
                specialUpgrades[i].enabled = false;
            }
            int b = Level % 5;
            SpecialPoints = Mathf.Clamp((Level - b) / 5 - 1, 0, int.MaxValue);
            UpdateSpecialStatus();
            AutoPickupItems.instance.TurnOff();

            Save();
        }

        public static float CritBonus()
        {
            float f = 1;
            float chance = 0;
            float range = UnityEngine.Random.Range(0, 100);

            if (instance.specialUpgrades[0].bought)
            {
                if (instance.specialUpgrades[4].bought)
                {
                    if (instance.specialUpgrades[56].bought)
                    {
                        if (instance.specialUpgrades[57].bought)
                        {
                            if (instance.specialUpgrades[58].bought)
                            {
                                if (instance.specialUpgrades[59].bought)
                                {
                                    if (instance.specialUpgrades[60].bought)
                                    {
                                        if (instance.specialUpgrades[61].bought)
                                        {
                                            if (instance.specialUpgrades[62].bought)
                                            {
                                                if (instance.specialUpgrades[63].bought)
                                                {
                                                    chance = 90;
                                                }
                                                else
                                                {
                                                    chance = 80;
                                                }
                                            }
                                            else
                                            {
                                                chance = 65;
                                            }
                                        }
                                        else
                                        {
                                            chance = 50;
                                        }
                                    }
                                    else
                                    {
                                        chance = 40;
                                    }
                                }
                                else
                                {
                                    chance = 30;
                                }
                            }
                            else
                            {
                                chance = 20;
                            }
                        }
                        else
                        {
                            chance = 15;
                        }
                    }
                    else
                    {
                        chance = 10;
                    }
                }
                else
                {
                    chance = 5;
                }



                if (range <= chance)
                {
                    if (instance.specialUpgrades[3].bought)
                    {
                        if (instance.specialUpgrades[64].bought)
                        {
                            if (instance.specialUpgrades[65].bought)
                            {
                                if (instance.specialUpgrades[66].bought)
                                {
                                    if (instance.specialUpgrades[67].bought)
                                    {
                                        if (instance.specialUpgrades[68].bought)
                                        {
                                            if (instance.specialUpgrades[69].bought)
                                            {
                                                if (instance.specialUpgrades[70].bought)
                                                {
                                                    if (instance.specialUpgrades[71].bought)
                                                    {
                                                        f=17.5f;
                                                    }
                                                    else
                                                    {
                                                        f = 14.25f;
                                                    }
                                                }
                                                else
                                                {
                                                    f = 12f;
                                                }
                                            }
                                            else
                                            {
                                                f = 9.5f;
                                            }
                                        }
                                        else
                                        {
                                            f = 7.5f;
                                        }
                                    }
                                    else
                                    {
                                        f = 6f;
                                    }
                                }
                                else
                                {
                                    f = 4.5f;
                                }
                            }
                            else
                            {
                                f = 3f;
                            }
                        }
                        else
                        {
                            f = 2.2f;
                        }
                    }
                    else
                    {
                        f = 1.75f;
                    }
                }
            }

            return f;
        }




        SpecialUpgrade[] AssignSpecialUpgrades()
        {
            //SpecialUpgrade[] s = new SpecialUpgrade[35];
            SpecialUpgrade[] s = new SpecialUpgrade[SpecialSize];

            int i = 0;
            s[i].name = "Critical strike";             //button name
            s[i].desc = "Have 5% chance to deal 75% more damage";             //description
            s[i].pos = new Vector2(500, 500);   // position of centre of button
            s[i].children = new int[] { 2, 3, 4 };     // enabled buttons on pucharse
            i++;
            //1
            s[i].name = "Strong strike";             //button name
            s[i].desc = "Strong Attacks deal 500% damage";             //description
            s[i].pos = new Vector2(300, 500);   // position of centre of button
            s[i].children = new int[] { 0 };     // enabled buttons on pucharse
            i++;
            //2
            s[i].name = "Stamina on hit";             //button name
            s[i].desc = "Hitting enemies grants points of stamina back";             //description
            s[i].pos = new Vector2(700, 500);   // position of centre of button
            s[i].children = new int[] { 5 };     // enabled buttons on pucharse
            i++;
            //3
            s[i].name = "Crit Damage";             //button name
            s[i].desc = "Critical hit damage increased to 120% damage";             //description
            s[i].pos = new Vector2(700, 300);   // position of centre of button
            s[i].children = new int[] { 64};     // enabled buttons on pucharse
            i++;
            //4
            s[i].name = "Crit Chance";             //button name
            s[i].desc = "Critical hits have 10% chance to proc";             //description
            s[i].pos = new Vector2(700, 700);   // position of centre of button
            s[i].children = new int[] {56 };     // enabled buttons on pucharse
            i++;
            //5
            s[i].name = "Armor";             //button name
            s[i].desc = "Get 100% more armor from equiping pieces and take 50% reduced damage if you have armor on";             //description
            s[i].pos = new Vector2(900, 500);   // position of centre of button
            s[i].children = new int[] { 6, 38 };     // enabled buttons on pucharse
            i++;
            //6
            s[i].name = "LifeSteal";             //button name
            s[i].desc = "Gain 20% of your damage as health on hit";             //description
            s[i].pos = new Vector2(1100, 500);   // position of centre of button
            s[i].children = new int[] { 35, 45, 48 };     // enabled buttons on pucharse
            i++;
            //7
            //
            //Stealth//Stealth//Stealth//Stealth//Stealth//Stealth//Stealth//Stealth//Stealth//
            //
            s[i].name = "Stealth I";             //button name
            s[i].desc = "Deal 2x damage when below 50% visibility";             //description
            s[i].pos = new Vector2(300, 1100);   // position of centre of button
            s[i].children = new int[] { 8, 9 };     // enabled buttons on pucharse
            i++;
            //8
            s[i].name = "Stealth II";             //button name
            s[i].desc = "Deal 5x damage when below 50% visibility";             //description
            s[i].pos = new Vector2(500, 900);   // position of centre of button
            s[i].children = new int[] { 10 };     // enabled buttons on pucharse
            i++;
            //9
            s[i].name = "Sneaking";             //button name
            s[i].desc = "Stealth increased by 100%";             //description
            s[i].pos = new Vector2(500, 1300);   // position of centre of button
            s[i].children = new int[] { 11 };     // enabled buttons on pucharse
            i++;
            //10
            s[i].name = "Stealth III";             //button name
            s[i].desc = "Deal 15x damage when below 50% visibility";             //description
            s[i].pos = new Vector2(700, 900);   // position of centre of button
            s[i].children = new int[] { 12 };     // enabled buttons on pucharse
            i++;
            //11
            s[i].name = "Sneaking II";             //button name
            s[i].desc = "Stealth increased by 300%";             //description
            s[i].pos = new Vector2(700, 1300);   // position of centre of button
            s[i].children = new int[] { 12 };     // enabled buttons on pucharse
            i++;
            //12
            s[i].name = "Dodge";             //button name
            s[i].desc = "30% chance to dodge attacks";             //description
            s[i].pos = new Vector2(900, 1100);   // position of centre of button
            s[i].children = new int[] { 42,100 };     // enabled buttons on pucharse
            i++;
            //13
            //
            //Carrying//Carrying//Carrying//Carrying//Carrying//Carrying//Carrying//Carrying//
            //
            s[i].name = "Big Backpack";             //button name
            s[i].desc = "Carry 40 more sticks, 20 more rocks";             //description
            s[i].pos = new Vector2(300, 1700);   // position of centre of button
            s[i].children = new int[] { 14, 15 };     // enabled buttons on pucharse
            i++;
            //14
            s[i].name = "Dynamite";             //button name
            s[i].desc = "+1m explosion radius, +20 max capacity";             //description
            s[i].pos = new Vector2(500, 1500);   // position of centre of button
            s[i].children = new int[] { 16 };     // enabled buttons on pucharse
            i++;
            //15
            s[i].name = "Ammo";             //button name
            s[i].desc = "Generate 5 arrows and 10 bullet per day";             //description
            s[i].pos = new Vector2(500, 1900);   // position of centre of button
            s[i].children = new int[] { 17 };     // enabled buttons on pucharse
            i++;
            //16
            s[i].name = "Cloth";             //button name
            s[i].desc = "Generate 25 cloth per day";             //description
            s[i].pos = new Vector2(700, 1500);   // position of centre of button
            s[i].children = new int[] { 18 };     // enabled buttons on pucharse
            i++;
            //17
            s[i].name = "Bones";             //button name
            s[i].desc = "Carry 200 more bones, 30 more bone armor, 5 more upgraded spears";             //description
            s[i].pos = new Vector2(700, 1900);   // position of centre of button
            s[i].children = new int[] { 19 };     // enabled buttons on pucharse
            i++;
            //18
            s[i].name = "Biggest Backpack";             //button name
            s[i].desc = "Carry 100 more sticks, 50 more rocks";             //description
            s[i].pos = new Vector2(900, 1500);   // position of centre of button
            s[i].children = new int[] { 20, 40 };     // enabled buttons on pucharse
            i++;
            //19
            s[i].name = "Huge Pockets";             //button name
            s[i].desc = "Carry more bombs, more food and drinks, more other crafting materials";             //description
            s[i].pos = new Vector2(900, 1900);   // position of centre of button
            s[i].children = new int[] { 20 };     // enabled buttons on pucharse
            i++;
            //20
            s[i].name = "Auto Pick Up";             //button name
            s[i].desc = "Automatically picks up some types of items around you";             //description
            s[i].pos = new Vector2(1100, 1700);   // position of centre of button
            s[i].children = new int[] { 87};     // enabled buttons on pucharse
            i++;
            //21
            //
            //Survival//Survival//Survival//Survival//Survival//Survival//Survival
            //
            s[i].name = "Berry";             //button name
            s[i].desc = "Generate 9 berries daily";             //description
            s[i].pos = new Vector2(300, 2300);   // position of centre of button
            s[i].children = new int[] { 22, 23, 37 };     // enabled buttons on pucharse
            i++;
            //22
            s[i].name = "Infection";             //button name
            s[i].desc = "50% less chance to get infected";             //description
            s[i].pos = new Vector2(500, 2100);   // position of centre of button
            s[i].children = new int[] { 24 };     // enabled buttons on pucharse
            i++;
            //23
            s[i].name = "Cold";             //button name
            s[i].desc = "Freeze slower and warm up faster by 80%";             //description
            s[i].pos = new Vector2(500, 2500);   // position of centre of button
            s[i].children = new int[] { 25 };     // enabled buttons on pucharse
            i++;
            //24
            s[i].name = "Poison";             //button name
            s[i].desc = "75% poison immunity";             //description
            s[i].pos = new Vector2(700, 2100);   // position of centre of button
            s[i].children = new int[] { 26 };     // enabled buttons on pucharse
            i++;
            //25
            s[i].name = "Thirst";             //button name
            s[i].desc = "Decrease the rate of water usage";             //description
            s[i].pos = new Vector2(700, 2500);   // position of centre of button
            s[i].children = new int[] { 26 };     // enabled buttons on pucharse
            i++;
            //26
            s[i].name = "Fire";             //button name
            s[i].desc = "25% fire resistance";             //description
            s[i].pos = new Vector2(900, 2300);   // position of centre of button
            s[i].children = new int[] { 27 };     // enabled buttons on pucharse
            i++;
            //27
            s[i].name = "Fire II";             //button name
            s[i].desc = "50% more fire resistance";             //description
            s[i].pos = new Vector2(1100, 2300);   // position of centre of button
            s[i].children = new int[] { 46 };     // enabled buttons on pucharse
            i++;
            //28
            //Archery//Archery//Archery//Archery//Archery//Archery//Archery//Archery//Archery
            //
            s[i].name = "Projectile damage I";             //button name
            s[i].desc = "Projectiles deal 100% more damage";             //description
            s[i].pos = new Vector2(300, 2900);   // position of centre of button
            s[i].children = new int[] { 29, 30, 49 };     // enabled buttons on pucharse
            i++;
            //29
            s[i].name = "Focus fire";             //button name
            s[i].desc = "Every succesive hit increases your damage up to 20 stacks, 25% for each stack, missing a target resets it";             //description
            s[i].pos = new Vector2(500, 2700);   // position of centre of button
            s[i].children = new int[] { 31,88,89 };     // enabled buttons on pucharse
            i++;
            //30
            s[i].name = "Spears";             //button name
            s[i].desc = "Thrown spears deal 250% more damage";             //description
            s[i].pos = new Vector2(500, 3100);   // position of centre of button
            s[i].children = new int[] { 32 };     // enabled buttons on pucharse
            i++;
            //31
            s[i].name = "Projectile speed";             //button name
            s[i].desc = "Projectiles fly with 150% higher velocity";             //description
            s[i].pos = new Vector2(700, 2700);   // position of centre of button
            s[i].children = new int[] { 33,75 };     // enabled buttons on pucharse
            i++;
            //32
            s[i].name = "Spears";             //button name
            s[i].desc = "Carry +15 spears and +10 upgraded spears";             //description
            s[i].pos = new Vector2(700, 3100);   // position of centre of button
            s[i].children = new int[] { 33 };     // enabled buttons on pucharse
            i++;
            //33
            s[i].name = "Ranger";             //button name
            s[i].desc = "Projectiles deal the more damage, the more distance they traveled. Damage increases by 100% for every 20 meters";             //description
            s[i].pos = new Vector2(900, 2900);   // position of centre of button
            s[i].children = new int[] { 34 };     // enabled buttons on pucharse
            i++;
            //34
            s[i].name = "Multishot";             //button name
            s[i].desc = "Fire 2 projectiles every time you use ranged attack";             //description
            s[i].pos = new Vector2(1100, 2900);   // position of centre of button
            s[i].children = new int[] { 39 };     // enabled buttons on pucharse
            i++;


            //35
            s[i].name = "Flying dragon";             //button name  -- this one is ripped of a weapons legendary affix in diablo
            s[i].desc = "Chance on hit to triple your attack for 10 seconds needs testing";             //description
            s[i].pos = new Vector2(1300, 500);   // position of centre of button
            s[i].children = new int[] { 51 };     // enabled buttons on pucharse
            i++;

            //36
            s[i].name = "Projectile damage IV";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Projectile damage increased by 1500%";             //description
            s[i].pos = new Vector2(1500, 2900);   // position of centre of button
            s[i].children = new int[] {90 };     // enabled buttons on pucharse
            i++;

            //37
            s[i].name = "Flashlight";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "100% brighter flashlight and battery is depleted 4x slower";             //description
            s[i].pos = new Vector2(500, 2300);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;

            //38
            s[i].name = "Energetic Strikes";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Base damage of your weapon is increased by the amount of stamina spent on attack";             //description
            s[i].pos = new Vector2(1100, 300);   // position of centre of button
            s[i].children = new int[] {99 };     // enabled buttons on pucharse
            i++;

            //39
            s[i].name = "Multishot II";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Shoot 2 more projectiles";             //description
            s[i].pos = new Vector2(1300, 2900);   // position of centre of button           
            s[i].children = new int[] { 36, 41, 43 };     // enabled buttons on pucharse
            i++;

            //40
            s[i].name = "Builder";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Have triple movement speed when pushing a sled, disables dropping of sled underwater and when falling from high places.";             //description
            s[i].pos = new Vector2(1100, 1500);   // position of centre of button
            s[i].children = new int[] { 104};     // enabled buttons on pucharse
            i++;

            //41
            s[i].name = "Momentum";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Base damage of your projectiles is increased by your velocity";             //description
            s[i].pos = new Vector2(1400, 3000);   // position of centre of button
            s[i].children = new int[] { 80};     // enabled buttons on pucharse
            i++;

            //42
            s[i].name = "Assasinate";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "When performing a stealth attack, reduce enemy's current health by 15%";             //description
            s[i].pos = new Vector2(1100, 1100);   // position of centre of button
            s[i].children = new int[] { 44 };     // enabled buttons on pucharse
            i++;

            //43
            s[i].name = "Infinity";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Firing a projectile has 30% chance to not consume ammunition";             //description
            s[i].pos = new Vector2(1400, 2800);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;

            //44
            s[i].name = "Tower";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Deal increased damage when standing still ";             //description
            s[i].pos = new Vector2(1300, 1100);   // position of centre of button
            s[i].children = new int[] {96 };     // enabled buttons on pucharse
            i++;

            //45
            s[i].name = "Brawler";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Have increased damage dealt and reduced damage taken for every enemy in a small radius around you. 15% increased damage dealt amd 10% damage reduction";             //description
            s[i].pos = new Vector2(1300, 300);   // position of centre of button
            s[i].children = new int[] {85 };     // enabled buttons on pucharse
            i++;

            //46
            s[i].name = "Second chance";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Upon recieving fatal damage, have your stamina and health fully restored. This effect might occur once every 1 hour";             //description
            s[i].pos = new Vector2(1300, 2300);   // position of centre of button
            s[i].children = new int[] { 47 };     // enabled buttons on pucharse
            i++;

            //47
            s[i].name = "Dash";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Quickly double tapping a movement key causes you to dash in that direction. This ability may be performed once every 20 seconds";             //descriptio
            s[i].pos = new Vector2(1500, 2300);   // position of centre of button
            s[i].children = new int[] {78,79 };     // enabled buttons on pucharse
            i++;

            //48
            s[i].name = "Combo strikes";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Upon strking an enemy with a melee weapon, leave a debuff on him that increases his damage taken using ranged weapons, upon hitting an enemy with a ranged weapon, increase his damage taken with melee attacks. Damage bonus is 300%";             //description
            s[i].pos = new Vector2(1300, 700);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;

            //49
            s[i].name = "Projectile damage II";             //button name
            s[i].desc = "Projectiles deal 200% more damage";             //description
            s[i].pos = new Vector2(400, 2900);   // position of centre of button
            s[i].children = new int[] { 50 };     // enabled buttons on pucharse
            i++;

            //50
            s[i].name = "Projectile damage III";             //button name
            s[i].desc = "Projectiles deal 700% more damage";             //description
            s[i].pos = new Vector2(500, 2900);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;

            //51
            s[i].name = "Overpower I";             //button name
            s[i].desc = "Melee Attacks deal 250% increased damage";             //description
            s[i].pos = new Vector2(1500, 500);   // position of centre of button
            s[i].children = new int[] { 52 };     // enabled buttons on pucharse
            i++;

            //52
            s[i].name = "Overpower II";             //button name
            s[i].desc = "Melee Attacks deal 500% increased damage";             //description
            s[i].pos = new Vector2(1600, 500);   // position of centre of button
            s[i].children = new int[] { 53 };     // enabled buttons on pucharse
            i++;

            //53
            s[i].name = "Overpower III";             //button name
            s[i].desc = "Melee Attacks deal 1000% increased damage";             //description
            s[i].pos = new Vector2(1700, 500);   // position of centre of button
            s[i].children = new int[] { 54 };     // enabled buttons on pucharse
            i++;

            //54
            s[i].name = "Overpower IV";             //button name
            s[i].desc = "Melee Attacks deal 1500% increased damage";             //description
            s[i].pos = new Vector2(1800, 500);   // position of centre of button
            s[i].children = new int[] { 55 ,72};     // enabled buttons on pucharse
            i++;
            //55
            s[i].name = "Revenge";             //button name
            s[i].desc = "Upon getting hit, gain stacks of revenge equal to 10% of damage taken. When performing an attack, all of the stacks are consumed and added to base weapon damage for that one attack. Stacks are added before damage reduction";             //description
            s[i].pos = new Vector2(2000, 500);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;
            //56
            int a = 1;
            s[i].name = "Crit Chance";             //button name
            s[i].desc = "Critical hits have 15% chance to proc";             //description
            s[i].pos = new Vector2(700 + a * 100, 700f + a * 17f);   // position of centre of button 
            a++;
            s[i].children = new int[] {57 };     // enabled buttons on pucharse
            i++;
            //57
            s[i].name = "Crit Chance";             //button name
            s[i].desc = "Critical hits have 20% chance to proc";             //description
            s[i].pos = new Vector2(700 + a * 100, 700f + a * 17f);   // position of centre of button 
            a++; s[i].children = new int[] {58 };     // enabled buttons on pucharse
            i++;
            //58
            s[i].name = "Crit Chance";             //button name
            s[i].desc = "Critical hits have 30% chance to proc";             //description
            s[i].pos = new Vector2(700 + a * 100, 700f + a * 17f);   // position of centre of button 
            a++; s[i].children = new int[] {59 };     // enabled buttons on pucharse
            i++;
            //59
            s[i].name = "Crit Chance";             //button name
            s[i].desc = "Critical hits have 40% chance to proc";             //description
            s[i].pos = new Vector2(700 + a * 100, 700f + a * 17f);   // position of centre of button 
            a++; s[i].children = new int[] { 60};     // enabled buttons on pucharse
            i++;
            //60
        
            s[i].name = "Crit Chance";             //button name
            s[i].desc = "Critical hits have 50% chance to proc";             //description
            s[i].pos = new Vector2(700 + a * 100, 700f + a * 17f);   // position of centre of button 
            a++; s[i].children = new int[] {61 };     // enabled buttons on pucharse
            i++;
            //61
            s[i].name = "Crit Chance";             //button name
            s[i].desc = "Critical hits have 65% chance to proc";             //description
            s[i].pos = new Vector2(700 + a * 100, 700f + a * 17f);   // position of centre of button 
            a++; s[i].children = new int[] { 62};     // enabled buttons on pucharse
            i++;
            //62
            s[i].name = "Crit Chance";             //button name
            s[i].desc = "Critical hits have 80% chance to proc";             //description
            s[i].pos = new Vector2(700 + a * 100, 700f + a * 17f);   // position of centre of button 
            a++; s[i].children = new int[] {63 };     // enabled buttons on pucharse
            i++;
            //63
            s[i].name = "Crit Chance";             //button name
            s[i].desc = "Critical hits have 90% chance to proc";             //description
            s[i].pos = new Vector2(700 + a * 100, 700f + a * 17f);   // position of centre of button 
            a++; s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;

            a = 1;
            //64
            s[i].name = "Crit Damage";             //button name
            s[i].desc = "Critical hit damage increased to 200% damage";             //description
            s[i].pos = new Vector2(700 + a* 100, 300 - a * 17f);   // position of centre of button
            a++;
            s[i].children = new int[] { 65};     // enabled buttons on pucharse
            i++;
            //65
            s[i].name = "Crit Damage";             //button name
            s[i].desc = "Critical hit damage increased to 350% damage";             //description
            s[i].pos = new Vector2(700 + a * 100, 300 - a * 17f);   // position of centre of button
            a++; s[i].children = new int[] { 66};     // enabled buttons on pucharse
            i++;
            //66
            s[i].name = "Crit Damage";             //button name
            s[i].desc = "Critical hit damage increased to 500% damage";             //description
            s[i].pos = new Vector2(700 + a * 100, 300 - a * 17f);   // position of centre of button
            a++; s[i].children = new int[] {67 };     // enabled buttons on pucharse
            i++;
            //67
            s[i].name = "Crit Damage";             //button name
            s[i].desc = "Critical hit damage increased to 650% damage";             //description
            s[i].pos = new Vector2(700 + a * 100, 300 - a * 17f);   // position of centre of button
            a++; s[i].children = new int[] { 68};     // enabled buttons on pucharse
            i++;
            //68
            s[i].name = "Crit Damage";             //button name
            s[i].desc = "Critical hit damage increased to 850% damage";             //description
            s[i].pos = new Vector2(700 + a * 100, 300 - a * 17f);   // position of centre of button
            a++; s[i].children = new int[] {69 };     // enabled buttons on pucharse
            i++;
            //69
            s[i].name = "Crit Damage";             //button name
            s[i].desc = "Critical hit damage increased to 1100% damage";             //description
            s[i].pos = new Vector2(700 + a * 100, 300 - a * 17f);   // position of centre of button
            a++; s[i].children = new int[] {70 };     // enabled buttons on pucharse
            i++;
            //70
            s[i].name = "Crit Damage";             //button name
            s[i].desc = "Critical hit damage increased to 1325% damage";             //description
            s[i].pos = new Vector2(700 + a * 100, 300 - a * 17f);   // position of centre of button
            a++; s[i].children = new int[] {71 };     // enabled buttons on pucharse
            i++;
            //71
            s[i].name = "Crit Damage";             //button name
            s[i].desc = "Critical hit damage increased to 1750% damage";             //description
            s[i].pos = new Vector2(700 + a * 100, 300 - a * 17f);   // position of centre of button
            a++; s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;




            //72
            s[i].name = "Overpower V";             //button name
            s[i].desc = "Melee Attacks deal 2000% increased damage";             //description
            s[i].pos = new Vector2(1900, 400);   // position of centre of button
            s[i].children = new int[] { 73 };     // enabled buttons on pucharse
            i++;
            //73
            s[i].name = "Overpower VI";             //button name
            s[i].desc = "Melee Attacks deal 2500% increased damage";             //description
            s[i].pos = new Vector2(2000, 300);   // position of centre of button
            s[i].children = new int[] { 74 };     // enabled buttons on pucharse
            i++;
            //74
            s[i].name = "Overpower VII";             //button name
            s[i].desc = "Melee Attacks deal 3000% increased damage";             //description
            s[i].pos = new Vector2(2100, 300);   // position of centre of button
            s[i].children = new int[] {     };     // enabled buttons on pucharse
            i++;



            //75
            s[i].name = "Projectile speed";             //button name
            s[i].desc = "Projectiles fly with 150% higher velocity";             //description
            s[i].pos = new Vector2(800, 2650);   // position of centre of button
            s[i].children = new int[] { 76 };     // enabled buttons on pucharse
            i++;
            //76
            s[i].name = "Projectile speed";             //button name
            s[i].desc = "Projectiles fly with 200% higher velocity";             //description
            s[i].pos = new Vector2(900, 2600);   // position of centre of button
            s[i].children = new int[] { 77 };     // enabled buttons on pucharse
            i++;
            //77
            s[i].name = "Projectile speed";             //button name
            s[i].desc = "Projectiles fly with 250% higher velocity";             //description
            s[i].pos = new Vector2(1000, 2600);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;

            //78
            s[i].name = "Dash Force";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Doubles force";             //description
            s[i].pos = new Vector2(1600, 2200);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;

            //79
            s[i].name = "Dash Cooldown I";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Halves cooldown";             //description
            s[i].pos = new Vector2(1600, 2400);   // position of centre of button
            s[i].children = new int[] { 84};     // enabled buttons on pucharse
            i++;


            //80
            s[i].name = "Momentum II";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Base damage of your projectiles is increased by x3 your velocity";             //description
            s[i].pos = new Vector2(1500, 3000);   // position of centre of button
            s[i].children = new int[] { 81};     // enabled buttons on pucharse
            i++;
            //81
            s[i].name = "Momentum II";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Base damage of your projectiles is increased by x7 your velocity";             //description
            s[i].pos = new Vector2(1600, 3000);   // position of centre of button
            s[i].children = new int[] { 82};     // enabled buttons on pucharse
            i++;
            //82
            s[i].name = "Momentum IV";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Base damage of your projectiles is increased by x12 your velocity";             //description
            s[i].pos = new Vector2(1700, 3000);   // position of centre of button
            s[i].children = new int[] {83 };     // enabled buttons on pucharse
            i++;
            //83
            s[i].name = "Momentum V";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Base damage of your projectiles is increased by x17 your velocity";             //description
            s[i].pos = new Vector2(1800, 3000);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;
            //84
            
            s[i].name = "Dash Cooldown II";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Halves cooldown again";             //description
            s[i].pos = new Vector2(1700, 2300);   // position of centre of button
            s[i].children = new int[] {  };     // enabled buttons on pucharse
            i++;


            //85
            s[i].name = "Brawler Damage";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Damage per enemy increased to 30%";             //description
            s[i].pos = new Vector2(1400, 300);   // position of centre of button
            s[i].children = new int[] {86 };     // enabled buttons on pucharse
            i++;
            //86
            s[i].name = "Brawler Toughness";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Damage reduction per enemy increased to 15%";             //description
            s[i].pos = new Vector2(1500, 300);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;
            //87
            s[i].name = "Auto Pick Up radius";             //button name
            s[i].desc = "Increases radius to 7 meters";             //description
            s[i].pos = new Vector2(1300, 1700);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;
            //88
            s[i].name = "Endless Focus fire";             //button name
            s[i].desc = "Increases max amount of stacks to 100";             //description
            s[i].pos = new Vector2(600, 2600);   // position of centre of button
            s[i].children = new int[] {  };     // enabled buttons on pucharse
            i++;
            //89
            s[i].name = "Focus fire damage";             //button name
            s[i].desc = "Increases damage per stack to 40%";             //description
            s[i].pos = new Vector2(600, 2800);   // position of centre of button
            s[i].children = new int[] {  };     // enabled buttons on pucharse
            i++;
            //90
            s[i].name = "Projectile damage V";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Projectile damage increased by 1900%";             //description
            s[i].pos = new Vector2(1600, 2900);   // position of centre of button
            s[i].children = new int[] { 91 };     // enabled buttons on pucharse
            i++;
            //91
            s[i].name = "Projectile damage VI";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Projectile damage increased by 2300%";             //description
            s[i].pos = new Vector2(1700, 2900);   // position of centre of button
            s[i].children = new int[] { 92};     // enabled buttons on pucharse
            i++;
            //92
            s[i].name = "Projectile damage VII";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Projectile damage increased by 2600%";             //description
            s[i].pos = new Vector2(1800, 2900);   // position of centre of button
            s[i].children = new int[] { 93 };     // enabled buttons on pucharse
            i++;
            //93
            s[i].name = "Projectile damage VIII";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Projectile damage increased by 3000%";             //description
            s[i].pos = new Vector2(1900, 2900);   // position of centre of button
            s[i].children = new int[] { 94 };     // enabled buttons on pucharse
            i++;
            //94
            s[i].name = "Projectile damage IX";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Projectile damage increased by 3250%";             //description
            s[i].pos = new Vector2(2000, 2900);   // position of centre of button
            s[i].children = new int[] { 95 };     // enabled buttons on pucharse
            i++;
            //95
            s[i].name = "Projectile damage X";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Projectile damage increased by 3500%";             //description
            s[i].pos = new Vector2(2100, 2900);   // position of centre of button
            s[i].children = new int[] {  };     // enabled buttons on pucharse
            i++;
            //96
            s[i].name = "Immovable object";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Take 30% decreased damage when standing still ";             //description
            s[i].pos = new Vector2(1400, 1200);   // position of centre of button
            s[i].children = new int[] {97 };     // enabled buttons on pucharse
            i++;
            //97
            s[i].name = "Immovable object II";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Take 50% decreased damage when standing still ";             //description
            s[i].pos = new Vector2(1500, 1200);   // position of centre of button
            s[i].children = new int[] {98 };     // enabled buttons on pucharse
            i++;
            //98
            s[i].name = "Immovable object III";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Take 60% decreased damage when standing still ";             //description
            s[i].pos = new Vector2(1600, 1200);   // position of centre of button
            s[i].children = new int[] {101 };     // enabled buttons on pucharse
            i++;
            //99
            s[i].name = "Stamina is useless anyways";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Deal 10% increased melee damage for every point of stamina missing";             //description
            s[i].pos = new Vector2(1200, 300);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;
            //100
            s[i].name = "Risky buisness";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Take 0.95% reduced damage for every point of health missing";             //description
            s[i].pos = new Vector2(1100, 1200);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;
            //101
            s[i].name = "Area damage radius I";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Increases are damage radius by 2 meters";             //description
            s[i].pos = new Vector2(1800, 1100);   // position of centre of button
            s[i].children = new int[] {102 };     // enabled buttons on pucharse
            i++;
            //102
            s[i].name = "Area damage radius II";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Increases are damage radius by 3 meters";             //description
            s[i].pos = new Vector2(1900, 1100);   // position of centre of button
            s[i].children = new int[] {103 };     // enabled buttons on pucharse
            i++;
            //103
            s[i].name = "Area damage radius III";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Increases are damage radius by 5 meters";             //description
            s[i].pos = new Vector2(2000, 1100);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;
            //104
            s[i].name = "Timber";             //button name  something like in team fortress 2 in mann vs machine for sniper
            s[i].desc = "Cutting a tree spawns twice as many logs";             //description
            s[i].pos = new Vector2(1300, 1500);   // position of centre of button
            s[i].children = new int[] { };     // enabled buttons on pucharse
            i++;
            Log.Write(i.ToString());
            return s;
        }

        public bool PushingSled;


        public void DrawBuffs()
        {
            float MaxWidth = Screen.width * 0.6f;
            float SpaceSize = 60 * RR;
            float Width = 50 * RR;
            float Height = 50 * RR;

            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.LowerRight,
                fontSize = Mathf.RoundToInt(15 * RR),
                fontStyle = FontStyle.Italic,
                wordWrap = false
            };

            Rect buffRect = new Rect(5 * RR, Screen.height - Height - 5 * RR, Width, Height);
            if (specialUpgrades[55].bought)
            {
                if (RevengeDamage > 0)
                {
                    DrawSingleBuff(buffRect, ModGui.RevengeBuff, ModGui.ActiveBuff, 1, 3);
                    GUI.Label(buffRect, Mathf.RoundToInt(RevengeDamage).ToString(), style);
                    //-------------
                    buffRect.x += SpaceSize;
                    if (buffRect.xMax > MaxWidth)
                    {
                        buffRect.x = 5 * RR;
                        buffRect.y -= SpaceSize;
                    }
                    //-------------
                }
            }
            if (specialUpgrades[46].bought)
            {
                if (SecondChanceCooldown > 0)
                {
                    DrawSingleBuff(buffRect, ModGui.SecondChanceBuff, ModGui.CooldownBuff, SecondChanceCooldown / 3600, 3);
                    //-------------
                    buffRect.x += SpaceSize;
                    if (buffRect.xMax > MaxWidth)
                    {
                        buffRect.x = 5 * RR;
                        buffRect.y -= SpaceSize;
                    }
                    //-------------
                }
            }
            if (specialUpgrades[40].bought)
            {
                if (PushingSled)
                {
                    DrawSingleBuff(buffRect, ModGui.BuilderBuff, ModGui.ActiveBuff, 1, 3);
                    //-------------
                    buffRect.x += SpaceSize;
                    if (buffRect.xMax > MaxWidth)
                    {
                        buffRect.x = 5 * RR;
                        buffRect.y -= SpaceSize;
                    }
                    //-------------
                }
            }
            if (specialUpgrades[44].bought)
            {
                if (CharacterControllerMod.FloatVelocity < 0.1f)
                {
                    DrawSingleBuff(buffRect, ModGui.TowerBuff, ModGui.ActiveBuff, 1, 3);
                    //-------------
                    buffRect.x += SpaceSize;
                    if (buffRect.xMax > MaxWidth)
                    {
                        buffRect.x = 5 * RR;
                        buffRect.y -= SpaceSize;
                    }
                    //-------------
                }
            }
            if (specialUpgrades[45].bought)
            {
                if (BrawlerUpgrade.EnemiesNearbyCount > 0)
                {
                    DrawSingleBuff(buffRect, ModGui.BrawlerBuff, ModGui.ActiveBuff, 1, 3);
                    GUI.Label(buffRect, BrawlerUpgrade.EnemiesNearbyCount.ToString(), style);

                    //-------------
                    buffRect.x += SpaceSize;
                    if (buffRect.xMax > MaxWidth)
                    {
                        buffRect.x = 5 * RR;
                        buffRect.y -= SpaceSize;
                    }
                    //-------------
                }
            }
            if (specialUpgrades[47].bought)
            {
                if (CharacterControllerMod.DashCooldown > 0)
                {
                    DrawSingleBuff(buffRect, ModGui.DashBuff, ModGui.CooldownBuff, CharacterControllerMod.DashCooldown / CharacterControllerMod.DashMaxCooldown, 3);

                    //-------------
                    buffRect.x += SpaceSize;
                    if (buffRect.xMax > MaxWidth)
                    {
                        buffRect.x = 5 * RR;
                        buffRect.y -= SpaceSize;
                    }
                    //-------------
                }
            }
            if (specialUpgrades[35].bought)
            {
                if (WeaponInfoMod.fdproc)
                {
                    DrawSingleBuff(buffRect, ModGui.FlyingDragonBuff, ModGui.ActiveBuff, 1, 3);

                    //-------------
                    buffRect.x += SpaceSize;
                    if (buffRect.xMax > MaxWidth)
                    {
                        buffRect.x = 5 * RR;
                        buffRect.y -= SpaceSize;
                    }
                    //-------------
                }
            }
            if (specialUpgrades[29].bought)
            {
                if (FocusFireStacks > 0)
                {
                    DrawSingleBuff(buffRect, ModGui.FocusFire, ModGui.ActiveBuff, 1, 3);
                    GUI.Label(buffRect, FocusFireStacks.ToString(), style);

                    //-------------
                    buffRect.x += SpaceSize;
                    if (buffRect.xMax > MaxWidth)
                    {
                        buffRect.x = 5 * RR;
                        buffRect.y -= SpaceSize;
                    }
                    //-------------
                }
            }
        }
        void DrawSingleBuff(Rect pos, Texture2D tex, Texture2D fill, float fillAmount, int filloffset)
        {
            float y = pos.y - filloffset;
            float h = pos.height + filloffset * 2;
            y += h * (1 - fillAmount);
            h *= fillAmount;
            Rect f = new Rect(pos.x - filloffset, y, pos.width + filloffset * 2, h);
            GUI.DrawTexture(f, fill);
            GUI.DrawTexture(pos, ModGui.BuffBackground);
            GUI.DrawTexture(pos, tex);
            GUI.DrawTexture(pos, ModGui.BuffFront);
        }




















        public float RevengeDamage;

        private float ComboStrikeDebuffDuration = 60f;
        private Dictionary<Transform, float> ComboStrikeTargetMelee;
        private Dictionary<Transform, float> ComboStrikeTargetRanged;
        public enum StrikeType { Melee, Ranged }


        public void AddToComboStrike(StrikeType type, Transform target)
        {
            if (type == StrikeType.Melee)
            {
                if (ComboStrikeTargetMelee.ContainsKey(target))
                {
                    ComboStrikeTargetMelee[target] = ComboStrikeDebuffDuration;
                }
                else
                {
                    ComboStrikeTargetMelee.Add(target, ComboStrikeDebuffDuration);
                }
            }
            else if (type == StrikeType.Ranged)
            {
                if (ComboStrikeTargetRanged.ContainsKey(target))
                {
                    ComboStrikeTargetRanged[target] = ComboStrikeDebuffDuration;
                }
                else
                {
                    ComboStrikeTargetRanged.Add(target, ComboStrikeDebuffDuration);
                }
            }
        }

        public IEnumerator CoolDownComboStrike()
        {
            while (true) // endless loop
            {
                foreach (KeyValuePair<Transform, float> entry in ComboStrikeTargetMelee)
                {
                    ComboStrikeTargetMelee[entry.Key] -= 1;
                    if (ComboStrikeTargetMelee[entry.Key] <= 0)
                    {
                        ComboStrikeTargetMelee.Remove(entry.Key);
                    }
                }
                foreach (KeyValuePair<Transform, float> entry in ComboStrikeTargetRanged)
                {
                    ComboStrikeTargetRanged[entry.Key] -= 1;
                    if (ComboStrikeTargetRanged[entry.Key] <= 0)
                    {
                        ComboStrikeTargetRanged.Remove(entry.Key);
                    }
                }
                yield return new WaitForSeconds(1); //pauses the execution of this coroutine for 1 second
            }
        }

        public bool ComboStrikeBonusDamage(StrikeType type, Transform target)
        {
            if (type == StrikeType.Melee)
            {
                if (ComboStrikeTargetMelee.ContainsKey(target))
                {
                    return true;
                }
            }
            else if (type == StrikeType.Ranged)
            {
                if (ComboStrikeTargetRanged.ContainsKey(target))
                {
                    return true;
                }

            }
            return false;
        }
        public void DoAreaDamage(Transform origin, float Damage)
        {
            float r = UnityEngine.Random.Range(0, 100);
            if(AreaDamage==0||r < 80)
            {
                return;
            }


            RaycastHit[] hits = Physics.SphereCastAll(origin.position, AreaDamageRadius, Vector3.one);
            int d = Mathf.RoundToInt(Damage * AreaDamage);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.root != origin.root)
                {
                    if (hits[i].transform.tag == "enemyCollide")
                    {
                        if (GameSetup.IsMpClient)
                        {
                            BoltEntity entity = hits[i].transform.GetComponent<BoltEntity>();
                            if (entity == null)
                            {
                                entity = hits[i].transform.GetComponentInParent<BoltEntity>();
                                ModAPI.Log.Write("getting entity in parrent");
                            }
                            if (entity != null)
                            {
                                PlayerHitEnemy playerHitEnemy = PlayerHitEnemy.Create(GlobalTargets.OnlyServer);
                                playerHitEnemy.Hit = d;
                                playerHitEnemy.Target = entity;
                                playerHitEnemy.Send();
                            }
                        }
                        else
                        {
                            EnemyHealthMod eh = hits[i].transform.GetComponent<EnemyHealthMod>();
                            if (eh == null)
                            {
                                eh = hits[i].transform.GetComponentInParent<EnemyHealthMod>();
                                ModAPI.Log.Write("getting in parrent");
                            }
                            if (eh != null)
                            {
                                eh.Hit(d);
                            }
                        }
                    }
                }
            }

        }
        public void DisplayTutorial()
        {
            Rect r = new Rect(0, 0, 400 * RR, Screen.height);
            r.center = new Vector2(Screen.width / 2, 0);
            r.y = 30 * RR;
            GUIStyle tutorialstyle = new GUIStyle(GUI.skin.label);
            tutorialstyle.alignment = TextAnchor.UpperCenter;
            tutorialstyle.fontStyle = FontStyle.Bold;
            tutorialstyle.fontSize = Mathf.RoundToInt(16 * RR);
            tutorialstyle.wordWrap = true;
            tutorialstyle.overflow = new RectOffset(0, 0, 0, 1000);
            tutorialstyle.margin = new RectOffset(3, 3, 3, 3);

            string tutorialtext = "";
            if (tutstatus == 0)
            {
                tutorialtext = "Welcome and thanks for downloading Player Upgrade Points! Press [SPACE] to continue or [F] to turn off this tutorial.";
                if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                {
                    tutstatus++;
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.F))
                {
                    showtut = false;
                }

            }
            else if (tutstatus == 1)
            {
                string modapiHotkey = ModAPI.Input.GetKeyBindingAsString("ToggleMenu");
                tutorialtext = "To open the menu of the mod, press [" + modapiHotkey + "].";
                if (ModGui.MenuOpened)
                {
                    tutstatus++;
                }

            }
            else if (tutstatus == 2)
            {
                tutorialtext = "Now, choose an upgrade of your choice and spend your points on it. Currently you have " + PointsToSpend + " points to assign. You can use your mouse's scroll wheel to rotate between upgrades. Spend all of your points to proceed.";

                if (PointsToSpend < 1)
                {
                    tutstatus++;
                }
            }
            else if (tutstatus == 3)
            {
                tutorialtext = "Now, find and kill some enemies to gain experience. You can also gain exp by destroying effigies left by cannibals. ";

                if (Exp > 0)
                {
                    tutstatus++;
                }

            }
            else if (tutstatus == 4)
            {
                if (Level > 9)
                {
                    tutorialtext = "After getting to level 10 you unlock special upgrades. From now on every 5 levels you will gain a special upgrade point. Open menu and press a button in top left and spend your newly gained point. Some upgrades that increase carrying capacity require you to exit the game to take effect. Additionally, at least in this version you can reassign your points for free by pressing 'refund' button that is right under 'specials' button.     Press [F] to finish this tutorial";
                    if (UnityEngine.Input.GetKeyDown(KeyCode.F))
                    {
                        showtut = false;
                    }
                }
                else
                {
                    return;
                }
            }
            GUI.Label(r, tutorialtext, tutorialstyle);
        }
        bool CustomButton(Rect rect, Texture2D off, Texture2D hover, Texture2D on, bool enabled)
        {
            Vector2 mposition = new Vector2(UnityEngine.Input.mousePosition.x, Screen.height - UnityEngine.Input.mousePosition.y);
            if (rect.Contains(mposition))
            {
                GUI.DrawTexture(rect, hover);
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    if (canSpend)
                    {
                        canSpend = false;
                        return true;

                    }
                }
            }
            else
            {
                if (enabled)
                {
                    GUI.DrawTexture(rect, on);
                }
                else
                {
                    GUI.DrawTexture(rect, off);

                }
            }
            return false;
        }

        private Quest selectedQuest;

        void DrawQuests()
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), ModGui.QuestMenu_BackGround);
            Rect ActiveQuestRect = new Rect(30 * RR, 100 * RR, 500 * RR, 80 * RR);
            Rect CompletedQuestRect = new Rect(550 * RR, 100 * RR, 500 * RR, 80 * RR);


            //Drawing all the quests
            float y = 0;
            if (ModGui.Quest_Completed)
            {
                y = 190 * RR - ModGui.Quest_Scroll;
                for (int i = 0; i < Quests.quests_completed.Count; i++)
                {

                    y += DrawQuest(Quests.quests_completed[i], new Rect(150 * RR, y * RR, 950 * RR, 200 * RR));
                }
            }
            else
            {

                y = 190 * RR - ModGui.Quest_Scroll;
                for (int i = 0; i < Quests.quests_active.Count; i++)
                {
                    y += DrawQuest(Quests.quests_active[i], new Rect(150 * RR, y * RR, 950 * RR, 200 * RR));

                }
            }
            //Drawing top and bottom borders
            GUI.DrawTexture(new Rect(0, 0, 1100 * RR, 190 * RR), ModGui.QuestMenu_FrontTop);

            //Drawing slider
            ModGui.Quest_Scroll = GUI.VerticalSlider(new Rect(0, 190 * RR, 70 * RR, Screen.height - 200 * RR), ModGui.Quest_Scroll, 0, y + 500 * RR);

            //Drawing main details
            if (selectedQuest != null)
            {
                DrawQuestDetail(selectedQuest);
            }
            if (CustomButton(ActiveQuestRect, ModGui.ActiveQuestList_Off, ModGui.ActiveQuestList_On, ModGui.ActiveQuestList_On, !ModGui.Quest_Completed))
            {
                ModGui.Quest_Completed = false;
            }
            if (CustomButton(CompletedQuestRect, ModGui.CompletedQuestList_Off, ModGui.CompletedQuestList_On, ModGui.CompletedQuestList_On, ModGui.Quest_Completed))
            {
                ModGui.Quest_Completed = true;
            }
        }
        float DrawQuest(Quest q, Rect r) // returns the height of the quest box
        {
            //rect dimensions = 950x200

            //background image
            GUI.DrawTexture(r, ModGui.QuestMenu_QuestFrame);

            //Quest title
            Rect titleRect = new Rect(r.x, r.y, r.width, 30);
            GUIStyle TitleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 30,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            Color c = GUI.color;
            GUI.color = Color.blue;
            GUI.Label(titleRect, q.Name, TitleStyle);
            GUI.color = c;
            //Quest description
            Rect descRect = new Rect(r.x, r.y + 50 * RR, r.width, 100 * RR);
            Rect descTitleRect = new Rect(r.x, r.y + 30 * RR, r.width, 100 * RR);
            GUIStyle descTitleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 25,
                fontStyle = FontStyle.Normal,
                alignment = TextAnchor.UpperCenter
            };
            GUIStyle descStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                fontStyle = FontStyle.Italic,
                alignment = TextAnchor.UpperCenter
            };
            GUI.Label(descRect, q.Description, descStyle);
            GUI.Label(descTitleRect, "Description:", descTitleStyle);

            //Drawing objectives
            float y = 150 + r.y;  //y position of the objective
            // width of objective is r.width which is 950
            // 150 pixels for label gives 800
            //finally 50 pixels of margin from left and right gives 700
            // because of margin x += 50
            //Also title above the bar
            GUIStyle OTStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.RoundToInt(24 * RR),
                fontStyle = FontStyle.BoldAndItalic,
                alignment = TextAnchor.UpperCenter
            };

            for (int i = 0; i < q.objectives.Count; i++)
            {
                //title
                GUI.DrawTexture(new Rect(r.x, y, r.width, 70 * RR), ModGui.ObjectiveBG);
                Rect Otitle = new Rect(r.x + 10 * RR, y + 5 * RR, r.width, 30 * RR);
                GUI.Label(Otitle, "Objective " + i + ":   " + q.objectives[i]._name, OTStyle);
                Rect Bar = new Rect(r.x + 50 * RR, y + 30 * RR, r.width - 250 * RR, 30 * RR);
                DrawQuestProgressBar(Bar, q.objectives[i]._current / q.objectives[i]._target);
                Rect ProgLab = new Rect(r.xMax - 150 * RR, y + 30 * RR, 150 * RR, 30 * RR);
                GUI.Label(ProgLab, (q.objectives[i]._current / q.objectives[i]._target) * 100 + "%", OTStyle);
                y += 70 * RR;

            }
            //selecting the quest to main display
            if (GUI.Button(r, "", GUI.skin.label))
            {
                selectedQuest = q;
            }
            //Dimiss button
            if (q.Finished)
            {
                if (CustomButton(new Rect(r.xMax - 30 * RR, r.y + 5 * RR, 25 * RR, 25 * RR), ModGui.ClaimButton_Off, ModGui.ClaimButton_Hover, ModGui.ClaimButton_Off, q.RewardsGiven))
                {
                    q.CollectRewards();
                }
            }
            return y - r.y + 20;
        }
        void DrawQuestDetail(Quest q)
        {
            GUILayout.BeginArea(new Rect(1100 * RR, 0, Screen.width - 1100 * RR, Screen.height));

            GUIStyle titleS = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.RoundToInt(40 * RR),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperCenter,
                margin = new RectOffset(30, 30, 15, 15)

            };
            GUIStyle descS = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.RoundToInt(30 * RR),
                fontStyle = FontStyle.Italic,
                alignment = TextAnchor.UpperLeft,
                margin = new RectOffset(30, 30, 15, 15)
            };
            GUIStyle headerS = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.RoundToInt(35 * RR),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperLeft,
                margin = new RectOffset(30, 30, 15, 15)


            };
            Color c = GUI.color;
            GUI.color = Color.black;
            GUILayout.Label("'" + q.Name + "'", titleS);
            GUILayout.Space(15 * RR);
            GUILayout.Label("Description", headerS);
            GUILayout.Label(q.Description, descS);
            GUILayout.Space(20 * RR);
            GUILayout.Label("Rewards", headerS);
            if (q.rewards._exp > 0)
            {
                GUILayout.Label("Experience:    " + q.rewards._exp, descS);
            }
            if (q.rewards._refundPoints > 0)
            {
                GUILayout.Label("Refund points:    " + q.rewards._refundPoints, descS);
            }
            if (q.rewards._items.Count > 0)
            {
                for (int i = 0; i < q.rewards._items.Count; i++)
                {
                    GUILayout.Label("Item:    '" + ItemDatabase.ItemById(q.rewards._items[i].ID)._name + "'   Quantity: " + q.rewards._items[i].Amount, descS);
                }
            }

            GUILayout.Space(40 * RR);
            GUILayout.Label("Objectives: ", headerS);
            GUILayout.Label("Completed: " + q.objectivesCompleted, descS);
            GUILayout.Label("Required: " + q.objectivesRequired, descS);
            for (int i = 0; i < q.objectives.Count; i++)
            {
                GUILayout.Label(q.objectives[i]._name, headerS);
                GUILayout.Label(q.objectives[i]._current + " / " + q.objectives[i]._target, headerS);
                Rect rt = GUILayoutUtility.GetRect(Screen.width - 1400 * RR, 50 * RR);
                GUI.color = c;

                DrawQuestProgressBar(rt, q.objectives[i]._current / q.objectives[i]._target);
                GUI.color = Color.black;

                GUILayout.Label(q.objectives[i]._description, descS);
                GUILayout.Space(30 * RR);
            }
            GUI.color = c;

            GUILayout.EndArea();
        }
        void DrawQuestProgressBar(Rect r, float amount)
        {
            Rect fill = new Rect(r);
            fill.width *= amount;
            GUI.DrawTexture(r, ModGui.ProgressBar_BG);

            GUI.DrawTexture(fill, ModGui.ProgressBar_Fill);

            GUI.DrawTexture(r, ModGui.ProgressBar_Front);

        }
       // private string PlName;
     /*   public void ChangeName()
        {
            string prefix = "( "+Level+" )";
            if(Level >= 10 && Level < 30)
            {
                prefix = "[ " + Level + " ]";
            }
            else if (Level >= 30 && Level < 50)
            {
                prefix = "{ " + Level + " }";
            }
            else if (Level >= 50 && Level < 75)
            {
                prefix = "< " + Level + " >";

            }
            else if (Level >= 75 && Level < 100)
            {
                prefix = "▬ " + Level + " ▬";

            }
            else if (Level >= 100 && Level < 120)
            {
                prefix = "♥ " + Level + " ♥";

            }
            else if (Level >= 120 )
            {
                prefix = "♦• " + Level + " •♦";

            }
            LocalPlayer.Entity.GetState<IPlayerState>().name = prefix + "   " + PlName;
            LocalPlayer.Transform.GetComponentInChildren<PlayerName>().Init(prefix + "   " + PlName);
            ModAPI.Log.Write("Player name is " + LocalPlayer.Entity.GetState<IPlayerState>().name);
        }*/
    }
}

    


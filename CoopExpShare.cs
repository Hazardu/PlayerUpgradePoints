using UnityEngine;
using TheForest.Utils;
using TheForest.Save;
using System.IO;
using Bolt;
using BoltInternal;
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
using TheForest.Items.World;
using TheForest.Networking;
using TheForest.Items;

namespace PlayerUpgradePoints
{
    public class CoopExpShare : MonoBehaviour
    {

        public static CoopExpShare instance = null;

        public List<CoopPlayerRemoteSetup> setup;

        public void Start()
        {
            if (GameSetup.IsMpClient)
            {
                Destroy(gameObject);
            }
            InvokeRepeating("DelayedStart", 30, 5);
            setup = new List<CoopPlayerRemoteSetup>();
            EventRegistry.Enemy.Subscribe(TfEvent.KilledEnemy, OnEnemyKilledMod);


        }

        void DelayedStart()
        {
            if (GameSetup.IsMpServer)
            {

                setup.Clear();
                for (int i = 0; i < TheForest.Utils.Scene.SceneTracker.allPlayerEntities.Count; i++)
                {
                    BoltEntity o = TheForest.Utils.Scene.SceneTracker.allPlayerEntities[i];
                    if (o.isAttached && o.StateIs<IPlayerState>() && (LocalPlayer.Entity != o && o.gameObject.activeSelf && o.gameObject.activeInHierarchy))
                    {
                        CoopPlayerRemoteSetup s = o.GetComponent<CoopPlayerRemoteSetup>();
                        if (s != null)
                        {
                            setup.Add(s);
                        }
                    }
                }
            }
        }
        public void OnEnemyKilledMod(object o)
        {
            EnemyHealth hp = (EnemyHealth)o;
            if (hp != null)
            {

                mutantAI ai = hp.gameObject.GetComponent<mutantAI>();
                if (ai != null)
                {
                 
                    if (hp.Health <= 0)
                    {
                        /*
                        if (ai.male)
                        {
                            if (ai.pale)
                            {
                                if (ai.skinned)
                                {
                                    Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.SkinnedMale,-1,Quests.HeldWeapon);

                                }
                                else
                                {
                                    Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.PaleMale, -1, Quests.HeldWeapon);

                                }
                            }
                            else if (ai.painted)
                            {
                                if (ai.fireman)
                                {
                                    Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.PaintedFirethrower, -1, Quests.HeldWeapon);

                                }
                                else if (ai.leader)
                                {
                                    Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.PaintedLeader, -1, Quests.HeldWeapon);

                                }
                                else
                                {
                                    Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.PaintedMale, -1, Quests.HeldWeapon);
                                }
                            }
                            else
                            {
                                if (ai.leader)
                                {
                                    Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.TribalMaleLeader, -1, Quests.HeldWeapon);

                                }
                                else
                                {


                                    Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.TribalMale, -1, Quests.HeldWeapon);
                                }
                            }

                        }
                        if (ai.creepy)
                        {
                            if (ai.pale)
                            {
                                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.PaleVirgina, -1, Quests.HeldWeapon);

                            }
                            else
                            {
                                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Virgina, -1, Quests.HeldWeapon);

                            }
                        }
                        if (ai.creepy_male)
                        {
                            if (ai.pale)
                            {
                                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.PaleArmsy, -1, Quests.HeldWeapon);

                            }
                            else
                            {
                                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Armsy, -1, Quests.HeldWeapon);

                            }
                        }
                        if (ai.creepy_baby)
                        {
                            Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Baby, -1, Quests.HeldWeapon);

                        }
                        if (ai.creepy_boss)
                        {
                            Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.EndBoss, -1, Quests.HeldWeapon);

                        }
                        if (ai.creepy_fat)
                        {
                            Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Cowman, -1, Quests.HeldWeapon);

                        }
                        if (ai.maleSkinny)
                        {
                            if (ai.pale)
                            {
                                if (ai.skinned)
                                {
                                    Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.SkinnedSkinny, -1, Quests.HeldWeapon);

                                }
                                else
                                {
                                    Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.SkinnyPale, -1, Quests.HeldWeapon);

                                }
                            }
                            else
                            {
                                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.SkinnyMale, -1, Quests.HeldWeapon);

                            }
                        }
                        if (ai.female)
                        {
                            if (ai.painted)
                            {
                                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.PaintedFemale, -1, Quests.HeldWeapon);

                            }
                            else
                            {
                                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.TribalFemale, -1, Quests.HeldWeapon);

                            }
                        }
                        if (ai.femaleSkinny)
                        {
                            Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.SkinnyFemale, -1, Quests.HeldWeapon);

                        }
                        */
                        int xppoints = 10;
                        if (ai.creepy_boss)
                        {
                            xppoints = 25000;
                        }
                        if (ai.leader)
                        {
                            xppoints += 200;
                        }
                        if (ai.female)
                        {
                            xppoints += 80;
                        }
                        if (ai.femaleSkinny)
                        {
                            if (ai.female)
                            {
                                xppoints = 70;
                            }
                            else
                            {
                                xppoints += 60;
                            }
                        }
                        if (ai.male)
                        {
                            xppoints += 75;
                        }
                        if (ai.creepy_male)
                        {
                            xppoints += 3000;
                            if (ai.pale)
                            {
                                xppoints += 1000;
                            }
                        }
                        if (ai.maleSkinny)
                        {
                            if (ai.male)
                            {
                                xppoints = 70;
                            }
                            else
                            {
                                xppoints += 60;

                            }
                        }

                        if (ai.creepy_fat)
                        {
                            xppoints += 3300;

                        }
                        if (ai.fireman)
                        {
                            xppoints += 150;
                        }
                        if (ai.fireman_dynamite)
                        {
                            xppoints += 200;
                        }
                        if (ai.pale)
                        {
                            xppoints += 400;
                        }
                        if (ai.painted)
                        {
                            xppoints += 350;
                        }
                        if (ai.skinned)
                        {
                            xppoints += 500;
                        }
                        if (ai.creepy)
                        {
                            xppoints += 2800;
                            if (ai.pale)
                            {
                                xppoints += 1700;
                            }
                        }
                        if (ai.creepy_baby)
                        {
                            xppoints = 80;
                        }
                        float r = UnityEngine.Random.Range(0.75f, 2f);
                        xppoints = Mathf.RoundToInt(r * xppoints);
                        ModAPI.Console.Write("Enemy died,sending exp");

                        List<CoopPlayerRemoteSetup> setupstoGiveExp = new List<CoopPlayerRemoteSetup>();
                        for (int i = 0; i < setup.Count; i++)
                        {
                            try
                            {
                                if (Vector3.Distance(setup[i].transform.position, hp.transform.position) < 300)
                                {
                                    setupstoGiveExp.Add(setup[i]);
                                }
                            }
                            catch
                            {
                                ModAPI.Console.Write("Error with checking distance parameter for a clinet");
                            }
                        }
                        int x = 0;
                        try
                        {
                            if (Vector3.Distance(LocalPlayer.Transform.position, hp.transform.position) < 300)
                            {
                                x = 1;
                            }
                    }
                            catch
                    {
                        ModAPI.Console.Write("Error with checking distance parameter for the local player");
                    }
                    if (x==0 && setupstoGiveExp.Count == 0)
                        {
                            return;
                        }

                        xppoints = Mathf.RoundToInt((float)xppoints / (x + setupstoGiveExp.Count));
                        for (int a = 0; a < setupstoGiveExp.Count; a++)
                        {
                            try
                            {



                                setupstoGiveExp[a].HitShark(-xppoints);
                            }
                            catch
                            {
                                ModAPI.Console.Write("Error with hitting clinet");
                            }
                        }

                        if (x == 1)
                        {
                            try
                            {
                                UpgradePointsMod.instance.AddXP(xppoints, true);
                            }
                            catch
                            {
                                ModAPI.Console.Write("Error with hitting local player");
                            }
                        }
                    }
                }
            }
        }
    }
}

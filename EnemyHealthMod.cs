using ModAPI.Attributes;
using UnityEngine;
using TheForest.Utils;
using System.Linq;

namespace PlayerUpgradePoints
{
    class EnemyHealthMod : EnemyHealth
    {
        public void ReduceHealth()
        {
            Health = Mathf.RoundToInt(Health * 0.15f);
        }

        [Priority(50)]
        public override void Hit(int damage)
        {
            if (UpgradePointsMod.instance.specialUpgrades[42].bought && StealthCheck.IsHidden)
            {
                ReduceHealth();
            }
            base.Hit(damage);
        }

        protected override void DieTrap(int type)
        {
            if (ai.male)
            {
                if (ai.pale)
                {
                    if (ai.skinned)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.SkinnedMale);

                    }
                    else
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.PaleMale);
                    }
                }
                else if (ai.painted)
                {
                    if (ai.fireman)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.PaintedFirethrower);
                    }
                    else if (ai.leader)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.PaintedLeader);
                    }
                    else
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.PaintedMale);
                    }
                }
                else
                {
                    if (ai.leader)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.TribalMaleLeader);

                    }
                    else
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.TribalMale);
                    }
                }
            }

            if (ai.creepy)
            {
                if (ai.pale)
                {
                    Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.PaleVirgina);
                }
                else
                {
                    Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.Virgina);
                }
            }

            if (ai.creepy_male)
            {
                if (ai.pale)
                {
                    Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.PaleArmsy);
                }
                else
                {
                    Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.Armsy);
                }
            }

            if (ai.creepy_baby)
            {
                Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.Baby);
            }

            if (ai.creepy_boss)
            {
                Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.EndBoss);
            }

            if (ai.creepy_fat)
            {
                Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.Cowman);
            }

            if (ai.maleSkinny)
            {
                if (ai.pale)
                {
                    if (ai.skinned)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.SkinnedSkinny);
                    }
                    else
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.SkinnyPale);
                    }
                }
                else
                {
                    Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.SkinnyMale);
                }
            }

            if (ai.female)
            {
                if (ai.painted)
                {
                    Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.PaintedFemale);
                }
                else
                {
                    Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.TribalFemale);
                }
            }

            if (ai.femaleSkinny)
            {
                Quests.UpdateActiveQuests(QuestObjective.Type.TrapEnemies, 1, QuestObjective.Enemy.SkinnyFemale);
            }

            base.DieTrap(type);
        }

        public override void Die()
        {
            int xp = 10;
            if (ai.creepy_boss)
            {
                xp = 25000;
            }

            if (ai.leader)
            {
                xp += 200;
            }

            if (ai.female)
            {
                xp += 80;
            }

            if (ai.femaleSkinny)
            {
                if (ai.female)
                {
                    xp = 70;
                }
                else
                {
                    xp += 60;
                }
            }

            if (ai.male)
            {
                xp += 75;
            }

            if (ai.creepy_male)
            {
                xp += 3000;

                if (ai.pale)
                {
                    xp += 1000;
                }
            }

            if (ai.maleSkinny)
            {
                if (ai.male)
                {
                    xp = 70;
                }
                else
                {
                    xp += 60;
                }
            }

            if (ai.creepy_fat)
            {
                xp += 3300;
            }

            if (ai.fireman)
            {
                xp += 150;
            }

            if (ai.fireman_dynamite)
            {
                xp += 200;
            }

            if (ai.pale)
            {
                xp += 400;
            }

            if (ai.painted)
            {
                xp += 350;
            }

            if (ai.skinned)
            {
                xp += 500;
            }

            if (ai.creepy)
            {
                xp += 2800;

                if (ai.pale)
                {
                    xp += 1700;
                }
            }

            if (ai.creepy_baby)
            {
                xp = 80;
            }

            float range = Random.Range(0.75f, 2f);
            xp = Mathf.RoundToInt(range * xp);

            Vector3 pos = ai.transform.position;

            int count = Scene.SceneTracker.allPlayers.Count(go => (go.transform.position - pos).sqrMagnitude < 250 * 250);
            bool giveLocalPlayer = false;

            if ((LocalPlayer.Transform.position - pos).sqrMagnitude < 250 * 250)
            {
                giveLocalPlayer = true;
                count++;
            }

            if (count == 0)
            {
                return;
            }

            xp = Mathf.RoundToInt((float)xp / (count));

            if (giveLocalPlayer)
            {
                UpgradePointsMod.instance.AddXP(xp, true);
            }

            Network.NetworkManager.SendExpCommand(xp, pos, true);
        }
    }
}
using System.Collections.Generic;
using ModAPI.Attributes;
using UnityEngine;
namespace PlayerUpgradePoints
{
    class EnemyHealthMod:EnemyHealth
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
                    else { 
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
    }
}

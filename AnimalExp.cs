using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using UnityEngine;

namespace PlayerUpgradePoints
{
    class AnimalExp : animalHealth
    {
        protected override void Die()
        {
            int xp = 0;
            if (spawnFunctions.lizard)
            {
                xp = UnityEngine.Random.Range((int)5, 10);
                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Lizard, -1, Quests.HeldWeapon);
            }
            if (spawnFunctions.turtle)
            {
                xp = UnityEngine.Random.Range((int)5, 15);

                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Turtle, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.rabbit)
            {
                xp = UnityEngine.Random.Range((int)10, 20);

                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Rabbit, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.fish)
            {
                xp = UnityEngine.Random.Range((int)5, 8);

                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Fish, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.tortoise)
            {
                xp = UnityEngine.Random.Range((int)15, 25);

                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Tortoise, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.raccoon)
            {
                xp = UnityEngine.Random.Range((int)30, 40);

                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Raccoon, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.deer)
            {
                xp = UnityEngine.Random.Range((int)20, 40);

                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Deer, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.squirrel)
            {
                xp = UnityEngine.Random.Range((int)5, 10);

                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Squirrel, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.boar)
            {
                xp = UnityEngine.Random.Range((int)25, 45);

                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Boar, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.crocodile)
            {
                xp = UnityEngine.Random.Range((int)60, 86);

                Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Crocodile, -1, Quests.HeldWeapon);

            }


            List<CoopPlayerRemoteSetup> setupstoGiveExp = new List<CoopPlayerRemoteSetup>();
            for (int i = 0; i < CoopExpShare.instance.setup.Count; i++)
            {
                if (Vector3.Distance(CoopExpShare.instance.setup[i].transform.position, transform.position) < 250)
                {
                    setupstoGiveExp.Add(CoopExpShare.instance.setup[i]);
                }
            }
            int x = 0;
            if (Vector3.Distance(LocalPlayer.Transform.position, transform.position) < 250)
            {
                x = 1;
            }

            if (x == 1 || setupstoGiveExp.Count > 0)
            {


                xp = Mathf.RoundToInt((float)xp / (x + setupstoGiveExp.Count));
                for (int a = 0; a < setupstoGiveExp.Count; a++)
                {
                    setupstoGiveExp[a].HitShark(-(xp + 1000000000));
                }

                if (x == 1)
                {
                    UpgradePointsMod.instance.AddXP(xp, false);
                }
            }
            base.Die();
        }
    }
}

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
                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Lizard, -1, Quests.HeldWeapon);
            }
            if (spawnFunctions.turtle)
            {
                xp = UnityEngine.Random.Range((int)5, 15);

                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Turtle, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.rabbit)
            {
                xp = UnityEngine.Random.Range((int)10, 20);

                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Rabbit, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.fish)
            {
                xp = UnityEngine.Random.Range((int)5, 8);

                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Fish, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.tortoise)
            {
                xp = UnityEngine.Random.Range((int)15, 25);

                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Tortoise, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.raccoon)
            {
                xp = UnityEngine.Random.Range((int)30, 40);

                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Raccoon, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.deer)
            {
                xp = UnityEngine.Random.Range((int)20, 40);

                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Deer, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.squirrel)
            {
                xp = UnityEngine.Random.Range((int)5, 10);

                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Squirrel, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.boar)
            {
                xp = UnityEngine.Random.Range((int)25, 45);

                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Boar, -1, Quests.HeldWeapon);

            }
            if (spawnFunctions.crocodile)
            {
                xp = UnityEngine.Random.Range((int)60, 86);

                //Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Crocodile, -1, Quests.HeldWeapon);

            }


            Vector3 pos = transform.position;
            int count = TheForest.Utils.Scene.SceneTracker.allPlayers.Count(go => (go.transform.position - pos).sqrMagnitude < 250 * 250);
            bool giveLocalPlayer = false;

            if ((LocalPlayer.Transform.position - pos).sqrMagnitude < 250 * 250)
            {
                giveLocalPlayer = true;
                count++;
            }


            if (count == 0)
                return;


            xp = Mathf.RoundToInt((float)xp / (count));
            Network.NetworkManager.SendExpCommand(xp, pos, false);

            if (giveLocalPlayer)
            {
                UpgradePointsMod.instance.AddXP(xp, false);
            }
            base.Die();
        }
    }
}

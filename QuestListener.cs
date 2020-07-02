using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest.Tools;
using TheForest.Utils;

namespace PlayerUpgradePoints
{
    class QuestListener : MonoBehaviour
    {
        void Start()
        {

            EventRegistry.Player.Subscribe(TfEvent.CutTree, OnTreeCut);

            EventRegistry.Player.Subscribe(TfEvent.AddedItem, OnAddItem);

            EventRegistry.Animal.Subscribe(TfEvent.KilledBird, BirdKilled);
            EventRegistry.Animal.Subscribe(TfEvent.KilledShark, SharkKilled);

            EventRegistry.Player.Subscribe(TfEvent.CraftedItem, OnCraftItem);
        }

        public void OnTreeCut(object o)
        {
            try
            {

                Quests.UpdateActiveQuests(QuestObjective.Type.CutTree, 1, QuestObjective.Enemy.None, -1, Quests.HeldWeapon, "");

                int xp = UnityEngine.Random.Range((int)15, 25);
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
            }
            catch (Exception ex)
            {
                ModAPI.Log.Write(ex.ToString());
            }
        }
        public void OnLimbCut(object o)
        {
            Quests.UpdateActiveQuests(QuestObjective.Type.CutLimbs, 1, QuestObjective.Enemy.None, -1, Quests.HeldWeapon, "");
            int i = UnityEngine.Random.Range(0, 100);
            if (i > 50)
            {
                int xp = UnityEngine.Random.Range((int)8, 16);
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
            }
        }
        public void OnAddItem(object o)
        {
            Quests.UpdateActiveQuests(QuestObjective.Type.GetItem, 1, QuestObjective.Enemy.None, (int)o);

        }
        public void OnCraftItem(object o)
        {
            Quests.UpdateActiveQuests(QuestObjective.Type.CraftItem, 1, QuestObjective.Enemy.None, (int)o);

        }


        public void BirdKilled(object o)
        {

            int xp = UnityEngine.Random.Range((int)5, 14);
            Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Bird, -1, Quests.HeldWeapon);

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
        }
        public void SharkKilled(object o)
        {
            int xp = UnityEngine.Random.Range((int)390, 450);
            Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Shark, -1, Quests.HeldWeapon);
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
        }





    }
}

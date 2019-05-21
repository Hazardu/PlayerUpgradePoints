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
              //  ModAPI.Log.Write("Giving Exp for cutting tree");


                Quests.UpdateActiveQuests(QuestObjective.Type.CutTree, 1, QuestObjective.Enemy.None, -1, Quests.HeldWeapon, "");

                int i2 = UnityEngine.Random.Range((int)8, 15);
                TreeHealth treehealth = (TreeHealth)o;
                Transform t = treehealth.transform;
                List<CoopPlayerRemoteSetup> setupstoGiveExp = new List<CoopPlayerRemoteSetup>();
                for (int a = 0; a < CoopExpShare.instance.setup.Count; a++)
                {
                 
                        setupstoGiveExp.Add(CoopExpShare.instance.setup[a]);
                 
                }
                int x =1;
               
                i2 = Mathf.RoundToInt((float)i2 / (x + setupstoGiveExp.Count));
                for (int a = 0; a < setupstoGiveExp.Count; a++)
                {
                    setupstoGiveExp[a].HitShark(-(i2 + 1000000000));
                }

                if (x == 1)
                {
                    UpgradePointsMod.instance.AddXP(i2, false);
                }
            }
            catch (Exception ex)
            {

                ModAPI.Log.Write(ex.ToString());
            }
        }
        public void OnLimbCut(object o)
        {
           // ModAPI.Log.Write("Giving Exp for cutting enemy");
            Quests.UpdateActiveQuests(QuestObjective.Type.CutLimbs, 1, QuestObjective.Enemy.None, -1, Quests.HeldWeapon, "");
            int i = UnityEngine.Random.Range((int)0, 100);
            if (i > 50)
            {
                int i2 = UnityEngine.Random.Range((int)10, 16);
                
                List<CoopPlayerRemoteSetup> setupstoGiveExp = new List<CoopPlayerRemoteSetup>();
                for (int a = 0; a < CoopExpShare.instance.setup.Count; a++)
                {
                 
                        setupstoGiveExp.Add(CoopExpShare.instance.setup[a]);
                    
                }
                int x = 1;
                
                i2 = Mathf.RoundToInt((float)i2 / (x + setupstoGiveExp.Count));
                for (int a = 0; a < setupstoGiveExp.Count; a++)
                {
                    setupstoGiveExp[a].HitShark(-(i2 + 1000000000));
                }

                if (x == 1)
                {
                    UpgradePointsMod.instance.AddXP(i2, false);
                }
            }
        }
        public void OnAddItem(object o)
        {
            Quests.UpdateActiveQuests(QuestObjective.Type.GetItem, 1, QuestObjective.Enemy.None,(int)o);

        }
        public void OnCraftItem(object o)
        {
            Quests.UpdateActiveQuests(QuestObjective.Type.CraftItem, 1, QuestObjective.Enemy.None, (int)o);

        }

       
        public void BirdKilled(object o)
        {
            //ModAPI.Log.Write("Giving Exp for killing bird");

            int i2 = UnityEngine.Random.Range((int)5, 10);
            Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Bird, -1, Quests.HeldWeapon);

           
            List<CoopPlayerRemoteSetup> setupstoGiveExp = new List<CoopPlayerRemoteSetup>();
            for (int i = 0; i < CoopExpShare.instance.setup.Count; i++)
            {
              
                    setupstoGiveExp.Add(CoopExpShare.instance.setup[i]);
               
            }
            int x =1;



            if (x == 0 && setupstoGiveExp.Count == 0)
            {
                return;
            }
            i2 = Mathf.RoundToInt((float)i2 / (x + setupstoGiveExp.Count));
            for (int a = 0; a < setupstoGiveExp.Count; a++)
            {
                setupstoGiveExp[a].HitShark(-(i2 + 1000000000));
            }

            if (x == 1)
            {
                UpgradePointsMod.instance.AddXP(i2, false);
            }
        }
        public void SharkKilled(object o)
        {
            //ModAPI.Log.Write("Giving Exp for killing shark");

            int i2 = UnityEngine.Random.Range((int)80, 150);
            Quests.UpdateActiveQuests(QuestObjective.Type.Kill, 1, QuestObjective.Enemy.Shark, -1, Quests.HeldWeapon);
            GameObject gameObjectO = (GameObject)o;
            Transform t = gameObjectO.transform;
            List<CoopPlayerRemoteSetup> setupstoGiveExp = new List<CoopPlayerRemoteSetup>();
            for (int i = 0; i < CoopExpShare.instance.setup.Count; i++)
            {
                if (Vector3.Distance(CoopExpShare.instance.setup[i].transform.position, t.position) < 300)
                {
                    setupstoGiveExp.Add(CoopExpShare.instance.setup[i]);
                }
            }
            int x = 0;
            if (Vector3.Distance(LocalPlayer.Transform.position, transform.position) < 300)
            {
                x = 1;
            }



            i2 = Mathf.RoundToInt((float)i2 / (x + setupstoGiveExp.Count));
            for (int a = 0; a < setupstoGiveExp.Count; a++)
            {
                setupstoGiveExp[a].HitShark(-(i2 + 1000000000));
            }

            if (x==1)
            {
                UpgradePointsMod.instance.AddXP(i2, false);
            }
        }





    }
}

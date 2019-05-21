using System.Collections.Generic;
using TheForest.Items.Inventory;
using TheForest.Utils;
namespace PlayerUpgradePoints
{
    public class Quests
    {
        public static List<Quest> quests_active = new List<Quest>();
        public static List<Quest> quests_completed = new List<Quest>();
        public static QuestObjective.Weapon HeldWeapon;
        public static void MoveActiveToCompleted(Quest key)
        {
           if( quests_active.Contains(key))
            {
                Quest quest = key;
                quests_completed.Reverse();
                quests_completed.Add(key);
                quests_completed.Reverse();
                quests_active.Remove(key);
            }
        }
        public static void UpdateActiveQuests(QuestObjective.Type type, float amount, QuestObjective.Enemy killname = QuestObjective.Enemy.None, int itemid= -1, QuestObjective.Weapon weaponusedid = QuestObjective.Weapon.None, string buildingname="")
        {
            for (int i = 0; i < quests_active.Count; i++)
            {
                quests_active[i].UpdateObjective(type, amount, killname, itemid, weaponusedid, buildingname);
                if (quests_active[i].Finished)
                {
                    MoveActiveToCompleted(quests_active[i]);
                }
            }
        }
        public static bool WasDoneBeforeByName(string QuestName)
        {
            for (int a = 0; a < quests_completed.Count; a++)
            {
                if(quests_completed[a].Name == QuestName)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool IsInProgressByName(string QuestName)
        {
            for (int a = 0; a < quests_active.Count; a++)
            {
                if (quests_active[a].Name == QuestName)
                {
                    return true;
                }
            }

            return false;
        }

        void DrawQuestMenu()
        {
            
        }
    }
    [System.Serializable]
    public class Quest
    {
        public Quest()
        {
            Name = "Unnamed quest";
            Description = "No description";
            Finished = false;
            RewardsGiven = false;
            rewards = new QuestRewards();
            objectives = new List<QuestObjective>();
            objectivesCompleted = 0;
            objectivesRequired = 0;
        }
        public string Name = "";
        public string Description = "";

        public bool Finished;
        public bool RewardsGiven;

        public List<QuestObjective> objectives;
        public QuestRewards rewards;    // rewards in items, exp, refund points to respec upgrades

        
        public int objectivesCompleted;
        public int objectivesRequired;  // a quest can have multipe quests but wont reqire all of them to be finished
        // for ex quest has 3 objectives, and after completing 2 the quest is finished 



        //Every action player takes updates quests objecties via this method
        public void UpdateObjective(QuestObjective.Type type,float amount, QuestObjective.Enemy killname, int itemid,QuestObjective.Weapon weaponusedid, string buildingname)
        {
            // loops all the quest to check if QuestObjective.Type parameter and one in quest objectives match
            for (int i = 0; i < objectives.Count; i++)
            {
                // if they do it continues
                if(objectives[i].type == type)
                {
                    // method in previous ss
                    if (objectives[i].CheckParameters(killname, itemid, weaponusedid,buildingname))
                    {
                        if (objectives[i].IncreaseObjective(amount))
                        {
                            objectivesCompleted++;
                            if(objectivesCompleted >= objectivesRequired)
                            {
                                FinishQuest();
                            }
                        }
                    }
                }
            }
        }
        void FinishQuest()
        {
            Finished = true;
           
        }
        public void CollectRewards()
        {
            if (!RewardsGiven)
            {
                RewardsGiven = rewards.GrantRewards();
            }
        }
    }
    [System.Serializable]
    public class QuestObjective
    {
        public QuestObjective()
        {
            _name = "Objective name";
            _description = "Objective description";
            _current = 0;
            _target = 1;
            fulfilled = false;
            type = Type.Level;
            _enemy = new List<Enemy>();
            _ItemID = -1;
            _WeaponUsed = Weapon.None;
            _Buildingname = "";
        }
        





        public string _name = "";
        public string _description = "";

        public float _current;
        public float _target;

        public bool fulfilled;


        public enum Weapon
        {
            None,
            Stick,
            PlaneAxe,
            ModernAxe,
            RustyAxe,
            CraftedAxe,
            ClimbingAxe,
            Katana,
            Machete,
            WeakSpear,
            UpgSpear,
            Rock,
            Club,
            CraftedClub,
            Tennis,
            Chainsaw,
            Arm,
            Leg,
            Bone,
            RepairTool,
            Rabbit,
            Lizard,
            Cod,
            Meat,
            SmallMeat,
            Shell,
            Skull,
            Head
        }
        public enum Enemy
        {
            None,
            SkinnyMale,
            SkinnyFemale,
            SkinnyPale,
            
            TribalMale,
            TribalMaleLeader,
            TribalFemale,

            PaintedMale,
            PaintedFirethrower,
            PaintedFemale,
            PaintedLeader,

            PaleMale,
            Baby,
            EndBoss,
            Armsy,
            PaleArmsy,
            Virgina,
            PaleVirgina,
            Cowman,

            SkinnedSkinny,
            SkinnedMale,

            Lizard,
            Rabbit,
            Bird,
            Shark,
            Raccoon,
            Turtle,
            Deer,
            Fish,
            Tortoise,
            Crocodile,
            Squirrel,
            Boar
            
                
        }
        public enum Type
        {
            Level,
            Kill,
            GetItem,
            DestroyEffigies,
            BuildObjects,
            Run,
            Swim,
            Fall,
            TrapEnemies,
            Sled,
            Zipline,
            CutBush,
            CutTree,
            StayInCave,
            RideShell,
            CutLimbs,
            UseItem,     
            CraftItem
        }

        public Type type;

        public List<Enemy> _enemy;
        public int _ItemID;
        public Weapon _WeaponUsed;
        
        public string _Buildingname;


        public bool IncreaseObjective(float amount)
        {
            _current += amount;
            if (_current >= _target)
            {
                FinishObjective();
                return true;
            }
            return false;
        }

        public void FinishObjective()
        {
            fulfilled = true;
        }

        public bool CheckParameters(Enemy killname,int itemid, Weapon weaponusedid,string buildingname)
        {
            if (!_enemy.NullOrEmpty())
            {
                if(!_enemy.Contains(killname))
                {
                    return false;
                }
            }
            if (_ItemID!= -1)
            {
                if (_ItemID != itemid)
                {
                    return false;
                }

            }
            if (_WeaponUsed!= Weapon.None)
            {
                if (_WeaponUsed != weaponusedid )
                {
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(_Buildingname))
            {
                if (_Buildingname != buildingname)
                {
                    return false;
                }
            }
            return true;
        }


    }
    [System.Serializable]
    public class QuestRewards
    {

        public QuestRewards()
        {
            _exp = 0;
            _refundPoints = 0;
            _items = new List<ItemtoAdd>();
        }
        public int _exp;
        public int _refundPoints;


        public struct ItemtoAdd
        {
            public int ID;
            public int Amount;
            public ItemProperties properties;
        }
        public List< ItemtoAdd> _items;

        public bool GrantRewards()
        {
            UpgradePointsMod mod = UpgradePointsMod.instance;
            mod.AddXP(_exp,false);
            mod.RefundPoints += _refundPoints;
            for (int i = 0; i < _items.Count; i++)
            {
                LocalPlayer.Inventory.AddItem(_items[i].ID, _items[i].Amount, true, false, _items[i].properties);
            }
            return true;
        }
    }
}

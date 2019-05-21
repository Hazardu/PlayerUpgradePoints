using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Buildings.Creation;
namespace PlayerUpgradePoints
{
    class Buildings : Create
    {
        public override void CreateBuilding(BuildingTypes type)
        {
            base.CreateBuilding(type);
            ModAPI.Console.Write(type.ToString(), "building finished");
            Quests.UpdateActiveQuests(QuestObjective.Type.BuildObjects, 1, QuestObjective.Enemy.None, -1, QuestObjective.Weapon.None, type.ToString());
        }
    }
}

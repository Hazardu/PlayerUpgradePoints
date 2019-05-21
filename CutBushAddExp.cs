using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerUpgradePoints
{
    class CutBushAddExp : CutBush
    {
        public override void CutDown()
        {
            
            Quests.UpdateActiveQuests(QuestObjective.Type.CutBush, 1,QuestObjective.Enemy.None,-1,Quests.HeldWeapon);
            base.CutDown();
        }
    }
    class CutBush2AddExp : CutBush2
    {
        public override void CutDown()
        {

            Quests.UpdateActiveQuests(QuestObjective.Type.CutBush, 1, QuestObjective.Enemy.None, -1, Quests.HeldWeapon);
            base.CutDown();
        }
    }
}

using UnityEngine;
namespace PlayerUpgradePoints
{
    public class CutEffigyAddExp : CutEffigy
    {
        protected override void CutDown()
        {
            if (!breakEventPlayed)
            {
                Quests.UpdateActiveQuests(QuestObjective.Type.DestroyEffigies, 1, QuestObjective.Enemy.None,-1,Quests.HeldWeapon, "");
               ModAPI.Console.Write("Destroying Effiggy, name of effiggy " + name);
                int r = Random.Range(5, 51);
                UpgradePointsMod.instance.AddXP(r, false);
            }
            base.CutDown();
        }



    }
}

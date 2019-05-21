using UnityEngine;
using Bolt;
namespace PlayerUpgradePoints
{
    public class CoopPlayerRemoteSetupMod : CoopPlayerRemoteSetup
    {
        /*
        public override void hitFromEnemy(int damage)
        {
            PlayerHitByEnemey playerHitByEnemey = PlayerHitByEnemey.Create();
            playerHitByEnemey.Damage = Mathf.RoundToInt(damage * UpgradePointsMod.instance.DamageReduction);
            if (UpgradePointsMod.instance.specialUpgrades[5].bought)
            {
                playerHitByEnemey.Damage = Mathf.RoundToInt(damage * 0.35f);
            }
                if (UpgradePointsMod.instance.specialUpgrades[12].bought)
            {
                int r = Random.Range(0, 100);
                if (r < 30)
                {
                    return;
                }
            }
            playerHitByEnemey.Target = base.entity;
            playerHitByEnemey.Send();
        }
        */
        public override void HitShark(int damage)
        {
            PlayerHitByEnemey playerHitByEnemey = PlayerHitByEnemey.Create();
            if (damage < 0) { playerHitByEnemey = PlayerHitByEnemey.Create(GlobalTargets.AllClients); }
            playerHitByEnemey.Damage = damage;
            playerHitByEnemey.Target = base.entity;
            playerHitByEnemey.SharkHit = true;
            playerHitByEnemey.Direction = hitDirection;
            playerHitByEnemey.Send();
        }
        
    }
}


    


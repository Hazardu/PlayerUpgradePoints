using Bolt;
using UnityEngine;

namespace PlayerUpgradePoints
{
    class DrowningEnemies : mutantWaterDetect
    {
        protected override void Update()
        {
            if (Time.time > valideColliderDelay)
            {
                ValidateWaterCollider();
                valideColliderDelay = Time.time + 2f;
            }
            if (inWater)
            {
                if ((bool)currentWaterCollider)
                {
                    Vector3 center = currentWaterCollider.bounds.center;
                    float y = center.y;
                    Vector3 extents = currentWaterCollider.bounds.extents;
                    float num = y + extents.y;
                    if (!netPrefab)
                    {
                        float num2 = num;
                        Vector3 position = setup.rootTr.position;
                        if (num2 - position.y > 0.9f && !setup.search.fsmInCave.Value)
                        {
                            if (!fsmExitWaterBool.Value)
                            {
                                setup.pmCombat.SendEvent("goToExitWater");
                            }
                            fsmExitWaterBool.Value = true;
                        }
                    }
                    float num3 = num;
                    Vector3 position2 = rootTr.position;
                    waterHeight = num3 - position2.y;
                    if (waterHeight > 4f)
                    {
                        underWater = true;
                        if (Time.time > inWaterTimer - 1f && !drowned)
                        {
                            if (BoltNetwork.isClient)
                            {
                                PlayerHitEnemy playerHitEnemy = PlayerHitEnemy.Create(GlobalTargets.OnlyServer);
                                playerHitEnemy.Target = base.GetComponent<BoltEntity>();
                                playerHitEnemy.Hit = int.MaxValue -1;
                                playerHitEnemy.Send();
                            }
                            else
                            {
                                setup.health.HitReal(setup.health.Health);
                            }
                            drowned = true;
                        }
                    }
                    else
                    {
                        underWater = false;
                        inWaterTimer = Time.time + 5f;
                    }
                }
                else
                {
                    underWater = false;
                }
            }
            else
            {
                inWaterTimer = Time.time + 5f;
                underWater = false;
            }
        }
    }
}
    


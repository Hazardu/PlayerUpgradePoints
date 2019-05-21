using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using UnityEngine;

namespace PlayerUpgradePoints
{
    class BrawlerUpgrade :MonoBehaviour
    {
        List<Transform> EnemiesNearby;
        public static float DamageBonus;
        public static float DamageReduction;
        public static int EnemiesNearbyCount;
        private float DamagePerEnemy = 1.2f;
        private float ReductionPerEnemy = 0.9f;
        private float Radius = 20;
        void Start()
        {
            EnemiesNearby = new List<Transform>();
            DamageBonus = 0;
            DamageReduction = 0;
            StartCoroutine(CheckEnemiesCoroutine());
        }
        IEnumerator CheckEnemiesCoroutine()
        {
            while (true)
            {
                if (UpgradePointsMod.instance.specialUpgrades[45].bought)
                {
                    if (UpgradePointsMod.instance.specialUpgrades[85].bought)
                    {
                        DamagePerEnemy = 1.3f;
                    }
                    else
                    {
                        DamagePerEnemy = 1.2f;
                    }
                    if (UpgradePointsMod.instance.specialUpgrades[86].bought)
                    {
                        ReductionPerEnemy = 0.85f;
                    }
                    else
                    {
                        ReductionPerEnemy = 0.9f;
                    }
                    EnemiesNearby.Clear();
                    RaycastHit[] raycastHits = Physics.SphereCastAll(LocalPlayer.Transform.position, Radius, Vector3.one);
                    for (int x = 0; x < raycastHits.Length; x++)
                    {
                        if (raycastHits[x].transform.CompareTag("enemyCollide"))
                        {
                            if (!EnemiesNearby.Contains(raycastHits[x].transform.root))
                            {
                                EnemiesNearby.Add(raycastHits[x].transform.root);
                            }
                        }
                    }
                    EnemiesNearbyCount = EnemiesNearby.Count;
                    DamageBonus = 1;
                    DamageReduction = 1;
                    for (int i = 0; i < EnemiesNearby.Count; i++)
                    {
                        DamageBonus *= DamagePerEnemy;
                        DamageReduction *= ReductionPerEnemy;
                    }
                    DamageReduction--;
                    DamageBonus--;
                }
                else
                {
                    DamageBonus = 0;
                    DamageReduction = 0;
                }
                yield return new WaitForSeconds(0.5f);

            }
        }
    }
}

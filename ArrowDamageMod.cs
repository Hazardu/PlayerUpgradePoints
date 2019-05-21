using System;
using UnityEngine;
using TheForest.Utils;
using TheForest.Save;
using System.IO;
using Bolt;
using PathologicalGames;
using TheForest.Tools;
using TheForest.Items.Inventory;
using ModAPI;
using ModAPI.Attributes;
using TheForest.Audio;
using TheForest.World;
using TheForest.Buildings.World;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.CompilerServices;
using TheForest.Utils.Settings;
using TheForest.Items;
using UniLinq;
using TheForest.Buildings.Creation;

namespace PlayerUpgradePoints
{
    public class ArrowDamageMod : ArrowDamage
    {
        public List<Transform> alreadyHit;
        public int BaseDamage;
        public Vector3 startingpos;
        private float velocitybonus;
        protected override void OnEnable()
        {
            base.OnEnable();
            if (UpgradePointsMod.instance.specialUpgrades[33].bought)
            {
                startingpos = LocalPlayer.Transform.position;
            }
        }
        protected override void Start()
        {
            base.Start();
            BaseDamage = damage;
            alreadyHit = new List<Transform>();
            velocitybonus = 0;
            if (UpgradePointsMod.instance.specialUpgrades[41].bought)
            {
                if (UpgradePointsMod.instance.specialUpgrades[80].bought)
                {
                    if (UpgradePointsMod.instance.specialUpgrades[81].bought)
                    {
                        if (UpgradePointsMod.instance.specialUpgrades[82].bought)
                        {
                            if (UpgradePointsMod.instance.specialUpgrades[83].bought)
                            {
                                velocitybonus = 17*CharacterControllerMod.FloatVelocity;

                            }
                            else
                            {
                                velocitybonus =12* CharacterControllerMod.FloatVelocity;

                            }
                        }
                        else
                        {
                            velocitybonus = 7*CharacterControllerMod.FloatVelocity;

                        }
                    }
                    else
                    {
                        velocitybonus = 3*CharacterControllerMod.FloatVelocity;

                    }
                }
                else
                {
                    velocitybonus = CharacterControllerMod.FloatVelocity;
                }
            }
        }
        public override void CheckHit(Vector3 position, Transform target, bool isTrigger, Collider targetCollider)
        {
            if (alreadyHit.Contains(target))
            {
                return;
            }
            else
            {
                alreadyHit.Add(target);
            }
            if (UpgradePointsMod.instance != null)
            {

                float a = BaseDamage+velocitybonus;
                
                a *= UpgradePointsMod.instance.ArrowDmg;

                if (UpgradePointsMod.instance.specialUpgrades[28].bought)
                {
                    if (UpgradePointsMod.instance.specialUpgrades[49].bought)
                    {
                        if (UpgradePointsMod.instance.specialUpgrades[50].bought)
                        {
                            if (UpgradePointsMod.instance.specialUpgrades[36].bought)
                            {
                                if (UpgradePointsMod.instance.specialUpgrades[90].bought)
                                {
                                    if (UpgradePointsMod.instance.specialUpgrades[91].bought)
                                    {
                                        if (UpgradePointsMod.instance.specialUpgrades[92].bought)
                                        {
                                            if (UpgradePointsMod.instance.specialUpgrades[93].bought)
                                            {
                                                if (UpgradePointsMod.instance.specialUpgrades[94].bought)
                                                {
                                                    if (UpgradePointsMod.instance.specialUpgrades[95].bought)
                                                    {
                                                        a *= 35;
                                                    }
                                                    else
                                                    {
                                                        a *= 32.5f;

                                                    }
                                                }
                                                else
                                                {
                                                    a *= 30f;

                                                }
                                            }
                                            else
                                            {
                                                a *= 27f;

                                            }
                                        }
                                        else
                                        {
                                            a *= 24f;

                                        }
                                    }
                                    else
                                    {
                                        a *= 20f;

                                    }
                                }
                                else
                                {
                                    a *= 16f;

                                }
                            }
                            else
                            {
                                a *= 8f;

                            }
                        }
                        else
                        {
                            a *= 3f;

                        }
                    }
                    else
                    {
                        a *= 2f;

                    }
                    if (spearType)
                    {
                        if (UpgradePointsMod.instance.specialUpgrades[30].bought)
                        {

                            a *= 3.5f;

                        }
                    }
                    if (UpgradePointsMod.instance.specialUpgrades[29].bought)
                    {
                   if (UpgradePointsMod.instance.specialUpgrades[89].bought)
                            {
                            a *= 1 + UpgradePointsMod.instance.FocusFireStacks * 0.4f;

                        }
                        else
                        {
                            a *= 1 + UpgradePointsMod.instance.FocusFireStacks * 0.25f;

                        }
                    }
                    if (UpgradePointsMod.instance.specialUpgrades[7].bought)
                    {
                        if (StealthCheck.IsHidden)
                        {
                            if (UpgradePointsMod.instance.specialUpgrades[8].bought)
                            {
                                if (UpgradePointsMod.instance.specialUpgrades[10].bought)
                                {
                                    a *= 15;

                                }
                                else
                                {
                                    a *= 5;

                                }
                            }
                            else
                            {
                                a *= 2;

                            }
                        }
                    }
                    a *= BrawlerUpgrade.DamageBonus + 1;
                   
                    if (UpgradePointsMod.instance.specialUpgrades[33].bought)
                    {
                        float dist = Vector3.Distance(startingpos, target.position);
                        dist /= 20; // every 20 meters
                        a = a * (1 + dist);

                    }
                    if (UpgradePointsMod.instance.specialUpgrades[48].bought)
                    {
                        if (target.CompareTag("enemyCollide"))
                        {
                            if (UpgradePointsMod.instance.ComboStrikeBonusDamage(UpgradePointsMod.StrikeType.Ranged, target.root))
                            {
                                a *= 4;
                            }
                            UpgradePointsMod.instance.AddToComboStrike(UpgradePointsMod.StrikeType.Melee, target.root);
                        }
                    }
                    if (UpgradePointsMod.instance.specialUpgrades[44].bought)
                    {
                        if (CharacterControllerMod.FloatVelocity < 0.1f)
                        {
                            a *= 3;
                        }
                    }
                }
                a *= UpgradePointsMod.CritBonus();
                damage = Mathf.RoundToInt(a);
            }
            if (UpgradePointsMod.instance.specialUpgrades[29].bought)
            {
                if (UpgradePointsMod.instance.FocusFireTarget == target.gameObject)
                {
                    if (UpgradePointsMod.instance.specialUpgrades[88].bought)
                    {
                        UpgradePointsMod.instance.FocusFireStacks = Mathf.Clamp(UpgradePointsMod.instance.FocusFireStacks + 1, 0, 100);

                    }
                    else
                    {
                        UpgradePointsMod.instance.FocusFireStacks = Mathf.Clamp(UpgradePointsMod.instance.FocusFireStacks + 1, 0, 20);
                    }
                }
                else
                {
                    UpgradePointsMod.instance.FocusFireStacks = 0;
                    UpgradePointsMod.instance.FocusFireTarget = target.gameObject;
                }
            }
           // ModAPI.Console.Write("Ranged hitting " + target.parent.name);



            if (!ignoreCollisionEvents(targetCollider))
            {
                if (!isTrigger)
                {
                    Molotov componentInParent = ((Component)base.transform).GetComponentInParent<Molotov>();
                    if ((bool)componentInParent)
                    {
                        componentInParent.IncendiaryBreak();
                    }
                }
                bool headDamage = false;
                if (target.gameObject.layer == LayerMask.NameToLayer("Water"))
                {
                    FMODCommon.PlayOneshotNetworked(hitWaterEvent, base.transform, FMODCommon.NetworkRole.Any);
                }
                else if (target.CompareTag("SmallTree"))
                {
                    FMODCommon.PlayOneshotNetworked(hitBushEvent, base.transform, FMODCommon.NetworkRole.Any);
                }
                if (target.CompareTag("PlaneHull"))
                {
                    FMODCommon.PlayOneshotNetworked(hitMetalEvent, base.transform, FMODCommon.NetworkRole.Any);
                }
                if (target.CompareTag("Tree") || target.root.CompareTag("Tree") || target.CompareTag("Target"))
                {
                    if (spearType)
                    {
                        base.StartCoroutine(HitTree(hit.point - base.transform.forward * 2.1f));
                    }
                    else if (hitPointUpdated)
                    {
                        base.StartCoroutine(HitTree(hit.point - base.transform.forward * 0.35f));
                    }
                    else
                    {
                        base.StartCoroutine(HitTree(base.transform.position - base.transform.forward * 0.35f));
                    }
                    disableLive();
                    if (target.CompareTag("Tree") || target.root.CompareTag("Tree"))
                    {
                        TreeHealth component = ((Component)target).GetComponent<TreeHealth>();
                        if (!(bool)component)
                        {
                            component = ((Component)target.root).GetComponent<TreeHealth>();
                        }
                        if ((bool)component)
                        {
                            component.LodTree.AddTreeCutDownTarget(base.gameObject);
                        }
                    }
                }
                else if (target.CompareTag("enemyCollide") || target.tag == "lb_bird" || target.CompareTag("animalCollide") || target.CompareTag("Fish") || target.CompareTag("enemyRoot") || target.CompareTag("animalRoot"))
                {
                    UpgradePointsMod.instance.DoAreaDamage(target.transform.root, damage);

                    bool flag = target.tag == "lb_bird" || target.CompareTag("lb_bird");
                    bool flag2 = target.CompareTag("Fish");
                    bool flag3 = target.CompareTag("animalCollide") || target.CompareTag("animalRoot");
                    arrowStickToTarget arrowStickToTarget = ((Component)target).GetComponent<arrowStickToTarget>();
                    if (!(bool)arrowStickToTarget)
                    {
                        arrowStickToTarget = ((Component)target.root).GetComponentInChildren<arrowStickToTarget>();
                    }
                    if (!spearType && !flintLockAmmoType && !flag2)
                    {
                        if ((bool)arrowStickToTarget && arrowStickToTarget.enabled)
                        {
                            if (flag)
                            {
                                EventRegistry.Achievements.Publish(TfEvent.Achievements.BirdArrowKill, null);
                            }
                            arrowStickToTarget.CreatureType(flag3, flag, flag2);
                            if (BoltNetwork.isRunning)
                            {
                                if ((bool)at && (bool)at._boltEntity && at._boltEntity.isAttached && at._boltEntity.isOwner)
                                {
                                    headDamage = arrowStickToTarget.stickArrowToNearestBone(base.transform);
                                }
                            }
                            else
                            {
                                headDamage = arrowStickToTarget.stickArrowToNearestBone(base.transform);
                            }
                        }
                        base.Invoke("destroyMe", 0.1f);
                    }

                        base.StartCoroutine(HitAi(target, flag || flag3, headDamage));
                    if (flag2)
                    {
                        base.StartCoroutine(HitFish(target, hit.point - base.transform.forward * 0.35f));
                    }
                    disableLive();
                }
                else if (target.CompareTag("PlayerNet"))
                {
                    if (BoltNetwork.isRunning)
                    {
                        BoltEntity boltEntity = ((Component)target).GetComponentInParent<BoltEntity>();
                        if (!(bool)boltEntity)
                        {
                            boltEntity = ((Component)target).GetComponent<BoltEntity>();
                        }
                        if ((bool)boltEntity)
                        {
                            HitPlayer.Create(boltEntity, EntityTargets.OnlyOwner).Send();
                            disableLive();
                        }
                    }
                }
                else if (target.CompareTag("TerrainMain") && !LocalPlayer.IsInCaves)
                {
                    if (ignoreTerrain)
                    {
                        ignoreTerrain = false;
                        base.StartCoroutine(RevokeIgnoreTerrain());
                    }
                    else
                    {
                        if (spearType)
                        {
                            if ((bool)bodyCollider)
                            {
                                bodyCollider.isTrigger = true;
                            }
                            base.StartCoroutine(HitStructure(base.transform.position - base.transform.forward * 2.1f, false));
                        }
                        else
                        {
                            Vector3 position2 = base.transform.position - base.transform.forward * -0.8f;
                            float num = Terrain.activeTerrain.SampleHeight(base.transform.position);
                            Vector3 position3 = Terrain.activeTerrain.transform.position;
                            float num2 = num + position3.y;
                            Vector3 position4 = base.transform.position;
                            if (position4.y < num2)
                            {
                                position2.y = num2 + 0.5f;
                            }
                            base.StartCoroutine(HitStructure(position2, false));
                        }
                        disableLive();
                        FMODCommon.PlayOneshotNetworked(hitGroundEvent, base.transform, FMODCommon.NetworkRole.Any);
                    }
                }
                else if (target.CompareTag("structure") || target.CompareTag("jumpObject") || target.CompareTag("SLTier1") || target.CompareTag("SLTier2") || target.CompareTag("SLTier3") || target.CompareTag("UnderfootWood"))
                {
                    if ((bool)target.transform.parent)
                    {
                        if ((bool)((Component)target.transform.parent).GetComponent<StickFenceChunkArchitect>())
                        {
                            return;
                        }
                        if ((bool)((Component)target.transform.parent).GetComponent<BoneFenceChunkArchitect>())
                        {
                            return;
                        }
                    }
                    if (!isTrigger)
                    {
                        if (spearType)
                        {
                            base.StartCoroutine(HitStructure(hit.point - base.transform.forward * 2.1f, true));
                        }
                        else
                        {
                            base.StartCoroutine(HitStructure(hit.point - base.transform.forward * 0.35f, true));
                        }
                        disableLive();
                    }
                }
                else if (target.CompareTag("CaveDoor"))
                {
                    ignoreTerrain = true;
                    Physics.IgnoreCollision(base.GetComponent<Collider>(), ((Component)Terrain.activeTerrain).GetComponent<Collider>(), true);
                }
                else if (flintLockAmmoType && (target.CompareTag("BreakableWood") || target.CompareTag("BreakableRock")))
                {
                    target.SendMessage("Hit", 40, SendMessageOptions.DontRequireReceiver);
                }
                if (!Live)
                {
                    destroyThisAmmo();
                    parent.BroadcastMessage("OnArrowHit", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
    
}


    


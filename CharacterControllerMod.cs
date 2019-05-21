using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using UnityEngine;
namespace PlayerUpgradePoints
{
    class CharacterControllerMod : FirstPersonCharacter
    {
        public static float FloatVelocity;
        public float BaseRunMs;
        public float BaseJumpHeight;
        public float BaseSwimmingSpeed;
        public float BaseStaminaPerSec;
        public float BaseWalkSpeed;

        public static float DashCooldown;
        public static float DashMaxCooldown = 20f;

        public static float DashForce = 1500;


        private float WcD;
        private float ScD;
        private float AcD;
        private float DcD;

        
        protected override void Start()
        {
            
            base.Start();
            BaseRunMs = runSpeed;
            BaseJumpHeight = jumpHeight;
            BaseSwimmingSpeed = swimmingSpeed;
            BaseStaminaPerSec = staminaCostPerSec;
            BaseWalkSpeed = walkSpeed;
        }
        private Vector3 prevFramePos;

        protected override void Update()
        {
            if (UpgradePointsMod.instance != null)
            {
                runSpeed = BaseRunMs * UpgradePointsMod.instance.sprintspeed;
                swimmingSpeed = BaseSwimmingSpeed * UpgradePointsMod.instance.Diving;
                jumpHeight = BaseJumpHeight * UpgradePointsMod.instance.jumpheight;
                staminaCostPerSec = BaseStaminaPerSec * UpgradePointsMod.instance.sprintstaminause;
               
                if (PushingSled && UpgradePointsMod.instance.specialUpgrades[40].bought)
                {
                    walkSpeed = BaseWalkSpeed * 3;
                    runSpeed *= 3;
                    UpgradePointsMod.instance.PushingSled = true;
                }
                else
                {
                    UpgradePointsMod.instance.PushingSled = false;

                }
                FloatVelocity = velocity.magnitude;
                if (UpgradePointsMod.instance.specialUpgrades[47].bought)
                {
                    if (DashCooldown > 0)
                    {
                        DashCooldown -= Time.deltaTime;
                    }
                    if (WcD > 0)
                    {
                        WcD -= Time.deltaTime;

                    }
                    if (ScD > 0)
                    {
                        ScD -= Time.deltaTime;

                    }
                    if (AcD > 0)
                    {
                        AcD -= Time.deltaTime;

                    }
                    if (DcD > 0)
                    {
                        DcD -= Time.deltaTime;

                    }


                    if (DashCooldown <= 0)
                    {
                        if (UnityEngine.Input.GetKeyDown(KeyCode.W))
                        {
                            if (WcD > 0)
                            {
                                rb.AddForce(transform.forward * DashForce, ForceMode.Impulse);
                                DashCooldown = DashMaxCooldown;
                            }
                            else
                            {
                                WcD = 0.17f;
                            }

                        }
                        if (UnityEngine.Input.GetKeyDown(KeyCode.S))
                        {
                            if (ScD > 0)
                            {
                                rb.AddForce(transform.forward * -DashForce, ForceMode.Impulse);
                                DashCooldown = DashMaxCooldown;

                            }
                            else
                            {
                                ScD = 0.17f;
                            }

                        }
                        if (UnityEngine.Input.GetKeyDown(KeyCode.A))
                        {
                            if (AcD > 0)
                            {
                                rb.AddForce(transform.right * -DashForce, ForceMode.Impulse);
                                DashCooldown = DashMaxCooldown;

                            }
                            else
                            {
                                AcD = 0.17f;
                            }

                        }
                        if (UnityEngine.Input.GetKeyDown(KeyCode.D))
                        {
                            if (DcD > 0)
                            {
                                rb.AddForce(transform.right * DashForce, ForceMode.Impulse);
                                DashCooldown = DashMaxCooldown;

                            }
                            else
                            {
                                DcD = 0.17f;
                            }

                        }
                    }
                }
                base.Update();
                #region Quests
                float Dist = Vector3.Distance(transform.position, prevFramePos);
                if (Dist > 0)
                {
                    if (PushingSled)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.Sled, Dist);
                    }
                    if (swimming)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.Swim, Dist);
                    }
                    if (LocalPlayer.AnimControl.doShellRideMode)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.RideShell, Dist);

                    }
                    if (!Grounded)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.Fall, Dist);

                    }
                    if (!LocalPlayer.AnimControl.doShellRideMode && !swimming && running)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.Run, Dist);

                    }
                    if (LocalPlayer.SpecialActions.GetComponent<playerZipLineAction>()._onZipLine)
                    {
                        Quests.UpdateActiveQuests(QuestObjective.Type.Zipline, Dist);
                    }
                }
                if (LocalPlayer.IsInCaves)
                {
                    Quests.UpdateActiveQuests(QuestObjective.Type.StayInCave, Time.deltaTime,QuestObjective.Enemy.None,-1,QuestObjective.Weapon.None,"");
                }
                #endregion
                prevFramePos = transform.position;
            }

        }
        public override void HandleLanded()
        {
            LocalPlayer.CamFollowHead.stopAllCameraShake();
            fallShakeBlock = false;
            base.StopCoroutine("startJumpTimer");
            jumpTimerStarted = false;
            float num = 28f;
            bool flag = false;
            if (LocalPlayer.AnimControl.doShellRideMode && prevVelocityXZ.magnitude > 32f)
            {
                flag = true;
            }
            if (prevVelocity > num && !flag && allowFallDamage && jumpingTimer > 0.75f)
            {
                if (!jumpLand && !Clock.planecrash)
                {
                    jumpCoolDown = true;
                    jumpLand = true;
                    float num2 = prevVelocity * 0.9f * (prevVelocity / 27.5f);
                    int damage = (int)num2;
                    float num3 = 3.8f;
                    if (LocalPlayer.AnimControl.doShellRideMode)
                    {
                        num3 = 5f;
                    }
                    bool flag2 = false;
                    if (jumpingTimer > num3)
                    {
                        damage = 1000;
                        flag2 = true;
                    }
                    if (LocalPlayer.AnimControl.doShellRideMode && !flag2)
                    {
                        damage = 17;
                    }
                    Stats.Hit(Mathf.RoundToInt(damage*UpgradePointsMod.instance.falldamage), true, PlayerStats.DamageType.Physical);
                    BoltSetReflectedShim.SetBoolReflected(LocalPlayer.Animator, "jumpBool", false);
                    if (Stats.Health > 0f)
                    {
                        if (!LocalPlayer.ScriptSetup.pmControl.FsmVariables.GetFsmBool("doingJumpAttack").Value && !LocalPlayer.AnimControl.doShellRideMode)
                        {
                            BoltSetReflectedShim.SetIntegerReflected(LocalPlayer.Animator, "jumpType", 1);
                            LocalPlayer.Animator.SetTrigger("landHeavyTrigger");
                            BoltSetReflectedShim.SetBoolReflected(LocalPlayer.Animator, "jumpBool", false);
                            CanJump = false;
                            LocalPlayer.HitReactions.StartCoroutine("doHardfallRoutine");
                            prevMouseXSpeed = LocalPlayer.MainRotator.rotationSpeed;
                            LocalPlayer.MainRotator.rotationSpeed = 0.55f;
                            BoltSetReflectedShim.SetLayerWeightReflected(LocalPlayer.Animator, 4, 0f);
                            BoltSetReflectedShim.SetLayerWeightReflected(LocalPlayer.Animator, 0, 1f);
                            BoltSetReflectedShim.SetLayerWeightReflected(LocalPlayer.Animator, 1, 0f);
                            BoltSetReflectedShim.SetLayerWeightReflected(LocalPlayer.Animator, 2, 0f);
                            BoltSetReflectedShim.SetLayerWeightReflected(LocalPlayer.Animator, 3, 0f);
                            base.Invoke("resetAnimSpine", 1f);
                        }
                        else
                        {
                            jumpLand = false;
                            jumpCoolDown = false;
                        }
                    }
                    else
                    {
                        jumpCoolDown = false;
                        jumpLand = false;
                    }
                }
                blockJumpAttack();
            }
            jumping = false;
            base.CancelInvoke("setAnimatorJump");
            if (!jumpCoolDown)
            {
                BoltSetReflectedShim.SetIntegerReflected(LocalPlayer.Animator, "jumpType", 0);
                BoltSetReflectedShim.SetBoolReflected(LocalPlayer.Animator, "jumpBool", false);
                LocalPlayer.ScriptSetup.pmControl.SendEvent("toWait");
                blockJumpAttack();
            }
            base.CancelInvoke("fallDamageTimer");
            allowFallDamage = false;
        }
    }
}

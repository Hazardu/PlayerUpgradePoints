using Bolt;
using TheForest.Audio;
using TheForest.Buildings.World;
using TheForest.Utils;
using TheForest.World;
using UnityEngine;
using System.Collections;

namespace PlayerUpgradePoints
{
    public class WeaponInfoMod : weaponInfo
    {
        public static bool fdproc; //flying dragon upgrade proc
        public float baseanimspeed;
        public float BaseBlockPercent;
        public float BaseBlockStamina;

        //public static string weaponUsed;
        protected override void Start()
        {
            base.Start();
            fdproc = false;
            baseanimspeed = animSpeed;
            BaseBlockPercent = blockDamagePercent;
            BaseBlockStamina = blockStaminaDrain;

            StartCoroutine("UpdateStats");

        }
        IEnumerator UpdateStats()
        {
            while (true)
            {
                ResetStats();
                yield return new WaitForSeconds(5);
            }
        }


        private void TurnOffFD()
        {
            if (fdproc)
            {
                setup.pmStamina.FsmVariables.GetFsmFloat("tiredSpeed").Value /= 3;
                setup.pmStamina.FsmVariables.GetFsmFloat("notTiredSpeed").Value /= 3;
                fdproc = false;
            }
            ResetStats();
        }


        public override void ResetStats()
        {
            try
            {
                if (UpgradePointsMod.instance != null)
                {
                    weaponSpeed = baseWeaponSpeed;
                    weaponDamage = baseWeaponDamage;
                    tiredSpeed = baseTiredSpeed;
                    smashDamage = baseSmashDamage;
                    staminaDrain = baseStaminaDrain;
                    if (UpgradePointsMod.instance.AreaDamage > 0)
                    {
                        soundDetectRange = baseSoundDetectRange / UpgradePointsMod.instance.AreaDamage;
                    }
                    else
                    {
                        soundDetectRange = baseSoundDetectRange;
                    }
                    weaponRange = baseWeaponRange;
                    blockDamagePercent = BaseBlockPercent * UpgradePointsMod.instance.Block;
                    blockStaminaDrain = BaseBlockStamina / UpgradePointsMod.instance.Block;
                }
            }
            catch (System.Exception ex)
            {
                ModAPI.Log.Write(ex.ToString());
            }
        
        }
        protected override void setupHeldWeapon()
        {
            ModAPI.Console.Write(this.ToString() + " " + name + ", " + transform.parent.name + ", " + transform.root.name, "Held Weapon");
            if (transform.parent.name == "TennisRacketHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Tennis;

            }
            else if (transform.parent.name == "KatanaHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Katana;
            }
            else if (transform.parent.name == "RepairToolHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.RepairTool;
            }
            else if (transform.parent.name == "AxeHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.ModernAxe;
            }
            else if (transform.parent.name == "AxeCraftedHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.CraftedAxe;
            }
            else if (transform.parent.name == "ClubHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Club;
            }
            else if (transform.parent.name == "ClubCraftedHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.CraftedClub;
            }
            else if (transform.parent.name == "AxeHeldRusty")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.RustyAxe;
            }
            else if (transform.parent.name == "AxePlaneHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.PlaneAxe;
            }
            else if (transform.parent.name == "AxeClimbingHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.ClimbingAxe;
            }
            else if (transform.parent.name == "MacheteHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Machete;
            }
            else if (transform.parent.name == "stickHeldUpgraded")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Stick;
            }
            else if (transform.parent.name == "rabbitHeldDead")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Rabbit;
            }
            else if (transform.parent.name == "LizardHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Lizard;
            }
            else if (transform.parent.name == "CodHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Cod;
            }
            else if (transform.parent.name == "GenericMeatHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Meat;
            }
            else if (transform.parent.name == "char3_base_jnt")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Chainsaw;
            }
            else if (transform.parent.name == "turtleShellHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Shell;
            }
            else if (transform.parent.name == "SmallGenericMeatHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.SmallMeat;
            }
            else if (transform.parent.name == "RockHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Rock;
            }
            else if (transform.parent.name == "SkullHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Skull;
            }
            else if (transform.parent.name == "HeadHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Head;
            }
            else if (transform.parent.name == "legHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Leg;
            }
            else if (transform.parent.name == "armHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Arm;
            }
            else if (transform.parent.name == "spearHeldUpgraded")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.UpgSpear;
            }
            else if (transform.parent.name == "spearHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.WeakSpear;
            }
            else if (transform.parent.name == "boneHeld")
            {
                Quests.HeldWeapon = QuestObjective.Weapon.Bone;
            }
            else
            {
                Quests.HeldWeapon = QuestObjective.Weapon.None;
            }
            if (!mainTrigger && !remotePlayer)
            {
                if ((bool)mainTriggerScript)
                {
                    mainTriggerScript.currentWeaponScript = this;
                    mainTriggerScript.weaponAudio = base.transform;
                    setupMainTrigger();
                    if (canDoGroundAxeChop)
                    {
                        mainTriggerScript.enableSpecialWeaponVars();
                    }
                }
                if (!(bool)thisCollider && (bool)base.transform.parent)
                {
                    thisCollider = base.transform.parent.GetComponentsInChildren<Collider>(true)[0];
                }
                if (sendColliderToEvents && (bool)thisCollider)
                {
                    setup.events.heldWeaponCollider = thisCollider;
                }
                base.Invoke("cleanUpSpearedFish", 0.2f);
                if ((bool)animator)
                {
                    checkBurnableCloth();
                }
                animSpeed = Mathf.Clamp(weaponSpeed, 0.01f, 20f) / 20f;
                animSpeed += 0.5f;
                if (UpgradePointsMod.instance != null)
                    animSpeed *= UpgradePointsMod.instance.attackspeed;
                
                animTiredSpeed = tiredSpeed / 10f * (animSpeed - 0.5f);
                animTiredSpeed += 0.5f;
                if ((bool)setup)
                {
                    if ((bool)setup.pmStamina)
                    {
                        if (animControl.tiredCheck)
                        {
                            animControl.tempTired = animSpeed;
                            setup.pmStamina.FsmVariables.GetFsmFloat("notTiredSpeed").Value = animSpeed;
                        }
                        else
                        {
                            setup.pmStamina.FsmVariables.GetFsmFloat("notTiredSpeed").Value = animSpeed;
                        }
                    }
                    if ((bool)setup && (bool)setup.pmStamina)
                    {
                        setup.pmStamina.FsmVariables.GetFsmFloat("tiredSpeed").Value = animTiredSpeed;
                    }
                    if ((bool)setup && (bool)setup.pmControl)
                    {
                        setup.pmControl.FsmVariables.GetFsmFloat("staminaDrain").Value = staminaDrain * -1f;
                    }
                    if ((bool)setup && (bool)setup.pmControl)
                    {
                        setup.pmControl.FsmVariables.GetFsmFloat("blockStaminaDrain").Value = blockStaminaDrain * -1f;
                    }
                    if ((bool)LocalPlayer.Stats)
                    {
                        LocalPlayer.Stats.blockDamagePercent = blockDamagePercent;
                    }
                    damageAmount = (int)WeaponDamage;
                    if ((bool)setup && (bool)setup.pmStamina)
                    {
                        setup.pmStamina.SendEvent("toSetStats");
                    }
                }
            }
        }







        protected override void OnTriggerEnter(Collider other)
        {
            
            if (!other.gameObject.CompareTag("Player") && animator.GetCurrentAnimatorStateInfo(2).tagHash != animControl.deathHash)
            {
                if (other.CompareTag("hanging") || other.CompareTag("corpseProp"))
                {
                    if (animControl.smashBool)
                    {
                        if (LocalPlayer.Animator.GetFloat("tiredFloat") < 0.35f)
                        {
                            base.Invoke("spawnSmashWeaponBlood", 0.1f);
                        }
                        else
                        {
                            base.Invoke("spawnSmashWeaponBlood", 0.03f);
                        }
                    }
                    else
                    {
                        spawnWeaponBlood(other, false);
                    }
                    Mood.HitRumble();
                    other.gameObject.SendMessageUpwards("Hit", 0, SendMessageOptions.DontRequireReceiver);
                    FauxMpHit(0);
                    FMODCommon.PlayOneshotNetworked(currentWeaponScript.fleshHitEvent, base.transform, FMODCommon.NetworkRole.Any);
                }
                if (!ForestVR.Enabled && GetInvalidAttackAngle(other))
                {
                    return;
                }
                PlayerHitEnemy playerHitEnemy = null;
                try
                {
                    if (mainTrigger && repairTool)
                    {
                        RepairTool component = currentWeaponScript.gameObject.GetComponent<RepairTool>();
                        if ((bool)component && component.IsRepairFocused)
                        {
                            currentWeaponScript.gameObject.SendMessage("OnRepairStructure", other.gameObject);
                            if ((bool)component.FocusedRepairCollider)
                            {
                                currentWeaponScript.PlaySurfaceHit(component.FocusedRepairCollider, SfxInfo.SfxTypes.HitWood);
                            }
                        }
                        goto end_IL_0108;
                    }
                    mutantTargetSwitching component2 = ((Component)other.transform).GetComponent<mutantTargetSwitching>();
                    if ((other.CompareTag("enemyCollide") || other.CompareTag("animalCollide") || other.CompareTag("Fish") || other.CompareTag("EnemyBodyPart")) && (mainTrigger || animControl.smashBool || chainSaw))
                    {
                        bool flag = false;
                        if ((bool)component2 && component2.regular)
                        {
                            flag = true;
                        }
                        if (animControl.smashBool)
                        {
                            if (LocalPlayer.Animator.GetFloat("tiredFloat") < 0.35f)
                            {
                                base.Invoke("spawnSmashWeaponBlood", 0.1f);
                            }
                            else
                            {
                                base.Invoke("spawnSmashWeaponBlood", 0.03f);
                            }
                        }
                        else if (!flag)
                        {
                            spawnWeaponBlood(other, false);
                        }
                    }
                    if (other.gameObject.CompareTag("PlayerNet") && (mainTrigger || (!mainTrigger && (animControl.smashBool || chainSaw))))
                    {
                        BoltEntity component3 = ((Component)other).GetComponent<BoltEntity>();
                        BoltEntity component4 = base.GetComponent<BoltEntity>();
                        if (!object.ReferenceEquals(component3, component4) && lastPlayerHit + 0.4f < Time.time)
                        {
                            other.transform.root.SendMessage("getClientHitDirection", animator.GetInteger("hitDirection"), SendMessageOptions.DontRequireReceiver);
                            other.transform.root.SendMessage("StartPrediction", SendMessageOptions.DontRequireReceiver);
                            lastPlayerHit = Time.time;
                            if (BoltNetwork.isRunning)
                            {
                                HitPlayer.Create(component3, EntityTargets.Everyone).Send();
                            }
                        }
                        goto end_IL_0108;
                    }
                    if (BoltNetwork.isClient)
                    {
                        playerHitEnemy = PlayerHitEnemy.Create(GlobalTargets.OnlyServer);
                        playerHitEnemy.Target = ((Component)other).GetComponentInParent<BoltEntity>();
                    }
                    if (other.gameObject.CompareTag("enemyHead") && !mainTrigger)
                    {
                        other.transform.SendMessageUpwards("HitHead", SendMessageOptions.DontRequireReceiver);
                        if (playerHitEnemy != null)
                        {
                            playerHitEnemy.HitHead = true;
                        }
                    }
                    if (other.gameObject.CompareTag("enemyCollide") && !mainTrigger && !animControl.smashBool && !repairTool)
                    {
                        other.transform.SendMessage("getSkinHitPosition", base.transform, SendMessageOptions.DontRequireReceiver);
                    }
                    if (other.gameObject.CompareTag("structure") && !repairTool)
                    {
                        setup.pmNoise.SendEvent("toWeaponNoise");
                        Mood.HitRumble();
                        other.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
                        float damage = WeaponDamage * 4f;
                        if (tht.atEnemy)
                        {
                            damage = WeaponDamage / 2f;
                        }
                        other.SendMessage("LocalizedHit", new LocalizedHitData(base.transform.position, damage), SendMessageOptions.DontRequireReceiver);
                    }
                    if (BoltNetwork.isClient && (other.CompareTag("jumpObject") || other.CompareTag("UnderfootWood")) && !repairTool)
                    {
                        FauxMpHit(Mathf.CeilToInt(WeaponDamage * 4f));
                    }
                    switch (other.gameObject.tag)
                    {
                        case "jumpObject":
                        case "UnderfootWood":
                        case "SLTier1":
                        case "SLTier2":
                        case "SLTier3":
                        case "UnderfootRock":
                        case "Target":
                        case "Untagged":
                        case "Block":
                            if (!repairTool)
                            {
                                other.SendMessage("LocalizedHit", new LocalizedHitData(base.transform.position, WeaponDamage * 4f), SendMessageOptions.DontRequireReceiver);
                                setup.pmNoise.SendEvent("toWeaponNoise");
                            }
                            break;
                    }
                    PlaySurfaceHit(other, SfxInfo.SfxTypes.None);
                    if (spear && other.gameObject.CompareTag("Fish") && ((Object)MyFish == (Object)null || !MyFish.gameObject.activeSelf) && !mainTrigger)
                    {
                        base.transform.parent.SendMessage("GotBloody", SendMessageOptions.DontRequireReceiver);
                        FMODCommon.PlayOneshotNetworked(fleshHitEvent, base.transform, FMODCommon.NetworkRole.Any);
                        spearedFish.Add(other.gameObject);
                        other.transform.parent = base.transform;
                        other.transform.position = SpearTip.position;
                        other.transform.rotation = SpearTip.rotation;
                        MyFish = ((Component)other.transform).GetComponent<Fish>();
                        if ((bool)MyFish && MyFish.typeCaveFish)
                        {
                            other.transform.position = SpearTip2.position;
                            other.transform.rotation = SpearTip2.rotation;
                        }
                        other.SendMessage("DieSpear", SendMessageOptions.DontRequireReceiver);
                    }
                    if (other.gameObject.CompareTag("hanging") || other.gameObject.CompareTag("corpseProp") || (other.gameObject.CompareTag("BreakableWood") && !mainTrigger))
                    {
                        Rigidbody component5 = ((Component)other).GetComponent<Rigidbody>();
                        float d = pushForce;
                        if (other.gameObject.CompareTag("BreakableWood"))
                        {
                            d = 4500f;
                        }
                        if ((bool)component5)
                        {
                            component5.AddForceAtPosition(playerTr.forward * d * 0.75f * (0.016666f / Time.fixedDeltaTime), base.transform.position, ForceMode.Force);
                        }
                        if (!(bool)other.gameObject.GetComponent<WeaponHitSfxInfo>() && (other.gameObject.CompareTag("hanging") || other.gameObject.CompareTag("corpseProp")))
                        {
                            FMODCommon.PlayOneshotNetworked(currentWeaponScript.fleshHitEvent, weaponAudio.transform, FMODCommon.NetworkRole.Any);
                        }
                    }
                    if (spear && !mainTrigger && (other.gameObject.CompareTag("Water") || other.gameObject.CompareTag("Ocean")))
                    {
                        if (!LocalPlayer.ScriptSetup.targetInfo.inYacht)
                        {
                            PlayGroundHit(waterHitEvent);
                            base.StartCoroutine(spawnSpearSplash(other));
                        }
                        setup.pmNoise.SendEvent("toWeaponNoise");
                    }
                    if (!spear && !mainTrigger && (other.gameObject.CompareTag("Water") || other.gameObject.CompareTag("Ocean")) && !LocalPlayer.ScriptSetup.targetInfo.inYacht)
                    {
                        PlayGroundHit(waterHitEvent);
                    }
                    if (other.gameObject.CompareTag("Shell") && !mainTrigger)
                    {
                        other.gameObject.SendMessage("getAttackerType", 4, SendMessageOptions.DontRequireReceiver);
                        other.gameObject.SendMessage("getAttacker", Player, SendMessageOptions.DontRequireReceiver);
                        Mood.HitRumble();
                        other.transform.SendMessageUpwards("Hit", 1, SendMessageOptions.DontRequireReceiver);
                        PlayEvent(currentWeaponScript.shellHitEvent, weaponAudio);
                    }
                    if (other.gameObject.CompareTag("PlaneHull") && !mainTrigger)
                    {
                        PlayEvent(currentWeaponScript.planeHitEvent, weaponAudio);
                    }
                    if (other.gameObject.CompareTag("Tent") && !mainTrigger)
                    {
                        PlayEvent(currentWeaponScript.tentHitEvent, weaponAudio);
                    }
                    mutantHitReceiver component6 = ((Component)other).GetComponent<mutantHitReceiver>();
                    if ((other.gameObject.CompareTag("enemyCollide") || other.gameObject.CompareTag("animalCollide")) && mainTrigger && !enemyDelay && !animControl.smashBool)
                    {
                       
                            if (UpgradePointsMod.instance.specialUpgrades[2].bought)
                            {
                                LocalPlayer.Stats.Stamina += 5;
                            }
                            if (UpgradePointsMod.instance.specialUpgrades[35].bought)
                            {
                                if (fdproc == false)
                                {
                                    float r = Random.value;
                                    if (r >= 0.75f)
                                    {
                                        fdproc = true;
                                        setup.pmStamina.FsmVariables.GetFsmFloat("notTiredSpeed").Value *= 3;
                                        setup.pmStamina.FsmVariables.GetFsmFloat("tiredSpeed").Value *= 3;

                                        Invoke("TurnOffFD", 10);

                                    }
                                }
                            }
                        
                        if (BoltNetwork.isClient && other.gameObject.CompareTag("enemyCollide"))
                        {
                            CoopMutantClientHitPrediction componentInChildren = other.transform.root.gameObject.GetComponentInChildren<CoopMutantClientHitPrediction>();
                            if ((bool)componentInChildren)
                            {
                                componentInChildren.getClientHitDirection(animator.GetInteger("hitDirection"));
                                componentInChildren.StartPrediction();
                            }
                        }
                        if ((bool)currentWeaponScript)
                        {
                            currentWeaponScript.transform.parent.SendMessage("GotBloody", SendMessageOptions.DontRequireReceiver);
                        }
                        Vector3 vector = other.transform.root.GetChild(0).InverseTransformPoint(playerTr.position);
                        float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
                        other.gameObject.SendMessage("getAttackerType", 4, SendMessageOptions.DontRequireReceiver);
                        other.gameObject.SendMessage("getAttacker", Player, SendMessageOptions.DontRequireReceiver);
                        if (playerHitEnemy != null)
                        {
                            playerHitEnemy.getAttackerType = 4;
                        }
                        animator.SetFloatReflected("connectFloat", 1f);
                        base.Invoke("resetConnectFloat", 0.3f);
                        if (num < -140f || num > 140f)
                        {
                            if ((bool)component6)
                            {
                                component6.takeDamage(1);
                            }
                            else
                            {
                                other.transform.SendMessageUpwards("takeDamage", 1, SendMessageOptions.DontRequireReceiver);
                            }
                            if (playerHitEnemy != null)
                            {
                                playerHitEnemy.takeDamage = 1;
                            }
                        }
                        else
                        {
                            if ((bool)component6)
                            {
                                component6.takeDamage(0);
                            }
                            else
                            {
                                other.transform.SendMessageUpwards("takeDamage", 0, SendMessageOptions.DontRequireReceiver);
                            }
                            if (playerHitEnemy != null)
                            {
                                playerHitEnemy.takeDamage = 0;
                            }
                        }
                        if (spear || shell || chainSaw)
                        {
                            other.transform.SendMessageUpwards("getAttackDirection", 3, SendMessageOptions.DontRequireReceiver);
                            if (playerHitEnemy != null)
                            {
                                playerHitEnemy.getAttackDirection = 3;
                            }
                        }
                        else if (axe || rock || stick)
                        {
                            int integer = animator.GetInteger("hitDirection");
                            if (axe)
                            {
                                if ((bool)component6)
                                {
                                    component6.getAttackDirection(integer);
                                    component6.getStealthAttack();
                                }
                                else
                                {
                                    other.transform.SendMessageUpwards("getAttackDirection", integer, SendMessageOptions.DontRequireReceiver);
                                    other.transform.SendMessageUpwards("getStealthAttack", SendMessageOptions.DontRequireReceiver);
                                }
                            }
                            else if (stick)
                            {
                                if ((bool)component6)
                                {
                                    component6.getAttackDirection(integer);
                                }
                                else
                                {
                                    other.transform.SendMessageUpwards("getAttackDirection", integer, SendMessageOptions.DontRequireReceiver);
                                }
                            }
                            else if ((bool)component6)
                            {
                                component6.getAttackDirection(0);
                                component6.getStealthAttack();
                            }
                            else
                            {
                                other.transform.SendMessageUpwards("getAttackDirection", 0, SendMessageOptions.DontRequireReceiver);
                                other.transform.SendMessageUpwards("getStealthAttack", SendMessageOptions.DontRequireReceiver);
                            }
                            if (playerHitEnemy != null)
                            {
                                if (axe)
                                {
                                    playerHitEnemy.getAttackDirection = integer;
                                }
                                else if (stick)
                                {
                                    playerHitEnemy.getAttackDirection = integer;
                                }
                                else
                                {
                                    playerHitEnemy.getAttackDirection = 0;
                                }
                                playerHitEnemy.getStealthAttack = true;
                            }
                        }
                        else
                        {
                            int integer2 = animator.GetInteger("hitDirection");
                            if ((bool)component6)
                            {
                                component6.getAttackDirection(integer2);
                            }
                            else
                            {
                                other.transform.SendMessageUpwards("getAttackDirection", integer2, SendMessageOptions.DontRequireReceiver);
                            }
                            if (playerHitEnemy != null)
                            {
                                playerHitEnemy.getAttackDirection = integer2;
                            }
                        }
                        if (fireStick && Random.value > 0.8f)
                        {
                            if ((bool)component6)
                            {
                                component6.Burn();
                            }
                            else
                            {
                                other.transform.SendMessageUpwards("Burn", SendMessageOptions.DontRequireReceiver);
                            }
                            if (playerHitEnemy != null)
                            {
                                playerHitEnemy.Burn = true;
                            }
                        }
                        float num2 = weaponDamage;
                        
                     
                        if ((bool)component2 && chainSaw && (component2.typeMaleCreepy || component2.typeFemaleCreepy || component2.typeFatCreepy))
                        {
                            num2 = weaponDamage / 2f;
                        }
                        if (UpgradePointsMod.instance.specialUpgrades[38].bought) {
                            num2 += PlayerStatsMod.DrainedStamina;
                            UpgradePointsMod.instance.ClearLastStaminaUsed();
                        }
                        if (UpgradePointsMod.instance.specialUpgrades[55].bought)
                        {
                            num2 += UpgradePointsMod.instance.RevengeDamage;
                            UpgradePointsMod.instance.StartClearingRevenge();
                        }
                        num2 *= UpgradePointsMod.instance.weaponDmg;
                        num2 *= UpgradePointsMod.CritBonus();
                        UpgradePointsMod.instance.ClearLastStaminaUsed();
                        UpgradePointsMod.instance.StartClearingRevenge();
                        if (UpgradePointsMod.instance.specialUpgrades[44].bought)
                        {
                            if (CharacterControllerMod.FloatVelocity < 0.1f)
                            {
                                num2 *= 3f;
                            }
                        }
                        if (UpgradePointsMod.instance.specialUpgrades[99].bought)
                        {
                            num2 *= 1 + ((100 - LocalPlayer.Stats.Stamina) * 0.1f);
                        }
                        if (UpgradePointsMod.instance.specialUpgrades[51].bought)
                        {
                            if (UpgradePointsMod.instance.specialUpgrades[52].bought)
                            {
                                if (UpgradePointsMod.instance.specialUpgrades[53].bought)
                                {
                                    if (UpgradePointsMod.instance.specialUpgrades[54].bought)
                                    {
                                        if (UpgradePointsMod.instance.specialUpgrades[72].bought)
                                        {
                                            if (UpgradePointsMod.instance.specialUpgrades[73].bought)
                                            {
                                                if (UpgradePointsMod.instance.specialUpgrades[74].bought)
                                                {
                                                    num2 *= 30;
                                                }
                                                else
                                                {
                                                    num2 *= 25;
                                                }
                                            }
                                            else
                                            {
                                                num2 *= 20;
                                            }
                                        }
                                        else
                                        {
                                            num2 *= 15;
                                        }
                                    }
                                    else
                                    {
                                        num2 *= 10;
                                    }
                                }
                                else
                                {
                                    num2 *= 5;
                                }
                            }
                            else
                            {
                                num2 *= 2.5f;
                            }
                        }
                        // ModAPI.Console.Write("Melee hitting " + other.transform.parent.name);

                        num2 *= BrawlerUpgrade.DamageBonus + 1;


                        if (UpgradePointsMod.instance.specialUpgrades[48].bought)
                        {
                            if (other.CompareTag("enemyCollide"))
                            {
                                if (UpgradePointsMod.instance.ComboStrikeBonusDamage(UpgradePointsMod.StrikeType.Melee, other.transform.root))
                                {
                                    num2 *= 4;
                                }
                                UpgradePointsMod.instance.AddToComboStrike(UpgradePointsMod.StrikeType.Ranged, other.transform.root);
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
                                        num2 *= 15;
                                    }
                                    else
                                    {
                                        num2 *= 5;
                                    }
                                }
                                else
                                {
                                    num2 *= 2;
                                }
                            }
                        }
                        float lifestolen = 0;
                        if (hitReactions.kingHitBool || fsmHeavyAttackBool.Value)
                        {
                            if ((bool)component6)
                            {
                                if (UpgradePointsMod.instance.specialUpgrades[1].bought)
                                {
                                    if (fsmHeavyAttackBool.Value && axe && !smallAxe)
                                    {
                                        UpgradePointsMod.instance.DoAreaDamage(other.transform.root, num2 * 18);
                                        component6.sendHitFallDown(num2 * 18f);
                                        if (playerHitEnemy != null)
                                        {
                                            playerHitEnemy.Hit = (int)(num2 * 18);
                                            playerHitEnemy.hitFallDown = true;
                                            lifestolen = num2 * 18;
                                        }
                                    }
                                    else
                                    {
                                        UpgradePointsMod.instance.DoAreaDamage(other.transform.root, num2 * 18);

                                        component6.getCombo(3);
                                        component6.hitRelay((int)num2 * 18);
                                        lifestolen = num2 * 18;
                                    }
                                }
                                else
                                {
                                    if (fsmHeavyAttackBool.Value && axe && !smallAxe)
                                    {
                                        UpgradePointsMod.instance.DoAreaDamage(other.transform.root, num2 * 3);

                                        component6.sendHitFallDown(num2 * 3f);
                                        if (playerHitEnemy != null)
                                        {
                                            playerHitEnemy.Hit = (int)num2 * 3;
                                            playerHitEnemy.hitFallDown = true;
                                            lifestolen = num2 * 3;
                                        }
                                    }
                                    else
                                    {
                                        UpgradePointsMod.instance.DoAreaDamage(other.transform.root, num2 * 3);

                                        component6.getCombo(3);
                                        component6.hitRelay((int)num2 * 3);
                                        lifestolen = num2 * 3;

                                    }
                                }
                               
                            }
                            else
                            {
                                UpgradePointsMod.instance.DoAreaDamage(other.transform.root, num2 * 3);

                                int animalHitDirection = animalHealth.GetAnimalHitDirection(num);
                                other.transform.SendMessageUpwards("getCombo", 3, SendMessageOptions.DontRequireReceiver);
                                other.transform.SendMessageUpwards("ApplyAnimalSkinDamage", animalHitDirection, SendMessageOptions.DontRequireReceiver);
                                other.transform.SendMessageUpwards("Hit", (int)num2 * 3, SendMessageOptions.DontRequireReceiver);
                                if (playerHitEnemy != null)
                                {
                                    playerHitEnemy.getAttackDirection = animalHitDirection;
                                }
                            }
                            if (playerHitEnemy != null)
                            {
                                UpgradePointsMod.instance.DoAreaDamage(other.transform.root, num2 * 3);

                                playerHitEnemy.Hit = (int)num2 * 3;
                                playerHitEnemy.getCombo = 3;
                                lifestolen = num2 * 3;
                            }
                            Mood.HitRumble();
                            FMODCommon.PlayOneshotNetworked(currentWeaponScript.fleshHitEvent, weaponAudio.transform, FMODCommon.NetworkRole.Any);
                        }
                        else
                        {
                            if ((bool)component6)
                            {
                                component6.hitRelay((int)num2);
                                lifestolen = num2;
                                UpgradePointsMod.instance.DoAreaDamage(other.transform.root, num2);

                            }
                            else
                            {
                                UpgradePointsMod.instance.DoAreaDamage(other.transform.root, num2);

                                int animalHitDirection2 = animalHealth.GetAnimalHitDirection(num);
                                other.transform.SendMessageUpwards("ApplyAnimalSkinDamage", animalHitDirection2, SendMessageOptions.DontRequireReceiver);
                                other.transform.SendMessageUpwards("Hit", (int)num2, SendMessageOptions.DontRequireReceiver);
                                if (playerHitEnemy != null)
                                {
                                    playerHitEnemy.getAttackDirection = animalHitDirection2;
                                    lifestolen = num2;

                                }
                            }
                            Mood.HitRumble();
                            if (playerHitEnemy != null)
                            {
                                playerHitEnemy.Hit = (int)num2;
                                lifestolen = num2;
                                UpgradePointsMod.instance.DoAreaDamage(other.transform.root, num2);

                            }
                            FMODCommon.PlayOneshotNetworked(currentWeaponScript.fleshHitEvent, weaponAudio.transform, FMODCommon.NetworkRole.Any);
                        }
                        if (UpgradePointsMod.instance.specialUpgrades[6].bought)
                        {
                            lifestolen *= 0.2f;
                            LocalPlayer.Stats.HealthTarget += lifestolen;
                            LocalPlayer.Stats.Health += lifestolen;
                        }
                        if ((bool)component2 && chainSaw && (component2.typeMaleCreepy || component2.typeFemaleCreepy || component2.typeFatCreepy))


                            setup.pmNoise.SendEvent("toWeaponNoise");
                        hitReactions.enableWeaponHitState();
                        animControl.hitCombo();
                        if (!axe && !rock)
                        {
                            goto IL_10fc;
                        }
                        if (animator.GetBool("smallAxe"))
                        {
                            goto IL_10fc;
                        }
                        goto IL_110c;
                    }
                    goto IL_11b9;
                    IL_11b9:
                    if ((other.CompareTag("suitCase") || other.CompareTag("metalProp")) && animControl.smashBool)
                    {
                        other.transform.SendMessage("Hit", smashDamage, SendMessageOptions.DontRequireReceiver);
                        Mood.HitRumble();
                        if (playerHitEnemy != null)
                        {
                            playerHitEnemy.Hit = (int)smashDamage;
                        }
                        if (BoltNetwork.isRunning && other.CompareTag("suitCase"))
                        {
                            OpenSuitcase openSuitcase = OpenSuitcase.Create(GlobalTargets.Others);
                            openSuitcase.Position = base.GetComponent<Collider>().transform.position;
                            openSuitcase.Damage = (int)smashDamage;
                            openSuitcase.Send();
                        }
                        if (smashSoundEnabled)
                        {
                            smashSoundEnabled = false;
                            base.Invoke("EnableSmashSound", 0.3f);
                            PlayEvent(smashHitEvent, null);
                            if (BoltNetwork.isRunning)
                            {
                                FmodOneShot fmodOneShot = FmodOneShot.Create(GlobalTargets.Others, ReliabilityModes.Unreliable);
                                fmodOneShot.EventPath = CoopAudioEventDb.FindId(smashHitEvent);
                                fmodOneShot.Position = base.transform.position;
                                fmodOneShot.Send();
                            }
                        }
                        setup.pmNoise.SendEvent("toWeaponNoise");
                        hitReactions.enableWeaponHitState();
                        if (other.CompareTag("metalProp"))
                        {
                            Rigidbody component7 = ((Component)other).GetComponent<Rigidbody>();
                            if ((bool)component7)
                            {
                                component7.AddForceAtPosition((Vector3.down + LocalPlayer.Transform.forward * 0.2f) * pushForce * 2f * (0.016666f / Time.fixedDeltaTime), base.transform.position, ForceMode.Force);
                            }
                        }
                    }
                    if ((other.CompareTag("enemyCollide") || other.CompareTag("lb_bird") || other.CompareTag("animalCollide") || other.CompareTag("Fish") || other.CompareTag("EnemyBodyPart")) && !mainTrigger && !enemyDelay && (animControl.smashBool || chainSaw))
                    {
                        float num3 = smashDamage;
                        if (chainSaw && !mainTrigger)
                        {
                            base.StartCoroutine(chainSawClampRotation(0.25f));
                            num3 = smashDamage / 2f;
                        }
                        if (other.CompareTag("enemyCollide"))
                        {
                            if (UpgradePointsMod.instance.specialUpgrades[38].bought)
                            {
                                num3 += PlayerStatsMod.DrainedStamina;
                                UpgradePointsMod.instance.ClearLastStaminaUsed();
                            }
                            if (UpgradePointsMod.instance.specialUpgrades[55].bought)
                            {
                                num3 += UpgradePointsMod.instance.RevengeDamage;
                                UpgradePointsMod.instance.StartClearingRevenge();
                            }
                            num3 *= UpgradePointsMod.instance.weaponDmg;
                            num3 *= UpgradePointsMod.CritBonus();
                            UpgradePointsMod.instance.ClearLastStaminaUsed();
                            UpgradePointsMod.instance.StartClearingRevenge();
                            if (UpgradePointsMod.instance.specialUpgrades[44].bought)
                            {
                                if (CharacterControllerMod.FloatVelocity < 0.1f)
                                {
                                    num3 *= 3f;
                                }
                            }
                            if (UpgradePointsMod.instance.specialUpgrades[99].bought)
                            {
                                num3 *= 1 + ((100 - LocalPlayer.Stats.Stamina) * 0.1f);
                            }
                            if (UpgradePointsMod.instance.specialUpgrades[51].bought)
                            {
                                if (UpgradePointsMod.instance.specialUpgrades[52].bought)
                                {
                                    if (UpgradePointsMod.instance.specialUpgrades[53].bought)
                                    {
                                        if (UpgradePointsMod.instance.specialUpgrades[54].bought)
                                        {
                                            if (UpgradePointsMod.instance.specialUpgrades[72].bought)
                                            {
                                                if (UpgradePointsMod.instance.specialUpgrades[73].bought)
                                                {
                                                    if (UpgradePointsMod.instance.specialUpgrades[74].bought)
                                                    {
                                                        num3 *= 30;
                                                    }
                                                    else
                                                    {
                                                        num3 *= 25;
                                                    }
                                                }
                                                else
                                                {
                                                    num3 *= 20;
                                                }
                                            }
                                            else
                                            {
                                                num3 *= 15;
                                            }
                                        }
                                        else
                                        {
                                            num3 *= 10;
                                        }
                                    }
                                    else
                                    {
                                        num3 *= 5;
                                    }
                                }
                                else
                                {
                                    num3 *= 2.5f;
                                }
                            }
                            // ModAPI.Console.Write("Melee hitting " + other.transform.parent.name);

                            num3 *= BrawlerUpgrade.DamageBonus + 1;


                            if (UpgradePointsMod.instance.specialUpgrades[48].bought)
                            {
                               
                                    if (UpgradePointsMod.instance.ComboStrikeBonusDamage(UpgradePointsMod.StrikeType.Melee, other.transform.root))
                                    {
                                        num3 *= 4;
                                    }
                                    UpgradePointsMod.instance.AddToComboStrike(UpgradePointsMod.StrikeType.Ranged, other.transform.root);
                               
                            }
                            if (UpgradePointsMod.instance.specialUpgrades[7].bought)
                            {
                                if (StealthCheck.IsHidden)
                                {
                                    if (UpgradePointsMod.instance.specialUpgrades[8].bought)
                                    {
                                        if (UpgradePointsMod.instance.specialUpgrades[10].bought)
                                        {
                                            num3 *= 15;
                                        }
                                        else
                                        {
                                            num3 *= 5;
                                        }
                                    }
                                    else
                                    {
                                        num3 *= 2;
                                    }
                                }
                            }
                        }
                        base.transform.parent.SendMessage("GotBloody", SendMessageOptions.DontRequireReceiver);
                        enemyDelay = true;
                        base.Invoke("resetEnemyDelay", 0.25f);
                        if ((rock || stick || spear || noBodyCut) && !allowBodyCut)
                        {
                            other.transform.SendMessageUpwards("ignoreCutting", SendMessageOptions.DontRequireReceiver);
                        }
                        other.transform.SendMessage("getSkinHitPosition", base.transform, SendMessageOptions.DontRequireReceiver);
                        other.transform.SendMessage("hitSuitCase", num3, SendMessageOptions.DontRequireReceiver);
                        other.gameObject.SendMessage("getAttacker", Player, SendMessageOptions.DontRequireReceiver);
                        other.gameObject.SendMessage("getAttackerType", 4, SendMessageOptions.DontRequireReceiver);
                        if (fsmJumpAttackBool.Value && LocalPlayer.FpCharacter.jumpingTimer > 1.2f && !chainSaw)
                        {
                            other.transform.SendMessageUpwards("Explosion", -1, SendMessageOptions.DontRequireReceiver);
                            if (BoltNetwork.isRunning)
                            {
                                playerHitEnemy.explosion = true;
                            }
                        }
                        else if (!other.gameObject.CompareTag("Fish"))
                        {
                            if (other.gameObject.CompareTag("animalCollide"))
                            {
                                Vector3 vector2 = other.transform.root.GetChild(0).InverseTransformPoint(playerTr.position);
                                float targetAngle = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
                                int animalHitDirection3 = animalHealth.GetAnimalHitDirection(targetAngle);
                                other.transform.SendMessageUpwards("ApplyAnimalSkinDamage", animalHitDirection3, SendMessageOptions.DontRequireReceiver);
                                other.transform.SendMessageUpwards("Hit", (int)num3, SendMessageOptions.DontRequireReceiver);
                                Mood.HitRumble();
                                if (playerHitEnemy != null)
                                {
                                    playerHitEnemy.getAttackDirection = animalHitDirection3;
                                }
                            }
                            else
                            {
                                other.transform.SendMessageUpwards("getAttackDirection", 3, SendMessageOptions.DontRequireReceiver);
                                other.transform.SendMessageUpwards("Hit", num3, SendMessageOptions.DontRequireReceiver);
                                Mood.HitRumble();
                            }
                        }
                        else if (other.gameObject.CompareTag("Fish") && !spear)
                        {
                            other.transform.SendMessage("Hit", num3, SendMessageOptions.DontRequireReceiver);
                            Mood.HitRumble();
                        }
                        if (playerHitEnemy != null)
                        {
                            playerHitEnemy.getAttackerType = 4;
                            playerHitEnemy.Hit = (int)num3;
                        }
                        if (axe)
                        {
                            other.transform.SendMessageUpwards("HitAxe", SendMessageOptions.DontRequireReceiver);
                            if (playerHitEnemy != null)
                            {
                                playerHitEnemy.HitAxe = true;
                            }
                        }
                        if (other.CompareTag("lb_bird") || other.CompareTag("animalCollide"))
                        {
                            FMODCommon.PlayOneshotNetworked(animalHitEvent, base.transform, FMODCommon.NetworkRole.Any);
                        }
                        else if (other.CompareTag("enemyCollide"))
                        {
                            FMODCommon.PlayOneshotNetworked(fleshHitEvent, base.transform, FMODCommon.NetworkRole.Any);
                        }
                        else if (other.CompareTag("EnemyBodyPart"))
                        {
                            FMODCommon.PlayOneshotNetworked(hackBodyEvent, base.transform, FMODCommon.NetworkRole.Any);
                            FauxMpHit((int)smashDamage);
                        }
                        setup.pmNoise.SendEvent("toWeaponNoise");
                        hitReactions.enableWeaponHitState();
                    }
                    if (!mainTrigger && (other.CompareTag("BreakableWood") || other.CompareTag("BreakableRock")))
                    {
                        other.transform.SendMessage("Hit", WeaponDamage, SendMessageOptions.DontRequireReceiver);
                        Mood.HitRumble();
                        other.SendMessage("LocalizedHit", new LocalizedHitData(base.transform.position, WeaponDamage), SendMessageOptions.DontRequireReceiver);
                        FauxMpHit((int)WeaponDamage);
                    }
                    if (other.CompareTag("lb_bird") && !mainTrigger)
                    {
                        base.transform.parent.SendMessage("GotBloody", SendMessageOptions.DontRequireReceiver);
                        other.transform.SendMessage("Hit", WeaponDamage, SendMessageOptions.DontRequireReceiver);
                        Mood.HitRumble();
                        FMODCommon.PlayOneshotNetworked(animalHitEvent, base.transform, FMODCommon.NetworkRole.Any);
                        setup.pmNoise.SendEvent("toWeaponNoise");
                        hitReactions.enableWeaponHitState();
                        if (playerHitEnemy != null)
                        {
                            playerHitEnemy.Hit = (int)WeaponDamage;
                        }
                    }
                    if (other.CompareTag("Tree") && !mainTrigger)
                    {
                        goto IL_18cc;
                    }
                    if (other.CompareTag("MidTree") && !mainTrigger)
                    {
                        goto IL_18cc;
                    }
                    goto IL_1ad1;
                    IL_110c:
                    if ((bool)component6)
                    {
                        component6.getCombo(3);
                    }
                    else
                    {
                        other.transform.SendMessageUpwards("getCombo", 3, SendMessageOptions.DontRequireReceiver);
                    }
                    if (playerHitEnemy != null)
                    {
                        playerHitEnemy.getCombo = 3;
                    }
                    goto IL_11b9;
                    IL_18cc:
                    if (chainSaw)
                    {
                        base.StartCoroutine(chainSawClampRotation(0.5f));
                    }
                    animEvents.cuttingTree = true;
                    animEvents.Invoke("resetCuttingTree", 0.5f);
                    if (stick || fireStick)
                    {
                        other.SendMessage("HitStick", SendMessageOptions.DontRequireReceiver);
                        setup.pmNoise.SendEvent("toWeaponNoise");
                        animator.SetFloatReflected("weaponHit", 1f);
                        PlayEvent(treeHitEvent, null);
                        if (BoltNetwork.isRunning && base.entity.isOwner)
                        {
                            FmodOneShot fmodOneShot2 = FmodOneShot.Create(GlobalTargets.Others, ReliabilityModes.Unreliable);
                            fmodOneShot2.Position = base.transform.position;
                            fmodOneShot2.EventPath = CoopAudioEventDb.FindId(treeHitEvent);
                            fmodOneShot2.Send();
                        }
                    }
                    else if (!Delay)
                    {
                        Delay = true;
                        base.Invoke("ResetDelay", 0.2f);
                        SapDice = Random.Range(0, 5);
                        setup.pmNoise.SendEvent("toWeaponNoise");
                        if (!noTreeCut)
                        {
                            if (SapDice == 1)
                            {
                                PlayerInv.GotSap(null);
                            }
                            if (other.GetType() == typeof(CapsuleCollider))
                            {
                                base.StartCoroutine(spawnWoodChips());
                            }
                            else
                            {
                                base.StartCoroutine(spawnWoodChips());
                            }
                            other.SendMessage("Hit", treeDamage, SendMessageOptions.DontRequireReceiver);
                            Mood.HitRumble();
                        }
                        PlayEvent(treeHitEvent, null);
                        if (BoltNetwork.isRunning && base.entity.isOwner)
                        {
                            FmodOneShot fmodOneShot3 = FmodOneShot.Create(GlobalTargets.Others, ReliabilityModes.Unreliable);
                            fmodOneShot3.Position = base.transform.position;
                            fmodOneShot3.EventPath = CoopAudioEventDb.FindId(treeHitEvent);
                            fmodOneShot3.Send();
                        }
                    }
                    goto IL_1ad1;
                    IL_10fc:
                    if (fsmHeavyAttackBool.Value)
                    {
                        goto IL_110c;
                    }
                    if (!hitReactions.kingHitBool)
                    {
                        if ((bool)component6)
                        {
                            component6.getCombo(animControl.combo);
                        }
                        else
                        {
                            other.transform.SendMessageUpwards("getCombo", animControl.combo, SendMessageOptions.DontRequireReceiver);
                        }
                        if (playerHitEnemy != null)
                        {
                            playerHitEnemy.getCombo = animControl.combo;
                        }
                    }
                    goto IL_11b9;
                    IL_1ad1:
                    if ((other.CompareTag("SmallTree") || other.CompareTag("Rope")) && !mainTrigger)
                    {
                        setup.pmNoise.SendEvent("toWeaponNoise");
                        int integer3 = animator.GetInteger("hitDirection");
                        other.transform.SendMessage("getAttackDirection", integer3, SendMessageOptions.DontRequireReceiver);
                        int num4 = DamageAmount;
                        if (chainSaw || machete)
                        {
                            num4 *= 5;
                        }
                        other.SendMessage("Hit", num4, SendMessageOptions.DontRequireReceiver);
                        Mood.HitRumble();
                        if (chainSaw || machete)
                        {
                            other.SendMessage("Hit", num4, SendMessageOptions.DontRequireReceiver);
                        }
                        FauxMpHit(num4);
                        if (chainSaw || machete)
                        {
                            FauxMpHit(num4);
                        }
                        if (!plantSoundBreak)
                        {
                            if (other.CompareTag("SmallTree"))
                            {
                                if (!string.IsNullOrEmpty(plantHitEvent))
                                {
                                    FMODCommon.PlayOneshotNetworked(plantHitEvent, base.transform, FMODCommon.NetworkRole.Any);
                                }
                            }
                            else if (other.CompareTag("Rope"))
                            {
                                PlayEvent(ropeHitEvent, null);
                            }
                            plantSoundBreak = true;
                            base.Invoke("disablePlantBreak", 0.3f);
                        }
                        if (other.CompareTag("SmallTree"))
                        {
                            PlayerInv.GotLeaf();
                        }
                    }
                    if (other.CompareTag("fire") && !mainTrigger && fireStick)
                    {
                        other.SendMessage("startFire");
                    }
                    end_IL_0108:;
                }
                finally
                {
                    if (playerHitEnemy != null && (bool)playerHitEnemy.Target && playerHitEnemy.Hit > 0)
                    {
                        playerHitEnemy.Send();
                    }
                }
            }
        }

    }
}


    


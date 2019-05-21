using TheForest.Items.World;
using TheForest.Utils;
using UnityEngine;

namespace PlayerUpgradePoints
{
    class FlashlightMod : BatteryBasedLight
    {
        public override void SetIntensity(float intensity)
        {
            if (UpgradePointsMod.instance.specialUpgrades[37].bought)
            {
                _mainLight.intensity = intensity*2;

            }
            else
            {
                _mainLight.intensity = intensity;

            }
            float num = intensity / 2f;
            if (intensity < 0.3f)
            {
                num = intensity / 3f;
            }
            if (num > 0.5f)
            {
                num = 0.5f;
            }
            if (UpgradePointsMod.instance.specialUpgrades[37].bought)
            {
                num *= 3;
            }
            if ((bool)_fillLight)
            {
                _fillLight.intensity = num;
            }
        }

        protected override void Update()
        {
            if (!BoltNetwork.isRunning || (BoltNetwork.isRunning && (bool)base.entity && base.entity.isAttached && base.entity.isOwner))
            {
                if (UpgradePointsMod.instance.specialUpgrades[37].bought)
                {
                    LocalPlayer.Stats.BatteryCharge -= _batterieCostPerSecond * Time.deltaTime/4;
                }
                else
                {
                    LocalPlayer.Stats.BatteryCharge -= _batterieCostPerSecond * Time.deltaTime;

                }
                if (LocalPlayer.Stats.BatteryCharge > 50f)
                {
                    SetIntensity(_highBatteryIntensity);
                }
                else if (LocalPlayer.Stats.BatteryCharge < 20f)
                {
                    if (LocalPlayer.Stats.BatteryCharge < 10f)
                    {
                        if (LocalPlayer.Stats.BatteryCharge < 5f)
                        {
                            if (LocalPlayer.Stats.BatteryCharge < 3f && Time.time > _animCoolDown && !_skipNoBatteryRoutine)
                            {
                                LocalPlayer.Animator.SetBool("noBattery", true);
                                _animCoolDown = Time.time + (float)Random.Range(30, 60);
                                base.Invoke("resetBatteryBool", 1.5f);
                            }
                            if (LocalPlayer.Stats.BatteryCharge <= 0f)
                            {
                                LocalPlayer.Stats.BatteryCharge = 0f;
                                if (_skipNoBatteryRoutine)
                                {
                                    SetEnabled(false);
                                }
                                else
                                {
                                    TorchLowerLightEvenMore();
                                    if (!_doingStash)
                                    {
                                        base.StartCoroutine("stashNoBatteryRoutine");
                                    }
                                    _doingStash = true;
                                }
                            }
                            else
                            {
                                SetEnabled(true);
                            }
                        }
                        else
                        {
                            TorchLowerLightMore();
                            SetEnabled(true);
                        }
                    }
                    else
                    {
                        TorchLowerLight();
                        SetEnabled(true);
                    }
                }
                if (BoltNetwork.isRunning)
                {
              if (UpgradePointsMod.instance.specialUpgrades[37].bought)
                    {
                        base.state.BatteryTorchIntensity = _mainLight.intensity*2;

                    }
                    else
                    {
                        base.state.BatteryTorchIntensity = _mainLight.intensity;

                    }
                    base.state.BatteryTorchEnabled = _mainLight.enabled;
                    base.state.BatteryTorchColor = _mainLight.color;
                }
            }
            TheForestQualitySettings.UserSettings.ApplyQualitySetting(_mainLight, LightShadows.Hard);
        }
    }
}

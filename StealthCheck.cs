using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TheForest.Utils;
using UnityEngine;
namespace PlayerUpgradePoints
{
    class StealthCheck : playerStealthMeter
    {

        public static bool IsHidden;
        protected override void Update()
        {
            if (!((Object)LocalPlayer.Animator == (Object)null) && !((Object)Scene.HudGui == (Object)null))
            {
                if (LocalPlayer.Animator.GetFloat("crouch") > 5f && !vis.currentlyTargetted && PlayerPreferences.ShowStealthMeter && !LocalPlayer.AnimControl.useRootMotion)
                {
                    Scene.HudGui.EyeIcon.SetActive(true);
                }
                else
                {
                    Scene.HudGui.EyeIcon.SetActive(false);
                }
                if (Scene.HudGui.EyeIcon.activeSelf)
                {
                    float value = (100f - vis.modVisRange) / 100f;
                    value = 1f - Mathf.Clamp(value, 0f, 1f);
                    if (value < 0.1)
                    {
                        value = 0;
                    }
                    if (value < 0.5f)
                    {
                        IsHidden = true;
                        if (UpgradePointsMod.instance.specialUpgrades[7].bought)
                        {
                            Scene.HudGui.EyeIconFill1.color = Color.red;
                            Scene.HudGui.EyeIconFill2.color = Color.red;
                        }
                    }
                    else
                    {
                        Scene.HudGui.EyeIconFill1.color = Color.white;
                        Scene.HudGui.EyeIconFill2.color = Color.white;
                        IsHidden = false;
                    }
                    if (vis.currentlyTargetted)
                    {
                        Scene.HudGui.EyeIconFill1.fillAmount = 1f;
                        Scene.HudGui.EyeIconFill2.fillAmount = 1f;
                    }
                    else
                    {
                        Scene.HudGui.EyeIconFill1.fillAmount = Mathf.SmoothDamp(Scene.HudGui.EyeIconFill1.fillAmount, value, ref fillVelocity, 0.45f);
                        Scene.HudGui.EyeIconFill2.fillAmount = Scene.HudGui.EyeIconFill1.fillAmount;
                    }
                }
                else
                {
                    fillVelocity = 0f;
                }
            }
        }
    }
}

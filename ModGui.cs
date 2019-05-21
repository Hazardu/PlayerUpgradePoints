using UnityEngine;

namespace PlayerUpgradePoints
{
    //GUI data
    public class ModGui
    {
        public static float value;
        public static float value_target;
        public static bool MenuOpened;
        public static bool ShowHelp;
        public static bool SpecialUnlocked;
        public static float OldStatsY;
        public static float NewStatsX;
        public static bool DisplayLvlUpMsg;
        public static float MenuYpos;
        public static bool slvlreminder;
        public static bool islvlreminder;
        public static float reminderx;
        public static int GainedExp;
        public static bool ShowGainedExp;
        public static int MasacreKills;
        public static string MasacreText;

        public static bool ShowSpecial;


        public static Texture2D[] NormalTextures;
        public static Texture2D border;

        public static Texture2D FrameB;
        public static Texture2D FrameE;
        public static Texture2D FrameD;
        public static Texture2D SpecialBG;
        public static Texture2D LevelUp;
        public static Texture2D StatDisplayBG;
        public static Texture2D[] LevelFramesArray;
        public static Texture2D MassacreBG;

        public static Texture2D SpecialHover;
        public static Texture2D SpecialON;
        public static Texture2D SpecialOFF;
        public static Texture2D ExpBarBG;
        public static Texture2D ExpBarFront;

        public static Texture2D BuffBackground;
        public static Texture2D BuffFront;
        public static Texture2D SecondChanceBuff;
        public static Texture2D BuilderBuff;
        public static Texture2D TowerBuff;
        public static Texture2D BrawlerBuff;
        public static Texture2D DashBuff;
        public static Texture2D FlyingDragonBuff;
        public static Texture2D RevengeBuff;
        public static Texture2D ActiveBuff;
        public static Texture2D CooldownBuff;
        public static Texture2D FocusFire;

        public static bool Quest_Show;
        public static bool Quest_Completed;
        public static float Quest_Scroll;
        //done
        public static Texture2D ClaimButton_Hover;
        public static Texture2D ClaimButton_Off;
        //not done
        public static Texture2D ActiveQuestList_On;
        public static Texture2D ActiveQuestList_Off;
        //not done
        public static Texture2D CompletedQuestList_On;
        public static Texture2D CompletedQuestList_Off;
        //not used
        public static Texture2D ScrollHandle;
        public static Texture2D ScrollBG;
        //not done
        public static Texture2D QuestMenu_BackGround;
        public static Texture2D QuestMenu_FrontTop;
        public static Texture2D QuestMenu_QuestFrame;
        //done
        public static Texture2D QuestButton_On;
        public static Texture2D QuestButton_Hover;
        public static Texture2D QuestButton_Off;
        //not used
        public static Texture2D QuestNameBorder;
        //not done
        public static Texture2D ProgressBar_BG;
        public static Texture2D ProgressBar_Fill;
        public static Texture2D ProgressBar_Front;


        public static Texture2D ObjectiveBG;

        public static void SetDefaults()
        {
            value = 0;
            value_target = 0;
            MenuOpened = false;
            ShowHelp = false;
            ShowSpecial = false;
            DisplayLvlUpMsg = false;
            slvlreminder = false;
            islvlreminder = false;
            OldStatsY = 5;

        }
        public static void GetImages()
        {
       BuffBackground = ModAPI.Resources.GetTexture("BuffBG.PNG");
            BuffFront = ModAPI.Resources.GetTexture("BuffBorder.PNG");
            SecondChanceBuff = ModAPI.Resources.GetTexture("SecondChanceCD.PNG");
            BuilderBuff = ModAPI.Resources.GetTexture("Builder.PNG");
            TowerBuff = ModAPI.Resources.GetTexture("Tower.PNG");
            BrawlerBuff = ModAPI.Resources.GetTexture("Brawler.PNG");
            DashBuff = ModAPI.Resources.GetTexture("DashCD.PNG");
            FlyingDragonBuff = ModAPI.Resources.GetTexture("FlyingDragon.PNG");
            RevengeBuff = ModAPI.Resources.GetTexture("Revenge.PNG");
            ActiveBuff = ModAPI.Resources.GetTexture("BuffActive.PNG");
            CooldownBuff = ModAPI.Resources.GetTexture("CooldownPng.PNG");
            FocusFire = ModAPI.Resources.GetTexture("FocusFire.PNG");

            ProgressBar_BG =ModAPI.Resources.GetTexture("QBarBG.PNG");
            ProgressBar_Fill= ModAPI.Resources.GetTexture("QBarFill.PNG");
            ProgressBar_Front= ModAPI.Resources.GetTexture("QBarFront.PNG");
            QuestNameBorder = ModAPI.Resources.GetTexture("PlaceHolder.PNG");
            QuestButton_On = ModAPI.Resources.GetTexture("QuestButtonOn.PNG");
            QuestButton_Hover = ModAPI.Resources.GetTexture("QuestButtonHover.PNG");
            QuestButton_Off = ModAPI.Resources.GetTexture("QuestButtonOff.PNG");
            QuestMenu_BackGround = ModAPI.Resources.GetTexture("BackGround.PNG");
            QuestMenu_FrontTop = ModAPI.Resources.GetTexture("QuestFront.PNG");
            QuestMenu_QuestFrame = ModAPI.Resources.GetTexture("QuestFrame.PNG");
            ScrollBG = ModAPI.Resources.GetTexture("PlaceHolder.PNG");
            ScrollHandle = ModAPI.Resources.GetTexture("PlaceHolder.PNG");
            CompletedQuestList_Off = ModAPI.Resources.GetTexture("CompletedQuestsOff.PNG");
            CompletedQuestList_On = ModAPI.Resources.GetTexture("CompletedQuestsOn.PNG");
            ActiveQuestList_Off = ModAPI.Resources.GetTexture("ActiveQuestsOff.PNG");
            ActiveQuestList_On = ModAPI.Resources.GetTexture("ActiveQuestsOn.PNG");
            ClaimButton_Hover = ModAPI.Resources.GetTexture("ClaimHover.PNG");
            ClaimButton_Off = ModAPI.Resources.GetTexture("ClaimOff.PNG");


            FrameB = ModAPI.Resources.GetTexture("FrameB.PNG");
            FrameE = ModAPI.Resources.GetTexture("FrameE.PNG");
            FrameD = ModAPI.Resources.GetTexture("FrameD.PNG");


            border = ModAPI.Resources.GetTexture("Border.PNG");
            NormalTextures = new Texture2D[16];
            int i = 0;
            NormalTextures[i] = ModAPI.Resources.GetTexture("SwingStamina.PNG");    //0
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("RunSpeed.PNG");    //1
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("SprintStaminaUse.PNG");    //2
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("JumpHeight.PNG");    //3
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("Fall damage.PNG");    //4
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("HealthRegen.PNG");    //5
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("Spray.PNG");    //6
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("Fuel.PNG");    //7
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("AtkSpeed.PNG");    //8
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("Damage.PNG");    //9
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("StaminaRegen.PNG");    //10
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("ArrowDamage.PNG");    //11
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("Diving.PNG");    //12
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("DmgReduction.PNG");    //13
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("Block.PNG");    //14
            i++;
            NormalTextures[i] = ModAPI.Resources.GetTexture("AreaDamage.PNG");    //15

            SpecialHover = ModAPI.Resources.GetTexture("SpecialHover.PNG");
            SpecialOFF = ModAPI.Resources.GetTexture("SpecialOff.PNG");
            SpecialON = ModAPI.Resources.GetTexture("SpecialON.PNG");

            SpecialBG = ModAPI.Resources.GetTexture("BackGround.PNG");
            MassacreBG = ModAPI.Resources.GetTexture("MassacreBG.PNG");
            StatDisplayBG = ModAPI.Resources.GetTexture("BackGround1.PNG");
            LevelUp = ModAPI.Resources.GetTexture("LevelUp.PNG");
            ExpBarBG =ModAPI.Resources.GetTexture("BarBG.PNG");
            ExpBarFront = ModAPI.Resources.GetTexture("BarFront.PNG");




            LevelFramesArray = new Texture2D[13];

            for (int A = 1; A <= 13; A++)
            {
                LevelFramesArray[A-1] = ModAPI.Resources.GetTexture("BG" + A + ".PNG");
            }

        }
    }

}

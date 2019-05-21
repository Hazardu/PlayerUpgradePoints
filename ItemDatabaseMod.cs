using UnityEngine;
using TheForest.Items;
using System.Linq;
using TheForest.Utils;
using TheForest.Items.Inventory;

namespace PlayerUpgradePoints
{
    public class ItemDatabaseMod : ItemDatabase
    {
        public static ItemDatabase instanceM = null;
        public static ItemDatabaseMod instanceD = null;
        public bool Modded = false;
        public override void OnEnable()
        {
            if (!Modded)
            {
                
                    Modded = true;
                    for (int i = 0; i < _items.Length; i++)
                    {



                        //ModAPI.Console.Write("Increasing item with name " + _items[i]._name);
                        switch (_items[i]._id)
                        {
                            case 57:

                                _items[i]._maxAmount += 140;//
                                break;
                            case 53:
                                _items[i]._maxAmount += 70;
                                break;
                            case 175:
                                _items[i]._maxAmount += 20;
                                break;
                            case 178:
                                _items[i]._maxAmount += 200;
                                break;
                            case 204:
                                _items[i]._maxAmount += 30;
                                break;
                            case 177:
                                _items[i]._maxAmount += 15;
                                break;
                            case 41:
                                _items[i]._maxAmount += 20;
                                break;
                            case 43:
                                _items[i]._maxAmount += 20;
                                break;
                            case 109:
                                _items[i]._maxAmount += 50;
                                break;
                            case 126:
                                _items[i]._maxAmount += 50;
                                break;
                            case 292:
                                _items[i]._maxAmount += 50;
                                break;
                            case 92:
                                _items[i]._maxAmount += 50;
                                break;
                            case 262:
                                _items[i]._maxAmount += 5;
                                break;
                            case 123:
                                _items[i]._maxAmount += 10;
                                break;
                            case 76:
                                _items[i]._maxAmount += 10;
                                break;
                            case 35:
                                _items[i]._maxAmount += 10;
                                break;
                            case 29:
                                _items[i]._maxAmount += 15;
                                break;
                            case 49:
                                _items[i]._maxAmount += 12;
                                break;
                            case 37:
                                _items[i]._maxAmount += 20;
                                break;
                            case 31:
                                _items[i]._maxAmount += 25;
                                break;
                            case 54:
                                _items[i]._maxAmount += 25;
                                break;
                            case 67:
                                _items[i]._maxAmount += 10;
                                break;
                            case 68:
                                _items[i]._maxAmount += 10;
                                break;
                            case 212:
                                _items[i]._maxAmount += 10;
                                break;
                            case 71:
                                _items[i]._maxAmount += 25;
                                break;
                            case 82:
                                _items[i]._maxAmount += 30;
                                break;
                            case 89:
                                _items[i]._maxAmount += 15;
                                break;
                            case 94:
                                _items[i]._maxAmount += 50;
                                break;
                            case 97:
                                _items[i]._maxAmount += 30;
                                break;
                            case 98:
                                _items[i]._maxAmount += 30;
                                break;
                            case 99:
                                _items[i]._maxAmount += 50;
                                break;
                            case 100:
                                _items[i]._maxAmount += 20;
                                break;
                            case 213:
                                _items[i]._maxAmount += 30;
                                break;
                            case 112:
                                _items[i]._maxAmount += 100;
                                break;
                            case 113:
                                _items[i]._maxAmount += 100;
                                break;
                            case 114:
                                _items[i]._maxAmount += 100;
                                break;
                            case 221:
                                _items[i]._maxAmount += 100;
                                break;
                            case 127:
                                _items[i]._maxAmount += 5;
                                break;
                            case 142:
                                _items[i]._maxAmount += 2;
                                break;
                            case 141:
                                _items[i]._maxAmount += 2;
                                break;
                        case 207:
                            _items[i]._maxAmount += 15;
                            break;

                    }
                    

                }
            }
            if (instanceD == null)
            {

                CreateMod();
            }
            base.OnEnable();


        }
        void CreateMod()
        {
            GameObject obj = new GameObject("_PUPDatabaseModifier_");
            DatabaseModifier modifier = obj.AddComponent<DatabaseModifier>();
            DatabaseModifier.instance = modifier;
            instanceM = _instance;
            instanceD = this;
        }

    }


    public class DatabaseModifier : MonoBehaviour
    {


        public static DatabaseModifier instance = null;


        public void Modify()
        {
            if (UpgradePointsMod.instance != null)
            {
                DoChanges();
                LocalPlayer.Inventory.FixMaxAmountBonuses();

            }
        }

        public void RemoveBonuses()
        {
            if (UpgradePointsMod.instance.specialUpgrades[13].bought)
            {

                IncreaseMaxAmount(57, -40);
                IncreaseMaxAmount(53, -20);
            }

            if (UpgradePointsMod.instance.specialUpgrades[14].bought)
            {

                IncreaseMaxAmount(175, -20);
            }
            if (UpgradePointsMod.instance.specialUpgrades[17].bought)
            {
                IncreaseMaxAmount(178, -200);
                IncreaseMaxAmount(204, -30);
                IncreaseMaxAmount(177, -5);
            }
            if (UpgradePointsMod.instance.specialUpgrades[18].bought)
            {

                IncreaseMaxAmount(57, -100);
                IncreaseMaxAmount(53, -50);
            }

            if (UpgradePointsMod.instance.specialUpgrades[19].bought)
            {

                IncreaseMaxAmount(41, -20);
                IncreaseMaxAmount(43, -20);
                IncreaseMaxAmount(109, -50);
                IncreaseMaxAmount(126, -50);
                IncreaseMaxAmount(292, -50);
                IncreaseMaxAmount(92, -50);
                IncreaseMaxAmount(262, -5);
                IncreaseMaxAmount(207, -15);
                IncreaseMaxAmount(123, -10);
                IncreaseMaxAmount(76, -10);
                IncreaseMaxAmount(35, -10);
                IncreaseMaxAmount(29, -15);
                IncreaseMaxAmount(49, -12);
                IncreaseMaxAmount(37, -20);
                IncreaseMaxAmount(31, -25);
                IncreaseMaxAmount(54, -10);
                IncreaseMaxAmount(67, -10);
                IncreaseMaxAmount(68, -10);
                IncreaseMaxAmount(212, -10);
                IncreaseMaxAmount(71, -25);
                IncreaseMaxAmount(82, -30);
                IncreaseMaxAmount(89, -15);
                IncreaseMaxAmount(94, -50);
                IncreaseMaxAmount(97, -30);
                IncreaseMaxAmount(98, -30);
                IncreaseMaxAmount(99, -50);
                IncreaseMaxAmount(100, -20);
                IncreaseMaxAmount(213, -30);
                IncreaseMaxAmount(112, -100);
                IncreaseMaxAmount(113, -100);
                IncreaseMaxAmount(114, -100);
                IncreaseMaxAmount(127, -5);
                IncreaseMaxAmount(221, -100);
                IncreaseMaxAmount(142, -2);
                IncreaseMaxAmount(141, -2);

            }
            if (UpgradePointsMod.instance.specialUpgrades[32].bought)
            {
                IncreaseMaxAmount(54, -15);
                IncreaseMaxAmount(177, -10);


            }

        }


        void DoChanges()
        {

            if (UpgradePointsMod.instance.specialUpgrades[13].bought)
            {

                IncreaseMaxAmount(57, 40);
                IncreaseMaxAmount(53, 20);
            }

            if (UpgradePointsMod.instance.specialUpgrades[14].bought)
            {

                IncreaseMaxAmount(175, 20);
            }
            if (UpgradePointsMod.instance.specialUpgrades[17].bought)
            {
                IncreaseMaxAmount(178, 200);
                IncreaseMaxAmount(204, 30);
                IncreaseMaxAmount(177, 5);
            }
            if (UpgradePointsMod.instance.specialUpgrades[18].bought)
            {

                IncreaseMaxAmount(57, 100);
                IncreaseMaxAmount(53, 50);
            }

            if (UpgradePointsMod.instance.specialUpgrades[19].bought)
            {

                IncreaseMaxAmount(41, 20);
                IncreaseMaxAmount(43, 20);
                IncreaseMaxAmount(109, 50);
                IncreaseMaxAmount(126, 50);
                IncreaseMaxAmount(292, 50);
                IncreaseMaxAmount(92, 50);
                IncreaseMaxAmount(262, 5);
                IncreaseMaxAmount(207, 15);
                IncreaseMaxAmount(123, 10);
                IncreaseMaxAmount(76, 10);
                IncreaseMaxAmount(35, 10);
                IncreaseMaxAmount(29, 15);
                IncreaseMaxAmount(49, 12);
                IncreaseMaxAmount(37, 20);
                IncreaseMaxAmount(31, 25);
                IncreaseMaxAmount(54, 10);
                IncreaseMaxAmount(67, 10);
                IncreaseMaxAmount(68, 10);
                IncreaseMaxAmount(212, 10);
                IncreaseMaxAmount(71, 25);
                IncreaseMaxAmount(82, 30);
                IncreaseMaxAmount(89, 15);
                IncreaseMaxAmount(94, 50);
                IncreaseMaxAmount(97, 30);
                IncreaseMaxAmount(98, 30);
                IncreaseMaxAmount(99, 50);
                IncreaseMaxAmount(100, 20);
                IncreaseMaxAmount(213, 30);
                IncreaseMaxAmount(112, 100);
                IncreaseMaxAmount(113, 100);
                IncreaseMaxAmount(114, 100);
                IncreaseMaxAmount(127, 5);
                IncreaseMaxAmount(221, 100);
                IncreaseMaxAmount(142, 2);
                IncreaseMaxAmount(141, 2);

            }
            if (UpgradePointsMod.instance.specialUpgrades[32].bought)
            {
                IncreaseMaxAmount(54, 15);
                IncreaseMaxAmount(177, 10);


            }
        }
        public static bool ChangeMaxAmount = false;
        void IncreaseMaxAmount(int id, int amount)
        {
            ChangeMaxAmount = true;
            LocalPlayer.Inventory.AddMaxAmountBonus(id, amount);
            ChangeMaxAmount = false;
        }
        public void Setup()
        {

            IncreaseMaxAmount(57, -40);
            IncreaseMaxAmount(53, -20);


            IncreaseMaxAmount(175, -20);

            IncreaseMaxAmount(178, -200);
            IncreaseMaxAmount(204, -30);
            IncreaseMaxAmount(177, -5);

            IncreaseMaxAmount(57, -100);
            IncreaseMaxAmount(53, -50);


            IncreaseMaxAmount(41, -20);
            IncreaseMaxAmount(43, -20);
            IncreaseMaxAmount(109, -50);
            IncreaseMaxAmount(126, -50);
            IncreaseMaxAmount(292, -50);
            IncreaseMaxAmount(92, -50);
            IncreaseMaxAmount(262, -5);
            IncreaseMaxAmount(207, -15);
            IncreaseMaxAmount(123, -10);
            IncreaseMaxAmount(76, -10);
            IncreaseMaxAmount(35, -10);
            IncreaseMaxAmount(29, -15);
            IncreaseMaxAmount(49, -12);
            IncreaseMaxAmount(37, -20);
            IncreaseMaxAmount(31, -25);
            IncreaseMaxAmount(54, -10);
            IncreaseMaxAmount(67, -10);
            IncreaseMaxAmount(68, -10);
            IncreaseMaxAmount(212, -10);
            IncreaseMaxAmount(71, -25);
            IncreaseMaxAmount(82, -30);
            IncreaseMaxAmount(89, -15);
            IncreaseMaxAmount(94, -50);
            IncreaseMaxAmount(97, -30);
            IncreaseMaxAmount(98, -30);
            IncreaseMaxAmount(99, -50);
            IncreaseMaxAmount(100, -20);
            IncreaseMaxAmount(213, -30);
            IncreaseMaxAmount(112, -100);
            IncreaseMaxAmount(113, -100);
            IncreaseMaxAmount(114, -100);
            IncreaseMaxAmount(127, -5);
            IncreaseMaxAmount(221, -100);
            IncreaseMaxAmount(142, -2);
            IncreaseMaxAmount(141, -2);

            IncreaseMaxAmount(54, -15);
            IncreaseMaxAmount(177, -10);

        }
    }

}


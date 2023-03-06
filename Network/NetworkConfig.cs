using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Bolt;
using TheForest.UI.Multiplayer;
using TheForest.Utils;
using UnityEngine;

namespace PlayerUpgradePoints.Network
{
    public class NetworkConfig
    {
        public const ulong ModNetworkPacked = 999999421;
        public readonly static NetworkId ModNetworkID = new NetworkId(ModNetworkPacked);
    }
    public class NetworkInformation : MonoBehaviour
    {
        [ModAPI.Attributes.ExecuteOnGameStart]
        static void Init()
        {
            new GameObject("PUP Network Data").AddComponent<NetworkInformation>();
        }

        public static int playerCount = 1;

        void Start()
        {
            playerCount = 1;
        }
    }

    public static class NetworkManager
    {
        public static byte[] StringToBytes(string cmd)
        {
            var a = cmd.ToCharArray();
            var b = new byte[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                b[i] = (byte)a[i];
            }
            return b;
        }
        public static string BytesToString(byte[] b)
        {
            string s = string.Empty;
            for (int i = 0; i < b.Length; i++)
            {
                s += (char)b[i];
            }
            return s;
        }


        /// <summary>
        /// Sends bytes to targets, calls RecieveCommand on them
        /// </summary>
        /// <param name="bytearray">Bytes to send</param>
        /// <param name="target">Choose between possible recievers</param>
        public static void SendCommand(byte[] bytearray, GlobalTargets target)
        {
            if (BoltNetwork.isRunning)
            {
                    ChatEvent chatEvent = ChatEvent.Create(target);
                    chatEvent.Message = BytesToString(bytearray);
                    chatEvent.Sender = NetworkConfig.ModNetworkID;
                    chatEvent.Send();
            }
        }



        /// <summary>
        /// Sends exp to clients with given parameters
        /// </summary>
        /// <param name="expAmount"></param>
        /// <param name="position"></param>
        /// <param name="countToMassacre"></param>
        public static void SendExpCommand(int expAmount, Vector3 position, bool countToMassacre)
        {
            if (GameSetup.IsMpServer)
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    using (System.IO.BinaryWriter w = new System.IO.BinaryWriter(stream))
                    {
                        w.Write(1);
                        w.Write(expAmount);
                        w.Write(position.x);
                        w.Write(position.y);
                        w.Write(position.z);
                        w.Write(countToMassacre);
                        w.Close();
                    }
                    SendCommand(stream.ToArray(), GlobalTargets.Others);
                    stream.Close();
                }
            }

        }




        public static void RecieveCommand(byte[] b)
        {
            using (MemoryStream stream = new MemoryStream(b))
            {
                using (BinaryReader r = new BinaryReader(stream))
                {
                    int cmdIndex = r.ReadInt32();
					try
					{
                        switch (cmdIndex)
                        {
                            case 1:
                                Cmd_RecieveExp(r);
                                break;
                        }
					}
					catch (Exception e)
					{
                        ModAPI.Log.Write("Network error: " + cmdIndex + "\n\n" + e.ToString());
					}
                    r.Close();
                }
                stream.Close();
            }
        }



        #region Commands 
        //Command 1 - recieve experience
        static void Cmd_RecieveExp(BinaryReader r)
        {

            int amount = r.ReadInt32();
            Vector3 pos = new Vector3(r.ReadSingle(), r.ReadSingle(), r.ReadSingle());
            bool countToMassacre = r.ReadBoolean();
            if ((LocalPlayer.Transform.position - pos).sqrMagnitude < 250 * 250)
            {
                UpgradePointsMod.instance.AddXP(amount, countToMassacre);
            }
        }

        #endregion

    }
}

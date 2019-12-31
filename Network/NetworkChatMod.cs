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
    public class NetworkChatMod : ChatBox
    {
        public const ulong ModNetworkPacked = 999999422;
        public readonly static NetworkId ModNetworkID = new NetworkId(ModNetworkPacked);

        [ModAPI.Attributes.Priority(201)]
        public override void AddLine(NetworkId? playerId, string message, bool system)
        {

            if (playerId == ModNetworkID)
            {
                NetworkManager.RecieveCommand(NetworkManager.StringToBytes(message));
            }
            else
            {
                base.AddLine(playerId, message, system);
            }
        }
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
        public enum Target
        {
            OnlyServer, Everyone, Clients, Others
        }

        public static byte[] StringToBytes(string s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
       
            return bytes;
        }
        public static string BytesToString(byte[] bytes)
        {
          string s= Encoding.ASCII.GetString(bytes);

            return s;
        }


        /// <summary>
        /// Sends bytes to targets, calls RecieveCommand on them
        /// </summary>
        /// <param name="bytearray">Bytes to send</param>
        /// <param name="target">Choose between possible recievers</param>
        public static void SendLine(byte[] bytearray, Target target = Target.Clients)
        {
            if (GameSetup.IsSinglePlayer || !BoltNetwork.isRunning)
            {
                return;
            }
            else
            {
                if (BoltNetwork.isRunning)
                {
                    ChatEvent chatEvent = null;
                    switch (target)
                    {
                        case Target.OnlyServer:
                            chatEvent = ChatEvent.Create(GlobalTargets.OnlyServer);
                            break;
                        case Target.Everyone:
                            chatEvent = ChatEvent.Create(GlobalTargets.Everyone);
                            break;
                        case Target.Clients:
                            chatEvent = ChatEvent.Create(GlobalTargets.AllClients);
                            break;
                        case Target.Others:
                            chatEvent = ChatEvent.Create(GlobalTargets.Others);
                            break;
                        default:
                            break;
                    }
                    chatEvent.Message = BytesToString(bytearray);
                    chatEvent.Sender = NetworkChatMod.ModNetworkID;
                    chatEvent.Send();
                }
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
            if (BoltNetwork.isRunning)
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
                    SendLine(stream.ToArray());
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
                    switch (cmdIndex)
                    {
                        case 1:
                            Cmd_RecieveExp(r);
                            break;
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

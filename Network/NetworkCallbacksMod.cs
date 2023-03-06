using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bolt;

using TheForest.Utils;

using UnityEngine;
namespace PlayerUpgradePoints.Network
{
	public class NetworkCallbacksServerMod : CoopServerCallbacks
	{
		public override void OnEvent(ChatEvent evnt)
		{
			if (evnt.Sender == NetworkConfig.ModNetworkID)
			{
				//Chat events work like this
				//Client sends a event to the host that a msg is sent;
				//The host broadcasts the message to everyone
				NetworkManager.RecieveCommand(NetworkManager.StringToBytes(evnt.Message));
				return;
			}

			if (!this.ValidateSender(evnt, global::SenderTypes.Any))
			{
				return;
			}

			for (int i = 0; i < TheForest.Utils.Scene.SceneTracker.allPlayerEntities.Count; i++)
			{
				if (TheForest.Utils.Scene.SceneTracker.allPlayerEntities[i].source == evnt.RaisedBy)
				{
					if (TheForest.Utils.Scene.SceneTracker.allPlayerEntities[i].networkId == evnt.Sender)
					{
						ChatEvent chatEvent = ChatEvent.Create(GlobalTargets.AllClients);
						chatEvent.Sender = evnt.Sender;
						chatEvent.Message = evnt.Message;
						chatEvent.Send();
					}
					return;
				}
			}
			if (BoltNetwork.isServer && evnt.RaisedBy == null)
			{
				ChatEvent chatEvent2 = ChatEvent.Create(GlobalTargets.AllClients);
				chatEvent2.Sender = evnt.Sender;
				chatEvent2.Message = evnt.Message;
				chatEvent2.Send();
			}
		}


		public override void EntityDetached(BoltEntity entity)
		{
			if (entity.StateIs<IPlayerState>() && TheForest.Utils.Scene.SceneTracker && GameSetup.IsMpServer)
			{
				NetworkInformation.playerCount = TheForest.Utils.Scene.SceneTracker.allPlayers.Count - 1;
			}
			base.EntityDetached(entity);
		}
		public override void EntityReceived(BoltEntity entity)
		{
			if (entity.StateIs<IPlayerState>() && GameSetup.IsMpServer)
			{
				NetworkInformation.playerCount = TheForest.Utils.Scene.SceneTracker.allPlayers.Count + 1;
			}
			base.EntityReceived(entity);
		}
	}
	public class NetworkCallbacksClientMod : CoopPlayerCallbacks
	{
		public override void OnEvent(ChatEvent evnt)
		{
			if (evnt.Sender == NetworkConfig.ModNetworkID)
			{
				NetworkManager.RecieveCommand(NetworkManager.StringToBytes(evnt.Message));
				return;
			}
			base.OnEvent(evnt);
		}
	}
}

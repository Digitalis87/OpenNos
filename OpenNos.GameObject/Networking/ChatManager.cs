﻿using OpenNos.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNos.GameObject
{
    public class ChatManager
    {
        private static ChatManager _instance;
        public List<ClientSession> sessions { get; set; }

        private ChatManager()
        {
            sessions = new List<ClientSession>();
        }

        public static ChatManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChatManager();

                return _instance;
            }
        }

        public List<String> GetInPacketArray(ClientSession client)
        {
            List<String> list = new List<String>();
            foreach (ClientSession session in sessions)
                if (session != client && client.Character.Map == session.Character.Map)
                {
                    list.Add(session.Character.GenerateIn());
                    list.Add(session.Character.GenerateCMode());
                }
            return list;
        }

        public bool Broadcast(ClientSession client, String message, ReceiverType receiver, String CharacterName ="", int CharacterId = -1)
        {
            switch (receiver)
            {
                case ReceiverType.All:
                    foreach (ClientSession session in sessions)
                    {
                        session.Client.SendPacket(message);
                    }

                    break;

                case ReceiverType.AllExceptMe:
                    foreach (ClientSession session in sessions)
                    {
                        if(session != client)
                        session.Client.SendPacket(message);
                    }
                    break;

                case ReceiverType.AllOnMap:
                    foreach (ClientSession session in sessions)
                    {
                        if (session.Character !=null && client.Character.Map == session.Character.Map)
                            session.Client.SendPacket(message);
                    }
                  
                    break;
                case ReceiverType.AllOnMapExceptMe:
                    foreach (ClientSession session in sessions)
                    {
                        if (session.Character != null && session != client && client.Character.Map == session.Character.Map)
                            session.Client.SendPacket(message);
                    }
                    break;
                case ReceiverType.OnlyMe:
                    client.Client.SendPacket(message);
                    break;
                case ReceiverType.OnlySomeone:
                    foreach (ClientSession session in sessions)
                    {
                        if (session.Character != null &&  (session.Character.Name == CharacterName || session.Character.CharacterId == CharacterId))
                        {
                            session.Client.SendPacket(message);
                            return true;
                        }
                           
                    }
                    return false;
                      
            }
            return true;
        }

        public void RequiereBroadcastFromMap(short MapId, string Message)
        {
                foreach (ClientSession session in sessions)
                {
                if (session.Character != null && session.Character.Map == MapId)
                    Broadcast(session, String.Format(Message, session.Character.CharacterId),ReceiverType.AllOnMap);
                }
            
        }

        public bool Kick(String CharacterName)
        {
            foreach (ClientSession session in sessions)
            {
                if (session.Character != null && session.Character.Name == CharacterName)
                {
                    session.Client.Disconnect();
                    return true;
                }
            }
            return false;
        }
    }
}

using Riftshot_Server;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Riftshot_Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();
            string _scene = _packet.ReadString();
            int _outfit = _packet.ReadInt();
            int _paint = _packet.ReadInt();
            //Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                //Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username, _scene, _outfit, _paint);
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            //bool[] _inputs = new bool[_packet.ReadInt()];
            //for (int i = 0; i < _inputs.Length; i++)
            //{
            //    _inputs[i] = _packet.ReadBool();
            //}
            Vector3 _position = _packet.ReadVector3();
            Quaternion _rotation = _packet.ReadQuaternion();

            Server.clients[_fromClient].player.SetInput(_position, _rotation);
        }

        public static void PlayerAnimation(int _fromClient, Packet _packet)
        {
            string AName = _packet.ReadString();
            Vector3 ComboValue = _packet.ReadVector3();

            Server.clients[_fromClient].player.SetAnims(AName, ComboValue);
        }

        public static void CustomIntPingPong(int _fromClient ,Packet _packet)
        {
            Vector3 WEREGONNA = _packet.ReadVector3();
            if(WEREGONNA.Z == 0)
            {
                Server.clients[_fromClient].player.SetOutfit(WEREGONNA);
            }
        }
        public static void ClientSceneUpdate(int _fromClient, Packet _packet)
        {
            string NewScene = _packet.ReadString();
            Server.clients[_fromClient].player.SetScene(NewScene);
            //Server.clients[_fromClient].SendIntoGame(Server.clients[_fromClient].player.username, NewScene);
        }
    }
}
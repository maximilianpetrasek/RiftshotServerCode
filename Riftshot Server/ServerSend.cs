﻿using Riftshot_Server;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Riftshot_Server
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
            
        }
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);
                _packet.Write(_player.scene);
                _packet.Write(_player.Outfit);
                _packet.Write(_player.Paintjob);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void PlayerPosition(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.position);

                SendUDPDataToAll(_packet);
            }
        }

        public static void PlayerRotation(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.rotation);

                SendUDPDataToAll(_player.id, _packet);
            }
        }


        public static void PlayerAnimation(Player _player, string AnimName, Vector3 ComboValues)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerAnimation))
            {
                _packet.Write(_player.id);
                _packet.Write(AnimName);
                _packet.Write(ComboValues);

                SendUDPDataToAll( _packet);
            }
        }

        public static void PingPong(Player _player, Vector3 DataToPong)
        {
                using (Packet _packet = new Packet((int)ServerPackets.customInt))
                {
                    _packet.Write(_player.id);
                    _packet.Write(DataToPong);
                    SendUDPDataToAll(_packet);
                }
        }

        public static void Disconned(int _PlayerID)
        {
            using (Packet _packet = new Packet((int)ServerPackets.dcPacket))
            {
                _packet.Write(_PlayerID);
                SendUDPDataToAll(_packet);
            }
        }

        public static void SendScene(string _sceneID)
        {

        }

            #endregion
        }
}
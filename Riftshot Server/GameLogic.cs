using System;
using System.Collections.Generic;
using System.Text;

namespace Riftshot_Server
{
    class GameLogic
    {
        
        public static void Update()
        {
            int lineIndex = 1;
            //Console.Clear();
            //Console.WriteLine("Connected Players: ");
            foreach (Client _client in Server.clients.Values)
            {
;
                //Console.WriteLine(lineIndex);
                if (_client.player != null)
                {
                    _client.player.Update();
                    lineIndex++;
                    Console.CursorLeft = 0;
                    Console.CursorTop = lineIndex;
                    Console.WriteLine("Player " + _client.player.id.ToString() + ", Nickname: " + _client.player.username + ", Currently in " + _client.player.scene + " With Outfit #" + _client.player.Outfit + "and Paintjob #" + _client.player.Paintjob);
                }
                    
            }

            ThreadManager.UpdateMain();
        }
    }
}
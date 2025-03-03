using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Riftshot_Server
{
    class Player
    {
        public int id;
        public string username, scene;
        public Vector3 position;
        public Quaternion rotation;
        public float Outfit, Paintjob;
        private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
        //private bool[] inputs;

        public Player(int _id, string _username, Vector3 _spawnPosition, string _scene, int _outfit, int _paint)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
            scene = _scene;
            Outfit = _outfit;
            Paintjob = _paint;
            //inputs = new bool[4];
        }

        public void Update()
        {
            

            Move(position);
        }

        private void Move(Vector3 _position)
        {
            

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }

        public void SetInput(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }

        public void SetAnims(string AnimName, Vector3 ComboValues)
        {
            ServerSend.PlayerAnimation(this, AnimName, ComboValues);
        }

        public void SetOutfit(Vector3 DataPing)
        {
            ServerSend.PingPong(this, DataPing);
            Outfit = DataPing.X;
            Paintjob = DataPing.Y;

        }
        public void SetScene(string NewScene)
        {
            scene = NewScene;
            //sServerSend.SendScene(NewScene);
        }
    }
}

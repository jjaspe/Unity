using Assets.Scripts.Foosball.InputHandlers;
using Assets.Scripts.Foosball.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Foosball
{
    public class GameManager:MonoBehaviour
    {
        static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (new GameObject()).AddComponent<GameManager>();
                }
                return instance;
            }
        }

        public KeyboardInputManager rodsManager
        {
            get { return KeyboardInputManager.Instance; }
        }
        public WallCollisionHandler wallHandler
        {
            get { return WallCollisionHandler.Instance; }
        }
        public RodMover rodMover
        {
            get
            {
                return RodMover.Instance;
            }
        }

        public int rotateSpeed = 1;
        public float moveSpeed = 1;
        public int dropSpeed = 100;

        private GameManager()
        {
            
        }
        
        public void MouseMoved(MouseMovement movement)
        {
            List<GameObject> rods = rodsManager.GetSelectedRods();
            rodMover.MoveRods(rods,movement);
        }        

        public void KeysPressed(List<string> keys,bool noMove,bool noRotate)
        {
            rodsManager.keysPressed = keys;
            rodMover.noMove = noMove;
            rodMover.noRotate = noRotate;
        }

        public void RodHitWall(Collider collider,GameObject bumper)
        {
            wallHandler.RodHitWall(collider, bumper);
        }                

        public void MoveRodToEdge(Transform rodParent,int direction=0)
        {
            rodMover.MoveRodToEdge(rodParent, direction);
        }                
    }
}

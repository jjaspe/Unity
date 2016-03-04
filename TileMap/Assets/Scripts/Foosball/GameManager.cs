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

        public int rotateSpeed = 1;
        public float moveSpeed = 1;
        public int dropSpeed = 100;

        int backRod = 0;
        bool[,] bumperAtWallWhite, bumperAtWallBlack;
        bool noMove = false, noRotate = false;
        RodMover mover;

        private GameManager()
        {
            bumperAtWallBlack = new bool[4, 2];
            bumperAtWallWhite = new bool[4, 2];
        }

        public void Start()
        {
            mover = (new GameObject()).AddComponent<RodMover>();
        }
        public void Update()
        {
            switch (Input.inputString.ToUpper())
            {
                case "S":
                    backRod = (backRod + 1) % 4;
                    break;
                case "A":
                    backRod = ((backRod - 1) + 4) % 4;
                    break;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
                noMove = true;
            if (Input.GetKeyUp(KeyCode.LeftShift))
                noMove = false;
            if (Input.GetKeyDown(KeyCode.LeftControl))
                noRotate = true;
            if (Input.GetKeyUp(KeyCode.LeftControl))
                noRotate = false;
        }

        public void MouseMoved(MouseMovement movement)
        {
            GameObject rod = GetCorrectRod();
            if(!noMove)
                tryMoveRod(rod, moveSpeed*movement.Speeds[1] / 100);
            if(!noRotate)
                mover.RotateRod(rod, rotateSpeed*movement.Speeds[0]/500);
        }        

        public void RodHitWall(Collider collider,GameObject bumper)
        {
            Transform rod = bumper.transform.parent;
            Transform wall = collider.transform;
            int rodIndex = getRodIndex(rod), wallIndex = getWallIndex(wall);
            if (rod.name.Contains("Black"))
                bumperAtWallBlack[rodIndex, wallIndex] = true;
            else if (rod.name.Contains("White"))
                bumperAtWallWhite[rodIndex, wallIndex] = true;

            MoveRodToEdge(bumper.transform.parent,wallIndex);
        }

        private int getRodIndex(Transform rod)
        {
            string name = rod.name;
            name = name.Replace("White ","").Replace("Black ","");
            return Int32.Parse(name)-1;
        }

        private void tryMoveRod(GameObject rod, float movement)
        {
            if(movement>=0)//move up
            {
                if(rod.name.Contains("White"))
                {
                    if (!bumperAtWallWhite[backRod, 0])
                    {
                        mover.MoveRod(rod.GetComponent<Rigidbody>(), movement);
                        bumperAtWallWhite[backRod, 1] = false;
                    }
                }
                else if(rod.name.Contains("Black"))
                {
                    if (!bumperAtWallBlack[backRod, 0])
                    {
                        mover.MoveRod(rod.GetComponent<Rigidbody>(), movement);
                        bumperAtWallBlack[backRod, 1] = false;
                    }
                }
            }else if (movement<0)//move down
            {
                if (rod.name.Contains("White"))
                {
                    if (!bumperAtWallWhite[backRod, 1])
                    {
                        mover.MoveRod(rod.GetComponent<Rigidbody>(), movement);
                        bumperAtWallWhite[backRod, 0] = false;
                    }
                }
                else if (rod.name.Contains("Black"))
                {
                    if (!bumperAtWallBlack[backRod, 1])
                    {
                        mover.MoveRod(rod.GetComponent<Rigidbody>(), movement);
                        bumperAtWallBlack[backRod, 0] = false;
                    }
                }
            }
        }

        private int getWallIndex(Transform rod)
        {
            switch(rod.name)
            {
                case "TopWall":
                    return 0;
                case "BottomWall":
                    return 1;
                default:
                    return -1;
            }
        }

        private void MoveRodToEdge(Transform rodParent,int direction=0)
        {
            Transform realRod = rodParent.FindChild("Rod");
            Transform bumper = rodParent.FindChild("Bumper");
            Rigidbody rb = rodParent.GetComponent<Rigidbody>();
            float toMove = realRod.localScale.y / 2 - bumper.localPosition.z - bumper.localScale.y;

            if (direction == 1)
                toMove *= -1;
            rb.MovePosition(new Vector3(rb.position.x, rb.position.y, toMove));
        }        

        private GameObject GetCorrectRod()
        {
            string rodName = "White " + (backRod + 1).ToString();
            return GameObject.Find(rodName);
        }
    }
}

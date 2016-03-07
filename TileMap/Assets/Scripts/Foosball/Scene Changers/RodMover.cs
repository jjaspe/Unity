using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Foosball.InputHandlers;
using UnityEngine;
using Assets.Scripts.Foosball.Triggers;

namespace Assets.Scripts.Foosball
{
    public class RodMover:FoosballScript
    {
        static RodMover instance;
        public static RodMover Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (new GameObject()).AddComponent<RodMover>();
                }
                return instance;
            }
        }

        private RodMover()
        {

        }

        int rotateSpeed { get { return Manager.rotateSpeed; } }
        float moveSpeed { get { return Manager.moveSpeed; } }
        WallCollisionHandler wallHandler { get { return WallCollisionHandler.Instance; } }

        public bool noMove = false, noRotate = false;

        public void RotateRod(GameObject rod, float movement)
        {
            rod.transform.Rotate(new Vector3(0, 0, rotateSpeed * movement));
        }

        public void MoveRod(Rigidbody rod, float movement)
        {
            rod.transform.Translate(new Vector3(0, 0, movement));
        }

        public void MoveRodToEdge(Transform rodParent, int direction = 0)
        {
            Transform realRod = rodParent.FindChild("Rod");
            Transform bumper = rodParent.FindChild("Bumper");
            Rigidbody rb = rodParent.GetComponent<Rigidbody>();
            float toMove = realRod.localScale.y / 2 - bumper.localPosition.z - bumper.localScale.y;

            if (direction == 1)
                toMove *= -1;
            rb.MovePosition(new Vector3(rb.position.x, rb.position.y, toMove));
        }

        public void tryMoveRod(GameObject rod, float movement,
            bool[,] bumperAtWallWhite,bool[,] bumperAtWallBlack,int backRod)
        {
            if (movement >= 0)//move up
            {
                if (rod.name.Contains("White"))
                {
                    if (!bumperAtWallWhite[backRod, 0])
                    {
                        MoveRod(rod.GetComponent<Rigidbody>(), movement);
                        bumperAtWallWhite[backRod, 1] = false;
                    }
                }
                else if (rod.name.Contains("Black"))
                {
                    if (!bumperAtWallBlack[backRod, 0])
                    {
                        MoveRod(rod.GetComponent<Rigidbody>(), movement);
                        bumperAtWallBlack[backRod, 1] = false;
                    }
                }
            }
            else if (movement < 0)//move down
            {
                if (rod.name.Contains("White"))
                {
                    if (!bumperAtWallWhite[backRod, 1])
                    {
                        MoveRod(rod.GetComponent<Rigidbody>(), movement);
                        bumperAtWallWhite[backRod, 0] = false;
                    }
                }
                else if (rod.name.Contains("Black"))
                {
                    if (!bumperAtWallBlack[backRod, 1])
                    {
                        MoveRod(rod.GetComponent<Rigidbody>(), movement);
                        bumperAtWallBlack[backRod, 0] = false;
                    }
                }
            }
        }

        public void MoveRods(List<GameObject> rods, MouseMovement movement)
        {
            foreach (GameObject rod in rods)
            {
                if (!noMove)
                    tryMoveRod(rod, moveSpeed * movement.Speeds[1] / 100,
                        wallHandler.bumperAtWallWhite, wallHandler.bumperAtWallBlack, 
                        wallHandler.getRodIndex(rod.transform));
                if (!noRotate)
                    RotateRod(rod, rotateSpeed * movement.Speeds[0] / 500);
            }
        }
    }
}

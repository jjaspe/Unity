using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Foosball
{
    public class RodMover:FoosballScript
    {
        int rotateSpeed { get { return Manager.rotateSpeed; } }

        public void RotateRod(GameObject rod, float movement)
        {
            rod.transform.Rotate(new Vector3(0, 0, rotateSpeed * movement));
        }

        public void MoveRod(Rigidbody rod, float movement)
        {
            rod.transform.Translate(new Vector3(0, 0, movement));
        }
    }
}

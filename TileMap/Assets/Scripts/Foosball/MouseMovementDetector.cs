using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Foosball
{
    public class MouseMovement
    {
        public float[] Speeds = new float[4];
        public bool[] Buttons = new bool[3];     
    }

    public class MouseMovementDetector:FoosballScript
    {
        public Vector3 deltaLeft = Vector3.zero,deltaRight=Vector3.zero;
        private Vector3 lastPosLeft = Vector3.zero,lastPosRight=Vector3.zero;
 
        void Update()
        {
            MouseMovement movement = new MouseMovement();
            if (Input.GetMouseButtonDown(0))
            {
                lastPosLeft = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                deltaLeft = Input.mousePosition - lastPosLeft;
                movement.Buttons[0] = true;
                movement.Speeds[0] = deltaLeft.x;
                movement.Speeds[1] = deltaLeft.y;
                lastPosLeft = Input.mousePosition;

                Manager.MouseMoved(movement);
            }
        }
    }
}

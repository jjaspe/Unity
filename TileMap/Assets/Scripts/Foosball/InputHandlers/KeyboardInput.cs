using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Foosball.InputHandlers
{
    public class KeyboardInput:FoosballScript
    {
        List<string> DownKeys = new List<string>();
        public void Update()
        {
            CheckKey(KeyCode.Q);
            CheckKey(KeyCode.W);
            CheckKey(KeyCode.E);
            CheckKey(KeyCode.R);

            bool noMove=false, noRotate=false;
            if (Input.GetKeyDown(KeyCode.LeftShift))
                noMove = true;
            if (Input.GetKeyUp(KeyCode.LeftShift))
                noMove = false;
            if (Input.GetKeyDown(KeyCode.LeftControl))
                noRotate = true;
            if (Input.GetKeyUp(KeyCode.LeftControl))
                noRotate = false;

            this.Manager.KeysPressed(DownKeys,noMove,noRotate);
        }

        void CheckKey(KeyCode key)
        {
            if (Input.GetKeyDown(key))
                DownKeys.Add(key.ToString());
            if (Input.GetKeyUp(key))
                DownKeys.Remove(key.ToString());
        }
    }
}

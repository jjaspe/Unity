using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Foosball.InputHandlers
{
    public class KeyboardInputManager:FoosballScript
    {
        static KeyboardInputManager instance;
        public static KeyboardInputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (new GameObject()).AddComponent<KeyboardInputManager>();
                }
                return instance;
            }
        }

        public List<string> keysPressed = new List<string>();
        List<int> selectedRods = new List<int>();

        public List<GameObject> GetSelectedRods()
        {
            selectedRods = new List<int>();
            List<GameObject> rods = new List<GameObject>();
            string rodColor = "White";
            int rodNumber = 0;
            foreach (string _char in keysPressed)
            {
                switch (_char.ToUpper())
                {
                    case "Q":
                        rodNumber = 1;
                        break;
                    case "W":
                        rodNumber = 2;
                        break;
                    case "E":
                        rodNumber = 3;
                        break;
                    case "R":
                        rodNumber = 4;
                        break;
                    default:
                        rodNumber = 0;
                        break;
                }
                if (rodNumber > 0 && !selectedRods.Contains(rodNumber))
                {
                    rods.Add(GameObject.Find(rodColor + " " + rodNumber));
                    selectedRods.Add(rodNumber);
                }
            }
            return rods;
        }
    }
}

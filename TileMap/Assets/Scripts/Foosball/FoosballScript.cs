using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Foosball
{
    public class FoosballScript:MonoBehaviour
    {
        public GameManager Manager { get { return GameManager.Instance; } }
    }
}

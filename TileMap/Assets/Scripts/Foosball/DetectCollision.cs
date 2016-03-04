using UnityEngine;
using System.Collections;
using Assets.Scripts.Foosball;

public class DetectCollision : FoosballScript {    
	
    void OnTriggerEnter(Collider collider)
    {        
        if(collider.name=="TopWall" || collider.name=="BottomWall")
        {
            Debug.Log("HitWall");
            Manager.RodHitWall(collider, this.gameObject);
        }
            
    }
}

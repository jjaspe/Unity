using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Foosball;

public class GameScript : FoosballScript
{
    public int angularSpeed = 200;
    public float moveSpeed = 0.1f;
    public float dropSpeed = 10f;
        
	// Use this for initialization
	void Start () {
        Manager.rotateSpeed = angularSpeed;
        Manager.moveSpeed = moveSpeed;
        Manager.dropSpeed = (int)dropSpeed;
        GameObject ball = GameObject.Find("Ball");
        ball.transform.Translate(0, 0, 5);
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        ballRb.velocity = new Vector3(0, 0, dropSpeed);
    }
}

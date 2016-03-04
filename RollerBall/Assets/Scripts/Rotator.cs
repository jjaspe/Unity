using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float rotationSpeed=30;
    private float mixer=1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15*rotationSpeed/360, 30*rotationSpeed/360, 45*rotationSpeed/360));
	}
}

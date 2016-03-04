using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"),
        moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(speed * moveHorizontal, 0, speed * moveVertical);
        player.AddForce(movement);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            return;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag.Equals("Wall"))
            return;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public int velocity;
    private Rigidbody2D myRB;
    private Vector3 ogPos;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRB.velocity = new Vector3(velocity, 0f, 0f);
        if (transform.position.x >= 14)
        {
            transform.position = ogPos;
        }
    }
}

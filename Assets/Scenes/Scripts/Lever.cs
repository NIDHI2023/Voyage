using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject lever;
    private bool isUp;
    public Transform startPos;
    public Transform endPos;
    public GameObject movingThing;

    // Start is called before the first frame update
    void Start()
    {
        isUp = true;
        startPos.position = movingThing.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //open door
        if (!isUp)
        {
            Debug.Log("mvoing up");
            movingThing.transform.position = Vector3.MoveTowards(movingThing.transform.position, endPos.position, 5 * Time.deltaTime);
        }
        //close door
        else if (isUp)
        {
            movingThing.transform.position = Vector3.MoveTowards(movingThing.transform.position, startPos.position, 5 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggered");
        isUp = !isUp;
            lever.transform.localScale = new Vector3(lever.transform.localScale.x, -lever.transform.localScale.y, lever.transform.localScale.z);
        
    }
}

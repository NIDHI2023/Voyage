using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject button;
    private bool isPressed;
    private Vector3 ogPos;

    public Transform startPos;
    public Transform endPos;
    public GameObject movingThing;
    // Start is called before the first frame update
    void Start()
    {
        startPos.position = movingThing.transform.position;
        ogPos = button.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            button.transform.position = new Vector3(button.transform.position.x, ogPos.y - 0.25f, button.transform.position.z);
            movingThing.transform.position = Vector3.MoveTowards(movingThing.transform.position, endPos.position, 5 * Time.deltaTime);
        } else
        {
            button.transform.position = ogPos;
            movingThing.transform.position = Vector3.MoveTowards(movingThing.transform.position, startPos.position, 5 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
        isPressed = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isPressed = false;
    }
}

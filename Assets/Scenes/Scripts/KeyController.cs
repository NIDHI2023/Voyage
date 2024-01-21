using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KeyController : MonoBehaviour
{
    public List<GameObject> objects;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in objects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (GameObject obj in objects)
        {
            obj.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}

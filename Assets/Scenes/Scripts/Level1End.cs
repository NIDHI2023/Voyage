using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1End : MonoBehaviour
{
    public string whoseDoor;
    public GameObject bars;
    private LevelManager level;
    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Equals(whoseDoor) && whoseDoor.Equals(level.leadingPlayer.name))
        {
            level.leadingPlayer.gameObject.SetActive(false);
            bars.SetActive(false);
            if (level.leadingPlayer != level.followingPlayer)
            {
                level.leadingDone = true;
            } else
            {
                level.followingDone = true;
            }
            level.leadingPlayer = level.followingPlayer;
            level.leadingPlayer.GetComponent<Rigidbody2D>().simulated = true;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController target;

    public float aheadDistance;

    public float aheadYDistance;

    public float slideInTime;

    private Vector3 targetPos;

    public bool following;

    private LevelManager level;
    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<LevelManager>();
        target = level.leadingPlayer;
        following = true;

    }

    // Update is called once per frame
    void Update()
    {
        target = level.leadingPlayer;
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y + aheadYDistance, transform.position.z);

        if (target.transform.localScale.x > 0f)
        {
            if (target.transform.localScale.y > 0f)
            {
                targetPos = new Vector3(targetPos.x + aheadDistance, targetPos.y + aheadYDistance, targetPos.z);
            }
            else
            {
                targetPos = new Vector3(targetPos.x + aheadDistance, targetPos.y - aheadYDistance, targetPos.z);
            }

        }
        else if (target.transform.localScale.x < 0f)
        {
            if (target.transform.localScale.y > 0f)
            {
                targetPos = new Vector3(targetPos.x - aheadDistance, targetPos.y + aheadYDistance, targetPos.z);
            }
            else
            {
                targetPos = new Vector3(targetPos.x - aheadDistance, targetPos.y - aheadYDistance, targetPos.z);
            }

            targetPos = new Vector3(targetPos.x - aheadDistance, targetPos.y, targetPos.z);
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, slideInTime * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerController leadingPlayer;
    public PlayerController followingPlayer;
    public bool leadingDone;
    public bool followingDone;
    public string levelNameToLoad;
    public string levelNameToUnlock;

    public float respawnCountdown;
    public bool respawnActive;
    public GameObject gameOver;
    private GameObject killPlane;
    private bool canRespawn;
    private List<ResetPlayerDeath> objectsResetting;
    private PauseMenu pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("at start the leading player is " + leadingPlayer.name);
        pauseMenu = FindObjectOfType<PauseMenu>();
        pauseMenu.gameObject.SetActive(true);
        killPlane = GameObject.FindGameObjectWithTag("KillPlane");
        canRespawn = true;
        objectsResetting = FindObjectsOfType<ResetPlayerDeath>().ToList();
        leadingDone = false;
        followingDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && leadingPlayer.isGrounded && !leadingPlayer.onLadder && !followingPlayer.onLadder && (!leadingPlayer.name.Equals(followingPlayer.name)))
        {
            
            PlayerController temp = leadingPlayer;
            leadingPlayer = followingPlayer;
            leadingPlayer.GetComponent<Rigidbody2D>().simulated = true;

            followingPlayer = temp;
            followingPlayer.GetComponent<Rigidbody2D>().simulated = false;
            Debug.Log("toggle");
            Debug.Log("new leading player is " + leadingPlayer.name  + " and new following is " + followingPlayer.name);
            //leadingPlayer.transform.position = followingPlayer.transform.position;
            //followingPlayer.transform.position = temp.transform.position;
        }

        if (leadingDone && followingDone)
        {
            StartCoroutine("LevelLoadCoroutine");
        }

    }

    public void Respawn()
    {
        if (canRespawn)
        {
                StartCoroutine("RespawnCoroutine");
        }
        else {
            leadingPlayer.gameObject.SetActive(false);
            gameOver.SetActive(true);

                //levelMusic.Stop();
                //gameOverMusic.Play();
        }
    }

    public IEnumerator RespawnCoroutine()
    {
        respawnActive = true;
        leadingPlayer.gameObject.SetActive(false);
        followingPlayer.gameObject.SetActive(false);

        killPlane.SetActive(false);

        yield return new WaitForSeconds(respawnCountdown);

        respawnActive = false;
        canRespawn = true;

        leadingPlayer.transform.position = leadingPlayer.respawnPos;
        followingPlayer.transform.position = followingPlayer.respawnPos;

        foreach (ResetPlayerDeath obj in objectsResetting)
        {
            //make obj active and the move them to position we need to make active first before moving them. when they're not active can't use script methods
            obj.gameObject.SetActive(true);
            obj.ResetAfterDeath();
        }

        //we now can reactivate ourselves since we have been moved to the correct position
        leadingPlayer.gameObject.SetActive(true);
        followingPlayer.gameObject.SetActive(true);

        Camera.main.transform.position = new Vector3(leadingPlayer.transform.position.x, 0f, Camera.main.transform.position.z);

        yield return new WaitForSeconds(1);
        killPlane.SetActive(true);

    }

    public IEnumerator LevelLoadCoroutine()
    {
        PlayerPrefs.SetInt(levelNameToUnlock, 1);
        //leadingPlayer.gameObject.SetActive(false);
        //followingPlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(levelNameToLoad);

    }
}


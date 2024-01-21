using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public string levelSelect;
    public string mainMenu;
    private LevelManager levelManager;

    private PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        pauseMenu.gameObject.SetActive(false);
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void RestartLevel()
    {
        //PlayerPrefs.SetInt("GemAmount", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelectLoad()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene(mainMenu);
    }
}

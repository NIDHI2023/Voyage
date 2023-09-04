using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private string levelToLoad;
    public bool isUnlocked;
    public List<GameObject> levels;
    private GameObject[] l;
    private int current;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        PlayerPrefs.SetInt("level1", 1);
        l = levels.ToArray();
        Debug.Log(l.GetLength(0));
        l[0].SetActive(true);
        for (int i = 1; i < l.GetLength(0); i++)
        {
            l[i].SetActive(false);
        }
    }

    public void Right()
    {
        l[current].SetActive(false);
        current++;
        if (current >= l.GetLength(0))
        {
            current = 0;
        }
        l[current].SetActive(true);

    }

    public void Left()
    {
        l[current].SetActive(false);
        current--;
        if (current < 0)
        {
            current = l.GetLength(0) - 1;
        }
        l[current].SetActive(true);
    }

    public void Click()
    {
        isUnlocked = PlayerPrefs.GetInt(l[current].name) == 1;
        if (isUnlocked)
        {
            SceneManager.LoadScene(l[current].name);
        }
    }
}

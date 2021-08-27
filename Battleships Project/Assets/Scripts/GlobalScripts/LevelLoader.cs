using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transotionTime = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            switchScene("MainMenu");
        }
    }

    public void switchScene(string sceneName)
    {
        StartCoroutine(switchLevel(sceneName));
    }

    IEnumerator switchLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transotionTime);
        SceneManager.LoadScene(levelName);
    }
}
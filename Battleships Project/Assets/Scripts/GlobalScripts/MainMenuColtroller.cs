using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuColtroller : MonoBehaviour
{
    public LevelLoader ll;
    //public Animator confirrmExit;
    
    public void play()
    {
        ll.switchScene("MainScene");
    }

    public void exitButton()
    {
        Application.Quit();
    }

}

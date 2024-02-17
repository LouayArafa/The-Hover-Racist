using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitcherMenu : MonoBehaviour
{
   public void TrainingScene()
    {
        SceneManager.LoadScene(1);
    }
    public void AlphaScene()
    {
        SceneManager.LoadScene(2);

    }
    public void Quit()
    {
        Application.Quit();
    }
}

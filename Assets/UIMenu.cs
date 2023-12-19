using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public void ExitApplicaiton()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}

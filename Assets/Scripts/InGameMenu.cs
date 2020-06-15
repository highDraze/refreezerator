using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public bool paused = false;
    Scene scene;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                Time.timeScale = 0f;
                SceneManager.LoadScene(0, LoadSceneMode.Additive);
            } else
            {
                SceneManager.UnloadSceneAsync(0, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                Time.timeScale = 1f;
            }
            paused = !paused;
        }

    }
}

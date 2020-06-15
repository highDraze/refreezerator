using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class PlayButton : MonoBehaviour
{

    public Button playButton;

// Start is called before the first frame update
    void Start()
    {
        Button btn = playButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {

        SceneManager.LoadScene("Level01", LoadSceneMode.Single);

    }
}
    
  
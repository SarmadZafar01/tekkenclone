using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageScript : MonoBehaviour
{
    public void SelectStage(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }
}

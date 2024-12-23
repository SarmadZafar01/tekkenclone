using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selectCharacterandStageMenul;
    public GameObject OptionMenu;
    public GameObject controlMenu;


    private void Start()
    {
        mainMenu.SetActive(true);
        selectCharacterandStageMenul.SetActive(false);
        OptionMenu.SetActive(false);
        controlMenu.SetActive(false);
    }

    public void PlayButton()
    {
        mainMenu.SetActive(false);
        selectCharacterandStageMenul.SetActive(true);

    }

    public void OptionButton()
    {
        mainMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void ControlButton()
    {
        OptionMenu.SetActive(false);
        controlMenu.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void backbutton()
    {
        mainMenu.SetActive(true);
        selectCharacterandStageMenul.SetActive(false);
        OptionMenu.SetActive(false);
        controlMenu.SetActive(false);
    }

    public void SelectCharacter(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }

    public void SelectStage(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

}


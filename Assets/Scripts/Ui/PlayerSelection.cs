using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSelection : MonoBehaviour
{
    public GameObject playerCharacter;
    private GameObject[] allcharacter;
    private int currentindex = 0;

    private void Start()
    {
        allcharacter = new GameObject[playerCharacter.transform.childCount];

        for (int i = 0; i < playerCharacter.transform.childCount; i++)
        {
            allcharacter[i] = playerCharacter.transform.GetChild(i).gameObject;
            allcharacter[i].SetActive(false);
        }

        if (PlayerPrefs.HasKey("SelectedCharacterIndex"))
        {
            currentindex = PlayerPrefs.GetInt("SelectedCharacterIndex");
        }

        ShowCurrentCharacter();
    }

    void ShowCurrentCharacter()
    {
        foreach (GameObject character in allcharacter)
        {
            character.SetActive(false);
        }

        allcharacter[currentindex].SetActive(true);
    }

    public void NextCharacter()
    {
        currentindex=(currentindex+1)%allcharacter.Length;
        ShowCurrentCharacter();
    }

    public void PreviousCharacter()
    {
        currentindex = (currentindex - 1+ allcharacter.Length) % allcharacter.Length;
        ShowCurrentCharacter();
    }
    public void onYesButton(string sceneName)
    {
      
        PlayerPrefs.SetInt("SelectedCharacterIndex", currentindex);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }
    
}

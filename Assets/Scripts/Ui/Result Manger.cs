using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManger : MonoBehaviour
{

    public GameObject resultPanel;
    public Text resultText;
    public CharacterFighterController[] characterFighterControllers;
    public OpponentAI[] opponentAI;

    private void Update()
    {
        foreach (CharacterFighterController characterFighterController in characterFighterControllers)
        {
            if(characterFighterController.gameObject.activeSelf && characterFighterController.currentHealth<=0)
            {
                setResult("You Are Dead");
                return;
            }
        }


        foreach (OpponentAI opponentAI in opponentAI)
        {
            if (opponentAI.gameObject.activeSelf && opponentAI.currentHealth <= 0)
            {
                setResult("You Win!");
                return;
            }
        }
    }

    void setResult(string result)
    {
        resultText.text = result;
        resultPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadMianMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}

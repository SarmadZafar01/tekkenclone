using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppManger : MonoBehaviour
{
    public GameObject[] opponentCharacter;


    private void Start()
    {
        if (opponentCharacter.Length == 0)
        {
            Debug.Log("No Opponent Assign in match");
            return;
        }
        else
        {
            ActiviteRandomOppoenent();
        }
    }
    void ActiviteRandomOppoenent()
    {
        int randomOppIndex = Random.Range(0, opponentCharacter.Length); 

        for (int i = 0; i < opponentCharacter.Length; i++)
        {
            if(i== randomOppIndex)
            {
                opponentCharacter[i].SetActive(true);
            }
            else
            {
                opponentCharacter[i].SetActive(false);
            }
        }
    }
  
 
}

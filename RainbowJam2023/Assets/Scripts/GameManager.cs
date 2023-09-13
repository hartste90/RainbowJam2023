using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    
    public GameObject playerDiedModal;
    public GameObject tutorialModal;
    public List<GameObject> allyPickupModals;
    public List<GameObject> enemyPickupModals;


    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    
    public void OnPlayerDied()
    {
        //pause game
        PauseGame();
        //show game over screen
        playerDiedModal.SetActive(true);
    }

    public void ShowTutorialModal()
    {
        tutorialModal.SetActive(true);
        PauseGame();
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }


    public void ShowAllyPickupModal(int allyIndex)
    {
        if (allyPickupModals.Count > allyIndex)
        {
            allyPickupModals[allyIndex].SetActive(true);
            PauseGame();
        }
    }

    public void ShowEnemyModal(int enemyIndex)
    {
        if (enemyPickupModals.Count > enemyIndex)
        {
            enemyPickupModals[enemyIndex].SetActive(true);
            PauseGame();
        }
    }
}

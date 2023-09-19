using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    
    public GameObject playerDiedModal;
    public GameObject tutorialModal;
    public GameObject gameCompletedModal;
    public List<GameObject> allyPickupModals;
    public List<GameObject> enemyPickupModals;
    public GameObject sparklerGO;

    private int totalAllyCount;


    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        totalAllyCount = FindObjectsOfType<AllyController>().Length;
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

    public void WinGame()
    {
        //show the win screen
        gameCompletedModal.SetActive(true);
        sparklerGO.SetActive(true);
        for(int i = 0; i < PlayerManager.Instance.allies.Count; i++)
        {
            AllyController ally = PlayerManager.Instance.allies[i];
            ally.transform.position = PlayerManager.Instance.transform.position + Vector3.right * (i + 1);
            ally.PlayJumpingAnimation();
        }
        //turn off keyboard input
        PlayerManager.Instance.DisableGameplayInput();
    }

    public bool IsAllAlliesFollowing()
    {
        return totalAllyCount == PlayerManager.Instance.allies.Count;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

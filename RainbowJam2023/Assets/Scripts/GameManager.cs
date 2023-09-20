using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    
    public DialogueModalController playerDiedModal;
    public DialogueModalController tutorialModal;
    public DialogueModalController gameCompletedModal;
    public List<DialogueModalController> allyPickupModals;
    public List<DialogueModalController> enemyPickupModals;
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
        //show game over screen
        playerDiedModal.Show();
    }

    public void ShowTutorialModal()
    {
        tutorialModal.Show();
    }
    
    public void PauseGame()
    {
        Debug.Log("Paused");
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Debug.Log("Unpaused");
        Time.timeScale = 1;
    }


    public void ShowAllyPickupModal(int allyIndex)
    {
        if (allyPickupModals.Count > allyIndex)
        {
            allyPickupModals[allyIndex].Show();
        }
    }

    public void ShowEnemyModal(int enemyIndex)
    {
        if (enemyPickupModals.Count > enemyIndex)
        {
            enemyPickupModals[enemyIndex].Show();
        }
    }

    public void WinGame()
    {
        //show the win screen
        gameCompletedModal.Show();
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

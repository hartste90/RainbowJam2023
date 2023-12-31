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
    public bool isPaused = false;

    public List<ModalBase> seenModals = new List<ModalBase>();

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
    
    public void PauseGame()
    {
        Debug.Log("Paused");
        Time.timeScale = 0;
        isPaused = true;
    }

    public void UnpauseGame()
    {
        Debug.Log("Unpaused");
        Time.timeScale = 1;
        isPaused = false;
    }

    
/// <summary>
/// Returns true if the modal was shown
/// </summary>
/// <param name="allyIndex"></param>
/// <returns></returns>
    public bool ShowAllyPickupModal(int allyIndex)
    {
        if (allyPickupModals.Count > allyIndex && allyIndex >= 0)
        {
            ModalBase m = allyPickupModals[allyIndex];
            if (!seenModals.Contains(m))
            {
                allyPickupModals[allyIndex].Show();
                seenModals.Add(m);
                return true;
            }
        }

        return false;
    }

    public void ShowEnemyModal(int enemyIndex)
    {
        if (enemyPickupModals.Count > enemyIndex)
        {
            ModalBase m = enemyPickupModals[enemyIndex];
            if (!seenModals.Contains(m))
            {
                enemyPickupModals[enemyIndex].Show();
                seenModals.Add(m);
            }
        }
    }

    public void WinGame()
    {
        //show the win screen
        gameCompletedModal.Show();
        sparklerGO.SetActive(true);
        //turn off keyboard input
        PlayerManager.Instance.DisableGameplayInput();
        //move all allies to the player and jump
        for(int i = 0; i < PlayerManager.Instance.allies.Count; i++)
        {
            AllyController ally = PlayerManager.Instance.allies[i];
            ally.transform.position = PlayerManager.Instance.transform.position + Vector3.left * (i + 1);
            ally.PlayJumpingAnimation();
        }
        
    }

    public bool IsAllAlliesFollowing()
    {
        return totalAllyCount == PlayerManager.Instance.allies.Count;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("TitleScene");
    }
    
    public void PlayGainedAllySound()
    {
        SoundManager.Instance.PlayGainedFollowerClip();
    }
    
    public void PlayUiClickSound()
    {
        SoundManager.Instance.PlayUISoundClip();
    }
}

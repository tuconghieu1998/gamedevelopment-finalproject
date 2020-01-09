using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] GameObject WinMenuPanel;

    [SerializeField] Button BackToMenuButton;

    [SerializeField] Button NextLevelButton;

    private void Start()
    {
        WinMenuPanel.SetActive(false);
        GameManager.Instance.EventBus.AddListener("OnAllEnemiesKilled", () =>
        {
            GameManager.Instance.Timer.Add(() => {
                if (Cursor.visible == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    print("Active curcor");
                }
                GameManager.Instance.IsPaused = true;
                WinMenuPanel.SetActive(true);
                
            }, 4);
        });
        BackToMenuButton.onClick.AddListener(()=> {
            //fix
            //GameManager.Instance.Timer.ClearEvent();
            SceneManager.LoadScene("MainMenu");
        });
        NextLevelButton.onClick.AddListener(() => {
            //fix
            //GameManager.Instance.Timer.ClearEvent();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("TheSwat1");
        });
    }
}

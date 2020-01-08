using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] GameObject EscapeMenuPanel;

    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;

    private void Start()
    {
        EscapeMenuPanel.SetActive(false);
        YesButton.onClick.AddListener(OnYesClicked);
        NoButton.onClick.AddListener(OnNoClicked);
    }

    void OnYesClicked()
    {
        //fix
        //GameManager.Instance.Timer.ClearEvent();
        SceneManager.LoadScene("MainMenu");
    }

    void OnNoClicked()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.IsPaused = false;
        EscapeMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (EscapeMenuPanel.activeSelf)
            return;
        if (GameManager.Instance.InputController.Escape)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.Instance.IsPaused = true;
            EscapeMenuPanel.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerControl playerControl;
    public DelayText delayText;
    public Slider HPBar;
    public Slider O2Bar;
    public GameObject MenuUI;
    public Text totalTime;

    public void InitGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameData.Instance.isMouse = false;
        HPBar = GameObject.Find("HPBar").GetComponent<Slider>();
        O2Bar = GameObject.Find("O2Bar").GetComponent<Slider>();  
    }


    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameModeChange()
    {
        if (!GameData.Instance.isMouse)
        {
            Cursor.lockState = CursorLockMode.None;
            GameData.Instance.isMouse = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameData.Instance.isMouse = false;
        }
    }
    public void SetGameClear(bool isGame)
    {
        MenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GameData.Instance.isMouse = true;
        if (isGame)
        {
            MenuUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Game Clear";
        }
        else
        {
            MenuUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Game Over";
        }
    }
    public void BtnMenu(int num)
    {
        switch (num)
        {
            case 0:
                {
                    GameData.Instance.isGame = false;
                    SceneManager.LoadScene("Main");
                }
                break;
            case 1:
                {
                    GameData.Instance.isGame = false;
                    SceneManager.LoadScene("Game");
                }
                break;
            case 2:
                {
                    Application.Quit();
                }
                break;
        }
    }
}

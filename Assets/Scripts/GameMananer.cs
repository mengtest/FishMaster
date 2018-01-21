using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMananer : MonoBehaviour {

    private Button newGame;
    private Button loadGame;
    private void Awake()
    {
        newGame = GameObject.Find("NewGame").GetComponent<Button>();
        loadGame = GameObject.Find("LoadGame").GetComponent<Button>();
        newGame.onClick.AddListener(NewGame);
        loadGame.onClick.AddListener(LoadGame);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void NewGame()
    {
        PlayerPrefs.SetInt("Lv", 0);
        PlayerPrefs.SetInt("Exp", 0);
        PlayerPrefs.SetInt("Gold", 10000);
        SceneManager.LoadScene("Main");
    }
}

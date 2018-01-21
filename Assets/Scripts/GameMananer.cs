using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMananer : MonoBehaviour {

    private Button newGame;
    private Button loadGame;
    private AudioSource audioSource;
    private void Awake()
    {
        newGame = GameObject.Find("NewGame").GetComponent<Button>();
        loadGame = GameObject.Find("LoadGame").GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        newGame.onClick.AddListener(NewGame);
        loadGame.onClick.AddListener(LoadGame);
    }
    public void LoadGame()
    {
        audioSource.Play();
        SceneManager.LoadScene("Main");
    }

    private void NewGame()
    {
        audioSource.Play();
        PlayerPrefs.SetInt("Lv", 0);
        PlayerPrefs.SetInt("Exp", 0);
        PlayerPrefs.SetInt("Gold", 10000);
        SceneManager.LoadScene("Main");
    }
}

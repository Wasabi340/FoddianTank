using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = true;
    public static bool gameStarted = false;
    public static bool gameEnded = false;
    public GameObject pauseMenuUI;
    public PlayerController pc;
    public GameObject player;
    public Text resumeUI;

    private bool musicOn = true;
    public Image musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    private void Start()
    {
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnded) {
            if (gameIsPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        if (!gameStarted)
        {
            gameStarted = true;
            player.SetActive(true);
        }
        else if (pc.active) {
            pc.Explode(false);
        }
    }

    private void Pause() {

        if (gameStarted)
        {
            if (!pc.active)
            {
                resumeUI.text = "RESUME";
            }
            else
            {
                resumeUI.text = "EXPLODE";
            }
        }
        else {
            resumeUI.text = "START";
        }

        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
    }
    public void Quit()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }

    public void Restart() {
        gameEnded = false;
        gameIsPaused = true;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    public void ToggleMusic() {
        if (musicOn)
        {
            musicOn = false;
            musicButton.sprite = musicOffSprite;
            FindObjectOfType<AudioManager>().Stop("Theme");
        }
        else {
            musicOn = true;
            musicButton.sprite = musicOnSprite;
            FindObjectOfType<AudioManager>().Play("Theme");
        }
    }
}

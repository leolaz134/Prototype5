using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject volumeElement;
    public AudioSource backgroundMusic;
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public int lives = 3;
    private int score;
    private float spawnRate = 1.0f;

    void Update()
    {
        AdjustVolume();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        volumeElement.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.text = "Lives: " + lives;
        livesText.gameObject.SetActive(true);
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
    }

    public void SetDifficulty(int difficulty)
    {
        spawnRate /= difficulty;
    }

    public void AdjustVolume()
    {
        //This grabs the slider compenent from the empty parent volumeElement
        //and sets volume of music to slider value
        //Different way:
        //volumeElement.transform.Find("Volume Slider").GetComponent<Slider>().value
        backgroundMusic.volume = volumeElement.GetComponentInChildren<Slider>().value;
    }
    //NOTE: Making a text a child object of a panel doesn't make it affected
    //by the panel's transparent color
    public void PauseScreen()
    {
        //Time.timeScale = 0 freezes all time-based operations in Unity
        if(isGameActive)
        {
            isGameActive = false;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            isGameActive = true;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }

    
}

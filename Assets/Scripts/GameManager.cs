using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;
    public int score;
    public int lives;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public bool isGameActive;
    public GameObject pausePanel;
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        bool pressPause=Input.GetKeyDown(KeyCode.P);
        if (pressPause)
        {
            pausePanel.SetActive(!pausePanel.activeInHierarchy);
            Time.timeScale = 1 - Time.timeScale;
        }

    }
    IEnumerator SpawnTargets()
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
    public void UpdateLives(int liveToAdd)
    {
        lives += liveToAdd;
        liveText.text = "Lives: " + lives;
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        lives = 3;
        titleScreen.SetActive(false);
        isGameActive = true;
        StartCoroutine("SpawnTargets");
        score = 0;
        UpdateScore(0);
        UpdateLives(0);
    }
}

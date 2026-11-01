using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UiManager uiManager;
    public ZombieSpawner zombieSpawner;

    private int score = 0;

    public bool isGameOver { get; private set; }

    private void Start()
    {
        score = 0;
        isGameOver = false;

        uiManager.SetScore(score);
        uiManager.SetActiveGameOverUI(false);
    }
    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    public void AddScore(int add)
    {
        if (isGameOver)
        {
            return;
        }

        score += add;
        uiManager.SetScore(score);
    }

    public void EndGame()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        uiManager.SetActiveGameOverUI(true);

        if (zombieSpawner != null)
        {
            zombieSpawner.enabled = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject gameOverUI;

    private void OnEnable()
    {
        SetActiveGameOverUI(false);
    }
    public void SetScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }
    public void SetActiveGameOverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }
}

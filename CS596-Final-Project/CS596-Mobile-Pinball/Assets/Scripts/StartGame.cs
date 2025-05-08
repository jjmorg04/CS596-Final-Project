using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{
    [Header("UI Screens")]
    public GameObject startUI;
    public GameObject endUI;

    [Header("UI Text Fields")]
    public TMP_Text startScreenPBText;   // PERSONAL BEST: on start screen
    public float originalTimeScale = 1.5f;

    void Start()
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (startUI != null)
            startUI.SetActive(true);

        UpdatePersonalBestDisplay();
    }

    public void StartGameNow()
    {
        Time.timeScale = originalTimeScale;

        if (startUI != null)
            startUI.SetActive(false);

        if (endUI != null)
            endUI.SetActive(false);
    }

    public void ReturnToStartScreen()
    {
        Time.timeScale = 0f;

        if (startUI != null)
            startUI.SetActive(true);

        if (endUI != null)
            endUI.SetActive(false);

        UpdatePersonalBestDisplay();
    }

    private void UpdatePersonalBestDisplay()
    {
        if (startScreenPBText != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            startScreenPBText.text = "PB " + highScore.ToString("D4");
        }
    }
}




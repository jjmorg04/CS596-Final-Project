using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{

    public GameObject startUI;
    public GameObject endUI;
    public TMP_Text startScreenPBText;   // Personal best score on start screen
    public float originalTimeScale = 1.5f;

    void Start()
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (startUI != null)
            startUI.SetActive(true);

        UpdatePersonalBestDisplay();
    }

    // Starts game with original 1.5 time scale
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

    // Update PB to screen
    private void UpdatePersonalBestDisplay()
    {
        if (startScreenPBText != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            startScreenPBText.text = "PB " + highScore.ToString("D4");
        }
    }
}




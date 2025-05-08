using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public GameObject startUI;
    private float originalTimeScale = 1.5f; // ✅ Set this to whatever your game speed is

    void Start()
    {
        originalTimeScale = Time.timeScale; // In case it's already set before this
        Time.timeScale = 0f;
        startUI.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = originalTimeScale;
        startUI.SetActive(false);
    }
}


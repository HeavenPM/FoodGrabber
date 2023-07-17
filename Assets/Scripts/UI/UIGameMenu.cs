using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private float _timeBeforeShowPanel;

    private void Start()
    {
        _pausePanel.SetActive(false);
        _losePanel.SetActive(false);
        _winPanel.SetActive(false);
    }

    public void PauseGame()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);       
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private IEnumerator ShowLosePanel()
    {
        yield return new WaitForSeconds(_timeBeforeShowPanel);
        _losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(_timeBeforeShowPanel);
        _winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        EventManager.LevelFailed += EventLevelFailed;
        EventManager.LevelPassed += EventLevelPassed;
    }

    private void OnDisable()
    {
        EventManager.LevelFailed -= EventLevelFailed;
        EventManager.LevelPassed -= EventLevelPassed;
    }

    private void EventLevelFailed()
    {
        _pauseButton.SetActive(false);
        StartCoroutine(ShowLosePanel());
    }

    private void EventLevelPassed()
    {
        _pauseButton.SetActive(false);
        StartCoroutine(ShowWinPanel());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tools;

public class MainManager : MonoSingleton<MainManager> 
{
    public bool started
    {
        get { return _started; }
    }

    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] Text _timer;

    float _time;

    bool _started;

	public void StartGame()
    {
        _started = true;

        _startPanel.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        _started = false;

        _gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!_started) return;

        _time += Time.deltaTime;

        int minutes = (int) _time / 60;
        int secondes = (int)(_time - minutes * 60);

        _timer.text = minutes.ToString("00") + ":" + secondes.ToString("00");
    }
}

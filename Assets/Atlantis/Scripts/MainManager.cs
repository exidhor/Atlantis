using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class MainManager : MonoSingleton<MainManager> 
{
    public bool started
    {
        get { return _started; }
    }

    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _gameOverPanel;

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
        _time += Time.deltaTime;
    }
}

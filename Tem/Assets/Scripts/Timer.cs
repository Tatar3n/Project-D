using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    [SerializeField] private Text timerText;
    [SerializeField] public float time;
 
    public float _timeLeft = 0f;
    private IEnumerator StartTimer() {
        while (_timeLeft > 0) {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }

        if (_timeLeft < 0)
        {
            _timeLeft = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Start() {
        _timeLeft = time;
        StartCoroutine(StartTimer());
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}

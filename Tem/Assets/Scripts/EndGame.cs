using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;

    void Update()
    {
        if (enemy == null)
        {
            End();
        }
    }

    void End()
    {
        Debug.Log("Вы убили моба! Игра завершается.");
        SceneManager.LoadScene("Menu");
    }
}

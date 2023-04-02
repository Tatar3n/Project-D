using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
	public GameObject settingsMenu;

	void Update()
	{
		
	}

	public void QuitGame()
	{
		Debug.Log("Выход");
		Application.Quit();
	}

	public void StartGame()
	{
		SceneManager.LoadScene("Main");
	}
}
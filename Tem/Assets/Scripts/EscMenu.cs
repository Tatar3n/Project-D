using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
	public GameObject settingsMenu;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ToggleSettingsMenu();
		}
	}

	public void ToggleSettingsMenu()
	{
		settingsMenu.SetActive(!settingsMenu.activeSelf);
		if (Time.timeScale == 1f)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1f;
	}

	public void QuitGame()
	{
		Debug.Log("Выход");
		Application.Quit();
	}

	public void ContinueGame()
	{
		settingsMenu.SetActive(false);
		Time.timeScale = 1f;
	}

	public void ToMenu()
	{
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1f;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public enum stateMenu
{
	Main,
	Tutorial,
	Pause,
	Play,
	Credit,
	Survey,
}
public class HUDManager : SingletonBehaviour<HUDManager> {

	public GameObject menuStart;
	public GameObject UIGame;
	public GameObject pausePanel;
	public GameObject ATH;
	public GameObject Tutorial;
	public GameObject BlackScreen;
	public GameObject EndGame;
	public GameObject CreditPanel;

	int index;
	bool joyDown;
	public Button[] buttonMain;
	public Button[] buttonPause;
	public Button[] buttonGameEnd;

	public stateMenu state = stateMenu.Main;
	bool haveJoy = false;


	 new void Awake () {
		base.Awake();
		index = 0;
		state = stateMenu.Main;
		joyDown = false;
	 }

	void Update() {

		foreach (string joy in Input.GetJoystickNames())
		{
			if (joy == "Controller(Xbox One For Windows)" || joy.Contains("Xbox") || joy.Contains("XBOX") || joy.Contains("xbox"))
			{
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				Tutorial.transform.Find("Keyboard").gameObject.SetActive(false);
				Tutorial.transform.Find("Joystick").gameObject.SetActive(true);
				haveJoy = true;
			}
			else
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				Tutorial.transform.Find("Keyboard").gameObject.SetActive(true);
				Tutorial.transform.Find("Joystick").gameObject.SetActive(false);
				haveJoy = false;
			}
		}

		if (state == stateMenu.Main && haveJoy)
		{

			if (Input.GetButtonDown("Escape"))
			{
				quitGame();
			}
			else if (Input.GetAxis("Vertical") < 0 && !joyDown)
			{
				index ++;
				joyDown = true;
			}
			else if (Input.GetAxis("Vertical") > 0 && !joyDown)
			{
				index --;
				joyDown = true;
			}
			else if (Input.GetAxis("Vertical") == 0)
			{
				joyDown = false;
			}

			index = Mathf.Clamp(index, 0, buttonMain.Length-1);
			buttonMain[index].Select();

		}
		else if (state == stateMenu.Tutorial && Input.GetButtonDown("Escape"))
		{
			Backtutorial();
		}
		else if (state == stateMenu.Credit && Input.GetButtonDown("Escape"))
		{
			Backcredits();
		}
		else if (state == stateMenu.Play && Input.GetButtonDown("Escape"))
		{
			pause();
		}
		else if (state == stateMenu.Pause && haveJoy)
		{
			if (Input.GetButtonDown("Escape"))
			{
				Backpause();
			}
			else if (Input.GetAxis("Horizontal") > 0 && !joyDown)
			{
				index++;
				joyDown = true;
			}
			else if (Input.GetAxis("Horizontal") < 0 && !joyDown)
			{
				index--;
				joyDown = true;
			}
			else if (Input.GetAxis("Horizontal") == 0)
			{
				joyDown = false;
			}
			index = Mathf.Clamp(index, 0, buttonPause.Length - 1);
			buttonPause[index].Select();
		}
		else if (state == stateMenu.Survey && haveJoy)
		{
			if (Input.GetButtonDown("Escape"))
			{
				menuLoad();
			}
			else if (Input.GetAxis("Vertical") < 0 && !joyDown)
			{
				index++;
				joyDown = true;
			}
			else if (Input.GetAxis("Vertical") > 0 && !joyDown)
			{
				index--;
				joyDown = true;
			}
			else if (Input.GetAxis("Vertical") == 0)
			{
				joyDown = false;
			}
			index = Mathf.Clamp(index, 0, buttonGameEnd.Length - 1);
			buttonGameEnd[index].Select();
		}
	}

	/*Scripts button*/

	//Menu

	public void playDemo() {
		LoadGameScene();
	}

	public void LoadGameScene()
	{
		Backtutorial();
		changeBlackScreen();
		SceneManager.LoadScene(1,LoadSceneMode.Single);
		state = stateMenu.Play;
		index = 0;

	}

	public void menuLoad()
	{
		Time.timeScale = 1f;
		state = stateMenu.Main;
		SceneManager.LoadScene(0, LoadSceneMode.Single);
		changeMenu();
		index = 0;
		Time.timeScale = 1f;
		pausePanel.SetActive(false);
		SetBlack(); SetEnd();
	}

	public void quitGame()
	{
		Application.Quit();
		Debug.Log("Game closed");
	}


	//ui\menuStart

	public void setUi()
	{
		HUDManager.Instance.menuStart.SetActive(false);
		HUDManager.Instance.UIGame.SetActive(true);
	}

	public void changeMenu()
	{
		changeState(menuStart);
		changeState(UIGame);
	}

	//Credits

	public void credits()
	{
		index = 0;
		state = stateMenu.Credit;
		CreditPanel.SetActive(true);
	}

	public void Backcredits()
	{
		state = stateMenu.Main;
		CreditPanel.SetActive(false);
	}

	//Tutorial

	public void tutorial()
	{
		state = stateMenu.Tutorial;
		Tutorial.SetActive(true);
		
	}

	public void Backtutorial()
	{
		state = stateMenu.Main;
		Tutorial.SetActive(false);
	}

	//Pause

	public void pause()
	{
		joyDown = false;
		Time.timeScale = 0f;
		state = stateMenu.Pause;
		pausePanel.SetActive(true);
	}

	public void Backpause()
	{
		Time.timeScale = 1f;
		state = stateMenu.Play;
		pausePanel.SetActive(false);
	}

	//BlackScreen
	public void SetBlack()
	{
		HUDManager.Instance.BlackScreen.SetActive(false);
	}

	public void SetEnd()
	{
		HUDManager.Instance.EndGame.SetActive(false);
	}

	public void changeBlackScreen()
	{
		changeState(BlackScreen);
	}

	public void changeEndGame()
	{
		index = 0;
		joyDown = false;
		changeState(EndGame);
		if (state == stateMenu.Survey)
		{
			state = stateMenu.Main;
		}else
		{
			state = stateMenu.Survey;
		}
	}

	//Questionnary

	public void openQuest()
	{
		if (Application.platform == RuntimePlatform.WindowsPlayer || 
			Application.platform == RuntimePlatform.WindowsEditor || 
			Application.platform == RuntimePlatform.LinuxPlayer)
		{
			Application.OpenURL("https://goo.gl/forms/8plFKg9zJ8LfzNvz2");
		}
	}

	//Utility

	public void changeState(GameObject panel)
	{
		panel.SetActive(!panel.activeSelf);
	}

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Text text;

	public void scoreScreen(){
		Application.LoadLevel("HighScore");
	}

	public void startGame(){
		Application.LoadLevel("Main");
	}


}

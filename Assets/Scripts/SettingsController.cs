using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

    public Toggle tutorialToggle;
    public Toggle sfxToggle;

    private string tutorialPrefsKey = "IsTutorialOn";
    private string sfxPrefsKey = "AreSoundEffectsOn";

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.HasKey(tutorialPrefsKey)) {
            int tutorialPref = PlayerPrefs.GetInt(tutorialPrefsKey);
            tutorialToggle.isOn = (tutorialPref > 0) ? false : true;
        }
        else {
            tutorialToggle.isOn = true;
        }

        if (PlayerPrefs.HasKey(sfxPrefsKey)) {
            int sfxPref = PlayerPrefs.GetInt(sfxPrefsKey);
            sfxToggle.isOn = (sfxPref > 0) ? false : true;
        }
        else {
            tutorialToggle.isOn = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SavePreferences() {
        int tutorialPref = (tutorialToggle.isOn) ? 0 : 1;
        int sfxPref = (sfxToggle.isOn) ? 0 : 1;

        PlayerPrefs.SetInt(tutorialPrefsKey, tutorialPref);
        PlayerPrefs.SetInt(sfxPrefsKey, sfxPref);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Main");
    }
}

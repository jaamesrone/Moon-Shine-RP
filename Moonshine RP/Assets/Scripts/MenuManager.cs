using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject OptionsMenuObject;

    bool Tutorial = true;
    [SerializeField]
    private Text TutorialText;

    //Main Menu Buttons

    public void StartGame()
    {
        MainMenu.SetActive(false);
        this.GetComponent<Inventory>().StatusEnabled = true;
    }

    public void OptionsMenu()
    {
        MainMenu.SetActive(false);
        OptionsMenuObject.SetActive(true);
    }

    public void Credits()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Options Menu Buttons


    public void TutorialToggle()
    {
        if (Tutorial) Tutorial = false;
        else Tutorial = true;
        if (!Tutorial) TutorialText.text = "TUTORIAL: OFF";
        else TutorialText.text = "TUTORIAL: ON";
    }

    public void BacktoMenu()
    {
        OptionsMenuObject.SetActive(false);
        MainMenu.SetActive(true);

    }
}

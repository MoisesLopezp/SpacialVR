using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Button : MonoBehaviour {

    scr_Menu Menu;

    public int IdButton = 0;

	// Use this for initialization
	void Start () {
        Menu = FindObjectOfType<scr_Menu>();
	}


    public void Action()
    {
        Menu.ButtonSound();
        switch(IdButton)
        {
            case 0: //Start Game
                {
                    Menu.StartGame(false);
                }break;
            case 1: //Options
                {
                    Menu.ShowOptions();
                }
                break;
            case 2: //Credits
                {
                    Menu.ShowCredits();
                }
                break;
            case 3: //End Game
                {
                    Menu.ExitGame();
                }
                break;
            case 4: //Back Menu
                {
                    Menu.BackMenu();
                }
                break;
        }
    }
}

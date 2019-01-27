using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveGameFree;

using UnityEngine.UI;

public class scr_Menu : MonoBehaviour {

    public static scr_Config MyData;
    public static string fileName = "PlayerData";

    public static int Op_Leng = 1;
    public static bool Op_360 = false;

    [HideInInspector]
    public bool HardMode = false;

    public GameObject InitialMenu;
    public GameObject Credits;
    public GameObject OptionsMenu;

    public Dropdown DD_Lang;
    public Toggle TG_r360;

    public static bool InGame = false;

    private void Awake()
    {
        MyData = new scr_Config();
        // Initialize the Saver with the default configurations
        Saver.Initialize();
        //MyData = new SaveGameFree.Data_player();
        MyData = Saver.Load<scr_Config>(fileName);
        Op_Leng = MyData.Op_Leng;
        Op_360 = MyData.Op_360;

        //Init Lang
        scr_Lang.setLanguage();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame(bool _HardMode)
    {
        InitialMenu.SetActive(false);
        scr_Mng.GM.StartGame();
        InGame = true;
        HardMode = _HardMode;
    }

    public void ChangeLang()
    {
        Op_Leng = DD_Lang.value;
        scr_Lang.setLanguage();
        SaveDataPlayer();
    }

    public void ChangeRotOps()
    {
        Op_360 = TG_r360.isOn;
        SaveDataPlayer();
    }

    public static void SaveDataPlayer()
    {
        MyData.Op_Leng = Op_Leng;
        MyData.Op_360 = Op_360;
        Saver.Save(MyData, fileName);
    }

    public void ShowGamesModes()
    {

    }

    public void ShowCredits()
    {
        InitialMenu.SetActive(false);
        Credits.SetActive(true);
    }

    public void ShowOptions()
    {
        InitialMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void BackMenu()
    {
        InitialMenu.SetActive(true);
        Credits.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

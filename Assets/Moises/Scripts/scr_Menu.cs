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

    public Dropdown DD_Lang;
    public Toggle TG_r360;

    public static bool InGame = false;

    private void Awake()
    {
        MyData = new scr_Config();
        // Initialize the Saver with the default configurations
        SaveGameFree.Saver.Initialize();
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
        scr_Mng.GM.CV_LH.SetActive(true);
        scr_Mng.GM.CV_RH.SetActive(true);
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

    }

    public void ShowOptions()
    {

    }

    public void BackMenu()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

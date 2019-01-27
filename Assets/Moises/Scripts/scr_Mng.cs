using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_Mng : MonoBehaviour {

    public static scr_Mng GM;

    public bool GameOver = false;

    int CurrentDay = 0;

    [HideInInspector]
    public System.TimeSpan TimePast = new System.TimeSpan(0,0,0,0,0);

    public float ProbWin = 0f;

    WaitForSeconds Delay1s = new WaitForSeconds(1f);

    System.TimeSpan TperSec = new System.TimeSpan(0, 8, 0);

    public scr_PlayerStats Astronaut;

    public Text Txt_Days;
    public Text Txt_Time;
    public Text Txt_ProvWin;

    public Text Txt_st_air;
    public Text Txt_st_food;
    public Text Txt_st_health;
    public Text Txt_st_Happiness;

    public GameObject CV_LH;
    public GameObject CV_RH;

    int TicsAir = 2;
    int TicsHappiness = 3;
    int TicsFood = 4;

    public GameObject GameOverScreen;
    public GameObject SuccesScreen;
    public GameObject Explosion;

    public Comunicadores Comunicador;

    public GameObject StartObject;

    public scr_TouchCtr LH;
    public scr_TouchCtr RH;

    string text_days;
    string text_hours;
    string text_signal;

    private void Awake()
    {
        GM = this;
    }

    // Use this for initialization
    public void StartGame () {

        RH.Selector.enabled = false;

        StartObject.SetActive(true);
        CV_LH.SetActive(true);
        CV_RH.SetActive(true);

        StartCoroutine(CTime());

        text_days = scr_Lang.GetText("txt_game_info_01");
        text_hours = scr_Lang.GetText("txt_game_info_02");
        text_signal = scr_Lang.GetText("txt_game_info_03");

        UpdateGeneralStats();
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateUIStats()
    {
        Txt_st_air.text = "A %" + ((int)Astronaut.St_Air).ToString();
        Txt_st_food.text = "C %" + ((int)Astronaut.St_Food).ToString();
        Txt_st_health.text = "V %" + ((int)Astronaut.St_Health).ToString();
        Txt_st_Happiness.text = "F %" + ((int)Astronaut.St_Happiness).ToString();
    }

    public void UpdateGeneralStats()
    {
        Txt_Days.text = text_days+":"+TimePast.Days.ToString();
        Txt_Time.text = text_hours + ":" + TimePast.Hours.ToString();
        Txt_ProvWin.text = text_signal + ": %" + ProbWin.ToString();
    }

    public void Add_ProbWin(float _plus)
    {
        ProbWin += _plus;
        if (ProbWin > 100f) { ProbWin = 100f; }
        UpdateGeneralStats();
    }

    public void GoGameOver()
    {
        CV_LH.SetActive(false);
        CV_RH.SetActive(false);
        scr_Menu.InGame = false;
        GameOver = true;
        GameOverScreen.SetActive(true);
    }

    void CheckWin()
    {
        if (Random.Range(5, 101)<=ProbWin)
        {
            GameOver = true;
            SuccesScreen.SetActive(true);
        }
    }

    IEnumerator CTime()
    {
        while(!GameOver)
        {
            //Debug.Log("TikTime");
            yield return Delay1s;
            TimePast = TimePast.Add(TperSec);
            UpdateGeneralStats();
            if (CurrentDay!=TimePast.Days)
            {
                CurrentDay = TimePast.Days;
                CheckWin();
            }
            TicsAir--;
            TicsHappiness--;
            TicsFood--;
            if (TicsAir<=0)
            {
                TicsAir = 2;
                Astronaut.Rest_Air(1);
            }
            if (TicsHappiness <= 0)
            {
                TicsHappiness = 2;
                Astronaut.Rest_Happiness(1);
            }
            if (TicsFood <= 0)
            {
                TicsFood = 2;
                Astronaut.Rest_Food(1);
            }
        }
    }
}

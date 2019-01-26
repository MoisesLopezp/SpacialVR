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
    public scr_RandomGen RandomSpawn;

    public Text Txt_Days;
    public Text Txt_Time;
    public Text Txt_ProvWin;

    int TicsAir = 2;
    int TicsHappiness = 3;
    int TicsFood = 4;

    GameObject GameOverScreen;
    GameObject SuccesScreen;

    private void Awake()
    {
        GM = this;
    }

    // Use this for initialization
    void Start () {
        Txt_Days.text = TimePast.Days.ToString();
        Txt_Time.text = TimePast.Hours.ToString();
        Txt_ProvWin.text = ProbWin.ToString();
        StartCoroutine(CTime());
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void Add_ProbWin(float _plus)
    {
        ProbWin += _plus;
        if (ProbWin > 100f) { ProbWin = 100f; }
        Txt_ProvWin.text = ProbWin.ToString();
    }

    public void GoGameOver()
    {
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
            Txt_Days.text = TimePast.Days.ToString();
            Txt_Time.text = TimePast.Hours.ToString();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class scr_PlayerStats : MonoBehaviour {

    public CapsuleCollider MyCollider;
    public AudioClip[] audioClips;

    public float St_Air = 100f;
    public float St_Happiness = 100f;
    public float St_Food = 100f;
    public float St_Health = 100f;

    private AudioSource audioSource;

    public bool IsDeath = false;

    public void Add_Air(float _plus)
    {
        St_Air += _plus;
        if (St_Air > 100f) { St_Air = 100f; }
        scr_Mng.GM.UpdateUIStats();
    }

    public void Rest_Air(float _val)
    {
        St_Air -= _val;
        if (St_Air <= 0f)
        {
            St_Air = 0f;
            Die();
        }
        scr_Mng.GM.UpdateUIStats();
    }

    public void Add_Happiness(float _plus)
    {
        St_Happiness += _plus;
        if (St_Happiness > 100f) { St_Happiness = 100f; }
        scr_Mng.GM.UpdateUIStats();
    }

    public void Rest_Happiness(float _val)
    {
        St_Happiness -= _val;
        if (St_Happiness <= 0f)
        {
            St_Happiness = 0f;
            Die();
        }
        scr_Mng.GM.UpdateUIStats();
    }

    public void Add_Food(float _plus)
    {
        St_Food += _plus;
        if (St_Food > 200f) { St_Food = 200f; }
        if (St_Food > 100f) { Add_Mass(); }
        scr_Mng.GM.UpdateUIStats();
    }

    public void Rest_Food(float _val)
    {
        St_Food -= _val;
        if (St_Food <= 0f)
        {
            St_Food = 0f;
            Die();
        }
        scr_Mng.GM.UpdateUIStats();
    }

    public void Add_Dmg(float dmg)
    {
        St_Health -= dmg;
        if (St_Health <= 0f)
        {
            St_Health = 0f;
            Die();
        }
        scr_Mng.GM.UpdateUIStats();
    }

    public void Add_ProbWin(float _plus)
    {
        scr_Mng.GM.Add_ProbWin(_plus);
    }

    public void Add_Mass()
    {

    }

    public void Die()
    {
        if (IsDeath)
            return;
        IsDeath = true;
        scr_Mng.GM.GoGameOver();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(DelayCollider());
        audioSource = GetComponent<AudioSource>();
	}
	
    IEnumerator DelayCollider()
    {
        yield return new WaitForSeconds(1f);
        MyCollider.enabled = true;
    }

	// Update is called once per frame
	void Update () {
		
	}

    void PlayAudio(int audioIndex)
    {
        audioSource.clip = audioClips[audioIndex];
        audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}

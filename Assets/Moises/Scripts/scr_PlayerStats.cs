using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class scr_PlayerStats : MonoBehaviour {

    public CapsuleCollider MyCollider;
    public AudioClip[] audioClips;
    public Animator MyAnimator;
    public AudioSource Respiracion;

    Rigidbody RB;

    public float St_Air = 100f;
    public float St_Happiness = 100f;
    public float St_Food = 100f;
    public float St_Health = 100f;

    private enum AudioClips
    {
        Grunt1,
        Grunt2
    }

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
        if (St_Food > 200f) { St_Food = 200f; /*RB.isKinematic = true; */}
        //if (St_Food > 100f) { Add_Mass(); RB.isKinematic = false; }
        scr_Mng.GM.UpdateUIStats();
    }

    public void Rest_Food(float _val)
    {
        PlayAudio(2);
        St_Food -= _val;
        if (St_Food <= 100)
        {
            transform.localScale = Vector3.one * 0.1f;
        }
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
        //RB.drag += St_Food - 100 * 0.01f;
        transform.localScale = Vector3.one * ((100 - St_Food) * 0.001f);

        /*
        Vector3 ts = transform.localScale;
        if (ts.x>2f)
        {
            return;
        }

        RB.drag += 0.2f;
        transform.localScale = new Vector3(ts.x + 0.1f, ts.x + 0.1f, ts.x + 0.1f);
        */
    }

    public void Die()
    {
        if (IsDeath)
            return;

        Respiracion.Stop();
        MyAnimator.speed = 0;
        IsDeath = true;
        scr_Mng.GM.GoGameOver();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(DelayCollider());
        audioSource = GetComponent<AudioSource>();
        //RB = GetComponent<Rigidbody>();

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
        if (collision.gameObject.CompareTag("Dmg"))
        {
            Add_Dmg(10f * Time.deltaTime);
            PlayAudio(Random.Range(0, 2));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Add_Dmg(1f * Time.deltaTime);
        }
    }
}

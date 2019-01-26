using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comunicadores : MonoBehaviour {

    public AudioClip[] audio_Comunicadores = new AudioClip[3];
    AudioSource source_Player;

    int[] milistilla;
    int contador = 0, iterador_lista_audio = 0;
    bool match = false;
    public int num_clips = 3;

    void Start()
    {
        Random_List();
    }

    public void Play_Audio()
    {
        source_Player.clip = audio_Comunicadores[milistilla[iterador_lista_audio]];
        source_Player.Play();
        iterador_lista_audio++;

        if(iterador_lista_audio < num_clips)
        {
            iterador_lista_audio = 0;
            Random_List();
        }

    }

    void Random_List()
    {
        milistilla = new int[num_clips];

        for (int i = 0; i < num_clips; i++)
        {
            int ite = Random.Range(1, num_clips);

            if (milistilla[ite] == 0)
            {
                milistilla[ite] = i;
            }
            else
            {
                while (!match)
                {
                    ite = Random.Range(0, num_clips);
                    if (milistilla[ite] == 0) { match = true; }
                }
                milistilla[ite] = i;
            }
            match = false;

        }

        for (int i = 0; i < num_clips; i++)
        {
            print(milistilla[i]);
        }
    }
}

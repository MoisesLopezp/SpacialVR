using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comunicadores : MonoBehaviour {

    int[] milistilla;
    int contador=0;
    bool match = false;


    void Start()
    {

        milistilla = new int[3];

        for (int i = 0; i < 3; i++)
        {
            int ite = Random.Range(1, 3);

            if (milistilla[ite] == 0)
            {
                milistilla[ite] = i;
            }
            else
            {
                while (!match)
                {
                    ite = Random.Range(0, 3);
                    if (milistilla[ite] == 0) { match = true; }
                }
                milistilla[ite] = i;
            }
            match = false;

        }

        for (int i = 0; i < 3; i++)
        {
            print(milistilla[i]);
        }

    }
}

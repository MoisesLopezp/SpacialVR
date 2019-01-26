//Codiguito creado por Ronysaurus
using System.Collections;
using UnityEngine;

public class SCR_HazardSpawner : MonoBehaviour
{
    //Rny: Distancia a la que se cran los objetos
    public float SpawnDistance;
    //Rny: arreglo con los objetos que dañan al jugador
    public GameObject[] hazards;

    private void Start()
    {
        //Rny: Si no se defina la distancia de Spawneo se pone en 30
        if (SpawnDistance == 0)
            SpawnDistance = 30;
    }

    //Rny: Funcion que spawnea los objetos en el espacio
    public void SpawnObject(SCR_SpaceManager.HAZARDS _hazardIndex, float _deathTime)
    {
        //Rny: Se setea la posicion random en la cual se spawnea el objeto asegurandonos que siempre esten a la misma distancia
        float posZ = 30 - Random.Range(0.0f, SpawnDistance);
        float posX = 30 - Random.Range(-SpawnDistance + Mathf.Abs(posZ), SpawnDistance - Mathf.Abs(posZ));
        float posY = 30 - Random.Range(-SpawnDistance + Mathf.Abs(posX) + Mathf.Abs(posZ), SpawnDistance - Mathf.Abs(posX) - Mathf.Abs(posZ));

        //Rny: Se spawnea el objeto en la posicion asignada
        GameObject myHazard = Instantiate(hazards[(int)_hazardIndex], new Vector3(posX, posY, posZ), Quaternion.identity);
        StartCoroutine(ObjectKiller(myHazard, _deathTime));
        Debug.Log("Spawning " + myHazard.name + " at " + new Vector3(posX, posY, posZ));
    }

    //Rny: funcion para destruir los objetos
    private IEnumerator ObjectKiller(GameObject objectToKill, float _deathTime)
    {
        //Rny: espera por la hora de su muerte x.x
        yield return new WaitForSeconds(_deathTime);
        //Rny: Mata el objeto RIP in peace
        Debug.Log("Hello I'm going to kill " + objectToKill.name);
        DestroyObject(objectToKill);
    }
}
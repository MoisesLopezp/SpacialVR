//Codiguito creado por Ronysaurus
using System.Collections;
using UnityEngine;

public class SCR_HazardSpawner : MonoBehaviour
{
    //Rny: Distancia a la que se cran los objetos
    public float SpawnDistance;
    public float AsteroidDistance;
    //Rny: arreglo con los objetos que dañan al jugador
    public GameObject[] hazards;
    //Rny: El objeto padre de todos los objetos que se creen
    public Transform hazardsParent;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //Rny: Si no se defina la distancia de Spawneo se pone en 30
        if (SpawnDistance == 0)
            SpawnDistance = 2.0f;
        if (AsteroidDistance == 0)
            AsteroidDistance = 30.0f;
    }

    //Rny: Funcion que spawnea los objetos en el espacio
    public void SpawnObject(SCR_SpaceManager.HAZARDS _hazardIndex, float _deathTime)
    {
        float ObjectMaxDistance = _hazardIndex == SCR_SpaceManager.HAZARDS.Asteroides 
                                  ? AsteroidDistance 
                                  : SpawnDistance;

        //Rny: Se setea la posicion random en la cual se spawnea el objeto asegurandonos que siempre esten a la misma distancia
        float posX = Random.Range(-ObjectMaxDistance, ObjectMaxDistance);
        float posY = Random.Range(-ObjectMaxDistance + Mathf.Abs(posX), ObjectMaxDistance - Mathf.Abs(posX));
        float posZ = (ObjectMaxDistance - (Mathf.Abs(posX) + Mathf.Abs(posY)));

        //Rny: Se spawnea el objeto en la posicion asignada
        GameObject myHazard = Instantiate(hazards[(int)_hazardIndex], playerTransform.position +  new Vector3(posX, posY, posZ), Quaternion.identity);
        myHazard.transform.parent = hazardsParent;
        StartCoroutine(ObjectKiller(myHazard, _deathTime));
        //Debug.Log("Spawning " + myHazard.name + " at " + new Vector3(posX, posY, posZ));
    }

    //Rny: funcion para destruir los objetos
    private IEnumerator ObjectKiller(GameObject objectToKill, float _deathTime)
    {
        //Rny: espera por la hora de su muerte x.x
        yield return new WaitForSeconds(_deathTime);
        if (objectToKill)
        {
            //Rny: Mata el objeto RIP in peace
            //Debug.Log("Hello I'm going to kill " + objectToKill.name);
            DestroyObject(objectToKill);
        }
    }
}
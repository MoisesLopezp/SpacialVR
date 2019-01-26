//Codiguito creado por Ronysaurus
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SCR_HazardSpawner))]
public class SCR_SpaceManager : MonoBehaviour
{
    public enum HAZARDS
    {
        Asteroides,
        Escombros,
        Radiacion,
        Explosivo,
        HoyoNegro
    }

    private SCR_HazardSpawner hzrd_Spawn;

    private void Start()
    {
        //Rny: Se asigna el componente de HazardSpawner
        hzrd_Spawn = GetComponent<SCR_HazardSpawner>();

        //Rny: Se inician las funciones recursivas de spawneo de hazards
        StartCoroutine(SpawnAsteroid());
        StartCoroutine(SpawnEscombro());
        StartCoroutine(SpawnRadiacion());
        StartCoroutine(SpawnHoyoNegro());
        //Rny: Esta solo se corre una vez al inicio
        SpawnExplosivo();
    }

    private IEnumerator SpawnAsteroid()
    {
        float coolDown = Random.Range(4.0f, 8.0f);
        yield return new WaitForSeconds(coolDown);

        hzrd_Spawn.SpawnObject(HAZARDS.Asteroides, 15.0f);
        StartCoroutine(SpawnAsteroid());
    }

    private IEnumerator SpawnEscombro()
    {
        float coolDown = Random.Range(4.0f, 8.0f);
        yield return new WaitForSeconds(coolDown);

        hzrd_Spawn.SpawnObject(HAZARDS.Escombros, 15.0f);
        SpawnEscombro();
        StartCoroutine(SpawnEscombro());
    }

    private IEnumerator SpawnRadiacion()
    {
        float coolDown = Random.Range(20.0f, 30.0f);
        yield return new WaitForSeconds(coolDown);

        hzrd_Spawn.SpawnObject(HAZARDS.Radiacion, Random.Range(10.0f, 15.0f));
        SpawnRadiacion();
        StartCoroutine(SpawnRadiacion());
    }

    public void SpawnExplosivo()
    {
        for (int i = 0; i < 6; i++)
            hzrd_Spawn.SpawnObject(HAZARDS.Explosivo, Mathf.Infinity);
    }

    private IEnumerator SpawnHoyoNegro()
    {
        float coolDown = Random.Range(30.0f, 60.0f);
        yield return new WaitForSeconds(coolDown);

        hzrd_Spawn.SpawnObject(HAZARDS.HoyoNegro, Random.Range(20.0f, 30.0f));
        SpawnHoyoNegro();
        StartCoroutine(SpawnHoyoNegro());
    }
}
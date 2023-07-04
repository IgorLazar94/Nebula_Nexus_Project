using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : GenericBuild, IProduce
{
    [SerializeField] private TypeOfProduct typeOfProduct;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnerProduceTimer;
    private GameObject productPrefab;

    private void Start()
    {
        productPrefab = ProductManager.Instance.ChooseProductPrefab(typeOfProduct);
        StartCoroutine(SpawnIron());
    }

    public void ProduceProduct()
    {

    }

    private IEnumerator SpawnIron()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnerProduceTimer);

        }
    }
}

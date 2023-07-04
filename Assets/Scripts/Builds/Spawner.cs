using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : GenericBuild, IProduce
{
    [SerializeField] private TypeOfProduct typeOfProduct;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnerProduceTimer;
    private GameObject productPrefab;
    private int countOfReadyProduct = 0;

    private float offset = 0.6f;
    private int rowCount = 5;
    private int columnCount = 5;

    private void Start()
    {
        productPrefab = ProductManager.Instance.ChooseProductPrefab(typeOfProduct);
        CalculateGridSize(productPrefab.transform);
        StartCoroutine(SpawnIron());
    }

    public void ProduceProduct(Vector3 productPos)
    {
        Instantiate(productPrefab, productPos, productPrefab.transform.rotation);
        countOfReadyProduct++;
    }

    private IEnumerator SpawnIron()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnerProduceTimer);
            ChoosePositionForInst();
        }
    }

    private void ChoosePositionForInst()
    {
        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(row * offset, 0, column * offset);

                ProduceProduct(spawnPosition);
            }
        }
    }

    private void CalculateGridSize(Transform transform)
    {

    }
}

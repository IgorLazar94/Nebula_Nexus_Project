using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : GenericBuild, IProduce
{
    [SerializeField] private TypeOfProduct typeOfProduct;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnerProduceTimer;
    [SerializeField] ProductManager productManager;

    private GameObject productPrefab;
    private int countOfReadyProduct = 0;

    private float offsetX;
    private float offsetY;
    private float offsetZ;

    private int widthLimit = 4;
    private int lengthLimit = 2;

    private float length = 0;
    private float height = 0;
    private float width = 0;

    private void Start()
    {
        productPrefab = productManager.ChooseProductPrefab(typeOfProduct);
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
        Vector3 spawnPosition = spawnPoint.position + new Vector3(length * offsetX, width * offsetY, -(height * offsetZ));

        ProduceProduct(spawnPosition);
        CalculateNewPosition();
    }

    private void CalculateNewPosition()
    {
        length++;
        if (length > widthLimit)
        {
            length = 0;
            height++;
            if (height > lengthLimit)
            {
                length = 0;
                height = 0;
                width++;
            }
        }
    }

    private void CalculateGridSize(Transform transform)
    {
        var boxCollider = transform.gameObject.GetComponent<BoxCollider>();

        offsetX = boxCollider.size.x * transform.localScale.x;
        offsetZ = boxCollider.size.y * transform.localScale.y;
        offsetY = boxCollider.size.z * transform.localScale.z;
    }
}

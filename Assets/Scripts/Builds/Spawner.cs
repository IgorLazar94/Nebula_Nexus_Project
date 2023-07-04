using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : GenericBuild, IProduce
{
    [SerializeField] private TypeOfProduct typeOfProduct;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnerProduceTimer;

    private GameObject productPrefab;
    private List<Product> readyProducts = new List<Product>();

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
        productPrefab = ProductManager.Instance.ChooseProductPrefab(typeOfProduct);
        CalculateGridSize(productPrefab.transform);
        StartCoroutine(SpawnIron());
    }

    public void ProduceProduct(Vector3 productPos)
    {
        GameObject productObject = Instantiate(productPrefab, productPos, productPrefab.transform.rotation);
        Product product = productObject.gameObject.GetComponent<Product>();
        readyProducts.Add(product);
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

    private void ResetReadyProducts()
    {
        width = 0;
        length = 0;
        height = 0;
        for (int i = 0; i < readyProducts.Count; i++)
        {
            Destroy(readyProducts[i].gameObject);
        }
        readyProducts.Clear();
    }

    public int TransmitProduct()
    {
        int allProductsCount = readyProducts.Count;
        ResetReadyProducts();
        return allProductsCount;
    }
}

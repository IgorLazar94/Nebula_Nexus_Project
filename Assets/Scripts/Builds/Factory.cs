using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Factory : GenericBuild, IProduce, IReceive
{
    private TypeOfProduct typeOfProductReceive;
    private TypeOfProduct typeOfProductProduce;

    private GameObject producePrefab;
    private GameObject receivePrefab;

    private List<Product> ironsList = new List<Product>();
    private List<Product> swordsList = new List<Product>();



    //[SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject receivePoint;


    private int ironOnFactory = 0;
    private int swordsOnFactory = 0;

    // Iron
    private float offsetXIron;
    private float offsetYIron;
    private float offsetZIron;

    private int widthLimitIron = 4;
    private int lengthLimitIron = 2;

    private float lengthIron = 0;
    private float heightIron = 0;
    private float widthIron = 0;


    private void Start()
    {
        receivePrefab = ProductManager.Instance.ChooseProductPrefab(typeOfProductReceive);
        producePrefab = ProductManager.Instance.ChooseProductPrefab(typeOfProductProduce);

    }

    private void AddReceiveProduct(Vector3 _spawnPos)
    {
        Debug.Log(_spawnPos + " spawn pos for iron");
        GameObject productObject = Instantiate(receivePrefab, _spawnPos, receivePrefab.transform.rotation);
        Product product = productObject.gameObject.GetComponent<Product>();
        ironsList.Add(product);
    }

    private void CalculateNewPosition()
    {
        lengthIron++;
        if (lengthIron > widthLimitIron)
        {
            lengthIron = 0;
            heightIron++;
            if (heightIron > lengthLimitIron)
            {
                lengthIron = 0;
                heightIron = 0;
                widthIron++;
            }
        }
    }

    public void ProduceProduct(Vector3 setProductPos)
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveProduct(int productAmount)
    {
        SetIronToFactory(productAmount);

        for (int i = 0; i < productAmount; i++)
        {
            PlaceNewIronAtReceivePos();
        }
    }

    private void SetIronToFactory(int ironAmount)
    {
        ironOnFactory += ironAmount;
    }

    private void PlaceNewIronAtReceivePos()
    {
        CalculateNewPosition();
        Vector3 spawnPosition = receivePoint.transform.position + new Vector3(lengthIron * offsetXIron, widthIron * offsetYIron, -(heightIron * offsetZIron));
        AddReceiveProduct(spawnPosition);
        // iron in sword coroutine
    }


    //private void Update()
    //{
    //    Debug.Log(ironOnFactory + " ironOnFactory");
    //}
}

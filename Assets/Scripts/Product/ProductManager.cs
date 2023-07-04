using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public static ProductManager Instance { get; private set; }

    [SerializeField] private GameObject IronPrefab;
    [SerializeField] private GameObject SwordPrefab;

    private void Start()
    {
        MakeSingleton();
    }

    public GameObject ChooseProductPrefab(TypeOfProduct typeOfProduct)
    {
        switch (typeOfProduct)
        {
            case TypeOfProduct.Iron:
                return IronPrefab;
            case TypeOfProduct.Sword:
                return SwordPrefab;
            default:
                Debug.LogError("Requested undefined type of product");
                return null;
        }
    }

    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


}

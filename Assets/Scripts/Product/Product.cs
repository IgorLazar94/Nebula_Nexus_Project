using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfProduct
{
    Iron,
    Sword,
}
public class Product : MonoBehaviour
{
    [SerializeField] private TypeOfProduct typeOfProduct;
}

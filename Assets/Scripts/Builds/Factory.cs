using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : GenericBuild, IProduce, IReceive
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject receivePoint;
    private int ironOnFactory = 0;
    private int swordsOnFactory = 0;

    public void ProduceProduct(Vector3 setProductPos)
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveProduct(int productAmount)
    {
        SetIronToFactory(productAmount);
        SetPositionToIron();
    }

    private void SetIronToFactory(int ironAmount)
    {
        ironOnFactory += ironAmount;
    }

    private void SetPositionToIron()
    {

    }


    private void Update()
    {
        Debug.Log(ironOnFactory + " ironOnFactory");
    }
}

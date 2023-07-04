using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : GenericBuild, IProduce, IReceive
{
    public void ProduceProduct(Vector3 productPos)
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveProduct()
    {
        throw new System.NotImplementedException();
    }
}

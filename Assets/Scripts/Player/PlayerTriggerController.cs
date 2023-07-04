using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerTriggerController : MonoBehaviour
{
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = gameObject.GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playerInventory.isBusyInventory)
        {
            // all
            if (other.transform.parent.gameObject.TryGetComponent(out Spawner spawner))
            {
                GetProductFromSpawner(spawner);
            }
        }
        else
        {
            if (playerInventory.playerCargoType == TypeOfProduct.Iron)
            {
                if (other.transform.parent.gameObject.TryGetComponent(out Spawner spawner))
                {
                    GetProductFromSpawner(spawner);
                }
            }

            if (playerInventory.playerCargoType == TypeOfProduct.Sword)
            {
                
            }

        }










    }





    private void GetProductFromSpawner(Spawner _spawner)
    {
        var iron = _spawner.TransmitProduct();
        playerInventory.SetPlayerIronSlot(iron);
    }

    private void GetProductFromFactory()
    {

    }
}

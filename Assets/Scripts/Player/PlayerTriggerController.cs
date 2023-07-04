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
            InteractionWithSpawner(other);
            InteractionWithFactory(other);
        }
        else
        {
            if (playerInventory.playerCargoType == TypeOfProduct.Iron)
            {
                InteractionWithSpawner(other);
                InteractionWithFactory(other);
            }

            if (playerInventory.playerCargoType == TypeOfProduct.Sword)
            {
                InteractionWithFactory(other);
            }

        }










    }

    private void InteractionWithSpawner(Collider collider)
    {
        if (collider.transform.parent.gameObject.TryGetComponent(out Spawner spawner) && collider.CompareTag(TagList.SpawnPoint))
        {
            GetProductFromSpawner(spawner);
        }
    }

    private void InteractionWithFactory(Collider collider)
    {
        if (collider.transform.parent.gameObject.TryGetComponent(out Factory factory))
        {
            if (collider.CompareTag(TagList.SpawnPoint) && playerInventory.playerCargoType == TypeOfProduct.Iron)
            {
                int tempIron = playerInventory.RemovePlayerIronSlot();
                factory.ReceiveProduct(tempIron);
            }
            if (collider.CompareTag(TagList.ReceivePoint))
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

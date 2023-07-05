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
            InteractionWithFactoryEmptyInventory(other);
        }
        else
        {
            if (playerInventory.playerCargoType == TypeOfProduct.Iron)
            {
                InteractionWithSpawner(other);
            }
                InteractionWithFactory(other);
        }










    }

    private void InteractionWithSpawner(Collider collider)
    {
        if (collider.transform.parent.gameObject.TryGetComponent(out Spawner spawner) && collider.CompareTag(TagList.SpawnPoint))
        {
            GetProductFromSpawner(spawner);
        }
    }

    private void InteractionWithFactoryEmptyInventory(Collider collider)
    {
        if (collider.transform.parent.gameObject.TryGetComponent(out Factory factory))
        {
            if (collider.CompareTag(TagList.ReceivePoint))
            {
                int tempIron = playerInventory.RemovePlayerIronSlot();
                factory.ReceiveProduct(tempIron);
            }
            if (collider.CompareTag(TagList.SpawnPoint))
            {
                GetProductFromFactory(factory);
            }
        }
    }

    private void InteractionWithFactory(Collider collider)
    {
        if (collider.transform.parent.gameObject.TryGetComponent(out Factory factory))
        {
            if (collider.CompareTag(TagList.ReceivePoint) && playerInventory.playerCargoType == TypeOfProduct.Iron)
            {
                int tempIron = playerInventory.RemovePlayerIronSlot();
                factory.ReceiveProduct(tempIron);
            }
            if (collider.CompareTag(TagList.SpawnPoint) && playerInventory.playerCargoType == TypeOfProduct.Sword)
            {
                GetProductFromFactory(factory);
            }
        }
    }

    private void GetProductFromSpawner(Spawner _spawner)
    {
        var ironsProduct = _spawner.TransmitProduct();
        playerInventory.SetPlayerIronSlot(ironsProduct);
    }

    private void GetProductFromFactory(Factory _factory)
    {
        var swordsProduct = _factory.TransmitSwords();
        playerInventory.SetPlayerSwordSlot(swordsProduct);
    }

}

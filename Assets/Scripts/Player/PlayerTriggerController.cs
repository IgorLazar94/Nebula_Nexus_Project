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
            if (playerInventory.playerCargoType == TypeOfProduct.Sword)
            {
                InteractionWithStockpile(other);
            }
                InteractionWithFactory(other);
        }

        if (other.transform.parent.gameObject.TryGetComponent(out Stockpile stockpile))
        {
            InteractionWithStockpileCanvas(stockpile, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject.TryGetComponent(out Stockpile stockpile))
        {
            InteractionWithStockpileCanvas(stockpile, false);
        }

        DisconnectSpawner(other);
    }

    private void DisconnectSpawner(Collider collider)
    {
        if (collider.transform.parent.gameObject.TryGetComponent(out Spawner spawner) && collider.CompareTag(TagList.SpawnPoint) && spawner.IsConnectWithPlayer)
        {
            spawner.IsConnectWithPlayer = false;
        }
    }

    private void InteractionWithSpawner(Collider collider)
    {
        if (collider.transform.parent.gameObject.TryGetComponent(out Spawner spawner) && collider.CompareTag(TagList.SpawnPoint) && !spawner.IsConnectWithPlayer)
        {
            spawner.IsConnectWithPlayer = true;
            //spawner.UpdatePlayerPos(transform.position);
            GetProductFromSpawner(spawner);
        }
    }

    private void InteractionWithStockpile(Collider collider)
    {
        if (collider.transform.parent.gameObject.TryGetComponent(out Stockpile stockpile) && collider.CompareTag(TagList.ReceivePoint))
        {
           SetProductToStockpile(stockpile);
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
    private void SetProductToStockpile(Stockpile stockpile)
    {
        var swordsProduct = playerInventory.RemovePlayerSwordSlot();
        stockpile.AddSwordsToStockpile(swordsProduct);
    }

    private void InteractionWithStockpileCanvas(Stockpile stockpile, bool isActivate)
    {
        stockpile.ActivateCanvas(isActivate);
    }


}

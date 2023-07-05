using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
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

            if (other.transform.parent.gameObject.TryGetComponent(out Builds.Stockpile stockpile))
            {
                InteractionWithStockpileCanvas(stockpile, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent.gameObject.TryGetComponent(out Builds.Stockpile stockpile))
            {
                InteractionWithStockpileCanvas(stockpile, false);
            }

            DisconnectSpawner(other);
            DisconnectFactory(other);
        }

        private void DisconnectSpawner(Collider collider)
        {
            if (collider.transform.parent.gameObject.TryGetComponent(out Builds.Spawner spawner) && collider.CompareTag(TagList.SpawnPoint) && spawner.IsConnectWithPlayer)
            {
                spawner.IsConnectWithPlayer = false;
            }
        }

        private void DisconnectFactory(Collider collider)
        {
            if (collider.transform.parent.gameObject.TryGetComponent(out Builds.Factory factory) && collider.CompareTag(TagList.SpawnPoint) && factory.IsConnectWithPlayer)
            {
                factory.IsConnectWithPlayer = false;
            }
        }

        private void InteractionWithSpawner(Collider collider)
        {
            if (collider.transform.parent.gameObject.TryGetComponent(out Builds.Spawner spawner) && collider.CompareTag(TagList.SpawnPoint) && !spawner.IsConnectWithPlayer)
            {
                spawner.IsConnectWithPlayer = true;
                GetProductFromSpawner(spawner);
            }
        }

        private void InteractionWithStockpile(Collider collider)
        {
            if (collider.transform.parent.gameObject.TryGetComponent(out Builds.Stockpile stockpile) && collider.CompareTag(TagList.ReceivePoint))
            {
                SetProductToStockpile(stockpile);
            }
        }

        private void InteractionWithFactoryEmptyInventory(Collider collider)
        {
            if (collider.transform.parent.gameObject.TryGetComponent(out Builds.Factory factory))
            {
                if (collider.CompareTag(TagList.ReceivePoint))
                {
                    int tempIron = playerInventory.RemovePlayerIronSlot();
                    factory.ReceiveProduct(tempIron);
                }
                if (collider.CompareTag(TagList.SpawnPoint) && !factory.IsConnectWithPlayer)
                {
                    factory.IsConnectWithPlayer = true;
                    GetProductFromFactory(factory);
                }
            }
        }

        private void InteractionWithFactory(Collider collider)
        {
            if (collider.transform.parent.gameObject.TryGetComponent(out Builds.Factory factory))
            {
                if (collider.CompareTag(TagList.ReceivePoint) && playerInventory.playerCargoType == TypeOfProduct.Iron)
                {
                    int tempIron = playerInventory.RemovePlayerIronSlot();
                    factory.ReceiveProduct(tempIron);
                }
                if (collider.CompareTag(TagList.SpawnPoint) && playerInventory.playerCargoType == TypeOfProduct.Sword && !factory.IsConnectWithPlayer)
                {
                    factory.IsConnectWithPlayer = true;
                    GetProductFromFactory(factory);
                }
            }
        }

        private void GetProductFromSpawner(Builds.Spawner _spawner)
        {
            var ironsProduct = _spawner.TransmitProduct();
            playerInventory.SetPlayerIronSlot(ironsProduct);
        }

        private void GetProductFromFactory(Builds.Factory _factory)
        {
            var swordsProduct = _factory.TransmitSwords();
            if (swordsProduct <= 0)
            {
                return;
            }
            playerInventory.SetPlayerSwordSlot(swordsProduct);
        }

        private void SetProductToStockpile(Builds.Stockpile stockpile)
        {
            var swordsProduct = playerInventory.RemovePlayerSwordSlot();
            stockpile.ReceiveProduct(swordsProduct);
        }

        private void InteractionWithStockpileCanvas(Builds.Stockpile stockpile, bool isActivate)
        {
            stockpile.ActivateCanvas(isActivate);
        }
    }
}


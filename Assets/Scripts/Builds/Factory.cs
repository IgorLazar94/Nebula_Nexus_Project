using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Builds
{
    public class Factory : MonoBehaviour, IProduce, IReceive
    {
        public bool IsConnectWithPlayer { get; set; }

        [SerializeField] private TypeOfProduct typeOfProductReceive;
        [SerializeField] private TypeOfProduct typeOfProductProduce;
        [SerializeField] private Transform playerPos;
        [SerializeField] private GameObject particlesContainer;
        [SerializeField] private GameObject spawnPoint;
        [SerializeField] private GameObject receivePoint;
        private GameObject producePrefab;
        private GameObject receivePrefab;
        private List<Product> ironsList = new List<Product>();
        private List<Product> swordsList = new List<Product>();
        private List<ParticleSystem> listOfParticles = new List<ParticleSystem>();
        private bool isReadyToWork = false;
        private bool isCoroutineEnabled = false;
        private float timeToWorkFabrica;
        // Iron container config
        private float offsetXIron;
        private float offsetYIron;
        private float offsetZIron;
        private int widthLimitIron = 4;
        private int lengthLimitIron = 2;
        private float lengthIron = 0;
        private float heightIron = 0;
        private float widthIron = 0;
        // Sword container config
        private float offsetXSword;
        private float offsetYSword;
        private float offsetZSword;
        private int widthLimitSword = 5;
        private int lengthLimitSword = 1;
        private float lengthSword = 0;
        private float heightSword = 0;
        private float widthSword = 0;
        private float DOTweenTimer;
        private float DOTweenTimerDefault = 0.03f;

        private void Start()
        {
            receivePrefab = ProductManager.Instance.ChooseProductPrefab(typeOfProductReceive);
            producePrefab = ProductManager.Instance.ChooseProductPrefab(typeOfProductProduce);
            timeToWorkFabrica = GameSettings.Instance.GetFabricaSpeed();
            CalculateReceiveProductSize(receivePrefab.transform);
            CalculateProduceProductSize(producePrefab.transform);
            DOTweenTimer = DOTweenTimerDefault;
            FillParticlesList();
        }

        private void FillParticlesList()
        {
            ParticleSystem[] particlesArray = particlesContainer.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < particlesArray.Length; i++)
            {
                listOfParticles.Add(particlesArray[i]);
            }
        }

        private void EnableParticles()
        {
            foreach (var fx in listOfParticles)
            {
                fx.Play();
            }
        }

        private void DisableParticles()
        {
            foreach (var fx in listOfParticles)
            {
                fx.Stop();
            }
        }

        private void CalculateReceiveProductSize(Transform transform)
        {
            var boxCollider = transform.gameObject.GetComponent<BoxCollider>();

            offsetXIron = boxCollider.size.x * transform.localScale.x;
            offsetZIron = boxCollider.size.y * transform.localScale.y;
            offsetYIron = boxCollider.size.z * transform.localScale.z;
        }

        private void CalculateProduceProductSize(Transform transform)
        {
            var boxCollider = transform.gameObject.GetComponent<BoxCollider>();

            offsetXSword = boxCollider.size.x * transform.localScale.x;
            offsetZSword = boxCollider.size.y * transform.localScale.y;
            offsetYSword = boxCollider.size.z * transform.localScale.z;
        }

        private void AddReceiveProduct(Vector3 _spawnPos)
        {
            GameObject receiveObject = Instantiate(receivePrefab, playerPos.transform.position, receivePrefab.transform.rotation);
            receiveObject.transform.DOJump(_spawnPos, 3f, 1, DOTweenTimer);
            Product product = receiveObject.gameObject.GetComponent<Product>();
            ironsList.Add(product);
        }

        private void CalculateNewIronPosition()
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

        private void CalculateNewSwordPosition()
        {
            lengthSword++;
            if (lengthSword > widthLimitSword)
            {
                lengthSword = 0;
                heightSword++;
                if (heightSword > lengthLimitSword)
                {
                    lengthSword = 0;
                    heightSword = 0;
                    widthSword++;
                }
            }
        }

        public void ProduceProduct(Vector3 newPoint)
        {
            GameObject produceObject = Instantiate(producePrefab, newPoint, producePrefab.transform.rotation);
            produceObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            produceObject.transform.DOMove(spawnPoint.transform.position, timeToWorkFabrica / 2)
                                    .SetEase(Ease.Linear)
                                    .OnComplete(() => produceObject.transform.DOMove(newPoint, 0.1f));
            Product product = produceObject.gameObject.GetComponent<Product>();
            swordsList.Add(product);
        }
        public void ReceiveProduct(int productAmount)
        {
            for (int i = 0; i < productAmount; i++)
            {
                DOTweenTimer += 0.02f;
                PlaceNewIronAtReceivePos();
            }
            DOTweenTimer = DOTweenTimerDefault;

            if (isReadyToWork && !isCoroutineEnabled)
            {
                StartCoroutine(FabricaConvertsIron());
            }
        }

        private void PlaceNewIronAtReceivePos()
        {
            CalculateNewIronPosition();
            Vector3 receivePos = receivePoint.transform.position + new Vector3(lengthIron * offsetXIron, widthIron * offsetYIron, -(heightIron * offsetZIron));
            AddReceiveProduct(receivePos);
            isReadyToWork = true;
        }

        private IEnumerator FabricaConvertsIron()
        {
            isCoroutineEnabled = true;
            while (true && isReadyToWork)
            {
                EnableParticles();
                CheckRemainingIron();
                if (isReadyToWork)
                {
                    OffsetIron();
                    yield return new WaitForSeconds(timeToWorkFabrica / 2);
                    AddSword();
                    yield return new WaitForSeconds(timeToWorkFabrica / 2);
                }
            }
        }

        private void OffsetIron()
        {
            var ironProduct = ironsList[ironsList.Count - 1].gameObject;
            ironProduct.transform.position = new Vector3(receivePoint.transform.position.x + 2, receivePoint.transform.position.y + 1, receivePoint.transform.position.z - 1);
            ironProduct.transform.DOMove(transform.position, timeToWorkFabrica / 2)
                                 .SetEase(Ease.Linear)
                                 .OnComplete(() => RemoveIron());
        }

        private void RemoveIron()
        {
            Destroy(ironsList[ironsList.Count - 1].gameObject);
            ironsList.RemoveAt(ironsList.Count - 1);
        }

        private void AddSword()
        {
            CalculateNewSwordPosition();
            Vector3 swordSpawnPos = spawnPoint.transform.position + new Vector3(lengthSword * offsetYSword, widthSword * offsetXSword, -(heightSword * offsetZSword));
            ProduceProduct(swordSpawnPos);
        }

        private void CheckRemainingIron()
        {
            if (ironsList.Count <= 0)
            {
                StopCoroutine(FabricaConvertsIron());
                isReadyToWork = false;
                isCoroutineEnabled = false;
                DisableParticles();
                ironsList.Clear();
                ResetIronsContainer();
            }
        }

        private void ResetIronsContainer()
        {
            lengthIron = 0;
            heightIron = 0;
            widthIron = 0;
        }

        private void ResetSwordsList()
        {
            widthSword = 0;
            lengthSword = 0;
            heightSword = 0;
            for (int i = 0; i < swordsList.Count; i++)
            {
                swordsList[i].transform.DOJump(playerPos.position, 3f, 1, 0.2f);
                Invoke(nameof(ResetList), 0.25f);
            }
        }
        private void ResetList()
        {
            for (int i = 0; i < swordsList.Count; i++)
            {
                Destroy(swordsList[i].gameObject);
            }
            swordsList.Clear();
        }

        public int TransmitSwords()
        {
            int allProductsCount = swordsList.Count;
            ResetSwordsList();
            return allProductsCount;
        }
    }
}


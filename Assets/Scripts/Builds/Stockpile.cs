using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stockpile : GenericBuild, IReceive
{
    [SerializeField] private RectTransform stockpileCanvas;
    [SerializeField] private Transform swordPoolContainer;
    private int swordsOnStockpile = 0;
    private TextMeshProUGUI swordsText;
    private float timeToScaleCanvas = 0.5f;
    private List<Product> productList = new List<Product>();
    private float DOTweenTimer;
    private float DOTweenTimerDefault = 0.1f;

    private void Start()
    {
        FillList();
        DOTweenTimer = DOTweenTimerDefault;
        DiactivateSwords();

        swordsText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateSwordsText();
        stockpileCanvas.DOScale(0f, timeToScaleCanvas);
    }

    public void AddSwordsToStockpile(int newSwords)
    {

    }

    public void ActivateCanvas(bool isActivate)
    {
        stockpileCanvas.gameObject.SetActive(isActivate);

        if (isActivate)
        {
            stockpileCanvas.DOScale(0.2f, timeToScaleCanvas);
        }
        else
        {
            stockpileCanvas.DOScale(0f, timeToScaleCanvas);
        }

    }

    private void UpdateSwordsText()
    {
        swordsText.text = swordsOnStockpile.ToString();
    }

    private void FixedUpdate()
    {
        stockpileCanvas.LookAt(Camera.main.transform);
    }

    public void ReceiveProduct(int productAmount)
    {
        swordsOnStockpile += productAmount;
        // play DOTween swords from player to stockpile
        ActivateSwords();

        UpdateSwordsText();
    }

    private void FillList()
    {
        Product[] productArray = GetComponentsInChildren<Product>();
        for (int i = 0; i < productArray.Length; i++)
        {
            productList.Add(productArray[i]);
        }
    }

    private void ActivateSwords()
    {
        foreach (var sword in productList)
        {
            sword.gameObject.SetActive(true);
            DOTweenTimer += 0.1f;
            sword.transform.DOMove(playerPos.position, 0.0f).OnComplete(() => sword.transform.DOMove(transform.position, 0.5f));
        }
        Invoke(nameof(DiactivateSwords), 0.6f);
        DOTweenTimer = DOTweenTimerDefault;
    }

    private void DiactivateSwords()
    {
        foreach (var sword in productList)
        {
            sword.gameObject.SetActive(false);
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stockpile : GenericBuild
{
    private int swordsOnStockpile = 0;
    [SerializeField] private RectTransform stockpileCanvas;
    private TextMeshProUGUI swordsText;
    private float timeToScaleCanvas = 0.5f;
    private void Start()
    {
        swordsText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateSwordsText();
        stockpileCanvas.DOScale(0f, timeToScaleCanvas);
    }

    public void AddSwordsToStockpile(int newSwords)
    {
        swordsOnStockpile += newSwords;
        UpdateSwordsText();
    }

    public void ActivateCanvas(bool isActivate)
    {
        stockpileCanvas.gameObject.SetActive(isActivate);

        if (isActivate)
        {
            stockpileCanvas.DOScale(0.2f, timeToScaleCanvas);
        } else
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

}

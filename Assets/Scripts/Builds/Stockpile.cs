using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stockpile : GenericBuild
{
    private int swordsOnStockpile = 0;
    [SerializeField] private RectTransform stockpileCanvas;
    private TextMeshProUGUI swordsText;

    private void Start()
    {
        swordsText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateSwordsText();
        stockpileCanvas.gameObject.SetActive(false);
    }

    public void AddSwordsToStockpile(int newSwords)
    {
        swordsOnStockpile += newSwords;
        UpdateSwordsText();
    }

    public void ActivateCanvas(bool value)
    {
        stockpileCanvas.gameObject.SetActive(value);
    }

    private void UpdateSwordsText()
    {
        Debug.LogWarning(swordsText);
        swordsText.text = swordsOnStockpile.ToString();
    }

    private void FixedUpdate()
    {
        stockpileCanvas.LookAt(Camera.main.transform);
    }

}

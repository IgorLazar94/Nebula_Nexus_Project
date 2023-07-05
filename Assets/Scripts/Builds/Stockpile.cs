using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : GenericBuild
{
    private int swordsOnStockpile = 0;

    public void AddSwordsToStockpile(int newSwords)
    {
        swordsOnStockpile += newSwords;
    }

}

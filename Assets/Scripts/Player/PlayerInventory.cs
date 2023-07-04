using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool isBusyInventory {get; private set;}
    public TypeOfProduct playerCargoType {get; private set;}

    private int ironSlotAmount = 0;
    private int swordSlotAmount = 0;

    private void Start()
    {
        isBusyInventory = false;
    }

    public void SetPlayerIronSlot(int value)
    {
        ironSlotAmount = value;
        playerCargoType = TypeOfProduct.Iron;
        isBusyInventory = true;
    }
}

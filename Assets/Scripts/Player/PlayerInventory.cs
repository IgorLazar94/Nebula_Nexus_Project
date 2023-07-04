using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool isBusyInventory { get; private set; }
    public TypeOfProduct playerCargoType { get; private set; }
    [SerializeField] private GameObject viewInventory;
    private List<GameObject> ironsList = new List<GameObject>();
    private List<GameObject> swordsList = new List<GameObject>();
    private int ironSlotAmount = 0;
    private int swordSlotAmount = 0;

    private void Start()
    {
        isBusyInventory = false;
        SetListViewInventory();
    }

    private void SetListViewInventory()
    {
        GameObject ironPool = viewInventory.transform.GetChild(0).gameObject;
        GameObject swordPool = viewInventory.transform.GetChild(1).gameObject;

        Product[] ironsArray = ironPool.GetComponentsInChildren<Product>();
        Product[] swordsArray = swordPool.GetComponentsInChildren<Product>();
        for (int i = 0; i < ironsArray.Length; i++)
        {
            ironsList.Add(ironsArray[i].gameObject);
            ironsArray[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < swordsArray.Length; i++)
        {
            swordsList.Add(swordsArray[i].gameObject);
            swordsArray[i].gameObject.SetActive(false);
        }
    }

    public void SetPlayerIronSlot(int value)
    {
        ironSlotAmount += value;
        playerCargoType = TypeOfProduct.Iron;
        ChangeIronViewInvery(value, true);
        isBusyInventory = true;
    }

    public int RemovePlayerIronSlot()
    {
        int ironsAmount = ironSlotAmount;
        ChangeIronViewInvery(ironsAmount, false);
        ironSlotAmount = 0;
        isBusyInventory = false;
        return ironsAmount;
    }

    private void ChangeIronViewInvery(int amount, bool isAddIron)
    {
        if (isAddIron)
        {
            for (int i = 0; i < amount; i++)
            {
                if (!ironsList[i].activeSelf)
                {
                    ironsList[i].SetActive(true);
                } else
                {
                    amount++;
                }
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                if (ironsList[i].activeSelf)
                {
                    ironsList[i].SetActive(false);
                } else
                {
                    amount++;
                }
            }
        }

    }
}

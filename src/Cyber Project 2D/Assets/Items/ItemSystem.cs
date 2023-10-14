using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    public bool AeliaOpenInventory = false;
    public List<GameObject> uniqueItems = new List<GameObject>();
    public void RespawnAllUniqueItems()
    {
        foreach (GameObject item in uniqueItems)
        {
            item.SetActive(true);
            item.GetComponent<InventoryPickableItem>().ResetQuantity();
        }
    }
    public void RespawnTargetItem(string itemName)
    {
        foreach (GameObject item in uniqueItems)
        {
            if (item.name == itemName)
            {
                item.GetComponent<InventoryPickableItem>().ResetQuantity();
                return;
            }
        }
    }

    public void TryOpenTheInventory(string name)
    {
        if (name == "Aelia")
        {
            AeliaOpenInventory = true;
        }else
        {
            AeliaOpenInventory = false;
        }

        //����NPC�򿪱��������
    }
    public string WhoIsOpeningInventory()
    {
        if (AeliaOpenInventory)
        {
            return "Aelia";
        }
        return "Nobody";
    }
    #region ����ģʽ
    private static ItemSystem instance;
    public static ItemSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ItemSystem>();
            }
            return instance;
        }
    }
    #endregion
}

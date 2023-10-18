using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Mystique : MonoBehaviour
{
    public int favorability = 50;
    public bool isGoodCondition = false;
    public DialogConf conf0;
    public DialogConf confai;

    public void StartDialog()
    {
        if (favorability < 50)
        {
            DialogueManager.Instance.StartDialog(conf0);
        }
        else
            DialogueManager.Instance.StartDialog(confai);
    }

    public void Buff(string itemName)
    {
        if (itemName == "Sedative")
        {
            NarratorSystem.Instance.BroadcastMessage("�������ʹ�����򾲼������������úܺ�");
            isGoodCondition = true;
            return;
        }
        NarratorSystem.Instance.ShowInfo("���β���������ϡ�");
    }

    public void ItemJudge(string itemName)
    {
        if (itemName == "Screw")
        {
            favorability += 10;
            NarratorSystem.Instance.SendDialogueInfo("�����õ�����˿�����øж�������10");
        }
        else
        {
            NarratorSystem.Instance.SendDialogueInfo("���β�����Ҫ�����");
        }
    }
    public void GiveItem()
    {
        foreach(GameObject item in ItemSystem.Instance.uniqueItems)
        {
            if (item.name == "Scanner")
            {
                GameObject.Find("MainInventory").GetComponent<Inventory>().AddItem(item.GetComponent<InventoryItem>(), 1);
                NarratorSystem.Instance.ShowInfo("���θ�����һ��ɨ����");
                return;
            }
        }
    }
    #region ����ģʽ
    private static NPC_Mystique instance;
    public static NPC_Mystique Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Mystique>();
            }
            return instance;
        }
    }
    #endregion
}

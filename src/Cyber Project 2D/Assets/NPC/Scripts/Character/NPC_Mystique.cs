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
            NarratorSystem.Instance.BroadcastMessage("你对迷梦使用了镇静剂，她的心情变得很好");
            isGoodCondition = true;
            return;
        }
        NarratorSystem.Instance.ShowInfo("迷梦并不想喝饮料。");
    }

    public void ItemJudge(string itemName)
    {
        if (itemName == "Screw")
        {
            favorability += 10;
            NarratorSystem.Instance.SendDialogueInfo("迷梦拿到了螺丝钉，好感度增加了10");
        }
        else
        {
            NarratorSystem.Instance.SendDialogueInfo("迷梦并不想要这个。");
        }
    }
    public void GiveItem()
    {
        foreach(GameObject item in ItemSystem.Instance.uniqueItems)
        {
            if (item.name == "Scanner")
            {
                GameObject.Find("MainInventory").GetComponent<Inventory>().AddItem(item.GetComponent<InventoryItem>(), 1);
                NarratorSystem.Instance.ShowInfo("迷梦给了你一个扫描仪");
                return;
            }
        }
    }
    #region 单例模式
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

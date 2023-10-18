using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Evelyn : MonoBehaviour
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
        if (itemName == "Coffee")
        {
            favorability += 10;
            NarratorSystem.Instance.SendDialogueInfo("Evelyn���˿��֣��øж�������10");
            NarratorSystem.Instance.ShowInfo(3);
        }
        else if (itemName == "Sedative")
        {
            NarratorSystem.Instance.BroadcastMessage("��԰�ܽ��ʹ�����򾲼������������úܺ�");
            isGoodCondition = true;
        }
        else
        { 
            favorability -= 10;
            NarratorSystem.Instance.SendDialogueInfo("Evelyn������������úȣ��øжȼ�����10");
        }
    }

    #region ����ģʽ
    private static NPC_Evelyn instance;
    public static NPC_Evelyn Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Evelyn>();
            }
            return instance;
        }
    }
    #endregion
}

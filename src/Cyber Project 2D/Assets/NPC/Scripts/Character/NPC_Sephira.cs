using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Sephira : MonoBehaviour
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
        NarratorSystem.Instance.ShowInfo("�����߲����ʡ�");
    }

    public void ItemJudge(string itemName)
    {
        if (itemName == "Money")
        {
            favorability += 20;
            NarratorSystem.Instance.SendDialogueInfo("�����߱���¸�ˣ��øж�����20");
        }
        else if(itemName=="ProofOfIllicitMoney")
        {
            NarratorSystem.Instance.SendDialogueInfo("�����ߵõ���ҽ��̰�۵�֤�ݣ��øжȴﵽ100");
        }
        else
        {
            NarratorSystem.Instance.SendDialogueInfo("�����߲�����Ҫ���");
        }
    }
    #region ����ģʽ
    private static NPC_Sephira instance;
    public static NPC_Sephira Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Sephira>();
            }
            return instance;
        }
    }
    #endregion
}

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
        NarratorSystem.Instance.ShowInfo("监视者并不渴。");
    }

    public void ItemJudge(string itemName)
    {
        if (itemName == "Money")
        {
            favorability += 20;
            NarratorSystem.Instance.SendDialogueInfo("监视者被贿赂了，好感度增加20");
        }
        else if(itemName=="ProofOfIllicitMoney")
        {
            NarratorSystem.Instance.SendDialogueInfo("监视者得到了医生贪污的证据，好感度达到100");
        }
        else
        {
            NarratorSystem.Instance.SendDialogueInfo("监视者并不需要这个");
        }
    }
    #region 单例模式
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

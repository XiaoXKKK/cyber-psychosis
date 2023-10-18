using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Aelia : MonoBehaviour
{
    public int favorability = 50;
    public bool isGoodCondition = false;
    public DialogConf conf0;
    public DialogConf confai;
    //public void IncreaseFavorability()//每次对话都会增加20好感度，被使用镇静剂时会增加30好感度
    //{
        
    //    if (isGoodCondition)
    //    {
    //        favorability += 30;
    //        NarratorSystem.Instance.SendDialogueInfo("Aelia心情很好！！增加了30好感度，现在好感度达到" + favorability + "啦，可能会提供更多有价值的信息哦！");
    //        isGoodCondition = false;
    //    }
    //    else
    //    {
    //        favorability += 20;
    //        NarratorSystem.Instance.SendDialogueInfo("Aelia增加了20好感度，现在好感度达到" + favorability + "啦，可能会提供更多有价值的信息哦！");
    //    }


    //    if (favorability >= 70 && favorability < 90)//限制好感度范围
    //    {
    //        NarratorSystem.Instance.SendDialogueInfo("Aelia告诉了你薇多拉喜欢打羽毛球的信息，可能有点用吧。");
    //    }
    //    else if (favorability >=90)
    //    {
    //        NarratorSystem.Instance.SendDialogueInfo("恭喜你通关了！！");
    //    }
    //    else if (favorability < 0)
    //        favorability = 0;
    //}

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
        NarratorSystem.Instance.SendDialogueInfo("Aelia现在并不渴。");
        NarratorSystem.Instance.ShowInfo(3);
    }

    #region 单例模式
    private static NPC_Aelia instance;
    public static NPC_Aelia Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NPC_Aelia>();
            }
            return instance;
        }
    }
    #endregion
}

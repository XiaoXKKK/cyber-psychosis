using MoreMountains.InventoryEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Aelia : MonoBehaviour
{
    public int favorability = 50;
    public bool isGoodCondition = false;
    public void IncreaseFavorability()//ÿ�ζԻ���������20�øжȣ���ʹ���򾲼�ʱ������30�øж�
    {
        
        if (isGoodCondition)
        {
            favorability += 30;
            NarratorSystem.Instance.SendDialogueInfo("Aelia����ܺã���������30�øжȣ����ںøжȴﵽ" + favorability + "�������ܻ��ṩ�����м�ֵ����ϢŶ��");
            isGoodCondition = false;
        }
        else
        {
            favorability += 20;
            NarratorSystem.Instance.SendDialogueInfo("Aelia������20�øжȣ����ںøжȴﵽ" + favorability + "�������ܻ��ṩ�����м�ֵ����ϢŶ��");
        }


        if (favorability >= 70 && favorability < 90)//���ƺøжȷ�Χ
        {
            NarratorSystem.Instance.SendDialogueInfo("Aelia��������ޱ����ϲ������ë�����Ϣ�������е��ðɡ�");
        }
        else if (favorability >=90)
        {
            NarratorSystem.Instance.SendDialogueInfo("��ϲ��ͨ���ˣ���");
        }
        else if (favorability < 0)
            favorability = 0;
        NarratorSystem.Instance.ShowInfo(3);
    }
    public void Buff(string itemName)
    {
        if (itemName == "Cola")
        {
            favorability += 10;
            NarratorSystem.Instance.SendDialogueInfo("Aelia���˿��֣��øж�������10");
            NarratorSystem.Instance.ShowInfo(3);
        }
    }

    #region ����ģʽ
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MoreMountains.CorgiEngine;
using Unity.VisualScripting;

public class NarratorSystem : MonoBehaviour
{
    List<String> items = new List<String>();
    List<String> actions = new List<String>();
    List<String> dialogue = new List<String>();
    List<String> interactions = new List<String>();
    int currentItemNum = 0;
    int currentActionNum = 0;
    int currentDialogueNum = 0;
    int currentInteractionNum = 0;
    Text NarratorText;
    TypewriterEffect typewriterEffect;
    CanvasGroup NarratorUI;
    float timer = 0;
    public float displayTime = 5f;//�԰���ʾʱ��
    private void Start()
    {
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 800, sequencesCapacity: 200);
        NarratorText = GameObject.Find("NarratorText").GetComponent<Text>();
        typewriterEffect = GameObject.Find("NarratorText").GetComponent<TypewriterEffect>();
        NarratorUI = GameObject.Find("NarratorUI").GetComponent<CanvasGroup>();
        currentItemNum = 0;
        currentActionNum = 0;
        currentDialogueNum = 0;
        currentInteractionNum = 0;
    }
    private void Update()
    {
       
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                NarratorUI.DOFade(0, 0.2f);
            }
           
    }

    #region ����ģʽ
    private static NarratorSystem instance;
    public static NarratorSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NarratorSystem>();
            }
            return instance;
        }
    }
    #endregion
    #region
    public void SendItemInfo(String itemName)//������Ʒ��ʹ����Ʒʱ����
    {
        items.Add(itemName);
    }

    public void SendActionInfo(String actionName)//�������䡢�����彻��ʱ����
    {
        actions.Add(actionName);
    }

    public void SendDialogueInfo(String dialogueName)//�Ի������
    {
        dialogue.Add(dialogueName);
    }

    public void SendInteractionInfo(String interactionInfo)//��UI����ʱ����
    {
        interactions.Add(interactionInfo);
    }
    #endregion
    public void ShowInfo(String info)//�޹�ID��ֱ����ʾinfo
    {
        NarratorUI.DOFade(1, 0.2f);
        NarratorText.text = info;
        typewriterEffect.ReStartEffect();
        timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 4;//��ʾʱ��
    }

    public void ShowInfo(int ID)//����ID��ʾinfo
    {
        if (ID == 1)
        {
            if (items != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowItemsList());
            }
        }
        else if (ID == 2)
        {
            if (actions != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowActionsList());
            }
        }
        else if (ID == 3)
        {
            if (dialogue != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowDialogueList());
            }

        }
        else if (ID == 4)
        {
            if (interactions != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowInteractionsList());
            }
        }
    }
    IEnumerator ShowItemsList()
    {
        for(int i = currentItemNum; i < items.Count; i++)
        {
            NarratorText.text = items[i];
            typewriterEffect.ReStartEffect();
            timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 5;
            yield return new WaitForSeconds(typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 1);
        }
        currentItemNum = items.Count;
    }
    IEnumerator ShowActionsList()
    {
        for(int i = currentActionNum; i < actions.Count; i++)
        {
            NarratorText.text = actions[i];
            typewriterEffect.ReStartEffect();
            timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 5;
            yield return new WaitForSeconds(typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 1);
        }
        currentActionNum = actions.Count;
    }
    IEnumerator ShowDialogueList()
    {
        for(int i = currentDialogueNum; i < dialogue.Count; i++)
        {
            NarratorUI.DOFade(1,0.2f);
            NarratorText.text = dialogue[i];
            typewriterEffect.ReStartEffect();
            timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 5;
            yield return new WaitForSeconds(typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 1);
        }
        currentDialogueNum = dialogue.Count;
    }
    IEnumerator ShowInteractionsList()
    {
        for(int i = currentInteractionNum; i < interactions.Count; i++)
        {
            NarratorText.text = interactions[i];
            typewriterEffect.ReStartEffect();
            timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 5;
            yield return new WaitForSeconds(typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 1);
        }
        currentInteractionNum = interactions.Count;
    }
}

using DG.Tweening;
using MoreMountains.CorgiEngine;
using System;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.InventoryEngine;

public class SafeController : MonoBehaviour
{
    public Text inputText; // ָ����Inspector��
    private string correctPassword = "12345"; // ���������Ϊ������λ������
    private string currentPlayerInput = "";
    public float fadeTime = 0.5f;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    GameObject player;
    private void Start()
    {
        canvasGroup = GameObject.Find("SafeCanvas").GetComponent<CanvasGroup>();
        rectTransform = GameObject.Find("SafePanel").GetComponent<RectTransform>();
    }
    public void ButtonClicked(string number)
    {
        if (currentPlayerInput.Length < 5)
        {
            currentPlayerInput += number;
            UpdateDisplay();
        }

        if (currentPlayerInput.Length == 5)
        {
            ValidatePassword();
        }
    }

    public void ResetButtonClicked()
    {
        currentPlayerInput = "";
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        inputText.text = currentPlayerInput;
    }

    private void ValidatePassword()
    {
        if (currentPlayerInput == correctPassword)
        {
            OpenSafe();
        }
        else
        {
            // ���������󣬿���ִ������������������ʾ������Ϣ����Ч��
            currentPlayerInput = "";
            UpdateDisplay();
        }
    }

    private void OpenSafe()
    {
        foreach(GameObject item in ItemSystem.Instance.uniqueItems)
        {
            if (item.name== "ProofOfIllicitMoney")
            {
                item.GetComponent<InventoryPickableItem>().Pick("MainInventory","Player1");
                Debug.Log("Safe Opened!");
            }
        } 
        // ����ִ�д򿪱��չ�Ĳ���

        // ����: YourMethodToOpenTheSafe();
    }

    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0;
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = false;
        rectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), fadeTime, false).SetEase(Ease.OutExpo);
        canvasGroup.DOFade(1, fadeTime);
    }
    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1;
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = true;
        rectTransform.DOAnchorPos(new Vector2(0, -1000f), fadeTime, false).SetEase(Ease.InExpo);
        canvasGroup.DOFade(0, fadeTime);
    }
}
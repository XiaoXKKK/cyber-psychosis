using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UI_Dialog : MonoBehaviour
{
    public static UI_Dialog Instance;
    private Image head;
    private Text mainText;
    private RectTransform content;
    private Transform Options;
    private GameObject prefab_OptionItem;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        head = transform.Find("Main/Head").GetComponent<Image>();
        //mainText = transform.Find("Main/MainText").GetComponent<Text>();
        mainText = transform.Find("Main/Scroll View/Viewport/Content/MainText").GetComponent<Text>();
        content = transform.Find("Main/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        Options = transform.Find("Options");
        prefab_OptionItem = Resources.Load<GameObject>("Options_Item");
        TestDialog();
    }

    /// <summary>
    /// 开始对话
    /// </summary>
    private void TestDialog()
    {
        StartCoroutine(DoMainTextEF("这是一个测试文字~~~~~~~~~~~~~~~~"));
    }



    IEnumerator DoMainTextEF(string txt)
    {

        // 字符数量决定了 conteng的高 每23个字符增加25的高
        float addHeight = txt.Length / 23 + 1;
        content.sizeDelta = new Vector2(content.sizeDelta.x, addHeight*25);

        string currStr ="";
        for (int i = 0; i < txt.Length; i++)
        {
            currStr += txt[i];
            yield return new WaitForSeconds(0.08f);
            mainText.text = currStr;
            // 每满23个字，下移一个距离 25
            if (i>23*3&&i % 23 == 0)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, content.anchoredPosition.y+25);
            }
        }
    }
   
}

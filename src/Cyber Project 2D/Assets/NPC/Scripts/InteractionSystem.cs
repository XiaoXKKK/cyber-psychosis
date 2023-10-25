using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    public GameObject InteractionList;//管理哪一种类型的交互列表
    public GameObject TargeObject;
    public float fadeTime =1f;//淡入淡出时间
    CanvasGroup canvasGroup;//获取画布组件
    public List<GameObject> buttons= new List<GameObject>();//获取交互列表的按钮

    private void Start()
    {
        canvasGroup = InteractionList.GetComponent<CanvasGroup>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InteractionList.SetActive(true);
            StartCoroutine(FadeInCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.active)
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }
    IEnumerator FadeOutCoroutine()
    {
        canvasGroup.alpha = 1;
        foreach (var button in buttons)
        {
            button.transform.DOLocalMove(new Vector3(0, -5, 0), fadeTime).SetEase(Ease.InExpo);//淡入
        }
        canvasGroup.DOFade(0, fadeTime);//淡出
        yield return new WaitForSeconds(fadeTime);
        InteractionList.SetActive(false);
    }
    IEnumerator FadeInCoroutine()
    {
        float i = 0;
        canvasGroup.alpha = 0;
        foreach (var button in buttons)
        {
            button.transform.localPosition = new Vector3(0, -5f, 0);
        }
        foreach (var button in buttons)
        {
            button.transform.DOLocalMove(new Vector3(0, 0+i, 0), fadeTime).SetEase(Ease.OutExpo);//淡入
            i -= 0.7f;
            yield return new WaitForSeconds(0.1f);
        }
        canvasGroup.DOFade(1, fadeTime);//淡入
    }
}

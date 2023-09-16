using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
  
    }


    /// <summary>
    /// 摄像机效果-闪烁
    /// </summary>
    public void ScreenEF(float delay)
    {
        StartCoroutine(DoScreenEF(delay));

    }

    private IEnumerator DoScreenEF(float delay)
    {
        GameObject.Find("Canvas/BG").GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(delay);
        GameObject.Find("Canvas/BG").GetComponent<Image>().color = Color.white;
    }
}

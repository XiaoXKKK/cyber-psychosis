using Cinemachine;
using DG.Tweening;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewDaySystem : MonoBehaviour
{
    public int dayCount = 0;
    Text dayText;
    public MMF_Player dayUI;
    public CinemachineVirtualCamera virtualCamera;
    public MMTweenType tweenType;
    GameObject UP;
    GameObject DOWN;

    float virtualCameraSize=7.5f;
    void Start()
    {
        dayText = GameObject.Find("DayCount").GetComponent<Text>();
        UP= GameObject.Find("UP");
        DOWN= GameObject.Find("DOWN");
    }
    private void Update()
    {
        dayText.text = "第" + dayCount.ToString() + "天";
        
    }
    public void StartFadeOut()
    {
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = false;
        virtualCamera.m_Lens.OrthographicSize = 5f;
        FadeOut();
    }
  
    void FadeOut()
    {
        MMFadeOutEvent.Trigger(3f, tweenType, 0);
        Invoke("ShowDayUI", 3f);
    }
    void ShowDayUI()
    {
        dayUI.PlayFeedbacks();
        Invoke("ToRegular", 5f);
    }
    void ToRegular()
    {
        float duration = 2f; // 变化所需的时间，例如2秒
        float targetValue = 7.5f; // 目标值
        UP.transform.DOLocalMoveY(400, duration);
        DOWN.transform.DOLocalMoveY(-400, duration);
        DOTween.To(() => virtualCameraSize, x => virtualCameraSize = x, targetValue, duration).OnUpdate(() => {
            virtualCamera.m_Lens.OrthographicSize = virtualCameraSize;
        });
        Invoke("CanMove", 2f);
    }

    //------------------------------------------
    public void StartFadeIn()
    {
        NewDaySystem.Instance.dayCount++;
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = false;
        ToCinema();
    }
    void ToCinema()
    {
        float duration = 2f; // 变化所需的时间，例如2秒
        float targetValue = 5f; // 目标值
        UP.transform.DOLocalMoveY(250, duration);
        DOWN.transform.DOLocalMoveY(-250, duration);
        DOTween.To(() => virtualCameraSize, x => virtualCameraSize = x, targetValue, duration).OnUpdate(() => {
            virtualCamera.m_Lens.OrthographicSize = virtualCameraSize;
        });
        Invoke("FadeIn", 2f);
    }
    void FadeIn()
    {
        MMFadeInEvent.Trigger(2f, tweenType, 0);
        Invoke("StartFadeOut", 4f);
    }

    void CanMove()
    {
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = true;
    }
#region 单例模式
    private static NewDaySystem _instance;
    public static NewDaySystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("NewDaySystem").GetComponent<NewDaySystem>();
            }
            return _instance;
        }
    }
#endregion
}

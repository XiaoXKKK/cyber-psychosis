using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.CorgiEngine;
using MoreMountains.InventoryEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    private DialogConf[] dialogConfs;
    private InventoryInputManager inventory;


    public GameObject canvas;
    public int AItimes = 3;
    
    private void Awake()
    {
        Instance = this;
        inventory = GameObject.Find("InventoryCanvas").GetComponent<InventoryInputManager>();
        dialogConfs = Resources.LoadAll<DialogConf>("Conf");
    }
    public void StartDialog(DialogConf conf)
    {
        canvas.SetActive(true);
        ChangeInput(false);
        UI_Dialog.Instance.InitDialog(conf);
    }
    public void ChangeInput(bool flag)
    {
        InputManager.Instance.InputDetectionActive = flag;
        if (flag)
        {
            inventory.ToggleInventoryKey = KeyCode.I;
        }
        else
        {
            inventory.ToggleInventoryKey = KeyCode.None;
        }
    }
    public DialogConf GetDialogConf(int index)
    {
        return dialogConfs[index];
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

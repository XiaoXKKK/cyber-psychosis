using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using MoreMountains.Tools;
public class CGManager : MonoBehaviour
{
    public static CGManager Instance;
    public List<DialogConf> cgs;
    public GameObject black;
    public void Awake()
    {
        Instance = this;
        SceneManager.sceneUnloaded += SceneUnloaded;
    }
    private void SceneUnloaded(Scene scene)
    {
        UI_Dialog.dialogEnd.RemoveAllListeners();
    }
    private void Start()
    {
        DialogueManager.Instance.StartDialog(cgs[0]);
        UI_Dialog.dialogEnd.AddListener(() => { MMSceneLoadingManager.LoadScene("GuideScene"); });
    }
    public void EndCG(int id)
    {
        DialogueManager.Instance.StartDialog(cgs[id]);
        UI_Dialog.dialogEnd.AddListener(() => { SceneManager.LoadScene("StartScreen"); });
    }
}

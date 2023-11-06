using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public InputField api_base;
    public InputField api_key;

    public APISettings settings;
    public void ExitGame()
    {
          Application.Quit();
    }

    public void SaveAPISettings()
    {
        if (api_base.text != "")
            settings.api_base = api_base.text;
        if (api_key.text != "")
            settings.api_key = api_key.text;
    }    
}

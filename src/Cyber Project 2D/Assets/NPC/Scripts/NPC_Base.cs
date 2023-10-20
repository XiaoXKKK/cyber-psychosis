using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Base : MonoBehaviour
{
    public string npcname;
    public int favorability;
    public bool isGoodCondition;

    public NPC_Base() : base()
    {
        favorability = 50;
        isGoodCondition = false;
    }
}

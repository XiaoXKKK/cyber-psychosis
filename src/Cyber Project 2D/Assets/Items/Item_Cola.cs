using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "ColaConfig", menuName = "MoreMountains/Items/Cola", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_Cola : InventoryItem
    {
        [Header("testItem")]
        [MMInformation("测测试", MMInformationAttribute.InformationType.Info, false)]

        /// the amount of health to add to the player when the item is used
        [Tooltip("测试")]
        public float HealthBonus;
        /// <summary>
        /// When the item is used, we try to grab our character's Health component, and if it exists, we add our health bonus amount of health
        /// </summary>
        /// 
        public override bool Use(string playerID)
        {
            base.Use(playerID);

            if (ItemSystem.Instance.WhoIsOpeningInventory() == "Aelia")
            {
                NPC_Aelia.Instance.Buff("Cola");
                return true;
            }

            //TODO:请其他NPC喝可乐时的情况

            NarratorSystem.Instance.SendItemInfo("你现在并不想喝可乐。");
            NarratorSystem.Instance.ShowInfo(1);
            return false;
        }

        public override bool Pick(string playerID)
        {
            InventoryItem[] inventoryItems = GameObject.Find("MainInventory").GetComponent<Inventory>().Content;
            foreach (InventoryItem item in inventoryItems)
            {      
                if (item!=null&&item.ItemID == "EmptyCup")
                {
                    NarratorSystem.Instance.SendInteractionInfo("你装了一杯可乐");
                    NarratorSystem.Instance.ShowInfo(4);
                    GameObject.Find("MainInventory").GetComponent<Inventory>().RemoveItemByID(item.ItemID, 1);
                    return true;
                }
            }
            foreach (InventoryItem item in inventoryItems)
            {      
                if (item != null && item.ItemID == "Cola")
                {
                    NarratorSystem.Instance.SendInteractionInfo("你没有空杯子，无法拾取可乐。");
                    NarratorSystem.Instance.ShowInfo(4);
                    GameObject.Find("MainInventory").GetComponent<Inventory>().RemoveItemByID(item.ItemID,1);
                    return false;
                }
            }
            NarratorSystem.Instance.SendInteractionInfo("你没有空杯子，无法拾取可乐。");
            NarratorSystem.Instance.ShowInfo(4);
            return false;           
        }
    }
}
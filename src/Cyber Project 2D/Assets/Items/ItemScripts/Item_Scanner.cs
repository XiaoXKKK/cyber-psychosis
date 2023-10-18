using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace MoreMountains.CorgiEngine
{
    [CreateAssetMenu(fileName = "ScannerConfig", menuName = "MoreMountains/Items/Scanner", order = 1)]
    [Serializable]
    /// <summary>
    /// Pickable health item
    /// </summary>
    public class Item_Scanner : InventoryItem
    {
        [Header("testItem")]
        [MMInformation("�����", MMInformationAttribute.InformationType.Info, false)]

        /// the amount of health to add to the player when the item is used
        [Tooltip("����")]
        public float HealthBonus;
        /// <summary>
        /// When the item is used, we try to grab our character's Health component, and if it exists, we add our health bonus amount of health
        /// </summary>
        /// 
        public override bool Use(string playerID)
        {
            base.Use(playerID);

            if (ItemSystem.Instance.WhoIsOpeningInventory() == "DestroyedRobot")
            {
                NarratorSystem.Instance.ShowInfo("�����������֡�");
                return true;
            }
            else
            {
                NarratorSystem.Instance.ShowInfo("ɨ����һ�£�ûʲô��Ӧ��");
                return false;
            }
        }

        public override bool Pick(string playerID)
        {
            NarratorSystem.Instance.ShowInfo("������ɨ���ǡ�");
            return true;
        }
    }
}
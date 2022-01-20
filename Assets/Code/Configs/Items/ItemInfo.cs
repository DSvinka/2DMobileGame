using System;
using UnityEngine;

namespace Code.Configs.Items
{
    [Serializable]
    public struct ItemInfo
    {
        public string Title { get; set; }
        public Sprite Icon { get; set; }
    }
}
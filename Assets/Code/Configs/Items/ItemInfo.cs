using System;
using UnityEngine;

namespace Code.Configs.Items
{
    [Serializable]
    public struct ItemInfo
    {
        [SerializeField] private string _title;
        [SerializeField] private Sprite _icon;

        public string Title => _title;
        public Sprite Icon => _icon;
    }
}
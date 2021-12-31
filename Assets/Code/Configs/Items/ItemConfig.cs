using UnityEngine;

namespace Code.Configs.Items
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig", order = 0)]
    public sealed class ItemConfig : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _title;
        [SerializeField] private Sprite _icon;

        public int ID => _id;
        public string Title => _title;
        public Sprite Icon => _icon;
    }
}
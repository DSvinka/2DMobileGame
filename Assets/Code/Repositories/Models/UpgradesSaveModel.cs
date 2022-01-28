using UnityEngine;

namespace Code.Repositories.Models
{
    public sealed class UpgradesSaveModel
    {
        public static string GunUpgradeIDKey = nameof(GunUpgradeIDKey);
        public static string ArmorUpgradeIDKey = nameof(ArmorUpgradeIDKey);
        public static string BulletUpgradeIDKey = nameof(BulletUpgradeIDKey);
        public static string BulletPowderUpgradeIDKey = nameof(BulletPowderUpgradeIDKey);
        
        public int GunUpgradeID
        {
            get => PlayerPrefs.GetInt(GunUpgradeIDKey, -1);
            set => PlayerPrefs.SetInt(GunUpgradeIDKey, value);
        }

        public int ArmorUpgradeID
        {
            get => PlayerPrefs.GetInt(ArmorUpgradeIDKey, -1);
            set => PlayerPrefs.SetInt(ArmorUpgradeIDKey, value);
        }

        public int BulletUpgradeID
        {
            get => PlayerPrefs.GetInt(BulletUpgradeIDKey, -1);
            set => PlayerPrefs.SetInt(BulletUpgradeIDKey, value);
        }
        
        public int BulletPowderUpgradeID
        {
            get => PlayerPrefs.GetInt(BulletPowderUpgradeIDKey, -1);
            set => PlayerPrefs.SetInt(BulletPowderUpgradeIDKey, value);
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(GunUpgradeIDKey);
            PlayerPrefs.DeleteKey(ArmorUpgradeIDKey);
            PlayerPrefs.DeleteKey(BulletUpgradeIDKey);
            PlayerPrefs.DeleteKey(BulletPowderUpgradeIDKey);
        }
    }
}
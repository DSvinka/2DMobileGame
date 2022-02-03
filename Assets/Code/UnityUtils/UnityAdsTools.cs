using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.UnityUtils
{
    public sealed class UnityAdsTools: MonoBehaviour, IAdsShower
    {
        private const string GameID = "4515932";
        private const string BannerPlacementID = "Banner_Android";
        
        private void Start()
        {
            Advertisement.Initialize(GameID);
        }

        public void ShowBanner()
        {
            Advertisement.Show(BannerPlacementID);
        }
    }
}
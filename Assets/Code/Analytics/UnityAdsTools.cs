using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Analytics
{
    public sealed class UnityAdsTools: MonoBehaviour, IAdsShower
    {
        private const string GameID = "4509583";
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
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Code.Views
{
    public sealed class LoadSpriteView : MonoBehaviour
    {
        [SerializeField] private Image _targetImage;
        [SerializeField] private AssetReference _loadSprite;

        private async void Start()
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(_loadSprite);
            
            await handle.Task;
            if (handle.Status != AsyncOperationStatus.Succeeded) 
                return;
            
            _targetImage.sprite = handle.Result;
            Addressables.Release(handle);
        }
    }
}
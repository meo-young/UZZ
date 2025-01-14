using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class AddressableManager : MonoBehaviour
{
    public static AddressableManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void LoadSprite(string _address, Image _image)
    {
        Addressables.LoadAssetAsync<Sprite>(_address).Completed += (operation) =>
        {
            _image.sprite = operation.Result;
        };
    }
}

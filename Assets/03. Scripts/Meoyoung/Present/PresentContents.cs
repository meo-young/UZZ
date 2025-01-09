using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentContents : MonoBehaviour
{
    private List<Image> lockImages;
    private List<Image> activeImages;
    private void Awake()
    {

        Image[] images = GetComponentsInChildren<Image>();
        lockImages = new List<Image>();
        activeImages = new List<Image>();

        for (int i = 0; i < images.Length; i++)
        {
            if (i % 3 == 2)
                lockImages.Add(images[i]);

            if (i % 3 == 1)
                activeImages.Add(images[i]);
        }
    }

    private void Start()
    {
        InitImages();
        UpdateImages();
   }

    void InitImages()
    {
        for (int i = 0; i < activeImages.Count; i++)
        {
            activeImages[i].enabled = false;
            lockImages[i].enabled = true;
        }
    }

    public void UpdateImages()
    {
        List<int> indexs = PresentManager.instance.presentInfo.items;
        for(int i=0; i < indexs.Count; i++)
        {
            activeImages[indexs[i]].enabled = true;
            lockImages[indexs[i]].enabled = false;
        }
    }
}

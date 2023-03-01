using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct SettingsIcon
{
    public Sprite openIcon;
    public Sprite closeIcon;
    public bool isStatus;
    public dataType dataType;

}
[System.Serializable]
public enum dataType
{
    MUSIC,
    VIBRATE
}
public class SettingItem : MonoBehaviour
{
    private const string Music = "Music";
    private const string Vibrate = "Vibrate";
    [SerializeField] private Image currentImage;
    public SettingsIcon iconData;

    void Start()
    {
        iconData.isStatus = UIManager.Instance.SettingsDataController(iconData);
        Image();
    }


    // Button Click 
    public void SetData()
    {
        iconData.isStatus = !iconData.isStatus;

        if (iconData.dataType == dataType.MUSIC)
        {
            int music = PlayerPrefs.GetInt(Music);
            if (music == 0)
            {
                PlayerPrefs.SetInt(Music, 1);
                iconData.isStatus = true;
            }
            else
            {
                PlayerPrefs.SetInt(Music, 0);
                iconData.isStatus = false;
            }
            
        }
        else if (iconData.dataType == dataType.VIBRATE)
        {
            int vibrate = PlayerPrefs.GetInt(Vibrate);
            if (vibrate == 0)
            {
                PlayerPrefs.SetInt(Vibrate, 1);
                iconData.isStatus = true;
            }
            else
            {
                PlayerPrefs.SetInt(Vibrate, 0);
                iconData.isStatus = false;
            }
        }

        Image();
    }

    void Image()
    {
        if (iconData.isStatus)
            currentImage.sprite = iconData.openIcon;
        else
            currentImage.sprite = iconData.closeIcon;
    }
}

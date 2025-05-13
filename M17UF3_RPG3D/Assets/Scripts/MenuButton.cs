using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public TMP_Text message;
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(ConstantValue.PlayerTag).transform;
    }

    private void OnDisable()
    {
        message.text = string.Empty;
    }

    public void LoadGame()
    {
        var keys = InventoryController.Instance.MyItems.Keys.ToList();

        foreach (var key in keys)
        {
            if (PlayerPrefs.HasKey(key))
            {
                if (PlayerPrefs.GetInt(key) == 1)
                {
                    InventoryController.Instance.AddItem(key);
                }
                else
                {
                    InventoryController.Instance.RemoveItem(key);
                }
            }
        }

        if (PlayerPrefs.HasKey(ConstantValue.PositionX))
        {
            _player.position = new Vector3(
                               PlayerPrefs.GetFloat(ConstantValue.PositionX),
                               PlayerPrefs.GetFloat(ConstantValue.PositionY),
                               PlayerPrefs.GetFloat(ConstantValue.PositionZ)
                               );
        }
        message.text = ConstantValue.LoadedMsg;
    }

    public void SaveGame()
    {
        foreach (var item in InventoryController.Instance.MyItems)
        {
            PlayerPrefs.SetInt(item.Key, item.Value ? 1 : 0);
        }

        PlayerPrefs.SetFloat(ConstantValue.PositionX, _player.position.x);
        PlayerPrefs.SetFloat(ConstantValue.PositionY, _player.position.y);
        PlayerPrefs.SetFloat(ConstantValue.PositionZ, _player.position.z);

        message.text = ConstantValue.SavedMsg;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}

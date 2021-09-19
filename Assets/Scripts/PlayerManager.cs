using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerSettings[] settings;
    [SerializeField] private int currentLevel = 0;

    void Start()
    {
        player.SetSettings(settings[currentLevel]);
    }
    public void LevelUp()
    {
        if (currentLevel + 1 >= settings.Length)
        {
            return;
        }
        currentLevel += 1;

        PlayerSettings currentSettings = settings[currentLevel];
        player.SetSettings(currentSettings);
    }
}

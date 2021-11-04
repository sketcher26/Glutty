using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerSettings[] settings;
    [SerializeField] private int playerCurrentLevel = 0;
    public PlayerSettings[] Settings => settings;
    public int CurrentLevel => playerCurrentLevel;
    public static PlayerManager Manager;

    void Awake()
    {
        Manager = this;
    }

    void Start()
    {
        player.SetSettings(settings[playerCurrentLevel]);
    }
    public void PlayerLevelUp()
    {
        if (playerCurrentLevel + 1 >= settings.Length)
        {
            return;
        }
        playerCurrentLevel += 1;

        PlayerSettings currentSettings = settings[playerCurrentLevel];
        player.SetSettings(currentSettings);
    }
}

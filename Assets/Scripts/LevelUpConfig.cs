using UnityEngine;

[CreateAssetMenu(menuName = "Entity/LevelUpConfig", fileName = "LevelUp Config")]
public class LevelUpConfig : ScriptableObject
{
    [SerializeField] private EntitySettings[] settings;

    public bool HasLevelSettings(int level)
    {
        return level < settings.Length;
    }

    public EntitySettings GetLevelSettings(int level)
    {
        if (level >= settings.Length)
            throw new System.NotImplementedException("нет конфига для левела");
        return settings[level];
    }
}
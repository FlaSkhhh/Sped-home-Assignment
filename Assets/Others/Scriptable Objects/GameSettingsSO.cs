using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Task2Game/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Spawning")]
    public float spawnInterval = 1.5f;
    public float fallSpeed = 3f;

    [Header("Scoring")]
    public int pointsPerCatch = 10;
    public int winningScore = 100;
}

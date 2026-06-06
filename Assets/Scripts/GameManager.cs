using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameSettingsSO settings;
    public SpawnManager spawner;

    private int currentScore = 0;
    private bool isGameOver = false;

    public UIManager uIManager;

    void OnEnable()
    {
        //subscribe to the falling item's event for catch completion
        FallingItem.OnCaught += HandleItemCaught;
    }


    private void HandleItemCaught(int points)
    {
        if (isGameOver) return;

        currentScore += points;
        uIManager.UpdateScoreUI(currentScore);

        if (currentScore >= settings.winningScore)
        {
            isGameOver = true;
            spawner.enabled = false; //stop dropping new items by disabling the monobehaviour
            uIManager.ShowGameOverScreen();
            spawner.ResetSpawner();
        }
    }
    //called from restart button
    public void ResetGame()
    {
        currentScore = 0;
        isGameOver = false;
    }

    void OnDisable()
    {
        //unsub for memory cleaning
        FallingItem.OnCaught -= HandleItemCaught;
    }
}
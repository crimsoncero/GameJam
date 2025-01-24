using MoreMountains.Feedbacks;
using UnityEngine;

public class TestingInit : MonoBehaviour
{
    public LevelData LevelData;

 


    private void StartGame()
    {
        LevelManager.Instance.Init(LevelData);
        GameManager.Instance.StartGame();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10,10,150,100), "Start Game"))
        {
            StartGame(); 
        }
        if (GUI.Button(new Rect(10, 120, 150, 100), "Gain Bloodlust"))
        {
            PlayerController.Instance.Unit.GainBloodlust();
        }
    }
}

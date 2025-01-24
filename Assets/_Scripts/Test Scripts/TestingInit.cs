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
        if (GUI.Button(new Rect(10, 120, 150, 100), "Increase Bubble by 10"))
        {
            PlayerController.Instance.Unit.Heal(10);
        }
        if (GUI.Button(new Rect(10, 240, 150, 100), "Lower Bubble by 10"))
        {
            PlayerController.Instance.Unit.TakeDamage(10);
        }
    }
}

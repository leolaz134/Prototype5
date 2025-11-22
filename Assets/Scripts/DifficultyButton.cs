using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty;
    private Button button;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //Basically add listner makes game see when button is clicked to
        //then call the SetDifficulty function
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}

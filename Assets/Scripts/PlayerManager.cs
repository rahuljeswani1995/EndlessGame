using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject staringText;
    public static int numberOfCoins;
    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        Time.timeScale = 1;
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        coinsText.text = "Coins: " + numberOfCoins;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            isGameStarted = true;
            Destroy(staringText);
        }
    }
}

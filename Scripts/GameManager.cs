using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int scoreCoin;
    public int scoreEnemy;
    public Text textScore;
    public Text textVida;
    public PlayerMovement player;
    public int nivelActual = 2;

    //private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        scoreCoin = 0;
        scoreEnemy = 0;
        textScore.text = "Coins: " + scoreCoin + "   Enemies: " + scoreEnemy ;
        textVida.text = "Vida: 100";
    }

    public void AddScoreCoins()
    {
        scoreCoin += 1;
        textScore.text = "Coins: " + scoreCoin + "   Enemies: " + scoreEnemy;
    }

    public void AddScoreEnemies()
    {
        scoreEnemy += 1;
        textScore.text = "Coins: " + scoreCoin + "   Enemies: " + scoreEnemy;
    }

    public void RestaVida(){

        textVida.text = "Vida: " + gameObject.GetComponent<PlayerMovement>().vida;
    }

    
}

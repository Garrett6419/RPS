using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //Cams
    public Camera cam1;
    public Camera cam2;


    // PLAYER VARIABLES
    public List<Card> deck = new List<Card>();
    public int deckSize ;
    public Transform[] cardSlots;
    public int playerHandSize;
    public List<Card> Hand = new List<Card>() ;
    public int battleHearts = 3; 
    public int gameHearts = 3 ;
    public List<Card> battleHeartsList  = new List<Card>();
    public List<Card> gameHeartsList = new List<Card>();


    // ENEMY VARIABLES
    public List<Card> enemyDeck = new List<Card>() ;
    public int enemyDeckSize ;
    public Transform[] enemyCardSlots;
    public int enemyHandSize;
    public List<Card> enemyHand = new List<Card>() ;
    public int enemyHearts = 3;
    public List<Card> enemyHeartsList = new List<Card>() ;

    // GAME VARIABLES
    public int gameRound ;

    // Shop Variables
    public int Money = 10;
    public Text moneyText ;
    public Text ShopMoneyText;
    public Text roundText ;
    public List<Card> shopCards = new List<Card>();
    public Transform[] shopCardSlots;


    public void Start() {
        cam1.enabled = true;
        cam2.enabled = false;
        getHand() ;
        moneyText.text =  "Money: $" + Money ;
        roundText.text = "Round: " + gameRound ;

    }

    // Function to draw a random card
    public void getHand()
    {
        // Get The Player's Hand
        for(int i = 0 ; i < playerHandSize ; i++) {
            Card randCard = deck[Random.Range(0, deck.Count)];
            randCard.gameObject.SetActive(true) ;
            randCard.transform.position = cardSlots[i].position ;
            deck.Remove(randCard) ;
            Hand.Add(randCard) ;
        }

        for(int i = 0 ; i < enemyHandSize ; i++) {
            Card randCard = enemyDeck[Random.Range(0, enemyDeckSize)] ;
            randCard.gameObject.SetActive(true) ;
            randCard.transform.position = enemyCardSlots[i].position ;
            enemyDeck.Remove(randCard) ;
            enemyDeckSize-- ;
            enemyHand.Add(randCard) ;
        }

    }

    public void battle(Card playerCard) {
        int enemyCardType = enemyPickCard();
        int winner = playerCard.getWinner(enemyCardType) ;
        if(winner == 0) {
            resetBoard() ;
        }
        else if(winner == 2) {
            int exp = Random.Range(1,2) ;
            winner = 1 ;
            for(int i = 0 ; i < exp ; i++) {
                winner *= -1 ;
            }
        }
        else if(winner == 1) {
            dealDamage() ;
            resetBoard() ;
        }
        else if(winner == -1) {
            takeDamage() ;
            resetBoard() ;
        }
    }

    public void takeDamage() {
        battleHeartsList[battleHearts-1].gameObject.SetActive(false) ;
        battleHearts-- ;
        
        if(battleHearts == 0 ) {
            gameHeartsList[gameHearts-1].gameObject.SetActive(false) ;
            gameHearts--; 
            if(gameHearts < 1) {
                gameOver() ;
            }
            battleHearts = 3 ;
            for(int i = 0 ; i < 3 ; i++) {
                battleHeartsList[i].gameObject.SetActive(true) ;
            }


        }
    }

    public void dealDamage() {
        enemyHeartsList[enemyHearts-1].gameObject.SetActive(false) ;
        enemyHearts--;

        if(enemyHearts < 1) {
            resetBoard() ;
            increaseGameRound() ;
        }
    }

    public void resetBoard() {
        for(int i = enemyHandSize - 1 ; i >= 0 ; i--) {
            enemyHand[i].gameObject.SetActive(false) ;
            enemyDeck.Insert(0, enemyHand[i]) ;
            enemyDeckSize++ ;
            enemyHand.Remove(enemyHand[i]) ;
        }
        for(int i = Hand.Count -1 ; i >= 0 ; i--) {
            Hand[i].gameObject.SetActive(false) ;
            deck.Add(Hand[i]) ;
            Hand.Remove(Hand[i]) ;
        }
        getHand() ;
    }

    public int enemyPickCard() {
        int chosenCard = enemyHand[Random.Range(0, enemyHandSize)].cardType ;
        int[] enemyCardUtility = new int[enemyHandSize] ;
        int min = 100 ;
        int max = -100 ;
        for(int i = 0 ; i < enemyHandSize ; i++) {
            enemyCardUtility[i] = 0 ;
            for(int j = 0 ; j < playerHandSize ; j++) {
                enemyCardUtility[i] -= Hand[j].getWinner(enemyHand[i].cardType) ;
            }
            if(enemyCardUtility[i] < min) {
                min = enemyHand[i].cardType ;
            }
            if(enemyCardUtility[i] > max) {
                max = enemyHand[i].cardType ;
            }
        }

        if(gameRound < 3) {
            return min;
        }
        else if(gameRound < 6) {
            return chosenCard ;
        }
        else if(gameRound >= 6) {
            return max ;
        }
        return 0 ;
    }

    public void playerWins() {
        Debug.Log("Player wins!") ;
    }

    public void gameOver() {
        Debug.Log("Game Over!") ;
    }

    public void increaseGameRound() {
        gameRound++ ;
        updateEnemyDeck() ;
        refreshEnemy() ;
        refreshPlayer() ;
        if(gameRound >= 10) {
            playerWins() ;
        }
        Money += 5;
        changeScene();
        initShop();

    }

    public void refreshEnemy() {
        enemyHearts = 3 ;
        for(int i = 0 ; i < enemyHearts ; i++) {
            enemyHeartsList[i].gameObject.SetActive(true) ;
        }
    }

    public void refreshPlayer() {
        battleHearts = 3 ;
        for(int i = 0 ; i < battleHearts ; i++) {
            battleHeartsList[i].gameObject.SetActive(true) ;
        }
    }

    public void updateEnemyDeck() {
        for(int i = 0 ; i < 3 ; i++) {
            int newCardPos = enemyDeckSize ;
            int randCard =  Random.Range(newCardPos, enemyDeck.Count) ;
            Card temp = enemyDeck[randCard] ;
            enemyDeck.RemoveAt(randCard) ;
            enemyDeck.Insert(newCardPos, temp) ;
            enemyDeckSize++ ;
        }
    }

    public void changeScene()
    {
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
        moneyText.text = "Money: $" + Money;
        roundText.text = "Round: " + gameRound;
    }

    public void initShop()
    {
        for (int i = 0; i < 3; i++)
        {
            Card randCard = shopCards[Random.Range(0, shopCards.Count)];
            randCard.gameObject.SetActive(true);
            randCard.transform.position = shopCardSlots[i].position;
            shopCards.Remove(randCard);
        }

        ShopMoneyText.text = "$" + Money;
    }


}
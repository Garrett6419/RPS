using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    // PLAYER VARIABLES
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public int playerHandSize;
    public List<Card> Hand = new List<Card>() ;
    public int battleHearts = 3; 
    public int gameHearts = 3 ;

    // ENEMY VARIABLES
    public List<Card> enemyDeck = new List<Card>() ;
    public Transform[] enemyCardSlots;
    public int  enemyHandSize;
    public List<Card> enemyHand = new List<Card>() ;
    public int enemyHearts = 3;

    public void Start() {
        getHand() ;
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
            Card randCard = enemyDeck[Random.Range(0, enemyDeck.Count)] ;
            randCard.gameObject.SetActive(true) ;
            randCard.transform.position = enemyCardSlots[i].position ;
            enemyDeck.Remove(randCard) ;
            enemyHand.Add(randCard) ;
        }

    }

    public void battle(Card playerCard) {
        int enemyCardType = enemyHand[Random.Range(0, enemyHandSize)].cardType ;
        int winner = playerCard.getWinner(enemyCardType) ;
        if(winner == 0) {
            resetBoard() ;
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
        battleHearts-- ;
        if(battleHearts == 0 ) {
            gameHearts--; 
            if(gameHearts < 1) {
                gameOver() ;
            }
            battleHearts = 3 ;

        }
    }

    public void dealDamage() {
        enemyHearts--;
        if(enemyHearts < 1) {
            resetBoard() ;
            playerWins() ;
        }
    }

    public void resetBoard() {
        for(int i = 0 ; i <  enemyHandSize ; i++) {
            enemyHand[i].gameObject.SetActive(false) ;
            enemyDeck.Add(enemyHand[i]) ;
            enemyHand.Remove(enemyHand[i]) ;
        }
        for(int i = 0 ; i <  Hand.Count ; i++) {
            Hand[i].gameObject.SetActive(false) ;
            deck.Add(Hand[i]) ;
            Hand.Remove(Hand[i]) ;
        }
        getHand() ;

    }

    public void playerWins() {
        Debug.Log("Player wins!") ;
    }

    public void gameOver() {
        Debug.Log("Game Over!") ;
    }



}
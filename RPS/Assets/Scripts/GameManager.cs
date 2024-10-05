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

    // ENEMY VARIABLES
    public List<Card> enemyDeck = new List<Card>() ;
    public Transform[] enemyCardSlots;
    public int  enemyHandSize;
    public List<Card> enemyHand = new List<Card>() ;


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

}
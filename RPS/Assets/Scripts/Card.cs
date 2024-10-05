using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardType ;
    private GameManager gm ;

    private int[][] typeChart  = new int[3][3] ; { 0, -1, 1, 1, 0, -1, -1, 1, 0 } ;
    private void Start() {
        gm = FindObjectOfType<GameManager>() ;
    }

    private void OnMouseDown() {
        battle(this) ;
    }

    public int getWinner(int enemyCardType) {
        return typeChart[cardType][enemyCardType] ;
    }
}

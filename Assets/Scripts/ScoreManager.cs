using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score1 = 0;
    public int score2 = 0;
    public int score3 = 0;
    public int score4 = 0;
    private float[] scores = {0,0,0,0 };
    public GameObject[] players = { };
    bool[] deadPlayers;
    private bool add;
    public Animator winner;
    public Animator starter;
    public TextMeshProUGUI roundNum;
    private GameObject newPlayer;

    

    public void AddPlayer(PlayerInput playerInput) 
    {
        newPlayer = playerInput.gameObject;
        add = true;
    }
    private void LateUpdate()
    {
        if (add) 
        {
            // add the newest player to the player list
            int p = players.Length + 1;
            Array.Resize(ref players, players.Length +1);
            players[players.Length-1]=newPlayer;
            add = false;
            //rezise the dead player array to the length of the current players
            Array.Resize(ref deadPlayers, players.Length);
            deadPlayers[deadPlayers.Length - 1] = true;
        }
    }
    public void IncreaseScore(int player)
    {
        scores[player] += 1;
        if (scores[player] >= 2) 
        {
            Win(player);
        }
        else {
            starter.SetTrigger("RoundStart");
            starter.SetInteger("Round", Round());
            
            roundNum.text = Round().ToString();
        }
    }

    void Win(int player)
    {
        winner.SetInteger("player", player+1);
        winner.SetTrigger("won");
        SceneManager.LoadScene("Title");
        Debug.Log("won");
    }
    void Died(int player) 
    {
        deadPlayers[player - 1] = false;
        int a=0;
        foreach (bool p in deadPlayers) 
        { 
            if (p) { a += 1; }
        }
        if (a == 1 && deadPlayers.Length >1)
        {
            IncreaseScore(Array.IndexOf(deadPlayers,true));
            Reset();
        }
    }
    private void Reset()
    {
        foreach(GameObject p in players) 
        {
            p.BroadcastMessage("Reset");
        }
        
    }
    private int Round() 
    {
        int a = 1;
        foreach (int s in scores) { a += s; }
        return a;
        
    }
    void Start()
    {
        scores[0] = score1;
        scores[1] = score2;
        scores[2] = score3;
        scores[3] = score4;
        StartCoroutine(Timer());
    }
    IEnumerator Timer() 
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.5f);
            roundNum.text = (30 - i).ToString();
            foreach (GameObject p in players) { p.GetComponentInChildren<GettingShotManager>().health = 3; }
        }
        starter.SetTrigger("RoundStart");
        foreach (GameObject p in players) { p.GetComponentInChildren<GettingShotManager>().health = 3; }

    }
}

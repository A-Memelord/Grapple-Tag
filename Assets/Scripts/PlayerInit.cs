using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    void Start()
    {
        // find the score manager
        ScoreManager sm = GameObject.Find("ScoreManagerObject").GetComponent<ScoreManager>();
        //get players in game
        int p = sm.players.Length + 1;
        // set name to player + the needed number
        gameObject.name = "Player" + p.ToString();
    }
}

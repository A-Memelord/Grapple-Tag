using UnityEngine;
using UnityEngine.UI;

public class PlayerInit : MonoBehaviour
{
    public int num;
    void Start()
    {
        // find the score manager
        ScoreManager sm = GameObject.Find("ScoreManagerObject").GetComponent<ScoreManager>();
        //get players in game
        int p = sm.players.Length + 1;
        // set name to player + the needed number
        gameObject.name = "Player" + p.ToString();
        num = p;
    }
    private void Reset()
    {
        GetComponentInChildren<GettingShotManager>().health = 3;
        Color c = new Color(255, 0, 0, 0);
        gameObject.GetComponentInChildren<Image>().color = c;
    }
}

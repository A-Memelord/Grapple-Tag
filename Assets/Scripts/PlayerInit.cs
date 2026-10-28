using UnityEngine;
using UnityEngine.UI;

public class PlayerInit : MonoBehaviour
{
    public int num;
    public Vector3[] starts;
    // find the score manager
    ScoreManager sm;
    void Start()
    {
        ScoreManager sm = GameObject.Find("ScoreManagerObject").GetComponent<ScoreManager>();
        //get players in game
        int p = sm.players.Length + 1;
        
  
        // set name to player + the needed number
        gameObject.name = "Player" + p.ToString();
        num = p;
        //if (p != 1)
        //{
        //    transform.position = starts[p];
        //    Debug.Log(starts[p]);
        //}
        //else
        //{
        //    Reset();
        //}
    }

    private void Reset()
    {
        transform.position = starts[num-1];
        GetComponentInChildren<GettingShotManager>().health = 3;
        Color c = new Color(255, 0, 0, 0);
        gameObject.GetComponentInChildren<Image>().color = c;
    }
}

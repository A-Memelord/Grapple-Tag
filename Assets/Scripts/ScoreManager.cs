using UnityEditor.Build.Player;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score1 = 0;
    public float score2 = 0;
    public float score3 = 0;
    public float score4 = 0;
    private float[] scores = {0,0,0,0 };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void IncreaseScore(int player)
    {
        scores[player] += 1;
        if (scores[player] >= 2) 
        {
            Win(player);
        }
        

    }

    // Update is called once per frame
    void Win(int player)
    {
        ;
    }
    void Start()
    {
        scores[0] = score1;
        scores[1] = score2;
        scores[2] = score3;
        scores[3] = score4;
    }
}

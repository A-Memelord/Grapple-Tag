using UnityEngine;
using UnityEngine.UI;

public class GettingShotManager : MonoBehaviour
{
    [Header("Possible behaviors: destructable, player")]
    public string onShotBehavior;
    
    private void Destruct() { Destroy(this.gameObject); }
    private void PlayerHit() 
    {
        Color c = new Color(255, 0, 0, 175);
        gameObject.GetComponentInChildren<Image>().color = c; 
    }

    void Hit()
    {
        Debug.Log("hit");
        if (onShotBehavior == "destructable") { Destruct(); }
        if (onShotBehavior == "player") { PlayerHit(); }
    }
}

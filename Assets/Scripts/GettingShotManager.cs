using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class GettingShotManager : MonoBehaviour
{
    [Header("Possible behaviors: destructable, player")]
    public string onShotBehavior;
    
    private void Destruct() { Destroy(this.gameObject); }
    public int health = 3;
    public Sprite[] bars;
    public Image bar;
    public ParticleSystem explode;
    private void PlayerHit() 
    {
        GetComponent<AudioSource>().Play();
        if (health != 0)
        { health -= 1; }
        if (health == 0)
        {
            Color c = new Color(255, 0, 0, 175);
            gameObject.GetComponentInChildren<Image>().color = c;
            explode.Play();
        }
        bar.sprite = bars[health];
    }

    void Hit()
    {
        Debug.Log("hit");
        if (onShotBehavior == "destructable") { Destruct(); }
        if (onShotBehavior == "player") { PlayerHit(); }
    }
}

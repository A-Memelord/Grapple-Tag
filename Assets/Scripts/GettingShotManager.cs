using UnityEngine;

public class GettingShotManager : MonoBehaviour
{
    [Header("Possible behaviors: destructable,")]
    public string onShotBehavior;
    
    private void destruct() { Destroy(this.gameObject); }

    void Hit()
    {
        if (onShotBehavior == "destructable") { destruct(); }
    }
    
   
}

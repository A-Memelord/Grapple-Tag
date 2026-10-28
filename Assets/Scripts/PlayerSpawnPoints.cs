using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnPoints : MonoBehaviour
{


    public GameObject playerPrefab;
    public Transform spawnA;
    public Transform spawnB;



    private bool useFirstSpot = true;


    private void OnSpawnInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            spawnObject();
        }
    }


    private void spawnObject()
    {
        if (playerPrefab == null || spawnA == null || spawnB == null) return;

        Vector3 targetPos = useFirstSpot ? spawnA.position : spawnB.position;
        Quaternion targetRotation = useFirstSpot ? spawnA.rotation : spawnB.rotation;


        Instantiate(playerPrefab, targetPos, targetRotation);


        useFirstSpot = !useFirstSpot;

    }



    
}

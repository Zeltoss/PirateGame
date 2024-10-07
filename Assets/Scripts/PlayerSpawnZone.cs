using UnityEngine;

public class PlayerSpawnZone : MonoBehaviour
{
    // this script is attached to the player spawn zone
    // it tracks when the player leaves the zone and calls an event that starts spawning the enemies

    private void OnTriggerExit(Collider other)
    {
        EnemySpawner.onLeavingSafeZone?.Invoke();
        Destroy(this.gameObject);
    }
}

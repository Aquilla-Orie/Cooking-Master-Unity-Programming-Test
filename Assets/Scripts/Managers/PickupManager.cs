using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private PickupBase[] _pickups;

    [SerializeField] private SpriteRenderer _leftKitchenBound;
    [SerializeField] private SpriteRenderer _rightKitchenBound;
    [SerializeField] private SpriteRenderer _topKitchenBound;
    [SerializeField] private SpriteRenderer _bottomKitchenBound;

    public static PickupManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnRandomPickupForPlayer(PlayerBase player)
    {
        //Select random pickup to spawn
        PickupBase pickupToSpawn = _pickups[Random.Range(0, _pickups.Length)];

        //Select a random position close to the player, but within the kitchen
        int minXPos = (int)(_leftKitchenBound.bounds.center.x + _leftKitchenBound.bounds.extents.x);
        int maxXPos = (int)(_rightKitchenBound.bounds.center.x - _rightKitchenBound.bounds.extents.x);
        int minYPos = (int)(_bottomKitchenBound.bounds.center.y + _bottomKitchenBound.bounds.extents.y);
        int maxYPos = (int)(_topKitchenBound.bounds.center.y - _topKitchenBound.bounds.extents.y);

        Vector2 spawnPos = new Vector2(Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos));

        //Spawn pickup in random location
        var inst = Instantiate(pickupToSpawn, spawnPos, Quaternion.identity);
        inst.PlayerForPickup = player;
    }
}

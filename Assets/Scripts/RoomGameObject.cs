using System;
using Generators.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGameObject : MonoBehaviour
{
    private Transform _enemies;

    private void Awake()
    {
        _enemies = transform.Find("Enemies");
    }

    public void CarveExit(DirectionsEnum direction)
    {
        var wall = transform.Find($"Walls/{direction.Name}").gameObject;
        if (wall != null)
        {
            wall.SetActive(false);
        }

        var tile = transform.Find($"Grid/Walls/{direction.Name}").gameObject;
        if (tile != null)
        {
            tile.SetActive(false);
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        var position = transform.position;
        var minX = (int)MathF.Floor(position.x) / 20 * 20 + 2;
        var minY = ((int)MathF.Floor(position.y) / 20 - 1) * 20 + 2;
        var maxX = ((int)MathF.Floor(position.x) / 20 + 1) * 20 - 2;
        var maxY = (int)MathF.Floor(position.y) / 20 * 20 - 2;
        Instantiate(enemy, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity, _enemies);
    }
    
}

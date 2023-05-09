using System.Collections.Generic;
using Generators;
using UnityEngine;
using System.Linq;
using Battle;
using Unity.Mathematics;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> rooms;

    [SerializeField] 
    private float gridSize;

    [SerializeField] 
    private GameObject level;

    private Generators.LevelGenerator _generator;

    [SerializeField]
    private List<GameObject> enemies; // todo transform into hashmap

    [SerializeField]
    private GameObject hero;

    [SerializeField]
    private float difficulty;
    
    void Start()
    {
        _generator = new Generators.LevelGenerator(new()
        {
            new MazeGenerator(),
            new RoomPlacement(rooms.Select(e => e.name).ToList()),
            new EnemyDisposer(enemies.Select(e => e.GetComponent<IPlayable>()).ToList(), hero.GetComponent<IPlayable>())
        });
        var result = _generator.Generate(5, 5, difficulty);
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                var room = Instantiate(rooms.Find(e => e.name == result[i, j].Type),
                    new Vector3(gridSize * j, -gridSize * i), quaternion.identity);
                room.transform.parent = level.transform;
                var roomObject = room.gameObject.GetComponent<RoomGameObject>();
                foreach (var directionsEnum in result[i,j].GetExits())
                {
                    roomObject.CarveExit(directionsEnum);
                }

                if (i == 0 && j == 0)
                {
                    continue;
                }

                foreach (var enemy in result[i, j].GetEnemies())
                {
                    roomObject.AddEnemy(enemies.Find(e => e.name == enemy.Name));
                }
            }
        }
    }
}

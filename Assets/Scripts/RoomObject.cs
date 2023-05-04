using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Room", order = 1)]
public class RoomObject : ScriptableObject
{

    public GameObject floor;
    public GameObject walls;
    public List<GameObject> enemies;

}

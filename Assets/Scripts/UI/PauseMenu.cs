using System.Collections;
using System.Collections.Generic;
using Battle;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] 
    private GameObject pause;

    [SerializeField] 
    private TextMeshProUGUI healthPoints;
    [SerializeField] 
    private TextMeshProUGUI movementSpeed;
    [SerializeField] 
    private TextMeshProUGUI attackSpeed;
    [SerializeField] 
    private TextMeshProUGUI attackDamage;
    [SerializeField] 
    private TextMeshProUGUI attackRange;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void Init()
    {
        var hero = GameObject.Find("Hero");
        var playable = hero.GetComponent<Playable>();
        healthPoints.text = $"Health points: {playable.HealthPoint} units";
        movementSpeed.text = $"Movement speed: {playable.MoveSpeed} units";
        attackDamage.text = $"Attack damage: {playable.AttackDamage} units";
        attackSpeed.text = $"Attack speed: {playable.AttackSpeed} per second";
        attackRange.text = $"Attack range: {playable.AttackRange} units";
    }

    public void TogglePause()
    {
        pause.SetActive(!pause.activeSelf);
        if (pause.activeSelf)
        {
            Init();
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

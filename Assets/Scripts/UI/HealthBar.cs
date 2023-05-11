using System;
using Battle;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] 
    private RectTransform rectTransform;

    private Playable _playable;

    [SerializeField]
    private float minValue;
    
    [SerializeField]
    private float maxValue;

    private int _maxHp;
    
    void Start()
    {
        var hero = GameObject.Find("Hero");
        _playable = hero.GetComponent<Playable>();
        _playable.OnDamage += UpdateHealthBar;
        _maxHp = _playable.HealthPoint;
    }

    private void UpdateHealthBar(int health)
    {
        var width = (health * 1.0f / _maxHp) * (maxValue - minValue) + minValue;
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }

    private void OnDestroy()
    {
        _playable.OnDamage -= UpdateHealthBar;
    }
}

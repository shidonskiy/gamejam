using System;
using GameJam.Scripts.Levels;
using UnityEngine;

public class LevelRestart : MonoBehaviour
{
    [SerializeField] private float _timeHoldToRestart = 1f;
    [SerializeField] private KeyCode _keyToHold = KeyCode.R;
    [SerializeField] private Level _level;

    private float _timer;

    private void Awake()
    {
        _level = GetComponent<Level>();
    }

    void Update()
    {
        if (Input.GetKeyDown(_keyToHold))
        {
            _timer = Time.time;
        }

        if (Input.GetKey(_keyToHold) && Time.time - _timer > _timeHoldToRestart)
        {
            _level.Restart();
        }
    }
}

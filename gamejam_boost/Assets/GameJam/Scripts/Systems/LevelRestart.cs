using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    [SerializeField] private float _timeHoldToRestart = 1f;
    [SerializeField] private KeyCode _keyToHold = KeyCode.R;

    private float _timer;

    void Update()
    {
        if (Input.GetKeyDown(_keyToHold))
        {
            _timer = Time.time;
        }

        if (Input.GetKey(_keyToHold) && Time.time - _timer > _timeHoldToRestart)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}

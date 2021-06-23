using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovement : MonoBehaviour
{
    [SerializeField] Transform joystickEye;
    [SerializeField] float joystickRadius;

    private Transform _player;

    Vector3 _eyeCenterPosition;
    private void Awake()
    {
        _eyeCenterPosition = joystickEye.localPosition;
    }

    void Start()
    {
        _player = NewPlayer.Instance.transform;
    }

    void Update() {
        Vector3 lookDir = (_player.position - (transform.parent.position + _eyeCenterPosition)).normalized;
        joystickEye.localPosition = _eyeCenterPosition + lookDir * joystickRadius ;
    }
}

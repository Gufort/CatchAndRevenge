using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimation : MonoBehaviour
{
    [Header("Настройки анимации для лучника: \n")]
    [SerializeField] private ArcherMove _archerMove;
    [SerializeField] private ArcherHP _archerHP;
    private Animator _animator;
    private string IS_RUNNING = "IsRunning";
    private string CHASING_SPEED = "ChasingSpeed";
    private string ATTACK = "Attack";
    private string IS_DIE = "IsDie";

    private void Awake(){
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _archerHP = GetComponent<ArcherHP>();
        _archerMove = GetComponent<ArcherMove>();
        _archerHP.OnArcherDeath += _archer_OnDeath;
    }

    private void  _archer_OnDeath(object sender, System.EventArgs e){
        _animator.SetBool(IS_DIE, true);
    }

}

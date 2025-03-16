using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxhealth  = 100;
     private int _health;
    private PhotonView _view;
    private int Health { get { return _health; } set { _health = value; _healthBar.OnHealthChanged(_health); } }


    private UiPlayerHealthBar _healthBar;

    public void TakeDamage(int damage)
    {
        _view.RPC("RemoteDamage", RpcTarget.All, damage);
    }
    [PunRPC] 
    private void RemoteDamage(int damage)
    {
        if (damage < 1)
        {
            return;
        }
        Health -= damage;
    }

    private void Awake()
    {
        
    }
    private void Start()
    {
        
       _view = GetComponent<PhotonView>();
        _healthBar = GetComponentInChildren<UiPlayerHealthBar>();
        _healthBar.SetMaxHealth(_maxhealth);
        Health = _maxhealth;
    }



}
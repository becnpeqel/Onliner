using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private float _speed = 8.0f;
    [SerializeField] private int _damage = 10;
    private GameObject _objectToIgnor;
    private Rigidbody _rb;
    
    private IEnumerator DestroyAuthor(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(gameObject);
    }

    private void Awake()
    {
        _rb = gameObject.AddComponent<Rigidbody>();
        _rb.useGravity = false;
       StartCoroutine(DestroyAuthor(gameObject, 1.0f));
    }

    
    private void Update()
    {
        
    }
    
    public void SetDirection(Vector3 direction, GameObject objectToIgnor = null)
    {
        _rb.AddForce(direction.normalized * _speed, ForceMode.Impulse);
        transform.forward = direction;
        

        _objectToIgnor = objectToIgnor;
        _collider.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _objectToIgnor && other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
           PhotonNetwork.Destroy(gameObject);
        }
    }


}
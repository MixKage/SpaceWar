using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : NetworkBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float distance;
    [SerializeField] private int damage;
    public Rigidbody rigidBody;
    public float force = 1000;
    private LayerMask whatIsSolid;
    bool inited;
    uint owner;
    public float destroyAfter = 2;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    public void Init(uint owner)
    {
        inited = true;
        this.owner = owner;
    }

    private void Start()
    {
        rigidBody.AddForce(transform.forward * force);
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other) => DestroySelf();
}
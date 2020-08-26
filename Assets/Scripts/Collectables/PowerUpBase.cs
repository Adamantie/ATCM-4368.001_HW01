using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);
    protected abstract void PowerDown(Player player);

    [SerializeField] float _powerupDuration = 5;
    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed { get { return _movementSpeed; } }

    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PowerUp(player);
            //spawn particles and sfx because we need to disable object
            Feedback();
            Disable();
            StartCoroutine(PowerUpDuration(player));
        }
    }

    private void Feedback()
    {
        //particles
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
        }
        //audio. TODO - consider Object Pooling for performance
        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 0.25f);
        }
    }

    private void Disable()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    IEnumerator PowerUpDuration(Player player)
    {
        yield return new WaitForSeconds(_powerupDuration);

        PowerDown(player);
        gameObject.SetActive(false);
    }
}

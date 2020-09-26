using System.Collections.Generic;
using UnityEngine;

public class HatCollider : MonoBehaviour
{
    public ParticleSystem HitParticles;
    public GameObject[] PresentPrefabs;
    
    private AudioSource _audioSource;
    private Exploder _exploder;
    private readonly List<Transform> _exploderPoints = new List<Transform>();

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _exploder = GetComponentInChildren<Exploder>();

        if (!_exploder)
        {
            return;
        }

        var transforms = _exploder.GetComponentsInChildren<Transform>();
        _exploderPoints.AddRange(transforms);
        _exploderPoints.RemoveAt(0);
    }
    

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);

        if (!HitParticles)
        {
            return;
        }

        SpawnPresents();
        HitParticles.Play();
        
        if (_audioSource)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }

    private void SpawnPresents()
    {
        if (PresentPrefabs.Length < 1)
        {
            return;
        }
        
        foreach (var exploderPoint in _exploderPoints)
        {
            var presentNumber = Random.Range(0, PresentPrefabs.Length - 1);
            
            var book = Instantiate(PresentPrefabs[presentNumber], exploderPoint.position, 
                PresentPrefabs[presentNumber].transform.rotation);
            var bookRigidBody = book.GetComponent<Rigidbody>();
            var direction = exploderPoint.transform.forward.normalized;
            bookRigidBody.AddForce(direction * 750);
        }
    }
}

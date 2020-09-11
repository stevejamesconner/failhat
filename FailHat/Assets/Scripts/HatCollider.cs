using UnityEngine;

public class HatCollider : MonoBehaviour
{
    public ParticleSystem HitParticles;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);

        if (!HitParticles)
        {
            return;
        }

        HitParticles.Play();
        if (_audioSource)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}

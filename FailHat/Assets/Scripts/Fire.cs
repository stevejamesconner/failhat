using TMPro;
using UnityEngine;
public class Fire : MonoBehaviour
{
    public Transform SpawnPosition;
    public GameObject BookPrefab;
    public float ForceMultiplier = 4f;
    public TextMeshProUGUI BookCounterText;

    private bool _allowedToShoot = true;
    private int _booksRemaining = 30;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0) || !_allowedToShoot || _booksRemaining == 0)
        {
            return;
        }

        _allowedToShoot = !_allowedToShoot;
    }

    private void FixedUpdate()
    {
        if (_allowedToShoot)
        {
            return;
        }

        _audioSource.PlayOneShot(_audioSource.clip);
        
        var book = Instantiate(BookPrefab, SpawnPosition.position, BookPrefab.transform.rotation);
        var bookRigidBody = book.GetComponent<Rigidbody>();
        var direction = SpawnPosition.transform.forward.normalized;
        bookRigidBody.AddForce(direction * ForceMultiplier);

        _booksRemaining--;
        BookCounterText.text = _booksRemaining.ToString();

        _allowedToShoot = !_allowedToShoot;
    }
}

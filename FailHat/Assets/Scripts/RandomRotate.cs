using UnityEngine;
public class RandomRotate : MonoBehaviour
{
    private Transform _transform;
    private float _xRot;
    private float _yRot;
    private float _zRot;
    
    public void Start()
    {
        _transform = gameObject.transform;
        _xRot = Random.Range(0, 360);
        _yRot = Random.Range(0, 360);
        _zRot = Random.Range(0, 360);
    }
    
    public void Update()
    {
        _transform.Rotate(Time.deltaTime * _xRot, Time.deltaTime * _yRot, Time.deltaTime * _zRot);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Camera : MonoBehaviour {
 
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	[FormerlySerializedAs("axes")] public RotationAxes Axes = RotationAxes.MouseXAndY;
	[FormerlySerializedAs("sensitivityX")] public float SensitivityX = 15F;
	[FormerlySerializedAs("sensitivityY")] public float SensitivityY = 15F;
	[FormerlySerializedAs("minimumX")] public float MinimumX = -360F;
	[FormerlySerializedAs("maximumX")] public float MaximumX = 360F;
	[FormerlySerializedAs("minimumY")] public float MinimumY = -60F;
	[FormerlySerializedAs("maximumY")] public float MaximumY = 60F;
	[FormerlySerializedAs("frameCounter")] public float FrameCounter = 20;

	private float _rotationX = 0F;
	private float _rotationY = 0F;
	private readonly List<float> _rotArrayX = new List<float>();
	private float _rotAverageX = 0F;
	private readonly List<float> _rotArrayY = new List<float>();
	private float _rotAverageY = 0F;
	private Quaternion _originalRotation;

	private void Update ()
	{
		if (Axes != RotationAxes.MouseXAndY)
		{
			if (Axes == RotationAxes.MouseX)
			{
				_rotAverageX = 0f;
				_rotationX += Input.GetAxis("Mouse X") * SensitivityX;
				_rotArrayX.Add(_rotationX);

				if (_rotArrayX.Count >= FrameCounter)
				{
					_rotArrayX.RemoveAt(0);
				}

				foreach (var t in _rotArrayX)
				{
					_rotAverageX += t;
				}

				_rotAverageX /= _rotArrayX.Count;
				_rotAverageX = ClampAngle(_rotAverageX, MinimumX, MaximumX);

				var xQuaternion = Quaternion.AngleAxis(_rotAverageX, Vector3.up);
				transform.localRotation = _originalRotation * xQuaternion;
			}
			else
			{
				_rotAverageY = 0f;

				_rotationY += Input.GetAxis("Mouse Y") * SensitivityY;

				_rotArrayY.Add(_rotationY);

				if (_rotArrayY.Count >= FrameCounter)
				{
					_rotArrayY.RemoveAt(0);
				}

				foreach (var t in _rotArrayY)
				{
					_rotAverageY += t;
				}

				_rotAverageY /= _rotArrayY.Count;

				_rotAverageY = ClampAngle(_rotAverageY, MinimumY, MaximumY);

				var yQuaternion = Quaternion.AngleAxis(_rotAverageY, Vector3.left);
				transform.localRotation = _originalRotation * yQuaternion;
			}
		}
		else
		{
			_rotAverageY = 0f;
			_rotAverageX = 0f;
			_rotationY += Input.GetAxis("Mouse Y") * SensitivityY;
			_rotationX += Input.GetAxis("Mouse X") * SensitivityX;
			_rotArrayY.Add(_rotationY);
			_rotArrayX.Add(_rotationX);

			if (_rotArrayY.Count >= FrameCounter)
			{
				_rotArrayY.RemoveAt(0);
			}

			if (_rotArrayX.Count >= FrameCounter)
			{
				_rotArrayX.RemoveAt(0);
			}

			foreach (var t in _rotArrayY)
			{
				_rotAverageY += t;
			}

			foreach (var t in _rotArrayX)
			{
				_rotAverageX += t;
			}

			_rotAverageY /= _rotArrayY.Count;
			_rotAverageX /= _rotArrayX.Count;

			_rotAverageY = ClampAngle(_rotAverageY, MinimumY, MaximumY);
			_rotAverageX = ClampAngle(_rotAverageX, MinimumX, MaximumX);

			var yQuaternion = Quaternion.AngleAxis(_rotAverageY, Vector3.left);
			var xQuaternion = Quaternion.AngleAxis(_rotAverageX, Vector3.up);

			transform.localRotation = _originalRotation * xQuaternion * yQuaternion;
		}
	}

	private void Start ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_originalRotation = transform.localRotation;
	}

	private static float ClampAngle (float angle, float min, float max)
	{
		angle %= 360;
		
		if ((!(angle >= -360F)) || (!(angle <= 360F)))
		{
			return Mathf.Clamp(angle, min, max);
		}

		if (angle < -360F)
		{
			angle += 360F;
		}
		
		if (angle > 360F) 
		{
			angle -= 360F;
		}
		
		return Mathf.Clamp (angle, min, max);
	}
}
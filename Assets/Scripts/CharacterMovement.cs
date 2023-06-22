using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	[RequireComponent(typeof(CharacterController))]
	public class CharacterMovement : MonoBehaviour
	{
		[SerializeField] private float _speed = 2;
		[SerializeField] private Transform _camera;
		[SerializeField] private CharacterAnimator _characterAnimator;
		[Range(0f, 10f)]
		[SerializeField] private float _rotationSpeed = 2;

		private CharacterController _controller;
		private Vector3 _lastDirection;
		private Vector3 _translation;

		private Quaternion CameraRotationXY => Quaternion.LookRotation(Vector3.ProjectOnPlane(_camera.forward, Vector3.up));
		private void Awake()
		{
			_controller = GetComponent<CharacterController>();
			_lastDirection = _controller.transform.forward;
		}

		public void Move(float speedForward, float speedRight, float deltaTime, bool rotateTowards)
		{
			_translation = CameraRotationXY * new Vector3(speedRight, 0, speedForward);
			Debug.DrawLine(transform.position, transform.position + _translation * 5, Color.green, .1f);
			_controller.Move(_speed * deltaTime * _translation);

			if (rotateTowards)
				Rotate(_translation.normalized, deltaTime);

			_characterAnimator.UpdateMovement(Time.deltaTime);
		}

		public void Rotate(Vector3 direction, float deltaTime)
		{
			if (direction == Vector3.zero)
				direction = _lastDirection;
			_controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation, Quaternion.LookRotation(direction), deltaTime * _rotationSpeed);
			_lastDirection = _controller.transform.rotation * Vector3.forward;
		}
	}
}

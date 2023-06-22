using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class CharacterAnimator : MonoBehaviour
	{
		[SerializeField] private Transform _root;
		[SerializeField] private Animator _animator;
		[Range(0f, 1f)]
		[SerializeField] private float _lerp = .5f;
		[SerializeField] private string _speedRight = "f_speedRight";
		[SerializeField] private string _speedForward = "f_speedForward";
		[SerializeField] private string _isEquipped = "b_isEquipped";

		[Space(16)]
		[SerializeField] private float _debugHeight = 2f;

		private float _smoothSpeedRight;
		private float _smoothSpeedForward;
		private Vector3 _lastPosition;

		private void Awake()
		{
			_smoothSpeedForward = SpeedForward;
			_smoothSpeedRight = SpeedRight;
			_lastPosition = _root.position;
		}

		public float SpeedRight
		{
			get => _animator.GetFloat(_speedRight);
			set
			{
				_smoothSpeedRight = Mathf.Lerp(_smoothSpeedRight, value, _lerp);
				_animator.SetFloat(_speedRight, _smoothSpeedRight);
			}
		}

		public float SpeedForward
		{
			get => _animator.GetFloat(_speedForward);
			set
			{
				_smoothSpeedForward = Mathf.Lerp(_smoothSpeedForward, value, _lerp);
				_animator.SetFloat(_speedForward, _smoothSpeedForward);
			}
		}

		/// <summary>
		/// x = SpeedRight
		/// <br>y = SpeedForward</br>
		/// </summary>
		public Vector2 Speed
		{
			set
			{
				SpeedRight = value.x;
				SpeedForward = value.y;
			}
		}

		public bool Equipped
		{
			get => _animator.GetBool(_isEquipped);
			set => _animator.SetBool(_isEquipped, value);
		}

		public void UpdateMovement(float deltaTime)
		{
			Vector3 deltaPosition = _root.position - _lastPosition;
			Vector3 forward = Vector3.Project(deltaPosition, _root.forward);
			Vector3 right = Vector3.Project(deltaPosition, _root.right);
			SpeedForward = forward.magnitude / deltaTime * (Vector3.Dot(forward, _root.forward) > 0 ? 1f : -1f);
			SpeedRight = right.magnitude / deltaTime * (Vector3.Dot(right, _root.right) > 0 ? 1f : -1f);
			_lastPosition = _root.position;

			Vector3 debug = _root.position + new Vector3(0, _debugHeight, 0);
			Debug.DrawLine(debug, debug + deltaPosition, Color.gray, 2f);
			Debug.DrawLine(debug, debug + forward / deltaTime, Color.blue, .025f);
			Debug.DrawLine(debug, debug + right / deltaTime, Color.red, .025f);
		}

	}
}

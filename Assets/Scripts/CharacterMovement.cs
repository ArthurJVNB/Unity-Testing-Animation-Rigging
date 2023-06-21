using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	[RequireComponent(typeof(CharacterController))]
	public class CharacterMovement : MonoBehaviour
	{
		private CharacterController _controller;

		private void Awake()
		{
			_controller = GetComponent<CharacterController>();
		}

		public void Move(Vector3 translate)
		{
			_controller.Move(translate);
		}
	}
}

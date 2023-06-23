using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class PlayerInput : MonoBehaviour
	{
		[SerializeField] private CharacterMovement _movement;

		private float _inputX;
		private float _inputY;
		private float _inputMouseX;
		private float _inputMouseY;


		private void Update()
		{
			_inputX = Input.GetAxis("Horizontal");
			_inputY = Input.GetAxis("Vertical");

			_inputMouseX = Input.GetAxis("Mouse X");
			_inputMouseY = Input.GetAxis("Mouse Y");

			_movement.Move(_inputY, _inputX, Time.deltaTime, true, Input.GetKey(KeyCode.LeftShift));

			// test
			if (Input.GetKeyDown(KeyCode.R))
			{
				CharacterAnimator characterAnimator = GetComponent<CharacterAnimator>();
				characterAnimator.Equipped = !characterAnimator.Equipped;
			}
		}
	}
}

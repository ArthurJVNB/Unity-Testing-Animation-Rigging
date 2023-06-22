using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace SW
{
	[RequireComponent(typeof(Collider))]
	public class CharacterLook : MonoBehaviour
	{
		[SerializeField] private MultiAimConstraint _aimConstraint;
		[SerializeField] private Transform _aim;
		[SerializeField] private float _smoothStrength = 90;

		private LookableObject _currentLooking;

		private void OnValidate()
		{
			GetComponent<Collider>().isTrigger = true;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out LookableObject lookableObject))
			{
				_currentLooking = lookableObject;
				Look();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out LookableObject lookableObject))
			{
				if (lookableObject == _currentLooking)
				{
					_currentLooking = null;
					StopLooking();
				}
			}
		}

		private void Look()
		{
			StopAllCoroutines();
			StartCoroutine(Routine());

			Debug.Log("Looking");

			IEnumerator Routine()
			{
				const float target = 1;
				float currentVelocity = 0f;
				while (_currentLooking != null)
				{
					_aim.transform.position = _currentLooking.transform.position;
					_aimConstraint.weight = Mathf.SmoothDamp(_aimConstraint.weight, target, ref currentVelocity, _smoothStrength * Time.deltaTime);
					yield return null;
				}
			}
		}

		private void StopLooking()
		{
			StopAllCoroutines();
			StartCoroutine(Routine());

			Debug.Log("StopLooking");

			IEnumerator Routine()
			{
				const float target = 0;
				float currentVelocity = 0f;
				while (_aimConstraint.weight > target)
				{
					_aimConstraint.weight = Mathf.SmoothDamp(_aimConstraint.weight, target, ref currentVelocity, _smoothStrength * Time.deltaTime);
					yield return null;
				}
			}
		}
	}
}

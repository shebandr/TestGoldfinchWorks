using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneControllerButton : MonoBehaviour
{
	public Vector3 direction;
	public float speed;
	public GameObject buttonObject;

	public Vector3 pressLocalOffset = new Vector3(0f, 0f, -0.01f);

	private Vector3 startLocalPos;

	void Start()
	{
		if (buttonObject == null) buttonObject = gameObject;
		startLocalPos = buttonObject.transform.localPosition;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 6)
		{
			buttonObject.transform.localPosition = startLocalPos + pressLocalOffset;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == 6)
		{
			buttonObject.transform.localPosition = startLocalPos;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 6)
		{
			CraneSignals.UniversalSignal?.Invoke(direction, speed);
		}
	}
}

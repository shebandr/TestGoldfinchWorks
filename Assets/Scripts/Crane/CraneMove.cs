using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraneMove : MonoBehaviour
{
	public Vector3 moveDirection;
	public List<float> limits;

	public GameObject tube;
	private AudioSource tubeAudio;
	private float signalTimeout = 0.1f; 
	private float lastSignalTime;
	void Start()
	{
		if (tube != null)
		{
			tubeAudio = tube.GetComponent<AudioSource>();
			if (tubeAudio == null)
				tubeAudio = tube.AddComponent<AudioSource>();
			
		}
	}

	private void Update()
	{
		if (tubeAudio != null && tubeAudio.isPlaying && Time.time - lastSignalTime > signalTimeout)
		{
			tubeAudio.Stop();
		}
	}

	void OnEnable()
	{
		CraneSignals.UniversalSignal += HandleMoveSignal;
	}

	void OnDisable()
	{
		CraneSignals.UniversalSignal -= HandleMoveSignal;
	}

	public void HandleMoveSignal(Vector3 direction, float speed)
	{
		bool flag = false;
		float checkPos = 0;
		if (direction.x == 1)
		{
			checkPos = transform.localPosition.x;
		}
		if (direction.y == 1)
		{
			checkPos = transform.localPosition.y;
		}
		if (direction.z == 1)
		{
			checkPos = transform.localPosition.z;
		}
		if (speed < 0 && checkPos > limits[0] && direction == moveDirection)
		{
			transform.Translate(direction * speed * Time.deltaTime, Space.Self);
			flag = true;

			
		}
		else
		{
			if (speed > 0 && checkPos < limits[1] && direction == moveDirection)
			{
				transform.Translate(direction * speed * Time.deltaTime, Space.Self);
				flag = true;

			}
			
		}
		if (tube != null && flag)
		{
			lastSignalTime = Time.time;
			if (tubeAudio != null && !tubeAudio.isPlaying)
			{
				tubeAudio.Play();
			}

			tube.transform.Rotate(new Vector3(speed * 90 * Time.deltaTime, 0, 0), Space.Self);
		}
		//Debug.Log("движение в сторону " + direction);
	}

}

using UnityEngine;

public class Wire : MonoBehaviour
{
	[Header("Объекты для работы")]
	public GameObject cable;  
	public Transform pointA;   
	public Transform pointB;   

	private Vector3 lastPosA;
	private Vector3 lastPosB;

	void Start()
	{
		if (cable == null || pointA == null || pointB == null)
		{
			Debug.LogError("CableBetweenPoints: не заданы все объекты!");
			enabled = false;
			return;
		}
		lastPosA = pointA.position;
		lastPosB = pointB.position;

		UpdateCable();
	}

	void Update()
	{
		if (pointA.position != lastPosA || pointB.position != lastPosB)
		{
			UpdateCable();
			lastPosA = pointA.position;
			lastPosB = pointB.position;
		}
	}

	private void UpdateCable()
	{
		Vector3 direction = pointB.position - pointA.position;
		float distance = direction.magnitude;

		cable.transform.position = (pointA.position + pointB.position) / 2f;

		cable.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

		Vector3 newScale = cable.transform.localScale;
		newScale.y = distance / 2f; 
		cable.transform.localScale = newScale;

		CapsuleCollider col = cable.GetComponent<CapsuleCollider>();
		if (col != null)
		{
			col.direction = 1; 
			col.height = distance;
		}
	}
}

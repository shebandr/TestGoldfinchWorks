using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GazanDangerZoneDistance : MonoBehaviour
{
    public GameObject zond;
    public string dangerZoneTag = "DangerZone";
    public GameObject screenText;
    private List<GameObject> dangerZonesList = new List<GameObject>();
    void Start()
    {
		GameObject[] objs = GameObject.FindGameObjectsWithTag(dangerZoneTag);
		dangerZonesList.AddRange(objs);
	}
    void Update()
    {

		float minDistance = float.MaxValue;

		foreach (var obj in dangerZonesList)
		{
			if (obj == null) continue;
			float dist = Vector3.Distance(zond.transform.position, obj.transform.position);
			if (dist < minDistance)
				minDistance = dist;
		}
		screenText.GetComponent<TextMeshPro>().text = minDistance.ToString("F2");
	}
}

using UnityEngine;
using HTC.UnityPlugin.Vive;

public class VRGrabber : MonoBehaviour
{
	public HandRole handRole = HandRole.RightHand;
	private LayerMask grabbedLayer = 7;
	private Transform grabbedTransform;

	private void OnTriggerStay(Collider other)
	{
		if (grabbedTransform != null) return;

		Transform candidate = other.transform;
		while (candidate != null && candidate.gameObject.layer != grabbedLayer)
		{
			candidate = candidate.parent;
		}

		if (candidate != null && candidate.gameObject.layer == grabbedLayer)
		{
			if (ViveInput.GetPress(handRole, ControllerButton.Trigger))
			{
				grabbedTransform = candidate;
				grabbedTransform.SetParent(transform, true);
			}
		}
	}

	private void Update()
	{
		if (grabbedTransform != null && !ViveInput.GetPress(handRole, ControllerButton.Trigger))
		{
			grabbedTransform.SetParent(null, true);
			grabbedTransform = null;
		}
	}
}

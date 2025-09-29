using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazanPower : MonoBehaviour
{
	public GameObject screen;
	public Vector3 signal = new Vector3(0, 0, 5);
	public float holdTime = 3f;
	public GameObject screenText;
	public GameObject screenProgressbar;

	public int holdTicksTolerance = 3;

	public Color highlightColor = Color.yellow;
	private Color originalColor;
	private bool isHighlighted = false;
	private float currentHold = 0f;
	private bool isActivated = false;
	private bool isHolding = false;
	private Vector3 barScale;
	private float barStartScale;

	private int ticksWithoutSignal = 0;

	private void Start()
	{
		barScale = screenProgressbar.transform.localScale;
		barStartScale = barScale.x;
		ResetProgressBar();
		if (screenText != null) screenText.SetActive(false);
	}

	void OnEnable()
	{
		CraneSignals.UniversalSignal += HandleMoveSignal;
	}

	void OnDisable()
	{
		CraneSignals.UniversalSignal -= HandleMoveSignal;
	}

	private void Update()
	{
		if (isHolding)
		{
			currentHold += Time.deltaTime;
			ticksWithoutSignal = 0;
		}
		else
		{
			ticksWithoutSignal++;
		}

		float progress = Mathf.Clamp01(currentHold / holdTime);
		barScale.x = barStartScale * progress;
		screenProgressbar.transform.localScale = barScale;

		if (currentHold >= holdTime)
		{
			ToggleActivation();
			ResetProgressBar();
			currentHold = 0f;
		}

		if (ticksWithoutSignal > holdTicksTolerance)
		{
			currentHold = 0f;
			ResetProgressBar();
		}
		isHolding = false;
	}

	public void HandleMoveSignal(Vector3 direction, float speed)
	{
		if (direction == signal)
		{
			isHolding = true;
		}
	}

	private void ToggleActivation()
	{
		isActivated = !isActivated;
		if (screenText != null) screenText.SetActive(isActivated);
		ScreenColorChange();
	}

	private void ResetProgressBar()
	{
		barScale.x = 0;
		screenProgressbar.transform.localScale = barScale;

	}
	public void ScreenColorChange()
	{
		Renderer rend = screen.GetComponent<Renderer>();
		if (rend == null || rend.materials.Length <= 1) return;

		Material[] mats = rend.materials;

		if (!isHighlighted)
			mats[1].color = highlightColor;
		else
			mats[1].color = originalColor;

		rend.materials = mats;
		isHighlighted = !isHighlighted; 
	}
}

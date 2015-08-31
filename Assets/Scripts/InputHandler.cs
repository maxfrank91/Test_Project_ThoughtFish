using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

	public static GameObject draggedObject;

	public enum UIColor {RED, GREEN, BLUE, COUNT};
	public GameObject panel;

	static int minMovement = 10;
	Vector3 startPosition;
	UIColor currentColor;
	bool over = false;
	Color lightBlue = new Color (0.5f, 0.5f, 1);
	Color lightGreen = new Color (0.5f, 1, 0.5f);
	Color lightRed = new Color (1, 0.5f, 0.5f);
	float time;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		startPosition = transform.position;
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if ((startPosition - Input.mousePosition).magnitude > minMovement) 
		{
			draggedObject = gameObject;
			transform.position = Input.mousePosition;
		}

	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		// click
		if (draggedObject == null) 
		{

		}
		draggedObject = null;
	}

	#endregion

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		over = true;
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		panel.SetActive(false);
		over = false;
		time = 0;
	}

	#endregion


	// Use this for initialization
	void Start () 
	{
		changeColor();
		time = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (over)
		{
			time += Time.deltaTime;
			if ( Input.GetMouseButtonUp(1)) 
			{
				currentColor = (UIColor)(((int)currentColor+1) % (int)UIColor.COUNT);
				changeColor();
			}
			if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse Y") < 0 )
			{
				time = 0;
				panel.SetActive(false);
			}
			if (time >= 0.5f) 
			{
				panel.SetActive(true);
			}
		}
	}

	void changeColor()
	{
		ColorBlock tempColors = GetComponent<Button>().colors;
		switch(currentColor) {
		case UIColor.BLUE:
			tempColors.normalColor = Color.blue;
			tempColors.highlightedColor = lightBlue;
			break;
		case UIColor.RED:
			tempColors.normalColor = Color.red;
			tempColors.highlightedColor = lightRed;
			break;
		case UIColor.GREEN:
			tempColors.normalColor = Color.green;
			tempColors.highlightedColor = lightGreen;
			break;
		}
		GetComponent<Button>().colors = tempColors;
	}
}








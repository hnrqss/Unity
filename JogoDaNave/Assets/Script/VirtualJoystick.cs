using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class VirtualJoystick : MonoBehaviour, IDragHandler , IPointerUpHandler , IPointerDownHandler {

	// Use this for initialization
	private Image bgAnalogico;
	private Image analogico;
	private Vector3 inputVector;

	void Start () {
		bgAnalogico = GetComponent<Image>();
		analogico = transform.GetChild(0).GetComponent<Image>();
	}

	public virtual void OnDrag (PointerEventData ped)
	{
		Vector2 pos ;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgAnalogico.rectTransform
		                                                           ,ped.position
		                                                           ,ped.pressEventCamera
		                                                           ,out pos ))
		{
			//Debug.Log("E ai?");
			pos.x = (pos.x / bgAnalogico.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgAnalogico.rectTransform.sizeDelta.y);
			inputVector = new Vector3 ((pos.x*2+1),0,(pos.y*2-1));
			inputVector =  (inputVector.magnitude > 1.0f)? inputVector.normalized : inputVector; 
			//Debug.Log(inputVector);
			// mover o analogico
			analogico.rectTransform.anchoredPosition = new Vector3 ( inputVector.x*(bgAnalogico.rectTransform.sizeDelta.x/2.5f)
			                                                        ,inputVector.z*(bgAnalogico.rectTransform.sizeDelta.y/2.5f));
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{ 
		OnDrag(ped);
	}
	
	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector3.zero;
		analogico.rectTransform.anchoredPosition = Vector3.zero;
	}
	
	public float Horizontal()
	{
		if(inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis("Horizontal");

	}

	public float Vertical()
	{
		if(inputVector.z != 0)
			return inputVector.z;
		else
			return Input.GetAxis("Vertical");
		
	}

}

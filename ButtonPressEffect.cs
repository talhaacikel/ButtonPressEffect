using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPressEffect : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    Button button;
    bool buttonPressed;
    
    [Header("Press Values")]
    public float pressScale = 0.9f; //how much scale downs it when pressed the button
    private Vector3 pressScaleV;
    public float buttonDownSpeed = 7.5f;
    public float buttonUpSpeed = 10f;
    
    [Header("Shake Values")]
    public float shakeSpeed = 45f; //how fast it shakes
    public float shakeAmount = 5f; //how much it shakes
    private Vector3 firstPos;

    void Start()
    {
        firstPos = transform.position;
        button = GetComponent<Button>();
        pressScaleV = new Vector3(pressScale, pressScale, pressScale);
    }

    void Update()
    {   
        if(buttonPressed)
        {
            if(button.interactable)
            {
                transform.localScale = Vector3.Slerp(transform.localScale, pressScaleV, 10f / Time.timeScale * Time.deltaTime);
            }
            else
            {
                transform.position += new Vector3(Mathf.Sin(shakeSpeed * Time.unscaledTime) * shakeAmount,0,0);
            }
        }
        else
        {
            transform.localScale = Vector3.Slerp(transform.localScale, new Vector3(1f, 1f, 1f), buttonDownSpeed / Time.timeScale * Time.deltaTime);
            transform.position =  Vector3.Slerp(transform.position, firstPos, buttonUpSpeed / Time.timeScale * Time.deltaTime);
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }  
}

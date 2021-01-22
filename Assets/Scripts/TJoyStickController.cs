using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TJoyStickController : MonoBehaviour
{
    private Rigidbody myBody;
    public float moveForce = 10f;

    private FixedJoystick joyStick;

    public float timeStart = 5.0f,reverseTimeStart=10.0f;
    public Text timeText,reverseTimeText,reverseKullanimText;
    public bool IsTiming = false;

    public bool Reverse = false;
    public int reverseKullanimHakki=3;
    public Button reverseButton;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        joyStick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
    }
    private void Start()
    {
        timeText.text = timeStart.ToString();
        reverseTimeText.text = reverseTimeStart.ToString();
        reverseKullanimText.text = "Reverse Kullanım Hakkı " + reverseKullanimHakki.ToString();
    }


    void Update()
    {
        if (!Reverse)
        {
            myBody.velocity = new Vector3(joyStick.Vertical * moveForce, myBody.velocity.y, joyStick.Horizontal * moveForce);
        }
        else
        {
            myBody.velocity = new Vector3(joyStick.Vertical * -moveForce, myBody.velocity.y, joyStick.Horizontal *moveForce );
        }
        if (IsTiming)
        {
            timeStart -= Time.deltaTime;
            timeText.text = Mathf.Round(timeStart).ToString();
            if (timeStart <= 0)
            {
                timeStart = 0.0f;
                Debug.Log("Oyun Bitti");
            }
        }
		if (Reverse)
		{
            reverseTimeStart -= Time.deltaTime;
            reverseTimeText.text = Mathf.Round(reverseTimeStart).ToString();
            if (reverseTimeStart <= 0)
            {
                Reverse = false;
                reverseTimeStart = 10.0f;
				if (!Reverse&&reverseKullanimHakki>0)
				{
                    reverseButton.interactable = true;
                }

            }
            
		}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            IsTiming = true;
        }
    }
    public void ReverseButton()
    {
        Reverse = true;
       
		if (Reverse)
		{
            reverseButton.interactable = false;
            reverseKullanimHakki -=1;
            reverseKullanimText.text ="Reverse Kullanım Hakkı "+ reverseKullanimHakki.ToString();
            if (reverseKullanimHakki == 0)
            {
                reverseButton.interactable = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickController : MonoBehaviour
{
    private Rigidbody myBody;
    public float moveForce = 10f;

    private FixedJoystick joyStick;

    public float timeStart = 5.0f;
    public Text timeText;
    public bool IsTiming = false;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        joyStick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
    }
	private void Start()
	{
        timeText.text = timeStart.ToString();
    }


	void Update()
    {
        myBody.velocity = new Vector3(joyStick.Vertical * moveForce, myBody.velocity.y, joyStick.Horizontal* -moveForce);
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            IsTiming = true;
        }
    }
}

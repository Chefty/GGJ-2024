using UnityEngine;

public class SteerDemo : MonoBehaviour
{
    public SteeringAgent agent;
    public float speed;

    public float TurnTime;

    void Update()
    {
        transform.position += agent.steerVector * Time.deltaTime * speed;
        /*
         * get joystick using 
         * agent.JoystickVector;
         * this will give you a non normalized vector (so player can stop)
         */
    }
}

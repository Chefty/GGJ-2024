using UnityEngine;

public class SteerDemo : MonoBehaviour
{
    public SteeringAgent agent;
    public float speed;

    void Update()
    {
        Vector3 newDirectionVector = agent.steerVector;
        transform.position +=  new Vector3(newDirectionVector.x, 0f, newDirectionVector.z) * Time.deltaTime * speed;
        /*
         * get joystick using 
         * agent.JoystickVector;
         * this will give you a non normalized vector (so player can stop)
         */
    }
}

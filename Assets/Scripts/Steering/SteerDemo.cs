using UnityEngine;

public class SteerDemo : MonoBehaviour
{
    public SteeringAgent agent;
    public float speed;

    public float TurnTime;

    void Update()
    {
        agent.UpdateAgent();
        transform.position += agent.steerVector * Time.deltaTime * speed;

    }
}

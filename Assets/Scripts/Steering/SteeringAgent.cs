using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    [SerializeField] private Steer[] _behaviours;
    private Vector3 _steerVector;
    private float _steerMagnitude;
    //if you want the direction the ai wants to go to.
    // TODO may need to multiply with a Quaternion to account for map/agent's rotation
    public Vector2 JoystickVector
    {
        get
        {
            UpdateAgent();
            return new Vector2(Mathf.Clamp(_steerVector.x, -1f, 1f), Mathf.Clamp(_steerVector.z, -1f, 1f));
        }
    }

    [SerializeField] private bool _DebugAgent;

    // unclamped non normalized world space vector
    public Vector3 steerVector
    { 
        get
        {
            UpdateAgent();
            return _steerVector; 
        }
    }
    public float steerMagnitude { get { return _steerMagnitude; } }

    private void Awake()
    {
        for (int i = 0; i < _behaviours.Length; i++)
        {
            _behaviours[i].Initialize(transform);
        }
    }

    public void UpdateAgent()
    {
        Vector3 newSteerVector = Vector3.zero;

        for (int i = 0; i < _behaviours.Length; i++)
        {
            newSteerVector += _behaviours[i].Compute() * _behaviours[i].Weight;
        }

        // do Lerp/Slerp here if need to smooth vectors
        _steerVector = newSteerVector;
        _steerMagnitude = newSteerVector.magnitude;
    }

    private void OnDrawGizmos()
    {
        if (!_DebugAgent || !Application.isPlaying) return;

        for (int i = 0; i < _behaviours.Length; i++)
        {
            _behaviours[i].DebugShow();
        }

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, steerVector);
        
        Gizmos.color = Color.blue;
        var vectorjoystick = JoystickVector;
        Gizmos.DrawRay(transform.position, new Vector3(vectorjoystick.x, 0f, vectorjoystick.y) * 10f);
    }
}

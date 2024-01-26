using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    //[Range(0f, 1f)]
    //[SerializeField] private float _turningSpeed;
    [SerializeField] private Steer[] _behaviours;
    private Vector3 _steerVector;
    private float _steerMagnitude;
    //if you want the direction the ai wants to go to.
    // TODO may need to multiply with a Quaternion to account for camera angle as this
    // vector is world spaced. Same in case the map rotates at some points
    public Vector2 VectorToJoystick
    {
        get
        {
            UpdateAgent();
            return new Vector2(_steerVector.x, _steerVector.z).normalized;
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

        // do slerp here if need to smooth vectors
        _steerVector = newSteerVector;
        _steerMagnitude = newSteerVector.magnitude;
    }

    private void OnDrawGizmos()
    {
        if (!_DebugAgent) return;

        for (int i = 0; i < _behaviours.Length; i++)
        {
            _behaviours[i].DebugShow();
        }
    }
}

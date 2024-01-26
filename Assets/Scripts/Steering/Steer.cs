using UnityEngine;

public abstract class Steer : MonoBehaviour
{
    public float Weight;
    public Vector3 ComputedSteeringVector { get { return _computedSteeringVector; } }

    protected Vector3 _computedSteeringVector;

    protected Transform _agentTransform;

    public virtual void Initialize(Transform agentTransform)
    {
        _agentTransform = agentTransform;
    }

    public abstract Vector3 Compute();

    public abstract void DebugShow();

}

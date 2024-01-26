using UnityEngine;

public class Wander : Steer
{
    [SerializeField] private float _arriveDistance;
    private Vector3 _wanderTarget = Vector3.zero;
    // hardcoded map value, may need an update
    private static Bounds _WanderBound = new Bounds(new Vector3(15f, 0f, -24f), new Vector3(14f, 0.5f, 24f));

    private void SetNewWanderTarget() => _wanderTarget = GetNewWanderTarget();

    public override void Initialize(Transform agentTransform)
    {
        base.Initialize(agentTransform);
        SetNewWanderTarget();
    }

    public override Vector3 Compute()
    {
        if (Vector3.Distance(_wanderTarget, _agentTransform.position) < _arriveDistance) SetNewWanderTarget();

        var vectorToTarget = _wanderTarget - _agentTransform.position;
        _computedSteeringVector = vectorToTarget.normalized;

        return _computedSteeringVector;
    }

    private Vector3 GetNewWanderTarget()
    {
        return new Vector3(Random.Range(_WanderBound.min.x, _WanderBound.max.x), 0f, Random.Range(_WanderBound.min.z, _WanderBound.max.z));
    }


    public override void DebugShow()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_wanderTarget, 1f);
    }
}

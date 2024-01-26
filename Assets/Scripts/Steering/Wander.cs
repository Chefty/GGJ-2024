using UnityEngine;

public class Wander : Steer
{
    [SerializeField] private float _arriveDistance;
    private Vector3 _wanderTarget = Vector3.zero;
    private Bounds _wanderBound;

    private void SetNewWanderTarget() => _wanderTarget = GetNewWanderTarget();

    public override void Initialize(Transform agentTransform)
    {
        base.Initialize(agentTransform);
        _wanderBound = new Bounds();
        _wanderBound.center = new Vector3(15f, 0f, -24f);
        _wanderBound.extents = new Vector3(14f, 0.5f, 24f);
        SetNewWanderTarget();
    }

    public override Vector3 Compute()
    {
        if (Vector3.Distance(_wanderTarget, _agentTransform.position) < _arriveDistance) SetNewWanderTarget();

        var vectorToTarget = _wanderTarget - _agentTransform.position;
        _computedSteeringVector = vectorToTarget;

        return _computedSteeringVector;
    }

    private Vector3 GetNewWanderTarget()
    {
        return new Vector3(Random.Range(_wanderBound.min.x, _wanderBound.max.x), 0f, Random.Range(_wanderBound.min.z, _wanderBound.max.z));
    }


    public override void DebugShow()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_wanderTarget, 1f);
    }
}

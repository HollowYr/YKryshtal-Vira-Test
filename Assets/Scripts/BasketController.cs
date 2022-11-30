using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BasketController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float ballInBasketMoveTime = .5f;
    [SerializeField] private Ease ballEase = Ease.OutBack;
    //[SerializeField] private LineRenderer lr;
    //[SerializeField] private int trajectoryPointsCount = 100;
    //[SerializeField] private TrajectoryPrediction TrajectoryPrediction;
    private Rigidbody2D ball_Rigidbody;
    private Transform ball_Transform;
    private Vector2 startPos, endPos;
    private float distance;
    private bool clicked = false;

    //// Trajectory
    //private Scene parallelScene;
    //private PhysicsScene2D parallelPhysicsScene;

    //public GameObject mainObject;
    //public GameObject plane;

    //private bool mainPhysics = true;
    //void Start()
    //{
    //    Physics2D.simulationMode = SimulationMode2D.Script;
    //    lr.positionCount = trajectoryPointsCount;

    //    CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
    //    parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
    //    parallelPhysicsScene = parallelScene.GetPhysicsScene2D();
    //}

    //void FixedUpdate()
    //{
    //    if (!mainPhysics) return;

    //    SceneManager.GetActiveScene().GetPhysicsScene2D().Simulate(Time.fixedDeltaTime);
    //}
    void Update()
    {
        if (ball_Transform == null || clicked == false) return;

        if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            distance = Vector2.Distance(startPos, endPos);
            Vector2 direction = endPos - startPos;
            RotateInDirection2D(direction);

            direction *= -1;

            TrajectoryPrediction.Instance.SimulatePhysics(ball_Rigidbody, direction * (distance / Screen.height) * 10f);

            //FirstProjection(direction);
            //Debug.Log("Distance: " + distance);
        }
    }

    //private void FirstProjection(Vector2 direction)
    //{
    //    Vector2[] trajectory = Plot(ball_Rigidbody, (Vector2)ball_Transform.position, direction * (distance / Screen.height) * 10f, 500);

    //    lr.positionCount = trajectory.Length;
    //    Vector3[] positions = new Vector3[trajectory.Length];


    //    for (int i = 0; i < positions.Length; i++)
    //    {
    //        positions[i] = trajectory[i];
    //    }
    //    lr.SetPositions(positions);

    //    Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    //    {
    //        Vector2[] results = new Vector2[steps];

    //        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
    //        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

    //        float drag = 1f - timestep * rigidbody.drag;
    //        Vector2 moveStep = velocity * timestep;

    //        for (int i = 0; i < steps; i++)
    //        {
    //            moveStep += gravityAccel;
    //            moveStep *= drag;
    //            pos += moveStep;
    //            results[i] = pos;
    //        }

    //        return results;
    //    }
    //}

    private void RotateInDirection2D(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out ball_Rigidbody) || clicked == true) return;
        ball_Rigidbody.simulated = false;
        ball_Transform = collision.transform;
        ball_Transform.parent = transform;
        ball_Transform.DOLocalMove(Vector3.zero, ballInBasketMoveTime).SetEase(ballEase);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.DoAfterTime(.05f, () => clicked = false);
        TrajectoryPrediction.Instance.EnableMainPhysics(true);

        if (ball_Rigidbody == null || ball_Transform == null) return;
        TrajectoryPrediction.Instance.EnableLineRenderer(false);

        Vector2 direction = startPos - endPos;

        ball_Rigidbody.simulated = true;
        ball_Transform.parent = null;
        ball_Rigidbody.AddForce(direction * (distance / Screen.height) * 10f);

        ball_Transform = null;
        ball_Rigidbody = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ball_Rigidbody == null || ball_Transform == null) return;

        startPos = Input.mousePosition;

        ball_Rigidbody.simulated = true;
        ball_Transform.parent = null;
        TrajectoryPrediction.Instance.EnableLineRenderer(true);
        TrajectoryPrediction.Instance.EnableMainPhysics(false);
        clicked = true;
    }

    //void SimulatePhysics()
    //{
    //    GameObject simulationObject = Instantiate(mainObject);
    //    GameObject simulationPlane = Instantiate(plane);

    //    SceneManager.MoveGameObjectToScene(simulationObject, parallelScene);
    //    SceneManager.MoveGameObjectToScene(simulationPlane, parallelScene);

    //    Vector3 direction = startPos - endPos;
    //    Vector2 force = direction * (distance / Screen.height) * 10f;

    //    Rigidbody2D rigidbody = simulationObject.GetComponent<Rigidbody2D>();
    //    rigidbody.velocity = ball_Rigidbody.velocity;
    //    rigidbody.angularVelocity = ball_Rigidbody.angularVelocity;
    //    rigidbody.simulated = true;
    //    rigidbody.transform.parent = null;
    //    rigidbody.AddForce(force);

    //    for (int i = 0; i < lr.positionCount; i++)
    //    {
    //        parallelPhysicsScene.Simulate(Time.fixedDeltaTime);
    //        lr.SetPosition(i, simulationObject.transform.position);
    //    }
    //    Destroy(simulationObject);
    //    Destroy(simulationPlane);
    //}
}

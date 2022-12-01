using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryPrediction : Singleton<TrajectoryPrediction>
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private int trajectoryPointsCount = 100;
    // Trajectory
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject cameraBorders;

    private Scene parallelScene;
    private PhysicsScene2D parallelPhysicsScene;

    private bool mainPhysics = true;

    void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
        lr.positionCount = trajectoryPointsCount;

        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        parallelPhysicsScene = parallelScene.GetPhysicsScene2D();
    }

    public void EnableMainPhysics(bool isEnabled) => mainPhysics = isEnabled;
    public void EnableLineRenderer(bool isEnabled) => lr.enabled = isEnabled;

    void FixedUpdate()
    {
        if (!mainPhysics) return;

        SceneManager.GetActiveScene().GetPhysicsScene2D().Simulate(Time.fixedDeltaTime);
    }

    public void SimulatePhysics(Rigidbody2D rigidbodyRef, Vector2 force)
    {
        GameObject simulationObject = Instantiate(ball);
        GameObject simulationPlane = Instantiate(cameraBorders);

        Destroy(simulationObject.GetComponent<BallCollision>());

        SceneManager.MoveGameObjectToScene(simulationObject, parallelScene);
        SceneManager.MoveGameObjectToScene(simulationPlane, parallelScene);

        Rigidbody2D rigidbody = simulationObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = rigidbodyRef.velocity;
        rigidbody.angularVelocity = rigidbodyRef.angularVelocity;
        rigidbody.simulated = true;
        rigidbody.transform.parent = null;
        rigidbody.AddForce(force);

        for (int i = 0; i < lr.positionCount; i++)
        {
            parallelPhysicsScene.Simulate(Time.fixedDeltaTime);
            lr.SetPosition(i, simulationObject.transform.position);
        }
        Destroy(simulationObject);
        Destroy(simulationPlane);
    }
}

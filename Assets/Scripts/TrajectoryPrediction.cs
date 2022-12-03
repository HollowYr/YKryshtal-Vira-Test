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

    public void SetBall(GameObject ball) => this.ball = ball;
    public void SetCameraBorders(GameObject cameraBorders) => this.cameraBorders = cameraBorders;
    void Start()
    {
        lr.positionCount = trajectoryPointsCount;

        Physics2D.simulationMode = SimulationMode2D.Script;
        CreateParallelScene();
    }

    public void CreateParallelScene()
    {
        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        if (SceneManager.GetSceneByName("ParallelScene").IsValid())
            parallelScene = SceneManager.GetSceneByName("ParallelScene");
        else
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

        if (simulationObject.TryGetComponent(out BallCollision ballCOl))
            DestroyImmediate(ballCOl);
        //Debug.Log("After deleting" + ballCOl != null);
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

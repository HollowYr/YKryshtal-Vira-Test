using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementSimulator : MonoBehaviour
{
    private Scene parallelScene;
    private PhysicsScene2D parallelPhysicsScene;

    public Vector2 initalVelocity;
    public GameObject mainObject;
    public GameObject plane;
    private LineRenderer lineRenderer;

    private bool mainPhysics = true;

    void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1000;

        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        parallelPhysicsScene = parallelScene.GetPhysicsScene2D();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SimulatePhysics();
            mainPhysics = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mainPhysics = true;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (!mainPhysics) return;
        SceneManager.GetActiveScene().GetPhysicsScene2D().Simulate(Time.fixedDeltaTime);
    }

    void SimulatePhysics()
    {
        GameObject simulationObject = Instantiate(mainObject);
        GameObject simulationPlane = Instantiate(plane);

        SceneManager.MoveGameObjectToScene(simulationObject, parallelScene);
        SceneManager.MoveGameObjectToScene(simulationPlane, parallelScene);

        simulationObject.GetComponent<Rigidbody2D>().velocity = mainObject.GetComponent<Rigidbody2D>().velocity + initalVelocity;
        simulationObject.GetComponent<Rigidbody2D>().angularVelocity = mainObject.GetComponent<Rigidbody2D>().angularVelocity;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            parallelPhysicsScene.Simulate(Time.fixedDeltaTime);
            lineRenderer.SetPosition(i, simulationObject.transform.position);
        }
        Destroy(simulationObject);
        Destroy(simulationPlane);
    }

    void Shoot()
    {
        mainObject.GetComponent<Rigidbody2D>().velocity += initalVelocity;
    }

}
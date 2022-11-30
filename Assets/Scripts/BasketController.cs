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

    private Rigidbody2D ball_Rigidbody;
    private Transform ball_Transform;
    private Vector2 startPos, endPos;
    private float distance;
    private bool clicked = false;
    private float forceClamp = 6f;
    private float clampValue;
    private Vector2 force;
    private void Start()
    {

    }
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
            force = direction * (distance / Screen.height) * 2f;
            clampValue = Screen.height / forceClamp;
            force = force.Clamp(-clampValue, clampValue);
            //force = new Vector2(force.normalized.x * force.x, force.normalized.y * force.y);
            Debug.Log("force: " + force);
            TrajectoryPrediction.Instance.SimulatePhysics(ball_Rigidbody, force);
        }
    }
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

        GameController.Instance.InvokeOnNewBasketScored(transform);

        ball_Rigidbody.simulated = false;
        ball_Rigidbody.velocity = Vector2.zero;
        ball_Rigidbody.angularVelocity = 0;

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
        //Vector2 force = direction * (distance / Screen.height) * 10f;
        //force = force.Clamp(0, Screen.height / forceClamp);
        ball_Rigidbody.AddForce(force);
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
}

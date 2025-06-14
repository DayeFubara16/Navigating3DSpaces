using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class CompassBarElement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform target;
    [SerializeField] private bool useFixDirection = false;
    [SerializeField] private Vector3 fixDirection;

    private CompassBar bar;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        bar = GetComponentInParent<CompassBar>();
    }

    private void Update()
    {
        if ((player == null) || (target == null) || (bar == null))
            return;
        {
            // 1.) calculate the direction from the player to the target 
            Vector3 directionToTarget = target.position - player.position;

            // zeroing out vertical difference to focus on horizontal direction
            directionToTarget.y = 0;
            Vector3 forward = useFixDirection ? fixDirection.normalized : player.forward;
            forward.y = 0;

            // 2.) Calculate the angle between the player's forward direction and the direction from the player 
            float angle = Vector3.SignedAngle (forward, directionToTarget, Vector3.up);

            // 3. Normalize angle to -1 to 1 range
            float mappedAngle = Mathf.Clamp (angle / 180f, -1f, 1f); // angle ranges from -180 to +180

            // 4.Calculate x position based on the bar's width and range
            float barWidth = bar.BarRectTransform.rect.width;
            float xPosition = mappedAngle * (360f / bar.BarRange) * (barWidth / 2f);

            // 5. Apply to UI
            _rectTransform.anchoredPosition = new Vector2 (xPosition, _rectTransform.anchoredPosition.y);


        }


    }
}
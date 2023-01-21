using UnityEngine;

//may have to get moved to camera holder, and have it apply this to both main and FP camera. Because this only
//affects main but the FP remains the same causing the weapons to look like they are floating.
public class CameraMovement : MonoBehaviour
{
    private float timeCounter = 0;
    private float lerpTime;

    private Vector3 originPosition;
    private Vector3 targetPosition;

    void Start()
    {
        originPosition = gameObject.transform.localPosition;
    }

    void Update()
    {
        Vector3 currentLocalPosition = gameObject.transform.localPosition;
        float horizontalInput = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        float vertialInput = Mathf.Abs(Input.GetAxisRaw("Vertical"));

        if (horizontalInput == 0 && vertialInput == 0)
        {
            timeCounter += Time.deltaTime;
            //HeadBob(timeCounter, 0.05f);
        }
        else
        {
            timeCounter += Time.deltaTime * 20f * Mathf.Max(horizontalInput, vertialInput);
            //HeadBob(timeCounter, 0.1f);
        }
        gameObject.transform.localPosition = Vector3.Lerp(currentLocalPosition, targetPosition, lerpTime);
    }

    void HeadBob(float counter, float intensity)
    {
        float x = originPosition.x;
        float y = Mathf.Sin(counter) * intensity * 2;
        float z = originPosition.z;

        lerpTime = Time.deltaTime * 8f;
        targetPosition = originPosition + new Vector3(x, y, z);
    }
}

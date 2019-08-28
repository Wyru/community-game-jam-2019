using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraBehavior : MonoBehaviour
{
    public float turnTime;

    float cTime;
    bool turning;

    Quaternion oldRotation, newRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (turning)
        {
            cTime += Time.deltaTime;
            float progress = Mathf.Clamp01(cTime / turnTime);
            transform.rotation = Quaternion.Lerp(oldRotation, newRotation, progress);
            if (progress == 1)
            {
                turning = false;
                transform.rotation = newRotation;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TurnRight();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TurnLeft();
        }
    }

    public void TurnLeft()
    {
        TurnDirection(-1);
    }

    public void TurnRight()
    {
        TurnDirection(1);
    }

    void TurnDirection(int dir)
    {
        if (!turning)
        {
            oldRotation = transform.rotation;
            newRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90 * dir, 0);
            cTime = 0;
            turning = true;
        }
    }


    [MenuItem("View Direction/Look North")]
    public static void LookNorth()
    {
        LookAngle(0);
    }

    [MenuItem("View Direction/Look East")]
    public static void LookEast()
    {
        LookAngle(90);
    }

    [MenuItem("View Direction/Look South")]
    public static void LookSouth()
    {
        LookAngle(180);
    }

    [MenuItem("View Direction/Look West")]
    public static void LookWest()
    {
        LookAngle(-90);
    }

    static void LookAngle(float angle)
    {
        Transform t = Camera.main.transform;
        if (t != null)
        {
            t.rotation = Quaternion.Euler(0, angle, 0);
        }
        var targetPos = Vector3.zero;
        var sceneView = SceneView.lastActiveSceneView;
        sceneView.LookAtDirect(targetPos, Quaternion.Euler(0, angle, 0));
        sceneView.orthographic = true;
    }
}

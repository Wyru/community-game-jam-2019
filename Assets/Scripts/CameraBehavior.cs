using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(turning){
            cTime += Time.deltaTime;
            float progress = Mathf.Clamp01(cTime / turnTime);
            transform.rotation = Quaternion.Lerp(oldRotation, newRotation, progress);
            if(progress == 1){
                turning = false;
                transform.rotation = newRotation;
            }
        }

        if(Input.GetKeyDown(KeyCode.D)){
            TurnRight();
        }

        if(Input.GetKeyDown(KeyCode.A)){
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
        if(!turning){
            oldRotation = transform.rotation;
            newRotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y + 90 * dir,0);
            cTime = 0;
            turning = true;
        }
    }
}

using UnityEngine;

public class MotorController : MonoBehaviour
{
    [SerializeField] ArticulationBody wheelLeft;
    [SerializeField] ArticulationBody wheelRight;
    [SerializeField] float speedScale = 300f;
    [SerializeField] float forceLimit = 10f;
    [SerializeField] float damping = 2f;

    private static readonly float ToleranceLimit = 0.001f;

    void Start()
    {
    }

    void FixedUpdate()
    {
        float dx = Input.GetAxis ("Horizontal");
        float dy = Input.GetAxis ("Vertical");
        if (Mathf.Abs(dy) >= ToleranceLimit || Mathf.Abs(dx) >= ToleranceLimit)
        {
            float tractionGain = 1f - Mathf.Clamp(Mathf.Abs(dx), 0f, 1f);
            Drive(wheelRight, (-dy * tractionGain + dx) * speedScale);
            Drive(wheelLeft, (-dy * tractionGain - dx) * speedScale);
        }
        else
        {
            Drive(wheelRight, 0f);
            Drive(wheelLeft, 0f);
        }
    }

    void Drive(ArticulationBody body, float speed)
    {
        if (body == null)
            return;

        ArticulationDrive drive = body.xDrive;
        drive.forceLimit = forceLimit;
        drive.damping = damping;
        drive.targetVelocity = speed;
        body.xDrive = drive;
    }
}

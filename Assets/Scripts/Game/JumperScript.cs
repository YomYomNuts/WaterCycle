using UnityEngine;
using System.Collections;

public class JumperScript : MonoBehaviour
{
    #region Public Attributs
    public float RatioPositionJump;
    public AnimationCurve AnimJump;
    public AnimationCurve AnimFall;
    #endregion

    #region Private Attributs
    private LevelScript _LevelScript;
    private Vector3 DiffBetweenLevels = new Vector3(0.0f, 0.0f, 20.0f);
    private Vector3 OffsetRaycastFall;
    private float SizeRay;
    private GameObject CurrentBlock;
    private Vector3 StartPointJump;
    private Vector3 EndPointJump;
    private bool IsJumping;
    private float TimeForJumping;
    private float TimerJumping;
    private bool IsFalling;
    #endregion

    // Use this for initialization
    void Start ()
    {
        _LevelScript = GetComponent<LevelScript>();
        SizeRay = DiffBetweenLevels.z * RatioPositionJump;
        CurrentBlock = null;
        IsJumping = false;
        TimeForJumping = DiffBetweenLevels.z * RatioPositionJump / _LevelScript.SpeedLevel;
        TimerJumping = 0.0f;
        IsFalling = false;
        OffsetRaycastFall = new Vector3(0.0f, -0.6f, DiffBetweenLevels.z * (1 - RatioPositionJump));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (IsJumping || IsFalling)
        {
            TimerJumping += Time.deltaTime;
            float factor = IsJumping ? AnimJump.Evaluate(TimerJumping / TimeForJumping) : AnimFall.Evaluate(TimerJumping / TimeForJumping);
            transform.position = new Vector3(0.0f, StartPointJump.y + (EndPointJump.y - StartPointJump.y) * factor, 0.0f);
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, SizeRay, ~Const.LAYER_LEVEL))
            {
                if (hit.collider != null && CurrentBlock != null)
                {
                    Transform nextBlock = hit.collider.transform;
                    Vector3 diff = new Vector3(0.0f, nextBlock.parent.position.y - CurrentBlock.transform.parent.position.y, 0.0f);
                    StartPointJump = transform.position;
                    EndPointJump = transform.position + diff;
                    IsJumping = true;
                    IsFalling = false;
                    TimerJumping = 0.0f;
                }
            }
            else if (Physics.Raycast(transform.position + OffsetRaycastFall, Vector3.down, out hit, DiffBetweenLevels.z, ~Const.LAYER_LEVEL))
            {
                if (hit.collider != null && CurrentBlock != null)
                {
                    Transform nextBlock = hit.collider.transform;
                    Vector3 diff = new Vector3(0.0f, nextBlock.parent.position.y - CurrentBlock.transform.parent.position.y, 0.0f);
                    if (diff.y < -0.1f)
                    {
                        StartPointJump = transform.position;
                        EndPointJump = transform.position + diff;
                        IsJumping = false;
                        IsFalling = true;
                        TimerJumping = 0.0f;
                    }
                }
            }
        }
    }

    void OnCollisionEnter(Collision parCollision)
    {
        switch (parCollision.gameObject.layer)
        {
            case Const.LAYER_LEVEL:
                CurrentBlock = parCollision.gameObject;
                IsJumping = false;
                IsFalling = false;
                break;
        }
    }
}

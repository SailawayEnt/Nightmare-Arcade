using UnityEngine;

/*The functionality for flying enemies*/

public class Flyer : MonoBehaviour
{

    [Header ("References")] Rigidbody2D _rigidbody2D;
    [SerializeField] GameObject bomb;
    [System.NonSerialized] EnemyBase _enemyBase;
    Transform _lookAtTarget; //If I'm a bomb, I will point to a transform, like the player

    [Header ("Ground Avoidance")]
    [SerializeField] float rayCastWidth = 5;
    [SerializeField] float rayCastOffsetY = 1;
    [SerializeField] LayerMask layerMask; //What will I be looking to avoid?
    RaycastHit2D _rayCastHit;

    [Header ("Flight")]
    [SerializeField] bool avoidGround; //Should I steer away from the ground?
    Vector3 _distanceFromPlayer;
    [SerializeField] float maxSpeedDeviation;
    [SerializeField] float easing = 1; //How intense should we ease when changing speed? The higher the number, the less air control!
    float _bombCounter;
    [SerializeField] float bombCounterMax = 5; //How many seconds before shooting another bomb?
    public float attentionRange; //How far can I see?
    public float lifeSpan; //Keep at zero if you don't want to explode after a certain period of time.
    [System.NonSerialized] float _lifeSpanCounter;
    bool _sawPlayer = false; //Have I seen the player?
    [SerializeField] float speedMultiplier; 
    [System.NonSerialized] public Vector3 Speed;
    [System.NonSerialized] public Vector3 SpeedEased;
    [SerializeField] bool shootsBomb;
    [SerializeField] Vector2 targetOffset = new Vector2(0, 2);

    // Use this for initialization
    void Start()
    {
        _enemyBase = GetComponent<EnemyBase>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_enemyBase.isBomb)
        {
            _lookAtTarget = NewPlayer.Instance.gameObject.transform;
        }

        speedMultiplier += Random.Range(-maxSpeedDeviation, maxSpeedDeviation);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position indicating the attentionRange
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attentionRange);
    }

    // Update is called once per frame
    void Update()
    {
        var position = NewPlayer.Instance.transform.position;
        var transform1 = transform;
        var position1 = transform1.position;
        _distanceFromPlayer.x = (position.x + targetOffset.x) - position1.x;
        _distanceFromPlayer.y = (position.y + targetOffset.y) - position1.y;
        SpeedEased += (Speed - SpeedEased) * (Time.deltaTime * easing);
        position1 += SpeedEased * Time.deltaTime;
        transform1.position = position1;

        if (Mathf.Abs(_distanceFromPlayer.x) <= attentionRange && Mathf.Abs(_distanceFromPlayer.y) <= attentionRange || _lookAtTarget != null)
        {
            _sawPlayer = true;
            Speed.x = (Mathf.Abs(_distanceFromPlayer.x) / _distanceFromPlayer.x) * speedMultiplier;
            Speed.y = (Mathf.Abs(_distanceFromPlayer.y) / _distanceFromPlayer.y) * speedMultiplier;

            if (!NewPlayer.Instance.frozen)
            {
                if (shootsBomb)
                {
                    if (_bombCounter > bombCounterMax)
                    {
                        ShootBomb();
                        _bombCounter = 0;
                    }
                    else
                    {
                        _bombCounter += Time.deltaTime;
                    }
                }
            }
            else
            {
                SpeedEased = Vector3.zero;
            }
        }
        else
        {
            Speed = Vector2.zero;
            if (transform.position.y > (NewPlayer.Instance.transform.position.y + targetOffset.y) && _sawPlayer)
            {
                Speed = new Vector2(0f, -.05f);
            }

        }

        // Check for walls and ground
        if (avoidGround)
        {
            var transform2 = transform;
            var position2 = transform2.position;
            _rayCastHit = Physics2D.Raycast(new Vector2(position2.x, position2.y + rayCastOffsetY), Vector2.right, rayCastWidth, layerMask);
            var transform3 = transform;
            var position3 = transform3.position;
            Debug.DrawRay(new Vector2(position3.x, position3.y + rayCastOffsetY), Vector2.right * rayCastWidth, Color.yellow);

            if (_rayCastHit.collider != null)
            {
                Speed.x = -(Mathf.Abs(Speed.x));

            }

            var transform4 = transform;
            var position4 = transform4.position;
            _rayCastHit = Physics2D.Raycast(new Vector2(position4.x, position4.y + rayCastOffsetY), Vector2.left, rayCastWidth, layerMask);
            var transform5 = transform;
            var position5 = transform5.position;
            Debug.DrawRay(new Vector2(position5.x, position5.y + rayCastOffsetY), Vector2.left * rayCastWidth, Color.blue);

            if (_rayCastHit.collider != null)
            {
                Speed.x = Mathf.Abs(Speed.x);

            }

            var transform6 = transform;
            var position6 = transform6.position;
            _rayCastHit = Physics2D.Raycast(new Vector2(position6.x, position6.y + rayCastOffsetY), Vector2.down, rayCastWidth, layerMask);
            var transform7 = transform;
            var position7 = transform7.position;
            Debug.DrawRay(new Vector2(position7.x, position7.y + rayCastOffsetY), Vector2.down * rayCastWidth, Color.red);

            if (_rayCastHit.collider != null)
            {
                Speed.y = Mathf.Abs(Speed.x);

            }
        }

        if (_lookAtTarget != null)
        {
            LookAt2D();
        }
      
        if (lifeSpan != 0)
        {
            if (_lifeSpanCounter < lifeSpan)
            {
                _lifeSpanCounter += Time.deltaTime;
            }
            else
            {
                _enemyBase.Die();
            }
        }
    }

    void LookAt2D()
    {
        float angle = Mathf.Atan2(SpeedEased.y, SpeedEased.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    void ShootBomb()
    {
        var transform1 = transform;
        var position = transform1.position;
        Instantiate(bomb, new Vector3(position.x, position.y, position.z), Quaternion.identity, null);
    }
}

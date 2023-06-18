using System.Diagnostics.CodeAnalysis;
using Scripts.SOArchitecture;
using UnityEngine;

namespace Scripts.Player
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PlayerController : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("speed");
        private static readonly int AttackRange = Animator.StringToHash("attackRange");

        private Rigidbody _rb;
        private float _movementSpeed;

        [SerializeField] private Transform[] targetArr;
        [SerializeField] private DynamicJoystick joystick;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform playerTransform;

        private Transform target;
        private void Awake() => InitVariables();

        private void InitVariables()
        {
            _rb = GetComponent<Rigidbody>();
            _movementSpeed = Resources.Load<FloatVariable>("PlayerData/PlayerSpeed").value;
            SetTarget(0);
        }

        private void Update() => CalculateAttackState();
        
        private void CalculateAttackState()
        {
            if (target == null)
            {
                SetAttackRange(99);
                return;
            }
    
            var distance = Vector3.Distance(transform.position, target.position);
            SetAttackRange(distance);
        }

        private void SetAttackRange(float distance)
        {
            animator.SetFloat(AttackRange, distance);
        }

        internal void SetTarget(int element)
        {
            target = targetArr[element];
        }

        private void FixedUpdate() => MoveCharacter();

        private void MoveCharacter()
        {
            var speed = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

            animator.SetFloat(Speed, speed.magnitude);

            _rb.velocity = new Vector3(joystick.Horizontal * _movementSpeed, _rb.velocity.y,
                joystick.Vertical * _movementSpeed);

            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                playerTransform.rotation = Quaternion.LookRotation(_rb.velocity);
            }
        }
    }
}
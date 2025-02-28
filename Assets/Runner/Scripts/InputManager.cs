using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A simple Input Manager for a Runner game.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Returns the InputManager.
        /// </summary>
        public static InputManager Instance => s_Instance;
        static InputManager s_Instance;

        [SerializeField]
        float m_InputSensitivity = 1.5f;
        float k_InputSensitivity = 1.5f;

        bool m_HasInput;
        Vector3 m_InputPosition;
        Vector3 m_PreviousInputPosition;

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
        }

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        // 28/02/2025 AI-Tag
        // This was created with assistance from Muse, a Unity Artificial Intelligence product

        void Update()
        {
            if (PlayerController.Instance == null)
            {
                return;
            }

#if UNITY_EDITOR
            m_InputPosition = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.isPressed)
            {
                if (!m_HasInput)
                {
                    m_PreviousInputPosition = m_InputPosition;
                }
                m_HasInput = true;
            }
            else
            {
                m_HasInput = false;
            }
#else
    if (Touch.activeTouches.Count > 0)
    {
        m_InputPosition = Touch.activeTouches[0].screenPosition;

        if (!m_HasInput)
        {
            m_PreviousInputPosition = m_InputPosition;
        }
        
        m_HasInput = true;
    }
    else
    {
        m_HasInput = false;
    }
#endif

            // Handle keyboard input
            HandleKeyboardInput();

            if (m_HasInput)
            {
                float normalizedDeltaPosition = (m_InputPosition.x - m_PreviousInputPosition.x) / Screen.width * m_InputSensitivity;
                PlayerController.Instance.SetDeltaPosition(normalizedDeltaPosition);
            }
            else
            {
                PlayerController.Instance.CancelMouseMovement();
            }

            m_PreviousInputPosition = m_InputPosition;
        }

        void HandleKeyboardInput()
        {
            bool hasKeyboardInput = false;
            float deltaPosition = 0f;

            if (Keyboard.current.leftArrowKey.isPressed)
            {
                deltaPosition = -k_InputSensitivity * Time.deltaTime;
                hasKeyboardInput = true;
            }
            else if (Keyboard.current.rightArrowKey.isPressed)
            {
                deltaPosition = k_InputSensitivity * Time.deltaTime;
                hasKeyboardInput = true;
            }

            if (hasKeyboardInput)
            {
                PlayerController.Instance.SetDeltaPositionKeyboard(deltaPosition);
            }
            else if (!hasKeyboardInput)
            {
                PlayerController.Instance.CancelKeyboardMovement();
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

    public class PlayerHealthBar : MonoBehaviour
    {
        [Tooltip("Image component dispplaying current health")]
        public Image HealthFillImage;

        Health m_PlayerHealth;

        void Start()
        {
            PlayerController m_Player = GameObject.FindObjectOfType<PlayerController>();
            m_PlayerHealth = m_Player.GetComponent<Health>();
        }

        void Update()
        {
            HealthFillImage.fillAmount = m_PlayerHealth.CurrentHealth / m_PlayerHealth.MaxHealth;
        }
    }

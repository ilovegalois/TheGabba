using UnityEngine;
using UnityEngine.UI;

    public class PlayerHealthBar : MonoBehaviour
    {
        [Tooltip("Image component dispplaying current health")]
        public Image HealthFillImage;

        Health m_PlayerHealth;

        void Start()
        {
            PlayerManager m_Player = GameObject.FindObjectOfType<PlayerManager>();
            m_PlayerHealth = m_Player.GetComponent<Health>();
        }

        void Update()
        {
            HealthFillImage.fillAmount = m_PlayerHealth.CurrentHealth / m_PlayerHealth.MaxHealth;
        }
    }

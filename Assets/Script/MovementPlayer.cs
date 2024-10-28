using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    CharacterController m_controller;
    [SerializeField] float m_speed;
    [SerializeField] float m_jump;
    [SerializeField] Transform m_groundChecked;
    [SerializeField] LayerMask m_layerGround;
    float m_gravity;
    float m_rotSpeed = 10f;

    Vector3 m_velocity;
    bool m_isGround;
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_gravity = -9.81f;
    }

    // Update is called once per frame
    void Update()
    {
        m_isGround = Physics.CheckSphere(m_groundChecked.position, 0.2f, m_layerGround);
        if (m_isGround && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }

        float m_moveX = Input.GetAxis("Horizontal");
        float m_moveZ = Input.GetAxis("Vertical");

        Vector3 m_move = new Vector3(m_moveX, m_velocity.y, m_moveZ);
        if (m_move.magnitude > 0.1f)
        {
            // Vector3 m_move = transform.right * m_moveX + transform.forward * m_moveX;
            m_controller.Move(m_move * m_speed * Time.deltaTime);

            Quaternion m_targetRotation = Quaternion.LookRotation(m_move);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_targetRotation, Time.deltaTime * m_rotSpeed);
        }

        // salto
        if (Input.GetKeyDown(KeyCode.Space) && m_isGround)
        {
            m_velocity.y = Mathf.Sqrt(m_jump * -2f * m_gravity);
        }

        //gravedad
        m_velocity.y += m_gravity * Time.deltaTime;
        m_controller.Move(m_velocity * Time.deltaTime);

    }
}

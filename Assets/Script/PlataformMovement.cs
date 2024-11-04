using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMovement : MonoBehaviour {
    [SerializeField] Transform m_firstPosition;
    [SerializeField] Transform m_secondPosition;
    [SerializeField] float m_speed = 1f;
    private Vector3 m_targetPosition;

    private void Start() {
        // Empiezo hacia la segunda posicion a moverme
        m_targetPosition = m_secondPosition.position;
    }

    void Update() {
        // mi plataforma se mueve de forma recta hacia una nueva posicion a base del tiempo que le puse al del tiempo
        transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, m_speed * Time.deltaTime);

        // Cuando mi distancia de donde empezo hacia la otra posicion se cumpla se cambia, haciendo un loop
        if (Vector3.Distance(transform.position, m_targetPosition) < 0.1f) {
            // si mi plataforma esta verdaderamente en la posicion 1 actualmente, entonces si es verdadera
            // se cambiara a la segunda posicion, pero como no se cumple la primera condicion se regresara a la posicion 1 
            m_targetPosition = (m_targetPosition == m_firstPosition.position) ? m_secondPosition.position : m_firstPosition.position;
        }
    }
}



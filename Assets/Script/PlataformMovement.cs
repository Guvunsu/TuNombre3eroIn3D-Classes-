using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMovement : MonoBehaviour
{
    [SerializeField] Transform m_firstUpdate;
    [SerializeField] Transform m_secondUpdate;
    [SerializeField] float m_time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(m_firstUpdate.position, m_secondUpdate.position, Time.deltaTime * m_time);
    }
}

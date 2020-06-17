using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    public Transform m_rotationX;
    public Transform m_rotationZ;
    public Transform m_rotationY;

    public float m_rotationSpeed = 20;
    public float m_deathZoneForRotation = 0.2f;

    public float m_lerpForceZ = 1;
    public float m_lerpForceX = 1;
    public float m_lerpForceY = 1;

    public bool m_inverseRotationX;
    public bool m_inverseRotationZ;
    public bool m_inverseRotationY;

    public bool m_activeRotationX;
    public bool m_activeRotationY;
    public bool m_activeRotationZ;

    [Header(" Accelerometer Debug")]
    [Range(-1, 1)]
    [SerializeField] float m_ZLerpedValue = 0;
    [Range(-1, 1)]
    [SerializeField] float m_XLerpedValue = 0;
    [Range(-1, 1)]
    [SerializeField] float m_YLerpedValue = 0;






    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Vector3 accelerometer = Input.acceleration;
        m_ZLerpedValue = Mathf.Lerp(m_ZLerpedValue, accelerometer.z, Time.deltaTime*m_lerpForceZ);
        m_XLerpedValue = Mathf.Lerp(m_XLerpedValue, accelerometer.x, Time.deltaTime*m_lerpForceX);
        m_YLerpedValue = Mathf.Lerp(m_YLerpedValue, accelerometer.y, Time.deltaTime*m_lerpForceY);
#endif

        if (m_activeRotationZ)
        {
            m_rotationZ.localRotation = Quaternion.Euler(
                new Vector3(m_ZLerpedValue * -75f * (m_inverseRotationZ ? -1f : 1f), 0, 0));
        }

        if (m_activeRotationX)
        {
            m_rotationX.localRotation = Quaternion.Euler(
           new Vector3(0, 0, m_XLerpedValue * -75f * (m_inverseRotationX ? -1f : 1f)));
        }

        if (m_activeRotationY)
        {
                m_rotationY.localRotation = Quaternion.Euler(
             new Vector3(0, m_YLerpedValue * -75f * (m_inverseRotationY ? -1f : 1f), 0));
        }

        //if (Mathf.Abs(m_XLerpedValue) > m_deathZoneForRotation)
        //{

        //    m_rotationCenter.Rotate(new Vector3(-m_XLerpedValue * m_rotationSpeed * (m_inverseRotationX ? -1f : 1f) * Time.deltaTime, 0, 0), Space.Self);
        //}
    }
}

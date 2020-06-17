using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_UseAccelerometer : MonoBehaviour
{
    public Transform m_rotationCenter;
    public Transform m_rotationVertical;
    public float m_rotationSpeed = 20;
    public float m_deathZoneForRotation = 0.2f;

    public float m_lerpForceVertical=1;
    public float m_lerpForceHorizontal=1;

    public bool m_inverseRotationHorizontal;
    public bool m_inverseRotationVertical;

    [Header(" Accelerometer Debug")]
    [Range(-1,1)]
    [SerializeField]float m_verticalLerpedValue=0;
    [Range(-1, 1)]
    [SerializeField] float m_horizontalLerpedValue = 0;






    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Vector3 accelerometer = Input.acceleration;
        m_verticalLerpedValue = Mathf.Lerp(m_verticalLerpedValue, accelerometer.z, Time.deltaTime*m_lerpForceVertical);
        m_horizontalLerpedValue = Mathf.Lerp(m_horizontalLerpedValue, accelerometer.x, Time.deltaTime*m_lerpForceHorizontal);
#endif


        m_rotationVertical.localRotation = Quaternion.Euler(
            new Vector3(m_verticalLerpedValue * -75f * (m_inverseRotationVertical ? -1f : 1f), 0, 0));
        
        if (Mathf.Abs(m_horizontalLerpedValue) > m_deathZoneForRotation)
        {

            m_rotationCenter.Rotate(new Vector3(0, -m_horizontalLerpedValue * m_rotationSpeed * (m_inverseRotationHorizontal ? -1f : 1f)*Time.deltaTime, 0), Space.Self);
        }
    }
}

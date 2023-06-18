using System;
using System.Diagnostics.CodeAnalysis;
using Cinemachine;
using UnityEngine;

namespace Scripts.Camera
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera gameCMCamera;

        public static Action<float, float> CameraShakeAction;

        private void OnEnable()
        {
            CameraShakeAction += ShakeCamera;
        }

        private void OnDisable()
        {
            CameraShakeAction -= ShakeCamera;
        }

        private void ShakeCamera(float intensity, float time) 
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
                gameCMCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        }
        
    }
}
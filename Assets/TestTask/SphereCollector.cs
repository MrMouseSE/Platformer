using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask
{
    [ExecuteInEditMode, ImageEffectAllowedInSceneView]
    public class SphereCollector : MonoBehaviour
    {
        public ComputeShader raymarching;
        public float SpecularPower = 2;
        public float FresnelPower = 2;
        public Color FresnelColor;

        [Space]
        public Slider SpecularSlider;
        public Slider FresnelSlider;
        public Slider FresnelColorSlider;
        public Gradient FresnelColorsGradient;
            
        RenderTexture target;
        Camera cam;
        Light lightSource;
        List<ComputeBuffer> buffersToDispose;

        void Init()
        {
            cam = Camera.current;
            lightSource = FindObjectOfType<Light>();
        }

        private void SetSpecularPower()
        {
            SpecularPower = SpecularSlider.value * 1000;
        }

        private void SetFresnelPower()
        {
            FresnelPower = FresnelSlider.value * 10;
        }

        private void SetFresnelColor()
        {
            FresnelColor = FresnelColorsGradient.Evaluate(FresnelColorSlider.value);
        }

        private void ResetValues()
        {
            SetSpecularPower();
            SetFresnelPower();
            SetFresnelColor();
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Init();
            buffersToDispose = new List<ComputeBuffer>();

            InitRenderTexture();
            CreateScene();
            ResetValues();
            SetParameters();

            raymarching.SetTexture(0, "Source", source);
            raymarching.SetTexture(0, "Destination", target);

            int threadGroupsX = Mathf.CeilToInt(cam.pixelWidth / 8.0f);
            int threadGroupsY = Mathf.CeilToInt(cam.pixelHeight / 8.0f);
            raymarching.Dispatch(0, threadGroupsX, threadGroupsY, 1);

            Graphics.Blit(target, destination);

            foreach (var buffer in buffersToDispose)
            {
                buffer.Dispose();
            }
        }

        void CreateScene()
        {
            List<Shape> allShapes = new List<Shape>(FindObjectsOfType<Shape>());
        
            ShapeData[] shapeData = new ShapeData[allShapes.Count];
            for (int i = 0; i < allShapes.Count; i++)
            {
                var s = allShapes[i];
                Vector3 col = new Vector3(s.colour.r, s.colour.g, s.colour.b);
                shapeData[i] = new ShapeData()
                {
                    position = s.Position,
                    scale = s.Scale, colour = col,
                    blendStrength = s.blendStrength * 3,
                };
            }

            ComputeBuffer shapeBuffer = new ComputeBuffer(shapeData.Length, 40);
            shapeBuffer.SetData(shapeData);
            raymarching.SetBuffer(0, "shapes", shapeBuffer);
            raymarching.SetInt("numShapes", shapeData.Length);

            buffersToDispose.Add(shapeBuffer);
        }

        void SetParameters()
        {
            bool lightIsDirectional = lightSource.type == LightType.Directional;
            raymarching.SetMatrix("_CameraToWorld", cam.cameraToWorldMatrix);
            raymarching.SetMatrix("_CameraInverseProjection", cam.projectionMatrix.inverse);
            raymarching.SetVector("_Light",
                (lightIsDirectional) ? lightSource.transform.forward : lightSource.transform.position);
            raymarching.SetBool("positionLight", !lightIsDirectional);
            raymarching.SetFloat("_SpecPower", SpecularPower);
            raymarching.SetFloat("_FresnelPower", FresnelPower);
            raymarching.SetVector("_FresnelColor", FresnelColor);
        }

        void InitRenderTexture()
        {
            if (target == null || target.width != cam.pixelWidth || target.height != cam.pixelHeight)
            {
                if (target != null)
                {
                    target.Release();
                }

                target = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 0, RenderTextureFormat.ARGBFloat,
                    RenderTextureReadWrite.Linear);
                target.enableRandomWrite = true;
                target.Create();
            }
        }

        struct ShapeData
        {
            public Vector3 position;
            public Vector3 scale;
            public Vector3 colour;
            public float blendStrength;

            public static int GetSize()
            {
                return sizeof(float) * 10 + sizeof(int) * 3;
            }
        }
    }
}
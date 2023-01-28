using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dollars
{
    [RequireComponent(typeof(LiveLinkFace))]
    public class BlendShapesController : MonoBehaviour
    {
        [SerializeField]
        SkinnedMeshRenderer[] m_SkinnedMeshRenderers = {};
        [SerializeField]
        BlendShapeMappings[] m_Mappings = { };
        [Range(0, 100)]
        [SerializeField]
        [Tooltip("Max value a scaled blend shape can reach.")]
        float m_BlendShapeMax = 100f;

        LiveLinkFace livelink;
        Dictionary<int, BlendShapeMappingIndex> mappingindex = new Dictionary<int, BlendShapeMappingIndex>();
        float[] blendShapesScaled;
        float[] init_bs;

        void Start()
        {
            livelink = GetComponent<LiveLinkFace>();

            int blendShapesCount = LiveLinkTrackingData.Names.Length;
            init_bs = new float[blendShapesCount];
            blendShapesScaled = new float[blendShapesCount];
            for (var i = 0; i < blendShapesCount; i++)
            {
                init_bs[i] = 0;
            }
            UpdateBlendShapeIndices();
        }

        void Update()
        {
            InterpolateBlendShapes();
            for (var i = 0; i < m_SkinnedMeshRenderers.Length; i++)

                //foreach (var meshRenderer in m_SkinnedMeshRenderers)
            {
                var mi = mappingindex[i];
                for (var j = 0; j < LiveLinkTrackingData.Names.Length; j++)
                {
                    if (mi.morphs[LiveLinkTrackingData.Names[j]] != -1)
                    {
                        m_SkinnedMeshRenderers[i].SetBlendShapeWeight(mi.morphs[LiveLinkTrackingData.Names[j]], blendShapesScaled[j]);
                    }
                }
            }
        }

        void UpdateBlendShapeIndices()
        {
            int index = 0;
            for (var i = 0; i < m_SkinnedMeshRenderers.Length; i++)

                //foreach (var meshRenderer in m_SkinnedMeshRenderers)
            {
                var mesh = m_SkinnedMeshRenderers[i].sharedMesh;
                BlendShapeMappingIndex indices = new BlendShapeMappingIndex();
                foreach (var mapping in m_Mappings[index].mappings)
                {
                    indices.morphs.Add(mapping.from, mesh.GetBlendShapeIndex(mapping.to));
                }
                mappingindex.Add(i, indices);
                index++;
            }
        }
        public void Calibrate()
        {
            for (var i = 0; i < LiveLinkTrackingData.Names.Length; i++)
            {
                init_bs[i] = livelink.values[LiveLinkTrackingData.Names[i]];
            }
        }

        void InterpolateBlendShapes(bool force = false)
        {
            for (var i = 0; i < LiveLinkTrackingData.Names.Length; i++)
            {
                var blendShapeTarget = ((livelink.values[LiveLinkTrackingData.Names[i]] - init_bs[i]) / (100 - init_bs[i])) * 100;
                blendShapesScaled[i] = Mathf.Min(blendShapeTarget * 100, m_BlendShapeMax);
            }
        }

        void OnValidate()
        {
            if (m_SkinnedMeshRenderers.Length != m_Mappings.Length)
                return;
        }
    }
}

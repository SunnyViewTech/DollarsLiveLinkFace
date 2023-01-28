using System;
using UnityEngine;

namespace Dollars
{
    /// <summary>
    /// Use to hold name remapping for blend shapes.
    /// </summary>
    [Serializable]
    public struct Mapping
    {
        /// <summary>
        /// Name of the blend shape in the skinned mesh.
        /// </summary>
        [SerializeField]
        string m_From;

        /// <summary>
        /// Name of the blend shape in the controller.
        /// </summary>
        [SerializeField]
        string m_To;

        public string from
        {
            set { m_From = value; }
            get { return m_From; }

        }

        public string to
        {
            get { return m_To; }
        }

        public Mapping(string from, string to)
        {
            m_From = from;
            m_To = to;
        }
    }
}

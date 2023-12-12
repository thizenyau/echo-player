using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class TestBodyAnchorScale : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The ARHumanBodyManager which will produce frame events.")]
        ARHumanBodyManager m_HumanBodyManager;

        /// <summary>
        /// Get or set the <c>ARHumanBodyManager</c>.
        /// </summary>
        public ARHumanBodyManager humanBodyManager
        {
            get { return m_HumanBodyManager; }
            set { m_HumanBodyManager = value; }
        }

        [SerializeField]
        Text m_ImageInfo;

        /// <summary>
        /// The UI Text used to display information about the image on screen.
        /// </summary>
        public Text imageInfo
        {
            get { return m_ImageInfo; }
            set { m_ImageInfo = value; }
        }

        void OnEnable()
        {
            Debug.Assert(m_ImageInfo != null, "text field is required");
            m_ImageInfo.enabled = true;

            Debug.Assert(m_HumanBodyManager != null, "Human body manager is required.");
            m_HumanBodyManager.trackablesChanged.AddListener(OnHumanBodiesChanged);
        }

        void OnDisable()
        {
            Debug.Assert(m_ImageInfo != null, "text field is required");
            m_ImageInfo.enabled = false;

            Debug.Assert(m_HumanBodyManager != null, "Human body manager is required.");
            m_HumanBodyManager.trackablesChanged.RemoveListener(OnHumanBodiesChanged);
        }

        void OnHumanBodiesChanged(ARTrackablesChangedEventArgs<ARHumanBody> eventArgs)
        {
            // Currently, the ARKit provider only ever produces one body anchor, so just reference the first
            float scale = ((eventArgs.added.Count > 0) ? eventArgs.added.First().estimatedHeightScaleFactor
                        : ((eventArgs.updated.Count > 0) ? eventArgs.updated.First().estimatedHeightScaleFactor
                            : Single.NaN));

            Debug.Assert(m_ImageInfo != null, "text field is required");
            m_ImageInfo.text = scale.ToString("F10");
        }
    }
}
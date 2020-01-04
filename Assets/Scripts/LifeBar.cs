using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MLBoxing
{
    public class LifeBar : MonoBehaviour
    {

        public Boxer boxer;
        private float originalHeight;
        private float lifePercentage = 0f;
        private float width;
        private Vector2 currentAnchoredPos;
        // Use this for initialization
        void Start()
        {
            originalHeight = this.GetComponent<RectTransform>().sizeDelta.y;
            width = this.GetComponent<RectTransform>().sizeDelta.x;
            this.currentAnchoredPos = this.GetComponent<RectTransform>().anchoredPosition;
            if (boxer != null)
            {
                lifePercentage = boxer.m_Life / Boxer.MAX_LIFE;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Update the life percentage
            lifePercentage = boxer.m_Life / Boxer.MAX_LIFE;
            // Resize the life bar
            float adjustedHeight = originalHeight * lifePercentage;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(width, adjustedHeight);
            // Reposition the bar
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentAnchoredPos.x, adjustedHeight / 2);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ShortScript
{
    class TextMeshRenderLayer : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Renderer>().sortingLayerName = transform.parent.GetComponent<Renderer>().sortingLayerName;
            GetComponent<Renderer>().sortingOrder = transform.parent.GetComponent<Renderer>().sortingOrder + 1;
        }
    }
}

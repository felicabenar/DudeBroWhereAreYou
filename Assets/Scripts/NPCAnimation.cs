using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPCs
{
    public class NPCAnimation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Autoplay random animation clips")] 
        private bool AutoPlayAnimations = true;
        public string CurrentPaletteName { get; private set; }

        private AnimationClip[] myClips;
        private Animator animator;
        public const string people_pal_prefix = "people_pal";
        private List<Renderer> _paletteMeshes;

        private void Awake()
        {
            var AllRenderers = gameObject.GetComponentsInChildren<Renderer>();
            _paletteMeshes = new List<Renderer>();
            foreach (Renderer r in AllRenderers)
            {
                var matName = r.sharedMaterial.name;
                var len = Math.Min( people_pal_prefix.Length, matName.Length);
                if (matName[0..len] == people_pal_prefix)
                {
                    _paletteMeshes.Add(r);
                }
            }
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            if (animator != null)
            {
                myClips = animator.runtimeAnimatorController.animationClips;
                if (AutoPlayAnimations)
                {
                    PlayAnyClip();
                    StartCoroutine(ShuffleClips());
                }
            }

            if (AutoPlayAnimations)
            {
                //collider for detect clicks near the character
                CapsuleCollider collider =  gameObject.AddComponent<CapsuleCollider>();
                //average character dimentions
                collider.center = new Vector3(0f, 0.8f, 0f);
                collider.radius = 0.3f;
                collider.height = 1.77f;
                collider.direction = 1;
            }

        }

        /*public void SetPalette(Material mat)
        {
            if (mat != null)
            {
                if (mat.name[0..people_pal_prefix.Length] == CityPeople.people_pal_prefix)
                {
                    CurrentPaletteName = mat.name;
                    foreach (Renderer r in _paletteMeshes)
                    {
                        r.material = mat;
                    }
                } else
                {
                    Debug.Log("Material name should start with 'palete_pal...' by convention.");
                } 
            }
        }*/

        public void PlayAnyClip()
        {
            var cl = myClips[Random.Range(0, myClips.Length)];
            animator.CrossFadeInFixedTime(cl.name, 1.0f, -1, Random.value * cl.length);
        }

        IEnumerator ShuffleClips()
        {
            while (true)
            {
                yield return new WaitForSeconds(15.0f + Random.value * 5.0f);
                PlayAnyClip();
            }
        }

    }
}

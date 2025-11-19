using UnityEngine;

namespace JeroManyMods.Patches.LootHighlighter.Components
{
    /// <summary>
    /// Componente que faz labels sempre olharem para a câmera.
    /// Usado para manter os labels de texto sempre visíveis ao jogador.
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        private Transform _cameraTransform;

        void Start()
        {
            // Cache main camera reference for better performance
            if (Camera.main != null)
            {
                _cameraTransform = Camera.main.transform;
            }
        }

        void Update()
        {
            // Use cached camera transform if available, otherwise try to get it again
            if (_cameraTransform == null)
            {
                if (Camera.main != null)
                {
                    _cameraTransform = Camera.main.transform;
                }
                return;
            }
            transform.rotation = Quaternion.LookRotation(transform.position - _cameraTransform.position);
        }
    }
}


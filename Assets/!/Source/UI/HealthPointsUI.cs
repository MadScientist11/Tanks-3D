using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthPointsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthPointsText;
        [SerializeField] private EntityProvider _entityProvider;


        private void Update()
        {
            transform.LookAt(Camera.main.transform);

            if (_entityProvider != null && !_entityProvider.Entity.IsNullOrDisposed())
            {
            }
        }
    }
}
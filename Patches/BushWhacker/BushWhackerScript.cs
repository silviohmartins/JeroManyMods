using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFT.Interactive;
using UnityEngine;

namespace JeroManyMods.Patches.BushWhacker
{
    public class BushWhackerScript : MonoBehaviour
    {
        private List<ObstacleCollider> Bushes;
        private List<BoxCollider> Swamps;
        private bool ReadyToEdit = false;

        private void Awake()
        {
            GetObjects();
        }

        public void StartTask()
        {
            _ = ChangeObjects();
        }

        private void GetObjects()
        {
            MainJeroManyMods.Logger.LogInfo("[BushWhacker] Starting to find objects...");
            Bushes = FindObjectsOfType<ObstacleCollider>().ToList();
            Swamps = FindObjectsOfType<BoxCollider>().ToList();
            MainJeroManyMods.Logger.LogInfo($"[BushWhacker] Found {Bushes?.Count ?? 0} bushes and {Swamps?.Count ?? 0} swamps.");
            ReadyToEdit = true;
        }

        public async Task ChangeObjects()
        {
            int timeout = 0;
            const int maxTimeout = 20; // 10 segundos máximo (20 * 500ms)
            
            while (!ReadyToEdit && timeout < maxTimeout)
            {
                await Task.Delay(500);
                timeout++;
            }

            if (!ReadyToEdit)
            {
                MainJeroManyMods.Logger.LogError("[BushWhacker] Timeout waiting for objects to load.");
                return;
            }

            bool shouldDisable = MainJeroManyMods.BushWhacker.Value;
            int processedSwamps = 0;
            int processedBushes = 0;

            if (Swamps != null)
            {
                foreach (var swamp in Swamps)
                {
                    if (swamp != null && swamp.name == "Swamp_collider")
                    {
                        swamp.SetEnabledUniversal(!shouldDisable);
                        processedSwamps++;
                    }
                }
            }

            if (Bushes != null)
            {
                foreach (var bushesItem in Bushes)
                {
                    if (bushesItem == null) continue;

                    var filbert = bushesItem?.transform?.parent?.gameObject?.name.ToLower().Contains("filbert");
                    var fibert = bushesItem?.transform?.parent?.gameObject?.name.ToLower().Contains("fibert");
                    var fibert2 = bushesItem?.transform?.name.ToLower().Contains("fibert");
                    var filbert2 = bushesItem?.transform?.name.ToLower().Contains("filbert");
                    var swamp = bushesItem?.transform?.name.ToLower().Contains("swamp");

                    if (filbert == true || fibert == true)
                    {
                        bushesItem.SetEnabledUniversal(!shouldDisable);
                        processedBushes++;
                    }

                    if (filbert2 == true || fibert2 == true || swamp == true)
                    {
                        bushesItem.gameObject.SetActive(!shouldDisable);
                        processedBushes++;
                    }
                }
            }

            MainJeroManyMods.Logger.LogInfo($"[BushWhacker] Processed {processedSwamps} swamps and {processedBushes} bushes. Disable: {shouldDisable}");
        }
    }
}
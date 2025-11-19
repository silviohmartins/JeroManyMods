using System.Collections.Generic;
using BepInEx.Logging;
using EFT;
using EFT.Interactive;
using JeroManyMods.Config;
using JeroManyMods.Managers;
using UnityEngine;

namespace JeroManyMods.Patches.LootHighlighter.Managers
{
    /// <summary>
    /// Gerencia a detecção de loot próximo ao jogador.
    /// Detecta itens, containers e corpos dentro de um raio configurável.
    /// </summary>
    public class LootDetectionManager : BaseManager
    {
        /// <summary>
        /// Informações sobre um item de loot detectado
        /// </summary>
        public class LootInfo
        {
            public GameObject GameObject;
            public string DisplayName;
            public int ObjectId;
            public string Category;
            public Vector3 Position;

            public LootInfo(GameObject gameObject, string displayName, int objectId, string category, Vector3 position)
            {
                GameObject = gameObject;
                DisplayName = displayName;
                ObjectId = objectId;
                Category = category;
                Position = position;
            }
        }

        public LootDetectionManager(ManualLogSource logger) : base(logger)
        {
        }

        /// <summary>
        /// Detecta loot próximo à posição do jogador
        /// </summary>
        /// <param name="playerPos">Posição do jogador</param>
        /// <param name="detectionRadius">Raio de detecção em metros</param>
        /// <param name="config">Configurações do mod</param>
        /// <returns>Lista de loot detectado</returns>
        public List<LootInfo> DetectNearbyLoot(Vector3 playerPos, float detectionRadius, LootHighlighterConfig config)
        {
            List<LootInfo> detectedLoot = new List<LootInfo>();

            Collider[] colliders = Physics.OverlapSphere(playerPos, detectionRadius);

            foreach (var collider in colliders)
            {
                if (collider == null || collider.gameObject == null)
                    continue;

                // Skip objects in inventories or UI
                if (IsInPlayerInventory(collider.gameObject))
                    continue;

                int objectId = collider.gameObject.GetInstanceID();

                // Check for items
                var lootItem = collider.GetComponent<LootItem>();
                if (lootItem != null && config.ShowItems.Value)
                {
                    string displayName = lootItem.Item?.Template?.Name?.Localized() ?? "Item";
                    if (config.ShowDistanceInLabel.Value)
                    {
                        float distance = Vector3.Distance(playerPos, collider.transform.position);
                        displayName += $" ({distance:F1}m)";
                    }
                    detectedLoot.Add(new LootInfo(lootItem.gameObject, displayName, objectId, "Items", collider.transform.position));
                    continue;
                }

                // Check for corpses
                var corpse = collider.GetComponent<ObservedCorpse>() ?? collider.GetComponent<Corpse>();
                if (corpse != null && config.ShowCorpses.Value)
                {
                    string displayName = "Corpse";
                    if (config.ShowDistanceInLabel.Value)
                    {
                        float distance = Vector3.Distance(playerPos, corpse.transform.position);
                        displayName += $" ({distance:F1}m)";
                    }
                    detectedLoot.Add(new LootInfo(corpse.gameObject, displayName, objectId, "Corpses", corpse.transform.position));
                    continue;
                }

                // Check for containers
                var container = collider.GetComponent<LootableContainer>();
                if (container != null && config.ShowContainers.Value)
                {
                    string containerName = container.gameObject.name.ToLower();
                    string containerType = DetermineContainerType(containerName);
                    string displayName = $"Container ({containerType})";

                    if (config.ShowDistanceInLabel.Value)
                    {
                        float distance = Vector3.Distance(playerPos, container.transform.position);
                        displayName += $" ({distance:F1}m)";
                    }
                    detectedLoot.Add(new LootInfo(container.gameObject, displayName, objectId, "Containers", container.transform.position));
                    continue;
                }
            }

            return detectedLoot;
        }

        private string DetermineContainerType(string containerName)
        {
            if (containerName.Contains("med"))
                return "Medical";
            else if (containerName.Contains("tech"))
                return "Tech";
            else if (containerName.Contains("ammo"))
                return "Ammo";
            else if (containerName.Contains("weapon"))
                return "Weapon";
            else if (containerName.Contains("tool"))
                return "Toolbox";
            else if (containerName.Contains("duffle"))
                return "Duffle Bag";
            else if (containerName.Contains("jacket"))
                return "Jacket";
            else if (containerName.Contains("cash"))
                return "Cash";
            else if (containerName.Contains("safe"))
                return "Safe";
            else if (containerName.Contains("drawer"))
                return "Drawer";
            else if (containerName.Contains("pc") || containerName.Contains("computer"))
                return "Computer";

            return "Unknown";
        }

        private bool IsInPlayerInventory(GameObject obj)
        {
            // Check if the object is in player inventory
            return obj.transform.root.name.Contains("Inventory") ||
                   obj.transform.root.name.Contains("UI") ||
                   obj.activeInHierarchy == false;
        }
    }
}


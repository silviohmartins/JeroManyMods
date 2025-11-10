using System.Collections.Generic;
using System.Linq;
using BepInEx.Logging;
using SPT.Reflection.Patching;

namespace JeroManyMods.Managers
{
    /// <summary>
    /// Gerencia o registro e habilitação de todos os patches do mod.
    /// Centraliza a lógica de inicialização de patches para facilitar manutenção.
    /// </summary>
    public class PatchManager : BaseManager
    {
        private readonly List<ModulePatch> _enabledPatches;

        /// <summary>
        /// Inicializa uma nova instância do PatchManager
        /// </summary>
        /// <param name="logger">Logger para mensagens de log</param>
        public PatchManager(ManualLogSource logger) : base(logger)
        {
            _enabledPatches = new List<ModulePatch>();
        }

        /// <summary>
        /// Registra e habilita um patch
        /// </summary>
        /// <param name="patch">Patch a ser habilitado</param>
        /// <param name="patchName">Nome do patch para logging (opcional)</param>
        public void EnablePatch(ModulePatch patch, string patchName = null)
        {
            if (patch == null)
            {
                Logger.LogWarning($"PatchManager: Tentativa de habilitar patch nulo. Nome: {patchName ?? "Unknown"}");
                return;
            }

            try
            {
                patch.Enable();
                _enabledPatches.Add(patch);
                
                string name = patchName ?? patch.GetType().Name;
                Logger.LogInfo($"PatchManager: Patch '{name}' habilitado com sucesso");
            }
            catch (System.Exception ex)
            {
                string name = patchName ?? patch.GetType().Name;
                Logger.LogError($"PatchManager: Erro ao habilitar patch '{name}': {ex.Message}");
            }
        }

        /// <summary>
        /// Habilita múltiplos patches de uma vez
        /// </summary>
        /// <param name="patches">Lista de patches com seus nomes</param>
        public void EnablePatches(params (ModulePatch patch, string name)[] patches)
        {
            foreach (var (patch, name) in patches)
            {
                EnablePatch(patch, name);
            }
        }

        /// <summary>
        /// Obtém a lista de patches habilitados
        /// </summary>
        /// <returns>Lista de patches habilitados</returns>
        public IReadOnlyList<ModulePatch> GetEnabledPatches()
        {
            return _enabledPatches.AsReadOnly();
        }

        /// <summary>
        /// Limpa todos os patches registrados
        /// </summary>
        public override void Cleanup()
        {
            Logger.LogInfo($"PatchManager: Limpando {_enabledPatches.Count} patches");
            _enabledPatches.Clear();
        }
    }
}


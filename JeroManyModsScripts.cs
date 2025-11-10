
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using EFT.UI;
using UnityEngine;
using JeroManyMods.Patches.EnvironmentEnjoyer;
using JeroManyMods.Patches.BushWhacker;

namespace JeroManyMods
{
    /// <summary>
    /// Gerencia scripts que precisam ser executados durante a raid (Easy Mode).
    /// Este componente é adicionado ao GameWorld quando uma raid inicia e inicializa
    /// os scripts do EnvironmentEnjoyer e BushWhacker.
    /// </summary>
    public class JeroManyModsScripts : MonoBehaviour
    {
        private GameWorld _gameWorld;
        private Player _player;
        public Patches.EnvironmentEnjoyer.EnvironmentEnjoyerScript _environmentEnjoyerScript;
        public Patches.BushWhacker.BushWhackerScript _bushWhackerScript;
        private System.EventHandler _environmentEnjoyerEventHandler;
        private System.EventHandler _bushWhackerEventHandler;

        private void Awake()
        {
            _gameWorld = Singleton<GameWorld>.Instance;

            if (_gameWorld == null)
            {
                ConsoleScreen.LogError("[JeroManyMods] GameWorld not found.");
                Destroy(this);
                return;
            }
            _player = _gameWorld.MainPlayer;

            if (_player == null)
            {
                ConsoleScreen.LogError("[JeroManyMods] Player not found.");
                Destroy(this);
                return;
            }

            if (_player.Location.ToLower() == "hideout")
            {
                ConsoleScreen.LogError("[JeroManyMods] Hideout not supported.");
                Destroy(this);
                return;
            }

            SetupMegaModScripts();
            SetupMegaModEvents();
            RunFirstTime();
        }

        private void SetupMegaModScripts()
        {
            MainJeroManyMods.Logger.LogInfo("[JeroManyMods] Setting up scripts...");
            _environmentEnjoyerScript = _gameWorld.gameObject.AddComponent<EnvironmentEnjoyerScript>();
            _bushWhackerScript = _gameWorld.gameObject.AddComponent<BushWhackerScript>();
            MainJeroManyMods.Logger.LogInfo("[JeroManyMods] Scripts added successfully.");
        }

        private async Task RunFirstTime()
        {
            MainJeroManyMods.Logger.LogInfo("[JeroManyMods] Running first time initialization...");
            _environmentEnjoyerScript?.StartTask();
            _bushWhackerScript?.StartTask();
            MainJeroManyMods.Logger.LogInfo("[JeroManyMods] First time initialization completed.");
        }

        private void SetupMegaModEvents()
        {
            _environmentEnjoyerEventHandler = (a, b) => _environmentEnjoyerScript?.StartTask();
            MainJeroManyMods.EnvironmentEnjoyer.SettingChanged += _environmentEnjoyerEventHandler;
            
            _bushWhackerEventHandler = (a, b) => _bushWhackerScript?.StartTask();
            MainJeroManyMods.BushWhacker.SettingChanged += _bushWhackerEventHandler;
        }

        private void OnDestroy()
        {
            if (_environmentEnjoyerEventHandler != null)
            {
                MainJeroManyMods.EnvironmentEnjoyer.SettingChanged -= _environmentEnjoyerEventHandler;
            }
            if (_bushWhackerEventHandler != null)
            {
                MainJeroManyMods.BushWhacker.SettingChanged -= _bushWhackerEventHandler;
            }
        }
    }
}
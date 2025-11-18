using System;
using System.Reflection;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;

namespace JeroManyMods.Patches.ContinuousLoadAmmo.Utils
{
    internal static class MultiSelectInterop
    {
        private static readonly Version _requiredVersion = new Version(5, 0);

        private static bool? _uiFixesLoaded;
        private static Func<object> _loadUnloadSerializerGetter;
        private static AccessTools.FieldRef<object, TaskCompletionSource<object>> _totalTaskField;
        private static MethodInfo _stopLoadingMethod;

        public static bool IsMultiSelectLoadSerializerActive
        {
            get
            {
                if (!Loaded()) return false;

                var serializer = _loadUnloadSerializerGetter?.Invoke();
                if (serializer == null) return false;

                return !_totalTaskField(serializer).Task.IsCompleted;
            }
        }

        public static MethodInfo StopLoadingMethod => Loaded() ? _stopLoadingMethod : null;

        private static bool Loaded()
        {
            if (_uiFixesLoaded.HasValue) return _uiFixesLoaded.Value;

            bool present = Chainloader.PluginInfos.TryGetValue("Tyfon.UIFixes", out PluginInfo pluginInfo);
            _uiFixesLoaded = present && pluginInfo.Metadata.Version >= _requiredVersion;

            if (!_uiFixesLoaded.Value) return _uiFixesLoaded.Value;

            var multiSelectType = Type.GetType("UIFixes.MultiSelect, Tyfon.UIFixes");
            if (multiSelectType != null)
            {
                var loadUnloadSerializerMethod = AccessTools.PropertyGetter(multiSelectType, "LoadUnloadSerializer");
                _loadUnloadSerializerGetter = AccessTools.MethodDelegate<Func<object>>(loadUnloadSerializerMethod);
                _stopLoadingMethod = AccessTools.Method(multiSelectType, "StopLoading");
            }

            var taskSerializerType = Type.GetType("UIFixes.MultiSelectItemContextTaskSerializer, Tyfon.UIFixes");
            if (taskSerializerType != null)
            {
                _totalTaskField = AccessTools.FieldRefAccess<TaskCompletionSource<object>>(taskSerializerType, "totalTask");
            }

            return _uiFixesLoaded.Value;
        }
    }
}


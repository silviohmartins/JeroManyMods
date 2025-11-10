using BepInEx.Logging;

namespace JeroManyMods.Managers
{
    /// <summary>
    /// Classe base abstrata para todos os managers do mod.
    /// Fornece funcionalidade comum como logging e inicialização.
    /// </summary>
    public abstract class BaseManager
    {
        /// <summary>
        /// Logger para mensagens de log do manager
        /// </summary>
        protected ManualLogSource Logger { get; }

        /// <summary>
        /// Inicializa uma nova instância do BaseManager
        /// </summary>
        /// <param name="logger">Logger para mensagens de log</param>
        protected BaseManager(ManualLogSource logger)
        {
            Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Método chamado a cada frame no Update() do plugin principal.
        /// Implemente a lógica específica do manager aqui.
        /// </summary>
        public virtual void Update()
        {
            // Implementação padrão vazia - sobrescreva nas classes derivadas
        }

        /// <summary>
        /// Método chamado quando o manager deve ser inicializado.
        /// Use para configurar estado inicial ou recursos.
        /// </summary>
        public virtual void Initialize()
        {
            // Implementação padrão vazia - sobrescreva nas classes derivadas
        }

        /// <summary>
        /// Método chamado quando o manager deve ser limpo/desabilitado.
        /// Use para liberar recursos ou desregistrar eventos.
        /// </summary>
        public virtual void Cleanup()
        {
            // Implementação padrão vazia - sobrescreva nas classes derivadas
        }
    }
}


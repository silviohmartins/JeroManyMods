# JeroManyMods

Um mod unificado para SPT (Single Player Tarkov) que combina múltiplas funcionalidades de qualidade de vida em um único plugin BepInEx.

## 📋 Descrição

JeroManyMods é uma coleção integrada de mods populares para SPT, oferecendo uma experiência de jogo melhorada com diversas funcionalidades de conveniência. Todas as funcionalidades podem ser configuradas individualmente através do menu de configuração do BepInEx.

## ✨ Funcionalidades

### 1. Easy Mode (Modo Fácil)

Dois modos que facilitam a navegação no jogo:

- **EnvironmentEnjoyer**: Remove árvores e arbustos do ambiente, melhorando a visibilidade.
- **BushWhacker**: Remove arbustos e pântanos, facilitando o movimento pelo mapa.

**Configuração**: Ative/desative cada funcionalidade individualmente no menu de configuração.

---

### 2. Skipper

Permite pular objetivos de quest diretamente da interface de quests, facilitando o progresso nas missões.

**Configurações**:
- **Enabled**: Ativa/desativa o mod globalmente
- **Always display Skip button**: Se habilitado, o botão Skip sempre estará visível
- **Display hotkey**: Tecla que, quando pressionada, faz os botões Skip aparecerem (padrão: Left Control)

---

### 3. Trader Scrolling

Permite a rolagem lateral dos comerciantes.

**Configurações**:
- **Scroll wheel speed**: Ajusta a velocidade de rolagem com a roda do mouse (padrão: 30)

---

### 4. VisorEffectManager

Remove efeitos visuais indesejados dos visores (face shields), melhorando a visibilidade durante o combate.

**Efeitos removíveis**:
- **Glass Damage**: Remove danos no vidro do visor
- **Scratches**: Remove arranhões
- **Blur**: Remove desfoque
- **Distortion**: Remove distorção

**Configurações**:
- Cada efeito pode ser ativado/desativado individualmente
- **Hotkeys**: Teclas de atalho para alternar cada efeito em tempo real durante a raid:
  - Glass Damage: `1` + `Right Control`
  - Scratches: `2` + `Right Control`
  - Blur: `3` + `Right Control`
  - Distortion: `4` + `Right Control`

---

### 5. ContinuousLoadAmmo

Permite carregar munição em pentes continuamente, mesmo fora do inventário, sem precisar abrir a interface de inventário repetidamente.

**Funcionalidades**:
- Carregamento contínuo de munição em pentes
- Funciona mesmo com o inventário fechado
- Limita a velocidade do jogador durante o carregamento
- Suporta múltiplas abas do inventário sem interromper o carregamento

**Funcionalidades Avançadas**:
- **Quick Load**: Carregue munição rapidamente fora do inventário usando a hotkey
- **Seletor de Munição**: Segure a hotkey e use a roda do mouse para escolher qual munição carregar
- **Notificações**: Receba notificações sobre qual munição está sendo carregada (opcional)
- **Compatibilidade MultiSelect**: Integração com UIFixes para carregar múltiplos pentes

**Configurações**:
- **Speed Limit**: Limite de velocidade durante o carregamento (padrão: 31% da velocidade de caminhada)
- **Reachable Places Only**: Permite carregar apenas quando o pente e munição estão no colete, bolsos ou secure container
- **Inventory Tabs**: Não interrompe o carregamento ao trocar de abas do inventário (mapas, tasks, etc.)
- **Quick Load Hotkey**: Tecla para iniciar o carregamento rápido (padrão: `K`)
- **Prioritize Highest Penetration**: Prioriza munição com maior poder de penetração ao usar Quick Load. Se desabilitado, prioriza a mesma munição do pente atual
- **Quick Load Notify**: Exibe notificação quando usa Quick Load (padrão: `true`)

**Controles**:
- Pressione a **Quick Load Hotkey** para carregar automaticamente a melhor munição disponível
- Segure a **Quick Load Hotkey** + **Roda do Mouse** para abrir o seletor de munição
- **Mouse Buttons** (esquerdo/direito) ou **Atirar** cancelam o carregamento

---

### 6. ContinuousHealing

Permite curar múltiplas partes do corpo continuamente sem precisar iniciar a cura manualmente para cada parte.

**Funcionalidades**:
- Cura contínua de múltiplos membros
- Suporta kits de cirurgia (surgery kits)
- Configurável para resetar animações entre curas

**Configurações**:
- **Heal Limbs**: Se kits de cirurgia também devem ser contínuos (nota: animação não faz loop)
- **Heal Delay**: Delay entre cada cura em cada membro (padrão: 10, padrão do jogo: 2, 0 para comportamento contínuo ideal)
- **Reset Animation**: Se uma nova animação deve ser reproduzida entre cada membro curado

---

## 🚀 Instalação

1. Compilar o projeto
2. Copie o arquivo `JeroManyMods.dll` para a pasta `BepInEx/plugins/`
3. Inicie o jogo e configure as opções através do menu de configuração do BepInEx (F12 no menu principal)

## ⚙️ Requisitos

- SPT 4.0.2 ou superior
- BepInEx 5.x
- .NET Standard 2.1

## 📝 Notas

- Todas as funcionalidades podem ser ativadas/desativadas individualmente
- As configurações são salvas automaticamente
- Algumas funcionalidades requerem reiniciar o jogo ou reabrir a interface para aplicar mudanças

---

## 🙏 Créditos e Referências

Este mod integra funcionalidades baseadas nos seguintes mods originais:

### BushWhacker e EnvironmentEnjoyer
- **Autor**: CWX
- **Repositório**: [CWXDEV/MegaMod](https://github.com/CWXDEV/MegaMod)
- **Licença**: License allows anyone to use the code, just give credit is all I ask.
- **Forge**: [CWX MegaMod](https://forge.sp-tarkov.com/mod/1454/cwx-megamod)

### TraderScrolling
- **Autores**: Kaeno (previously Naekami) e CWX
- **Repositório**: [CWXDEV/Kaeno-TraderScrolling](https://github.com/CWXDEV/Kaeno-TraderScrolling)
- **Licença**: MIT License - Copyright (c) 2024 - 2025 Kaeno (previously Naekami) and CWX
- **Forge**: [Kaeno TraderScrolling](https://forge.sp-tarkov.com/mod/1089/kaeno-traderscrolling)

### ContinuousHealing
- **Autor**: Lacyway
- **Repositório**: [Lacyway/ContinuousHealing](https://github.com/Lacyway/ContinuousHealing)
- **Licença**: Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International
- **Forge**: [Continuous Healing](https://forge.sp-tarkov.com/mod/1884/continuous-healing)

### SPT-ContinuousLoadAmmo
- **Autor**: ozen-m
- **Repositório**: [ozen-m/SPT-ContinuousLoadAmmo](https://github.com/ozen-m/SPT-ContinuousLoadAmmo)
- **Licença**: Copyright (c) 2025 ozen-m
- **Forge**: [Continuous Load Ammo](https://forge.sp-tarkov.com/mod/2112/continuous-load-ammo)

### Skipper
- **Autor**: Terkoiz
- **Repositório**: [acidphantasm/SPT-Skipper](https://github.com/acidphantasm/SPT-Skipper)
- **Licença**: NCSA Open Source License - Copyright (c) 2024 Terkoiz. All rights reserved.
- **Forge**: [Skipper](https://forge.sp-tarkov.com/mod/1343/skipper)

### VisorEffectManager
- **Autor**: silviohmartins
- **Repositório**: [silviohmartins/VisorEffectManager](https://github.com/silviohmartins/VisorEffectManager)
- **Licença**: MIT License - Copyright (c) 2025 silviohmartins
- **Forge**: [VisorEffectManager](https://forge.sp-tarkov.com/mod/2429/visoreffectmanager)

---

## 📄 Licença

Este projeto integra código de múltiplos mods com diferentes licenças. Por favor, respeite as licenças originais de cada mod individual. Todos os créditos vão para os autores originais mencionados acima.

---

## Importante

Este MOD não sera publicado e não havera disponibilização de Releases, foi feito apenas para uso pessoal e em tom de aprendizado.
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

### 7. HideUI

Remove elementos UI do menu do jogo para uma interface mais limpa.

**Funcionalidades**:
- Remove o aviso Alpha/Beta do menu principal

**Configurações**:
- **Hide Beta Warning**: Remove o aviso Alpha/Beta do menu principal (padrão: `true`)

---

### 8. LootHighlighter

Destaca visualmente itens, containers e corpos próximos ao jogador, facilitando a localização de loot durante as raids.

**Funcionalidades**:
- Destaca itens soltos no chão
- Destaca containers (baús, mochilas, gavetas, etc.)
- Destaca corpos de jogadores/PMCs
- Sistema de cores baseado em raridade para itens
- Labels de texto opcionais com informações do loot
- Luzes de highlight para melhor visibilidade
- Toggle on/off durante a raid com hotkey

**Configurações**:
- **Enable Mod**: Ativa/desativa o mod globalmente (padrão: `true`)
- **Toggle Keyboard Shortcut**: Tecla para alternar o highlight durante a raid (padrão: `F1`)
- **Detection Radius**: Raio de detecção em metros (padrão: `10m`, range: 5-30m)
- **Check Interval**: Intervalo entre verificações de loot em segundos (padrão: `0.5s`, range: 0.1-2.0s)
- **Show Text Labels**: Exibe labels de texto nos itens destacados (padrão: `true`)
- **Show Distance in Label**: Mostra a distância até o loot no label (padrão: `false`)
- **Show Items**: Destaca itens soltos (padrão: `true`)
- **Show Containers**: Destaca containers (padrão: `true`)
- **Show Corpses**: Destaca corpos (padrão: `true`)
- **Items Color**: Cor para highlights de itens (padrão: Vermelho)
- **Containers Color**: Cor para highlights de containers (padrão: Verde)
- **Corpses Color**: Cor para highlights de corpos (padrão: Amarelo)

**Notas**:
- O sistema de cores de raridade sobrescreve a cor configurada para itens baseado no template ID
- Itens raros/ultra raros são destacados em cores especiais (roxo, amarelo, ciano)
- Os highlights são limpos automaticamente quando os objetos saem do raio de detecção

---

### 9. HealingAutoCancel

Cancela automaticamente kits médicos aplicados quando a parte do corpo está totalmente curada e não está sangrando ou quebrada.

**Funcionalidades**:
- Cancela automaticamente a cura quando a parte do corpo atinge a saúde máxima
- Não cancela quando há sangramento ativo
- Não cancela quando está consertando um membro quebrado
- Funciona perfeitamente em conjunto com Continuous Healing

**Configurações**:
- **Enable automatic heal canceling**: Ativa/desativa o cancelamento automático de cura (padrão: `true`)

**Notas**:
- Altamente recomendado usar em conjunto com Continuous Healing para melhor experiência
- O mod cancela a cura apenas quando seguro fazê-lo (sem sangramento, sem membro quebrado)
- Quando o kit médico está esgotado, a cura também é cancelada automaticamente

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

### LootHighlighter
- **Autor**: karmaMGL (original), flir063 (atualização)
- **Repositório**: 
  - [karmaMGL/EFT-SPT-mod-3.11-loot-highligher-source-code](https://github.com/karmaMGL/EFT-SPT-mod-3.11-loot-highligher-source-code) (original SPT 3.11)
  - [flir063-spt/avatarLootHighlighter](https://gitlab.com/flir063-spt/loothighlighter) (atualização SPT 4.0)
- **Licença**: Boost Software License
- **Forge**: [Loot Highlighter](https://forge.sp-tarkov.com/mod/2136/loot-highlighter)

### HealingAutoCancel
- **Autor**: minihazel
- **Repositório**: [minihazel/HealingAutoCancel](https://github.com/minihazel/HealingAutoCancel)
- **Licença**: MIT License
- **Forge**: [Healing Autocancel](https://forge.sp-tarkov.com/mod/1274/healing-autocancel)

---

## 📄 Licença

Este projeto integra código de múltiplos mods com diferentes licenças. Por favor, respeite as licenças originais de cada mod individual. Todos os créditos vão para os autores originais mencionados acima.

---

## Importante

Este MOD não sera publicado e não havera disponibilização de Releases, foi feito apenas para uso pessoal e em tom de aprendizado.
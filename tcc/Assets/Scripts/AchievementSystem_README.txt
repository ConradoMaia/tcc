# Sistema de Conquistas - Instruu00e7u00f5es de Implementau00e7u00e3o

Este documento explica como implementar o sistema de conquistas no seu jogo.

## Passos para implementau00e7u00e3o

1. **Criar o prefab do AchievementSystem**:
   - Crie um GameObject vazio na hierarquia
   - Adicione o componente AchievementSystem a ele
   - Configure as conquistas no Inspector:
     - Adicione uma conquista para cada nu00edvel do jogo
     - Defina IDs u00fanicos para cada conquista (ex: "level1_completed", "level2_completed", etc.)
     - Adicione tu00edtulos e descriu00e7u00f5es para cada conquista
     - Adicione u00edcones para cada conquista
   - Arraste o GameObject para a pasta Prefabs para criar um prefab
   - Delete o GameObject da hierarquia

2. **Criar o prefab do AchievementNotification**:
   - Crie um GameObject vazio na hierarquia
   - Adicione o componente AchievementNotification a ele
   - Crie um painel de notificau00e7u00e3o como filho deste GameObject
   - Configure o painel com u00edcone e texto para a notificau00e7u00e3o
   - Arraste o GameObject para a pasta Prefabs para criar um prefab
   - Delete o GameObject da hierarquia

3. **Configurar a cena inicial**:
   - Na sua cena inicial (StartMenu), adicione um GameObject vazio
   - Adicione o componente AchievementSystemSetup a ele
   - Arraste o prefab do AchievementSystem para o campo "Achievement System Prefab"
   - Adicione outro GameObject vazio
   - Adicione o componente AchievementNotification a ele
   - Arraste o prefab do AchievementNotification para o campo "Notification Panel"

4. **Configurar o painel de conquistas**:
   - Na sua cena inicial, crie um painel de conquistas (pode ser inicialmente inativo)
   - Adicione o componente AchievmentUI a ele
   - Configure os campos:
     - Achievement Panel: o painel principal
     - Achievement Item Prefab: um prefab para cada item de conquista
     - Achievement Container: um Transform onde os itens seru00e3o instanciados
     - Close Button: botu00e3o para fechar o painel
     - Open Achievements Button: botu00e3o no menu principal para abrir o painel

5. **Configurar o botu00e3o de conquistas no menu principal**:
   - No seu menu principal, adicione um botu00e3o para abrir o painel de conquistas
   - No Inspector, adicione um evento OnClick que chama o mu00e9todo MostrarConquistas() do InitialMenuController

6. **Configurar os GameManagers em cada cena de nu00edvel**:
   - Em cada cena de nu00edvel, encontre o GameManager
   - No Inspector, adicione o ID da conquista correspondente ao nu00edvel no campo "Level Achievement Id"

## Lista de Conquistas Sugeridas

1. **first_game_completed** - "Primeiro Jogo Concluu00eddo" - "Vocu00ea concluiu seu primeiro jogo!"
2. **level1_completed** - "Nu00edvel 1 Concluu00eddo" - "Vocu00ea concluiu o primeiro nu00edvel!"
3. **level2_completed** - "Nu00edvel 2 Concluu00eddo" - "Vocu00ea concluiu o segundo nu00edvel!"
4. **level3_completed** - "Nu00edvel 3 Concluu00eddo" - "Vocu00ea concluiu o terceiro nu00edvel!"
5. **all_levels_completed** - "Todos os Nu00edveis Concluu00eddos" - "Vocu00ea concluiu todos os nu00edveis do jogo!"

## Como funciona o sistema

Quando um nu00edvel u00e9 concluu00eddo, o GameManager desbloqueia a conquista correspondente. O AchievementSystem salva o estado das conquistas nos PlayerPrefs para que sejam mantidas entre sessu00f5es. Quando uma conquista u00e9 desbloqueada, uma notificau00e7u00e3o u00e9 exibida na tela.

## Soluu00e7u00e3o de problemas

Se as conquistas nu00e3o estiverem sendo desbloqueadas:

1. Verifique se o AchievementSystem estu00e1 sendo instanciado na cena inicial
2. Verifique se os IDs das conquistas no GameManager correspondem aos IDs configurados no AchievementSystem
3. Verifique se o GameManager estu00e1 detectando corretamente quando o nu00edvel u00e9 concluu00eddo

Se as notificau00e7u00f5es nu00e3o estiverem sendo exibidas:

1. Verifique se o AchievementNotification estu00e1 sendo instanciado na cena inicial
2. Verifique se o painel de notificau00e7u00e3o estu00e1 configurado corretamente
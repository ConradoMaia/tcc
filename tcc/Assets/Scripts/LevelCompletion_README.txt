# Painel de Parabu00e9ns - Instruu00e7u00f5es de Implementau00e7u00e3o

Este documento explica como implementar o painel de parabu00e9ns que aparece quando um nu00edvel u00e9 concluu00eddo, agora compatibilizado com o sistema de conquistas.

## Passos para implementau00e7u00e3o

1. **Configurar o PopupManager em cada cena de nu00edvel**:
   - Certifique-se de que cada cena de nu00edvel tenha um GameObject com o componente PopupManager
   - Configure os campos:
     - Popup Panel: o painel de parabu00e9ns (pode estar inicialmente inativo)
     - Close Button: o botu00e3o para fechar o painel
     - Success Sound: o som que toca quando o painel u00e9 exibido
     - Next Scene Name: o nome da cena para onde o jogo deve ir apu00f3s fechar o painel (geralmente "MoodThermometer")

2. **Adicionar o LevelCompletionManager em cada cena de nu00edvel**:
   - Adicione um GameObject vazio na cena
   - Adicione o componente LevelCompletionManager a ele
   - Configure os campos:
     - Popup Manager: arraste o PopupManager da cena
     - Level Achievement Id: o ID da conquista para este nu00edvel (ex: "level1_completed")
     - Next Level Scene: o nome da cena do pru00f3ximo nu00edvel (opcional)

3. **Configurar o GameManager em cada cena de nu00edvel**:
   - Encontre o GameManager na cena
   - No Inspector, arraste o LevelCompletionManager para o campo "Level Completion Manager"
   - Certifique-se de que o campo "Level Achievement Id" estu00e1 preenchido com o ID correto da conquista

## Como funciona o sistema

Quando todos os itens necessários são colocados corretamente, o GameManager chama o LevelCompletionManager para completar o nível. O LevelCompletionManager marca o nível como concluído, desbloqueia a conquista correspondente e mostra o painel de parabéns.

Quando o usuário fecha o painel de parabéns, o jogo navega para a próxima cena usando o SceneNavigator, mantendo o histórico de navegação correto.

## Soluu00e7u00e3o de problemas

Se o painel de parabu00e9ns nu00e3o aparecer:

1. Verifique se o PopupManager estu00e1 configurado corretamente
2. Verifique se o GameManager tem a referu00eancia correta para o LevelCompletionManager
3. Verifique se o totalItemsNeeded no GameManager estu00e1 configurado corretamente

Se o jogo nu00e3o navegar corretamente apu00f3s fechar o painel:

1. Verifique se o nextSceneName no PopupManager estu00e1 configurado corretamente
2. Verifique se o SceneNavigator estu00e1 sendo instanciado na cena inicial
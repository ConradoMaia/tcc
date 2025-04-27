# Sistema de Navegau00e7u00e3o Entre Cenas - Instruu00e7u00f5es de Implementau00e7u00e3o

Este documento explica como implementar o novo sistema de navegau00e7u00e3o entre cenas que corrige os problemas com o botu00e3o "Voltar".

## Passos para implementau00e7u00e3o

1. **Criar o prefab do SceneNavigator**:
   - Crie um GameObject vazio na hierarquia
   - Adicione o componente SceneNavigator a ele
   - Arraste o GameObject para a pasta Prefabs para criar um prefab
   - Delete o GameObject da hierarquia

2. **Configurar a cena inicial**:
   - Na sua cena inicial (StartMenu), adicione um GameObject vazio
   - Adicione o componente SceneNavigatorSetup a ele
   - Arraste o prefab do SceneNavigator para o campo "Scene Navigator Prefab"

3. **Verificar os botu00f5es de voltar**:
   - Certifique-se de que todos os botu00f5es de voltar nas suas cenas chamam a funu00e7u00e3o GoBack() do componente BackButton
   - Exemplo: botu00e3o.onClick.AddListener(GetComponent<BackButton>().GoBack);

4. **Verificar as transiu00e7u00f5es entre cenas**:
   - Todas as transiu00e7u00f5es entre cenas devem usar o SceneNavigator.Instance.NavigateToScene()
   - O cu00f3digo ju00e1 foi atualizado para usar esse mu00e9todo quando possu00edvel

## Como funciona o sistema

O novo sistema de navegau00e7u00e3o mantu00e9m um histu00f3rico das cenas visitadas em uma lista. Quando o usuu00e1rio pressiona o botu00e3o de voltar, o sistema carrega a cena anterior do histu00f3rico.

O histu00f3rico u00e9 persistente entre cenas e u00e9 salvo nos PlayerPrefs para que seja mantido mesmo se o jogo for fechado e reaberto.

## Soluu00e7u00e3o de problemas

Se o botu00e3o de voltar ainda nu00e3o funcionar corretamente em algumas cenas:

1. Verifique se a cena tem um componente BackButton anexado ao botu00e3o de voltar
2. Verifique se todas as transiu00e7u00f5es para essa cena estu00e3o usando o SceneNavigator
3. Certifique-se de que o prefab do SceneNavigator estu00e1 sendo instanciado na cena inicial

Se precisar limpar o histu00f3rico de navegau00e7u00e3o (por exemplo, ao iniciar um novo jogo), chame:
```csharp
BackButton.ClearNavigationHistory();
```
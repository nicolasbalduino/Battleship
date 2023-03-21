# Batalha Naval
## Atividade desenvolvida em grupo para o projeto interação 5by5

<div>
  <img src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/csharp/csharp-plain.svg" title="CSharp" alt="Csharp" width="40" height="40"/>&nbsp;<img src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/visualstudio/visualstudio-plain.svg" title="VisualStudio" alt="VisualStudio" width="40" height="40"/>&nbsp;<img src="https://github.com/devicons/devicon/blob/master/icons/git/git-original-wordmark.svg" title="Git" **alt="Git" width="40" height="40"/>
  </div>


## Requisitos

Batalha naval precisa da versão 6.0 do [.NET](https://dotnet.microsoft.com
) para rodar.

Pa instalar as dependências use o comando a seguir.

```sh
dotnet restore
```

Use os seguintes comandos para abrir e rodar a aplicação.

```sh
dotnet build
dotnet run
```

## O jogo

 - o Jogo se inicia com uma tela de apresentação simples onde o usuário pode apertar o Enter para começar a jogar.
 - ![Start Screen](C:\Users\adm\Desktop\AulaDeObjetos\ProjEmGrupo\Battleship\Images\Starting.png)

 - Na tela a seguir o jogador podera escolher se jogará contra um adversário físoco ou contra a I.A.
 ![Players Selection](C:\Users\adm\Desktop\AulaDeObjetos\ProjEmGrupo\Battleship\Images\GameMode.png)
 - Depois de coletados os dados do(s) jogador(es),  é exibido um tabuleiro para que se posicione as peças. A primeira peça a ser posicionada é o submarino que ocupa 2 posições no tabuleiro. O jogador deve posicioná-lo inserindo as coordenadas primeiro a coluna desejada e em seguida linha (Ex: A1). 
 - O programa irá perguntar se a peça deve ser colocada em posção Horizontal ou Vertical. Se por acaso a peça não couber na posição desejada o tabuleiro irá ajustar para que ocupe a posição aproximada.
 - Feito isso a peça posicionada é mostrada no tabuleiro com uma marcação em volta, já que não se pode colocar duas peças encostadas uma na outra. 
 - O procedimento se repete com o Destroyer e o Carrier, que ocupam 3 e 4 espaços respectivamente.
 - Ainda assim o programa oferece para que o jogador zere seu posicionamento e faça um novo.
 - Mesmo procedimento para o jogador 2.
 - No caso de I.A. obviamente o posicionamento não aparecerá na tela.
 - O objetivo do jogo em si é tentar acertar onde seu oponente seja I.A ou jogador inimigo, posicionou as peças dele.
 - Cada vez que um jogador acerta uma peça inimiga ele tem o dirteito de dar mais um tiro (inclusive a I.A).
 - Quando o tiro não acerta a posição de uma peça inimiga, é a vez do oponente e o lugar onde voce atirou fica marcado com um quadro azul indicativo de "tiro na agua", e vc preciona "Enter" para que o jogo continue
![Acerto na Agua](C:\Users\adm\Desktop\AulaDeObjetos\ProjEmGrupo\Battleship\Images\Agua.png)
  - Quando um navio é acertado é exibida a mensagem Acertou Navio!, após pressionar Enter joga-se de novo.
![Acerto no Navio](C:\Users\adm\Desktop\AulaDeObjetos\ProjEmGrupo\Battleship\Images\Navio.png)
 - Quando um dos jogadores atinge 100% dos navios do oponente o mapa é mostrado.
![Endgame](C:\Users\adm\Desktop\AulaDeObjetos\ProjEmGrupo\Battleship\Images\Endgame.png)
 - O jogador vencedor é parabenizado e sobem os créditos.
![Credits](C:\Users\adm\Desktop\AulaDeObjetos\ProjEmGrupo\Battleship\Images\Credits.png)
## Desenvolvimento

Gostaria de ajudar a melhorar o projeto?

Batalha naval foi desenvolvido no projeto interação e seu código fonte está disponível neste repositório.  Você poderá cloná-lo para sua maquina local, criar uma nova branch onde poderá fazer as devidas melhorias.
Assim que fizer o commit de suas alterações, ficaremos felizes em avaliar cuidadosamente, testar e ocasionalmente fazer o merge.

## Licença

**Interação 5by5**

## Contato dos desenvolvedores

- [Daniel ](https://github.com/DanielVisicatto)
- [Luis Guilherme Francisco da Silva](https://github.com/LuisGuilh3rme)
- [Nicolas Antonio Balduino](https://github.com/nicolasbalduino)
- [Vinicius Picolo](https://github.com/Picolo21)
   

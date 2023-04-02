# BATTLESHIP
## Activity developed in group for projeto interação 5by5

<div>
  <img src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/csharp/csharp-plain.svg" title="CSharp" alt="Csharp" width="40" height="40"/>&nbsp;<img src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/visualstudio/visualstudio-plain.svg" title="VisualStudio" alt="VisualStudio" width="40" height="40"/>&nbsp;<img src="https://github.com/devicons/devicon/blob/master/icons/git/git-original-wordmark.svg" title="Git" **alt="Git" width="40" height="40"/>
  </div>


## Requirements

Battleship requires .NET 6.0 to run. [Install here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

To install the dependencies use the command below.

```sh
dotnet restore
```

Use the commands below to open and run the application.

```sh
dotnet build
dotnet run
```

## Game Guide

- The game starts with a simple presentation screen where the user can press Enter to start playing.
- ![Start Screen](https://github.com/nicolasbalduino/Battleship/blob/main/Battleship/Images/Starting.png)

- On the next screen the player can choose whether to play against a physical opponent or against the machine.
 ![Players Selection](https://github.com/nicolasbalduino/Battleship/blob/main/Battleship/Images/GameMode.png)
 
- After collecting the data of the player(s), a board is displayed for positioning the pieces. The first piece to be positioned is the submarine that occupies 2 positions on the board. The player must position it by entering the coordinates first the desired column and then row (Ex: A1).
 
- The program will ask if the piece should be placed in Horizontal or Vertical position. If by chance the piece does not fit in the desired position, the board will adjust to occupy an approximate position.

- Once this is done, the positioned piece is shown on the board with a marking around it, since you cannot place two pieces next to each other.
 
- The procedure is repeated with Destroyer and Carrier, which occupy 3 and 4 spaces respectively.
- Still, the program offers for you to reset your positioning and make a new one.
 
- Same procedure for player 2.

- AI positioning will not appear on screen.

- The objective of game itself is try hitting where your opponent whether AI or enemy player positioned his pieces.

- Every time a player hits an enemy piece he has right to take another shot.

- When shot does not hit position of an enemy piece, it’s opponent’s turn and the place where you shot gets marked with blue square, indicative of “Acerto na Agua!”, press “Enter” for game continue.
![Acerto na Agua](https://github.com/nicolasbalduino/Battleship/blob/main/Battleship/Images/Agua.png)

 - When a ship is hit, the message "Acerto no Navio!" is displayed, after pressing Enter you play again.
![Acerto no Navio](https://github.com/nicolasbalduino/Battleship/blob/main/Battleship/Images/Navio.png)

 - When one of players hits 100% of opponent’s ships map is shown
![Endgame](https://github.com/nicolasbalduino/Battleship/blob/main/Battleship/Images/Endgame.png)

 - The winning player is congratulated and credits go up.
![Credits](https://github.com/nicolasbalduino/Battleship/blob/main/Battleship/Images/Credits.png)

## Development

Would you like to help improve the project?

Battleship was developed in interaction project and its source code is available in this repository. You can clone it to your local machine, create a new branch where you can make the appropriate improvements. As soon as you commit your changes, we will be happy to carefully evaluate, test and occasionally merge them.

![Battleship logic flowchart](https://user-images.githubusercontent.com/89887370/226598409-d3e93841-3bec-40fc-b48e-b82fc37d26ea.png)

## Licence

**Interação 5by5**

## Developers

- [Daniel Visicatto](https://github.com/DanielVisicatto)
- [Luis Guilherme Francisco da Silva](https://github.com/LuisGuilh3rme)
- [Nicolas Antonio Balduino](https://github.com/nicolasbalduino)
- [Vinicius Picolo](https://github.com/Picolo21)
   

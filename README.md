# Connect 4 Game
By Andoni Iparraguirre, Paul Thibaud, Peter Lin, Hugues Guiffard. 

This is a Connect 4 web application built with ASP.NET Core Blazor for the client-side and ASP.NET Core for the API. The application uses SQLite as the database.

## Project Structure

- **connect4**: This is the API project that handles the game logic and interacts with the SQLite database.
- **connect4GameClient**: This is the Blazor client project that provides the user interface for the Connect 4 game.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (--> Notre version : 8.0.402)
- [SQLite](https://www.sqlite.org/download.html)

## Getting Started
*(open new terminal)*
```sh
cd connect4
dotnet run
```
*(open new terminal)*
```sh
cd connect4GameClient
dotnet run
```

## Playing a Connect4 Game

- Open the 2 different web browsers at this adress : http://localhost:5098/  //For exemple : Chrome and Bing

- Login on both with 2 different users :  //You can also register a new user. You will need to login after having it succefully registered.

    Registered user already in the database:
        - (login: admin) (pwd: Admin64%)
        - (login: admin01) (pwd: Admin01%)
        - (login: admin02) (pwd: Admin02%)

- Play, Join or Create a game :
    - Create game : Give a name and register the game, you will become the host of the game and can only Play it.
    - Join game : If you are not the host/creator of this game you can join it as guest, then you access the game view.
    - Play game : You can access to the game view.

- Game view : Click on a cell to drop your coin in the column. Be careful, the Host plays first !

ENJOY playing CONNECT 4 !
## About

You can access our selfhosted AI by clicking on the About section on all our web pages.

## Develop your own FrontEnd

If you don't like Blazor, you can also develop your own FrontEnd using our API at this address : http://localhost:5034/swagger

## Tests

*(open new terminal)*
```sh
cd Connect4Game.Tests
dotnet test
```

## Entities Diagram
Keep in mind that we used the package :  Microsoft.AspNetCore.Identity 
We thought that it would have been a good thing to familiarize with a user management package to improve our dotnet skills

+---------------------------+
|    Player                 |
|---------------------------|
| + Login: string           |
| + Password: string        |
|---------------------------|
| + Authenticate(           |
|    login: string,         |
|    password: string): bool|
+---------------------------+
         ^
         |
         |
+-------------------+
|    Game           |
|-------------------|
| + Id: int         |
| + Host: Player    |
| + HostId: string  |
| + Guest: Player?  |
| + GuestId: string |
| + Name: string    |
| + Grid: string    |
| + Status: string  |
| + CurrentTurnId:  |
|   string          |
| + CurrentTurn:    |
|   Player?         |
| + Winner: Player? |
|-------------------|
| + StartGame()     |
| + JoinGame(guest: |
|   Player): void   |
| + PlayTurn(player:|
|   Player, column: |
|   int): bool      |
+-------------------+


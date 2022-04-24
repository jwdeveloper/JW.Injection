# JW.Injection

The goal of creating this project was to learn how a dependency injection container works under the hood.

🤔 ***What is dependecy injection?***

Dependency injection is a design pattern that is used to deliver needed dependency into a class constructor
you can read more about it [up here](https://refactoring.guru/design-patterns/singleton)


🤔 ***What is dependecy injection container?***

To escape from the tedious job of creating an instance and putting them into the constructor, a group of the wise people came up with the idea to
automize the whole process by creating API library where you just need to register the class type into `Container`
and then just get it. Object with all of its dependencies will be instantiated automatically. Here are some of 
the library mostly used by people

 > [Microsoft.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/7.0.0-preview.3.22175.4)
 
 > [Autofac](https://www.nuget.org/packages/Autofac/)

 > [UnityContainer](https://unitycontainer.github.io/)	

 Down below is my implementation and I strongly encourage you to look into [code](https://github.com/jwdeveloper/JW.Injection/tree/master/jw.injection/implementation) and learn how the whole thing works 😀
 
### Usage
```c#
//Transient means Instance of class will be created time new
//Singleton means Instance will be created once

builder.RegisterTransient<IGame, Game>();
builder.RegisterTransient<IPlayer, Player>();
builder.RegisterTransient<IPlayerStats, PlayerStats>();
//In case you need to specify out coming object instance use lamda
builder.RegisterSingleton<GameSettings>(() =>
{
    var filePath = @"C:\folder\settings.json";
    return new GameSettings(filePath);
});

var container = builder.Build();
var game = container.Find<IGame>();
game.DisplayObjects();
```

### Output
```cmd
=================================================
Created -> GameSettings  Hash Code: 23458411
Created -> PlayerStats  Hash Code: 21083178
Created -> Player  Hash Code: 55530882
Created -> PlayerStats  Hash Code: 30015890
Created -> Player  Hash Code: 1707556
Created -> Game  Hash Code: 15368010

=========        Objects hash codes     =========
Game 15368010
Player1 55530882
Player1.PlayerStats 21083178
Player1.GameSettings 23458411
Player2 1707556
Player2.PlayerStats 30015890
Player2.GameSettings 23458411
GameSettings 23458411
=================================================
```

# Example classes
## Game
```c#
public interface IGame
{
    public void DisplayObjects();
}

public class Game : IGame
{
    private readonly IPlayer _player1;
    private readonly IPlayer _player2;
    private readonly GameSettings _gameSettings;

    public Game(IPlayer player1, IPlayer player2, GameSettings gameSettings)
    {
        _player1 = player1;
        _player2 = player2;
        _gameSettings = gameSettings;
        Console.WriteLine($"Created -> {GetType()}  Hash Code: {GetHashCode()}");
    }

    public void DisplayObjects()
    {
        //hashcode is basically id of the object
        Console.WriteLine("========= Objects hash codes ==========");
        Console.WriteLine($"Game {GetHashCode()}");
        _player1.DisplayObjects("Player1");
        _player2.DisplayObjects("Player2");
        Console.WriteLine($"GameSettings {_gameSettings.GetHashCode()}");
        Console.WriteLine("=======================================");
    }
}
```

## Player
```c#
public interface IPlayer
{
    public void DisplayObjects(string playerName);
}

public class Player : IPlayer
{
    private readonly string name;
    private readonly GameSettings _gameSettings;
    private readonly IPlayerStats _playerStats;

    public Player(string name) : this(new GameSettings("settings.json"), new PlayerStats())
    {
        this.name = name;
    }

    //By the default first constructor is injected therefore
    //in case you have more then one constructor use attribute 'Inject' to mark constructor for injection
    [Inject]
    public Player(GameSettings gameSettings, IPlayerStats playerStats)
    {
        _gameSettings = gameSettings;
        _playerStats = playerStats;
        Console.WriteLine($"Created -> {GetType()}  Hash Code: {GetHashCode()}");
    }

    public void DisplayObjects(string playerName)
    {
        Console.WriteLine($"{playerName} {GetHashCode()}");
        Console.WriteLine($"{playerName}.PlayerStats {_playerStats.GetHashCode()}");
        Console.WriteLine($"{playerName}.GameSettings {_gameSettings.GetHashCode()}");
    }
}
```

## PlayerStats
```c#
public interface IPlayerStats
{
}

public class PlayerStats : IPlayerStats
{
    public PlayerStats()
    {
        Console.WriteLine($"Created -> {GetType()}  Hash Code: {GetHashCode()}");
    }
}
```

## GameSettings
```c#
public class GameSettings
{
    private readonly string _filePath;
    public GameSettings(string filePath)
    {
        _filePath = filePath;
        Console.WriteLine($"Created -> {GetType()}  Hash Code: {GetHashCode()}");
    }
}
```

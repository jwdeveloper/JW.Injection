using jw.injection.api.attributes;
using jw.injection.implementation;

//Transient - Instance of class will be created every invoke
//Singleton Instance will be created once and then delivered evert time


var builder = JwInjection.CreateContainer();

builder.RegisterTransient<IGame, Game>();
builder.RegisterTransient<IPlayer, Player>();
builder.RegisterTransient<IPlayerStats, PlayerStats>();
//in case you need to specify out coming instance use lamda registration
builder.RegisterSingleton<GameSettings>(() =>
{
    var filePath = @"C:\folder\settings.json";
    return new GameSettings(filePath);
});

var container = builder.Build();
var game = container.Find<IGame>();
game.DisplayObjects();


//-------------------- MODELS -------------------------
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

public class GameSettings
{
    private readonly string _filePath;
    public GameSettings(string filePath)
    {
        _filePath = filePath;
        Console.WriteLine($"Created -> {GetType()}  Hash Code: {GetHashCode()}");
    }
}
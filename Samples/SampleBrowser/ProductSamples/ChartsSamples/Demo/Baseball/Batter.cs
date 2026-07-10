namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball;

public class Batter : ObservableObjectBase {

	private string? _firstName;
	private string? _lastName;
	private int _number;
	private string? _position;
	private static readonly Random _random = new();
	private Team? _team;

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Gets a random position.
	/// </summary>
	private static string GetRandomPosition() {
		var positions = Positions.ToArray();
		int index = _random.Next(0, positions.Length);
		return positions[index];
	}

	/// <summary>
	/// The positions.
	/// </summary>
	private static IEnumerable<string> Positions
		=> ["C", "SS", "3B", "2B", "1B", "CF", "LF", "RF", "DH"];

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Builds the random stats.
	/// </summary>
	/// <param name="startingYear">The starting year.</param>
	/// <param name="endingYear">The ending year.</param>
	public void BuildRandomStats(int startingYear, int endingYear) {
		for (var year = startingYear; year <= endingYear; year++)
			Stats.Add(BatterSeasonStats.Random(year));
	}

	/// <summary>
	/// Builds the random batter.
	/// </summary>
	/// <param name="firstName">The first name.</param>
	/// <param name="lastName">The last name.</param>
	/// <param name="statStartingYear">The stat starting year.</param>
	/// <param name="statEndingYear">The stat ending year.</param>
	public static Batter BuildRandomBatter(string firstName, string lastName, int statStartingYear, int statEndingYear) {
		var batter = new Batter {
			FirstName = firstName,
			LastName = lastName,
			Number = _random.Next(0, 60),
			Position = GetRandomPosition()
		};
		batter.BuildRandomStats(statStartingYear, statEndingYear);
		return batter;
	}

	/// <summary>
	/// The current year stats.
	/// </summary>
	public BatterSeasonStats CurrentYearStats
		=> Stats.Last();

	/// <summary>
	/// The first name.
	/// </summary>
	public string? FirstName {
		get => _firstName;
		set {
			if (SetProperty(ref _firstName, value)) {
				OnPropertyChanged(nameof(Name));
				OnPropertyChanged(nameof(OrderedName));
			}
		}
	}

	/// <summary>
	/// The last name.
	/// </summary>
	public string? LastName {
		get => _lastName;
		set {
			if (SetProperty(ref _lastName, value)) {
				OnPropertyChanged(nameof(Name));
				OnPropertyChanged(nameof(OrderedName));
			}
		}
	}


	/// <summary>
	/// The name.
	/// </summary>
	public string Name
		=> string.Format("{0} {1}", FirstName, LastName);

	/// <summary>
	/// The player number.
	/// </summary>
	public int Number {
		get => _number;
		set => SetProperty(ref _number, value);
	}

	/// <summary>
	/// The ordered name.
	/// </summary>
	public string OrderedName
		=> string.Format("{0}, {1}", LastName, FirstName);

	/// <summary>
	/// The position.
	/// </summary>
	public string? Position {
		get => _position;
		set => SetProperty(ref _position, value);
	}

	/// <summary>
	/// The stats.
	/// </summary>
	public ObservableCollection<BatterSeasonStats> Stats { get; } = [];

	/// <summary>
	/// The team.
	/// </summary>
	public Team? Team {
		get => _team;
		set => SetProperty(ref _team, value);
	}

}

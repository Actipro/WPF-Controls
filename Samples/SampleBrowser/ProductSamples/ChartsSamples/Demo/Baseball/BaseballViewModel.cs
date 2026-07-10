using ActiproSoftware.Extensions;
using ActiproSoftware.Windows.Controls.Charts;
using ActiproSoftware.Windows.Controls.Charts.Palettes;

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball;

/// <summary>
/// The view model for the baseball demo.
/// </summary>
public class BaseballViewModel : ObservableObjectBase {

	private static readonly Random _random = new();
	private static readonly int EndingYear = 2018;
	private static readonly int StartingYear = 2010;

	private Batter? _selectedTeamOneBatter;
	private Batter? _selectedTeamTwoBatter;
	private ISeriesStyleSelector? _styleSelector;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public BaseballViewModel() {
		BuildTeamOneBatters();
		BuildTeamTwoBatters();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static IEnumerable<Batter> BuildBatters(List<string> teamNames, List<Color> teamColors, List<string> firstNames, List<string> lastNames) {
		Debug.Assert(lastNames.Count >= firstNames.Count, "There must be at least as many last name values as first name values.");

		var teams = BuildTeams(teamNames, teamColors);
		var unsortedBatters = new HashSet<Batter>();
		for (var i = 0; i < firstNames.Count; i++) {
			var firstName = firstNames[i];
			var lastName = lastNames[i];
			var batter = Batter.BuildRandomBatter(firstName, lastName, StartingYear, EndingYear);
			var teamIndex = _random.Next(0, teams.Count);
			batter.Team = teams[teamIndex];
			unsortedBatters.Add(batter);
		}

		return unsortedBatters.OrderBy(b => b.OrderedName);
	}

	private static List<Team> BuildTeams(List<string> names, List<Color> colors) {
		Debug.Assert(colors.Count >= names.Count, "There must be at least as many colors as there are teams.");

		var teams = new List<Team>();
		for (var i = 0; i < names.Count; i++) {
			var team = new Team {
				Name = names[i],
				Color = colors[i]
			};
			teams.Add(team);
		}

		return teams;
	}

	/// <summary>
	/// Builds the team one batters.
	/// </summary>
	private void BuildTeamOneBatters() {
		TeamOneBatters.AddRange(BuildBatters(
			TeamOneNames.ToList(),
			TeamOneColors.ToList(),
			TeamOneBatterFirstNames.ToList(),
			TeamOneBatterLastNames.ToList()
		));

		SelectedTeamOneBatter = TeamOneBatters[0];
	}

	/// <summary>
	/// Builds the team two batters.
	/// </summary>
	private void BuildTeamTwoBatters() {
		TeamTwoBatters.AddRange(BuildBatters(
			TeamTwoNames.ToList(),
			TeamTwoColors.ToList(),
			TeamTwoBatterFirstNames.ToList(),
			TeamTwoBatterLastNames.ToList()
		));

		SelectedTeamTwoBatter = TeamTwoBatters[0];
	}

	/// <summary>
	/// The team one batter first names.
	/// </summary>
	private static IEnumerable<string> TeamOneBatterFirstNames
		=> ["Allan", "Christian", "Guy", "Jaime", "Lonnie", "Jessie", "Hugh", "Kelly", "Allan", "Max", "Lance", "Clayton", "Max", "Neil"];

	/// <summary>
	/// The team one batter last names.
	/// </summary>
	private static IEnumerable<string> TeamOneBatterLastNames
		=> ["Brobst", "Crespin", "Hursh", "Stenzel", "Iser", "Orenstein", "Loth", "Dunworth", "Atha", "Sardina", "Stimage", "Mally", "Kinslow", "Lenser"];

	/// <summary>
	/// The team two batter first names.
	/// </summary>
	private static IEnumerable<string> TeamTwoBatterFirstNames
		=> ["Julio", "Kelly", "Ted", "Darryl", "Jamie", "Lonnie", "Kurt", "Neil", "Darren", "Christian", "Erik", "Nelson", "Matthew", "Ted"];

	/// <summary>
	/// The team two batter last names.
	/// </summary>
	private static IEnumerable<string> TeamTwoBatterLastNames
		=> ["Milbourn", "Catoe", "Dulmage", "Yocom", "Loken", "Coursey", "Weekly", "Spells", "Pazos", "Lucus", "Coursey", "Wiggin", "Geddie", "Sedlak"];

	/// <summary>
	/// The team one colors.
	/// </summary>
	private static IEnumerable<Color> TeamOneColors {
		get => [
			Color.FromArgb(255, 135, 188, 222),
			Color.FromArgb(255, 219, 68, 39),
			Color.FromArgb(255, 162, 161, 177),
			Color.FromArgb(255, 0, 134, 166)
		];
	}

	/// <summary>
	/// The team two colors.
	/// </summary>
	private static IEnumerable<Color> TeamTwoColors {
		get => [
			Color.FromArgb(255, 3, 136, 89),
			Color.FromArgb(255, 242, 167, 42),
			Color.FromArgb(255, 81, 69, 141),
			Color.FromArgb(255, 131, 71, 123)
		];
	}

	/// <summary>
	/// The team one names.
	/// </summary>
	private static IEnumerable<string> TeamOneNames
		=> ["Chattanooga Jellyfish", "Reno Catfish", "Chicopee Plankton", "Scranton Mermen"];

	/// <summary>
	/// The team two names.
	/// </summary>
	private static IEnumerable<string> TeamTwoNames
		=> ["Des Moines Poodles", "Roanoke Squirrels", "Dodge City Wombats", "Cupertino Meercats"];

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The selected team one batter.
	/// </summary>
	public Batter? SelectedTeamOneBatter {
		get => _selectedTeamOneBatter;
		set {
			if (SetProperty(ref _selectedTeamOneBatter, value))
				UpdateStyleSelector();
		}
	}

	/// <summary>
	/// The selected team two batter.
	/// </summary>
	public Batter? SelectedTeamTwoBatter {
		get => _selectedTeamTwoBatter;
		set {
			if (SetProperty(ref _selectedTeamTwoBatter, value))
				UpdateStyleSelector();
		}
	}

	/// <summary>
	/// The style selector.
	/// </summary>
	public ISeriesStyleSelector? StyleSelector {
		get => _styleSelector;
		set => SetProperty(ref _styleSelector, value);
	}

	/// <summary>
	/// The team one batters.
	/// </summary>
	public ObservableCollection<Batter> TeamOneBatters { get; } = [];

	/// <summary>
	/// The team two batters.
	/// </summary>
	public ObservableCollection<Batter> TeamTwoBatters { get; } = [];

	/// <summary>
	/// Updates the style selector.
	/// </summary>
	private void UpdateStyleSelector() {
		if (
			SelectedTeamOneBatter?.Team is { } teamOne
			&& SelectedTeamTwoBatter?.Team is { } teamTwo
		) {
			var selector = new SeriesPaletteStyleSelector {
				Palette = new Palette(teamOne.Color, teamTwo.Color)
			};
			StyleSelector = selector;
		}
	}

}

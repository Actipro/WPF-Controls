using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball;

/// <summary>
/// A collection of batter stats for a single season.
/// </summary>
public class BatterSeasonStats : ObservableObjectBase {

	private static readonly Random _random = new();

	private int _atBats;
	private double _battingAverage;
	private int _gamesPlayed;
	private int _hits;
	private int _homeRuns;
	private double _onBasePercentage;
	private int _runs;
	private double _sluggingPercentage;
	private int _year;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The number of at bats.
	/// </summary>
	[Display(Order = 3, Name = "At Bats")]
	public int AtBats {
		get => _atBats;
		set => SetProperty(ref _atBats, value);
	}

	/// <summary>
	/// The batting average.
	/// </summary>
	[Display(Order = 1, Name = "Batting Avg")]
	public double BattingAverage {
		get => _battingAverage;
		set => SetProperty(ref _battingAverage, value);
	}

	/// <summary>
	/// The number of games played.
	/// </summary>
	[Display(Order = 2, Name = "Games Played")]
	public int GamesPlayed {
		get => _gamesPlayed;
		set => SetProperty(ref _gamesPlayed, value);
	}

	/// <summary>
	/// The number of hits.
	/// </summary>
	[Display(Order = 4)]
	public int Hits {
		get => _hits;
		set => SetProperty(ref _hits, value);
	}

	/// <summary>
	/// The number of home runs.
	/// </summary>
	[Display(Order = 6, Name = "Home Runs")]
	public int HomeRuns {
		get => _homeRuns;
		set => SetProperty(ref _homeRuns, value);
	}

	/// <summary>
	/// The on base percentage.
	/// </summary>
	[Display(Order = 8, Name = "On Base %")]
	public double OnBasePercentage {
		get => _onBasePercentage;
		set => SetProperty(ref _onBasePercentage, value);
	}

	/// <summary>
	/// Returns a random set of stats.
	/// </summary>
	/// <param name="year">The year.</param>
	public static BatterSeasonStats Random(int year) {
		var stats = new BatterSeasonStats {
			Year = year,

			GamesPlayed = _random.Next(20, 120),
			AtBats = _random.Next(50, 170),
			Runs = _random.Next(1, 20),
			Hits = _random.Next(6, 35),
			HomeRuns = _random.Next(6, 25),
			BattingAverage = _random.Next(200, 300) / 1000.0d,
			OnBasePercentage = _random.Next(300, 450) / 1000.0d,
			SluggingPercentage = _random.Next(400, 600) / 1000.0d
		};

		return stats;
	}

	/// <summary>
	/// The number of runs.
	/// </summary>
	[Display(Order = 5)]
	public int Runs {
		get => _runs;
		set => SetProperty(ref _runs, value);
	}

	/// <summary>
	/// The slugging percentage.
	/// </summary>
	[Display(Order = 7, Name = "Slugging %")]
	public double SluggingPercentage {
		get => _sluggingPercentage;
		set => SetProperty(ref _sluggingPercentage, value);
	}

	/// <summary>
	/// The year.
	/// </summary>
	[Browsable(false)]
	public int Year {
		get => _year;
		set => SetProperty(ref _year, value);
	}

}

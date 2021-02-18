using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#if WINRT
using ActiproSoftware.UI.Xaml;
#else
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball {

	/// <summary>
	/// A collection of batter stats for a single season.
	/// </summary>
	public class BatterSeasonStats : ObservableObjectBase {

		private static readonly Random random = new Random();

		private int atBats;
		private double battingAverage;
		private int gamesPlayed;
		private int hits;
		private int homeRuns;
		private double onBasePercentage;
		private int runs;
		private double sluggingPercentage;
		private int year;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets at bats.
		/// </summary>
		/// <value>At bats.</value>
		[Display(Order = 3, Name = "At Bats")]
		public int AtBats {
			get { return atBats; }
			set {
				atBats = value;
				NotifyPropertyChanged("AtBats");
			}
		}

		/// <summary>
		/// Gets or sets the batting average.
		/// </summary>
		/// <value>The batting average.</value>
		[Display(Order = 1, Name = "Batting Avg")]
		public double BattingAverage {
			get { return battingAverage; }
			set {
				battingAverage = value;
				NotifyPropertyChanged("BattingAverage");
			}
		}

		/// <summary>
		/// Gets or sets the games played.
		/// </summary>
		/// <value>The games played.</value>
		[Display(Order = 2, Name = "Games Played")]
		public int GamesPlayed {
			get { return gamesPlayed; }
			set {
				gamesPlayed = value;
				NotifyPropertyChanged("GamesPlayed");
			}
		}

		/// <summary>
		/// Gets or sets the hits.
		/// </summary>
		/// <value>The hits.</value>
		[Display(Order = 4)]
		public int Hits {
			get { return hits; }
			set {
				hits = value;
				NotifyPropertyChanged("Hits");
			}
		}

		/// <summary>
		/// Gets or sets the home runs.
		/// </summary>
		/// <value>The home runs.</value>
		[Display(Order = 6, Name = "Home Runs")]
		public int HomeRuns {
			get { return homeRuns; }
			set {
				homeRuns = value;
				NotifyPropertyChanged("HomeRuns");
			}
		}

		/// <summary>
		/// Gets or sets the on base percentage.
		/// </summary>
		/// <value>The on base percentage.</value>
		[Display(Order = 8, Name = "On Base %")]
		public double OnBasePercentage {
			get { return onBasePercentage; }
			set {
				onBasePercentage = value;
				NotifyPropertyChanged("OnBasePercentage");
			}
		}

		/// <summary>
		/// Gets a random set of stats.
		/// </summary>
		/// <param name="year">The year.</param>
		/// <returns>A random set of stats.</returns>
		public static BatterSeasonStats Random(int year) {
			var stats = new BatterSeasonStats();
			stats.Year = year;

			stats.GamesPlayed = random.Next(20, 120);
			stats.AtBats = random.Next(50, 170);
			stats.Runs = random.Next(1, 20);
			stats.Hits = random.Next(6, 35);
			stats.HomeRuns = random.Next(6, 25);
			stats.BattingAverage = random.Next(200, 300) / 1000.0d;
			stats.OnBasePercentage = random.Next(300, 450) / 1000.0d;
			stats.SluggingPercentage = random.Next(400, 600) / 1000.0d;

			return stats;
		}

		/// <summary>
		/// Gets or sets the runs.
		/// </summary>
		/// <value>The runs.</value>
		[Display(Order = 5)]
		public int Runs {
			get { return runs; }
			set {
				runs = value;
				NotifyPropertyChanged("Runs");
			}
		}

		/// <summary>
		/// Gets or sets the slugging percentage.
		/// </summary>
		/// <value>The slugging percentage.</value>
		[Display(Order = 7, Name = "Slugging %")]
		public double SluggingPercentage {
			get { return sluggingPercentage; }
			set {
				sluggingPercentage = value;
				NotifyPropertyChanged("SluggingPercentage");
			}
		}

		/// <summary>
		/// Gets or sets the year.
		/// </summary>
		/// <value>The year.</value>
		[Browsable(false)]
		public int Year {
			get { return year; }
			set {
				year = value;
				NotifyPropertyChanged("Year");
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}

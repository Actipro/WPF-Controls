using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
#if WINRT
using ActiproSoftware.UI.Xaml;
#else
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball {

	public class Batter : ObservableObjectBase {

		private string firstName;
		private string lastName;
		private int number;
		private string position;
		private static readonly Random random = new Random();
		private readonly ObservableCollection<BatterSeasonStats> stats = new ObservableCollection<BatterSeasonStats>();
		private Team team;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a random position.
		/// </summary>
		/// <returns>A random position.</returns>
		private static string GetRandomPosition() {
			int position = random.Next(0, Positions.Count());
			return Positions.ElementAt(position);
		}

		/// <summary>
		/// Gets the positions.
		/// </summary>
		/// <value>The positions.</value>
		private static IEnumerable<string> Positions {
			get {
				return new List<string> {"C", "SS","3B", "2B", "1B", "CF", "LF", "RF", "DH"};
			}
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Builds the random stats.
		/// </summary>
		/// <param name="startingYear">The starting year.</param>
		/// <param name="endingYear">The ending year.</param>
		public void BuildRandomStats(int startingYear, int endingYear) {
			for (int year = startingYear; year <= endingYear; year++) {
				Stats.Add(BatterSeasonStats.Random(year));
			}
		}

		/// <summary>
		/// Builds the random batter.
		/// </summary>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		/// <param name="statStartingYear">The stat starting year.</param>
		/// <param name="statEndingYear">The stat ending year.</param>
		/// <returns>The batter.</returns>
		public static Batter BuildRandomBatter(string firstName, string lastName, int statStartingYear, int statEndingYear) {
			var batter = new Batter();
			batter.FirstName = firstName;
			batter.LastName = lastName;
			batter.Number = random.Next(0, 60);
			batter.BuildRandomStats(statStartingYear, statEndingYear);
			batter.Position = GetRandomPosition();
			return batter;
		}

		/// <summary>
		/// Gets the current year stats.
		/// </summary>
		/// <value>The current year stats.</value>
		public BatterSeasonStats CurrentYearStats {
			get {
				return Stats.Last();
			}
		}

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>The first name.</value>
		public string FirstName {
			get { return firstName; }
			set {
				firstName = value;
				NotifyPropertyChanged("FirstName");
				NotifyPropertyChanged("Name");
				NotifyPropertyChanged("OrderedName");
			}
		}

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>The last name.</value>
		public string LastName {
			get { return lastName; }
			set {
				lastName = value;
				NotifyPropertyChanged("LastName");
				NotifyPropertyChanged("Name");
				NotifyPropertyChanged("OrderedName");
			}
		}


		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get { return string.Format("{0} {1}", firstName, LastName); }
		}

		/// <summary>
		/// Gets or sets the player number.
		/// </summary>
		/// <value>The player number.</value>
		public int Number {
			get { return number; }
			set {
				number = value;
				NotifyPropertyChanged("Number");
			}
		}

		/// <summary>
		/// Gets the ordered name.
		/// </summary>
		/// <value>The ordered name.</value>
		public string OrderedName {
			get { return string.Format("{0}, {1}", lastName, firstName); }
		}

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public string Position {
			get { return position; }
			set {
				position = value;
				NotifyPropertyChanged("Position");
			}
		}

		/// <summary>
		/// Gets the stats.
		/// </summary>
		/// <value>The stats.</value>
		public ObservableCollection<BatterSeasonStats> Stats {
			get { return stats; }
		}

		/// <summary>
		/// Gets or sets the team.
		/// </summary>
		/// <value>The team.</value>
		public Team Team {
			get { return team; }
			set {
				team = value;
				NotifyPropertyChanged("Team");
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}

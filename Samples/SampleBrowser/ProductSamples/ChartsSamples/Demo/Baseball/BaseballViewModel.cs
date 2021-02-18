using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
#if WINRT
using ActiproSoftware.UI.Xaml;
using ActiproSoftware.UI.Xaml.Controls.Charts;
using ActiproSoftware.UI.Xaml.Controls.Charts.Palettes;
using Windows.UI;
#else
using System.Windows.Media;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Charts;
using ActiproSoftware.Windows.Controls.Charts.Palettes;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball {

	/// <summary>
	/// The view model for the baseball demo.
	/// </summary>
	public class BaseballViewModel : ObservableObjectBase {

		private static readonly Random random = new Random();
		private static readonly int EndingYear = 2018;
		private static readonly int StartingYear = 2010;

		private Batter selectedTeamOneBatter;
		private Batter selectedTeamTwoBatter;
		private ISeriesStyleSelector styleSelector;
		private readonly ObservableCollection<Batter> teamOneBatters = new ObservableCollection<Batter>();
		private readonly ObservableCollection<Batter> teamTwoBatters = new ObservableCollection<Batter>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseballViewModel" /> class.
		/// </summary>
		public BaseballViewModel() {
			BuildBatters();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Builds the batters.
		/// </summary>
		private void BuildBatters() {
			BuildTeamOneBatters();
			BuildTeamTwoBatters();
		}

		/// <summary>
		/// Builds the team one teams.
		/// </summary>
		/// <returns>The team one teams.</returns>
		private IEnumerable<Team> BuildTeamOneTeams() {
			var teams = new List<Team>();
			for (int i = 0; i < TeamOneNames.Count(); i++) {
				var team = new Team();
				team.Name = TeamOneNames.ElementAt(i);
				team.Color = TeamOneColors.ElementAt(i);
				teams.Add(team);
			}

			return teams;
		}

		/// <summary>
		/// Builds the team two teams.
		/// </summary>
		/// <returns>The team two teams.</returns>
		private IEnumerable<Team> BuildTeamTwoTeams() {
			var teams = new List<Team>();
			for (int i = 0; i < TeamTwoNames.Count(); i++) {
				var team = new Team();
				team.Name = TeamTwoNames.ElementAt(i);
				team.Color = TeamTwoColors.ElementAt(i);
				teams.Add(team);
			}

			return teams;
		}

		/// <summary>
		/// Builds the team one batters.
		/// </summary>
		private void BuildTeamOneBatters() {
			var teamOneTeams = BuildTeamOneTeams();
			var unsortedBatters = new List<Batter>();
			for (int i = 0; i < TeamOneBatterFirstNames.Count(); i++) {
				string firstName = TeamOneBatterFirstNames.ElementAt(i);
				string lastName = TeamOneBatterLastNames.ElementAt(i);
				var batter = Batter.BuildRandomBatter(firstName, lastName, StartingYear, EndingYear);
				int teamIndex = random.Next(0, teamOneTeams.Count());
				batter.Team = teamOneTeams.ElementAt(teamIndex);
				unsortedBatters.Add(batter);
			}

			foreach (var batter in unsortedBatters.OrderBy(batter => batter.OrderedName)) {
				TeamOneBatters.Add(batter);
			}

			SelectedTeamOneBatter = teamOneBatters[0];
		}

		/// <summary>
		/// Builds the team two batters.
		/// </summary>
		private void BuildTeamTwoBatters() {
			var teamTwoTeams = BuildTeamTwoTeams();
			var unsortedBatters = new List<Batter>();
			for (int i = 0; i < TeamTwoBatterFirstNames.Count(); i++) {

				string firstName = TeamTwoBatterFirstNames.ElementAt(i);
				string lastName = TeamTwoBatterLastNames.ElementAt(i);
				var batter = Batter.BuildRandomBatter(firstName, lastName, StartingYear, EndingYear);
				int teamIndex = random.Next(0, teamTwoTeams.Count());
				batter.Team = teamTwoTeams.ElementAt(teamIndex);
				unsortedBatters.Add(batter);
			}

			foreach (var batter in unsortedBatters.OrderBy(batter => batter.OrderedName)) {
				TeamTwoBatters.Add(batter);
			}

			SelectedTeamTwoBatter = teamTwoBatters[0];
		}

		/// <summary>
		/// Gets the team one batter first names.
		/// </summary>
		/// <value>The team one batter first names.</value>
		private IEnumerable<string> TeamOneBatterFirstNames {
			get { return new List<string> { "Allan", "Christian", "Guy", "Jaime", "Lonnie", "Jessie", "Hugh", "Kelly", "Allan", "Max", "Lance", "Clayton", "Max", "Neil" }; }
		}

		/// <summary>
		/// Gets the team one batter last names.
		/// </summary>
		/// <value>The team one batter last names.</value>
		private IEnumerable<string> TeamOneBatterLastNames {
			get { return new List<string> { "Brobst", "Crespin", "Hursh", "Stenzel", "Iser", "Orenstein", "Loth", "Dunworth", "Atha", "Sardina", "Stimage", "Mally", "Kinslow", "Lenser" }; }
		}

		/// <summary>
		/// Gets the team two batter first names.
		/// </summary>
		/// <value>The team two batter first names.</value>
		private IEnumerable<string> TeamTwoBatterFirstNames {
			get { return new List<string> { "Julio", "Kelly", "Ted", "Darryl", "Jamie", "Lonnie", "Kurt", "Neil", "Darren", "Christian", "Erik", "Nelson", "Matthew", "Ted" }; }
		}

		/// <summary>
		/// Gets the team two batter last names.
		/// </summary>
		/// <value>The team two batter last names.</value>
		private IEnumerable<string> TeamTwoBatterLastNames {
			get { return new List<string> { "Milbourn", "Catoe", "Dulmage", "Yocom", "Loken", "Coursey", "Weekly", "Spells", "Pazos", "Lucus", "Coursey", "Wiggin", "Geddie", "Sedlak" }; }
		}

		/// <summary>
		/// Gets the team one colors.
		/// </summary>
		/// <value>The team one colors.</value>
		private static IEnumerable<Color> TeamOneColors {
			get {
				return new List<Color>{
					Color.FromArgb(255, 135, 188, 222), 
					Color.FromArgb(255, 219, 68, 39), 
					Color.FromArgb(255, 162, 161, 177), 
					Color.FromArgb(255, 0, 134, 166)};
			}
		}

		/// <summary>
		/// Gets the team two colors.
		/// </summary>
		/// <value>The team two colors.</value>
		private static IEnumerable<Color> TeamTwoColors {
			get {
				return new List<Color> { 
				Color.FromArgb(255, 3, 136, 89), 
				Color.FromArgb(255, 242, 167, 42), 
				Color.FromArgb(255, 81, 69, 141), 
				Color.FromArgb(255, 131, 71, 123) };
			}
		}

		/// <summary>
		/// Gets the team one names.
		/// </summary>
		/// <value>The team one names.</value>
		private IEnumerable<string> TeamOneNames {
			get { return new List<string> {"Chattanooga Jellyfish", "Reno Catfish", "Chicopee Plankton", "Scranton Mermen"}; }
		}

		/// <summary>
		/// Gets the team two names.
		/// </summary>
		/// <value>The team two names.</value>
		private IEnumerable<string> TeamTwoNames {
			get { return new List<string> {"Des Moines Poodles", "Roanoke Squirrels", "Dodge City Wombats", "Cupertino Meercats"}; }
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the selected team one batter.
		/// </summary>
		/// <value>The selected team one batter.</value>
		public Batter SelectedTeamOneBatter {
			get { return selectedTeamOneBatter; }
			set {
				selectedTeamOneBatter = value;
				NotifyPropertyChanged("SelectedTeamOneBatter");
				UpdateStyleSelector();
			}
		}

		/// <summary>
		/// Gets or sets the selected team two batter.
		/// </summary>
		/// <value>The selected team two batter.</value>
		public Batter SelectedTeamTwoBatter {
			get { return selectedTeamTwoBatter; }
			set {
				selectedTeamTwoBatter = value;
				NotifyPropertyChanged("SelectedTeamTwoBatter");
				UpdateStyleSelector();
			}
		}

		/// <summary>
		/// Gets or sets the style selector.
		/// </summary>
		/// <value>The style selector.</value>
		public ISeriesStyleSelector StyleSelector {
			get { return styleSelector; }
			set {
				styleSelector = value;
				NotifyPropertyChanged("StyleSelector");
			}
		}

		/// <summary>
		/// Gets the team one batters.
		/// </summary>
		/// <value>The team one batters.</value>
		public ObservableCollection<Batter> TeamOneBatters {
			get { return teamOneBatters; }
		}

		/// <summary>
		/// Gets the team two batters.
		/// </summary>
		/// <value>The team two batters.</value>
		public ObservableCollection<Batter> TeamTwoBatters {
			get { return teamTwoBatters; }
		}

		/// <summary>
		/// Updates the style selector.
		/// </summary>
		private void UpdateStyleSelector() {
			if (selectedTeamOneBatter == null || selectedTeamTwoBatter == null)
				return;

			var selector = new SeriesPaletteStyleSelector();
			selector.Palette = new Palette(selectedTeamOneBatter.Team.Color, selectedTeamTwoBatter.Team.Color);
			StyleSelector = selector;
		}

		#endregion PUBLIC PROCEDURES
	}
}

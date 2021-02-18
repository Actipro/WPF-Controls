using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ActiproSoftware.Compatibility;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.WallCalendar {

	/// <summary>
	/// A page from a calendar.
	/// </summary>
	public partial class CalendarPage : UserControl {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Month"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Month"/> dependency property.</value>
		public static readonly DependencyProperty MonthProperty = DependencyProperty.Register("Month",
			typeof(Month), typeof(CalendarPage), new FrameworkPropertyMetadata(ActiproSoftware.ProductSamples.ViewsSamples.Demo.WallCalendar.Month.January, OnMonthPropertyValueChanged));

		/// <summary>
		/// Identifies the <see cref="StartDay"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="StartDay"/> dependency property.</value>
		public static readonly DependencyProperty StartDayProperty = DependencyProperty.Register("StartDay",
			typeof(Day), typeof(CalendarPage), new FrameworkPropertyMetadata(ActiproSoftware.ProductSamples.ViewsSamples.Demo.WallCalendar.Day.Sunday, OnStartDayPropertyValueChanged));

		#endregion // Dependency Properties

		private Dictionary<Month, int> daysPerMonth = new Dictionary<Month, int>() {
			{Month.January, 31},
			{Month.February, 28},
			{Month.March, 31},
			{Month.April, 30},
			{Month.May, 31},
			{Month.June, 30},
			{Month.July, 31},
			{Month.August, 31},
			{Month.September, 30},
			{Month.October, 31},
			{Month.November, 30},
			{Month.December, 31},
		};
		private DeferrableObservableCollection<Holiday> holidays = new DeferrableObservableCollection<Holiday>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarPage"/> class.
		/// </summary>
		public CalendarPage() {
			InitializeComponent();
			holidays.CollectionChanged += (o, e) => ClearAndPopulateCalendar();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Clears and populates the calendar.
		/// </summary>
		private void ClearAndPopulateCalendar() {
			coreGrid.Children.Clear();

			Month month = this.Month;
			Day startDay = this.StartDay;

			int totalDays;
			daysPerMonth.TryGetValue(month, out totalDays);
			if (totalDays == 0)
				return;

			// Populate blank day boxes
			Day currentDay = Day.Sunday;
			for (int i = 0; i < (int)startDay; i++) {
				// Create a border and set parameters
				Border border = new Border() {
					BorderBrush = new SolidColorBrush(Colors.Black),
					BorderThickness = new Thickness(0)
				};
				Grid.SetColumn(border, (int)currentDay);
				Grid.SetRow(border, 0);

				// Create a grid
				Grid grid = new Grid();

				// Make the grid a child of the border
				border.Child = grid;

				// Color the current day
				grid.Background = new SolidColorBrush(Color.FromArgb(255, 224, 224, 224));

				// Add the border to the core grid
				coreGrid.Children.Add(border);

				// Increment the current day
				currentDay++;
			}

			// Iterate through all the days of the month
			currentDay = startDay;
			for (int i = 0; i < totalDays; i++) {
				// Create a border and set parameters
				Border border = new Border() {
					BorderBrush = new SolidColorBrush(Colors.Black),
					BorderThickness = new Thickness(1)
				};
				Grid.SetColumn(border, (int)currentDay);
				Grid.SetRow(border, (int)((i + (int)startDay) / 7));

				// Create a grid and configure it
				Grid grid = new Grid();
				grid.RowDefinitions.Add(new RowDefinition() {
					Height = new GridLength(0, GridUnitType.Auto)
				});
				grid.RowDefinitions.Add(new RowDefinition());

				// Make the grid a child of the border
				border.Child = grid;

				// Create the text block that displays the day number and set parameters
				TextBlock textBox = new TextBlock() {
					Text = (i + 1).ToString(),
					FontSize = 10,
					Margin = new Thickness(1, 1, 0, 0)
				};

				// Make the text block that displays the day number a child of the grid
				grid.Children.Add(textBox);

				// Generate this day's holiday string
				string holidayString = "";
				foreach (var holiday in holidays) {
					if (holiday.Day == i + 1)
						holidayString += holiday.HolidayName + Environment.NewLine;
				}
				if (!String.IsNullOrEmpty(holidayString))
					holidayString = holidayString.Remove(holidayString.Length - 2);

				// Create the text block that displays holiday information
				textBox = new TextBlock() {
					Text = holidayString,
					FontSize = 8,
					Margin = new Thickness(1, 0, 0, 0),
					VerticalAlignment = VerticalAlignment.Bottom
				};

				// Set the grid row of the text block that displays holiday information to 1
				Grid.SetRow(textBox, 1);

				// Make the text block that displays the holiday information a child of the grid
				grid.Children.Add(textBox);

				// Color the current day
				if ((DateTime.Now.Month == ((int)Month) + 1) && (DateTime.Now.Day == (i + 1)))
					grid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 150));
				// Color days that contain holidays
				else if (!String.IsNullOrEmpty(holidayString))
					grid.Background = new SolidColorBrush(Color.FromArgb(255, 158, 211, 255));

				// Add the border to the core grid
				coreGrid.Children.Add(border);

				// Increment the current day
				if (currentDay != Day.Saturday)
					currentDay++;
				else
					currentDay = Day.Sunday;
			}

			int cell = (int)startDay + totalDays;
			int row = (int)(cell / 7);
			int column = cell % 7;
			while (row < 6) {
				// Create a border and set parameters
				Border border = new Border() {
					BorderBrush = new SolidColorBrush(Colors.Black),
					BorderThickness = new Thickness(0)
				};
				Grid.SetColumn(border, column);
				Grid.SetRow(border, row);

				// Create a grid
				Grid grid = new Grid();

				// Make the grid a child of the border
				border.Child = grid;

				// Color the current day
				grid.Background = new SolidColorBrush(Color.FromArgb(255, 224, 224, 224));

				// Add the border to the core grid
				coreGrid.Children.Add(border);

				// Increment the cell
				column++;
				if (column > 6) {
					column = 0;
					row++;
				}
			}
		}

		/// <summary>
		/// Occurs when the <see cref="MonthProperty"/> value is changed.
		/// </summary>
		/// <param name="d">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnMonthPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			CalendarPage control = d as CalendarPage;
			if (control != null)
				control.ClearAndPopulateCalendar();
		}

		/// <summary>
		/// Occurs when the <see cref="StartDayProperty"/> value is changed.
		/// </summary>
		/// <param name="d">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnStartDayPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			CalendarPage control = d as CalendarPage;
			if (control != null)
				control.ClearAndPopulateCalendar();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the holidays in this month.
		/// </summary>
		/// <value>The holidays in this month.</value>
		public DeferrableObservableCollection<Holiday> Holidays {
			get {
				return holidays;
			}
		}

		/// <summary>
		/// Gets or sets the associated month.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The associated month.
		/// The default value is <c>Month.January</c>.
		/// </value>
		public Month Month {
			get { return (Month)this.GetValue(CalendarPage.MonthProperty); }
			set { this.SetValue(CalendarPage.MonthProperty, value); }
		}

		/// <summary>
		/// Gets or sets the day that the month starts on.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The day that the month starts on.
		/// The default value is <c>Day.Sunday</c>.
		/// </value>
		public Day StartDay {
			get { return (Day)this.GetValue(CalendarPage.StartDayProperty); }
			set { this.SetValue(CalendarPage.StartDayProperty, value); }
		}
	}
}

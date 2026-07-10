using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.WallCalendar;

/// <summary>
/// A page from a calendar.
/// </summary>
public partial class CalendarPage : UserControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Month"/> property.
	/// </summary>
	public static readonly DependencyProperty MonthProperty
		= DependencyProperty.Register(nameof(Month), typeof(Month), typeof(CalendarPage), new FrameworkPropertyMetadata(defaultValue: Month.January, OnMonthPropertyValueChanged));

	/// <summary>
	/// Defines the <see cref="StartDay"/> property.
	/// </summary>
	public static readonly DependencyProperty StartDayProperty
		= DependencyProperty.Register(nameof(StartDay), typeof(Day), typeof(CalendarPage), new FrameworkPropertyMetadata(defaultValue: Day.Sunday, OnStartDayPropertyValueChanged));

	#endregion

	private readonly Dictionary<Month, int> _daysPerMonth = new() {
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

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CalendarPage() {
		InitializeComponent();
		Holidays.CollectionChanged += (_, _) => ClearAndPopulateCalendar();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Clears and populates the calendar.
	/// </summary>
	private void ClearAndPopulateCalendar() {
		coreGrid.Children.Clear();

		var month = Month;
		var startDay = StartDay;

		_daysPerMonth.TryGetValue(month, out var totalDays);
		if (totalDays == 0)
			return;

		// Populate blank day boxes
		var currentDay = Day.Sunday;
		for (var i = 0; i < (int)startDay; i++) {
			// Create a border and set parameters
			var border = new Border() {
				BorderBrush = new SolidColorBrush(Colors.Black),
				BorderThickness = new Thickness(0)
			};
			Grid.SetColumn(border, (int)currentDay);
			Grid.SetRow(border, 0);

			// Create a grid
			var grid = new Grid();

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
		for (var i = 0; i < totalDays; i++) {
			// Create a border and set parameters
			var border = new Border() {
				BorderBrush = new SolidColorBrush(Colors.Black),
				BorderThickness = new Thickness(1)
			};
			Grid.SetColumn(border, (int)currentDay);
			Grid.SetRow(border, (i + (int)startDay) / 7);

			// Create a grid and configure it
			var grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition() {
				Height = new GridLength(0, GridUnitType.Auto)
			});
			grid.RowDefinitions.Add(new RowDefinition());

			// Make the grid a child of the border
			border.Child = grid;

			// Create the text block that displays the day number and set parameters
			var textBox = new TextBlock() {
				Text = (i + 1).ToString(),
				FontSize = 10,
				Margin = new Thickness(1, 1, 0, 0)
			};

			// Make the text block that displays the day number a child of the grid
			grid.Children.Add(textBox);

			// Generate this day's holiday string
			var holidayString = "";
			foreach (var holiday in Holidays) {
				if (holiday.Day == i + 1)
					holidayString += holiday.HolidayName + Environment.NewLine;
			}
			if (!string.IsNullOrEmpty(holidayString))
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
			else if (!string.IsNullOrEmpty(holidayString))
				grid.Background = new SolidColorBrush(Color.FromArgb(255, 158, 211, 255));

			// Add the border to the core grid
			coreGrid.Children.Add(border);

			// Increment the current day
			if (currentDay != Day.Saturday)
				currentDay++;
			else
				currentDay = Day.Sunday;
		}

		var cell = (int)startDay + totalDays;
		var row = (cell / 7);
		var column = cell % 7;
		while (row < 6) {
			// Create a border and set parameters
			var border = new Border() {
				BorderBrush = new SolidColorBrush(Colors.Black),
				BorderThickness = new Thickness(0)
			};
			Grid.SetColumn(border, column);
			Grid.SetRow(border, row);

			// Create a grid
			var grid = new Grid();

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

	private static void OnMonthPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		var control = d as CalendarPage;
		control?.ClearAndPopulateCalendar();
	}

	private static void OnStartDayPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		var control = d as CalendarPage;
		control?.ClearAndPopulateCalendar();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The holidays in this month.
	/// </summary>
	public DeferrableObservableCollection<Holiday> Holidays { get; } = [];

	/// <summary>
	/// The associated month.
	/// </summary>
	/// <value>
	/// The default value is <see cref="Month.January"/>.
	/// </value>
	public Month Month {
		get => (Month)GetValue(MonthProperty);
		set => SetValue(MonthProperty, value);
	}

	/// <summary>
	/// The day that the month starts on.
	/// </summary>
	/// <value>
	/// The default value is <see cref="Day.Sunday"/>.
	/// </value>
	public Day StartDay {
		get => (Day)GetValue(StartDayProperty);
		set => SetValue(StartDayProperty, value);
	}

}

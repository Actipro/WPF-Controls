using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.DataAggregation {

	/// <summary>
	/// This QuickStart shows that the data rendered in a chart can be aggregated to reduce the number of data points, improving performance and readability. 
	/// Several built-in aggregation functions are provided, which include average, first, last, maximum, and minimum.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region DEPENDENCY PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Dependency Property for <see cref="IsAggregationEnabled"/>.
		/// </summary>
		public static readonly DependencyProperty IsAggregationEnabledProperty =
			DependencyProperty.Register("IsAggregationEnabled", typeof(bool), typeof(MainControl), new PropertyMetadata(false, OnIsAggregationEnabledChanged));

		/// <summary>
		/// Dependency Property for <see cref="SelectedAggregationKind"/>.
		/// </summary>
		public static readonly DependencyProperty SelectedAggregationKindProperty =
			DependencyProperty.Register("SelectedAggregationKind", typeof(AggregationKind), typeof(MainControl), new PropertyMetadata(AggregationKind.Average, OnSelectedAggregationKindChanged));

		/// <summary>
		/// Dependency Property for <see cref="SelectedSettings"/>.
		/// </summary>
		public static readonly DependencyProperty SelectedSettingsProperty =
			DependencyProperty.Register("SelectedSettings", typeof(IEnumerable<AggregationSetting>), typeof(MainControl), new PropertyMetadata(NoneSettings));

		#endregion DEPENDENCY PROPERTIES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			var generator = new TimeAggregatedDataGenerator {
				AllowNegativeNumbers = true,
				DataPointCount = 500, 
				StartAmount = 0, 
				StepRange = 10
			};
			generator.Generate();

			Items = generator;
			MaximumAmount = (decimal)Items.Max(x => x.Amount);
			MinimumAmount = (decimal)Items.Min(x => x.Amount);

			InitializeComponent();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Called when IsAggregationEnabled changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void OnIsAggregationEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args) {
			var mainControl = (MainControl)sender;
			mainControl.RefreshSelectedSettings((bool)args.NewValue, mainControl.SelectedAggregationKind);
		}

		/// <summary>
		/// Called when SelectedAggregationKind changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void OnSelectedAggregationKindChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args) {
			var mainControl = (MainControl)sender;
			mainControl.RefreshSelectedSettings(mainControl.IsAggregationEnabled, (AggregationKind)args.NewValue);
		}

		/// <summary>
		/// Refreshes the selected settings.
		/// </summary>
		/// <param name="isAggregationEnabled">if set to <c>true</c> then aggregation is enabled.</param>
		/// <param name="aggregationKind">The aggregation kind.</param>
		private void RefreshSelectedSettings(bool isAggregationEnabled, AggregationKind aggregationKind) {
			if (!isAggregationEnabled)
				SelectedSettings = NoneSettings;
			else if (aggregationKind == AggregationKind.Average)
				SelectedSettings = AverageSettings;
			else if (aggregationKind == AggregationKind.First)
				SelectedSettings = FirstSettings;
			else if (aggregationKind == AggregationKind.Last)
				SelectedSettings = LastSettings;
			else if (aggregationKind == AggregationKind.Maximum)
				SelectedSettings = MaximumSettings;
			else if (aggregationKind == AggregationKind.Minimum)
				SelectedSettings = MinimumSettings;
			else if (aggregationKind == AggregationKind.SignedMaximum)
				SelectedSettings = SignedMaximumSettings;
			else if (aggregationKind == AggregationKind.SignedMinimum)
				SelectedSettings = SignedMinimumSettings;
			else
				throw new NotImplementedException();
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets a value indicating whether aggregation is enabled.
		/// </summary>
		/// <value><c>true</c> if aggregation is enabled; otherwise, <c>false</c>.</value>
		public bool IsAggregationEnabled {
			get { return (bool)GetValue(IsAggregationEnabledProperty); }
			set { SetValue(IsAggregationEnabledProperty, value); }
		}

		/// <summary>
		/// Gets or sets the selected aggregation kind.
		/// </summary>
		/// <value>The selected aggregation kind.</value>
		public AggregationKind SelectedAggregationKind {
			get { return (AggregationKind)GetValue(SelectedAggregationKindProperty); }
			set { SetValue(SelectedAggregationKindProperty, value); }
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> AverageSettings {
			get {
				return new[] {
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Average, Factor = 0.05 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Average, Factor = 0.10 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Average, Factor = 0.25 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Average, Factor = 0.50 }
				};
			}
		}

		/// <summary>
		/// Gets the sales data.
		/// </summary>
		public IEnumerable<TimeAggregatedData> Items {
			get;
			private set;
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> FirstSettings {
			get {
				return new[] {
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.First, Factor = 0.05 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.First, Factor = 0.10 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.First, Factor = 0.25 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.First, Factor = 0.50 }
				};
			}
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> LastSettings {
			get {
				return new[] {
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Last, Factor = 0.05 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Last, Factor = 0.10 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Last, Factor = 0.25 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Last, Factor = 0.50 }
				};
			}
		}

		/// <summary>
		/// Gets the maximum sale amount.
		/// </summary>
		public decimal MaximumAmount {
			get;
			private set;
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> MaximumSettings {
			get {
				return new[] {
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Maximum, Factor = 0.05 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Maximum, Factor = 0.10 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Maximum, Factor = 0.25 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Maximum, Factor = 0.50 }
				};
			}
		}

		/// <summary>
		/// Gets the minimum sale amount.
		/// </summary>
		public decimal MinimumAmount {
			get;
			private set;
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> MinimumSettings {
			get {
				return new[] {
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Minimum, Factor = 0.05 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Minimum, Factor = 0.10 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Minimum, Factor = 0.25 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.Minimum, Factor = 0.50 }
				};
			}
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> NoneSettings {
			get {
				return new[] {
					new AggregationSetting { IsEnabled = false }
				};
			}
		}

		/// <summary>
		/// Gets or sets the selected settings.
		/// </summary>
		/// <value>The selected settings.</value>
		public IEnumerable<AggregationSetting> SelectedSettings {
			get { return (IEnumerable<AggregationSetting>)GetValue(SelectedSettingsProperty); }
			set { SetValue(SelectedSettingsProperty, value); }
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> SignedMaximumSettings {
			get {
				return new[] {
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.SignedMaximum, Factor = 0.05 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.SignedMaximum, Factor = 0.10 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.SignedMaximum, Factor = 0.25 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.SignedMaximum, Factor = 0.50 }
				};
			}
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> SignedMinimumSettings {
			get {
				return new[] {
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.SignedMinimum, Factor = 0.05 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.SignedMinimum, Factor = 0.10 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.SignedMinimum, Factor = 0.25 },
					new AggregationSetting { IsEnabled = true, Kind = AggregationKind.SignedMinimum, Factor = 0.50 }
				};
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}

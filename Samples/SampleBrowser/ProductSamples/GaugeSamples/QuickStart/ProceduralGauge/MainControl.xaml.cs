using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Gauge;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.ProceduralGauge {

	/// <summary>
	/// Interaction logic for MainControl.xaml
	/// </summary>
	public partial class MainControl {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Value"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Value"/> dependency property.</value>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(42.0));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainControl"/> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.Loaded += this.OnLoaded;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Called when the control has loaded.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void OnLoaded(object sender, EventArgs e) {
			// Create the main CircularGauge and add it to the target panel
			CircularGauge circularGauge = new CircularGauge() {
				Width = 220,
				Height = 220,
				Radius = 110,
				HorizontalAlignment = HorizontalAlignment.Center,
				Background = new SolidColorBrush(Color.FromArgb(0xff, 0xee, 0xee, 0xe3)),
				RimBrush = new SolidColorBrush(Color.FromArgb(0xff, 0xf4, 0xf3, 0xf8)),
				FrameType = CircularFrameType.CircularGear,
				IsBackgroundEffectEnabled = false,
			};
			this.targetPanel.Children.Insert(0, circularGauge);

			// Create and add a CircularScale to the CircularGauge
			CircularScale circularScale = new CircularScale() {
				Radius = 75,
				StartAngle = 30,
				SweepAngle = 330,
				BarExtent = 1
			};
			circularGauge.Scales.Add(circularScale);

			// Create and add a CircularTickSet to the CircularScale
			CircularTickSet circularTickSet = new CircularTickSet() {
				MajorInterval = 10,
				MinorInterval = 2
			};
			circularScale.TickSets.Add(circularTickSet);

			// Create and add two CircularRanges to the CircularTickSet
			circularTickSet.Ranges.Add(new CircularRange() {
				ScalePlacement = ScalePlacement.Inside,
				StartValue = 80,
				EndValue = 100,
				HasDropShadow = false,
				StartExtent = 15,
				EndExtent = 15,
				Background = LinearGradientBrushExtension.CreateBrush(Colors.Red, Colors.DarkRed, LinearGradientType.LeftToRight),
			});
			circularTickSet.Ranges.Add(new CircularRange() {
				ScalePlacement = ScalePlacement.Inside,
				StartValue = 0,
				EndValue = 20,
				HasDropShadow = false,
				StartExtent = 15,
				EndExtent = 15,
				Background = LinearGradientBrushExtension.CreateBrush(Colors.Green, Colors.DarkGreen, LinearGradientType.LeftToRight)
			});

			// Create and add a CircularTickMarkMajor and CircularTickMarkMinor to the CircularTickSet
			circularTickSet.Ticks.Add(new CircularTickMarkMajor() {
				TickMarkExtent = 10,
				TickMarkAscent = 4,
				TickMarkType = TickMarkType.SwordBlunt,
				Background = LinearGradientBrushExtension.CreateBrush(Colors.Black, Colors.DarkGray, LinearGradientType.TopToBottom)
			});
			circularTickSet.Ticks.Add(new CircularTickMarkMinor() {
				TickMarkExtent = 7,
				TickMarkAscent = 4,
				TickMarkType = TickMarkType.TriangleSharp,
				Background = LinearGradientBrushExtension.CreateBrush(Colors.Black, Colors.DarkGray, LinearGradientType.TopToBottom)
			});

			// Create and add a CircularTickLabelMajor to the CircularTickSet
			circularTickSet.Ticks.Add(new CircularTickLabelMajor() {
				Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x0c, 0x09, 0x09)),
				FontSize = 10,
				ScalePlacement = ScalePlacement.Outside,
				ScaleOffset = 5,
				TextOrientation = TextOrientation.Rotated
			});

			// Create and add a CircularPointerNeedle and CircularPointerCap to the CircularTickSet
			CircularPointerNeedle circularPointerNeedle = new CircularPointerNeedle() {
				NeedleType = PointerNeedleType.TriangleSharp,
				PointerExtent = 75,
				PointerAscent = 10,
				Background = LinearGradientBrushExtension.CreateBrush(Color.FromArgb(0xff, 0xe1, 0x61, 0x79), Color.FromArgb(0xff, 0x9a, 0x12, 0x25), LinearGradientType.LeftToRight),
			};
			circularTickSet.Pointers.Add(circularPointerNeedle);

			// Binding the needle to the value slider
			circularPointerNeedle.SetBinding(CircularPointerNeedle.ValueProperty, new Binding("Value") {
				Source = this,
				Mode = BindingMode.TwoWay
			});

			// Create and add a CircularPointerCap to the CircularTickSet
			circularTickSet.Pointers.Add(new CircularPointerCap() {
				PointerExtent = 25,
				CapType = PointerCapType.CircleConvex,
				Background = new SolidColorBrush(Color.FromArgb(0xff, 0x9a, 0x12, 0x25))
			});
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public double Value {
			get { return (double)this.GetValue(ValueProperty); }
			set { this.SetValue(ValueProperty, value); }
		}
		
	}
}

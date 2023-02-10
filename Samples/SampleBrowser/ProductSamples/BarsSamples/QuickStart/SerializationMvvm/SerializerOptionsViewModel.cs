using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.SerializationMvvm {

	/// <summary>
	/// Defines a view model for turning options on or off that will be used during Ribbon serialization.
	/// </summary>
	public class SerializerOptionsViewModel : ObservableObjectBase {

		private bool layoutMode = true;
		private bool minimizedStated = true;
		private bool quickAccessToolBarItems = true;
		private bool quickAccessToolBarLocation = true;
		private bool quickAccessToolBarMode = true;
		private bool userInterfaceDensity = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="RibbonSerializerOptions"/> that are reflected by the current configuration.
		/// </summary>
		/// <returns>One or more of the <see cref="RibbonSerializerOptions"/> values.</returns>
		public RibbonSerializerOptions CreateOptions() {
			// Start with no options enabled
			var options = RibbonSerializerOptions.None;

			// Add each option that is currently turned on
			if (LayoutMode)
				options |= RibbonSerializerOptions.LayoutMode;
			if (MinimizedState)
				options |= RibbonSerializerOptions.MinimizedState;
			if (QuickAccessToolBarItems)
				options |= RibbonSerializerOptions.QuickAccessToolBarItems;
			if (QuickAccessToolBarLocation)
				options |= RibbonSerializerOptions.QuickAccessToolBarLocation;
			if (QuickAccessToolBarMode)
				options |= RibbonSerializerOptions.QuickAccessToolBarMode;
			if (UserInterfaceDensity)
				options |= RibbonSerializerOptions.UserInterfaceDensity;

			return options;
		}

		/// <summary>
		/// Gets or sets if <see cref="Ribbon.LayoutMode"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool LayoutMode {
			get => layoutMode;
			set {
				if (layoutMode != value) {
					layoutMode = value;
					NotifyPropertyChanged(nameof(LayoutMode));
				}
			}
		}

		/// <summary>
		/// Gets or sets if <see cref="Ribbon.IsMinimized"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool MinimizedState {
			get => minimizedStated;
			set {
				if (minimizedStated != value) {
					minimizedStated = value;
					NotifyPropertyChanged(nameof(MinimizedState));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the items displayed in <see cref="Ribbon.QuickAccessToolBar"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool QuickAccessToolBarItems {
			get => quickAccessToolBarItems;
			set {
				if (quickAccessToolBarItems != value) {
					quickAccessToolBarItems = value;
					NotifyPropertyChanged(nameof(QuickAccessToolBarItems));
				}
			}
		}

		/// <summary>
		/// Gets or sets if <see cref="Ribbon.QuickAccessToolBarLocation"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool QuickAccessToolBarLocation {
			get => quickAccessToolBarLocation;
			set {
				if (quickAccessToolBarLocation != value) {
					quickAccessToolBarLocation = value;
					NotifyPropertyChanged(nameof(QuickAccessToolBarLocation));
				}
			}
		}

		/// <summary>
		/// Gets or sets if <see cref="Ribbon.QuickAccessToolBarMode"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool QuickAccessToolBarMode {
			get => quickAccessToolBarMode;
			set {
				if (quickAccessToolBarMode != value) {
					quickAccessToolBarMode = value;
					NotifyPropertyChanged(nameof(QuickAccessToolBarMode));
				}
			}
		}

		/// <summary>
		/// Gets or sets if <see cref="Ribbon.UserInterfaceDensity"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		/// <value><c>true</c> to process the option; otherwise <c>false</c> to ignore it.</value>
		public bool UserInterfaceDensity {
			get => userInterfaceDensity;
			set {
				if (userInterfaceDensity != value) {
					userInterfaceDensity = value;
					NotifyPropertyChanged(nameof(UserInterfaceDensity));
				}
			}
		}

	}
}

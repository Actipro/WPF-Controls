using ActiproSoftware.Windows.Controls;
using System;
using System.Windows;
using System.Windows.Threading;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SharedSamples.Demo.FileCopyDialog {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/// <summary>
		/// Holds a <see cref="DispatchTimer"/> used to simulate a file copy operation.
		/// </summary>
		private DispatcherTimer fileCopyTimer;

		/// <summary>
		/// Holds a random number generator.
		/// </summary>
		private Random random = new Random();

		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="CanSimulateAnError"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="CanSimulateAnError"/> dependency property.</value>
		public static readonly DependencyProperty CanSimulateAnErrorProperty = DependencyProperty.Register("CanSimulateAnError", typeof(bool), typeof(MainControl), new FrameworkPropertyMetadata(true));

		/// <summary>
		/// Identifies the <see cref="CanSimulateAPause"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="CanSimulateAPause"/> dependency property.</value>
		public static readonly DependencyProperty CanSimulateAPauseProperty = DependencyProperty.Register("CanSimulateAPause", typeof(bool), typeof(MainControl), new FrameworkPropertyMetadata(true));

		/// <summary>
		/// Identifies the <see cref="FileCopyData"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="FileCopyData"/> dependency property.</value>
		public static readonly DependencyProperty FileCopyDataProperty = DependencyProperty.Register("FileCopyData", typeof(FileCopyData), typeof(MainControl), new FrameworkPropertyMetadata(null));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			this.FileCopyData = new FileCopyData();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a <see cref="FileCopyData"/> with test values.
		/// </summary>
		/// <returns></returns>
		private static FileCopyData CreateSampleFileCopyData() {
			FileCopyData data = new FileCopyData();
			data.TotalFileCount = data.RemainingFileCount = 51;
			data.TotalFileSize = data.RemainingFileSize = 59.3;
			data.TimeRemaining = new TimeSpan(0, 15, 45);
			return data;
		}

		/// <summary>
		/// Handles the Click event of the cancel button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnCancelButtonClick(object sender, RoutedEventArgs e) {
			if (null != this.fileCopyTimer) {
				this.fileCopyTimer.Stop();
				this.fileCopyTimer.Tick -= new EventHandler(OnFileCopyTimerTick);
				this.fileCopyTimer = null;
			}
		}

		/// <summary>
		/// Handles the Tick event of the fileCopyTimer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnFileCopyTimerTick(object sender, EventArgs e) {
			if ((null != this.fileCopyTimer) && (0 == this.FileCopyData.RemainingFileCount)) {
				this.fileCopyTimer.Stop();
				this.fileCopyTimer.Tick -= new EventHandler(OnFileCopyTimerTick);
				this.fileCopyTimer = null;
			}
			else {
				SimulateFileCopy();
			}
		}

		/// <summary>
		/// Handles the Click event of the start button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnStartButtonClick(object sender, RoutedEventArgs e) {
			this.FileCopyData = CreateSampleFileCopyData();

			this.fileCopyTimer = new DispatcherTimer();
			this.fileCopyTimer.Interval = TimeSpan.FromMilliseconds(100);
			this.fileCopyTimer.Tick += new EventHandler(OnFileCopyTimerTick);
			this.fileCopyTimer.IsEnabled = true;
		}

		/// <summary>
		/// Simulates the file copy.
		/// </summary>
		private void SimulateFileCopy() {
			FileCopyData fileCopyData = this.FileCopyData;
			if (0 != fileCopyData.RemainingFileCount) {

				if (this.CanSimulateAPause && (40 == fileCopyData.RemainingFileCount)) {
					this.animatedProgressBar.State = OperationState.Paused;
					MessageBox.Show("The file 'xyz.txt' already exists, would you like to overwrite it?",
						"File Copy (Simulated)", MessageBoxButton.YesNo, MessageBoxImage.Question);
					this.animatedProgressBar.State = OperationState.Normal;
				}
				else if (this.CanSimulateAnError && (30 == fileCopyData.RemainingFileCount)) {
					this.animatedProgressBar.State = OperationState.Error;
					MessageBox.Show("An error occurred while copying the file 'abc.txt'.",
						"File Copy (Simulated)", MessageBoxButton.OK, MessageBoxImage.Error);
					this.animatedProgressBar.State = OperationState.Normal;
				}

				double fileTime = fileCopyData.TimeRemaining.TotalSeconds / fileCopyData.RemainingFileCount;
				double fileSize = fileCopyData.RemainingFileSize / fileCopyData.RemainingFileCount;
				fileCopyData.RemainingFileCount--;
				fileCopyData.RemainingFileSize -= fileSize;
				fileCopyData.CopiedFileSize = fileCopyData.TotalFileSize - fileCopyData.RemainingFileSize;
				fileCopyData.Speed = ((double)this.random.Next(6000) + 3000) / 100.0;
				fileCopyData.TimeRemaining -= TimeSpan.FromSeconds(fileTime);
			}
			this.FileCopyData = fileCopyData;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether a pause can be simulated.
		/// </summary>
		/// <value>
		/// <c>true</c> if a pause can be simulated; otherwise, <c>false</c>.
		/// </value>
		public bool CanSimulateAPause {
			get {
				return (bool)GetValue(CanSimulateAPauseProperty);
			}
			set {
				SetValue(CanSimulateAPauseProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets whether an error can be simulated.
		/// </summary>
		/// <value>
		/// <c>true</c> if an error can be simulated; otherwise, <c>false</c>.
		/// </value>
		public bool CanSimulateAnError {
			get {
				return (bool)GetValue(CanSimulateAnErrorProperty);
			}
			set {
				SetValue(CanSimulateAnErrorProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the file copy meta-data associated with this control.
		/// </summary>
		/// <value>The file copy meta-data associated with this control.</value>
		public FileCopyData FileCopyData {
			get {
				return (FileCopyData)GetValue(FileCopyDataProperty);
			}
			set {
				SetValue(FileCopyDataProperty, value);
			}
		}

	}

}
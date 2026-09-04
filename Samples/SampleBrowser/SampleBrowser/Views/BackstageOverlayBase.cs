using ActiproSoftware.Windows.Controls.Views;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides a base class for a Backstage overlay.
/// </summary>
public partial class BackstageOverlayBase : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public BackstageOverlayBase() {
		DataContextChanged += OnDataContextChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnDataContextChanged(object? sender, DependencyPropertyChangedEventArgs e) {
		if (e.OldValue is ApplicationViewModel oldViewModel)
			oldViewModel.PropertyChanged -= OnViewModelPropertyChanged;

		if (e.NewValue is ApplicationViewModel newViewModel)
			newViewModel.PropertyChanged += OnViewModelPropertyChanged;
	}

	private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		switch (e.PropertyName) {
			case nameof(ApplicationViewModel.IsBackstageOpen):
				if (ViewModel.IsBackstageOpen)
					ScrollViewer?.ScrollToTop(TimeSpan.Zero);
				break;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The primary scroll viewer.
	/// </summary>
	public virtual InertiaScrollViewer? ScrollViewer
		=> null;

	/// <summary>
	/// The view-model for this view.
	/// </summary>
	public ApplicationViewModel ViewModel
		=> (ApplicationViewModel)DataContext;

}

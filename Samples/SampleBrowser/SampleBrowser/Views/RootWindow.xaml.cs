using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using System.Windows.Threading;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides the root window for the application.
/// </summary>
public partial class RootWindow {

	private ApplicationOverlayMode _currentOverlayMode;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public RootWindow() {
		InitializeComponent();

		UpdateOverlayUI();

		ViewModel.PropertyChanged += OnViewModelPropertyChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void FocusContentElement(FrameworkElement contentElement) {
		// If a DockSite is the first focusable element, try to activate a docking window
		var firstFocusableElement = VisualTreeHelperExtended.GetFirstFocusableDescendant(contentElement) as FrameworkElement;
		if (firstFocusableElement is DockSite dockSite) {
			if (dockSite.Documents.Count > 0) {
				dockSite.ActivatePrimaryDocument();
				return;
			}
			if (dockSite.ToolWindows.Count > 0) {
				dockSite.ToolWindows[0].Activate();
				return;
			}
		}

		// Move focus to the first focusable element (if it is still in the presenter)
		contentElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
	}

	private void OnTransitionPresenterTransitionCompleted(object sender, RoutedPropertyChangedEventArgs<object> e) {
		if (e.OriginalSource == rootPresenter) {
			if (rootPresenter.Content is FrameworkElement contentElement) {
				Dispatcher.BeginInvoke(DispatcherPriority.Input, () => {
					if ((ViewModel.ViewItemInfo is { CanFocusOnLoad: true }) && IsActive && (rootPresenter.Content == contentElement)) {
						// There is no need to focus within the content element if focus is already there
						if (!contentElement.IsKeyboardFocusWithin)
							FocusContentElement(contentElement);
					}
				});
			}
		}
	}

	private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		switch (e?.PropertyName) {
			case nameof(ApplicationViewModel.IsBackstageOpen): {
				var rootPresenterIsVisible = !(ViewModel.ViewHasInterop && ViewModel.IsBackstageOpen);
				rootPresenter.Visibility = (rootPresenterIsVisible ? Visibility.Visible : Visibility.Hidden);
				break;
			}
			case nameof(ApplicationViewModel.OverlayMode):
				UpdateOverlayUI();
				break;
		}
	}

	/// <summary>
	/// Updates the overlay UI.
	/// </summary>
	private void UpdateOverlayUI() {
		// When the overlay mode changes, ensure we clear the overlay content to ensure a switch between the Backstage modes executes the template selector again
		var viewModel = ViewModel;
		if (_currentOverlayMode != viewModel.OverlayMode) {
			WindowChrome.SetOverlayContent(this, null);
			_currentOverlayMode = viewModel.OverlayMode;
		}

		switch (viewModel.OverlayMode) {
			case ApplicationOverlayMode.ExternalSample:
				WindowChrome.SetOverlayAnimationKinds(this, OverlayAnimationKinds.Fade);
				WindowChrome.SetOverlayContent(this, "Loading external sample...");
				SetBinding(WindowChrome.IsOverlayVisibleProperty, new Binding() { Source = ViewModel, Path = new PropertyPath(nameof(ApplicationViewModel.IsLoadingExternalSample)), Mode = BindingMode.TwoWay });
				BindingOperations.ClearBinding(this, WindowChrome.UseAlternateTitleBarStyleProperty);
				break;
			default:  // Backstage (all kinds)
				WindowChrome.SetOverlayAnimationKinds(this, OverlayAnimationKinds.FadeSlide);
				WindowChrome.SetOverlayContent(this, viewModel);
				SetBinding(WindowChrome.IsOverlayVisibleProperty, new Binding() { Source = ViewModel, Path = new PropertyPath(nameof(ApplicationViewModel.IsBackstageOpen)), Mode = BindingMode.TwoWay });
				SetBinding(WindowChrome.UseAlternateTitleBarStyleProperty, new Binding() { Source = ViewModel, Path = new PropertyPath(nameof(ApplicationViewModel.IsBackstageOpen)), Mode = BindingMode.TwoWay });
				break;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnKeyDown(KeyEventArgs e) {
		base.OnKeyDown(e);

		if (!e.Handled) {
			switch (e.Key) {
				case Key.Escape:
					// Ensure the Backstage is closed when Esc is pressed
					ViewModel.IsBackstageOpen = false;
					break;
				case Key.F9:
					// Write out the focused element
					Debug.WriteLine(DateTime.Now + "RootWindow.OnKeyDown: FocusedElement=" + Keyboard.FocusedElement);
					break;
			}
		}
	}

	/// <inheritdoc/>
	protected override void OnMouseUp(MouseButtonEventArgs e) {
		base.OnMouseUp(e);

		// Look for unhandled navigation buttons
		if (!e.Handled) {
			switch (e.ChangedButton) {
				case MouseButton.XButton1:
					ViewModel.NavigateViewBackward();
					break;
				case MouseButton.XButton2:
					ViewModel.NavigateViewForward();
					break;
			}
		}
	}

	/// <summary>
	/// The view-model for this view.
	/// </summary>
	public ApplicationViewModel ViewModel
		=> (ApplicationViewModel)DataContext;

}

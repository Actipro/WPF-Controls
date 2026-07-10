using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditorViewsSynchronizing;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	private bool _isSynchronizingSplitter;
	private bool _isSynchronizingEditorView;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Listen for events to be raised by each editor to keep them in sync
		AddEditorEventHandlers(leftEditor);
		AddEditorEventHandlers(rightEditor);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds event handlers for an editor.
	/// </summary>
	/// <param name="editor">The editor.</param>
	private void AddEditorEventHandlers(SyntaxEditor editor) {
		// Listen for changes in editor splitter
		editor.ViewSplitAdded += OnViewSplitChanged;
		editor.ViewSplitRemoved += OnViewSplitChanged;
		editor.ViewSplitMoved += OnViewSplitChanged;

		// Set event handlers for the active view and listen for views opened/closed
		//   so event handlers for those views can be added/removed
		SetEditorViewEventHandlers(editor.ActiveView, addHandlers: true);
		editor.ViewOpened += OnViewOpened;
		editor.ViewClosed += OnViewClosed;
	}

	/// <summary>
	/// Occurs when a view is closed in the editor (e.g., split removed).
	/// </summary>
	private void OnViewClosed(object? sender, TextViewEventArgs e) {
		// Remove event handlers from the view which was closed
		SetEditorViewEventHandlers((IEditorView)e.View, addHandlers: false);
	}

	/// <summary>
	/// Occurs when a view is opened in the editor (e.g., split added).
	/// </summary>
	private void OnViewOpened(object? sender, TextViewEventArgs e) {
		// Add event handlers to the view which was opened
		SetEditorViewEventHandlers((IEditorView)e.View, addHandlers: true);
	}

	private void OnViewSplitChanged(object sender, RoutedEventArgs e) {
		// Get the editor which changed
		var sourceEditor = (SyntaxEditor)sender;

		// Get the other editor to be synchronized
		var targetEditor = ResolveOtherEditor(sourceEditor);

		// Synchronize the splitter
		SynchronizeSplitter(sourceEditor, targetEditor);
	}

	private void OnViewTextAreaLayout(object? sender, TextViewTextAreaLayoutEventArgs e) {
		// Get the view which was updated
		var sourceView = (IEditorView)sender!;

		// Get the editor which owns the updated view
		var sourceEditor = sourceView.SyntaxEditor;

		// Get the other editor to be synchronized
		var targetEditor = ResolveOtherEditor(sourceEditor);

		// Get the view in the other other which is located at the same position as the source view (to support split views)
		var targetView = targetEditor.GetView(sourceView.Placement);

		// Synchronize the views
		if ((sourceView is not null) && (targetView is not null))
			SynchronizeEditorView(sourceView, targetView);
	}

	/// <summary>
	/// Resolve the editor (left or right) which is different from the reference editor.
	/// </summary>
	/// <param name="referenceEditor">The editor to examine.</param>
	/// <returns>
	/// The left editor when <paramref name="referenceEditor"/> is the right editor, or
	/// the right editor when <paramref name="referenceEditor"/> is the left editor.
	/// </returns>
	private SyntaxEditor ResolveOtherEditor(SyntaxEditor referenceEditor) {
		// Return the editor which was not passed as the reference (left returns right, right returns left)
		return (referenceEditor == rightEditor)
			? leftEditor
			: rightEditor;
	}

	/// <summary>
	/// Adds or removes event handlers for an editor's views.
	/// </summary>
	/// <param name="editorView">The editor's view.</param>
	/// <param name="addHandlers"><c>true</c> to add event handlers; otherwise <c>false</c> to remove them.</param>
	private void SetEditorViewEventHandlers(IEditorView editorView, bool addHandlers) {
		if (editorView is null)
			return;

		// The TextAreaLayout event is raised after a layout has been updated (which can be caused by scrolling) and
		//   will be used to trigger synchronization of the views
		if (addHandlers)
			editorView.TextAreaLayout += OnViewTextAreaLayout;
		else
			editorView.TextAreaLayout -= OnViewTextAreaLayout;
	}

	/// <summary>
	/// Updates the the zoom level and scroll state of the target editor view to match the source editor view.
	/// </summary>
	/// <param name="sourceView">The source view whose state is to be copied.</param>
	/// <param name="targetView">The target view to be updated.</param>
	private void SynchronizeEditorView(IEditorView sourceView, IEditorView targetView) {
		// Prevent re-entry
		if (_isSynchronizingEditorView)
			return;

		_isSynchronizingEditorView = true;
		try {
			// Sync zoom level
			if (targetView.ZoomLevel != sourceView.ZoomLevel)
				targetView.SyntaxEditor.ZoomLevel = sourceView.SyntaxEditor.ZoomLevel;

			// Sync scroll state
			if (targetView.ScrollState != sourceView.ScrollState)
				targetView.Scroller.ScrollTo(sourceView.ScrollState);
		}
		finally {
			_isSynchronizingEditorView = false;
		}
	}

	/// <summary>
	/// Updates the splitter state of the target editor to match the source editor.
	/// </summary>
	/// <param name="sourceEditor">The source editor whose state is to be copied.</param>
	/// <param name="targetEditor">The target editor to be updated.</param>
	private void SynchronizeSplitter(SyntaxEditor sourceEditor, SyntaxEditor targetEditor) {
		// Prevent re-entry
		if (_isSynchronizingSplitter)
			return;

		_isSynchronizingSplitter = true;
		try {
			// Sync splitter presence
			if (targetEditor.HasHorizontalSplit != sourceEditor.HasHorizontalSplit)
				targetEditor.HasHorizontalSplit = sourceEditor.HasHorizontalSplit;

			// Sync splitter position
			if (targetEditor.HorizontalSplitPercentage != sourceEditor.HorizontalSplitPercentage)
				targetEditor.HorizontalSplitPercentage = sourceEditor.HorizontalSplitPercentage;
		}
		finally {
			_isSynchronizingSplitter = false;
		}
	}

}

using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet.Resolution;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.Rendering;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddonGoToDefinition;

/// <summary>
/// A custom tagger that adds a custom <see cref="IClassificationTag"/> over tokens that might be
/// valid for "Go To Definition" functionality and provides special handling for clicking over
/// those tags.
/// </summary>
public class GoToDefinitionTagger : TaggerBase<IClassificationTag> {

	private TextSnapshotRange? _currentTaggedSnapshotRange;
	private readonly GoToDefinitionService? _goToDefinitionService;
	private bool _isCustomCursorActive;
	private readonly IEditorView _view;

	private static readonly Color _defaultDarkForegroundColor = UIColor.FromWebColor("#569cd6");
	private static readonly Color _defaultLightForegroundColor = UIColor.FromWebColor("#0000ff");
	private static readonly ClassificationType _goToDefinitionClassificationType = new("GoToDefinition", "Go To Definition");

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The view to which this manager is attached.</param>
	public GoToDefinitionTagger(IEditorView view) : base(nameof(GoToDefinitionTagger), [new Ordering(TaggerKeys.Token, OrderPlacement.Before)], view.SyntaxEditor.Document) {
		_view = view ?? throw new ArgumentNullException(nameof(view));

		// Ensure a classification type is registered to apply styles to tags and listen for theme changes
		EnsureClassificationTypeRegistered();
		ThemeManager.CurrentThemeChanged += OnThemeManagerCurrentThemeChanged;

		// Get the required service
		_goToDefinitionService = view.SyntaxEditor.Document.Language.GetService<GoToDefinitionService>();
		Debug.Assert(_goToDefinitionService is not null);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Configures the view with a custom cursor to indicate the tag can be clicked.
	/// </summary>
	private void ApplyCustomCursor() {
		if (!_isCustomCursorActive) {
			Mouse.OverrideCursor = Cursors.Hand;
			_isCustomCursorActive = true;
		}
	}

	/// <summary>
	/// Clears the custom cursor configuration from the view.
	/// </summary>
	private void ClearCustomCursor() {
		if (_isCustomCursorActive) {
			Mouse.OverrideCursor = null;
			_isCustomCursorActive = false;
		}
	}

	/// <summary>
	/// Tests if the given key is one that could impact the tagging process.
	/// </summary>
	/// <param name="key">The key to be tested.</param>
	private static bool DoesKeyImpactTagging(Key key) {
		// Any of the modifier keys can impact the tagging process
		return key == System.Windows.Input.Key.LeftCtrl
			|| key == System.Windows.Input.Key.RightCtrl
			|| key == System.Windows.Input.Key.LeftShift
			|| key == System.Windows.Input.Key.RightShift
			|| key == System.Windows.Input.Key.LeftAlt
			|| key == System.Windows.Input.Key.RightAlt;
	}

	/// <summary>
	/// Ensures a <see cref="IClassificationType"/> is registered with a <see cref="IHighlightingStyle"/> for the tags created by this tagger.
	/// </summary>
	private static void EnsureClassificationTypeRegistered() {
		var registry = AmbientHighlightingStyleRegistry.Instance;
		if (registry.GetClassificationType(_goToDefinitionClassificationType.Key) is null) {
			// Configure light/dark color palettes with default colors
			registry.LightColorPalette?.SetForeground(_goToDefinitionClassificationType.Key, _defaultLightForegroundColor);
			registry.DarkColorPalette?.SetForeground(_goToDefinitionClassificationType.Key, _defaultDarkForegroundColor);

			// Define a style with the underline decoration
			var style = new HighlightingStyle() { UnderlineKind = LineKind.Solid };

			// Associate the style with the classification type
			//   and the current color palette color will be automatically applied
			registry.Register(_goToDefinitionClassificationType, style);
		}
	}

	/// <summary>
	/// Invalidates any previously defined tags.
	/// </summary>
	/// <param name="snapshotRange">The invalidated snapshot range.</param>
	private void InvalidateTags(TextSnapshotRange? snapshotRange) {
		// Quit if the range is undefined or zero-length
		if (!snapshotRange.HasValue || snapshotRange.Value.IsZeroLength)
			return;

		// Notify that tags have changed for this range
		OnTagsChanged(new TagsChangedEventArgs(snapshotRange.Value));
	}

	/// <summary>
	/// Indicates whether tagging is currently active.
	/// </summary>
	private static bool IsTaggingActive {
		// Tagging is only performed when just the CTRL key is pressed
		get => Keyboard.Modifiers == ModifierKeys.Control;
	}

	private void OnThemeManagerCurrentThemeChanged(object? sender, EventArgs e)
		=> EnsureClassificationTypeRegistered();

	/// <summary>
	/// Updates the tagged range for a "Go To Definition" tag.
	/// </summary>
	/// <param name="cursorLocation">The current cursor location.</param>
	/// <returns><c>true</c> if a valid tagged range was detected; otherwise <c>false</c>.</returns>
	private bool UpdateTaggedRange(Point cursorLocation)
		=> UpdateTaggedRange(cursorLocation, out _);

	/// <summary>
	/// Updates the tagged range for a "Go To Definition" tag.
	/// </summary>
	/// <param name="cursorLocation">The current cursor location.</param>
	/// <param name="resolverResult">Outputs the result of a resolver operation, if any, that was found for a successfully tagged range.</param>
	/// <returns><c>true</c> if a valid tagged range was detected; otherwise <c>false</c>.</returns>
	private bool UpdateTaggedRange(Point cursorLocation, out IResolverResult? resolverResult) {
		// Initialize output argument
		resolverResult = null;

		// Cache the current snapshot range
		var oldSnapshotRange = _currentTaggedSnapshotRange;

		// Reset the snapshot range
		_currentTaggedSnapshotRange = null;

		// Only perform additional testing if tagging is active
		if (IsTaggingActive) {

			// Check if the mouse is currently positioned a character in the text area of the view
			var hitTestResult = _view.SyntaxEditor.HitTest(cursorLocation);
			if (hitTestResult.Type == HitTestResultType.ViewTextAreaOverCharacter) {

				// Find the token whose range can be tagged and ensure it's for an identifier or keyword
				var currentToken = hitTestResult.Snapshot!.GetReader(hitTestResult.Offset).Token;
				if (
					currentToken is not null
					&& (CSharpTokenId.IsIdentifierClassificationType(currentToken.Id) || CSharpTokenId.IsKeywordClassificationType(currentToken.Id))
				) {

					// Attempt to resolve the item at this position so tags are only placed over items which can be resolved
					var snapshotOffset = new TextSnapshotOffset(hitTestResult.Snapshot, hitTestResult.Offset);
					var resolutionResultSet = _goToDefinitionService?.PerformResolution(snapshotOffset);
					if (resolutionResultSet is { Results.Count: > 0 }) {

						// Update the currently tagged snapshot range to match the token
						_currentTaggedSnapshotRange = new TextSnapshotRange(hitTestResult.Snapshot, currentToken.StartOffset, currentToken.EndOffset);

						// Output the resolver result
						resolverResult = resolutionResultSet.Results[0];

					}

				}

			}

		}

		if (oldSnapshotRange != _currentTaggedSnapshotRange) {
			// Notify tags have changed
			InvalidateTags(oldSnapshotRange);
			InvalidateTags(_currentTaggedSnapshotRange);
		}

		if (resolverResult is null) {
			// Clear any custom cursor when the pointer is not over a successfully tagged range
			ClearCustomCursor();
			return false;
		}
		else {
			// Apply a custom cursor when the pointer is over a successfully tagged range
			ApplyCustomCursor();
			return true;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<TagSnapshotRange<IClassificationTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		// Quit if the range is undefined or zero-length
		if (!_currentTaggedSnapshotRange.HasValue || _currentTaggedSnapshotRange.Value.IsZeroLength)
			yield break;

		// Loop through the requested snapshot ranges...
		foreach (var snapshotRange in snapshotRanges) {
			if (
				!snapshotRange.IsZeroLength
				&& snapshotRange.Contains(_currentTaggedSnapshotRange.Value)
			) {
				// Yield a tag for the current range
				yield return new TagSnapshotRange<IClassificationTag>(
					_currentTaggedSnapshotRange.Value,
					new ClassificationTag(_goToDefinitionClassificationType)
				);

				// There is only ever a single tag, so no additional processing is necessary
				yield break;
			}
		}
	}

	/// <summary>
	/// Notifies when a key is pressed down while focus is in the <see cref="IEditorView"/> for this tagger.
	/// </summary>
	/// <param name="e">The event data.</param>
	public void NotifyKeyDown(KeyEventArgs e) {
		if (DoesKeyImpactTagging(e.Key)) {
			// The state of the modifier key has changed, so tagging should be updated
			UpdateTaggedRange(Mouse.GetPosition(_view.SyntaxEditor));
		}
	}

	/// <summary>
	/// Notifies when a key is released while focus is in the <see cref="IEditorView"/> for this tagger.
	/// </summary>
	/// <param name="e">The event data.</param>
	public void NotifyKeyUp(KeyEventArgs e) {
		if (DoesKeyImpactTagging(e.Key)) {
			// The state of the modifier key has changed, so tagging should be updated
			UpdateTaggedRange(Mouse.GetPosition(_view.SyntaxEditor));
		}
	}

	/// <summary>
	/// Occurs when the pointer leaves the <see cref="IEditorView"/> for this tagger.
	/// </summary>
	/// <param name="e">The event data.</param>
	public void NotifyPointerExited(InputPointerEventArgs e) {
		// Make sure tags and custom cursors are cleared when the mouse leaves
		InvalidateTags(_currentTaggedSnapshotRange);
		_currentTaggedSnapshotRange = null;
		ClearCustomCursor();
	}

	/// <summary>
	/// Occurs when the pointer is moved over the <see cref="IEditorView"/> for this tagger.
	/// </summary>
	/// <param name="e">The event data.</param>
	public void NotifyPointerMoved(InputPointerEventArgs e) {
		// Update the tagged range for the current mouse position
		UpdateTaggedRange(e.GetPosition(_view.SyntaxEditor));
	}

	/// <summary>
	/// Occurs when a pointer button is pressed over the <see cref="IEditorView"/> for this tagger.
	/// </summary>
	/// <param name="e">The event data.</param>
	public void NotifyPointerPressed(InputPointerButtonEventArgs e) {
		if ((e.ButtonKind == InputPointerButtonKind.Primary) && UpdateTaggedRange(e.GetPosition(_view.SyntaxEditor))) {
			// Block the editor from picking up the click over the URL
			e.Handled = true;
		}
	}

	/// <summary>
	/// Occurs when a pointer button is released over the <see cref="IEditorView"/> for this tagger.
	/// </summary>
	/// <param name="e">The event data.</param>
	public void NotifyPointerReleased(InputPointerButtonEventArgs e) {
		if (
			e.ButtonKind == InputPointerButtonKind.Primary
			&& UpdateTaggedRange(e.GetPosition(_view.SyntaxEditor), out var resolverResult)
		) {
			// The user has performed a single left-click on an item, so attempt to go to definition
			e.Handled = true;
			_goToDefinitionService?.NavigateToDefinition(resolverResult);

			// Set focus to the view since the click event was not allowed to be processed by SyntaxEditor
			_view.Focus(Windows.FocusState.Programmatic);
		}
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Clear any custom cursor that might be in effect
		ClearCustomCursor();

		// Stop listening for theme changes
		ThemeManager.CurrentThemeChanged -= OnThemeManagerCurrentThemeChanged;

		base.OnClosed();
	}

}

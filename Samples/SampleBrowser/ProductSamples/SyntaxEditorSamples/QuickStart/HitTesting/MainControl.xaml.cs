using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Primitives;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.HitTesting;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Load a language from a language definition
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("Html.langdef");
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the display name of the view's placement.
	/// </summary>
	/// <param name="view">The <see cref="IEditorView"/> to examine.</param>
	private static string GetPlacementName(IEditorView view) {
		if (view.SyntaxEditor.HasHorizontalSplit) {
			// Horizontal split
			switch (view.Placement) {
				case EditorViewPlacement.Upper:
					return "upper";
				case EditorViewPlacement.Lower:
					return "lower";
			}
		}

		return "default";
	}

	private void OnSyntaxEditorMouseMove(object sender, MouseEventArgs e) {
		var result = editor.HitTest(e.GetPosition(editor));
		UpdateHitTestInfo(result);
	}

	private void OnSyntaxEditorMouseLeave(object sender, MouseEventArgs e)
		=> UpdateHitTestInfo(result: null);

	/// <summary>
	/// Updates the hit test info.
	/// </summary>
	/// <param name="result">The hit test result, if any.</param>
	private void UpdateHitTestInfo(IHitTestResult? result) {
		var text = new StringBuilder();

		if (result is not null) {
			if (result.Snapshot is not null)
				text.AppendFormat("Snapshot version {0}{1}", result.Snapshot.Version.Number, Environment.NewLine);

			if (result.View is not null)
				text.AppendFormat("Over '{0}' view{1}", GetPlacementName(result.View), Environment.NewLine);

			switch (result.Type) {
				case HitTestResultType.Splitter:
					if (result.VisualElement is EditorViewSplitter)
						text.AppendLine("Over view splitter");
					break;
				case HitTestResultType.ViewMargin:
					text.AppendFormat("Over '{0}' margin{1}", result.ViewMargin!.Key, Environment.NewLine);
					if (result.Position.HasValue)
						text.AppendFormat("Closest text position is ({0},{1}){2}", result.Position.Value.Line, result.Position.Value.Character, Environment.NewLine);
					break;
				case HitTestResultType.ViewScrollBar:
					if (result.VisualElement is ScrollBar scrollBar)
						text.AppendFormat("Over '{0}' scrollbar{1}", scrollBar.Orientation, Environment.NewLine);
					break;
				case HitTestResultType.ViewScrollBarBlock:
					text.AppendLine("Over scroll bar block");
					break;
				case HitTestResultType.ViewScrollBarSplitter:
					if (result.VisualElement is ScrollBarSplitter)
						text.AppendLine("Over scroll bar splitter");
					break;
				case HitTestResultType.ViewScrollBarTray:
					text.AppendLine("Over scroll bar tray (that can contain other controls like buttons)");
					break;
				case HitTestResultType.ViewTextArea:
					text.AppendFormat("Not directly over any view line or character{0}", Environment.NewLine);
					if (result.Position.HasValue)
						text.AppendFormat("Closest text position is ({0},{1}){2}", result.Position.Value.Line, result.Position.Value.Character, Environment.NewLine);
					break;
				case HitTestResultType.ViewTextAreaOverCharacter: {
					text.AppendFormat("Directly over offset {0} and text position ({1},{2}){3}", result.Offset, result.Position?.Line, result.Position?.Character, Environment.NewLine);

					var reader = result.GetReader();
					var isLineTerminator = reader?.TokenText.IsLineTerminator() == true;
					if (isLineTerminator)
						text.AppendFormat("Directly over line terminator{0}", Environment.NewLine);
					else
						text.AppendFormat("Directly over character '{0}'{1}", reader?.Character, Environment.NewLine);

					var token = reader?.Token;
					if (token is not null) {
						text.AppendFormat("Directly over token '{0}' with range ({1},{2})-({3},{4}){5}", token.Key,
							token.StartPosition.Line, token.StartPosition.Character,
							token.EndPosition.Line, token.EndPosition.Character, Environment.NewLine);

						if (!isLineTerminator)
							text.AppendFormat("Directly over token text '{0}'{1}", reader!.TokenText, Environment.NewLine);
					}
					break;
				}
				case HitTestResultType.ViewTextAreaOverIntraTextSpacer:
					text.AppendFormat("Over spacer '{0}' on document line {1}{2}", result.IntraTextSpacerTag, result.Position?.Line, Environment.NewLine);
					break;
				case HitTestResultType.ViewTextAreaOverLine:
					text.AppendFormat("Over whitespace at the end of document line {0}{1}", result.Position?.Line, Environment.NewLine);
					break;
				default:
					if (result.VisualElement is not null)
						text.AppendFormat("Over a '{0}' element{1}", result.VisualElement.GetType().FullName, Environment.NewLine);
					else
						text.AppendLine("No other hit test details available");
					break;
			}
		}
		else {
			text.AppendLine("Not over the SyntaxEditor");
		}

		resultsTextBox.Text = text.ToString();
	}

}

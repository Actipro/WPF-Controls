using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Ribbon;
using ActiproSoftware.Windows.Controls.Ribbon.Input;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor;

/// <summary>
/// Contains the application-defined commands used for the <see cref="Ribbon"/> control.
/// </summary>
public class ApplicationCommands {

	private static RibbonCommand? _applicationExit;
	private static RibbonCommand? _applicationOptions;
	private static RibbonCommand? _applyBackground;
	private static RibbonCommand? _applyDefaultBackground;
	private static RibbonCommand? _applyDefaultForeground;
	private static RibbonCommand? _applyForeground;
	private static RibbonCommand? _clearFormatting;
	private static RibbonCommand? _comments;
	private static RibbonCommand? _coverPage;
	private static RibbonCommand? _disabled;
	private static RibbonCommand? _fileNewRtfDocument;
	private static RibbonCommand? _fileNewTextDocument;
	private static RibbonCommand? _fontFamily;
	private static RibbonCommand? _fontSize;
	private static RibbonCommand? _showDialog;
	private static RibbonCommand? _toggleContextualTabGroup;
	private static RibbonCommand? _toggleFlowDirection;
	private static RibbonCommand? _toggleStrikethrough;

	// --------------------------------------------------------------------------------------------------
	// UI PROVIDER REGISTRATION
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Registers <see cref="IRibbonCommandUIProvider"/> objects with existing built-in commands.
	/// </summary>
	public static void RegisterUIProvidersForNonRibbonCommands() {
		RibbonCommandUIManager.Register(EditingCommands.AlignCenter,
			new RibbonCommandUIProvider("Center", null, ImageLoader.GetIcon("AlignTextCenter16.png"), "Center text."));
		RibbonCommandUIManager.Register(EditingCommands.AlignJustify,
			new RibbonCommandUIProvider("Justify", null, ImageLoader.GetIcon("AlignTextJustify16.png"), "Align text to both the left and right margins, adding extra space between words as necessary.\r\n\r\nThis creates a clean look along the left and right sides of the page."));
		RibbonCommandUIManager.Register(EditingCommands.AlignLeft,
			new RibbonCommandUIProvider("Align Text Left", null, ImageLoader.GetIcon("AlignTextLeft16.png"), "Align text to the left."));
		RibbonCommandUIManager.Register(EditingCommands.AlignRight,
			new RibbonCommandUIProvider("Align Text Right", null, ImageLoader.GetIcon("AlignTextRight16.png"), "Align text to the right."));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Close,
			new RibbonCommandUIProvider("Close", ImageLoader.GetIcon("Close32.png"), ImageLoader.GetIcon("Close16.png")));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Copy,
			new RibbonCommandUIProvider("Copy", null, ImageLoader.GetIcon("Copy16.png"), "Copy the selection and put it on the Clipboard."));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Cut,
			new RibbonCommandUIProvider("Cut", null, ImageLoader.GetIcon("Cut16.png"), "Cut the selection from the document and put it on the Clipboard."));
		RibbonCommandUIManager.Register(EditingCommands.DecreaseFontSize,
			new RibbonCommandUIProvider("Shrink Font", null, ImageLoader.GetIcon("ShrinkFont16.png"), "Decrease the font size."));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Find,
			new RibbonCommandUIProvider("Find:", null, ImageLoader.GetIcon("Find16.png"), "Finds text in the text editor."));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Help,
			new RibbonCommandUIProvider("About Ribbon", null, ImageLoader.GetIcon("Help16.png"), "See the About window for this product."));
		RibbonCommandUIManager.Register(EditingCommands.IncreaseFontSize,
			new RibbonCommandUIProvider("Grow Font", null, ImageLoader.GetIcon("GrowFont16.png"), "Increase the font size."));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.New,
			new RibbonCommandUIProvider("New", ImageLoader.GetIcon("New32.png"), ImageLoader.GetIcon("New16.png")));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Open,
			new RibbonCommandUIProvider("Open", ImageLoader.GetIcon("Open32.png"), ImageLoader.GetIcon("Open16.png")));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Paste,
			new RibbonCommandUIProvider("Paste", ImageLoader.GetIcon("Paste32.png"), ImageLoader.GetIcon("Paste16.png"), "Paste the contents of the Clipboard."));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Redo,
			new RibbonCommandUIProvider("Redo", null, ImageLoader.GetIcon("Redo16.png")));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Save,
			new RibbonCommandUIProvider("Save", ImageLoader.GetIcon("Save32.png"), ImageLoader.GetIcon("Save16.png")));
		RibbonCommandUIManager.Register(EditingCommands.ToggleBold,
			new RibbonCommandUIProvider("Bold", null, ImageLoader.GetIcon("Bold16.png"), "Make the selected text bold."));
		RibbonCommandUIManager.Register(EditingCommands.ToggleItalic,
			new RibbonCommandUIProvider("Italic", null, ImageLoader.GetIcon("Italic16.png"), "Italicize the selected text."));
		RibbonCommandUIManager.Register(EditingCommands.ToggleSubscript,
			new RibbonCommandUIProvider("Subscript", null, ImageLoader.GetIcon("Subscript16.png"), "Create small letters below the text baseline."));
		RibbonCommandUIManager.Register(EditingCommands.ToggleSuperscript,
			new RibbonCommandUIProvider("Superscript", null, ImageLoader.GetIcon("Superscript16.png"), "Create small letters above the line of text."));
		RibbonCommandUIManager.Register(EditingCommands.ToggleUnderline,
			new RibbonCommandUIProvider("Underline", null, ImageLoader.GetIcon("Underline16.png"), "Underline the selected text."));
		RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Undo,
			new RibbonCommandUIProvider("Undo", null, ImageLoader.GetIcon("Undo16.png")));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to exit the application.
	/// </summary>
	public static RibbonCommand ApplicationExit
		=> _applicationExit ??= new RibbonCommand("ApplicationExit", typeof(Ribbon), "Exit Sample", null, ImageLoader.GetIcon("CloseTab16.png"));

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to show the application options dialog.
	/// </summary>
	public static RibbonCommand ApplicationOptions
		=> _applicationOptions ??= new RibbonCommand("ApplicationOptions", typeof(Ribbon), "Options", null, ImageLoader.GetIcon("Options16.png"));

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to apply a background.
	/// </summary>
	public static RibbonCommand ApplyBackground
		=> _applyBackground ??= new RibbonCommand("ApplyBackground", typeof(Ribbon), "Text Highlight Color", null, ImageLoader.GetIcon("TextHighlightColor16.png"));

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to apply a default background.
	/// </summary>
	public static RibbonCommand ApplyDefaultBackground
		=> _applyDefaultBackground ??= new RibbonCommand("ApplyDefaultBackground", typeof(Ribbon), "Text Highlight Color", null, ImageLoader.GetIcon("TextHighlightColor16.png"));

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to apply a default foreground.
	/// </summary>
	public static RibbonCommand ApplyDefaultForeground
		=> _applyDefaultForeground ??= new RibbonCommand("ApplyDefaultForeground", typeof(Ribbon), "Font Color", null, ImageLoader.GetIcon("FontColor16.png"));

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to apply a foreground.
	/// </summary>
	public static RibbonCommand ApplyForeground
		=> _applyForeground ??= new RibbonCommand("ApplyForeground", typeof(Ribbon), "Text Color");

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to clear the formatting.
	/// </summary>
	public static RibbonCommand ClearFormatting
		=> _clearFormatting ??= new RibbonCommand("ClearFormatting", typeof(Ribbon), "Clear Formatting", null, ImageLoader.GetIcon("ClearFormatting16.png"), "Clear all the formatting from the selection, leaving only the plain text.");

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to provide comments.
	/// </summary>
	public static RibbonCommand Comments
		=> _comments ??= new RibbonCommand("Comments", typeof(Ribbon), "\uD83D\uDCAC Comments", (ImageSource?)null, null, "See and respond to comments in this document.");

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to add a cover page.
	/// </summary>
	public static RibbonCommand CoverPage {
		get => _coverPage ??= new RibbonCommand("CoverPage", typeof(Ribbon), "Cover Page", ImageLoader.GetIcon("CoverPage32.png"), ImageLoader.GetIcon("CoverPage16.png"), "Insert a fully-formatted cover page.\r\n\r\nYou fill in the title, author, date, and other information.") {
			ScreenTipImageSource = ImageLoader.GetOther("CoverPageScreenTip.png"),
			ScreenTipHelpUri = new Uri("http://www.actiprosoftware.com", UriKind.Absolute)
		};
	}

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to demo a disabled command.
	/// </summary>
	public static RibbonCommand Disabled
		=> _disabled ??= new RibbonCommand("Disabled", typeof(Ribbon));

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to create a new RTF document.
	/// </summary>
	public static RibbonCommand FileNewRtfDocument
		=> _fileNewRtfDocument ??= new RibbonCommand("FileNewRTFDocument", typeof(Ribbon), "New RTF Document", ImageLoader.GetIcon("RichTextDocument32.png"), null);

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to create a new text document.
	/// </summary>
	public static RibbonCommand FileNewTextDocument
		=> _fileNewTextDocument ??= new RibbonCommand("FileNewTextDocument", typeof(Ribbon), "New Text Document", ImageLoader.GetIcon("TextDocument32.png"), null);

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to change the font family.
	/// </summary>
	public static RibbonCommand FontFamily
		=> _fontFamily ??= new RibbonCommand("FontFamily", typeof(Ribbon), "Font Family");

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to change the font size.
	/// </summary>
	public static RibbonCommand FontSize
		=> _fontSize ??= new RibbonCommand("FontSize", typeof(Ribbon), "Font Size");

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to display a dialog.
	/// </summary>
	public static RibbonCommand ShowDialog
		=> _showDialog ??= new RibbonCommand("ShowDialog", typeof(Ribbon));

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to toggle a contextual tab group for demonstration purposes.
	/// </summary>
	public static RibbonCommand ToggleContextualTabGroup
		=> _toggleContextualTabGroup ??= new RibbonCommand("ToggleContextualTabGroup", typeof(Ribbon), "Toggle Contextual Tab Group", null, ImageLoader.GetIcon("QuickStart16.png"), "Toggles the visibility of a contextual tab group for demonstration purposes.");

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to toggle flow direction.
	/// </summary>
	public static RibbonCommand ToggleFlowDirection
		=> _toggleFlowDirection ??= new RibbonCommand("ToggleFlowDirection", typeof(Ribbon), "Toggle Flow Direction", ImageLoader.GetIcon("FlowDirection32.png"), null, "Toggles flow direction of the control so that you can see how right-to-left mode operates.");

	/// <summary>
	/// The <see cref="RibbonCommand"/> that is used to toggle strikethrough.
	/// </summary>
	public static RibbonCommand ToggleStrikethrough
		=> _toggleStrikethrough ??= new RibbonCommand("ToggleStrikethrough", typeof(Ribbon), "Strikethrough", null, ImageLoader.GetIcon("Strikethrough16.png"), "Draw a line through the middle of the selected text.");

}

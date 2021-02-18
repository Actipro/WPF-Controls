using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Controls.Ribbon;
using ActiproSoftware.Windows.Controls.Ribbon.Input;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor {

	/// <summary>
	/// Contains the application-defined commands used for the <see cref="Ribbon"/> control.
	/// </summary>
	public class ApplicationCommands {

        private static RibbonCommand applicationExit;
        private static RibbonCommand applicationOptions;
        private static RibbonCommand applyBackground;
        private static RibbonCommand applyDefaultBackground;
        private static RibbonCommand applyDefaultForeground;
        private static RibbonCommand applyForeground;
        private static RibbonCommand clearFormatting;
        private static RibbonCommand comments;
        private static RibbonCommand coverPage;
        private static RibbonCommand disabled;
        private static RibbonCommand fileNewRtfDocument;
        private static RibbonCommand fileNewTextDocument;
        private static RibbonCommand fontFamily;
        private static RibbonCommand fontSize;
        private static RibbonCommand showDialog;
        private static RibbonCommand toggleContextualTabGroup;
        private static RibbonCommand toggleFlowDirection;
        private static RibbonCommand toggleStrikethrough;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// UI PROVIDER REGISTRATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Registers <see cref="IRibbonCommandUIProvider"/> objects with existing built-in commands.
		/// </summary>
		public static void RegisterUIProvidersForNonRibbonCommands() {
			RibbonCommandUIManager.Register(EditingCommands.AlignCenter, 
				new RibbonCommandUIProvider("Center", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/AlignTextCenter16.png", "Center text."));
			RibbonCommandUIManager.Register(EditingCommands.AlignJustify, 
				new RibbonCommandUIProvider("Justify", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/AlignTextJustify16.png", "Align text to both the left and right margins, adding extra space between words as necessary.\r\n\r\nThis creates a clean look along the left and right sides of the page."));
			RibbonCommandUIManager.Register(EditingCommands.AlignLeft, 
				new RibbonCommandUIProvider("Align Text Left", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/AlignTextLeft16.png", "Align text to the left."));
			RibbonCommandUIManager.Register(EditingCommands.AlignRight, 
				new RibbonCommandUIProvider("Align Text Right", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/AlignTextRight16.png", "Align text to the right."));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Close, 
				new RibbonCommandUIProvider("Close", "pack://application:,,,/SampleBrowser;component/Images/Icons/Close32.png", "pack://application:,,,/SampleBrowser;component/Images/Icons/Close16.png"));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Copy, 
				new RibbonCommandUIProvider("Copy", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Copy16.png", "Copy the selection and put it on the Clipboard."));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Cut, 
				new RibbonCommandUIProvider("Cut", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Cut16.png", "Cut the selection from the document and put it on the Clipboard."));
			RibbonCommandUIManager.Register(EditingCommands.DecreaseFontSize, 
				new RibbonCommandUIProvider("Shrink Font", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/ShrinkFont16.png", "Decrease the font size."));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Find, 
				new RibbonCommandUIProvider("Find:", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Find16.png", "Finds text in the text editor."));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Help, 
				new RibbonCommandUIProvider("About Ribbon", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Help16.png", "See the About window for this product."));
			RibbonCommandUIManager.Register(EditingCommands.IncreaseFontSize, 
				new RibbonCommandUIProvider("Grow Font", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/GrowFont16.png", "Increase the font size."));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.New, 
				new RibbonCommandUIProvider("New", "pack://application:,,,/SampleBrowser;component/Images/Icons/New32.png", "pack://application:,,,/SampleBrowser;component/Images/Icons/New16.png"));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Open, 
				new RibbonCommandUIProvider("Open", "pack://application:,,,/SampleBrowser;component/Images/Icons/Open32.png", "pack://application:,,,/SampleBrowser;component/Images/Icons/Open16.png"));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Paste, 
				new RibbonCommandUIProvider("Paste", "pack://application:,,,/SampleBrowser;component/Images/Icons/Paste32.png", "pack://application:,,,/SampleBrowser;component/Images/Icons/Paste16.png", "Paste the contents of the Clipboard."));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Redo, 
				new RibbonCommandUIProvider("Redo", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Redo16.png"));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Save, 
				new RibbonCommandUIProvider("Save", "pack://application:,,,/SampleBrowser;component/Images/Icons/Save32.png", "pack://application:,,,/SampleBrowser;component/Images/Icons/Save16.png"));
			RibbonCommandUIManager.Register(EditingCommands.ToggleBold, 
				new RibbonCommandUIProvider("Bold", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Bold16.png", "Make the selected text bold."));
			RibbonCommandUIManager.Register(EditingCommands.ToggleItalic, 
				new RibbonCommandUIProvider("Italic", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Italic16.png", "Italicize the selected text."));
			RibbonCommandUIManager.Register(EditingCommands.ToggleSubscript, 
				new RibbonCommandUIProvider("Subscript", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Subscript16.png", "Create small letters below the text baseline."));
			RibbonCommandUIManager.Register(EditingCommands.ToggleSuperscript, 
				new RibbonCommandUIProvider("Superscript", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Superscript16.png", "Create small letters above the line of text."));
			RibbonCommandUIManager.Register(EditingCommands.ToggleUnderline, 
				new RibbonCommandUIProvider("Underline", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Underline16.png", "Underline the selected text."));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Undo, 
				new RibbonCommandUIProvider("Undo", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Undo16.png"));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to exit the application.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to exit the application.</value>
		public static RibbonCommand ApplicationExit {
			get {
				if (applicationExit == null)
					applicationExit = new RibbonCommand("ApplicationExit", typeof(Ribbon), "Exit Sample", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/CloseTab16.png");
				return applicationExit;
			}
		}

		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to show the application options dialog.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to show the application options dialog.</value>
		public static RibbonCommand ApplicationOptions {
			get {
				if (applicationOptions == null)
					applicationOptions = new RibbonCommand("ApplicationOptions", typeof(Ribbon), "Options", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Options16.png");
				return applicationOptions;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to apply a background.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to apply a background.</value>
		public static RibbonCommand ApplyBackground {
			get {
				if (applyBackground == null)
					applyBackground = new RibbonCommand("ApplyBackground", typeof(Ribbon), "Text Highlight Color", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/TextHighlightColor16.png");				
				return applyBackground;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to apply a default background.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to apply a default background.</value>
		public static RibbonCommand ApplyDefaultBackground {
			get {
				if (applyDefaultBackground == null)
					applyDefaultBackground = new RibbonCommand("ApplyDefaultBackground", typeof(Ribbon), "Text Highlight Color", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/TextHighlightColor16.png");				
				return applyDefaultBackground;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to apply a default foreground.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to apply a default foreground.</value>
		public static RibbonCommand ApplyDefaultForeground {
			get {
				if (applyDefaultForeground == null)
					applyDefaultForeground = new RibbonCommand("ApplyDefaultForeground", typeof(Ribbon), "Font Color", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/FontColor16.png");				
				return applyDefaultForeground;
			}
		}

		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to apply a foreground.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to apply a foreground.</value>
		public static RibbonCommand ApplyForeground {
			get {
				if (applyForeground == null)
					applyForeground = new RibbonCommand("ApplyForeground", typeof(Ribbon), "Text Color");
				return applyForeground;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to clear the formatting.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to clear the formatting.</value>
		public static RibbonCommand ClearFormatting {
			get {
				if (clearFormatting == null)
					clearFormatting = new RibbonCommand("ClearFormatting", typeof(Ribbon), "Clear Formatting", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/ClearFormatting16.png", "Clear all the formatting from the selection, leaving only the plain text.");
				return clearFormatting;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to provide comments.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to provide comments.</value>
		public static RibbonCommand Comments {
			get {
				if (comments == null)
					comments = new RibbonCommand("Comments", typeof(Ribbon), "\uD83D\uDCAC Comments", (ImageSource)null, null, "See and respond to comments in this document.");
				return comments;
			}
		}

		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to add a cover page.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to add a cover page.</value>
		public static RibbonCommand CoverPage {
			get {
				if (coverPage == null) {
					coverPage = new RibbonCommand("CoverPage", typeof(Ribbon), "Cover Page", "pack://application:,,,/SampleBrowser;component/Images/Icons/CoverPage32.png", "pack://application:,,,/SampleBrowser;component/Images/Icons/CoverPage16.png", "Insert a fully-formatted cover page.\r\n\r\nYou fill in the title, author, date, and other information.");
					coverPage.ScreenTipImageSource = new BitmapImage(new Uri("pack://application:,,,/SampleBrowser;component/Images/Other/CoverPageScreenTip.png", UriKind.Absolute));
					coverPage.ScreenTipHelpUri = new Uri("http://www.actiprosoftware.com", UriKind.Absolute);
				}
				return coverPage;
			}
		}

		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to demo a disabled command.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to demo a disabled command.</value>
		public static RibbonCommand Disabled {
			get {
				if (disabled == null)
					disabled = new RibbonCommand("Disabled", typeof(Ribbon));
				return disabled;
			}
		}

		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to create a new RTF document.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to create a new RTF document.</value>
		public static RibbonCommand FileNewRtfDocument {
			get {
				if (fileNewRtfDocument == null) 
					fileNewRtfDocument = new RibbonCommand("FileNewRTFDocument", typeof(Ribbon), "New RTF Document", "pack://application:,,,/SampleBrowser;component/Images/Icons/RichTextDocument32.png", null);
				return fileNewRtfDocument;
			}
		}

		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to create a new text document.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to create a new text document.</value>
		public static RibbonCommand FileNewTextDocument {
			get {
				if (fileNewTextDocument == null)
					fileNewTextDocument = new RibbonCommand("FileNewTextDocument", typeof(Ribbon), "New Text Document", "pack://application:,,,/SampleBrowser;component/Images/Icons/TextDocument32.png", null);
				return fileNewTextDocument;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to change the font family.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to change the font family.</value>
		public static RibbonCommand FontFamily {
			get {
				if (fontFamily == null)
					fontFamily = new RibbonCommand("FontFamily", typeof(Ribbon), "Font Family");
				return fontFamily;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to change the font size.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to change the font size.</value>
		public static RibbonCommand FontSize {
			get {
				if (fontSize == null)
					fontSize = new RibbonCommand("FontSize", typeof(Ribbon), "Font Size");
				return fontSize;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to display a dialog.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to display a dialog.</value>
		public static RibbonCommand ShowDialog {
			get {
				if (showDialog == null)
					showDialog = new RibbonCommand("ShowDialog", typeof(Ribbon));
				return showDialog;
			}
		}

		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to toggle a contextual tab group for demonstration purposes.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to toggle a contextual tab group for demonstration purposes.</value>
		public static RibbonCommand ToggleContextualTabGroup {
			get {
				if (toggleContextualTabGroup == null)
					toggleContextualTabGroup = new RibbonCommand("ToggleContextualTabGroup", typeof(Ribbon), "Toggle Contextual Tab Group", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/QuickStart16.png", "Toggles the visiblility of a contextual tab group for demonstration purposes.");
				return toggleContextualTabGroup;
			}
		}

		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to toggle flowdirection.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to toggle flowdirection.</value>
		public static RibbonCommand ToggleFlowDirection {
			get {
				if (toggleFlowDirection == null)
					toggleFlowDirection = new RibbonCommand("ToggleFlowDirection", typeof(Ribbon), "Toggle Flow Direction", "pack://application:,,,/SampleBrowser;component/Images/Icons/FlowDirection32.png", null, "Toggles flow direction of the control so that you can see how right-to-left mode operates.");
				return toggleFlowDirection;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="RibbonCommand"/> that is used to toggle strikethrough.
		/// </summary>
		/// <value>The <see cref="RibbonCommand"/> that is used to toggle strikethrough.</value>
		public static RibbonCommand ToggleStrikethrough {
			get {
				if (toggleStrikethrough == null)
					toggleStrikethrough = new RibbonCommand("ToggleStrikethrough", typeof(Ribbon), "Strikethrough", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Strikethrough16.png", "Draw a line through the middle of the selected text.");
				return toggleStrikethrough;
			}
		}


	}
}


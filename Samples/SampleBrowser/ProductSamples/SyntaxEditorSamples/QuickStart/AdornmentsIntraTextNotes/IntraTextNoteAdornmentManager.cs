using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes {

    /// <summary>
	/// Represents an adornment manager for a view that renders intra-text notes.
    /// </summary>
    public class IntraTextNoteAdornmentManager : IntraTextAdornmentManagerBase<IEditorView, IntraTextNoteTag> {

		private static AdornmentLayerDefinition layerDefinition = 
			new AdornmentLayerDefinition("IntraTextNote", new Ordering(AdornmentLayerDefinitions.TextForeground.Key, OrderPlacement.Before));

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>IntraTextNoteAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public IntraTextNoteAdornmentManager(IEditorView view) : base(view, layerDefinition) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Changes the status of the specified note tag.
		/// </summary>
		/// <param name="tagRange">The tag range.</param>
		/// <param name="status">The new status.</param>
		private void ChangeNoteStatus(TagSnapshotRange<IntraTextNoteTag> tagRange, ReviewStatus status) {
			// Get the tagger from the code document
			ICodeDocument document = tagRange.SnapshotRange.Snapshot.Document as ICodeDocument;
			if (document != null) {
				IntraTextNoteTagger tagger = null;
				if (document.Properties.TryGetValue(typeof(IntraTextNoteTagger), out tagger)) {
					// Change the tag's status and raise an event so the UI knows to update
					tagRange.Tag.Status = status;
					tagger.RaiseTagsChanged(new TagsChangedEventArgs(tagRange.SnapshotRange));
				}
			}
		}
		
		/// <summary>
		/// Occurs when a menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to this event.</param>
		private void OnMarkNoteAsAccpeted(object sender, RoutedEventArgs e) {
			MenuItem item = (MenuItem)sender;
			this.ChangeNoteStatus((TagSnapshotRange<IntraTextNoteTag>)item.Tag, ReviewStatus.Accepted);
		}

		/// <summary>
		/// Occurs when a menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to this event.</param>
		private void OnMarkNoteAsPending(object sender, RoutedEventArgs e) {
			MenuItem item = (MenuItem)sender;
			this.ChangeNoteStatus((TagSnapshotRange<IntraTextNoteTag>)item.Tag, ReviewStatus.Pending);
		}

		/// <summary>
		/// Occurs when a menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to this event.</param>
		private void OnMarkNoteAsRejected(object sender, RoutedEventArgs e) {
			MenuItem item = (MenuItem)sender;
			this.ChangeNoteStatus((TagSnapshotRange<IntraTextNoteTag>)item.Tag, ReviewStatus.Rejected);
		}

		/// <summary>
		/// Occurs when a menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains data related to this event.</param>
		private void OnRemoveNote(object sender, RoutedEventArgs e) {
			MenuItem item = (MenuItem)sender;

			// Get the tag range
			TagSnapshotRange<IntraTextNoteTag> tagRange = (TagSnapshotRange<IntraTextNoteTag>)item.Tag;

			// Get the tagger from the code document
			ICodeDocument document = tagRange.SnapshotRange.Snapshot.Document as ICodeDocument;
			if (document != null) {
				IntraTextNoteTagger tagger = null;
				if (document.Properties.TryGetValue(typeof(IntraTextNoteTagger), out tagger)) {
					// Try and find the tag version range that contains the tag
					TagVersionRange<IIntraTextSpacerTag> tagVersionRange = tagger[tagRange.Tag];
					if (tagVersionRange != null) {
						// Remove the tag version range from the tagger
						tagger.Remove(tagVersionRange);
					}
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds an adornment to the <see cref="AdornmentLayer"/>.
		/// </summary>
		/// <param name="reason">An <see cref="AdornmentChangeReason"/> indicating the add reason.</param>
		/// <param name="viewLine">The current <see cref="ITextViewLine"/> being examined.</param>
		/// <param name="tagRange">The <see cref="ITag"/> and the range it covers.</param>
		/// <param name="bounds">The text bounds in which to render an adornment.</param>
		protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<IntraTextNoteTag> tagRange, TextBounds bounds) {
			// Create the adornment
			var image = new DynamicImage();
			image.Width = 16;
			image.Height = 16;
			image.SnapsToDevicePixels = true;
			image.Source = new BitmapImage(new Uri("/Images/Icons/Notes16.png", UriKind.Relative));
			image.Stretch = Stretch.Fill;

			// Create a popup button
			PopupButton button = new PopupButton();
			button.Content = image;
			button.Cursor = Cursors.Arrow;
			button.DisplayMode = PopupButtonDisplayMode.Merged;
			button.Focusable = false;
			button.IsTabStop = false;
			button.IsTransparencyModeEnabled = true;
			button.Margin = new Thickness(0);
			button.Padding = new Thickness(-1);
			button.ToolTip = new HtmlContentProvider(String.Format("<span style=\"color: green;\">{0}</span><br/>Created at <b>{1}</b> by <span style=\"color: blue;\">{2}</span><br/>Status: <b>{3}</b>",
				tagRange.Tag.Message, tagRange.Tag.Created.ToShortTimeString(), tagRange.Tag.Author, tagRange.Tag.Status)).GetContent();

			// Add a context menu
			ContextMenu contextMenu = new ContextMenu();
			button.PopupMenu = contextMenu;

			MenuItem removeItem = new MenuItem();
			removeItem.Header = "Remove Note";
			removeItem.Tag = tagRange;
			removeItem.Click += new RoutedEventHandler(OnRemoveNote);
			contextMenu.Items.Add(removeItem);
			
			contextMenu.Items.Add(new Separator());

			MenuItem pendingItem = new MenuItem();
			pendingItem.Header = "Mark as Pending";
			pendingItem.IsChecked = (tagRange.Tag.Status == ReviewStatus.Pending);
			pendingItem.Tag = tagRange;
			pendingItem.Click += new RoutedEventHandler(OnMarkNoteAsPending);
			contextMenu.Items.Add(pendingItem);
			
			MenuItem acceptedItem = new MenuItem();
			acceptedItem.Header = "Mark as Accepted";
			acceptedItem.IsChecked = (tagRange.Tag.Status == ReviewStatus.Accepted);
			acceptedItem.Tag = tagRange;
			acceptedItem.Click += new RoutedEventHandler(OnMarkNoteAsAccpeted);
			contextMenu.Items.Add(acceptedItem);

			MenuItem rejectedItem = new MenuItem();
			rejectedItem.Header = "Mark as Rejected";
			rejectedItem.IsChecked = (tagRange.Tag.Status == ReviewStatus.Rejected);
			rejectedItem.Tag = tagRange;
			rejectedItem.Click += new RoutedEventHandler(OnMarkNoteAsRejected);
			contextMenu.Items.Add(rejectedItem);

			// Get the location
			Point location = new Point(Math.Round(bounds.Left) + 1, Math.Round(bounds.Top + (bounds.Height - tagRange.Tag.Size.Height) / 2));

			// Add the adornment to the layer
			this.AdornmentLayer.AddAdornment(reason, button, location, tagRange.Tag.Key, null);
		}

		/// <summary>
		/// Occurs when the manager is closed and detached from the view.
		/// </summary>
		/// <remarks>
		/// Overrides should release any event handlers set up in the manager's constructor.
		/// </remarks>
		protected override void OnClosed() {
			// Remove any remaining adornments
			this.AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);
		
			// Call the base method
			base.OnClosed();
		}

    }
	
}

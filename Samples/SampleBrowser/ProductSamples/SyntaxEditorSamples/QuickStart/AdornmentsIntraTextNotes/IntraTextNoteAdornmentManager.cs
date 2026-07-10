using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes;

/// <summary>
/// Represents an adornment manager for a view that renders intra-text notes.
/// </summary>
/// <param name="view">The view to which this manager is attached.</param>
public class IntraTextNoteAdornmentManager(IEditorView view) : IntraTextAdornmentManagerBase<IEditorView, IntraTextNoteTag>(view, _layerDefinition) {

	private static readonly AdornmentLayerDefinition _layerDefinition = new("IntraTextNote", new Ordering(AdornmentLayerDefinitions.TextForeground.Key, OrderPlacement.Before));

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Changes the placement of the specified note tag.
	/// </summary>
	/// <param name="tagRange">The tag range.</param>
	/// <param name="isBefore">Whether the adornment is before the tagged range.</param>
	private static void ChangeNotePlacement(TagSnapshotRange<IntraTextNoteTag> tagRange, bool isBefore) {
		// Get the tagger from the code document
		var document = tagRange.SnapshotRange.Snapshot.Document as ICodeDocument;
		if (document is not null) {
			if (document.Properties.TryGetValue<IntraTextNoteTagger>(out var tagger)) {
				// Change the tag's placement and raise an event so the UI knows to update
				tagRange.Tag.IsSpacerBefore = isBefore;
				tagger!.RaiseTagsChanged(new TagsChangedEventArgs(tagRange.SnapshotRange));
			}
		}
	}

	/// <summary>
	/// Changes the status of the specified note tag.
	/// </summary>
	/// <param name="tagRange">The tag range.</param>
	/// <param name="status">The new status.</param>
	private static void ChangeNoteStatus(TagSnapshotRange<IntraTextNoteTag> tagRange, ReviewStatus status) {
		// Get the tagger from the code document
		var document = tagRange.SnapshotRange.Snapshot.Document as ICodeDocument;
		if (document is not null) {
			if (document.Properties.TryGetValue<IntraTextNoteTagger>(out var tagger)) {
				// Change the tag's status and raise an event so the UI knows to update
				tagRange.Tag.Status = status;
				tagger!.RaiseTagsChanged(new TagsChangedEventArgs(tagRange.SnapshotRange));
			}
		}
	}

	private void OnMarkNoteAsAccepted(object sender, RoutedEventArgs e) {
		var item = (MenuItem)sender;
		ChangeNoteStatus((TagSnapshotRange<IntraTextNoteTag>)item.Tag, ReviewStatus.Accepted);
	}

	private void OnMarkNoteAsPending(object sender, RoutedEventArgs e) {
		var item = (MenuItem)sender;
		ChangeNoteStatus((TagSnapshotRange<IntraTextNoteTag>)item.Tag, ReviewStatus.Pending);
	}

	private void OnMarkNoteAsRejected(object sender, RoutedEventArgs e) {
		var item = (MenuItem)sender;
		ChangeNoteStatus((TagSnapshotRange<IntraTextNoteTag>)item.Tag, ReviewStatus.Rejected);
	}

	private void OnRemoveNote(object sender, RoutedEventArgs e) {
		var item = (MenuItem)sender;

		// Get the tag range
		var tagRange = (TagSnapshotRange<IntraTextNoteTag>)item.Tag;

		// Get the tagger from the code document
		var document = tagRange.SnapshotRange.Snapshot.Document as ICodeDocument;
		if (document is not null) {
			if (document.Properties.TryGetValue<IntraTextNoteTagger>(out var tagger)) {
				// Try and find the tag version range that contains the tag
				if (tagger![tagRange.Tag] is { } tagVersionRange) {
					// Remove the tag version range from the tagger
					tagger.Remove(tagVersionRange);
				}
			}
		}
	}

	private void OnToggleNotePlacement(object sender, RoutedEventArgs e) {
		var item = (MenuItem)sender;
		var tagRange = (TagSnapshotRange<IntraTextNoteTag>)item.Tag;
		ChangeNotePlacement(tagRange, !tagRange.Tag.IsSpacerBefore);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<IntraTextNoteTag> tagRange, TextBounds bounds) {
		// Create the adornment
		var image = new DynamicImage {
			Width = 16,
			Height = 16,
			SnapsToDevicePixels = true,
			Source = new BitmapImage(new Uri("/Images/Icons/Notes16.png", UriKind.Relative)),
			Stretch = Stretch.Fill
		};

		// Create a popup button
		var button = new PopupButton {
			Content = image,
			Cursor = Cursors.Arrow,
			DisplayMode = PopupButtonDisplayMode.Merged,
			Focusable = false,
			IsTabStop = false,
			IsTransparencyModeEnabled = true,
			Margin = new Thickness(0),
			Padding = new Thickness(-1),
			ToolTip = new HtmlContentProvider(
				string.Format(
					"<span style=\"color: green;\">{0}</span><br/>Created at <b>{1}</b> by <span style=\"color: blue;\">{2}</span><br/>Status: <b>{3}</b>",
					tagRange.Tag.Message, tagRange.Tag.Created.ToShortTimeString(), tagRange.Tag.Author, tagRange.Tag.Status
				)).GetContent()
		};

		// Add a context menu
		var contextMenu = new ContextMenu();
		button.PopupMenu = contextMenu;

		var removeItem = new MenuItem {
			Header = "Remove Note",
			Tag = tagRange
		};
		removeItem.Click += OnRemoveNote;
		contextMenu.Items.Add(removeItem);

		contextMenu.Items.Add(new Separator());

		var pendingItem = new MenuItem {
			Header = "Mark as Pending",
			IsChecked = (tagRange.Tag.Status == ReviewStatus.Pending),
			Tag = tagRange
		};
		pendingItem.Click += OnMarkNoteAsPending;
		contextMenu.Items.Add(pendingItem);

		var acceptedItem = new MenuItem {
			Header = "Mark as Accepted",
			IsChecked = (tagRange.Tag.Status == ReviewStatus.Accepted),
			Tag = tagRange
		};
		acceptedItem.Click += OnMarkNoteAsAccepted;
		contextMenu.Items.Add(acceptedItem);

		var rejectedItem = new MenuItem {
			Header = "Mark as Rejected",
			IsChecked = (tagRange.Tag.Status == ReviewStatus.Rejected),
			Tag = tagRange
		};
		rejectedItem.Click += OnMarkNoteAsRejected;
		contextMenu.Items.Add(rejectedItem);

		contextMenu.Items.Add(new Separator());

		var placementItem = new MenuItem {
			Header = "Note Before Text",
			IsChecked = tagRange.Tag.IsSpacerBefore,
			Tag = tagRange
		};
		placementItem.Click += OnToggleNotePlacement;
		contextMenu.Items.Add(placementItem);

		// Get the location
		var location = new Point(
			Math.Round(bounds.Left) + 1,
			Math.Round(bounds.Top + (bounds.Height - tagRange.Tag.Size.Height) / 2)
		);

		// Add the adornment to the layer
		AdornmentLayer.AddAdornment(reason, button, location, tagRange.Tag.Key, removedCallback: null);
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Remove any remaining adornments
		AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

		base.OnClosed();
	}

}

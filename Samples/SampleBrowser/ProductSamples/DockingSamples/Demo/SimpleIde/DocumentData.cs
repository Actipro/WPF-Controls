using ActiproSoftware.Text.Searching;

namespace ActiproSoftware.ProductSamples.DockingSamples.Demo.SimpleIde;

/// <summary>
/// Stores information about a document.
/// </summary>
public class DocumentData : ObservableObjectBase {

	private string? _fileName;
	private bool _isModified;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The file name.
	/// </summary>
	[Category("Name")]
	[DisplayName("File name")]
	public string? FileName {
		get => _fileName;
		set {
			if (SetProperty(ref _fileName, value))
				OnPropertyChanged(nameof(Title));
		}
	}

	/// <summary>
	/// Indicates whether the document has been modified.
	/// </summary>
	[Category("State")]
	[DisplayName("Is modified")]
	public bool IsModified {
		get => _isModified;
		set => SetProperty(ref _isModified, value);
	}

	/// <summary>
	/// The action to invoke when document outline data is updated.
	/// </summary>
	[Browsable(false)]
	public Action<EditorDocumentWindow>? NotifyDocumentOutlineUpdated { get; set; }

	/// <summary>
	/// The action to invoke when a search occurs.
	/// </summary>
	[Browsable(false)]
	public Action<EditorDocumentWindow, ISearchResultSet>? NotifySearchAction { get; set; }

	/// <summary>
	/// The title.
	/// </summary>
	[Category("Name")]
	public string Title
		=> string.IsNullOrEmpty(_fileName) ? "Document" : Path.GetFileName(_fileName);

}

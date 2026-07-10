namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a view model for image and text content within a ribbon footer.
/// </summary>
public class RibbonFooterSimpleContentViewModel : ObservableObjectBase, IHasTag {

	private ImageSource? _imageSource;
	private object? _tag;
	private string? _text;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="System.Windows.Media.ImageSource"/> for the image.
	/// </summary>
	public ImageSource? ImageSource {
		get => _imageSource;
		set => SetProperty(ref _imageSource, value);
	}

	/// <inheritdoc cref="IHasTag.Tag"/>
	public object? Tag {
		get => _tag;
		set => SetProperty(ref _tag, value);
	}

	/// <summary>
	/// The text content.
	/// </summary>
	public string? Text {
		get => _text;
		set => SetProperty(ref _text, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> $"{GetType().FullName}[Text='{Text}']";

}

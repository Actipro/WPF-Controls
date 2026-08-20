using ActiproSoftware.Shell;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.CustomShellObjects;

/// <summary>
/// Represents a custom <see cref="IShellObject"/> implementation.
/// </summary>
public class CustomShellObject : ShellObjectBase {

	private ImageSource? _extraLargeIcon;
	private ImageSource? _extraLargeIconOverlay;
	private ImageSource? _extraLargeThumbnail;
	private readonly ShellObjectKind _kind;
	private ImageSource? _largeIcon;
	private ImageSource? _largeIconOverlay;
	private ImageSource? _largeThumbnail;
	private ImageSource? _mediumIcon;
	private ImageSource? _mediumIconOverlay;
	private ImageSource? _mediumThumbnail;
	private string? _name;
	private readonly string? _parsingName;
	private readonly string? _relativeParsingName;
	private readonly string? _editingName;
	private ImageSource? _smallIcon;
	private ImageSource? _smallIconOverlay;
	private object? _toolTip;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="shellService">The <see cref="IShellService"/> used to return this shell object's children.</param>
	/// <param name="kind">The kind of shell object.</param>
	/// <param name="name">The name of the shell object.</param>
	/// <param name="parsingName">The full parsing name of the shell object, if available.</param>
	/// <param name="relativeParsingName">Optionally define the relative parsing name of the shell object used as the individual part of a full parsing name, if different than <paramref name="name"/>.</param>
	/// <param name="editingName">Optionally define the user-friendly editing name of the shell object, if different than <paramref name="parsingName"/>.</param>
	public CustomShellObject(IShellService shellService, ShellObjectKind kind, string? name, string? parsingName, string? relativeParsingName = null, string? editingName = null) : base(shellService) {
		_kind = kind;
		_name = name;
		_parsingName = parsingName;
		_relativeParsingName = relativeParsingName;
		_editingName = editingName;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override string? EditingName
		=> _editingName ?? ParsingName;

	/// <inheritdoc/>
	public override ImageSource? ExtraLargeIcon {
		get => _extraLargeIcon;
		set => SetProperty(ref _extraLargeIcon, value);
	}

	/// <inheritdoc/>
	public override ImageSource? ExtraLargeIconOverlay {
		get => _extraLargeIconOverlay;
		set => SetProperty(ref _extraLargeIconOverlay, value);
	}

	/// <inheritdoc/>
	public override ImageSource? ExtraLargeThumbnail {
		get => _extraLargeThumbnail;
		set => SetProperty(ref _extraLargeThumbnail, value);
	}

	/// <inheritdoc/>
	public override ShellObjectKind Kind
		=> _kind;

	/// <inheritdoc/>
	public override ImageSource? LargeIcon {
		get => _largeIcon;
		set => SetProperty(ref _largeIcon, value);
	}

	/// <inheritdoc/>
	public override ImageSource? LargeIconOverlay {
		get => _largeIconOverlay;
		set => SetProperty(ref _largeIconOverlay, value);
	}

	/// <inheritdoc/>
	public override ImageSource? LargeThumbnail {
		get => _largeThumbnail;
		set => SetProperty(ref _largeThumbnail, value);
	}

	/// <inheritdoc/>
	public override ImageSource? MediumIcon {
		get => _mediumIcon;
		set => SetProperty(ref _mediumIcon, value);
	}

	/// <inheritdoc/>
	public override ImageSource? MediumIconOverlay {
		get => _mediumIconOverlay;
		set => SetProperty(ref _mediumIconOverlay, value);
	}

	/// <inheritdoc/>
	public override ImageSource? MediumThumbnail {
		get => _mediumThumbnail;
		set => SetProperty(ref _mediumThumbnail, value);
	}

	/// <inheritdoc/>
	public override string? Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

	/// <inheritdoc/>
	public override string? ParsingName
		=> _parsingName;

	/// <inheritdoc/>
	public override string? RelativeParsingName
		=> _relativeParsingName ?? base.RelativeParsingName;

	/// <inheritdoc/>
	public override ImageSource? SmallIcon {
		get => _smallIcon;
		set => SetProperty(ref _smallIcon, value);
	}

	/// <inheritdoc/>
	public override ImageSource? SmallIconOverlay {
		get => _smallIconOverlay;
		set => SetProperty(ref _smallIconOverlay, value);
	}

	/// <inheritdoc/>
	public override object? ToolTip {
		get => _toolTip;
		set => SetProperty(ref _toolTip, value);
	}

}

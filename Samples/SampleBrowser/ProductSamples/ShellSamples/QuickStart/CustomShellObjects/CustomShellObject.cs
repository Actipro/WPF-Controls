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
		set {
			if (_extraLargeIcon != value) {
				_extraLargeIcon = value;
				NotifyPropertyChanged(nameof(ExtraLargeIcon));
			}
		}
	}

	/// <inheritdoc/>
	public override ImageSource? ExtraLargeIconOverlay {
		get => _extraLargeIconOverlay;
		set {
			if (_extraLargeIconOverlay != value) {
				_extraLargeIconOverlay = value;
				NotifyPropertyChanged(nameof(ExtraLargeIconOverlay));
			}
		}
	}

	/// <inheritdoc/>
	public override ImageSource? ExtraLargeThumbnail {
		get => _extraLargeThumbnail;
		set {
			if (_extraLargeThumbnail != value) {
				_extraLargeThumbnail = value;
				NotifyPropertyChanged(nameof(ExtraLargeThumbnail));
			}
		}
	}

	/// <inheritdoc/>
	public override ShellObjectKind Kind
		=> _kind;

	/// <inheritdoc/>
	public override ImageSource? LargeIcon {
		get => _largeIcon;
		set {
			if (_largeIcon != value) {
				_largeIcon = value;
				NotifyPropertyChanged(nameof(LargeIcon));
			}
		}
	}

	/// <inheritdoc/>
	public override ImageSource? LargeIconOverlay {
		get => _largeIconOverlay;
		set {
			if (_largeIconOverlay != value) {
				_largeIconOverlay = value;
				NotifyPropertyChanged(nameof(LargeIconOverlay));
			}
		}
	}

	/// <inheritdoc/>
	public override ImageSource? LargeThumbnail {
		get => _largeThumbnail;
		set {
			if (_largeThumbnail != value) {
				_largeThumbnail = value;
				NotifyPropertyChanged(nameof(LargeThumbnail));
			}
		}
	}

	/// <inheritdoc/>
	public override ImageSource? MediumIcon {
		get => _mediumIcon;
		set {
			if (_mediumIcon != value) {
				_mediumIcon = value;
				NotifyPropertyChanged(nameof(MediumIcon));
			}
		}
	}

	/// <inheritdoc/>
	public override ImageSource? MediumIconOverlay {
		get => _mediumIconOverlay;
		set {
			if (_mediumIconOverlay != value) {
				_mediumIconOverlay = value;
				NotifyPropertyChanged(nameof(MediumIconOverlay));
			}
		}
	}

	/// <inheritdoc/>
	public override ImageSource? MediumThumbnail {
		get => _mediumThumbnail;
		set {
			if (_mediumThumbnail != value) {
				_mediumThumbnail = value;
				NotifyPropertyChanged(nameof(MediumThumbnail));
			}
		}
	}

	/// <inheritdoc/>
	public override string? Name {
		get => _name;
		set {
			if (_name != value) {
				_name = value;
				NotifyPropertyChanged(nameof(Name));
			}
		}
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
		set {
			if (_smallIcon != value) {
				_smallIcon = value;
				NotifyPropertyChanged(nameof(SmallIcon));
			}
		}
	}

	/// <inheritdoc/>
	public override ImageSource? SmallIconOverlay {
		get => _smallIconOverlay;
		set {
			if (_smallIconOverlay != value) {
				_smallIconOverlay = value;
				NotifyPropertyChanged(nameof(SmallIconOverlay));
			}
		}
	}

	/// <inheritdoc/>
	public override object? ToolTip {
		get => _toolTip;
		set {
			if (_toolTip != value) {
				_toolTip = value;
				NotifyPropertyChanged(nameof(ToolTip));
			}
		}
	}

}

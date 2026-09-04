using ActiproSoftware.Text;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.UI.WinForms.Drawing;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Media;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles;

/// <summary>
/// Represents a provider of <see cref="IClassificationType"/> objects for the UI display items within a <see cref="SyntaxEditor"/>.
/// </summary>
public class CompareFilesClassificationTypeProvider {

	private readonly IHighlightingStyleRegistry _registry;

	private IClassificationType? _diffAdded;
	private IClassificationType? _diffImaginary;
	private IClassificationType? _diffModifiedNew;
	private IClassificationType? _diffModifiedOld;
	private IClassificationType? _diffRemoved;

	// Default foreground colors
	private static readonly Color DefaultDiffImaginaryForegroundLightColor = UIColor.FromWebColor("#d0d0d0");
	private static readonly Color DefaultDiffImaginaryForegroundDarkColor = UIColor.FromWebColor("#3d3d3d");

	// Default background colors
	private static readonly Color DefaultDiffAddedBackgroundLightColor = UIColor.FromWebColor("#d7e3bc");
	private static readonly Color DefaultDiffAddedBackgroundDarkColor = UIColor.FromWebColor("#265e4d");
	private static readonly Color DefaultDiffModifiedNewBackgroundLightColor = UIColor.FromWebColor("#ebf1dd");
	private static readonly Color DefaultDiffModifiedNewBackgroundDarkColor = UIColor.FromWebColor("#15352c");
	private static readonly Color DefaultDiffModifiedOldBackgroundLightColor = UIColor.FromWebColor("#ffcccc");
	private static readonly Color DefaultDiffModifiedOldBackgroundDarkColor = UIColor.FromWebColor("#2d0000");
	private static readonly Color DefaultDiffRemovedBackgroundLightColor = UIColor.FromWebColor("#ff9999");
	private static readonly Color DefaultDiffRemovedBackgroundDarkColor = UIColor.FromWebColor("#3c0000");

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CompareFilesClassificationTypeProvider() : this(registry: null) { }

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="registry">The <see cref="IHighlightingStyleRegistry"/> to use when registering classification types and highlighting styles.</param>
	public CompareFilesClassificationTypeProvider(IHighlightingStyleRegistry? registry) {
		_registry = registry ?? AmbientHighlightingStyleRegistry.Instance;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="IClassificationType"/> to use for added lines in a file difference view.
	/// </summary>
	public IClassificationType DifferenceAdded {
		get {
			if (_diffAdded is null) {
				_diffAdded = _registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceAdded.Key);
				if (_diffAdded is null) {
					_diffAdded = CompareFilesClassificationTypes.DifferenceAdded;

					// Configure the color palettes with light/dark colors for this style
					_registry.LightColorPalette?.SetBackground(_diffAdded.Key, DefaultDiffAddedBackgroundLightColor);
					_registry.DarkColorPalette?.SetBackground(_diffAdded.Key, DefaultDiffAddedBackgroundDarkColor);

					// Register the style and the colors for the current color palette will be applied
					_registry.Register(_diffAdded,
						new HighlightingStyle() {
							IsBackgroundEditable = true,
							IsBoldEditable = false,
							IsForegroundEditable = false,
							IsItalicEditable = false
						});
				}
			}
			return _diffAdded;
		}
	}

	/// <summary>
	/// The <see cref="IClassificationType"/> to use for imaginary lines in a file difference view.
	/// </summary>
	/// <remarks>
	/// Only the foreground is editable in the default <see cref="IHighlightingStyle"/> that is registered for this classification type.
	/// </remarks>
	public IClassificationType DifferenceImaginary {
		get {
			if (_diffImaginary is null) {
				_diffImaginary = _registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceImaginary.Key);
				if (_diffImaginary is null) {
					_diffImaginary = CompareFilesClassificationTypes.DifferenceImaginary;

					// Configure the color palettes with light/dark colors for this style
					_registry.LightColorPalette?.SetForeground(_diffImaginary.Key, DefaultDiffImaginaryForegroundLightColor);
					_registry.DarkColorPalette?.SetForeground(_diffImaginary.Key, DefaultDiffImaginaryForegroundDarkColor);

					// Register the style and the colors for the current color palette will be applied
					_registry.Register(_diffImaginary,
						new HighlightingStyle() {
							IsBackgroundEditable = false,
							IsBoldEditable = false,
							IsForegroundEditable = true,
							IsItalicEditable = false
						});
				}
			}
			return _diffImaginary;
		}
	}

	/// <summary>
	/// The <see cref="IClassificationType"/> to use for modified lines in a file difference view for the latest version.
	/// </summary>
	/// <remarks>
	/// Only the background is editable in the default <see cref="IHighlightingStyle"/> that is registered for this classification type.
	/// </remarks>
	public IClassificationType DifferenceModifiedNew {
		get {
			if (_diffModifiedNew is null) {
				_diffModifiedNew = _registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceModifiedNew.Key);
				if (_diffModifiedNew is null) {
					_diffModifiedNew = CompareFilesClassificationTypes.DifferenceModifiedNew;

					// Configure the color palettes with light/dark colors for this style
					_registry.LightColorPalette?.SetBackground(_diffModifiedNew.Key, DefaultDiffModifiedNewBackgroundLightColor);
					_registry.DarkColorPalette?.SetBackground(_diffModifiedNew.Key, DefaultDiffModifiedNewBackgroundDarkColor);

					// Register the style and the colors for the current color palette will be applied
					_registry.Register(_diffModifiedNew,
						new HighlightingStyle() {
							IsBackgroundEditable = true,
							IsBoldEditable = false,
							IsForegroundEditable = false,
							IsItalicEditable = false
						});
				}
			}
			return _diffModifiedNew;
		}
	}

	/// <summary>
	/// The <see cref="IClassificationType"/> to use for modified lines in a file difference view for the oldest version.
	/// </summary>
	/// <remarks>
	/// Only the background is editable in the default <see cref="IHighlightingStyle"/> that is registered for this classification type.
	/// </remarks>
	public IClassificationType DifferenceModifiedOld {
		get {
			if (_diffModifiedOld is null) {
				_diffModifiedOld = _registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceModifiedOld.Key);
				if (_diffModifiedOld is null) {
					_diffModifiedOld = CompareFilesClassificationTypes.DifferenceModifiedOld;

					// Configure the color palettes with light/dark colors for this style
					_registry.LightColorPalette?.SetBackground(_diffModifiedOld.Key, DefaultDiffModifiedOldBackgroundLightColor);
					_registry.DarkColorPalette?.SetBackground(_diffModifiedOld.Key, DefaultDiffModifiedOldBackgroundDarkColor);

					// Register the style and the colors for the current color palette will be applied
					_registry.Register(_diffModifiedOld,
						new HighlightingStyle() {
							IsBackgroundEditable = true,
							IsBoldEditable = false,
							IsForegroundEditable = false,
							IsItalicEditable = false
						});
				}
			}
			return _diffModifiedOld;
		}
	}

	/// <summary>
	/// The <see cref="IClassificationType"/> to use for removed lines in a file difference view.
	/// </summary>
	public IClassificationType DifferenceRemoved {
		get {
			if (_diffRemoved is null) {
				_diffRemoved = _registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceRemoved.Key);
				if (_diffRemoved is null) {
					_diffRemoved = CompareFilesClassificationTypes.DifferenceRemoved;

					// Configure the color palettes with light/dark colors for this style
					_registry.LightColorPalette?.SetBackground(_diffRemoved.Key, DefaultDiffRemovedBackgroundLightColor);
					_registry.DarkColorPalette?.SetBackground(_diffRemoved.Key, DefaultDiffRemovedBackgroundDarkColor);

					// Register the style and the colors for the current color palette will be applied
					_registry.Register(_diffRemoved,
						new HighlightingStyle() {
							IsBackgroundEditable = true,
							IsBoldEditable = false,
							IsForegroundEditable = false,
							IsItalicEditable = false
						});
				}
			}
			return _diffRemoved;
		}
	}

	/// <summary>
	/// Registers all classification types and highlighting styles with the <see cref="IHighlightingStyleRegistry"/> used by this class.
	/// </summary>
	/// <returns>The collection of <see cref="IClassificationType"/> objects that were registered.</returns>
	public IEnumerable<IClassificationType> RegisterAll() {
		return [
			DifferenceAdded,
			DifferenceImaginary,
			DifferenceModifiedNew,
			DifferenceModifiedOld,
			DifferenceRemoved,
		];
	}

}

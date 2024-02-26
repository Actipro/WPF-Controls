using ActiproSoftware.Text;
using System.Collections.Generic;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Highlighting.Implementation;
using System.Drawing;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Media;
using System.Windows.Media;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Represents a provider of <see cref="IClassificationType"/> objects for the UI display items within a <see cref="SyntaxEditor"/>.
	/// </summary>
	public class CompareFilesClassificationTypeProvider {

		private readonly IHighlightingStyleRegistry registry;

		private IClassificationType diffAdded;
		private IClassificationType diffImaginary;
		private IClassificationType diffModifiedNew;
		private IClassificationType diffModifiedOld;
		private IClassificationType diffRemoved;

		// Default foreground colors
		private static Color DefaultDiffImaginaryForegroundLightColor		= UIColor.FromWebColor("#d0d0d0");
		private static Color DefaultDiffImaginaryForegroundDarkColor		= UIColor.FromWebColor("#3d3d3d");

		// Default background colors
		private static Color DefaultDiffAddedBackgroundLightColor			= UIColor.FromWebColor("#d7e3bc");
		private static Color DefaultDiffAddedBackgroundDarkColor			= UIColor.FromWebColor("#265e4d");
		private static Color DefaultDiffModifiedNewBackgroundLightColor		= UIColor.FromWebColor("#ebf1dd");
		private static Color DefaultDiffModifiedNewBackgroundDarkColor		= UIColor.FromWebColor("#15352c");
		private static Color DefaultDiffModifiedOldBackgroundLightColor		= UIColor.FromWebColor("#ffcccc");
		private static Color DefaultDiffModifiedOldBackgroundDarkColor		= UIColor.FromWebColor("#2d0000");
		private static Color DefaultDiffRemovedBackgroundLightColor			= UIColor.FromWebColor("#ff9999");
		private static Color DefaultDiffRemovedBackgroundDarkColor			= UIColor.FromWebColor("#3c0000");

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="CompareFilesClassificationTypeProvider"/>.
		/// </summary>
		public CompareFilesClassificationTypeProvider() : this(null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="CompareFilesClassificationTypeProvider"/>.
		/// </summary>
		/// <param name="registry">The <see cref="IHighlightingStyleRegistry"/> to use when registering classification types and highlighting styles.</param>
		public CompareFilesClassificationTypeProvider(IHighlightingStyleRegistry registry) {
			this.registry = registry ?? AmbientHighlightingStyleRegistry.Instance;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> to use for added lines in a file difference view.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> to use for an added line.</value>
		public IClassificationType DifferenceAdded {
			get {
				if (diffAdded == null) {
					diffAdded = registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceAdded.Key);
					if (diffAdded == null) {
						diffAdded = CompareFilesClassificationTypes.DifferenceAdded;

						// Configure the color palettes with light/dark colors for this style
						registry.LightColorPalette?.SetBackground(diffAdded.Key, DefaultDiffAddedBackgroundLightColor);
						registry.DarkColorPalette?.SetBackground(diffAdded.Key, DefaultDiffAddedBackgroundDarkColor);

						// Register the style and the colors for the current color palette will be applied
						registry.Register(diffAdded,
							new HighlightingStyle() {
								IsBackgroundEditable = true,
								IsBoldEditable = false,
								IsForegroundEditable = false,
								IsItalicEditable = false
							});
					}
				}
				return diffAdded;
			}
		}

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> to use for imaginary lines in a file difference view.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> to use for an imaginary line.</value>
		/// <remarks>
		/// Only the foreground is editable in the default <see cref="IHighlightingStyle"/> that is registered for this classification type.
		/// </remarks>
		public IClassificationType DifferenceImaginary {
			get {
				if (diffImaginary == null) {
					diffImaginary = registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceImaginary.Key);
					if (diffImaginary == null) {
						diffImaginary = CompareFilesClassificationTypes.DifferenceImaginary;

						// Configure the color palettes with light/dark colors for this style
						registry.LightColorPalette?.SetForeground(diffImaginary.Key, DefaultDiffImaginaryForegroundLightColor);
						registry.DarkColorPalette?.SetForeground(diffImaginary.Key, DefaultDiffImaginaryForegroundDarkColor);

						// Register the style and the colors for the current color palette will be applied
						registry.Register(diffImaginary,
							new HighlightingStyle() {
								IsBackgroundEditable = false,
								IsBoldEditable = false,
								IsForegroundEditable = true,
								IsItalicEditable = false
							});
					}
				}
				return diffImaginary;
			}
		}

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> to use for modified lines in a file difference view for the latest version.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> to use for a modified line in the latest version.</value>
		/// <remarks>
		/// Only the background is editable in the default <see cref="IHighlightingStyle"/> that is registered for this classification type.
		/// </remarks>
		public IClassificationType DifferenceModifiedNew {
			get {
				if (diffModifiedNew == null) {
					diffModifiedNew = registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceModifiedNew.Key);
					if (diffModifiedNew == null) {
						diffModifiedNew = CompareFilesClassificationTypes.DifferenceModifiedNew;

						// Configure the color palettes with light/dark colors for this style
						registry.LightColorPalette?.SetBackground(diffModifiedNew.Key, DefaultDiffModifiedNewBackgroundLightColor);
						registry.DarkColorPalette?.SetBackground(diffModifiedNew.Key, DefaultDiffModifiedNewBackgroundDarkColor);

						// Register the style and the colors for the current color palette will be applied
						registry.Register(diffModifiedNew,
							new HighlightingStyle() {
								IsBackgroundEditable = true,
								IsBoldEditable = false,
								IsForegroundEditable = false,
								IsItalicEditable = false
							});
					}
				}
				return diffModifiedNew;
			}
		}

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> to use for modified lines in a file difference view for the oldest version.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> to use for a modified line in the oldest version.</value>
		/// <remarks>
		/// Only the background is editable in the default <see cref="IHighlightingStyle"/> that is registered for this classification type.
		/// </remarks>
		public IClassificationType DifferenceModifiedOld {
			get {
				if (diffModifiedOld == null) {
					diffModifiedOld = registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceModifiedOld.Key);
					if (diffModifiedOld == null) {
						diffModifiedOld = CompareFilesClassificationTypes.DifferenceModifiedOld;

						// Configure the color palettes with light/dark colors for this style
						registry.LightColorPalette?.SetBackground(diffModifiedOld.Key, DefaultDiffModifiedOldBackgroundLightColor);
						registry.DarkColorPalette?.SetBackground(diffModifiedOld.Key, DefaultDiffModifiedOldBackgroundDarkColor);

						// Register the style and the colors for the current color palette will be applied
						registry.Register(diffModifiedOld,
							new HighlightingStyle() {
								IsBackgroundEditable = true,
								IsBoldEditable = false,
								IsForegroundEditable = false,
								IsItalicEditable = false
							});
					}
				}
				return diffModifiedOld;
			}
		}

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> to use for removed lines in a file difference view.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> to use for a removed line.</value>
		public IClassificationType DifferenceRemoved {
			get {
				if (diffRemoved == null) {
					diffRemoved = registry.GetClassificationType(CompareFilesClassificationTypes.DifferenceRemoved.Key);
					if (diffRemoved == null) {
						diffRemoved = CompareFilesClassificationTypes.DifferenceRemoved;

						// Configure the color palettes with light/dark colors for this style
						registry.LightColorPalette?.SetBackground(diffRemoved.Key, DefaultDiffRemovedBackgroundLightColor);
						registry.DarkColorPalette?.SetBackground(diffRemoved.Key, DefaultDiffRemovedBackgroundDarkColor);

						// Register the style and the colors for the current color palette will be applied
						registry.Register(diffRemoved,
							new HighlightingStyle() {
								IsBackgroundEditable = true,
								IsBoldEditable = false,
								IsForegroundEditable = false,
								IsItalicEditable = false
							});
					}
				}
				return diffRemoved;
			}
		}

		/// <summary>
		/// Registers all classification types and highlighting styles with the <see cref="IHighlightingStyleRegistry"/> used by this class.
		/// </summary>
		/// <returns>The collection of <see cref="IClassificationType"/> objects that were registered.</returns>
		public IEnumerable<IClassificationType> RegisterAll() {
			return new IClassificationType[] {
				this.DifferenceAdded,
				this.DifferenceImaginary,
				this.DifferenceModifiedNew,
				this.DifferenceModifiedOld,
				this.DifferenceRemoved,
			};
		}

	}

}

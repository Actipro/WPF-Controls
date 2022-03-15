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
using System.Windows.Media;
#endif

#if USE_BORDERS_FOR_INLINE_DIFF
using ActiproSoftware.Windows.Controls.Rendering;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Represents a provider of <see cref="IClassificationType"/> objects for the UI display items within a <see cref="SyntaxEditor"/>.
	/// </summary>
	public class CompareFilesClassificationTypeProvider {

		// IMPLEMENTATION NOTE: The default colors are based on a light theme. The Sample Browser defines dark theme colors in the
		// following file which is imported when changing to a dark theme: /ProductSamples/SyntaxEditorSamples/Languages/Themes/Dark.vssettings

		private readonly IHighlightingStyleRegistry registry;

		private IClassificationType diffAdded;
		private IClassificationType diffImaginary;
		private IClassificationType diffModifiedNew;
		private IClassificationType diffModifiedOld;
		private IClassificationType diffRemoved;

		// Default foreground colors
		private static Color DefaultDiffImaginaryForegroundColor	= Color.FromArgb(byte.MaxValue, 208, 208, 208);

		// Default background colors
		private static Color DefaultDiffAddedBackgroundColor		= Color.FromArgb(byte.MaxValue, 215, 227, 188);
		private static Color DefaultDiffModifiedNewBackgroundColor	= Color.FromArgb(byte.MaxValue, 235, 241, 221);
		private static Color DefaultDiffModifiedOldBackgroundColor	= Color.FromArgb(byte.MaxValue, 255, 204, 204);
		private static Color DefaultDiffRemovedBackgroundColor		= Color.FromArgb(byte.MaxValue, 255, 153, 153);

		#if USE_BORDERS_FOR_INLINE_DIFF
		// Default border colors
		private static Color DefaultDiffAddedBorderColor			= Color.FromArgb(byte.MaxValue, 118, 146, 60);
		private static Color DefaultDiffRemovedBorderColor			= Color.FromArgb(byte.MaxValue, 255, 102, 102);
		#endif

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
						registry.Register(diffAdded,
							new HighlightingStyle() {
								Background = DefaultDiffAddedBackgroundColor,
								IsBackgroundEditable = true,
								IsBoldEditable = false,
								IsForegroundEditable = false,
								IsItalicEditable = false,
								#if USE_BORDERS_FOR_INLINE_DIFF
								BorderColor = DefaultDiffAddedBorderColor,
								BorderKind = LineKind.Solid,
								IsBorderEditable = true,
								#endif
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
						registry.Register(diffImaginary,
							new HighlightingStyle(DefaultDiffImaginaryForegroundColor) {
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
						registry.Register(diffModifiedNew,
							new HighlightingStyle() {
								Background = DefaultDiffModifiedNewBackgroundColor,
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
						registry.Register(diffModifiedOld,
							new HighlightingStyle() {
								Background = DefaultDiffModifiedOldBackgroundColor,
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
						registry.Register(diffRemoved,
							new HighlightingStyle() {
								Background = DefaultDiffRemovedBackgroundColor,
								IsBackgroundEditable = true,
								IsBoldEditable = false,
								IsForegroundEditable = false,
								IsItalicEditable = false,
								#if USE_BORDERS_FOR_INLINE_DIFF
								BorderColor = DefaultDiffRemovedBorderColor,
								BorderKind = LineKind.Solid,
								IsBorderEditable = true,
								#endif
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

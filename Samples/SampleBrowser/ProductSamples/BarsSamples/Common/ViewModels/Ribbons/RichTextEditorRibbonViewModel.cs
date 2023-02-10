using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.Windows.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a view model for a ribbon control to be used with a single-document rich text editor.
	/// </summary>
	public class RichTextEditorRibbonViewModel : RibbonViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> to be associated with the view model.</param>
		/// <param name="recentDocuments">The <see cref="RecentDocumentManager"/> to be associated with the view model.</param>
		public RichTextEditorRibbonViewModel(BarManager barManager, RecentDocumentManager recentDocuments) {
			if (barManager is null)
				throw new ArgumentNullException(nameof(barManager));

			this.RecentDocuments = recentDocuments ?? throw new ArgumentNullException(nameof(recentDocuments));

			// Create Application Button with Backstage
			this.ApplicationButton = CreateApplicationButton();
			this.Backstage = CreateBackstage(barManager, recentDocuments);

			// Hide the application button if a backstage is not defined
			if (this.Backstage is null)
				this.IsApplicationButtonVisible = false;

			// Create toolbars
			this.QuickAccessToolBar = CreateQuickAccessToolBar(barManager);
			this.TabRowToolBar = CreateTabRowToolBar(barManager);

			// Create tabs
			this.Tabs.AddRange(CreateTabs(barManager));
			this.ContextualTabGroups.AddRange(CreateContextualTabGroups());
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the <see cref="RibbonApplicationButtonViewModel"/> to be used with <see cref="RibbonViewModel.ApplicationButton"/>.
		/// </summary>
		/// <returns>A new instance of <see cref="RibbonApplicationButtonViewModel"/>.</returns>
		protected virtual RibbonApplicationButtonViewModel CreateApplicationButton() {
			return new RibbonApplicationButtonViewModel();
		}

		/// <summary>
		/// Creates the <see cref="RibbonBackstageViewModel"/> to be used with <see cref="RibbonViewModel.Backstage"/>.
		/// </summary>
		/// <returns>A new instance of <see cref="RibbonBackstageViewModel"/>.</returns>
		protected virtual RibbonBackstageViewModel CreateBackstage(BarManager barManager, RecentDocumentManager recentDocuments) {
			// See derived types for backstage examples
			return null;
		}

		/// <summary>
		/// Gets created instances of <see cref="RibbonContextualTabGroupViewModel"/> that will be used to initialize the
		/// <see cref="RibbonViewModel.ContextualTabGroups"/> collection.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of type <see cref="RibbonContextualTabGroupViewModel"/>.</returns>
		protected virtual IEnumerable<RibbonContextualTabGroupViewModel> CreateContextualTabGroups() {
			// NOTE: This group is included only as an example for how one might be defined but is not activated in the samples
			yield return new RibbonContextualTabGroupViewModel(ContextualTabGroupKeys.PictureTools);
		}

		/// <summary>
		/// Creates the <see cref="RibbonTabViewModel"/> that represents the "Home" tab of the ribbon.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> which defines the control view models that might be populated on the tab.</param>
		/// <returns>A new instance of <see cref="RibbonTabViewModel"/>.</returns>
		protected virtual RibbonTabViewModel CreateHomeTab(BarManager barManager) {
			var imageProvider = barManager.ImageProvider;

			return new RibbonTabViewModel("Home") {
				Description = "Common commands used throughout the application.",
				GroupVariants = new VariantCollection() {
					new SizeVariant(BarControlKeys.GroupClipboard, VariantSize.Small),
					new SizeVariant(BarControlKeys.GroupStyles, VariantSize.Medium),
					new SizeVariant(BarControlKeys.GroupEditing, VariantSize.Collapsed),
					new SizeVariant(BarControlKeys.GroupParagraph, VariantSize.Medium),
					new SizeVariant(BarControlKeys.GroupFont, VariantSize.Medium),
					new SizeVariant(BarControlKeys.GroupStyles, VariantSize.Small),
					new SizeVariant(BarControlKeys.GroupParagraph, VariantSize.Collapsed),
					new SizeVariant(BarControlKeys.GroupFont, VariantSize.Collapsed),
				},
				ControlVariants = new VariantCollection() {
					new VariantSet(
						new SizeVariant(BarControlKeys.Find, VariantSize.Small),
						new SizeVariant(BarControlKeys.Replace, VariantSize.Small)
					),
					new VariantSet(
						new SizeVariant(BarControlKeys.AlignLeft, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.AlignCenter, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.AlignRight, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.AlignJustify, VariantSize.Collapsed)
					),
					new SizeVariant(BarControlKeys.QuickStylesGallery, VariantSize.Collapsed),
					new VariantSet(
						new SizeVariant(BarControlKeys.Find, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.Replace, VariantSize.Collapsed)
					),
					new VariantSet(
						new SizeVariant(BarControlKeys.DecreaseIndent, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.IncreaseIndent, VariantSize.Collapsed)
					),
					new VariantSet(
						new SizeVariant(BarControlKeys.Bullets, VariantSize.Collapsed),
						new SizeVariant(BarControlKeys.Numbering, VariantSize.Collapsed)
					),
					new SizeVariant(BarControlKeys.TextHighlightColor, VariantSize.Collapsed),
					new SizeVariant(BarControlKeys.Underline, VariantSize.Collapsed),
				},
				Groups = {
					new RibbonGroupViewModel(BarControlKeys.GroupUndo, "Undo") {
						CanAutoCollapse = false,
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupUndo, BarImageSize.Small),
						Items = {
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.AlwaysSmall,
								Items = {
									barManager.ControlViewModels[BarControlKeys.Undo],
									barManager.ControlViewModels[BarControlKeys.Redo],
								}
							}
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupClipboard, "Clipboard") {
						CanAutoCollapse = false,
						LauncherButton = new RibbonGroupLauncherButtonViewModel(BarControlKeys.GroupLauncherClipboard, "Clipboard", "FO", barManager.NotImplementedCommand) {
							Description = "See all the items you've copied to the Clipboard",
						},
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupClipboard, BarImageSize.Small),
						Items = {
							barManager.ControlViewModels[BarControlKeys.PasteMenu],
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
								Items = {
									barManager.ControlViewModels[BarControlKeys.Cut],
									barManager.ControlViewModels[BarControlKeys.Copy],
									barManager.ControlViewModels[BarControlKeys.FormatPainter]
								}
							}
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupFont, "Font") {
						CanUseMultiRowLayout = true,
						ChildOverflowTarget = RibbonGroupChildOverflowTarget.Group,
						CollapsedButtonDescription = "Contains numerous commands for formatting fonts.",
						LargeImageSource = imageProvider.GetImageSource(BarControlKeys.GroupFont, BarImageSize.Large),
						LauncherButton = new RibbonGroupLauncherButtonViewModel(BarControlKeys.GroupLauncherFont, "Font Settings", "FN", barManager.NotImplementedCommand) {
							Description = "Customize your text using advanced font and character options",
						},
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupFont, BarImageSize.Small),
						ThreeRowItemSortOrder = new Int32Collection { 0, 3, 2, 4, 1 },
						Items = {
							new RibbonControlGroupViewModel() {
								SeparatorMode = RibbonControlGroupSeparatorMode.Never,
								Items = {
									barManager.ControlViewModels[BarControlKeys.Font],
									barManager.ControlViewModels[BarControlKeys.FontSize]
								}
							},
							new RibbonControlGroupViewModel() {
								Items = {
									barManager.ControlViewModels[BarControlKeys.IncreaseFontSize],
									barManager.ControlViewModels[BarControlKeys.DecreaseFontSize]
								}
							},
							barManager.ControlViewModels[BarControlKeys.ClearFormatting],
							new RibbonControlGroupViewModel() {
								Items = {
									barManager.ControlViewModels[BarControlKeys.Bold],
									barManager.ControlViewModels[BarControlKeys.Italic],
									barManager.ControlViewModels[BarControlKeys.Underline],
									barManager.ControlViewModels[BarControlKeys.Strikethrough],
									barManager.ControlViewModels[BarControlKeys.Subscript],
									barManager.ControlViewModels[BarControlKeys.Superscript]
								}
							},
							new RibbonControlGroupViewModel() {
								Items = {
									barManager.ControlViewModels[BarControlKeys.TextHighlightColor],
									barManager.ControlViewModels[BarControlKeys.FontColor],
								}
							},
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupParagraph, "Paragraph") {
						CanUseMultiRowLayout = true,
						CollapsedButtonDescription = "Contains commands for formatting paragraphs.",
						LargeImageSource = imageProvider.GetImageSource(BarControlKeys.GroupParagraph, BarImageSize.Large),
						LauncherButton = new RibbonGroupLauncherButtonViewModel(BarControlKeys.GroupLauncherParagraph, "Paragraph Settings", "PG", barManager.NotImplementedCommand) {
							Description = "Fine-tune the layout of the current paragraph.",
						},
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupParagraph, BarImageSize.Small),
						ThreeRowItemSortOrder = new Int32Collection { 0, 1, 4, 5, 6, 2, 3 },
						Items = {
							new RibbonControlGroupViewModel() {
								Items = {
									barManager.ControlViewModels[BarControlKeys.Bullets],
									barManager.ControlViewModels[BarControlKeys.Numbering],
								}
							},
							new RibbonControlGroupViewModel() {
								Items = {
									barManager.ControlViewModels[BarControlKeys.DecreaseIndent],
									barManager.ControlViewModels[BarControlKeys.IncreaseIndent],
								}
							},
							barManager.ControlViewModels[BarControlKeys.Sort],
							barManager.ControlViewModels[BarControlKeys.ShowSymbols],
							new RibbonControlGroupViewModel() {
								Items = {
									barManager.ControlViewModels[BarControlKeys.AlignLeft],
									barManager.ControlViewModels[BarControlKeys.AlignCenter],
									barManager.ControlViewModels[BarControlKeys.AlignRight],
									barManager.ControlViewModels[BarControlKeys.AlignJustify],
								}
							},
							barManager.ControlViewModels[BarControlKeys.LineSpacing],
							new RibbonControlGroupViewModel() {
								Items = {
									barManager.ControlViewModels[BarControlKeys.Shading],
									barManager.ControlViewModels[BarControlKeys.Borders],
								}
							},
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupStyles, "Styles") {
						CanAutoCollapse = false,
						LauncherButton = new RibbonGroupLauncherButtonViewModel(BarControlKeys.GroupLauncherStyles, "Styles Settings", "FY", barManager.NotImplementedCommand) {
							Description = "Preview, manage, and customize styles.",
						},
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupStyles, BarImageSize.Small),
						Items = {
							barManager.ControlViewModels[BarControlKeys.QuickStylesGallery],
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupEditing, "Editing") {
						CollapsedButtonDescription = "Contains commands for search and selection.",
						LargeImageSource = imageProvider.GetImageSource(BarControlKeys.GroupEditing, BarImageSize.Large),
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupEditing, BarImageSize.Small),
						Items = {
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
								Items = {
									barManager.ControlViewModels[BarControlKeys.Find],
									barManager.ControlViewModels[BarControlKeys.Replace],
									barManager.ControlViewModels[BarControlKeys.SelectMenu]
								}
							}
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the <see cref="RibbonTabViewModel"/> that represents the "Insert" tab of the ribbon.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> which defines the control view models that might be populated on the tab.</param>
		/// <returns>A new instance of <see cref="RibbonTabViewModel"/>.</returns>
		protected virtual RibbonTabViewModel CreateInsertTab(BarManager barManager) {
			var imageProvider = barManager.ImageProvider;

			return new RibbonTabViewModel("Insert") {
				KeyTipText = "N",
				GroupVariants = new VariantCollection() {
					new SizeVariant(BarControlKeys.GroupLinks, VariantSize.Medium),
					new SizeVariant(BarControlKeys.GroupPages, VariantSize.Medium),
					new SizeVariant(BarControlKeys.GroupText, VariantSize.Medium),
					new SizeVariant(BarControlKeys.GroupHeaderFooter, VariantSize.Medium),
					new SizeVariant(BarControlKeys.GroupIllustrations, VariantSize.Medium),
					new SizeVariant(BarControlKeys.GroupText, VariantSize.Small),
					new SizeVariant(BarControlKeys.GroupLinks, VariantSize.Collapsed),
					new SizeVariant(BarControlKeys.GroupPages, VariantSize.Collapsed),
					new SizeVariant(BarControlKeys.GroupSymbols, VariantSize.Collapsed),
					new SizeVariant(BarControlKeys.GroupIllustrations, VariantSize.Small),
					new SizeVariant(BarControlKeys.GroupText, VariantSize.Collapsed),
					new SizeVariant(BarControlKeys.GroupHeaderFooter, VariantSize.Collapsed),
					new SizeVariant(BarControlKeys.GroupIllustrations, VariantSize.Collapsed),
				},
				Groups = {
					new RibbonGroupViewModel(BarControlKeys.GroupPages, "Pages") {
						LargeImageSource = imageProvider.GetImageSource(BarControlKeys.GroupPages, BarImageSize.Large),
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupPages, BarImageSize.Small),
						Items = {
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.NeverSmall,
								Items = {
									barManager.ControlViewModels[BarControlKeys.CoverPageInsert],
									barManager.ControlViewModels[BarControlKeys.BlankPageInsert],
									barManager.ControlViewModels[BarControlKeys.PageBreakInsert]
								}
							}
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupTables, "Tables", "ZB") {
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupTables, BarImageSize.Small),
						CanAutoCollapse = false,
						Items = {
							barManager.ControlViewModels[BarControlKeys.TableInsertMenu],
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupIllustrations, "Illustrations") {
						LargeImageSource = imageProvider.GetImageSource(BarControlKeys.GroupIllustrations, BarImageSize.Large),
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupIllustrations, BarImageSize.Small),
						Items = {
							barManager.ControlViewModels[BarControlKeys.PictureInsert],
							new RibbonControlGroupViewModel() {
								Items = {
									barManager.ControlViewModels[BarControlKeys.ShapesMenu],
									barManager.ControlViewModels[BarControlKeys.SmartArtInsert],
									barManager.ControlViewModels[BarControlKeys.ChartInsert],
								}
							}
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupLinks, "Links") {
						LargeImageSource = imageProvider.GetImageSource(BarControlKeys.GroupLinks, BarImageSize.Large),
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupLinks, BarImageSize.Small),
						Items = {
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.NeverSmall,
								Items = {
									barManager.ControlViewModels[BarControlKeys.LinkInsertMenu],
									barManager.ControlViewModels[BarControlKeys.BookmarkInsert],
									barManager.ControlViewModels[BarControlKeys.CrossReferenceInsert]
								}
							}
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupHeaderFooter, "Header & Footer") {
						LargeImageSource = imageProvider.GetImageSource(BarControlKeys.GroupHeaderFooter, BarImageSize.Large),
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupHeaderFooter, BarImageSize.Small),
						Items = {
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.NeverSmall,
								Items = {
									barManager.ControlViewModels[BarControlKeys.HeaderInsert],
									barManager.ControlViewModels[BarControlKeys.FooterInsert],
									barManager.ControlViewModels[BarControlKeys.PageNumberInsert]
								}
							}
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupText, "Text") {
						LargeImageSource = imageProvider.GetImageSource(BarControlKeys.GroupText, BarImageSize.Large),
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupText, BarImageSize.Small),
						Items = {
							barManager.ControlViewModels[BarControlKeys.TextBoxInsert],
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.NeverSmall,
								Items = {
									barManager.ControlViewModels[BarControlKeys.QuickPartsInsert],
									barManager.ControlViewModels[BarControlKeys.WordArtInsert],
									barManager.ControlViewModels[BarControlKeys.DropCapInsert],
								}
							},
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
								Items = {
									barManager.ControlViewModels[BarControlKeys.SignatureLineInsert],
									barManager.ControlViewModels[BarControlKeys.DateTimeInsert],
									barManager.ControlViewModels[BarControlKeys.ObjectInsertMenu],
								}
							}
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupSymbols, "Symbols") {
						LargeImageSource = imageProvider.GetImageSource(BarControlKeys.GroupSymbols, BarImageSize.Large),
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupSymbols, BarImageSize.Small),
						Items = {
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.NeverSmall,
								Items = {
									barManager.ControlViewModels[BarControlKeys.EquationInsert],
									barManager.ControlViewModels[BarControlKeys.SymbolInsertMenu],
								}
							}
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the <see cref="RibbonQuickAccessToolBarViewModel"/> to be used with <see cref="RibbonViewModel.QuickAccessToolBar"/>.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> which defines the control view models that might be populated on the toolbar.</param>
		/// <returns>A new instance of <see cref="RibbonQuickAccessToolBarViewModel"/>.</returns>
		protected virtual RibbonQuickAccessToolBarViewModel CreateQuickAccessToolBar(BarManager barManager) {
			return new RibbonQuickAccessToolBarViewModel() {
				CommonItems = {
					barManager.ControlViewModels[BarControlKeys.Save],
					barManager.ControlViewModels[BarControlKeys.Undo],
					barManager.ControlViewModels[BarControlKeys.Redo],
				},
				Items = {
					barManager.ControlViewModels[BarControlKeys.Save],
				},
			};
		}

		/// <summary>
		/// Creates the <see cref="RibbonTabRowToolBarViewModel"/> to be used with <see cref="RibbonViewModel.TabRowToolBar"/>.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> which defines the control view models that might be populated on the toolbar.</param>
		/// <returns>A new instance of <see cref="RibbonTabRowToolBarViewModel"/>.</returns>
		protected virtual RibbonTabRowToolBarViewModel CreateTabRowToolBar(BarManager barManager) {
			return new RibbonTabRowToolBarViewModel() {
				Items = {
					barManager.ControlViewModels[BarControlKeys.Share],
				}
			};
		}

		/// <summary>
		/// Gets created instances of <see cref="RibbonTabViewModel"/> that will be used to initialize the
		/// <see cref="RibbonViewModel.Tabs"/> collection.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of type <see cref="RibbonTabViewModel"/>.</returns>
		protected virtual IEnumerable<RibbonTabViewModel> CreateTabs(BarManager barManager) {
			// Create the main tabs of the demo
			yield return CreateHomeTab(barManager);
			yield return CreateInsertTab(barManager);
			yield return CreateViewTab(barManager);

			// Create additional, empty tabs to fill out the tab row
			yield return new RibbonTabViewModel("Draw");
			yield return new RibbonTabViewModel("Design") { KeyTipText = "G" };
			yield return new RibbonTabViewModel("Layout");
			yield return new RibbonTabViewModel("References") { KeyTipText = "S" };
			yield return new RibbonTabViewModel("Mailings");
			yield return new RibbonTabViewModel("Review");
			yield return new RibbonTabViewModel("Help") { KeyTipText = "P" };

			// Create contextual tabs (only visible when related contextual tab group is visible)
			// NOTE: This contextual tab is included only as an example for how one might be defined but the
			//		 group is not activated in the samples and this tab will not be visible
			yield return new RibbonTabViewModel("PictureFormat") {
				ContextualTabGroupKey = ContextualTabGroupKeys.PictureTools,
				KeyTipText = "JP",
			};
		}

		/// <summary>
		/// Creates the <see cref="RibbonTabViewModel"/> that represents the "View" tab of the ribbon.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> which defines the control view models that might be populated on the tab.</param>
		/// <returns>A new instance of <see cref="RibbonTabViewModel"/>.</returns>
		protected virtual RibbonTabViewModel CreateViewTab(BarManager barManager) {
			var imageProvider = barManager.ImageProvider;

			return new RibbonTabViewModel("View") {
				Groups = {
					new RibbonGroupViewModel(BarControlKeys.GroupShow, "Show", "ZW") {
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupShow, BarImageSize.Small),
						Items = {
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
								Items = {
									barManager.ControlViewModels[BarControlKeys.ShowApplicationButton],
									barManager.ControlViewModels[BarControlKeys.ShowQuickAccessToolBar],
								}
							}
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupOther, "Other") {
						SmallImageSource = imageProvider.GetImageSource(BarControlKeys.GroupOther, BarImageSize.Small),
						Items = {
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
								Items = {
									barManager.ControlViewModels[BarControlKeys.SearchForText],
									barManager.ControlViewModels[BarControlKeys.Font],
									barManager.ControlViewModels[BarControlKeys.FontSize]
								}
							}
						}
					},
					new RibbonGroupViewModel(BarControlKeys.GroupGlobalization, "Globalization") {
						CanAutoCollapse = false,
						Items = {
							new RibbonControlGroupViewModel() {
								ItemVariantBehavior = ItemVariantBehavior.AlwaysLarge,
								Items = {
									barManager.ControlViewModels[BarControlKeys.FlowDirection],
								}
							}
						}
					}
				}
			};
		}

		/// <summary>
		/// Gets the <see cref="RecentDocumentManager"/> associated with the view model.
		/// </summary>
		/// <value>A <see cref="RecentDocumentManager"/>.</value>
		public RecentDocumentManager RecentDocuments { get; }

	}

}

﻿using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.ProductSamples.WizardSamples.QuickStart.CustomPageClasses;
using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors {

	/// <summary>
	/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
	/// </summary>
	public class SampleControlBase : UserControl {

		private ICommand comboBoxGalleryCommand;
		private ICommand comboBoxUnmatchedNumberTextCommand;
		private ICommand comboBoxUnmatchedTextCommand;
		private ICommand notImplementedCommand;
		private ICommand textBoxCommitCommand;

		private CollectionViewSource comboBoxColorItems;
		private CollectionViewSource comboBoxEnumItems;
		private CollectionViewSource comboBoxFontFamilyItems;
		private IEnumerable comboBoxFontSizeItems;
		private CollectionViewSource comboBoxNumberItems;
		private CollectionViewSource comboBoxPersonItems;

		#region Dependency Properties

		public static readonly DependencyProperty ComboboxPreviewLabelProperty = DependencyProperty.Register(nameof(ComboboxPreviewLabel), typeof(string), typeof(SampleControlBase), new PropertyMetadata("<None>"));

		#endregion Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the items to be displayed in combobox for selecting colors.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of type <see cref="SimpleComboBoxGalleryItem"/>.</value>
		public IEnumerable ComboBoxColorItems {
			get {
				if (comboBoxColorItems is null) {
					var primaryCategory = "Primary Colors";
					var secondaryCategory = "Secondary Colors";
					
					comboBoxColorItems = BarGalleryViewModel.CreateCollectionViewSource(new [] {
						new TextBarGalleryItemViewModel("Red", primaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.RedSwatch) },
						new TextBarGalleryItemViewModel("Yellow", primaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.YellowSwatch) },
						new TextBarGalleryItemViewModel("Blue", primaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.BlueSwatch) },

						new TextBarGalleryItemViewModel("Orange", secondaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.OrangeSwatch) },
						new TextBarGalleryItemViewModel("Green", secondaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.GreenSwatch) },
						new TextBarGalleryItemViewModel("Purple", secondaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.PurpleSwatch) },
					}, categorize: true);
				}

				return comboBoxColorItems.View;
			}
		}

		/// <summary>
		/// Gets the items to be displayed in combobox based on an enum.
		/// </summary>
		/// <value>An <see cref="IEnumerable"/> of type <see cref="EnumBarGalleryItemViewModel{TEnum}"/>.</value>
		public IEnumerable ComboBoxEnumItems {
			get {
				if (comboBoxEnumItems is null) {
					comboBoxEnumItems = BarGalleryViewModel.CreateCollectionViewSource(
						EnumBarGalleryItemViewModel<SampleEnum>.CreateCollection().Select(vm => {
							// Apply a default category
							if (vm.Category is null)
								vm.Category = "Uncategorized";
							return vm;
						}),
						categorize: true);
				}

				return comboBoxEnumItems.View;
			}
		}

		/// <summary>
		/// Gets the items to be displayed in combobox for selecting font families.
		/// </summary>
		/// <value>An <see cref="IEnumerable"/> of type <see cref="FontFamilyBarGalleryItemViewModel"/>.</value>
		public IEnumerable ComboBoxFontFamilyItems {
			get {
				if (comboBoxFontFamilyItems is null) {
					const string RecentlyUsedCategory = "Recently-Used Fonts";

					comboBoxFontFamilyItems = BarGalleryViewModel.CreateCollectionViewSource(
						new FontFamilyBarGalleryItemViewModel[] {
							new FontFamilyBarGalleryItemViewModel(FontSettings.DefaultFontFamilyName, RecentlyUsedCategory)
						}.Concat(FontFamilyBarGalleryItemViewModel.CreateDefaultCollection()),
					categorize: true);
				}

				return comboBoxFontFamilyItems.View;
			}
		}

		/// <summary>
		/// Gets the items to be displayed in combobox for selecting font sizes.
		/// </summary>
		/// <value>An <see cref="IEnumerable"/> of type <see cref="FontSizeBarGalleryItemViewModel"/>.</value>
		public IEnumerable ComboBoxFontSizeItems {
			get {
				if (comboBoxFontSizeItems is null)
					comboBoxFontSizeItems = FontSizeBarGalleryItemViewModel.CreateDefaultCollection();

				return comboBoxFontSizeItems;
			}
		}

		/// <summary>
		/// Gets the command for a gallery item selection from a combobox.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ComboBoxGalleryCommand {
			get {
				if (comboBoxGalleryCommand is null) {
					comboBoxGalleryCommand = new PreviewableDelegateCommand<IBarGalleryItemViewModel>(
						executeAction:			param => ThemedMessageBox.Show($"The value '{param?.Label}' was matched from the gallery.", "Value Committed", MessageBoxButton.OK, MessageBoxImage.Information),
						canExecuteFunc:			param => true,
						
						// The items of BarComboBox support previewing the current item just like other gallery-based controls
						previewAction:			param => this.ComboboxPreviewLabel = param?.Label ?? "<Unknown>",
						cancelPreviewAction:	param => this.ComboboxPreviewLabel = "<None>"
					);
				}
				return comboBoxGalleryCommand;
			}
		}

		/// <summary>
		/// Gets the items to be displayed in combobox for selecting numbers.
		/// </summary>
		/// <value>An <see cref="IEnumerable"/> of type <see cref="SimpleComboBoxGalleryItem"/>.</value>
		public IEnumerable ComboBoxNumberItems {
			get {
				if (comboBoxNumberItems is null) {
					var evenCategory = "Even Numbers";
					var oddCategory = "Odd Numbers";

					var items = new List<SimpleComboBoxGalleryItem>();
					for (var i = 1; i <= 20; i++) {
						bool isEven = (i % 2 == 0);
						items.Add(new SimpleComboBoxGalleryItem(i.ToString(), (isEven ? evenCategory : oddCategory)));
					}

					comboBoxNumberItems = BarGalleryViewModel.CreateCollectionViewSource(items, categorize: true);
				}

				return comboBoxNumberItems.View;
			}
		}

		/// <summary>
		/// Gets the items to be displayed in combobox for selecting people.
		/// </summary>
		/// <value>An <see cref="IEnumerable"/> of type <see cref="SimpleComboBoxGalleryItem"/>.</value>
		public IEnumerable ComboBoxPersonItems {
			get {
				if (comboBoxPersonItems is null) {
					var items = new List<SimpleComboBoxGalleryItem>();

					foreach (var person in People.All.OrderBy(x => x.FullName))
						items.Add(new SimpleComboBoxGalleryItem(person.FullName, person.Position));

					comboBoxPersonItems = BarGalleryViewModel.CreateCollectionViewSource(items, categorize: true);
				}

				return comboBoxPersonItems.View;
			}
		}
		
		/// <summary>
		/// Gets or sets the combobox preview label.
		/// </summary>
		/// <value>The combobox preview label.</value>
		public string ComboboxPreviewLabel {
			get => (string)GetValue(ComboboxPreviewLabelProperty);
			set => SetValue(ComboboxPreviewLabelProperty, value);
		}
		
		/// <summary>
		/// Gets the command that is executed when an unmatched value is entered into a combobox for selecting numbers.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ComboBoxUnmatchedNumberTextCommand {
			get {
				if (comboBoxUnmatchedNumberTextCommand is null) {
					// This command is raised when text is typed in a BarComboBox that does not match
					// one of the known gallery items
					comboBoxUnmatchedNumberTextCommand = new DelegateCommand<string>(
						executeAction: param => {
							// No action necessary, but show a message to indicate that the value was accepted
							ThemedMessageBox.Show($"The integer text value '{param}' was manually entered and accepted without a match in the gallery.", "Custom Number Text Value Committed", MessageBoxButton.OK, MessageBoxImage.Information);
						},
						canExecuteFunc: param => {
							// The BarComboBox.UnmatchedTextCommand.CanExecute result will determine if the
							// typed text should be allowed... true to allow the value and false to reject it
							return int.TryParse(param, out var number) && number > 0;
						}
					);
				}
				return comboBoxUnmatchedNumberTextCommand;
			}
		}

		/// <summary>
		/// Gets the command that is executed when an unmatched value is entered into a general combobox.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ComboBoxUnmatchedTextCommand {
			get {
				if (comboBoxUnmatchedTextCommand is null) {
					// This command is raised when text is typed in a BarComboBox that does not match
					// one of the known gallery items
					comboBoxUnmatchedTextCommand = new DelegateCommand<string>(
						executeAction: param => {
							// No action necessary, but show a message to indicate that the value was accepted
							ThemedMessageBox.Show($"The text value '{param}' was manually entered and accepted without a match in the gallery.", "Custom Text Value Committed", MessageBoxButton.OK, MessageBoxImage.Information);
						},
						canExecuteFunc: param => {
							// The BarComboBox.UnmatchedTextCommand.CanExecute result will determine if the
							// typed text should be allowed... true to allow the value and false to reject it
							return true;
						}
					);
				}
				return comboBoxUnmatchedTextCommand;
			}
		}

		/// <summary>
		/// Gets the committed text associated with <see cref="TextBoxCommitCommand"/>.
		/// </summary>
		/// <param name="commandParameter">The parameter passed to <see cref="TextBoxCommitCommand"/>.</param>
		/// <returns>The committed text; or <c>null</c> if the text could not be determined.</returns>
		protected virtual string GetTextBoxCommitCommandText(object commandParameter) {
			return null;
		}

		/// <summary>
		/// Gets the command for functionality that has not been implemented by the sample.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand NotImplementedCommand {
			get {
				if (notImplementedCommand is null) {
					notImplementedCommand = new DelegateCommand<object>(
						param => {
							ThemedMessageBox.Show(
								"This control is for user interface demonstration purposes only and no application functionality has been implemented for it.", "Not Implemented",
								MessageBoxButton.OK, MessageBoxImage.Information);
						}
					);

				}
				return notImplementedCommand;
			}
		}

		/// <summary>
		/// Gets the command for a commit from a textbox.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand TextBoxCommitCommand {
			get {
				if (textBoxCommitCommand is null) {
					textBoxCommitCommand = new DelegateCommand<string>(
						executeAction:			param => ThemedMessageBox.Show($"The value '{GetTextBoxCommitCommandText(param)}' was committed from the textbox.", "Value Committed", MessageBoxButton.OK, MessageBoxImage.Information),
						canExecuteFunc:			param => true
					);
				}
				return textBoxCommitCommand;
			}
		}

	}

}

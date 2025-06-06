﻿using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for image and text content within a ribbon footer.
	/// </summary>
	public class RibbonFooterSimpleContentViewModel : ObservableObjectBase, IHasTag {

		private ImageSource imageSource;
		private object tag;
		private string text;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="System.Windows.Media.ImageSource"/> for the image.
		/// </summary>
		/// <value>The <see cref="System.Windows.Media.ImageSource"/> for the image.</value>
		public ImageSource ImageSource {
			get => imageSource;
			set {
				if (imageSource != value) {
					imageSource = value;
					this.NotifyPropertyChanged(nameof(ImageSource));
				}
			}
		}

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object Tag {
			get => tag;
			set {
				if (tag != value) {
					tag = value;
					this.NotifyPropertyChanged(nameof(Tag));
				}
			}
		}

		/// <summary>
		/// Gets or sets the text content.
		/// </summary>
		/// <value>The text content.</value>
		public string Text {
			get => text;
			set {
				if (text != value) {
					text = value;
					this.NotifyPropertyChanged(nameof(Text));
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Text='{this.Text}']";
		}
		
	}

}

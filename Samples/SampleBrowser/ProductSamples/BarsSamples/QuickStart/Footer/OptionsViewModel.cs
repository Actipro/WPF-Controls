using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Bars;
using System.ComponentModel;
using System.Windows;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.Footer {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class OptionsViewModel : ObservableObjectBase {

		private RibbonFooterKind					footerKind		= RibbonFooterKind.Warning;
		private bool								isFooterVisible	= true;
		private Thickness							padding			= new Thickness(10, 5, 10, 5);
		private RibbonQuickAccessToolBarLocation	qatLocation		= RibbonQuickAccessToolBarLocation.Below;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets of sets the footer kind.
		/// </summary>
		/// <value>One of the <see cref="RibbonFooterKind"/> values.</value>
		[DisplayName("Kind")]
		public RibbonFooterKind FooterKind {
			get => footerKind;
			set {
				if (footerKind != value) {
					footerKind = value;
					NotifyPropertyChanged(nameof(FooterKind));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the footer is visible.
		/// </summary>
		/// <value><c>true</c> if the footer is visible; otherwise <c>false</c>.</value>
		public bool IsFooterVisible {
			get => isFooterVisible;
			set {
				if (isFooterVisible != value) {
					isFooterVisible = value;
					NotifyPropertyChanged(nameof(IsFooterVisible));
				}
			}
		}

		/// <summary>
		/// Gets or sets the padding for the footer content.
		/// </summary>
		/// <value>A <see cref="Thickness"/> value.</value>
		[DisplayName("Padding")]
		public Thickness Padding {
			get => padding;
			set {
				if (padding != value) {
					padding = value;
					NotifyPropertyChanged(nameof(Padding));
				}
			}
		}

		/// <summary>
		/// Gets or sets the location of the Quick Access Toolbar.
		/// </summary>
		/// <value>One of the <see cref="RibbonQuickAccessToolBarLocation"/> values.</value>
		[DisplayName("QAT Location")]
		public RibbonQuickAccessToolBarLocation QuickAccessToolBarLocation {
			get => qatLocation;
			set {
				if (qatLocation != value) {
					qatLocation = value;
					NotifyPropertyChanged(nameof(QuickAccessToolBarLocation));
				}
			}
		}

	}

}

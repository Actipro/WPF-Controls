using ActiproSoftware.Products;
using ActiproSoftware.Text.Parsing;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Remove the C# parser
			resultEditor.Document.Language.UnregisterService<IParser>();
			
			// Bind products and resources
			this.BindProducts();
			this.BindResources();
        }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Binds the list of products.
		/// </summary>
		private void BindProducts() {
			// Manually reference these type to ensure the related assemblies are loaded since they may not yet have been loaded by default
			var srTypes = new Type[] {
				// None: typeof(ActiproSoftware.Products.SyntaxEditor.Addons.JavaScript.SR),
				typeof(ActiproSoftware.Products.SyntaxEditor.Addons.Python.SR),
				typeof(ActiproSoftware.Products.SyntaxEditor.Addons.Xml.SR),
				typeof(ActiproSoftware.Products.Text.Addons.JavaScript.SR),
				typeof(ActiproSoftware.Products.Text.Addons.Python.SR),
				typeof(ActiproSoftware.Products.Text.Addons.Xml.SR),
			};

			var productResources = new List<ProductResource>();

			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				var name = assembly.GetName().Name;
				if ((name.StartsWith("ActiproSoftware.", StringComparison.OrdinalIgnoreCase)) && (name.EndsWith(".Wpf", StringComparison.OrdinalIgnoreCase))) {
					var productResource = new ProductResource(assembly);
					if (productResource.IsValid)
						productResources.Add(productResource);
				}
			}

			productResources.Sort((x, y) => x.Name.CompareTo(y.Name));

			productComboBox.ItemsSource = productResources;

			if (productComboBox.Items.Count > 0)
				productComboBox.SelectedIndex = 0;
		}

		/// <summary>
		/// Binds the list of resources.
		/// </summary>
		private void BindResources() {
			var productResource = (ProductResource)productComboBox.SelectedItem;
			this.BindResources(productResource.SRType, productResource.SRNameType);
		}

		/// <summary>
		/// Binds the list of resources for the specified <see cref="Type"/>.
		/// </summary>
		/// <param name="srType">The <see cref="SRBase"/> <see cref="Type"/>.</param>
		/// <param name="enumType">The enumeration <see cref="Type"/>.</param>
		private void BindResources(Type srType, Type enumType) {
			if (resourcesListView == null)
				return;

			// Build list
			List<ResourceData> resources = new List<ResourceData>();
			string[] names = Enum.GetNames(enumType);
			foreach (string name in names)
				resources.Add(new ResourceData(srType, enumType, name));

			// Bind
			resourcesListView.ItemsSource = resources;
		
			// Update selection
			if (resourcesListView.Items.Count > 0)
				resourcesListView.SelectedIndex = 0;
		}
		
		/// <summary>
		/// Occurs when the value has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="TextChangedEventArgs"/> that contains the event data.</param>
		private void OnCustomValueTextBoxTextChanged(object sender, TextChangedEventArgs e) {
			this.UpdateCustomizedOutput();
		}

		/// <summary>
		/// Occurs when the selection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnProductComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			// Bind resources
			this.BindResources();
		}
		
		/// <summary>
		/// Occurs when the selection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnResourcesListViewSelectionChanged(object sender, SelectionChangedEventArgs e) {
			ResourceData resource = resourcesListView.SelectedItem as ResourceData;
			if (resource != null) {
				// Update the customized text
				customValueTextBox.Text = resource.Value;
			}

			this.UpdateCustomizedOutput();
		}

		/// <summary>
		/// Updates the customized output.
		/// </summary>
		private void UpdateCustomizedOutput() {
			if (resourcesListView == null)
				return;

			// Get the resource
			ResourceData resource = resourcesListView.SelectedItem as ResourceData;
			if (resource == null)
				return;

			// Generate output
			resultEditor.Document.SetText(String.Format("{0}.SR.SetCustomString({0}.SRName.{1}.ToString(), \"{2}\");", 
				resource.EnumType.Namespace, resource.Name, customValueTextBox.Text.Replace("\"", "\\\"")));
		}
		
	}

}
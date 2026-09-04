using ActiproSoftware.Properties;
using ActiproSoftware.Text.Parsing;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Remove the C# parser
		resultEditor.Document.Language.UnregisterService<IParser>();

		// Bind products and resources
		BindProducts();
		BindResources();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Binds the list of products.
	/// </summary>
	private void BindProducts() {
		// Manually reference these type to ensure the related assemblies are loaded since they may not yet have been loaded by default
		var srTypes = new Type[] {
			// None: typeof(ActiproSoftware.Properties.SyntaxEditor.Addons.JavaScript.SR),
			typeof(ActiproSoftware.Properties.SyntaxEditor.Addons.Python.SR),
			typeof(ActiproSoftware.Properties.SyntaxEditor.Addons.Xml.SR),
			typeof(ActiproSoftware.Properties.Text.Addons.JavaScript.SR),
			typeof(ActiproSoftware.Properties.Text.Addons.Python.SR),
			typeof(ActiproSoftware.Properties.Text.Addons.Xml.SR),
		};

		var productResources = new List<ProductResource>();

		foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
			var name = assembly.GetName().Name;
			if (
				name?.StartsWith("ActiproSoftware.", StringComparison.OrdinalIgnoreCase) == true
				&& name.EndsWith(".Wpf", StringComparison.OrdinalIgnoreCase)
			) {
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
		if (productComboBox.SelectedItem is ProductResource { IsValid: true } productResource)
			BindResources(productResource.SRType!, productResource.SRNameType!);
	}

	/// <summary>
	/// Binds the list of resources for the specified <see cref="Type"/>.
	/// </summary>
	/// <param name="srType">The <see cref="SRBase"/> <see cref="Type"/>.</param>
	/// <param name="enumType">The enumeration <see cref="Type"/>.</param>
	private void BindResources(Type srType, Type enumType) {
		if (resourcesListView is null)
			return;

		// Build list
		var resources = new List<ResourceData>();
		foreach (string name in Enum.GetNames(enumType))
			resources.Add(new ResourceData(srType, enumType, name));

		// Bind
		resourcesListView.ItemsSource = resources;

		// Update selection
		if (resourcesListView.Items.Count > 0)
			resourcesListView.SelectedIndex = 0;
	}

	private void OnCustomValueTextBoxTextChanged(object sender, TextChangedEventArgs e)
		=> UpdateCustomizedOutput();

	private void OnProductComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		=> BindResources();

	private void OnResourcesListViewSelectionChanged(object sender, SelectionChangedEventArgs e) {
		if (resourcesListView.SelectedItem is ResourceData resource) {
			// Update the customized text
			customValueTextBox.Text = resource.Value;
		}

		UpdateCustomizedOutput();
	}

	private void UpdateCustomizedOutput() {
		// Get the resource
		if (resourcesListView?.SelectedItem is not ResourceData resource)
			return;

		// Generate output
		resultEditor.Document.SetText(
			string.Format("{0}.SR.SetCustomString({0}.SRName.{1}.ToString(), \"{2}\");",
				resource.EnumType.Namespace,
				resource.Name,
				customValueTextBox.Text.Replace("\"", "\\\"")
			)
		);
	}

}

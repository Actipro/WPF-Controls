---
title: "Validation Rules"
page-title: "Validation Rules - Bar Code Advanced Features"
order: 4
---
# Validation Rules

Each symbology included with Actipro Bar code has a related `ValidationRule` implementation that may be used when data binding in XAML to ensure that a value supplied is supported by the symbology.

All the validation rules are located in the [ActiproSoftware.Windows.Controls.BarCode.ValidationRules](xref:@ActiproUIRoot.Controls.BarCode.ValidationRules) namespace.

## Using a ValidationRule

This sample XAML code shows how to use the validation rule for `Code 39 Extended`, [Code39ExtendedValidationRule](xref:@ActiproUIRoot.Controls.BarCode.ValidationRules.Code39ExtendedValidationRule), to ensure that the value that is bound to a `TextBox` is valid for that symbology.  When the value is not valid, the error message will be set to a `ToolTip` on the `TextBox`.

```xaml
<StackPanel>
	<barCode:BarCode HorizontalAlignment="Left" Caption="Product ID" BorderThickness="1" CornerRadius="3">
		<barCode:Code39ExtendedSymbology x:Name="symbology" Value="ABC-123" />
	</barCode:BarCode>			
	<TextBox>
		<TextBox.Style>
			<Style TargetType="{x:Type TextBox}">
				<Style.Triggers>
					<Trigger Property="Validation.HasError" Value="true">
						<Setter Property="ToolTip" Value="{Binding RelativeSource={RelativeSource Self}, Path=(Validation.Errors)[0].ErrorContent}" />
					</Trigger>
				</Style.Triggers>
			</Style>
		</TextBox.Style>
		<TextBox.Text>
			<Binding ElementName="symbology" Path="Value" Mode="TwoWay" UpdateSourceTrigger="PropertyChanged">
				<Binding.ValidationRules>
					<barCode:Code39ExtendedValidationRule />
				</Binding.ValidationRules>
			</Binding>
		</TextBox.Text>
	</TextBox>
</StackPanel>
```

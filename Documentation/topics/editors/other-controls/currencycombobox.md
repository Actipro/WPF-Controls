---
title: "CurrencyComboBox"
page-title: "CurrencyComboBox - Other Editors Controls"
order: 6
---
# CurrencyComboBox

The [CurrencyComboBox](xref:ActiproSoftware.Windows.Controls.Editors.CurrencyComboBox) can be used to select a [Currency](xref:ActiproSoftware.Windows.Controls.Editors.Currency) from a list of currencies in the world.  The currency code can then be used with LINQ and [CountryCurrencyMapping](xref:ActiproSoftware.Windows.Controls.Editors.CountryCurrencyMapping) to query the [Country](xref:ActiproSoftware.Windows.Controls.Editors.Country) or countries that use that [Currency](xref:ActiproSoftware.Windows.Controls.Editors.Currency).

![Screenshot](../images/currencycombobox-closed.png)

## Overview

The [CurrencyComboBox](xref:ActiproSoftware.Windows.Controls.Editors.CurrencyComboBox) extends the native ComboBox and displays a list of currencies as defined by the ISO.  A currency is represented by an instance of [Currency](xref:ActiproSoftware.Windows.Controls.Editors.Currency), and the list of currencies is defined by the [Currency](xref:ActiproSoftware.Windows.Controls.Editors.Currency).[Currencies](xref:ActiproSoftware.Windows.Controls.Editors.Currency.Currencies) collection.

## List of Currencies

The list of currencies can be customized by updating the [Currency](xref:ActiproSoftware.Windows.Controls.Editors.Currency).[Currencies](xref:ActiproSoftware.Windows.Controls.Editors.Currency.Currencies) collection.  Items can be added or removed as needed directly from this list.  In addition, a custom list can be assigned to the `ItemsSource` property.

## Selection

The selected [Currency](xref:ActiproSoftware.Windows.Controls.Editors.Currency) is accessible through the `SelectedItem` property.  In addition, the unique ISO code associated with the currency can be accessed through the `SelectedValue` property.

## Country/Currency Mapping

See the [CountryComboBox](countrycombobox.md) topic for more information on the [CountryCurrencyMapping](xref:ActiproSoftware.Windows.Controls.Editors.CountryCurrencyMapping) class.

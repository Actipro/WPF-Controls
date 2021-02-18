---
title: "Auto-Corrector"
page-title: "Auto-Corrector - Feature Services - SyntaxEditor Language Creation Guide"
order: 17
---
# Auto-Corrector

An auto-corrector is a language service that typically occurs after text changes and can fix up things like character casing.

A pre-built class is available that can help add automatic case correction to a language.

## Basic Concepts

Auto-correctors are classes implementing [IAutoCorrector](xref:ActiproSoftware.Text.Analysis.IAutoCorrector) with an [AutoCorrect](xref:ActiproSoftware.Text.Analysis.IAutoCorrector.AutoCorrect*) method that performs some modification of text over the specified range.

### AutoCorrectorBase Class

An abstract [AutoCorrectorBase](xref:ActiproSoftware.Text.Analysis.Implementation.AutoCorrectorBase) class is available that implements [IAutoCorrector](xref:ActiproSoftware.Text.Analysis.IAutoCorrector), providing a great starting point for an auto-corrector.  This class watches for document text changes and caret moves.  If a document text change spans multiple lines, auto-correct is immediately called for the affected lines.  If a document text change occurs on a single line, a flag is set.  When the caret moves to a new line, auto-correct is called for the previously modified line.

All the an inheritor class needs to implement is an override for the [AutoCorrect](xref:ActiproSoftware.Text.Analysis.Implementation.AutoCorrectorBase.AutoCorrect*) method to perform the actual auto-correct logic.

### AutoCaseCorrector Class

The [AutoCaseCorrector](xref:ActiproSoftware.Text.Analysis.Implementation.AutoCaseCorrector) class inherits [AutoCorrectorBase](xref:ActiproSoftware.Text.Analysis.Implementation.AutoCorrectorBase) and implements auto-correct behavior to cycle over the tokens in the specified range.

The [GetCaseCorrectText](xref:ActiproSoftware.Text.Analysis.Implementation.AutoCaseCorrector.GetCaseCorrectText*) method is passed the current [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) and the [IToken](xref:ActiproSoftware.Text.Lexing.IToken) to examine.  It returns a string value if there is some auto-correction of text that should be made.  It can return `null` to indicate no auto-correct change is necessary.

The method is virtual and its default implementation looks to see if the token is an [IMergableToken](xref:ActiproSoftware.Text.Lexing.IMergableToken).  If so, it checks to see if the token didn't match its source lexical pattern in a case sensitive way.  In that scenario, it returns the [IMergableToken](xref:ActiproSoftware.Text.Lexing.IMergableToken).[AutoCaseCorrectText](xref:ActiproSoftware.Text.Lexing.IMergableToken.AutoCaseCorrectText) value.

Thus the [AutoCaseCorrector](xref:ActiproSoftware.Text.Analysis.Implementation.AutoCaseCorrector) can be registered on any language that uses a dynamic lexer and it will work out of the box to provide case correction functionality.  Note that the lexer must have some lexical patterns' case sensitivities marked as auto-correct.

For more advanced programmatic lexers, as long as they are mergable, this class can still provide a good base for an auto-case corrector.  Simply override the [GetCaseCorrectText](xref:ActiproSoftware.Text.Analysis.Implementation.AutoCaseCorrector.GetCaseCorrectText*) method and implement logic to determine if the token was a case sensitive match.  If it wasn't, return the auto-case corrected text to use.

## Registering with a Language

Any object that implements [IAutoCorrector](xref:ActiproSoftware.Text.Analysis.IAutoCorrector) can be associated with a syntax language by registering it as an [IAutoCorrector](xref:ActiproSoftware.Text.Analysis.IAutoCorrector) service on the language.

This code creates an [AutoCaseCorrector](xref:ActiproSoftware.Text.Analysis.Implementation.AutoCaseCorrector) object and registers it with the syntax language that is already declared in the `language` variable:

```csharp
AutoCaseCorrector corrector = new AutoCaseCorrector();
language.RegisterAutoCorrector(corrector);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterAutoCorrector](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterAutoCorrector*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling Auto-Correct

Auto-correct can be disabled within a SyntaxEditor instance by setting the [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[IsAutoCorrectEnabled](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.IsAutoCorrectEnabled) property to `false`.

---
title: "Overview"
page-title: "Core Text Features - SyntaxEditor Text/Parsing Framework"
order: 1
---
# Overview

The core text features presented in this portion of the documentation are essential knowledge for working with nearly every other portion of the product.

## Documents, Snapshots, and Versions

Documents, snapshots, and versions are used to store text data.  Documents allow for text updates, while a snapshot provides an immutable view of text within a document for a certain version that can be read.

See the [Documents, Snapshots, and Versions](documents-snapshots-versions.md) topic for more information.

## Offsets, Ranges, and Positions

There are several ways to reference the location of characters within a string or snapshot, each with their own benefits.

See the [Offsets, Ranges, and Positions](offsets-ranges-positions.md) topic for more information.

## Line Terminators

While the text framework can load text using any sort of standard line terminator character sequence, the framework internally tracks all line terminators as a single line feed character.  Text can easily be exported back out with any line terminator.

See the [Line Terminators](line-terminators.md) topic for more information.

## Snapshot Translation

Offsets and offset ranges can be translated from one snapshot to another using a feature called snapshot translation.  Snapshots are immutable views of a document and are able to determine the changes made between any two snapshots for the same document.  This allows for the locating of a certain offset or range of text in the current snapshot based on some offset/range created using an older snapshot, even with multiple document changes being made in between the two snapshots.

See the [Snapshot Translation](snapshot-translation.md) topic for more information.

## Text Changes and Operations

A large portion of the text framework is focused on the ability to make changes to document text.  Multiple individual replace transactions, called text change operations, can be batched up into a single text change and performed together as a single modification.

See the [Text Changes and Operations](text-changes.md) topic for more information.

## Scanning Text Using a Reader

The text framework has very robust features built-in for scanning through snapshot text via the use of readers.

See the [Scanning Text Using a Reader](scanning-text.md) topic for more information.

## Loading and Saving Files

It's very easy to load document text from a file, stream, or string.  Likewise, document text can be saved back out to a file, stream, or string.

See the [Loading and Saving Files](loading-saving-files.md) topic for more information.

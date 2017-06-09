# Sitecore Content Anonymizer Module Readme

> Note: This module supports Sitecore 8.1+. For prior versions, please submit an Issue, and I will try to add support.  You can also submit a pull request.

## Overview

The Sitecore Content Anonymizer allows anonymization of the field values on items. 
Anonymization is performed per template type on a field-by-field basis.
The Content Anonymizer allows administrators to anonymize data within Sitecore's content tree. 
A few examples of how data can be anonymized are as follows:

1. Paragraph text replaced with Lorem Ipsum text.
1. First and last names replaced with randomly selected names.
1. Email addresses replaced with new addresses based on the randomly generated names.
1. Dates replaced with randomly generated dates.

There may be several reasons to anonymize data:

1. Data from an existing client implementation might need to be anonymized so it can be used for a demo.
1. Data from live should not be brought down to development due to confidentiality.
1. Data needs to be sent to Sitecore for support, but needs to be anonymized for confidentiality.

### Features

Here are the key features of the Data Anonymizer:

1. Anonymize the values of fields on items based on the underlying template.
1. Anonymization is applied to selected items of the selected template type.
1. Only the latest version of the items are anonymized. **All prior versions of an item will be removed and the version number will be reset to 1**
1. All language versions are anonymized.  Currently English based content will be used for some anonymization types.
1. Only fields with an anonymization type selected will be anonymized.
1. Fields that are are currently empty can be optionally filled with anonymized data.
1. 30+ out-of-the-box field value anonymization formats.
1. Basic custom field formats are supported.  This allows combining one or more field values into a string for use on another field.
1. Item renaming based on custom fields is supported.  Items names will be updated to follow the configured Sitecore naming conventions.
1. Global search and replace can be performed on all fields that are not flagged to be anonymized.

> Note: Anonymization is a permanent action to your data.  It is recommended to back up the database(s) before anonymizing.

### Supported Fields

The following field types and anonymization options are supported by the Data Anonymizer:

* Date
    * Past
    * Future
    * Recent
* Integer
    * Random
* Single-Line Text | Multi-Line Text | Rich Text
    * Custom Format
    * Lorem
        * Replace
        * Words
        * Sentence
        * Sentences
        * Paragraph
        * Paragraphs
    * Name
        * First
        * Last
        * Prefix
        * Suffix
    * Phone
        * Phone
        * Fax
        * Mobile
    * Internet
        * Email
        * URL
        * UserName
    * Date
        * Past
        * Future
        * Recent
    * Number
        * Integer
    * Address
        * Street
        * City
        * State
        * PostalCode
        * Country
        * Latitude
        * Longitude
        * Coordinates
* File
    * Random - Randomly chosen media item.
* Image
    * Random - Randomly chosen image.

> Note: Other field types are not currently supported.  Relationship-based fields, such as Droplink and Multi-list, should be anonymized by anonymizing the related item.

### Cautions
**Only content you choose to be anonymized will be anonymized.  This only includes content in the content tree.  Other content and data will NOT be anonymized.**

## Installation

Install the update packages located here: https://github.com/onenorth/content-anonymizer/releases

## Configuration

The Sitecore Content Anonymizer is dependent on the "sitecore_master_index" Lucene index.
For the Anonymizer to process all items of a template type, the maximum number of items returned from a Lucene query needs to be increased.
Set the following in a App_Config/Include .config file.
Confirm the setting by viewing /sitecore/admin/showconfig.aspx

    <setting patch:instead="*[@name='Query.MaxItems']" name="Query.MaxItems" value="0"/>

## Usage

The Content Anonymizer lives under the Sitecore Admin folder.  To run the Anonymizer, navigate to:

    Desktop > Start Menu > Demo Data Tools > Content Anonymizer

or

    http://{site domain}/sitecore/admin/contentanonymizer

You will be required to sign in if you are not already signed in.

### Select Template
To start, you first need to select a template.
Data is anonymized based on the template selected.
Only items based on the chosen template can be anonymized.
To select a template, start typing the template name in the type-ahead.  
A list of templates should appear that contain the text you typed.
Select the template you desire.

![Select Template](https://raw.github.com/onenorth/content-anonymizer/master/img/select-template.png)

Once the template is chosen, additional options appear.

### Configure Search and Replace
You can define a global search and replace for text-based fields that are not chosen to be anonymized.
If you need to add a search and replace, click the **Add** button.
Click the Add button as many times as you need search and replace.
The content entered in the **Replace** field is replaced with the content entered in the **With** field.
The replacement is case sensitive.

![Configure Search and Replace](https://raw.github.com/onenorth/content-anonymizer/master/img/configure-search-and-replace.png)

### Configure Item Names
You can optionally rename the items that are anonymized.
Currently, **Custom Format** and **Lorem Replace** anonymization types are supported.
If renaming, choose an anonymization type.

For Custom Format, type of the name of the custom format to use and chose the item in the type-ahead.
The type-ahead will search the names provided in the Custom Format section.
You can define a custom format by clicking the "...".
The Configure Custom Formats dialog will appear.
You need to specify the language that the custom format will use to populate the name. 

![Configure Naming](https://raw.github.com/onenorth/content-anonymizer/master/img/configure-naming.png)

### Configure Custom Formats
Custom formats allow you to combine one or more value(s) into a single string format.
The resulting string can be used for text-based fields or the name of the item.

If you need a custom format, click the **Add** button.
Click Add as many times as you need custom formats.

Each entry needs a **name**.
The name is used when specifying the Item Name or Field format.
The name can be anything you desire.

Each entry also needs at least one **token**.
The token is tied to a field and represents the chosen field.
The first field, will be assigned "$0" as the token.
The second "$1", third "$2", and so on.
The tokens are used in the format string.

A **format** string also needs to be specified.
The format string should contain the tokens and any desired surrounding text.
The **result** shows the interpreted format.

An Example may be as follows for an email address related to person template:
* **Name**: Email
* **Tokens**: $0|FirstName  $1|LastName
* **Format**: $0_$1@domain.com
* **Result**: FirstName_LastName@domain.com

![Configure Custom Formats](https://raw.github.com/onenorth/content-anonymizer/master/img/configure-custom-formats.png)

> Note: Configured Formats will persist when changing Templates.  However, the Formats will only be selectable if the Fields used within the Tokens match with the Fields available within the current Template.

### Configure Fields
Choose which fields you want to anonymize by specifying the type of anonymization.
Fields that do not have a selected anonymization type will not be anonymized.
Relationships remain as-is (Anonymize relationships by anonymizing the target template).
You can optionally anonymize fields that are currently empty by selecting the **Anonymize Empty Values** checkbox.

![Configure Fields](https://raw.github.com/onenorth/content-anonymizer/master/img/configure-fields.png)

### Configure Standard Fields
Choose which standard fields you want to anonymize by specifying the type of anonymization.
This functionality is the same as for configuring normal fields.

![Configure Fields](https://raw.github.com/onenorth/content-anonymizer/master/img/configure-standardfields.png)

### Select Items
Choose which items you want to anonymize.
You can optionally select all with the **all items** checkbox.

![Configure Items](https://raw.github.com/onenorth/content-anonymizer/master/img/configure-items.png)

### Running
To run the anonymization, click the **Anonymize** button.
> Note: the anonymize button appears disabled if required fields are not populated. Please make sure all required fields have been filled out.

You will see a confirmation dialog appear that summarizes the selections.

![Confirm](https://raw.github.com/onenorth/content-anonymizer/master/img/confirm.png)

If everything looks okay, click the **Anonymize** button on the dialog.

## Development
In order to develop, you must update the references to the Sitecore assemblies.
To do this, open the **Properties** of the **OneNorth.ContentAnonymizer** project.
In the properties, navigate to the **Reference Paths** tab.
Enter the location of the bin folder for the Sitecore installation.

In order to generate the .update packages, TDS needs to be updated to reference the location of the Sitecore assemblies.
To do this, open the **Properties** of the **TDS.Core** project.
In the properties, navigate to the **Update Package** tab.
Enter the location of the bin folder for the Sitecore installation in the **Sitecore Assembly Path** field. 

#License

The associated code is released under the terms of the [MIT license](http://onenorth.mit-license.org).

The Sitecore Content Anonymizer was inspired by and has used data definitions from:
* https://github.com/marak/Faker.js/ - Copyright (c) 2014-2015 Matthew Bergman & Marak Squires 
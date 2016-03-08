using System.Collections.Generic;

namespace OneNorth.ContentAnonymizer.Data.Locales
{
    public interface ILocale
    {
        List<string> AddressCityPrefix { get; }
        List<string> AddressCitySuffix { get; }
        List<string> AddressCounty { get; }
        List<string> AddressCountry { get; }
        List<string> AddressCountryCode { get; }
        List<string> AddressBuildingNumber { get; }
        List<string> AddressStreetSuffix { get; }
        List<string> AddressSecondaryAddress { get; }
        List<string> AddressPostalCode { get; }
        List<string> AddressState { get; }
        List<string> AddressStateAbbreviation { get; }
        List<string> AddressTimeZone { get; }
        List<string> CompanySuffix { get; }
        List<string> CompanyAdjective { get; }
        List<string> CompanyDescriptor { get; }
        List<string> CompanyNown { get; }
        List<string> InternetFreeEmail { get; }
        List<string> InternetDomainSuffix { get; }
        List<string> LoremWords { get; }
        List<string> LoremSupplemental { get; }
        List<string> NameFirst { get; }
        List<string> NameLast { get; }
        List<string> NamePrefix { get; }
        List<string> NameSuffix { get; }
        List<string> NameTitleDescriptor { get; }
        List<string> NameTitleLevel { get; }
        List<string> NameTitleJob { get; }
        List<string> PhoneNumberFormats { get; }
        List<string> CellPhoneNumberFormats { get; }
    }
}
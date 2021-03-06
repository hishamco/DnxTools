﻿using System.Reflection;
using NuGet;

namespace NuGetPackageVerifier
{
    public static class PackageIssueFactory
    {
        public static PackageVerifierIssue AssemblyMissingServicingAttribute(string assemblyPath)
        {
            return new PackageVerifierIssue(
                "SERVICING_ATTRIBUTE",
                assemblyPath,
                string.Format(
                    @"The managed assembly '{0}' in this package is missing the '[assembly: AssemblyMetadata(""Serviceable"", ""True"")]' attribute.",
                    assemblyPath,
                    typeof(AssemblyFileVersionAttribute).Name),
                MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue AssemblyMissingFileVersionAttribute(string assemblyPath)
        {
            return AssemblyMissingVersionAttributeCore("VERSION_FILEVERSION", assemblyPath, typeof(AssemblyFileVersionAttribute).Name);
        }

        public static PackageVerifierIssue AssemblyMissingInformationalVersionAttribute(string assemblyPath)
        {
            return AssemblyMissingVersionAttributeCore("VERSION_INFORMATIONALVERSION", assemblyPath, typeof(AssemblyInformationalVersionAttribute).Name);
        }

        private static PackageVerifierIssue AssemblyMissingVersionAttributeCore(string issueId, string assemblyPath, string attributeName)
        {
            return new PackageVerifierIssue(issueId, assemblyPath, string.Format("The managed assembly '{0}' in this package is missing the '{1}' attribute.", assemblyPath, attributeName), MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue AssemblyNotStrongNameSigned(string assemblyPath, int hResult)
        {
            // TODO: Translate common HRESULTS http://blogs.msdn.com/b/yizhang/
            return new PackageVerifierIssue("SIGN_STRONGNAME", assemblyPath, string.Format("The managed assembly '{0}' in this package is either not signed or is delay signed. HRESULT=0x{1:X}", assemblyPath, hResult), MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue NotSemanticVersion(SemanticVersion version)
        {
            return new PackageVerifierIssue("VERSION_NOTSEMANTIC",
                    string.Format("Version '{0}' does not follow semantic versioning guidelines.", version), MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue Satellite_PackageSummaryNotLocalized()
        {
            return new PackageVerifierIssue("LOC_SUMMARY", "Package summary is not localized correctly", MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue Satellite_PackageTitleNotLocalized()
        {
            return new PackageVerifierIssue("LOC_TITLE", "Package title is not localized correctly", MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue Satellite_PackageDescriptionNotLocalized()
        {
            return new PackageVerifierIssue("LOC_DESC", "Package description is not localized correctly", MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue RequiredCopyright()
        {
            return RequiredCore("NUSPEC_COPYRIGHT", "Copyright", MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue RequiredLicenseUrl()
        {
            return RequiredCore("NUSPEC_LICENSEURL", "License Url", MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue RequiredIconUrl()
        {
            return RequiredCore("NUSPEC_ICONURL", "Icon Url", MyPackageIssueLevel.Warning);
        }

        public static PackageVerifierIssue RequiredTags()
        {
            return RequiredCore("NUSPEC_TAGS", "Tags", MyPackageIssueLevel.Warning);
        }

        public static PackageVerifierIssue RequiredTitle()
        {
            return RequiredCore("NUSPEC_TITLE", "Title", MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue RequiredSummary()
        {
            return RequiredCore("NUSPEC_SUMMARY", "Summary", MyPackageIssueLevel.Warning);
        }

        public static PackageVerifierIssue RequiredProjectUrl()
        {
            return RequiredCore("NUSPEC_PROJECTURL", "Project Url", MyPackageIssueLevel.Warning);
        }

        public static PackageVerifierIssue RequiredRequireLicenseAcceptanceTrue()
        {
            return new PackageVerifierIssue("NUSPEC_ACCEPTLICENSE", string.Format("NuSpec Require License Acceptance is not set to true"), MyPackageIssueLevel.Error);
        }

        private static PackageVerifierIssue RequiredCore(string issueId, string attributeName, MyPackageIssueLevel issueLevel)
        {
            return new PackageVerifierIssue(issueId, string.Format("NuSpec {0} attribute is missing", attributeName), issueLevel);
        }

        public static PackageVerifierIssue PowerShellScriptNotSigned(string scriptPath)
        {
            return new PackageVerifierIssue("SIGN_POWERSHELL", scriptPath, string.Format("The PowerShell script '{0}' is not signed.", scriptPath), MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue PEFileNotAuthenticodeSigned(string assemblyPath)
        {
            return new PackageVerifierIssue("SIGN_AUTHENTICODE", assemblyPath, string.Format("The PE file '{0}' in this package is not authenticode signed.", assemblyPath), MyPackageIssueLevel.Error);
        }

        public static PackageVerifierIssue AssemblyHasNoDocFile(string assemblyPath)
        {
            return new PackageVerifierIssue("DOC_MISSING", assemblyPath, string.Format("The assembly '{0}' doesn't have a corresponding XML document file.", assemblyPath), MyPackageIssueLevel.Warning);
        }
    }
}

using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
namespace UnityEditor.AddressableAssets.Build.AnalyzeRules
{
    [InitializeOnLoad]
    internal class RegisterAnalyzeDuplicateAddressableNames
    {
        static RegisterAnalyzeDuplicateAddressableNames()
        {
            AnalyzeSystem.RegisterNewRule<AnalyzeDuplicateAddressableNames>();
        }
    }

    internal class AnalyzeDuplicateAddressableNames : AnalyzeRule
    {

        public override bool CanFix
        {
            get { return false; }
        }
        public override string ruleName
        {
            get { return "Check Duplicate Addressable Names"; }
        }


        public override List<AnalyzeResult> RefreshAnalysis(AddressableAssetSettings settings)
        {
            List<AnalyzeResult> results = new List<AnalyzeResult>();
            HashSet<string> addressable_names = new HashSet<string>();

            for (int i = 0; i < settings.groups.Count; ++i)
            {
                AddressableAssetGroup group = settings.groups[i];
                foreach (AddressableAssetEntry entry in group.entries)
                {
                    // ToLower fix from AutumnYard
                    string entry_add = entry.address.ToLower();
                    if (addressable_names.Contains(entry_add))
                    {
                        AnalyzeResult r = new AnalyzeResult();
                        r.resultName = $"{group.name}:{entry_add}";
                        r.severity = MessageType.Warning;
                        results.Add(r);
                    }
                    else
                    {
                        addressable_names.Add(entry_add);
                    }

                }
            }
            return results;
        }
        public override void FixIssues(AddressableAssetSettings settings)
        {
            HashSet<string> addressable_names = new HashSet<string>();

            for (int i = 0; i < settings.groups.Count; ++i)
            {
                AddressableAssetGroup group = settings.groups[i];
                foreach (AddressableAssetEntry entry in group.entries)
                {
                    if (addressable_names.Contains(entry.address.ToLower()))
                    {
                        // For each duplicated entry, add the extension to the address
                        string ext = System.IO.Path.GetExtension(entry.AssetPath);
                        UnityEngine.Debug.Log($"Found duplicate: {entry.AssetPath} of extension {ext}. Fixing.");
                        entry.address += ext;
                    }
                    else
                    {
                        addressable_names.Add(entry.address.ToLower());
                    }
                }
            }
        }
        public override void ClearAnalysis()
        {
            base.ClearAnalysis();
        }

        //--------------------------------
    }
}
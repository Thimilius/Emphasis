using System.Linq;

namespace Emphasis
{
    internal static class Utils
    {
        internal static bool IsClassifiedAs(string[] source, string[] search)
        {
            return source.Length > 0 && search.Length > 0 &&
            (
                from sourceClassification in source
                from searchClassification in search

                let sourceEntry = sourceClassification.ToLower()
                let searchEntry = searchClassification.ToLower()

                where(sourceEntry == searchEntry || sourceEntry.StartsWith(searchEntry + "."))

                select sourceEntry
            ).Any();
        }
    }
}

using System.Text.RegularExpressions;

namespace Pro.Api.Model.Concrete
{
    public static class TypeAheadAbbreviations
    {
        private static Dictionary<string, string> _states;

        public static Dictionary<string, string> States
        {
            get
            {
                var states = new Dictionary<string, string>
                {
                    {"montana", "MT"},
                    {"wyoming", "WY"},
                    {"wisconsin", "WI"},
                    {"west virginia", "WV"},
                    {"washington", "WA"},
                    {"virginia", "VA"},
                    {"vermont", "VT"},
                    {"utah", "UT"},
                    {"texas", "TX"},
                    {"tennessee", "TN"},
                    {"south dakota", "SD"},
                    {"south carolina", "SC"},
                    {"rhode island", "RI"},
                    {"pennsylvania", "PA"},
                    {"oregon", "OR"},
                    {"oklahoma", "OK"},
                    {"ohio", "OH"},
                    {"north dakota", "ND"},
                    {"north carolina", "NC"},
                    {"new york", "NY"},
                    {"new mexico", "NM"},
                    {"new jersey", "NJ"},
                    {"new hampshire", "NH"},
                    {"nevada", "NV"},
                    {"nebraska", "NE"},
                    {"missouri", "MO"},
                    {"mississippi", "MS"},
                    {"minnesota", "MN"},
                    {"michigan", "MI"},
                    {"massachusetts", "MA"},
                    {"maryland", "MD"},
                    {"maine", "ME"},
                    {"louisiana", "LA"},
                    {"kentucky", "KY"},
                    {"kansas", "KS"},
                    {"iowa", "IA"},
                    {"indiana", "IN"},
                    {"illinois", "IL"},
                    {"idaho", "ID"},
                    {"hawaii", "HI"},
                    {"georgia", "GA"},
                    {"florida", "FL"},
                    {"delaware", "DE"},
                    {"connecticut", "CT"},
                    {"colorado", "CO"},
                    {"california", "CA"},
                    {"arkansas", "AR"},
                    {"arizona", "AZ"},
                    {"alaska", "AK"},
                    {"alabama", "AL"}
                };
                return _states ??= states;
            }
        }

        public static string ChangeAbbreviations(string location, Dictionary<string, string> abbreviations)
        {
            var textChanged = location;
            var keywords = location.Split(' ');

            foreach (var word in keywords)
            {
                if (abbreviations.ContainsKey(word.ToLower()))
                {
                    textChanged = textChanged.Replace(word, abbreviations[word.ToLower()].ToLower());
                }
            }

            return textChanged;
        }

        public static string ReplaceStateName(string location)
        {
            var keywords = Regex.Split(location, @"\W+");
            var textChanged = location;
            if (keywords.Length < 2) return textChanged;
            var stateName =
                keywords[keywords.Length - 1]
                    .ToLower();
            if (stateName.Length <= 2) return textChanged;
            if (States.ContainsKey(stateName))
                textChanged = textChanged.Replace(oldValue: stateName, newValue: States[stateName]);
            return textChanged;
        }
    }
}

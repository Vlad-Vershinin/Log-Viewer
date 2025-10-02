
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

static class StringPartialComparer
{
    // Источник https://codtricks9.blogspot.com/2020/10/how-to-check-similarity-between-strings.html
    public static bool Compare(string a, string b)
    {
        List<string> pairs1 = WordLetterPairs(a.ToUpper());
        List<string> pairs2 = WordLetterPairs(b.ToUpper());

        int intersection = 0;
        int union = pairs1.Count + pairs2.Count;

        for (int i = 0; i < pairs1.Count; i++)
        {
            for (int j = 0; j < pairs2.Count; j++)
            {
                if (pairs1[i] == pairs2[j])
                {
                    intersection++;
                    pairs2.RemoveAt(j); //Must remove the match to prevent "AAAA" from appearing to match "AA" with 100% success
                    break;
                }
            }
        }
        return ((2.0 * intersection) / union) >= 0.7;
    }

    public static bool JObjectCompare(JObject dict, string prompt)
    {
        foreach (KeyValuePair<string, JToken> item in dict)
        {
            if (Compare(item.Key, prompt) || Compare(item.Value.ToString(), prompt)) return true;
        }
        return false;
    }

    private static List<string> WordLetterPairs(string str)
    {
        List<string> AllPairs = new List<string>();

        // Tokenize the string and put the tokens/words into an array
        string[] Words = Regex.Split(str, @"\s");

        // For each word
        for (int w = 0; w < Words.Length; w++)
        {
            if (!string.IsNullOrEmpty(Words[w]))
            {
                // Find the pairs of characters
                String[] PairsInWord = LetterPairs(Words[w]);

                for (int p = 0; p < PairsInWord.Length; p++)
                {
                    AllPairs.Add(PairsInWord[p]);
                }
            }
        }
        return AllPairs;
    }

    private static string[] LetterPairs(string str)
    {
        int numPairs = str.Length - 1;
        string[] pairs = new string[numPairs];

        for (int i = 0; i < numPairs; i++)
        {
            pairs[i] = str.Substring(i, 2);
        }
        return pairs;
    }
}
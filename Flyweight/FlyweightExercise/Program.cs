// See https://aka.ms/new-console-template for more information
using System.Text;

//var sentence = new Sentence("hello world");
var sentence = new Sentence("alpha beta gamma");
sentence[1].Capitalize = true;
Console.WriteLine(sentence); // writes "hello WORLD"



public class Sentence
{
    private string[] _sentenceArray;
    private Dictionary<int, WordToken> _wordMapCapitalize = new Dictionary<int, WordToken>();
    public Sentence(string plainText)
    {
        // todo
        _sentenceArray = plainText.Split(' ');
    }

    public WordToken this[int index]
    {
        get
        {
            // todo
            _wordMapCapitalize.Add(index, new WordToken());
            return _wordMapCapitalize[index];
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        // output formatted text here
        for (int i = 0; i < _sentenceArray.Length; i++)
        {
            if (_wordMapCapitalize.ContainsKey(i) && _wordMapCapitalize[i].Capitalize)
                _sentenceArray[i] = _sentenceArray[i].ToUpper();
        }
        sb.Append(string.Join(" ", _sentenceArray));

        return sb.ToString();
    }

    public class WordToken
    {
        public bool Capitalize;
    }
}
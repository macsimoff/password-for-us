using System.Text;
using PasswordForUs.Abstractions;

namespace PasswordForUsLibrary.PassGenerator;

public class PassGenerator:IPassGenerator
{
   private static readonly string[] DefaultCharacterSets =
    [
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
        "012345678901234567890123456789",
        "abcdefghijklmnopqrstuvwxyz",
        "!@#$%^&*_+{}[]|\\:;\"'<>?/~`()_-+="
    ];

    public string Generate(int passLength, string[]? characterSets)
    {
        var s = (characterSets?.Length > 0 ? characterSets : DefaultCharacterSets);
        var sFull = GetFullString(s);
        
        if (passLength < s.Length)
        {
            throw new ArgumentException($"Password length must be at least equal to the number of selected character categories ({s.Length}).");
        }

        var tempAlphabet = GetAlphabet(passLength, s, sFull);
        var tempSequence = tempAlphabet.Keys.Order();

        var buff = new StringBuilder(passLength);
        foreach (var n in tempSequence)
        {
            buff.Append(tempAlphabet[n]);
        }

        return buff.ToString();
    }

    private string GetFullString(string[] s)
    {
        var buff = new StringBuilder();
        foreach (var item in s)
        {
            buff.Append(item);
        }

        return buff.ToString();
    }

    private Dictionary<int, char> GetAlphabet(int passLength, string[] s, string sFull)
    {
        var rnd = new Random();
        var res = new Dictionary<int, char>();

        foreach (var sItem in s)
        {
            var k = GetRandomNumber(sItem.Length);
            res.Add(rnd.Next(), sItem[k]);
        }

        while (res.Count < passLength)
        {
            var n = GetRandomNumber(sFull.Length);
            var key = rnd.Next();
            if (!res.ContainsKey(key)) res.Add(key, sFull[n]);
        }

        return res;
    }

    private int GetRandomNumber(int maxValue)
    {
        var rnd = new Random();
        var number = rnd.Next(0,int.MaxValue); 
        number %= maxValue;
        return number;
    }
}
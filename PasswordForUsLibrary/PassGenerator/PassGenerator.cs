using System.Security.Cryptography;
using System.Text;
using PasswordForUs.Abstractions;

namespace PasswordForUsLibrary.PassGenerator;

public class PassGenerator : IPassGenerator
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
        if (passLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(passLength), "The password length must be greater than zero.");

        var sets = ValidateSets(characterSets, out var validSets) ? validSets : DefaultCharacterSets;
        
        if (passLength < sets.Length)
            throw new ArgumentException(
                $"Password length ({passLength}) must be at least equal to the number of selected character categories ({sets.Length})."
            );

        var fullSet = string.Concat(sets);

        var chars = new List<char>(passLength);
        
        chars.AddRange(sets.Select(set => set[RandomNumberGenerator.GetInt32(set.Length)]));
        for (var i = chars.Count; i < passLength; i++)
        {
            chars.Add(fullSet[RandomNumberGenerator.GetInt32(fullSet.Length)]);
        }

        return Shuffle(chars);
    }

    private bool ValidateSets(string[]? characterSets, out string[] sets)
    {
        if (characterSets == null)
        {
            sets = [];
            return false;
        }

        sets = characterSets.Where(s => !string.IsNullOrEmpty(s)).ToArray();
        return sets.Length != 0;
    }

    private string Shuffle(List<char> chars)
    {
        for (var i = chars.Count - 1; i > 0; i--)
        {
            var j = RandomNumberGenerator.GetInt32(i + 1);
            (chars[i], chars[j]) = (chars[j], chars[i]);
        }
        return new string(chars.ToArray());
    }
}
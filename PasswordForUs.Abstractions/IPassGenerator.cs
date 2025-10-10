namespace PasswordForUs.Abstractions;

public interface IPassGenerator
{
    string Generate(int passLength, string[]? characterSets);
}
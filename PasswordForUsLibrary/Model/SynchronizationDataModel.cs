namespace PasswordForUsLibrary.Model;

public class SynchronizationDataModel
{
    public string FileName { get; set; } = string.Empty;
    //todo: эти данные нужны только для шифрованных хранилищь
    //todo: вынести их от сюда если появятся синхранизация без шифрования (что врятли)
    public byte[] Key { get; set; } = [];
    public byte[] IV { get; set; } = [];
}
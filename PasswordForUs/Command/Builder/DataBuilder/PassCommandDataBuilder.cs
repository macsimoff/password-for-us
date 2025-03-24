using PasswordForUs.Const;
using PasswordForUs.Model;

namespace PasswordForUs.Command.Builder.DataBuilder;

public class PassCommandDataBuilder
{
    public PassCommandData CreateCommandData(string[] commandData)
    {
        var data = new PassCommandData();
        var enumerator = commandData.GetEnumerator();
        var oldKeyString = string.Empty;
        while (enumerator.MoveNext())
        {
            var s = (string)enumerator.Current;
            if (PassCommandConst.KeyCode.TryGetValue(s, out var key))
            {
                if (enumerator.MoveNext())
                    SetValue(data,key, (string)enumerator.Current);
                else return data;

                oldKeyString = s;
            }
            else if (s.StartsWith('-') || s.StartsWith("--"))
            {
                if (enumerator.MoveNext())
                    data.SetDataValue(s.TrimStart('-'), (string)enumerator.Current);
                else return data;
                
                oldKeyString = s;
            }
            else
            {
                if (PassCommandConst.KeyCode.TryGetValue(oldKeyString, out var oldKey))
                {
                    SetValue(data, oldKey, $"{GetValue(data,oldKey)} {s}");
                }
                else
                {
                    var dataKeyString = oldKeyString.TrimStart('-');
                    data.SetDataValue(dataKeyString, $"{data.GetDataValue(dataKeyString)} {s}");
                }
            }
        }

        return data;
    }
    
    private void SetValue(PassCommandData data, byte key, string? s)
    {
        switch (key)
        {
            case PassCommandConst.IdCode:
                if (s != null) data.Id = int.Parse(s);
                else throw new ArgumentException("Invalid id.");
                break;
            case PassCommandConst.UserCode:
                data.User = s;
                break;
            case PassCommandConst.UrlCode:
                data.Url = s;
                break;
            case PassCommandConst.NameCode:
                data.Title = s;
                break;
            case PassCommandConst.PassCode:
                data.Pass = s;
                break;
            case PassCommandConst.LoginCode:
                data.Login = s;
                break;
        }
    }
    
    private string? GetValue(PassCommandData data, byte key)
    {
        return key switch
        {
            PassCommandConst.UserCode => data.User,
            PassCommandConst.UrlCode => data.Url,
            PassCommandConst.NameCode => data.Title,
            PassCommandConst.PassCode => data.Pass,
            PassCommandConst.LoginCode => data.Login,
            _ => string.Empty
        };
    }
}
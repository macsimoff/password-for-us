using PasswordForUs.Const;
using PasswordForUs.Model;

namespace PasswordForUs.Command.Builder.DataBuilder;

public class FindCommandDataBuilder
{
    public FindCommandData Build(string[] commandData)
    {
        return CreateFindCommandData(commandData);
    }

    private FindCommandData CreateFindCommandData(string[] commandData)
    {
        var findCommandData = new FindCommandData();

        for (int i = 0; i < commandData.Length; i++)
        {
            var s = commandData[i];
            if(FindKeyConst.KeyCode.TryGetValue(s, out var code))
                switch (code)
                {
                    case FindKeyConst.IdCode:
                        if (i + 1 < commandData.Length && int.TryParse(commandData[++i], out int id))
                        {
                            findCommandData.Id = id;
                        }
                        else
                        {
                            throw new ArgumentException("The id is not correct.");
                        }

                        break;
                    case FindKeyConst.UrlCode:
                        if (i + 1 < commandData.Length)
                        {
                            findCommandData.UrlText = commandData[++i];
                        }
                        else
                        {
                            throw new ArgumentException("The url is not correct.");
                        }

                        break;
                    case FindKeyConst.NameCode:
                        if (i + 1 < commandData.Length)
                        {
                            findCommandData.NameText = commandData[++i];
                        }
                        else
                        {
                            throw new ArgumentException("The name is not correct.");
                        }

                        break;
                }
            else if(findCommandData.Id == null 
                    && string.IsNullOrEmpty(findCommandData.NameText)
                    && string.IsNullOrEmpty(findCommandData.UrlText))
            {
                findCommandData = new FindCommandData(s);
            }
            else
            {
                throw new ArgumentException("The command is not correct.");
            }
        }

        return findCommandData;
    }
}
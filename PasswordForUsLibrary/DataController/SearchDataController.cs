﻿using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;

namespace PasswordForUsLibrary.DataController;

public class SearchDataController(IRepository repository) : ISearchDataController
{
    public IEnumerable<NodeData> Search(SearchDataModel searchModel)
    {
        return repository.FindNode(searchModel);
    }
}
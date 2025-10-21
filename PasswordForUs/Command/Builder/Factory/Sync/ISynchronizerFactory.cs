using PasswordForUs.Abstractions;
using PasswordForUsLibrary.DataSynchronizer;

namespace PasswordForUs.Command.Builder.Factory.Sync;

public interface ISynchronizerFactory
{
    Synchronizer Create(IRepository repo);
}
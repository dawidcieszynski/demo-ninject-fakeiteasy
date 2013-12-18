namespace Ninject_FakeItEasyDemo.Infrastructure
{
    public interface IStorage
    {
        void Set(string name, string value);
        string Get(string name);
    }
}

namespace Autenticador.QuartzProvider.Domain
{
    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
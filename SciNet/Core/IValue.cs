namespace SciNet.Core
{
    public interface IValue
    {
        public string ToJson(bool pretty = false);
        public string ToInline();
    }
}
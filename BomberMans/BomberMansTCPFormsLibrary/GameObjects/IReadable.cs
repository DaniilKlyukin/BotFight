namespace BomberMansTCPFormsLibrary.GameObjects
{
    public interface IReadable<T>
    {
        public abstract static T Read(string s);
    }
}

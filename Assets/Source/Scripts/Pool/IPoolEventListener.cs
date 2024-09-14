
namespace BugStrategy.Pool
{
    public interface IPoolEventListener
    {
        public void OnElementReturn();
        public void OnElementExtract();
    }
}

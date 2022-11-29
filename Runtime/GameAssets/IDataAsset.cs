namespace Dre0Dru.GameAssets
{
    public interface IDataAsset<out TData>
    {
        TData Data { get; }
    }
}

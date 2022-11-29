namespace Dre0Dru.GameAssets
{
    public interface IGameAsset
    {
        //TODO guid? name? name -> guid -> asset?
        //TODO multiple keys?
        public string Guid { get; }
    }

    //TODO guid? name? name -> guid -> asset?
    //TODO multiple keys?
    public interface IGameAsset<TId>
    {
        public string Id { get; }
    }
}

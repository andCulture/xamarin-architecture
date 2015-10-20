namespace Core.Domain.Interfaces.Application {
    public interface IEntity<T> : IEntity where T : IEntity<T> { }

    public interface IEntity {
        bool IsNew();
    }
}

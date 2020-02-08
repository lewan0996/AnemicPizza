namespace AnemicPizza.Core
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}

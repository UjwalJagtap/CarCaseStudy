namespace MVCCoreApp.Repositories
{
    public interface IRepository<T>  where T : class
    {
        bool Add (T entity);

        bool Update (T entity);
        bool Delete (T entity);
        List<T> GetAll ();
        T Search(object id);
    }
}

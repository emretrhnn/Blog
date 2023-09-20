using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Records.Bases;
using DataAccess.Context;

namespace DataAccess;

public class Repo<TEntity> : RepoBase<TEntity> where TEntity : RecordBase, new()
{
    public Repo(DContext dbContext) : base(dbContext)
    {
    }
}
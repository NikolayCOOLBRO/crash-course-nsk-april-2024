namespace Market.DAL;

internal record DbResult(DbResultStatus Status)
{
    public static DbResult Of(DbResultStatus dbResultStatus)
    {
        return new DbResult(dbResultStatus);
    }

    public static DbResult Ok()
    {
        return new DbResult(DbResultStatus.Ok);
    }

    public static DbResult NotFound()
    {
        return new DbResult(DbResultStatus.NotFound);
    }
}

internal record DbResult<T>(T Result, DbResultStatus Status)
{
    public static DbResult<T> Of(T result, DbResultStatus status)
    {
        return new DbResult<T>(result, status);
    }

    public static DbResult<T> Ok(T result)
    {
        return new DbResult<T>(result, DbResultStatus.Ok);
    }

    public static DbResult<T> NotFound()
    {
        return new DbResult<T>(default!, DbResultStatus.Ok);
    }
}
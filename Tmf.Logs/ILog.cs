namespace Tmf.Logs;

public interface ILog
{
    Task<Guid> AddLogs(LogModel logModel);
    Task UpdateLogs(LogModel logModel);
}

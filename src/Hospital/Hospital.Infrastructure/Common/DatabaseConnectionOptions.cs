namespace Hospital.Infrastructure.Common
{
    public sealed class DatabaseConnectionOptions
    {
        public required string HostName { get; init; }
        public required string DatabaseName { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }

        public string CreateConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}
using System.Data;
using Dapper;

public class UserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _db.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Username = @Username",
            new { Username = username });
    }

    public async Task<User> CreateAsync(string username, string passwordHash)
    {
        var sql = """
            INSERT INTO Users (Username, PasswordHash, CreatedAt)
            OUTPUT INSERTED.*
            VALUES (@Username, @PasswordHash, GETUTCDATE())
            """;

        return await _db.QuerySingleAsync<User>(sql, new
        {
            Username = username,
            PasswordHash = passwordHash
        });
    }
}

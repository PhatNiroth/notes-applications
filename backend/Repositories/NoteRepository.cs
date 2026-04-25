using System.Data;
using Dapper;

public class NoteRepository
{
    private readonly IDbConnection _db;

    public NoteRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Note>> GetAllAsync(int userId)
    {
        return await _db.QueryAsync<Note>(
            "SELECT * FROM Notes WHERE UserId = @UserId ORDER BY CreatedAt DESC",
            new { UserId = userId });
    }

    public async Task<Note?> GetByIdAsync(int id, int userId)
    {
        return await _db.QueryFirstOrDefaultAsync<Note>(
            "SELECT * FROM Notes WHERE Id = @Id AND UserId = @UserId",
            new { Id = id, UserId = userId });
    }

    public async Task<Note> CreateAsync(int userId, CreateNote dto)
    {
        var sql = """
            INSERT INTO Notes (UserId, Title, Content, CreatedAt, UpdatedAt)
            OUTPUT INSERTED.*
            VALUES (@UserId, @Title, @Content, GETUTCDATE(), GETUTCDATE())
            """;

        return await _db.QuerySingleAsync<Note>(sql, new
        {
            UserId = userId,
            dto.Title,
            dto.Content
        });
    }

    public async Task<Note?> UpdateAsync(int id, int userId, UpdateNote dto)
    {
        var sql = """
            UPDATE Notes
            SET Title = @Title, Content = @Content, UpdatedAt = GETUTCDATE()
            OUTPUT INSERTED.*
            WHERE Id = @Id AND UserId = @UserId
            """;

        return await _db.QueryFirstOrDefaultAsync<Note>(sql, new
        {
            Id = id,
            UserId = userId,
            dto.Title,
            dto.Content
        });
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var rows = await _db.ExecuteAsync(
            "DELETE FROM Notes WHERE Id = @Id AND UserId = @UserId",
            new { Id = id, UserId = userId });

        return rows > 0;
    }
}

namespace CandidateManagement.Repositories;

using CandidateManagement.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using CandidateManagement.Infrastructure;

public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
{
    private readonly Context _context;

    public CandidateRepository(string connectionString)
    {
        _context = connectionString;
    }

    public async Task<Candidate> AddCandidateAsync(Candidate candidate)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "INSERT INTO Candidates (Forename, Surname, Email, DateOfBirth, SwqrNumber) OUTPUT INSERTED.* VALUES (@Forename, @Surname, @Email, @DateOfBirth, @SwqrNumber)";
        return await connection.QuerySingleAsync<Candidate>(query, candidate);
    }

    public async Task<Candidate?> GetCandidateByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "SELECT * FROM dbo.Candidate WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Candidate>(query, new { Id = id });
    }

    public async Task<IEnumerable<Candidate>> GetCandidatesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "SELECT * FROM dbo.Candidate";
        return await connection.QueryAsync<Candidate>(query);
    }

    public async Task<Candidate> UpdateCandidateAsync(Candidate candidate)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "UPDATE dbo.Candidate SET Forename = @Forename, Surname = @Surname, Email = @Email, DateOfBirth = @DateOfBirth, SwqrNumber = @SwqrNumber OUTPUT INSERTED.* WHERE Id = @Id";
        return await connection.QuerySingleAsync<Candidate>(query, candidate);
    }

    public async Task<Candidate> DeleteCandidateAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "DELETE FROM dbo.Candidate OUTPUT DELETED.* WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Candidate>(query, new { Id = id });
    }

    public async Task<Candidate?> GetCandidateByEmailAsync(string email)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "SELECT * FROM dbo.Candidate WHERE Email = @Email";
        return await connection.QueryFirstOrDefaultAsync<Candidate>(query, new { Email = email });
    }
}


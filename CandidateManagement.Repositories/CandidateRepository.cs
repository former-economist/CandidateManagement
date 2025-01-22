namespace CandidateManagement.Repositories;

using CandidateManagement.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CandidateRepository : ICandidateRepository
{
    private readonly string _connectionString;

    public CandidateRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Candidate> AddCandidateAsync(Candidate candidate)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "INSERT INTO Candidates (Forename, Surname, Email, DateOfBirth, SwqrNumber) OUTPUT INSERTED.* VALUES (@Forename, @Surname, @Email, @DateOfBirth, @SwqrNumber)";
        return await connection.QuerySingleAsync<Candidate>(query, candidate);
    }

    public async Task<Candidate?> GetCandidateByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "SELECT * FROM Candidates WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Candidate>(query, new { Id = id });
    }

    public async Task<IEnumerable<Candidate>> GetCandidatesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "SELECT * FROM Candidates";
        return await connection.QueryAsync<Candidate>(query);
    }

    public async Task<Candidate> UpdateCandidateAsync(Candidate candidate)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "UPDATE Candidates SET Forename = @Forename, Surname = @Surname, Email = @Email, DateOfBirth = @DateOfBirth, SwqrNumber = @SwqrNumber OUTPUT INSERTED.* WHERE Id = @Id";
        return await connection.QuerySingleAsync<Candidate>(query, candidate);
    }

    public async Task<Candidate> DeleteCandidateAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = "DELETE FROM Candidates OUTPUT DELETED.* WHERE Id = @Id";
        return await connection.QuerySingleAsync<Candidate>(query, new { Id = id });
    }

}


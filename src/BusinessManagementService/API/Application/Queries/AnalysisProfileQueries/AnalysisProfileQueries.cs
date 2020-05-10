using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Npgsql;
using System.Data;
using BusinessManagementService.Infrastructure;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementService.API.Application.Queries.AnalysisProfileQueries
{
    public class AnalysisProfileQueries : IAnalysisProfileQueries
    {
        // private string _connectionString = string.Empty;
        // public AnalysisProfileQueries(string connectionString)
        // {
        //     _connectionString = !string.IsNullOrWhiteSpace(connectionString) ?
        //      connectionString : 
        //      throw new ArgumentNullException(nameof(connectionString));
        // }

        private readonly BusinessManagementContext _context;
        private readonly IMapper _mapper;

        public AnalysisProfileQueries(BusinessManagementContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<AnalysisProfileViewModel>> GetAllAsync()
        {
            return await _context.AnalysisProfiles
                .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AnalysisProfileViewModel> GetById(Guid id)
        {
            return await _context.AnalysisProfiles
                .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // public async Task<IEnumerable<AnalysisProfile>> GetAllAsync()
        // {
        //     using (var connection = new NpgsqlConnection(_connectionString))
        //     {
        //         connection.Open();

        //         return await connection.QueryAsync<AnalysisProfile>(
        //             @"SELECT a.*
        //             FROM AnalysisProfiles a
        //             LEFT JOIN AnalysisProfileRequiredFeatures aprf
        //             ON a.Id = aprf.AnalysisProfileID"
        //         );
        //     }
        // }

        // public async Task<AnalysisProfileViewModel> GetById(Guid id)
        // {
        //     using (var connection = new NpgsqlConnection(_connectionString))
        //     {
        //         connection.Open();

        //         var result = await connection.QueryAsync<dynamic>(
        //         @"SELECT a.* FROM AnalysisProfiles a
        //         WHERE a.Id = @apid

        //         SELECT * FROM AnalysisProfileRequiredFeatures aprf
        //         ON a.Id = aprf.AnalysisProfileID
        //         ",
        //         new { apid = id});


        //         return null;
        //     }
        // }
    }
}
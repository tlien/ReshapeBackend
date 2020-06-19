using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Reshape.BusinessManagementService.Infrastructure;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;

namespace Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileExtrasQueries
{
    public class AnalysisProfileExtrasQueries : IAnalysisProfileExtrasQueries
    {
        private readonly BusinessManagementContext _context;
        private readonly IMapper _mapper;

        public AnalysisProfileExtrasQueries(BusinessManagementContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ScriptFileViewModel>> GetScriptFiles()
        {
            return await _context.ScriptFiles
                .AsNoTracking()
                .ProjectTo<ScriptFileViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ScriptParametersFileViewModel>> GetScriptParametersFiles()
        {
            return await _context.ScriptParametersFiles
                .AsNoTracking()
                .ProjectTo<ScriptParametersFileViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ScriptFileViewModel> GetScriptFileById(Guid id)
        {
            return await _context.ScriptFiles
                .AsNoTracking()
                .ProjectTo<ScriptFileViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ScriptParametersFileViewModel> GetScriptParametersFileById(Guid id)
        {
            return await _context.ScriptParametersFiles
                .AsNoTracking()
                .ProjectTo<ScriptParametersFileViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
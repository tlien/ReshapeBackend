using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.Common.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Reshape.BusinessManagementService.Infrastructure.Repositories {
    public class AnalysisProfileRepository : IAnalysisProfileRepository
    {
        private readonly BusinessManagementContext _context;
        public IUnitOfWork UnitOfWork 
        {
            get 
            { 
                return _context; 
            } 
        }

        public AnalysisProfileRepository(BusinessManagementContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public AnalysisProfile Add(AnalysisProfile analysisProfile)
        {
            return _context.Add(analysisProfile).Entity;
        }

        public async Task<AnalysisProfile> GetAsync(Guid id)
        {
            var analysisProfile = 
            await _context.AnalysisProfiles
                    .Include(a => a.MediaType)
                    .Include(a => a.ScriptFile)
                    .Include(a => a.ScriptParametersFile)
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (analysisProfile == null)
            {
                analysisProfile = _context.AnalysisProfiles.Local.FirstOrDefault(a => a.Id == id);
            }

            return analysisProfile;
        }

        public async Task<List<AnalysisProfile>> GetAllAsync()
        {
            return await _context.AnalysisProfiles
                            .Include(a => a.MediaType)
                            .Include(a => a.ScriptFile)
                            .Include(a => a.ScriptParametersFile)
                            .ToListAsync();
        }

        public async void Remove(Guid id)
        {
            var analysisProfile = await _context.AnalysisProfiles.FirstOrDefaultAsync(a => a.Id == id);
            // _context.AnalysisProfiles.Remove(analysisProfile);
            _context.Entry(analysisProfile).State = EntityState.Deleted;

            // @TODO: Launch domain event from command handler? AnalysisProfiles are not likely to be removed once pushed to clients.
        }

        public void Update(AnalysisProfile analysisProfile)
        {
            _context.Entry(analysisProfile).State = EntityState.Modified;

            // @TODO: Launch domain event from command handler? Useful if updating file contents to fix bugs or improve the analysis script.
        }

        public MediaType AddMediaType(MediaType mediaType)
        {
            var existing = _context.MediaTypes.FirstOrDefault(m => m.Id == mediaType.Id || m.Name == mediaType.Name);

            if(existing != null)
                return existing;

            return _context.Add(mediaType).Entity;
        }

        public ScriptFile AddScriptFile(ScriptFile scriptFile)
        {
            var existing = _context.ScriptFiles.FirstOrDefault(s => s.Id == scriptFile.Id);

            if(existing != null)
                return existing;

            return _context.Add(scriptFile).Entity;
        }

        public ScriptParametersFile AddScriptParametersFile(ScriptParametersFile scriptParametersFile)
        {
            var existing = _context.ScriptParametersFiles.FirstOrDefault(s => s.Id == scriptParametersFile.Id);

            if (existing != null)
                return existing;

            return _context.Add(scriptParametersFile).Entity;
        }
    }
}
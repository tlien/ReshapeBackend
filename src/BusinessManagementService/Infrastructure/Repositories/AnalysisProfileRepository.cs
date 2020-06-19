using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Reshape.Common.SeedWork;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.Infrastructure.Repositories
{
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

        public MediaType AddMediaType(MediaType mediaType)
        {
            var existing = _context.MediaTypes.FirstOrDefault(m => m.Id == mediaType.Id || m.Name == mediaType.Name);

            if (existing != null)
                return existing;

            return _context.Add(mediaType).Entity;
        }

        public ScriptFile AddScriptFile(ScriptFile scriptFile)
        {
            var existing = _context.ScriptFiles.FirstOrDefault(s => s.Id == scriptFile.Id);

            if (existing != null)
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

        public async Task<MediaType> GetMediaTypeAsync(Guid id)
        {
            var mediaType =
            await _context.MediaTypes
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (mediaType == null)
            {
                mediaType = _context.MediaTypes.Local.FirstOrDefault(a => a.Id == id);
            }

            return mediaType;
        }

        public async Task<ScriptFile> GetScriptFileAsync(Guid id)
        {
            var scriptFile =
            await _context.ScriptFiles
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (scriptFile == null)
            {
                scriptFile = _context.ScriptFiles.Local.FirstOrDefault(a => a.Id == id);
            }

            return scriptFile;
        }

        public async Task<ScriptParametersFile> GetScriptParametersFileAsync(Guid id)
        {
            var scriptParametersFile =
            await _context.ScriptParametersFiles
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (scriptParametersFile == null)
            {
                scriptParametersFile = _context.ScriptParametersFiles.Local.FirstOrDefault(a => a.Id == id);
            }

            return scriptParametersFile;
        }

        public void Update(AnalysisProfile analysisProfile)
        {
            _context.Entry(analysisProfile).State = EntityState.Modified;
        }

        public void UpdateMediaType(MediaType mediaType)
        {
            _context.Entry(mediaType).State = EntityState.Modified;
        }

        public void UpdateScriptFile(ScriptFile scriptFile)
        {
            _context.Entry(scriptFile).State = EntityState.Modified;
        }

        public void UpdateScriptParametersFile(ScriptParametersFile scriptParametersFile)
        {
            _context.Entry(scriptParametersFile).State = EntityState.Modified;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Reshape.Common.SeedWork;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace Reshape.BusinessManagementService.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for the <c>AnalysisProfile</c> domain aggregate.
    /// Implements <c>IAnalysisProfileRepository</c>
    /// </summary>
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

        /// <summary>
        /// Adds an <c>AnalysisProfile</c> to the <c>BusinessManagementContext</c> in-memory
        /// </summary>
        /// <param name="analysisProfile"></param>
        /// <returns>The added <c>AnalysisProfile</c> entity</returns>
        public AnalysisProfile Add(AnalysisProfile analysisProfile)
        {
            return _context.Add(analysisProfile).Entity;
        }

        /// <summary>
        /// Adds a <c>MediaType</c> to the <c>BusinessManagementContext</c> in-memory,
        /// unless a <c>MediaType</c> with the same name already exists in the database.
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns>The added or existing <c>MediaType</c> object</returns>
        public MediaType AddMediaType(MediaType mediaType)
        {
            var existing = _context.MediaTypes.FirstOrDefault(m => m.Id == mediaType.Id || m.Name == mediaType.Name);

            if (existing != null)
                return existing;

            return _context.Add(mediaType).Entity;
        }

        /// <summary>
        /// Adds a <c>ScriptFile</c> to the <c>BusinessManagementContext</c> in-memory,
        /// unless the <c>ScriptFile</c> already exists in the database.
        /// </summary>
        /// <param name="scriptFile"></param>
        /// <returns>The added or existing <c>ScriptFile</c> object</returns>
        public ScriptFile AddScriptFile(ScriptFile scriptFile)
        {
            var existing = _context.ScriptFiles.FirstOrDefault(s => s.Id == scriptFile.Id);

            if (existing != null)
                return existing;

            return _context.Add(scriptFile).Entity;
        }

        /// <summary>
        /// Adds a <c>ScriptParametersFile</c> to the <c>BusinessManagementContext</c> in-memory,
        /// unless the <c>ScriptParametersFile</c> already exists in the database.
        /// </summary>
        /// <param name="scriptParametersFile"></param>
        /// <returns>The added or existing <c>ScriptParametersFile</c> object</returns>
        public ScriptParametersFile AddScriptParametersFile(ScriptParametersFile scriptParametersFile)
        {
            var existing = _context.ScriptParametersFiles.FirstOrDefault(s => s.Id == scriptParametersFile.Id);

            if (existing != null)
                return existing;

            return _context.Add(scriptParametersFile).Entity;
        }

        /// <summary>
        /// Gets a single <c>AnalysisProfile</c> by id.
        /// If the <c>AnalysisProfile</c> does not exist in the database,
        /// an additional in-memory look-up will take place.
        /// </summary>
        /// <param name="id">The <c>AnalysisProfile.Id</c></param>
        /// <returns>A Task that returns the retrieved <c>AnalysisProfile</c> when awaited.</returns>
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

        /// <summary>
        /// Gets all <c>AnalysisProfiles</c>.
        /// </summary>
        /// <returns>A Task that returns a list of <c>AnalysisProfiles</c> when awaited.</returns>
        public async Task<List<AnalysisProfile>> GetAllAsync()
        {
            return await _context.AnalysisProfiles
                            .Include(a => a.MediaType)
                            .Include(a => a.ScriptFile)
                            .Include(a => a.ScriptParametersFile)
                            .ToListAsync();
        }

        /// <summary>
        /// Gets a single <c>MediaType</c> by id.
        /// If the <c>MediaType</c> does not exist in the database,
        /// an additional in-memory look-up will take place.
        /// </summary>
        /// <param name="id">The <c>MediaType.Id</c></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a single <c>ScriptFile</c> by id.
        /// If the <c>ScriptFile</c> does not exist in the database,
        /// an additional in-memory look-up will take place.
        /// </summary>
        /// <param name="id">The <c>ScriptFile.Id</c></param>
        /// <returns>A Task that returns the retrieved <c>ScriptFile</c> when awaited.</returns>
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

        /// <summary>
        /// Gets a single <c>ScriptParametersFile</c> by id.
        /// If the <c>ScriptParametersFile</c> does not exist in the database,
        /// an additional in-memory look-up will take place.
        /// </summary>
        /// <param name="id">The <c>ScriptParametersFile.Id</c></param>
        /// <returns>A Task that returns the retrieved <c>ScriptParametersFile</c> when awaited.</returns>
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

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of an <c>AnalysisProfile</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>AnalysisProfile</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="analysisProfile"></param>
        public void Update(AnalysisProfile analysisProfile)
        {
            _context.Entry(analysisProfile).State = EntityState.Modified;
        }

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of a <c>MediaType</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>MediaType</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="mediaType"></param>
        public void UpdateMediaType(MediaType mediaType)
        {
            _context.Entry(mediaType).State = EntityState.Modified;
        }

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of a <c>ScriptFile</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>ScriptFile</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="scriptFile"></param>
        public void UpdateScriptFile(ScriptFile scriptFile)
        {
            _context.Entry(scriptFile).State = EntityState.Modified;
        }

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of a <c>ScriptParametersFile</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>ScriptParametersFile</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="scriptParametersFile"></param>
        public void UpdateScriptParametersFile(ScriptParametersFile scriptParametersFile)
        {
            _context.Entry(scriptParametersFile).State = EntityState.Modified;
        }
    }
}
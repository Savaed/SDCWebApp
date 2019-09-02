﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Provides methods for GET, ADD, UPDATE and DELETE operations for <see cref="GeneralSightseeingInfo"/> entities in the database.
    /// </summary>
    public class GeneralSightseeingInfoDbService : ServiceBase, IGeneralSightseeingInfoDbService
    {
        private readonly ILogger<GeneralSightseeingInfoDbService> _logger;
        private readonly ApplicationDbContext _context;


        public GeneralSightseeingInfoDbService(ApplicationDbContext context, ILogger<GeneralSightseeingInfoDbService> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }


        /// <summary>
        /// Asynchronously adds <see cref="GeneralSightseeingInfo"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="info">The info to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="info"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="GeneralSightseeingInfo"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<GeneralSightseeingInfo> AddAsync(GeneralSightseeingInfo info)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");

            if (info is null)
                throw new ArgumentNullException($"Argument '{nameof(info)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.GeneralSightseeingInfo ?? throw new InternalDbServiceException($"Table of type '{typeof(GeneralSightseeingInfo).Name}' is null.");

            try
            {
                if (_context.GeneralSightseeingInfo.Contains(info))
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{info.Id}'.");

                _logger.LogDebug($"Starting add general sightseeing info with id '{info.Id}'.");
                var addedInfo = _context.GeneralSightseeingInfo.Add(info).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddAsync)}'.");
                return addedInfo;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} Changes made by add operations cannot be saved properly. See the inner exception. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See the inner exception", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} There is already the same element in the database as the one to be added. Id of this element: '{info.Id}'.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding sightseeing info with id '{info?.Id}' to the database. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously deletes <see cref="GeneralSightseeingInfo"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="GeneralSightseeingInfo"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.GeneralSightseeingInfo ?? throw new InternalDbServiceException($"Table of type '{typeof(GeneralSightseeingInfo).Name}' is null.");

            try
            {
                if (_context.GeneralSightseeingInfo.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_context.GeneralSightseeingInfo.GetType().Name} does not contain any element.");

                if (await _context.GeneralSightseeingInfo.AnyAsync(x => x.Id.Equals(id)) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");

                var infoToBeDeleted = await _context.GeneralSightseeingInfo.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove sightseeing info with id '{infoToBeDeleted.Id}'.");
                _context.GeneralSightseeingInfo.Remove(infoToBeDeleted);
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Remove data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(DeleteAsync)}'.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element. See exception Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when removing sightseeing with id '{id}' from database. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs all <see cref="GeneralSightseeingInfo"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="GeneralSightseeingInfo"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<GeneralSightseeingInfo>> GetAllAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.GeneralSightseeingInfo ?? throw new InternalDbServiceException($"Table of type '{typeof(GeneralSightseeingInfo).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve all sightseeing info from database.");
                var info = await _context.GeneralSightseeingInfo.ToListAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning {info.Count} elements.");
                return info.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving all sightseeing info from database. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs <see cref="GeneralSightseeingInfo"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<GeneralSightseeingInfo> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.GeneralSightseeingInfo ?? throw new InternalDbServiceException($"Table of type '{typeof(GeneralSightseeingInfo).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve sightseeing info with id: '{id}' from database.");
                var info = await _context.GeneralSightseeingInfo.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return info;
            }
            catch (InvalidOperationException ex)
            {
                string message = _context.GeneralSightseeingInfo.Count() == 0 ? $"Element not found because resource {_context.GeneralSightseeingInfo.GetType().Name} does contain any elements. See the inner exception"
                    : "Element not found. See the inner exception";
                _logger.LogError(ex, $"{ex.GetType().Name} {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving sightseeing info with id '{id}' from database. See the inner exception", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="GeneralSightseeingInfo"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="GeneralSightseeingInfo"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<GeneralSightseeingInfo>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equal to 1 are valid.");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equal to 0 are valid.");

            // TODO Create only for unit tests purposes. In debug and later should be Migrate()!!!
            await EnsureDatabaseCreatedAsync();
            _ = _context?.GeneralSightseeingInfo ?? throw new InternalDbServiceException($"Table of type '{typeof(GeneralSightseeingInfo).Name}' is null.");

            try
            {
                IEnumerable<GeneralSightseeingInfo> info = new GeneralSightseeingInfo[] { }.AsEnumerable();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _context.GeneralSightseeingInfo.CountAsync();
                int numberOfElementsOnLastPage = numberOfResourceElements % pageSize;
                int numberOfFullPages = (numberOfResourceElements - numberOfElementsOnLastPage) / pageSize;

                if (numberOfElementsOnLastPage > 0)
                {
                    maxNumberOfPageWithData = ++numberOfFullPages;
                    _logger.LogWarning($"Last page of data contain {numberOfElementsOnLastPage} elements which is less than specified in {nameof(pageSize)}: {pageSize}.");
                }
                else
                    maxNumberOfPageWithData = numberOfFullPages;

                if (numberOfResourceElements == 0 || pageSize == 0 || pageNumber > maxNumberOfPageWithData)
                {
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {info.Count()} elements.");
                    return info;
                }

                _logger.LogDebug($"Starting retrieve data. {nameof(pageNumber)} '{pageNumber.ToString()}', {nameof(pageSize)} '{pageSize.ToString()}'.");
                info = _context.GeneralSightseeingInfo.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return info;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving sightseeing info from database. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="GeneralSightseeingInfo"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="info">The sightseeing info to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="info"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="info"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<GeneralSightseeingInfo> UpdateAsync(GeneralSightseeingInfo info)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");

            _ = info ?? throw new ArgumentNullException(nameof(info), $"Argument '{nameof(info)}' cannot be null.");

            if (string.IsNullOrEmpty(info.Id))
                throw new ArgumentException($"Argument '{nameof(info.Id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.GeneralSightseeingInfo ?? throw new InternalDbServiceException($"Table of type '{typeof(GeneralSightseeingInfo).Name}' is null.");

            try
            {
                // If _context.GeneralSightseeingInfo does not null, but does not exist (as table in database, not as object using by EF Core)
                // following if statement (exactly Count method) will throw exception about this table ("no such table: 'GeneralSightseeingInfo'." or something like that).
                // So you can catch this exception and re-throw in InternalDbServiceException to next handling in next level layer e.g Controller.

                // Maybe throwing exception in try block seems to be bad practice and a little bit tricky, but in this case is neccessery.
                // Refference to Groups while it does not exist cause throwing exception and without this 2 conditions below you cannot check 
                // is there any element for update in database.
                if (_context.GeneralSightseeingInfo.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{info.Id}' for update. Resource {_context.GeneralSightseeingInfo.GetType().Name} does not contain any element.");

                if (await _context.GeneralSightseeingInfo.ContainsAsync(info) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{info.Id}' for update. Any element does not match to the one to be updated.");

                _logger.LogDebug($"Starting update singhtseeing info with id '{info.Id}'.");
                info.UpdatedAt = DateTime.UtcNow;
                var updatedInfo = _context.GeneralSightseeingInfo.Update(info).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(UpdateAsync)}'.");
                return updatedInfo;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element for update. See exception Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating sightseeing info with id '{info.Id}'. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously adds <see cref="GeneralSightseeingInfo"/> entity to the database. Do not allow to add entity with the same properties value as existing one.
        /// Throws an exception if already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="info">The info to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="info"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="GeneralSightseeingInfo"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<GeneralSightseeingInfo> RestrictedAddAsync(GeneralSightseeingInfo info)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedAddAsync)}'.");

            if (info is null)
                throw new ArgumentNullException($"Argument '{nameof(info)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.GeneralSightseeingInfo ?? throw new InternalDbServiceException($"Table of type '{typeof(GeneralSightseeingInfo).Name}' is null.");

            try
            {
                // Check if exist in db tariff with the same 'Name' as adding.
                if (await IsEntityAlreadyExistsAsync(info))
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. The value of '{nameof(info)}' is not unique.");

                _logger.LogDebug($"Starting add info with id '{info.Id}'.");
                var addedInfo = _context.GeneralSightseeingInfo.Add(info).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddAsync)}'.");
                return addedInfo;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} Changes made by add operations cannot be saved properly. See the inner exception. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See the inner exception", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} There is already the same element in the database as the one to be added. The value of '{nameof(info)}' is not unique.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding sighseeing info with id '{info?.Id}' to the database. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="SightseeingTariff"/> entity ignoring read-only properties like Id, CreatedAt, UpdatedAt, ConcurrencyToken. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tariff"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="tariff"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<GeneralSightseeingInfo> RestrictedUpdateAsync(GeneralSightseeingInfo info)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedUpdateAsync)}'.");

            _ = info ?? throw new ArgumentNullException(nameof(info), $"Argument '{nameof(info)}' cannot be null.");

            if (string.IsNullOrEmpty(info.Id))
                throw new ArgumentException($"Argument '{nameof(info.Id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.GeneralSightseeingInfo ?? throw new InternalDbServiceException($"Table of type '{typeof(GeneralSightseeingInfo).Name}' is null.");

            try
            {
                if (_context.GeneralSightseeingInfo.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{info.Id}' for update. Resource {_context.Groups.GetType().Name} does not contain any element.");

                if (await _context.GeneralSightseeingInfo.ContainsAsync(info) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{info.Id}' for update. Any element does not match to the one to be updated.");

                _logger.LogDebug($"Starting update sightseeing info with id '{info.Id}'.");
                var originalInfo = await _context.GeneralSightseeingInfo.SingleAsync(x => x.Id.Equals(info.Id));
                var updatedInfo = BasicRestrictedUpdate(originalInfo, info) as GeneralSightseeingInfo;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(RestrictedUpdateAsync)}'.");
                return updatedInfo;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element for update. See exception Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating sighseeing info with id '{info.Id}'. See inner excpetion", ex);
                throw internalException;
            }

        }


        #region Privates

        protected override async Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            var allInfo = await _context.GeneralSightseeingInfo.ToArrayAsync();
            return allInfo.Any(x => x.Equals(entity as GeneralSightseeingInfo));
        }

     

        #endregion

    }
}

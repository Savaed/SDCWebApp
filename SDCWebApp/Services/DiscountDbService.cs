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
    // NOTE The code repeats in catch blocks, so why not wrap it in a function and call it when needed?
    // The answer is that an exception is caught in the catch block, and then a new exception with the caught exception as 
    // inner is thrown up. But if we catch the exception, then we call the function that creates and throws
    // new exception with caught one, stack persistence will not be preserved.
    // Using this approach in small projects like this one can be the triumph of form over content, but in larger projects 
    // will be very useful and helpful for future maintenance.

    /// <summary>
    /// Provides methods for GET, ADD, UPDATE and DELETE operations for <see cref="Discount"/> entities in the database.
    /// </summary>
    public class DiscountDbService : ServiceBase, IDiscountDbService
    {
        private readonly ILogger<DiscountDbService> _logger;
        private readonly ApplicationDbContext _context;


        public DiscountDbService(ApplicationDbContext context, ILogger<DiscountDbService> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }


        /// <summary>
        /// Asynchronously adds <see cref="Discount"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="discount">The discount to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of discount to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Discount"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<Discount> AddAsync(Discount discount)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");

            if (discount is null)
                throw new ArgumentNullException($"Argument '{nameof(discount)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Discounts ?? throw new InternalDbServiceException($"Table of type '{typeof(Discount).Name}' is null.");

            try
            {
                if (_context.Discounts.Contains(discount))
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{discount.Id}'.");

                _logger.LogDebug($"Starting add discount with id '{discount.Id}'.");
                var addedDiscount = _context.Discounts.Add(discount).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddAsync)}'.");
                return addedDiscount;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} Changes made by add operations cannot be saved properly. See the inner exception. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See the inner exception", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} There is already the same element in the database as the one to be added. Id of this element: '{discount.Id}'.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding disount with id '{discount?.Id}' to database. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously deletes <see cref="Discount"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Discount"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Discounts ?? throw new InternalDbServiceException($"Table of type '{typeof(Discount).Name}' is null.");

            try
            {
                if (_context.Discounts.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_context.Discounts.GetType().Name} does not contain any element.");

                if (await _context.Discounts.AnyAsync(x => x.Id.Equals(id)) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");

                var disountToBeDeleted = await _context.Discounts.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove discount with id '{disountToBeDeleted.Id}'.");
                _context.Discounts.Remove(disountToBeDeleted);
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
                var internalException = new InternalDbServiceException($"Encountered problem when removing disounts with id '{id}' from database. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs all <see cref="Discount"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="Discount"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Discounts ?? throw new InternalDbServiceException($"Table of type '{typeof(Discount).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve all discounts from database.");
                var discounts = await _context.Discounts.ToListAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning {discounts.Count} elements.");
                return discounts.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving disounts from database. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs <see cref="Discount"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<Discount> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Discounts ?? throw new InternalDbServiceException($"Table of type '{typeof(Discount).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve discount with id: '{id}' from database.");
                var discount = await _context.Discounts.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return discount;
            }
            catch (InvalidOperationException ex)
            {
                string message = _context.Discounts.Count() == 0 ? $"Element not found because resource {_context.Discounts.GetType().Name} is empty. See the inner exception"
                    : "Element not found. See the inner exception";
                _logger.LogError(ex, $"{ex.GetType().Name} {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving discount with id '{id}' from database. See the inner exception", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="Discount"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="Discount"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<Discount>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equal to 1 are valid.");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equal to 0 are valid.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Discounts ?? throw new InternalDbServiceException($"Table of type '{typeof(Discount).Name}' is null.");

            try
            {
                IEnumerable<Discount> discounts = new Discount[] { }.AsEnumerable();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _context.Discounts.CountAsync();
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
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {discounts.Count()} elements.");
                    return discounts;
                }

                _logger.LogDebug($"Starting retrieve data. {nameof(pageNumber)} '{pageNumber.ToString()}', {nameof(pageSize)} '{pageSize.ToString()}'.");
                discounts = _context.Discounts.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return discounts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving disounts from database. See inner excpetion", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="Discount"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="discount">The discount to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="discount"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="discount"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<Discount> UpdateAsync(Discount discount)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");

            _ = discount ?? throw new ArgumentNullException(nameof(discount), $"Argument '{nameof(discount)}' cannot be null.");

            if (string.IsNullOrEmpty(discount.Id))
                throw new ArgumentException($"Argument '{nameof(discount.Id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Discounts ?? throw new InternalDbServiceException($"Table of type '{typeof(Discount).Name}' is null.");

            try
            {
                // If _context.Discounts does not null, but does not exist (as table in database, not as object using by EF Core)
                // following if statement (exacly Count method) will throw exception about this table ("no such table: 'Discounts'." or something like that).
                // So you can catch this exception and re-throw in InternalDbServiceException to next handling in next level layer e.g Controller.

                // Maybe throwing exception in try block seems to be bad practice and a little bit tricky, but in this case is neccessery.
                // Refference to Discounts while it does not exist cause throwing exception and without this 2 conditions below you cannot check 
                // is there any element for update in database.
                if (_context.Discounts.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{discount.Id}' for update. Resource {_context.Discounts.GetType().Name} does not contain any element.");

                if (await _context.Discounts.ContainsAsync(discount) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{discount.Id}' for update. Any element does not match to the one to be updated.");

                _logger.LogDebug($"Starting update discount with id '{discount.Id}'.");
                discount.UpdatedAt = DateTime.UtcNow;
                var updatedDiscount = _context.Discounts.Update(discount).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(UpdateAsync)}'.");
                return updatedDiscount;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element for update. See exception Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating disount with id '{discount.Id}'. See inner excpetion", ex);
                throw internalException;
            }
        }

        public async Task<Discount> RestrictedUpdateAsync(Discount discount)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");

            _ = discount ?? throw new ArgumentNullException(nameof(discount), $"Argument '{nameof(discount)}' cannot be null.");

            if (string.IsNullOrEmpty(discount.Id))
                throw new ArgumentException($"Argument '{nameof(discount.Id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Discounts ?? throw new InternalDbServiceException($"Table of type '{typeof(Discount).Name}' is null.");

            try
            {
               if (_context.Discounts.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{discount.Id}' for update. Resource {_context.Discounts.GetType().Name} does not contain any element.");

                if (await _context.Discounts.ContainsAsync(discount) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{discount.Id}' for update. Any element does not match to the one to be updated.");

                _logger.LogDebug($"Starting update discount with id '{discount.Id}'.");
                discount.UpdatedAt = DateTime.UtcNow;
                var originalDiscount = await _context.Discounts.SingleAsync(x => x.Id.Equals(discount.Id));
                var updatedDiscount = BasicRestrictedUpdate(originalDiscount, discount) as Discount;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(UpdateAsync)}'.");
                return updatedDiscount;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element for update. See exception Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating disount with id '{discount.Id}'. See inner excpetion", ex);
                throw internalException;
            }
        }

        public async Task<Discount> RestrictedAddAsync(Discount discount)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");

            if (discount is null)
                throw new ArgumentNullException($"Argument '{nameof(discount)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Discounts ?? throw new InternalDbServiceException($"Table of type '{typeof(Discount).Name}' is null.");

            try
            {
                // Check if exist in db tariff with the same props as adding.
                if (await IsEntityAlreadyExistsAsync(discount))
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. " +
                        $"The value of '{nameof(discount.Description)}', '{nameof(discount.DiscountValueInPercentage)}' etc. , are not unique.");

                _logger.LogDebug($"Starting add discount with id '{discount.Id}'.");
                var addedDiscount = _context.Discounts.Add(discount).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddAsync)}'.");
                return addedDiscount;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} Changes made by add operations cannot be saved properly. See the inner exception. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See the inner exception", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} There is already the same element in the database as the one to be added. Id of this element: '{discount.Id}'.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding disount with id '{discount?.Id}' to database. See inner excpetion", ex);
                throw internalException;
            }
        }


        #region Privates      

       

        protected override async Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            var allDiscounts = await _context.Discounts.ToArrayAsync();
            return allDiscounts.Any(x => x.Equals(entity as Discount));
        }

        #endregion
    }
}

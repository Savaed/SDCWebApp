﻿using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface ITicketTariffDbService : IServiceBase
    {
        /// <summary>
        /// Filters set of data of type <see cref="TicketTariff"/>. Returns filtered data set. Throws an exception if <paramref name="predicate"/> is null, 
        /// or if cannot filter data due to any internal problem.
        /// </summary>
        /// <typeparam name="T">The type of entity to set be filtered.</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Filterd <see cref="TicketTariff"/> set.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Cannot filter data.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>        
        Task<IEnumerable<TicketTariff>> GetByAsync(Expression<Func<TicketTariff, bool>> predicate);

        /// <summary>
        /// Asynchronously retrieves <see cref="TicketTariff"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<TicketTariff> GetAsync(string id);

        /// <summary>
        /// Asynchronously retrieves all <see cref="TicketTariff"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="TicketTariff"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<IEnumerable<TicketTariff>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves <see cref="TicketTariff"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="TicketTariff"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<IEnumerable<TicketTariff>> GetWithPaginationAsync(int pageNumber = 1, int pageSize = 30);

        /// <summary>
        /// Asynchronously updates <see cref="TicketTariff"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tariff"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="tariff"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<TicketTariff> UpdateAsync(TicketTariff tariff);

        /// <summary>
        /// Asynchronously updates <see cref="TicketTariff"/> entity ignoring read-only properties like Id, CreatedAt, UpdatedAt, ConcurrencyToken. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tariff"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="tariff"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<TicketTariff> RestrictedUpdateAsync(TicketTariff tariff);

        /// <summary>
        /// Asynchronously deletes <see cref="TicketTariff"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="TicketTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        Task DeleteAsync(string id);

        /// <summary>
        /// Asynchronously adds <see cref="TicketTariff"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="tariff"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="TicketTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        Task<TicketTariff> AddAsync(TicketTariff tariff);

        /// <summary>
        /// Asynchronously adds <see cref="TicketTariff"/> entity to the database. Do not allow add entity with the same Description, IsPerHour, IsPerPerson and DefaultPrice. 
        /// Throws an exception if already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="tariff"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="TicketTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        Task<TicketTariff> RestrictedAddAsync(TicketTariff tariff);
    }
}

﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SDCWebApp.Data;
using SDCWebApp.Models;
using SDCWebApp.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests.Services
{
    [TestFixture]
    public class VisitTariffDbServiceTest
    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private ILogger<VisitTariffDbService> _logger;
        private Expression<Func<VisitTariff, bool>> _predicate;
        private readonly VisitTariff _validTariff = new VisitTariff
        {
            Id = "1",
            Name = "Sample sightseeing tariff for unit tests."
        };
        private static readonly VisitTariff[] _tariffForRestrictedUpdateCases = new VisitTariff[]
        {
            new VisitTariff{ ConcurrencyToken = Encoding.ASCII.GetBytes("Updated ConcurrencyToken") },    // Attempt to change 'ConcurrencyToken' which is read-only property.
            new VisitTariff{ UpdatedAt = DateTime.Now.AddYears(100) }                                     // Attempt to change 'UpdatedAt' which is read-only property.
        };


        [OneTimeSetUp]
        public void SetUp()
        {
            _predicate = x => x.CreatedAt > DateTime.MinValue;
            _dbContextMock = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext)));
            _logger = Mock.Of<ILogger<VisitTariffDbService>>();
        }


        #region GetByAsync(predicate)
        // tabela nie istnieje -> internal exc
        // tabela jest nullem - > internal exc
        // zasob jest pusty -> pusty ienumer
        // predicate jest null -> arg null exc
        // znalazlo -> ienum<VisitTariff> dla wszystkich znalezionych
        // zaden ele nie spelnia war -> pusty ienum

        [Test]
        public async Task GetByAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs = null as DbSet<VisitTariff>;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetByAsync(_predicate);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetByAsync__Argument_predicate_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetByAsync(null);

                    await action.Should().ThrowExactlyAsync<ArgumentNullException>();
                }
            }
        }

        [Test]
        public async Task GetByAsync__Resource_does_not_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop SightseeingTariffs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [VisitTariffs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetByAsync(_predicate);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitTariff. " +
                         "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetByAsync__Resource_is_empty__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.RemoveRange(await context.VisitTariffs.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.GetByAsync(_predicate);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetByAsync__At_least_one_sightseeing_tariffs_found__Should_return_IEnumerable_for_this_tarifs()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.VisitTariffs.ToArray().Length;
                    var service = new VisitTariffDbService(context, _logger);

                    // This predicate filters tariffs with CreatedAt after DateTime.MinValue. It will be all of tariffs.
                    var result = await service.GetByAsync(_predicate);

                    result.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task GetByAsync__None_sightseeing_tariffs_satisfy_predicate__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    // This predicate filters tariffs with CreatedAt equals DateTime.MaxValue. It will none tariffs.
                    var result = await service.GetByAsync(x => x.CreatedAt == DateTime.MaxValue);

                    result.Count().Should().Be(0);
                }
            }
        }

        #endregion


        #region GetAsync(string id)
        // nie istnieje tabela -> internal ex
        // tabela jest nullem -> internal ex
        // id jest nullem/pusty -> arg exc
        // 0 znaleziono -> invalid oper exc
        // zasob jest pusty -> invalid oper exc
        // znalazlo -> zwraca tylko jeden element
        // znalazlo  -> zwraca i sightseeing tariff zawiera ticket tariffs

        [Test]
        public async Task GetAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs = null as DbSet<VisitTariff>;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop SightseeingTariffs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [VisitTariffs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitTariff. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new VisitTariffDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetAsync(id);

            await action.Should().ThrowExactlyAsync<ArgumentException>("Because id cannot be null or empty string.");
        }

        [Test]
        public async Task GetAsync__Found_zero_matching_SightseeingTariff__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because VisitTariff not found.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.RemoveRange(await context.VisitTariffs.ToListAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot get single instance of VisitTariff.");
                }
            }
        }

        [Test]
        public async Task GetAsync__SightseeingTariff_found__Should_return_this_sightseeing_tariff()
        {
            VisitTariff expectedSightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedSightseeingTariff = await context.VisitTariffs.Include(x => x.TicketTariffs).FirstOrDefaultAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.GetAsync(expectedSightseeingTariff.Id);

                    result.Should().BeEquivalentTo(expectedSightseeingTariff);
                }
            }
        }

        [Test]
        public async Task GetAsync__SightseeingTariff_found__Should_return_this_sightseeing_tariff_with_not_null_ticket_tariffs_list()
        {
            VisitTariff expectedSightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedSightseeingTariff = await context.VisitTariffs.Include(x => x.TicketTariffs).FirstOrDefaultAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.GetAsync(expectedSightseeingTariff.Id);

                    result.TicketTariffs.Should().NotBeNull();
                }
            }
        }

        #endregion


        #region GetAllAsync()
        // tabela nie istnieje -> internal exc
        // tabela jest nullem - > internal exc
        // zasob jest pusty -> pusty ienumer
        // znalazlo -> ienum<VisitTariff> dla wszystkich z zasobu
        // znalazlo -> wszystkie zwrocome ele zawieraja ticket tariff

        [Test]
        public async Task GetAllAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs = null as DbSet<VisitTariff>;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAllAsync();

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetAllAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop SightseeingTariffs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [VisitTariffs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAllAsync();

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitTariff. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetAllAsync__Resource_is_empty__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.RemoveRange(await context.VisitTariffs.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetAllAsync__All_SightseeingTariffs_found__Should_return_IEnumerable_for_all_sightseeing_groups()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.VisitTariffs.ToArray().Length;
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    result.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task GetAllAsync__SightseeingTariffs_found__Should_return_IEnumerable_for_all_sightseeing_groups_with_not_null_ticket_tariffs_list()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.VisitTariffs.ToArray().Length;
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    foreach (var sightseeingTariff in result)
                    {
                        sightseeingTariff.TicketTariffs.Should().NotBeNull();
                    }
                }
            }
        }

        #endregion


        #region Add(SightseeingTariff sightseeingTariff)
        // zasob nie istnieje -> exc z msg
        // zasob jest nullem -> null ref exc
        // problem z zapisaniem zmian -> inter | Nie mam pojecia jak to przetestowac xD
        // arg jest nullem -> arg null exc
        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac -> invalid oper exc
        // dodawanie sie udalo -> zwraca dodany bilet
        // dodawanie sie udalo -> zasob jest wiekszy o jeden
        // dodawanie sie udalo -> istnieje w zasobie dodany bilet

        [Test]
        public async Task AddAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs = null as DbSet<VisitTariff>;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task AddAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop SightseeingTariffs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [VisitTariffs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitTariff. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            var service = new VisitTariffDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.AddAsync(null as VisitTariff);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'VisitTariff' is null.");
        }

        [Test]
        public async Task AddAsync__In_resource_exists_sightseeing_tariff_with_the_same_id_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Add(_validTariff);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    _validTariff.Name = "changed name";
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.AddAsync(_validTariff);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same VisitTariff as this one to be added.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Attempt_to_add_entity_with_the_same_properties_value_as_existing_one_but_wit_different_id__Add_should_be_successful()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Add(_validTariff);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    _validTariff.Id = null;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.AddAsync(_validTariff);

                    await result.Should().NotThrowAsync("Because this method allows to add a new entity with the same properties value but different 'Id'. It is intentional behaviour.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Should_return_added_sightseeing_tariff()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.AddAsync(_validTariff);

                    result.Should().BeEquivalentTo(_validTariff);
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Resource_length_should_be_greater_by_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.VisitTariffs.Count() + 1;
                    var service = new VisitTariffDbService(context, _logger);

                    await service.AddAsync(_validTariff);

                    context.VisitTariffs.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Resource_contains_added_sightseeing_tariff()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    await service.AddAsync(_validTariff);

                    context.VisitTariffs.Contains(_validTariff).Should().BeTrue();
                }
            }
        }

        #endregion


        #region RestrictedAddAsync(SightseeingTariff sightseeingTariff)
        // zasob nie istnieje -> exc z msg
        // zasob jest nullem -> null ref exc
        // problem z zapisaniem zmian -> inter | Nie mam pojecia jak to przetestowac xD
        // arg jest nullem -> arg null exc
        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac (takie samo id, wartosci ) -> invalid oper exc
        // dodawanie sie udalo -> zwraca dodany bilet
        // dodawanie sie udalo -> zasob jest wiekszy o jeden
        // dodawanie sie udalo -> istnieje w zasobie dodany bilet
        // proba doda

        [Test]
        public async Task RestrictedAddAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs = null as DbSet<VisitTariff>;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedAddAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop SightseeingTariffs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [VisitTariffs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedAddAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitTariff. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            var service = new VisitTariffDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.RestrictedAddAsync(null as VisitTariff);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'VisitTariff' is null.");
        }

        [Test]
        public async Task RestrictedAddAsync__In_resource_exists_sightseeing_tariff_with_the_same_id_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Add(_validTariff);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    _validTariff.Name = "changed name";
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedAddAsync(_validTariff);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same VisitTariff as this one to be added.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Attempt_to_add_entity_with_the_same_description_as_existing_one_but_wit_different_id__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Add(_validTariff);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    _validTariff.Id += "_changed_id";
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedAddAsync(_validTariff);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because this method DOES NOT allow to add a new entity with the same properties value (Description) " +
                        "but different 'Id'. It is intentional behaviour.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Add_successful__Should_return_added_sightseeing_tariff()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.RestrictedAddAsync(_validTariff);

                    result.Should().BeEquivalentTo(_validTariff);
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Add_successful__Resource_length_should_be_greater_by_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.VisitTariffs.Count() + 1;
                    var service = new VisitTariffDbService(context, _logger);

                    await service.RestrictedAddAsync(_validTariff);

                    context.VisitTariffs.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Add_successful__Resource_contains_added_sightseeing_tariff()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    await service.RestrictedAddAsync(_validTariff);

                    context.VisitTariffs.Contains(_validTariff).Should().BeTrue();
                }
            }
        }

        #endregion


        #region UpdateAsync(SightseeingTariff tariff)
        // arg jest nullem -> arg null exc
        // arg ma id ktore jest nullem albo pusty -> arg exc   
        // zasob jest nullem -> inter exc
        // zasob nie istnieje -> inter exc
        // zasob jest pusty -> invalid oper exc
        // nie znaleziono podanego biletu -> invalid oper exc
        // update udal sie -> tyle samo biletow co przed operacja 
        // update sie udal -> w zasobie istnieje zmodyfikowany bilet
        // update sie udal -> w zasobie nie istnieje poprzednia wersja biletu
        // update sie udal -> zwraca zmodyfikowany bilet

        [Test]
        public async Task UpdateAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs = null as DbSet<VisitTariff>;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.UpdateAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop SightseeingTariffs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [VisitTariffs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.UpdateAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitTariff. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(null as VisitTariff);

                    await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'VisitTariff' cannot be null.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Argument_has_null_or_empty_id__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var invalidSightseeingTariff = new VisitTariff { Id = id };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(invalidSightseeingTariff);

                    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'VisitTariff' has null or empty id which is invalid.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariffBeforUpdate = await context.VisitTariffs.FirstAsync();
                    sightseeingTariff = sightseeingTariffBeforUpdate.Clone() as VisitTariff;
                    context.VisitTariffs.RemoveRange(await context.VisitTariffs.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(sightseeingTariff);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because esource is empty.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Matching_SightseeingTariff_not_found__Should_throw_InvalidOperationException()
        {
            VisitTariff sightseeingTariff = new VisitTariff
            {
                Id = "0",
                Name = "Sample name for only this test.",
                UpdatedAt = DateTime.Now.AddHours(-3)
            };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    // In db does not matching VisitTariff to belowe disount.
                    Func<Task> result = async () => await service.UpdateAsync(sightseeingTariff);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching VisitTariff not found.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Should_return_updated_sightseeing_tariff()
        {
            VisitTariff sightseeingTariffBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariffBeforUpdate = await context.VisitTariffs.FirstAsync();
                    VisitTariff sightseeingTariff = sightseeingTariffBeforUpdate;
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.UpdateAsync(sightseeingTariff);

                    result.Should().BeEquivalentTo(sightseeingTariff);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_contains_updated_sightseeing_tariff()
        {
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff = await context.VisitTariffs.FirstAsync();
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.UpdateAsync(sightseeingTariff);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Contains(sightseeingTariff).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_sightseeing_tariff()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff = await context.VisitTariffs.FirstAsync();
                    sightseeingTariffBeforUpdate = sightseeingTariff.Clone() as VisitTariff;
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.UpdateAsync(sightseeingTariff);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Single(x => x == sightseeingTariff).Should().NotBeSameAs(sightseeingTariffBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariffBeforUpdate = await context.VisitTariffs.FirstAsync();
                    sightseeingTariff = sightseeingTariffBeforUpdate;
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);
                    expectedLength = await context.VisitTariffs.CountAsync();

                    await service.UpdateAsync(sightseeingTariff);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_first_time__Updated_element_should_have_updated_at_not_set_to_MinValue()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff = await context.VisitTariffs.FirstAsync();
                    sightseeingTariffBeforUpdate = sightseeingTariff.Clone() as VisitTariff;
                    sightseeingTariffBeforUpdate.UpdatedAt = DateTime.MinValue;
                    sightseeingTariff.Name = "Changed name";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.UpdateAsync(sightseeingTariff);

                    ((DateTime)result.UpdatedAt).Should().NotBeSameDateAs(DateTime.MinValue);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_not_first_time__Updated_element_should_have_new_updated_at_date_after_previous_one()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff = await context.VisitTariffs.FirstAsync();
                    sightseeingTariffBeforUpdate = sightseeingTariff.Clone() as VisitTariff;
                    sightseeingTariffBeforUpdate.UpdatedAt = DateTime.UtcNow.AddMinutes(-30);
                    sightseeingTariff.Name = "Changed name";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.UpdateAsync(sightseeingTariff);

                    ((DateTime)result.UpdatedAt).Should().BeAfter((DateTime)sightseeingTariffBeforUpdate.UpdatedAt);
                }
            }
        }

        #endregion


        #region RestrictedUpdateAsync(SightseeingTariff tariff)
        // wszystko co w normalnym update
        // proba zmian readonly properties -> metoda niczego nie zmieni i zaloguje warny

        [Test]
        public async Task RestrictedUpdateAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs = null as DbSet<VisitTariff>;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedUpdateAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop SightseeingTariffs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [VisitTariffs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedUpdateAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitTariff. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedUpdateAsync(null as VisitTariff);

                    await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'VisitTariff' cannot be null.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Argument_has_null_or_empty_id__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var invalidSightseeingTariff = new VisitTariff { Id = id };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedUpdateAsync(invalidSightseeingTariff);

                    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'VisitTariff' has null or empty id which is invalid.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariffBeforUpdate = await context.VisitTariffs.FirstAsync();
                    sightseeingTariff = sightseeingTariffBeforUpdate.Clone() as VisitTariff;
                    context.VisitTariffs.RemoveRange(await context.VisitTariffs.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedUpdateAsync(sightseeingTariff);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because esource is empty.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Matching_SightseeingTariff_not_found__Should_throw_InvalidOperationException()
        {
            VisitTariff sightseeingTariff = new VisitTariff
            {
                Id = "0",
                Name = "Sample name for only this test.",
                UpdatedAt = DateTime.Now.AddHours(-3)
            };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    // In db does not matching VisitTariff to belowe disount.
                    Func<Task> result = async () => await service.RestrictedUpdateAsync(sightseeingTariff);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching VisitTariff not found.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Should_return_updated_sightseeing_tariff()
        {
            VisitTariff sightseeingTariffBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariffBeforUpdate = await context.VisitTariffs.FirstAsync();
                    VisitTariff sightseeingTariff = sightseeingTariffBeforUpdate;
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(sightseeingTariff);

                    result.Should().BeEquivalentTo(sightseeingTariff);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Resource_should_contains_updated_sightseeing_tariff()
        {
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff = await context.VisitTariffs.FirstAsync();
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(sightseeingTariff);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Contains(sightseeingTariff).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_sightseeing_tariff()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff = await context.VisitTariffs.FirstAsync();
                    sightseeingTariffBeforUpdate = sightseeingTariff.Clone() as VisitTariff;
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(sightseeingTariff);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Single(x => x == sightseeingTariff).Should().NotBeSameAs(sightseeingTariffBeforUpdate);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariffBeforUpdate = await context.VisitTariffs.FirstAsync();
                    sightseeingTariff = sightseeingTariffBeforUpdate;
                    sightseeingTariff.Name = "Changed name.";
                    var service = new VisitTariffDbService(context, _logger);
                    expectedLength = await context.VisitTariffs.CountAsync();

                    await service.RestrictedUpdateAsync(sightseeingTariff);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful_first_time__Updated_element_should_have_updated_at_not_set_to_MaxValue()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff = await context.VisitTariffs.FirstAsync();
                    sightseeingTariffBeforUpdate = sightseeingTariff.Clone() as VisitTariff;
                    sightseeingTariffBeforUpdate.UpdatedAt = DateTime.MinValue;
                    sightseeingTariff.Name = "Changed name";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(sightseeingTariff);

                    ((DateTime)result.UpdatedAt).Should().NotBeSameDateAs(DateTime.MinValue);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful_not_first_time__Updated_element_should_have_new_updated_at_date_after_previous_one()
        {
            VisitTariff sightseeingTariffBeforUpdate;
            VisitTariff sightseeingTariff;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingTariff = await context.VisitTariffs.FirstAsync();
                    sightseeingTariffBeforUpdate = sightseeingTariff.Clone() as VisitTariff;
                    sightseeingTariffBeforUpdate.UpdatedAt = DateTime.UtcNow.AddMinutes(-30);
                    sightseeingTariff.Name = "Changed name";
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(sightseeingTariff);

                    ((DateTime)result.UpdatedAt).Should().BeAfter((DateTime)sightseeingTariffBeforUpdate.UpdatedAt);
                }
            }
        }

        [TestCaseSource(nameof(_tariffForRestrictedUpdateCases))]
        public async Task RestrictedUpdateAsync__Attempt_to_update_readonly_properties__These_changes_will_be_ignored(VisitTariff updatedTariffCase)
        {
            // In fact, any read-only property changes will be ignored and no exception will be thrown, but the method will log any of these changes as warning.

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var tariffBeforeUpdate = await context.VisitTariffs.FirstAsync();
                    updatedTariffCase.Id = tariffBeforeUpdate.Id;
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(updatedTariffCase);

                    // Those properties should be unchanged since they are readonly.
                    result.CreatedAt.Should().BeSameDateAs(tariffBeforeUpdate.CreatedAt);
                    result.ConcurrencyToken.Should().BeSameAs(tariffBeforeUpdate.ConcurrencyToken);
                }
            }
        }
        #endregion


        #region DeleteAsync(string id)
        // zasob jest nullem -> internal
        // zasob nie istnieje -> intern
        // id jest nullem albo pusty -> arg exc
        // zasob jest pusty -> invalid oper exc
        // nie ma takiego biletu jak podany -> invalid oper exc
        // usuwanie sie udalo -> nie w zasobie usunietego biletu
        // usuwanie sie udalo -> rozmiar zasobu jest o jeden mniejszy

        [Test]
        public async Task DeleteAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs = null as DbSet<VisitTariff>;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.DeleteAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop SightseeingTariffs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [VisitTariffs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.DeleteAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitTariff. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new VisitTariffDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.DeleteAsync(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Test]
        public async Task DeleteAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.RemoveRange(await context.VisitTariffs.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> result = async () => await service.DeleteAsync("1");

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot delete any element.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__SightseeingTariff_to_be_deleted_not_found__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    // Only positive Id is valid so there is not Id = '-100'.
                    Func<Task> result = async () => await service.DeleteAsync("-100");

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because VisitTariff to be deleted not found.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Delete_successful__Resources_length_should_be_less_by_1()
        {
            string id;
            int expectedLength;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.VisitTariffs.Count() - 1;
                    VisitTariff SightseeingTariffToBeDeleted = context.VisitTariffs.First();
                    id = SightseeingTariffToBeDeleted.Id;
                    var service = new VisitTariffDbService(context, _logger);
                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Delete_successful__Resources_should_not_contain_deleted_sightseeing_tariff()
        {
            string id;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitTariff SightseeingTariffToBeDeleted = context.VisitTariffs.First();
                    id = SightseeingTariffToBeDeleted.Id;
                    var service = new VisitTariffDbService(context, _logger);

                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
                }
            }
        }

        #endregion


        #region GetWithPagination(int pageNumber, int pageSize)
        // zasob jest nullem -> internal
        // zasob nie istnieje -> internal
        // page number/ size jest nullem -> argument null exc
        // page nie moze byc < 1 -> arg out of range
        // size nie moze byc < 0 -> arg out of range
        // przypadek: jest 201 elementow, bierzemy nr 3 i size 100 el, to skip 200 i 1 el sie wyswietli -> zwracamy liste z tym jednym elementem
        // jest 200 ele nr jest 3, size 100 -> pusta lista
        // tabela jest pusta -> jw

        [Test]
        public async Task GetWithPaginationAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs = null as DbSet<VisitTariff>;
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetWithPaginationAsync(1, 1);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop SightseeingTariffs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [VisitTariffs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    Func<Task> action = async () => await service.GetWithPaginationAsync(1, 1);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitTariff. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_number_is_less_than_1__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new VisitTariffDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetWithPaginationAsync(0, 10);

            await action.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because page number cannot be less than 1.");
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_size_is_negative__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new VisitTariffDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetWithPaginationAsync(3, -10);

            await action.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because page size cannot be negative number.");
        }

        [Test]
        public async Task GetWithPaginationAsync__Number_of_elements_on_single_page_is_less_than_page_size__Should_return_all_these_elements()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.RemoveRange(await context.VisitTariffs.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await context.VisitTariffs.AddAsync(new VisitTariff { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    // Page will be first with 6 elements and second with 4 elements. Second page will be return.
                    var result = await service.GetWithPaginationAsync(2, 6);

                    // More generic solution is: Count() should be count of elements in resource % pageSize.
                    result.Count().Should().Be(4);
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__There_are_not_elements_on_single_page__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await context.VisitTariffs.AddAsync(new VisitTariff { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    // In this case is only 2 pages with any data.
                    var result = await service.GetWithPaginationAsync(4, 5);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Resource_is_empty__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.VisitTariffs.RemoveRange(await context.VisitTariffs.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);

                    var result = await service.GetWithPaginationAsync(4, 5);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Found_any_sightseeing_tariff__Should_return_these_elements_with_not_null_ticket_tariffs()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitTariffDbService(context, _logger);
                    int elementsCount = await context.VisitTariffs.CountAsync();

                    var result = await service.GetWithPaginationAsync(1, elementsCount);

                    foreach (var sightseeingTariff in result)
                    {
                        sightseeingTariff.TicketTariffs.Should().NotBeNull();
                    }
                }
            }
        }

        #endregion
    }
}

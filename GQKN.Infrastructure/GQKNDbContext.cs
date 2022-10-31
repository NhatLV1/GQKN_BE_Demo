using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using PVI.GQKN.Infrastructure.Idempotency;

namespace PVI.GQKN.Infrastructure;

public class GQKNDbContext: ApiAuthorizationDbContext<ApplicationUser>, IUnitOfWork
{
    public DbSet<PhongBan> PhongBan { get; set; }
    public DbSet<ChucDanh> ChucVu { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationUserRoles { get; set; }

    public DbSet<BieuMau> BieuMau { get; set; }

    public DbSet<DonVi> DonVi { get; set; }

    public DbSet<KhaiBaoTonThat> KhaiBaoTonThat { get; set; }

    public DbSet<TaiLieu> TaiLieu { get; set; }

    public DbSet<TaiLieuBookMark> TaiLieuBookMark { get; set; }

    public DbSet<ThuMuc> ThuMuc { get; set; }

    public const string DEFAULT_SCHEMA = "gqkn";

    private readonly IMediator _mediator;

    private IDbContextTransaction _currentTransaction;

    //public GQKNDbContext(DbContextOptions<GQKNDbContext> options) : base(options) { }

    //public GQKNDbContext(DbContextOptions<GQKNDbContext> options, IMediator mediator) : base(options)
    //{
    //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


    //    System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
    //}

    public GQKNDbContext(
         DbContextOptions<GQKNDbContext> options,
         IOptions<OperationalStoreOptions> operationalStoreOptions,
         IMediator mediator) : base(options, operationalStoreOptions)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }


    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;


    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        UpdatedDate();
        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ApplicationUserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PhongBanEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ChucDanhEntityConfiguration());

        //modelBuilder.ApplyConfiguration(new ChiNhanhEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new KhaiBaoTonThatEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DonViEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new BieuMauEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BieuMauEmailEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new HoSoTonThatEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new TaiLieuEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TaiLieuBookMarkEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ThuMucEntityTypeConfiguration());

        //modelBuilder.ApplyConfiguration(new DoiTacEntityTypeConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdatedDate();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdatedDate()
    {
        var now = DateTime.Now;
        var items = ChangeTracker.Entries<Entity>().Where(e => e.State == EntityState.Modified);
        foreach (var item in ChangeTracker.Entries<Entity>().Where(e => e.State == EntityState.Modified))
        {
            item.Entity.NgaySua = now;
            // TODO cần thêm người sửa vào đây
        }
    }

    public override int SaveChanges()
    {
        //ChangeTracker.DetectChanges();
        UpdatedDate();

        return base.SaveChanges();
    }
}